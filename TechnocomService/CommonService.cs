using TechnocomShared.Entities;
using TechnocomShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnocomService
{
    public class CommonService
    {
        public IList<NameValuePairData> GetBooleanType()
        {
            var response = new List<NameValuePairData>
                               {
                                   new NameValuePairData {Name = "True", Value = "1"},
                                   new NameValuePairData {Name = "False", Value = "2"}
                               };
            return response;
        }
    }
}
