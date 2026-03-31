namespace CasaAsa.Data.Models
{
    public class OrderListSummary : IAuditEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string FullName { get; set; }
        public int TotalOrders { get; set; }
        public int OrderStatusId { get; set; }

        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
