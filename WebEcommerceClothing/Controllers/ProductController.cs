﻿using Microsoft.AspNetCore.Mvc;

namespace WebEcommerceClothing.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductDetails() 
        { 
            return View();
        }
    }
}
