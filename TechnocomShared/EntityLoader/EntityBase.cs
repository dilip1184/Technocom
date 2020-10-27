using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnocomShared.DataAccess;
using TechnocomShared.Exceptions;
using TechnocomShared.Logging;

namespace TechnocomShared.EntityLoader
{
    public abstract class EntityBase
    {
        /// <summary>
        /// Loads the PropertyMappingInfo collection for the type specified by objType from the cache, or creates the
        /// collection and adds it to the cache if it does not exist.
        /// </summary>
        /// <param name="objType">Type to load the properties for.</param>
        /// <returns>
        /// A collection of PropertyMappingInfo objects that are associated with the Type.
        /// </returns>
        private static IList<PropertyMappingInfo> LoadPropertyMappingInfo(Type objType)
        {
            return objType.GetProperties().Select(info => new PropertyMappingInfo(info)).ToList();

            //return (from info in objType.GetProperties()
            //        let mapAttr = (DataMappingAttribute) Attribute.GetCustomAttribute(info, typeof (DataMappingAttribute))
            //        where mapAttr != null
            //        select new PropertyMappingInfo(mapAttr.DataFieldName, mapAttr.NullValue, info)).ToList();
        }

        private static int[] LoadOrdinalsInfo(IList<PropertyMappingInfo> propClassList, IDataRecord dr)
        {
            var ordinals = new int[propClassList.Count];

            if (dr != null)
            {
                for (var i = 0; i <= propClassList.Count - 1; i++)
                {
                    ordinals[i] = -1;
                    try
                    {
                        ordinals[i] = dr.GetOrdinal(propClassList[i].PropertyInfo.Name);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // Field name does not exist in the datareader.
                    }
                }
            }

            return ordinals;
        }

        /// <summary>
        /// Loads the PropertyMappingInfo collection for type specified.
        /// </summary>
        /// <param name="objType">Type that contains the properties to load.</param>
        /// <returns>
        /// A collection of PropertyMappingInfo objects that are associated with the Type.
        /// </returns>
        private static IList<PropertyMappingInfo> GetProperties(Type objType)
        {
            var info = MappingInfoCache.GetCache(objType.Name);

            if (info == null)
            {
                info = LoadPropertyMappingInfo(objType);
                MappingInfoCache.SetCache(objType.Name, info);
            }
            return info;
        }

        /// <summary>
        /// Returns an array of integers that correspond to the index of the matching field in the PropertyInfoCollection.
        /// </summary>
        /// <param name="propClassList">PropertyMappingInfo Collection</param>
        /// <param name="dr">DataReader</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>
        /// Array of integers that correspond to the field's index position in the datareader for each one of the PropertyMappingInfo objects.
        /// </returns>
        private static int[] GetOrdinals(IList<PropertyMappingInfo> propClassList, IDataRecord dr,
                                         string storedProcedureName)
        {
            var info = OrdinalsCache.GetCache(storedProcedureName);

            if (info == null)
            {
                info = LoadOrdinalsInfo(propClassList, dr);
                OrdinalsCache.SetCache(storedProcedureName, info);
            }
            return info;
        }

