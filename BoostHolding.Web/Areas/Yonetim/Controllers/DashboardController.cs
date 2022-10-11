using BoostHolding.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BoostHolding.Web.Areas.Yonetim.Controllers
{
    [Area("Yonetim")]
    [Login]
    [OnlyEmployee]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return Content("selam");
        }
    }
}
