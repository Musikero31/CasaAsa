namespace CasaAsa.Core.Common
{
    public abstract class AuditEntity
    {
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
