using Microsoft.AspNetCore.Mvc;

namespace NetCoreA7.Areas.Admins.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
