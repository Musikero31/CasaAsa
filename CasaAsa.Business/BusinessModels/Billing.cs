namespace CasaAsa.Business.BusinessModels
{
    public class Billing
    {
        public int BillingId { get; set; }
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmountDue { get; set; }
        public bool IsPaid { get; set; }
        public decimal SubTotal { get; set; }
        public decimal AdditionalCharge { get; set; }
        public string? Reason { get; set; }
        public decimal DeliveryCharge { get; set; }
        public int UserID { get; set; }
    }
}
