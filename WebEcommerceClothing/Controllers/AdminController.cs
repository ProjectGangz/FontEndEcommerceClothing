using Microsoft.AspNetCore.Mvc;

namespace WebEcommerceClothing.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult TemplateAdmin()
        {
            return View();
        }
        public IActionResult CrudShopEmployee()
        {
            return View();
        }
    }
}
