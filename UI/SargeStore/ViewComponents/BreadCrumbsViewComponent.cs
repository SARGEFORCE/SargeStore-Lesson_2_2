using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SargeStoreDomain.DTO.Products;
using SargeStoreDomain.ViewModels.BreadCrumbs;
using SargeStore.Interfaces.Services;

namespace SargeStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BreadCrumbsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(BreadCrumbsType Type, int id, BreadCrumbsType FromType)
        {
            switch (Type)
            {
                default: return View(Array.Empty<BreadCrumbViewModel>());

                case BreadCrumbsType.Section:
                    return View(new[]
                    {
                        new BreadCrumbViewModel
                        {
                            BreadCrumbsType = BreadCrumbsType.Section,
                            Id = id.ToString(),
                            Name = _ProductData.GetSectionById(id).Name
                        }
                    });

                case BreadCrumbsType.Brand:
                    return View(new[]
                    {
                        new BreadCrumbViewModel
                        {
                            BreadCrumbsType = BreadCrumbsType.Brand,
                            Id = id.ToString(),
                            Name = _ProductData.GetBrandById(id).Name
                        }
                    });

                case BreadCrumbsType.Product:
                    return View(GetProductBreadCrumbs(_ProductData.GetProductById(id), FromType));
            }
        }

        private IEnumerable<BreadCrumbViewModel> GetProductBreadCrumbs(ProductDTO Product, BreadCrumbsType FromType)
            => new[]
            {
                new BreadCrumbViewModel
                {
                    BreadCrumbsType = FromType,
                    Id = FromType == BreadCrumbsType.Section
                         ? Product.Section.Id.ToString()
                         : Product.Brand.Id.ToString(),
                    Name = FromType == BreadCrumbsType.Section
                           ? Product.Section.Name
                           : Product.Brand.Name
                },
                new BreadCrumbViewModel
                {
                    BreadCrumbsType = BreadCrumbsType.Product,
                    Id = Product.Id.ToString(),
                    Name = Product.Name
                },
            };
    }
}
