using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TechnocomShared.Configuration;
using TechnocomShared.Exceptions;
using TechnocomShared.Logging;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace TechnocomShared.DataAccess
{
    public static class DataConnection
    {
        private static readonly SqlDatabase Db = (SqlDatabase)DatabaseFactory.CreateDatabase();
        public static void ExecuteSQLQuery(string queryText)
        {
            System.Data.Common.DbCommand cmd = Db.GetSqlStringCommand(queryText);
            cmd.CommandTimeout = 0;
            Db.ExecuteNonQuery(cmd);
        }
        private static int ExecuteNonQuery(string storedProdeureName, params object[] parametrValues)
        {
            System.Data.Common.DbCommand cmd = Db.GetStoredProcCommand(storedProdeureName, parametrValues);
            cmd.CommandTimeout = 0;
            return Db.ExecuteNonQuery(cmd);
        }
        public static void ExecuteBulkLoad(object parametrValue, string spname)
        {
            var startTime = DateTime.Now;
            var cmd = Db.GetStoredProcCommand(spname);
            Db.AddInParameter(cmd, "@Aliases", SqlDbType.Structured, parametrValue);
            Db.ExecuteNonQuery(cmd);
            LogPe(spname, startTime);
        }
        public static void EExecuteNonQuery(string storedProdeureName, params object[] parametrValues)
        {
            var startTime = DateTime.Now;
            try
            {
                Log(storedProdeureName, parametrValues);
                var result = ExecuteNonQuery(storedProdeureName, parametrValues);
            }
            catch (Exception ex)
            {
                throw new TechnicalException("", ex);
            }
            finally
            {
                LogPe(storedProdeureName, startTime, parametrValues);
            }
        }
        public static object EExecuteScalarSendAlert(string storedProdeureName, params object[] parametrValues)
        {
            var startTime = DateTime.Now;
            try
            {
                Log(storedProdeureName, parametrValues);
                var result = ExecuteScalarSendAlert(storedProdeureName, parametrValues);

                if (result == null)
                    result = 1;
                return result;
            }
            catch (Exception ex)
            {
                throw new TechnicalException("", ex);
            }
            finally
            {
                LogPe(storedProdeureName, startTime, parametrValues);
            }
        }
        private static object ExecuteScalarSendAlert(string storedProdeureName, params object[] parametrValues)
        {
            System.Data.Common.DbCommand cmd = Db.GetStoredProcCommand(storedProdeureName);

            Db.AddInParameter(cmd, "@date", DbType.Date, parametrValues[0]);
            Db.AddInParameter(cmd, "@time", DbType.Time, parametrValues[1]);
            Db.AddInParameter(cmd, "@alertMessage", DbType.String, parametrValues[2]);
            Db.AddInParameter(cmd, "@alertType", DbType.String, parametrValues[3]);
            Db.AddInParameter(cmd, "@alertCategoryID", DbType.Int32, parametrValues[4]);
            Db.AddInParameter(cmd, "@UserEntityIds", DbType.String, parametrValues[5]);
            Db.AddInParameter(cmd, "@ExecutedBy", DbType.String, parametrValues[6]);
            Db.AddInParameter(cmd, "@IsUnicode", DbType.Boolean, parametrValues[7]);

            cmd.CommandTimeout = 0;
            return Db.ExecuteScalar(cmd);
        }
        private static object ExecuteScalar(string storedProdeureName, params object[] parametrValues)
        {
            System.Data.Common.DbCommand cmd = Db.GetStoredProcCommand(storedProdeureName, parametrValues);
            cmd.CommandTimeout = 0;
            return Db.ExecuteScalar(cmd);
        }
        public static object ExecuteScalar(string storedProdeureName, SqlParameter parameter)
        {
            SqlConnection connection = new SqlConnection(Db.ConnectionString);

            SqlCommand sqlCommand = new SqlCommand(storedProdeureName, connection);
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection.Open();
            object retValue = sqlCommand.ExecuteScalar();
            sqlCommand.Connection.Close();
            return retValue;
            // return Db.ExecuteScalar(storedProdeureName, parametrValues);
        }
        public static object EExecuteScalar(string storedProdeureName, params object[] parametrValues)
        {
            var startTime = DateTime.Now;
            try
            {
                Log(storedProdeureName, parametrValues);
                var result = ExecuteScalar(storedProdeureName, parametrValues);

                if (result == null)
                    result = 1;
                return result;
            }
            catch (Exception ex)
            {
                throw new TechnicalException("", ex);
            }
            finally
            {
                LogPe(storedProdeureName, startTime, parametrValues);
            }
        }
        private static void Log(string storedProdeureName, params object[] parametrValues)
        {
            try
            {
                return;
                //var message = string.Empty;
                //message = "Executing Stored Procedure :" + storedProdeureName + " Parameters :" +
                //    parametrValues.Aggregate(message, (current, parameter) => current + "," + (parameter == null ? "NULL" : parameter.ToString()));
                //LogWriter.GetLogWriter().Debug(message);
            }
            catch
            {
            }
        }
        private static void LogPe(string storedProdeureName, DateTime startTime, params object[] parametrValues)
        {
            try
            {
                var endTime = DateTime.Now;
                var message = string.Empty;
                message = "Executed Stored Procedure :" + storedProdeureName + " Parameters :" +
                    parametrValues.Aggregate(message, (current, parameter) => current + "," + (parameter == null ? "NULL" : parameter.ToString()));
                var timeSpan = endTime - startTime;

                message += "!StartTime:" + startTime.ToString("HH:mm:ss.ffff") + "!EndTime:" + endTime.ToString("HH:mm:ss.ffff") + "!Duration:" + (timeSpan.TotalMinutes + timeSpan.TotalSeconds);
                LogWriter.GetLogWriter().Log(message);
            }
            catch
            {
            }
        }
        public static IDataReader ExecuteReader(string storedProdeureName, params object[] parametrValues)
        {
            var startTime = DateTime.Now;
            Log(storedProdeureName, parametrValues);
            System.Data.Common.DbCommand cmd = Db.GetStoredProcCommand(storedProdeureName, parametrValues);
            cmd.CommandTimeout = 0;
            var result = Db.ExecuteReader(cmd);
            LogPe(storedProdeureName, startTime, parametrValues);
            return result;
        }
        public static IDataReader ExecuteReaderSQLQuery(string SQLQuery)
        {
            var startTime = DateTime.Now;
            System.Data.Common.DbCommand cmd = Db.GetSqlStringCommand(SQLQuery);
            cmd.CommandTimeout = 0;
            var result = Db.ExecuteReader(cmd);
            return result;
        }

        public static DataSet GetDataSet(string storedProdeureName, params object[] parametrValues)
        {
            var startTime = DateTime.Now;
            Log(storedProdeureName, parametrValues);
            System.Data.Common.DbCommand cmd = Db.GetStoredProcCommand(storedProdeureName, parametrValues);
            cmd.CommandTimeout = 0;
            var result = Db.ExecuteDataSet(cmd);
            LogPe(storedProdeureName, startTime, parametrValues);
            return result;
        }
    }
}