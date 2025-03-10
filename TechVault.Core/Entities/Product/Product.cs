using System.ComponentModel.DataAnnotations.Schema;

namespace TechVault.Core.Entities.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Photo>Photos { get; set; }
    }

}
