﻿using Microsoft.AspNetCore.Mvc;

namespace Product.Controllers
{
    public class ProductController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()  
        {
            return View();
        }
    }
}
