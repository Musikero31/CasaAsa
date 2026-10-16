namespace CasaAsa.Data.Entities
{
    public class MenuOfTheDay : IAuditEntity
    {
        public int Id { get; set; }
        public DateTime MenuDate { get; set; }
        public DateTime LockDate { get; set; }
        public bool IsActivated { get; set; }
        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<MenuOfTheDayDetail>? MenuOfTheDayDetails { get; set; }
    }
}
