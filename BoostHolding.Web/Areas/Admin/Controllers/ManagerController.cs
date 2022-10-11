using BoostHolding.Entities.Data;
using BoostHolding.Web.Filters;
using BoostHolding.Entities.Identity;

namespace BoostHolding.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Login]
    [SadeceYonetici]
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ManagerController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            var manager = _db.Employees.Include(x => x.Department).Include(x => x.Title).Where(x => x.Role == "Manager").OrderByDescending(x => x.Id).ToList();
            return View(manager);

        }

        public IActionResult Update()
        {
            Employee employee = _db.Employees.Where(x => x.Role == "Manager").FirstOrDefault();
            if (employee == null)
                return NotFound();
            var vm = new UpdateEmployeeViewModel();
            vm.Id = employee.Id;
            vm.PhoneNumber = employee.PhoneNumber;
            vm.Address = employee.Address;
            return View(vm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, UpdateEmployeeViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }
            Employee employee = _db.Employees.Where(x => x.Id == vm.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                employee.PhoneNumber = vm.PhoneNumber;
                employee.Address = vm.Address;
                if (vm.File != null)
                    employee.ImageUrl = SaveImage(vm.File);
                _db.Employees.Update(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        public IActionResult Details()
        {
            Employee employee = _db.Employees.Include(x => x.Department).Include(x => x.Title).Where(x => x.Role == "Manager").FirstOrDefault();
            ViewBag.Employee = employee;
            return View();

        }

        public IActionResult DemandList()
        {
            var expenditure = _db.Expenditures.Include(x => x.Employee).OrderByDescending(x => x.RequestDate).ToList();
            return View(expenditure);
        }

        public IActionResult PermissionList()
        {           
            var permission = _db.Permissions.Include(x=>x.Employee).Include(x => x.TypesOfPermission).OrderByDescending(x => x.DateOfRequest).ToList();
            return View(permission);
        }

        public IActionResult AdvancePaymentList()
        { var advancePayment = _db.AdvancePayments.Include(x => x.Employee).OrderByDescending(x => x.RequestDate).ToList();    
            return View(advancePayment);
        }
        private string SaveImage(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            var newFileName = Guid.NewGuid() + ext;
            var path = Path.Combine(_env.WebRootPath, "img", newFileName);
            using (FileStream stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            return newFileName;
        }

    }
}
