using System.Reflection;

namespace TechnocomShared.EntityLoader
{
    internal sealed class PropertyMappingInfo
    {
        private readonly object _defaultValue;
        private readonly PropertyInfo _propInfo;
        private string _dataFieldName;

        internal PropertyMappingInfo()
            : this(string.Empty, null, null)
        {
        }

        internal PropertyMappingInfo(string dataFieldName, object defaultValue, PropertyInfo info)
        {
            _dataFieldName = dataFieldName;
            _defaultValue = defaultValue;
            _propInfo = info;
        }

        internal PropertyMappingInfo(PropertyInfo info)
        {
            _propInfo = info;
        }
        public string DataFieldName
        {
            get
            {
                if (string.IsNullOrEmpty(_dataFieldName))
                {
                    _dataFieldName = _propInfo.Name;
                }
                return _dataFieldName;
            }
        }
        public object DefaultValue
        {
            get { return _defaultValue; }
        }
        public PropertyInfo PropertyInfo
        {
            get { return _propInfo; }
        }
    }
}