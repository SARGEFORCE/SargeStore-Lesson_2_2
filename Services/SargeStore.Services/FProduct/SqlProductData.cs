using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using SargeStore.Interfaces.Services;
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

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            return query.AsEnumerable(); //query.ToArray();
        }

        public Product GetProductById(int id) => _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == id);
    }
}
