namespace CasaAsa.Data.Models
{
    public interface IEntity
    {
        bool ActiveStatus { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}