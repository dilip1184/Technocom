using TechnocomShared.Interfaces;

namespace TechnocomShared.Entities
{
    public class OperationStatusEntity : IBusinessEntity
    {
        public virtual long ResultId { get; set; }
        public virtual string InfoMessage { get; set; }
        public virtual bool StatusResult { get; set; }
    }
}
