using Microsoft.AspNetCore.Mvc;

namespace SargeStore.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity.IsAuthenticated
            ? View("UserInfoView")
            : View();
    }
}
