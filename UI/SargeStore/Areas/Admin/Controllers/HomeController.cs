using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SargeStore.Interfaces.Services;
using SargeStoreDomain.Entities.Identity;

namespace SargeStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class HomeController : Controller
    {
        private readonly IProductData _ProductData;
        public HomeController(IProductData ProductData) => _ProductData = ProductData;
        public IActionResult Index() => View();

        public IActionResult ProductList() => View(_ProductData.GetProducts());

        public IActionResult Edit(int? id) => View();
        public IActionResult Delete(int id) => View();

        [HttpPost, ActionName(nameof(Delete))]
        public IActionResult DeleteConfirm(int id) => RedirectToAction("ProductList");
    }
}