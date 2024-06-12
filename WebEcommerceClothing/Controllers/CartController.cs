using Microsoft.AspNetCore.Mvc;

namespace WebEcommerceClothing.Controllers
{
    public class CartController : Controller
    {
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
