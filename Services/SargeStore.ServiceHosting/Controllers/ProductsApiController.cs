using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SargeStore.Interfaces.Services;
using SargeStoreDomain.DTO.Products;
using SargeStoreDomain.Entities;

namespace SargeStore.ServiceHosting.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;
        
        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands() => _ProductData.GetBrands();

        [HttpGet("{id}"), ActionName("Get")]
        public ProductDTO GetProductById(int id) => _ProductData.GetProductById(id);

        [HttpPost,ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts([FromBody]ProductFilter Filter = null) => _ProductData.GetProducts(Filter);
        
        [HttpGet("sections")]
        public IEnumerable<Section> GetSections() => _ProductData.GetSections();
    }
}