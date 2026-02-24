using CasaAsa.Core.Common;

namespace CasaAsa.Core.BusinessModels
{
    public class MenuCategories : AuditEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
