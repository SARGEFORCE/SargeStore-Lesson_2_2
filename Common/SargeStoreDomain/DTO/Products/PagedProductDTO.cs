using System.Collections.Generic;

namespace SargeStoreDomain.DTO.Products
{
    public class PagedProductDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; }

        public int TotalCount { get; set; }
    }
}