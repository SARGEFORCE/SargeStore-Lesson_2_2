using SargeStoreDomain.DTO.Products;
using SargeStoreDomain.Entities;

namespace SargeStore.Services.Map
{
    public static class ProductMapper
    {
        public static ProductDTO ToDTO(this Product product) => product is null ? null : new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Order = product.Order,
            Brand = product.Brand.ToDTO()
        };

        public static Product FromDTO(this ProductDTO product) => product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Order = product.Order,
            BrandId = product.Brand?.Id,
            Brand = product.Brand.FromDTO()
        };
    }
}
