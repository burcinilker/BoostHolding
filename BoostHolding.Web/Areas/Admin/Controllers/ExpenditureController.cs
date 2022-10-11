using BoostHolding.Entities.Data;
using BoostHolding.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BoostHolding.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Login]
    [SadeceYonetici]
    public class ExpenditureController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenditureController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            Demand demand = _db.Demands.OrderByDescending(x => x.Status).FirstOrDefault();
            ViewBag.Demand = demand;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
