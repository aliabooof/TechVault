using System.ComponentModel.DataAnnotations.Schema;

namespace TechVault.Core.Entities.Product
{
    public class Photo : BaseEntity<int>
    {
        public string ImageName { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        //public virtual Product Product{ get; set; }
    }
}
