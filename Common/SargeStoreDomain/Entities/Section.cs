using SargeStoreDomain.Entities.Base;
using SargeStoreDomain.Entities.Base.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SargeStoreDomain.Entities
{
    /// <summary>Секция</summary>
    [Table("Sections")]
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Section ParentSection { get; set; }        
        public virtual ICollection<Product> Products { get; set; }
    }
}
