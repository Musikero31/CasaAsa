
namespace CasaAsa.Data.Entities
{
    public class MenuOfTheDayDetail : IAuditEntity
    {
        public int Id { get; set; }
        public int MenuOfTheDayId { get; set; }
        public MenuOfTheDay? MenuOfTheDay { get; set; }
        public int MenuDetailId { get; set; }
        public MenuDetail? MenuDetail { get; set; }
        /// <summary>
        /// This is to be used on whether the menu is available or sold out
        /// </summary>
        public bool IsAvailable { get; set; }
        public int AvailableServings { get; set; }

        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
