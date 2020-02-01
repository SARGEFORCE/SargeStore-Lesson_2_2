using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SargeStoreDomain.Entities;
using SargeStoreDomain.ViewModels;
using SargeStore.Interfaces.Services;
using log4net.Core;
using Microsoft.Extensions.Logging;

namespace SargeStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        public CatalogController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Shop(int? SectionId, int? BrandId)
        {
            var products = _ProductData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId
            });

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Brand =p.Brand.Name
                }).OrderBy(p => p.Order)
            });
        }

        public IActionResult Details(int id, [FromServices] ILogger<CatalogController> Logger)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null)
            {
                Logger.LogWarning("Запрошенный товар {0} не найден в каталоге", id);
                return NotFound();
            }

            Logger.LogInformation("Товар {0} найден: {1}", id, product.Name);

            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Brand = product.Brand?.Name
            });
        }
    }
}