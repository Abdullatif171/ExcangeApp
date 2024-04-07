using Microsoft.AspNetCore.Mvc;

namespace Store.Areas.Admin.controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}