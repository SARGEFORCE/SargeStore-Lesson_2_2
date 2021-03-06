﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SargeStoreDomain.Entities;
using SargeStoreDomain.ViewModels;
using SargeStore.Interfaces.Services;
using log4net.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace SargeStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly IConfiguration _Configuration;
        private const string __PageSize = "PageSize";

        public CatalogController(IProductData ProductData, IConfiguration Configuration)
        {
            _ProductData = ProductData;
            _Configuration = Configuration;
        }

        public IActionResult Shop(int? SectionId, int? BrandId, int Page = 1)
        {
            var page_size = int.TryParse(_Configuration["PageSize"], out var size) ? size : (int?)null;

            var products = _ProductData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = Page,
                PageSize = page_size
            });

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Brand = p.Brand?.Name
                }).OrderBy(p => p.Order),
                PageViewModel = new PageViewModel
                {
                    PageSize = page_size ?? 0,
                    PageNumber = Page,
                    TotalItems = products.TotalCount
                }
            });
        }

        public IActionResult Details(int id, [FromServices] ILogger<CatalogController> Logger)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null)
            {
                Logger.LogWarning("Запрошенный товар {0} отсутствует в каталоге", id);
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

        #region API

        public IActionResult GetFilteredItems(int? SectionId, int? BrandId, int Page)
        {
            var products = GetProducts(SectionId, BrandId, Page);
            return PartialView("Partial/_FeaturesItem", products);
        }

        public IEnumerable<ProductViewModel> GetProducts(int? SectionId, int? BrandId, int Page)
        {
            var products_model = _ProductData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = Page,
                PageSize = int.Parse(_Configuration[__PageSize])
            });

            return products_model.Products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Order = product.Order,
                Brand = product.Brand?.Name ?? string.Empty,
                ImageUrl = product.ImageUrl
            });
        }

        #endregion
    }
}