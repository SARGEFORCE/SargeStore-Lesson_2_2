using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SargeStore.Interfaces.Services;
using SargeStoreDomain.ViewModels;
using System.Linq;

namespace SargeStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Orders([FromServices] IOrderService OrderService)
        {

            return View(OrderService
                .GetUserOrders(User.Identity.Name)
                .Select(order => new UserOrderViewModel
                { 
                    Id = order.Id,
                    Name = order.Name, 
                    Address = order.Address,
                    Phone = order.Phone,
                    TotalSum = order.OrderItems.Sum(item => item.Price * item.Quantity)
                }));
        }
    }
}