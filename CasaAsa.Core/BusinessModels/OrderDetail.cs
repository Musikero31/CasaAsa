using CasaAsa.Core.Common;

namespace CasaAsa.Core.BusinessModels
{
    public class OrderDetail : AuditEntity
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Remarks { get; set; }
    }
}
