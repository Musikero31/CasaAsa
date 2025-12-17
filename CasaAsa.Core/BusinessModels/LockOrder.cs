using CasaAsa.Core.Common;

namespace CasaAsa.Core.BusinessModels
{
    public class LockOrder : AuditEntity
    {
        public DateOnly LockDate { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
