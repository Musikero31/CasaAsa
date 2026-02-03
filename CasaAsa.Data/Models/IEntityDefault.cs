using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasaAsa.Data.Models
{
    public interface IEntityDefault
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int Id { get; set; }
        bool ActiveStatus { get; set; }
        Guid CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        Guid? UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}