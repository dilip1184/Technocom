using TechnocomShared.Configuration;
using TechnocomShared.Constants;
using TechnocomShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnocomShared.Helpers
{
    public static class DataHelper
    {
        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList">The source list.</param>
        /// <returns></returns>
        public static T GetObject<T>(IEnumerable<IBusinessEntity> sourceList)
        {
            return (T)sourceList.First(x => x.GetType().Equals(typeof(T)));
        }

        public static IList<T> GetListLevelZero<T>(IEnumerable<IBusinessEntity> sourceList)
        {
            return sourceList.Where(entity => entity.GetType() == typeof(T)).Select(entity => (T)entity).ToList();
        }
                
        /// <summary>
        /// Determines whether [is valid date] [the specified date].
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        ///   <c>true</c> if [is valid date] [the specified date]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDate(string date)
        {
            DateTime result;
            return DateTime.TryParse(date, out result);
        }

        /// <summary>
        /// Will return the name 
        /// </summary>
        /// <param name="fName">First Name</param>
        /// <param name="mName">Middle Name</param>
        /// <param name="lName">Last Name</param>
        /// <returns></returns>
        public static string DisplayNameFormat(string fName, string mName, string lName)
        {
            var displayNameFormat = AppConfigurationHelper.GetValue<string>(ConfigKeys.DisplayNameFormat);
            return displayNameFormat.Replace("<L>", lName).Replace("<F>", fName).Replace("<M>", mName != string.Empty ? mName : string.Empty);
        }
    }
}
