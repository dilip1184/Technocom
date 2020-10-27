using System;

namespace TechnocomShared.EntityLoader
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DataMappingAttribute : Attribute
    {

        private readonly string _dataFieldName;
        private readonly object _nullValue;
        public DataMappingAttribute(string dataFieldName, object nullValue)
        {
            _dataFieldName = dataFieldName;
            _nullValue = nullValue;
        }

        public DataMappingAttribute(object nullValue) : this(string.Empty, nullValue)
        {
        }
        public string DataFieldName
        {
            get { return _dataFieldName; }
        }

        public object NullValue
        {
            get { return _nullValue; }
        }
    }
}