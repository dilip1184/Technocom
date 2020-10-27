using System;
using System.Linq;

namespace TechnocomShared.Exceptions
{
    public class FinderException : BusinessException
    {
        public FinderException()
        {
        }

        public FinderException(string message)
            : base(message)
        {
        }

        public FinderException(string storedProcedure, object[] parameters)
        {
            var message = string.Empty;
            message = "Finder Exception Stored Procedure :" + storedProcedure + " Parameters :" +
                      parameters.Where(parameter => parameter != null).Aggregate(message,
                                                                                 (current, parameter) =>
                                                                                 current + parameter.ToString());
            Log(message);
        }

    }
}