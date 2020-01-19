using System.Collections.Generic;

namespace SargeStoreDomain.Entities
{
    public class ProductFilter
    {
        public int? SectionId { get; set; }

        public int? BrandId { get; set; }

        public List<int> Ids { get; set; }
    }
}