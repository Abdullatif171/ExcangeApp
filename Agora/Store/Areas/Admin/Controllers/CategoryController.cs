using Microsoft.AspNetCore.Mvc;

namespace Store.Areas.Admin.controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}