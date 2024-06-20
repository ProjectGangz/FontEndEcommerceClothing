using Microsoft.AspNetCore.Mvc;

namespace WebEcommerceClothing.Controllers
{
    public class CrudController : Controller
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
