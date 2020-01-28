using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SargeStore.Controllers
{

    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;

        //public HomeController(IConfiguration Configuration) => _Configuration = Configuration;
        public IActionResult Index() => View();
        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult Cart() => View();
        public IActionResult CheckOut() => View();
        public IActionResult ContactUs() => View();
        public IActionResult Login() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Shop() => View();
        public IActionResult Error404() => View();

        //public IActionResult ThrowException() => throw new ApplicationException("Тестовая ошибка");
    }
}