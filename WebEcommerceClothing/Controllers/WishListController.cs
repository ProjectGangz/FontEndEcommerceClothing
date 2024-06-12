using Microsoft.AspNetCore.Mvc;

namespace WebEcommerceClothing.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult MyWishlist()
        {
            return View();
        }
    }
}