        /// <summary>
        /// Iterates through the object type's properties and attempts to assign the value from the datareader to
        /// the matching property.
        /// </summary>
        /// <typeparam name="T">The type of object to populate.</typeparam>
        /// <param name="dr">The IDataReader that contains the data to populate the object with.</param>
        /// <param name="propInfoList">List of PropertyMappingInfo objects.</param>
        /// <param name="ordinals">Array of integers that indicate the index into the IDataReader to get the value from.</param>
        /// <returns>
        /// A populated instance of type T
        /// </returns>
        private static T CreateObject<T>(IDataRecord dr,
                                         IList<PropertyMappingInfo> propInfoList, IList<int> ordinals) where T : class, new()
        {
            var obj = new T();

            // iterate through the PropertyMappingInfo objects for this type.
            for (var i = 0; i <= propInfoList.Count - 1; i++)
            {
                if (!propInfoList[i].PropertyInfo.CanWrite) continue;
                var type = propInfoList[i].PropertyInfo.PropertyType;
                var value = propInfoList[i].DefaultValue;

                if (ordinals[i] != -1 && dr.IsDBNull(ordinals[i]) == false)
                    value = dr.GetValue(ordinals[i]);

                try
                {
                    // try implicit conversion first
                    propInfoList[i].PropertyInfo.SetValue(obj, value, null);
                }
                catch
                {
                    LogWriter.GetLogWriter().Debug("DATATypes not matching" + propInfoList[i].PropertyInfo.Name +
                                                       value);
                    // data types do not match

                    try
                    {
                        // need to handle enumeration types differently than other base types.
                        propInfoList[i].PropertyInfo.SetValue(
                            obj,
                            type.BaseType.Equals(typeof (Enum))
                                ? Enum.ToObject(type, value)
                                : Convert.ChangeType(value, type), null);
                    }
                    catch
                    {
                        LogWriter.GetLogWriter().Debug("unable to assign value to" + propInfoList[i].PropertyInfo.Name +
                                                       value);
                        // error assigning the datareader value to a property
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// Fills collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProdeureName">Name of the stored prodeure.</param>
        /// <returns></returns>
        public static IList<T> FillCollection<T>(string storedProdeureName) where T : class, new()
        {
            return FillCollection<T>(storedProdeureName, new object[] {});
        }

        /// <summary>
        /// Fills collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProdeureName">Name of the stored prodeure.</param>
        /// <param name="parametrValues">The parametr values.</param>
        /// <returns></returns>
        public static IList<T> FillCollection<T>(string storedProdeureName,
                                                  params object[] parametrValues) where T : class, new()
        {

           
            var coll = new List<T>();
            try
            {
                using (var dr = DataConnection.ExecuteReader(storedProdeureName, parametrValues))
                {
                    var mapInfo = GetProperties(typeof (T));
                    var ordinals = GetOrdinals(mapInfo, dr, storedProdeureName);

                    while (dr.Read())
                        coll.Add(CreateObject<T>(dr, mapInfo, ordinals));
                }
                if (coll.Count.Equals(0))
                    throw new FinderException(storedProdeureName, parametrValues);
            }
            catch (FinderException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TechnicalException("Error executing:" + storedProdeureName, ex);
            }
            return coll;
        }

        /// <summary>
        /// Fill object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProdeureName">Name of the stored prodeure.</param>
        /// <returns></returns>
        public static T FillObject<T>(string storedProdeureName) where T : class, new()
        {
            return FillObject<T>(storedProdeureName, new object[] {});
        }

        /// <summary>
        /// Fill object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProdeureName">Name of the stored prodeure.</param>
        /// <param name="parametrValues">The parametr values.</param>
        /// <returns></returns>
        public static T FillObject<T>(string storedProdeureName, params object[] parametrValues)
            where T : class, new()
        {
            T obj = null;
            try
            {
                using (var dr = DataConnection.ExecuteReader(storedProdeureName, parametrValues))
                {
                    var mapInfo = GetProperties(typeof (T));
                    var ordinals = GetOrdinals(mapInfo, dr, storedProdeureName);
                    if (dr.Read())
                        obj = CreateObject<T>(dr, mapInfo, ordinals);
                }

                if (obj == null)
                    throw new FinderException(storedProdeureName, parametrValues);
            }
            catch (FinderException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var message = string.Empty;
                throw new TechnicalException(
                    "Stored Procedure :" + storedProdeureName + " \nParameters :" +
                    parametrValues.Where(parameter => parameter != null).Aggregate(message,
                                                                                   (current, parameter) =>
                                                                                   current + parameter.ToString()), ex);
            }
            return obj;
        }

        public static DataSet FillDataSet(string storedProdeureName, params object[] parametrValues)
        {
            return DataConnection.GetDataSet(storedProdeureName, parametrValues);
        }

        public static IList<T> FillCollectionBySQLQuery<T>(string SQLQuery) where T : class, new()
        {
            var coll = new List<T>();
            try
            {
                using (var dr = DataConnection.ExecuteReaderSQLQuery(SQLQuery))
                {
                    var mapInfo = GetProperties(typeof(T));
                    var ordinals = GetOrdinals(mapInfo, dr, SQLQuery);

                    while (dr.Read())
                        coll.Add(CreateObject<T>(dr, mapInfo, ordinals));
                }
                if (coll.Count.Equals(0))
                    throw new FinderException(SQLQuery);
            }
            catch (FinderException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TechnicalException("Error executing:" + SQLQuery, ex);
            }
            return coll;
        }
    }
}