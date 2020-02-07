using Microsoft.AspNetCore.Mvc;
using SargeStore.Interfaces.Services;

namespace SargeStore.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _CartService;
        public CartViewComponent(ICartService CartService) => _CartService = CartService;

        public IViewComponentResult Invoke() => View(_CartService.TransformFromCart());
    }
}