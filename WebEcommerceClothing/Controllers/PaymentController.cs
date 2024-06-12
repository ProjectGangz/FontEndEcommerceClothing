using Microsoft.AspNetCore.Mvc;

namespace WebEcommerceClothing.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
