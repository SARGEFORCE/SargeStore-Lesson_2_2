using SargeStoreDomain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SargeStoreDomain.Entities
{
    public class OrderItem : BaseEntity
    {
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
