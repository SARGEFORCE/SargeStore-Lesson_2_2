using SargeStoreDomain.Entities.Base;
using SargeStoreDomain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SargeStoreDomain.Entities
{
    [Table("Brands")]
    /// <summary>Брэнд</summary>
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
