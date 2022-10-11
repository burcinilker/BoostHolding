using BoostHolding.Entities.Data;
using BoostHolding.Entities.Identity;
using BoostHolding.Web.Filters;
using BoostHolding.Web.Services;

namespace BoostHolding.Web.Areas.Admin.Controllers
{
    [Area("Yonetim")]
    [Login]
    [OnlyEmployee]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public UserController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            UserSignedIn user = new UserSignedIn(_db);
            var employee = _db.Employees.Include(x => x.Department).Include(x => x.Title).Where(x => x.Email == user.GetUserName()).FirstOrDefault();
            var totalAdvance = _db.AdvancePayments.Where(x => x.EmployeeId == employee.Id && x.Status == Status.Accepted).Sum(x => x.Amount);
            var totalPermission = _db.Permissions.Where(x => x.EmployeeId == employee.Id && x.ApprovalStatus == "Approved").Sum(x => x.TotalDaysOff);
            int newTotalPermission = 0;
            if ((DateTime.Now.Year - employee.DateOfStart.Year)>=5)
            {
                 newTotalPermission = 20 - totalPermission;
            }
            else if ((DateTime.Now.Year - employee.DateOfStart.Year) < 5)
            {
                newTotalPermission = 14 - totalPermission;
            }
            var totalExpenditure = _db.Expenditures.Where(x => x.EmployeeId == employee.Id && x.Status == Status.Accepted).Sum(x => x.Amount);
            var totalDays = (DateTime.Now - employee.DateOfStart).Days;
            ViewBag.TotalAdvance = 10000m-totalAdvance;
            ViewBag.TotalPermission = newTotalPermission;
            ViewBag.TotalExpenditure = totalExpenditure;
            ViewBag.TotalDays = totalDays;
            return View(employee);

        }

        public IActionResult Update()
        {
            UserSignedIn user = new UserSignedIn(_db);
            var employee = _db.Employees.Include(x => x.Department).Include(x => x.Title).Where(x => x.Email == user.GetUserName()).FirstOrDefault();
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
                return RedirectToAction("Index", new {userName= employee.Email});
            }
            return View(vm);
        }
        public IActionResult Details()
        {
            UserSignedIn user = new UserSignedIn(_db);
            var employee = _db.Employees.Include(x => x.Department).Include(x => x.Title).Where(x => x.Email == user.GetUserName()).FirstOrDefault();
            ViewBag.Employee = employee;
            return View();

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
