namespace CasaAsa.Data.Models
{
    public class ApplicationSetting : IEntityDefault
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
