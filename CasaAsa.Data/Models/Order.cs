namespace CasaAsa.Data.Models
{
    public class Order : IEntityDefault
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public int OrderedBy { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<OrderListSummary> OrderListSummaries { get; set; }
    }
}
