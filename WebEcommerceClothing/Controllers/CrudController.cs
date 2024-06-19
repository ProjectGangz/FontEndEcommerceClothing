using Microsoft.AspNetCore.Mvc;

namespace WebEcommerceClothing.Controllers
{
    public class CrudController : Controller
    {
        public IActionResult CrudAdmin()
        {
            return View();
        }
        public IActionResult CrudShopEmployee()
        {
            return View();
        }
    }
}
