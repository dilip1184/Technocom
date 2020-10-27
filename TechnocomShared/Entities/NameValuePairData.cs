using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class NameValuePairData : IBusinessEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}