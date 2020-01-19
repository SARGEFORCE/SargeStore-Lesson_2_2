using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using SargeStore.Interfaces.Services;
using SargeStoreDomain.DTO.Products;
using SargeStoreDomain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SargeStore.Services.FProduct
{
    public class SqlProductData : IProductData //Unit of work
    {
        public SargeStoreDB _db;

        public SqlProductData(SargeStoreDB db) => _db = db;

        public IEnumerable<Section> GetSections() => _db.Sections
            .Include(section => section.Products)
            .AsEnumerable();

        public IEnumerable<Brand> GetBrands() => _db.Brands
            .Include(brand => brand.Products)
            .AsEnumerable();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            return query
                .AsEnumerable()
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Brand = p.Brand is null ? null: new BrandDTO
                    {
                        Id = p.Brand.Id,
                        Name = p.Brand.Name
                    }
                });
                
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == id);
            return product is null? null : new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand is null ? null : new BrandDTO
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name
                }
            };
        }
    }
}
