using SargeStoreDomain.DTO.Products;
using SargeStoreDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SargeStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        Section GetSectionById(int id);
        IEnumerable<Brand> GetBrands();
        Brand GetBrandById(int id);
        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null);
        ProductDTO GetProductById(int id);
    }
}
