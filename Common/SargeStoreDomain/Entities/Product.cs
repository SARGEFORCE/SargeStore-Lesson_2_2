using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SargeStoreDomain.Entities.Base;
using SargeStoreDomain.Entities.Base.Interfaces;

namespace SargeStoreDomain.Entities
{
    //[Table("Products")]
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }
        
        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; } //автоматически генерируемый внешний ключ Brand_ID
        public int? BrandId { get; set; }

        public string ImageUrl { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}