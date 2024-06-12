using Microsoft.AspNetCore.Mvc;

namespace WebEcommerceClothing.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductShop()
        {
            return View();
        }
        public IActionResult ProductDetails() 
        { 
            return View();
        }
    }
}
