using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VvtLession2.Models;

namespace VvtLesson2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Trek 820 - 2016", YearRelease = 2016, Price = 379.99 },
                new Product { ProductId = 2, ProductName = "Ritchay Tinkerwolf Framsett - 2016", YearRelease = 2016, Price = 709.99 },
                new Product { ProductId = 3, ProductName = "Surly Wednesday Framsett - 2016", YearRelease = 2016, Price = 999.99 }
            };

            return View(products);
        }
    }
}