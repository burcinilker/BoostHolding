using BoostHolding.DataAccessLayer.Migrations;
using BoostHolding.Entities.Data;
using BoostHolding.Web.Filters;
using BoostHolding.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoostHolding.Web.Areas.Yonetim.Controllers
{
    [Area("Yonetim")]
    [Login]
    
    public class ExpenditureController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserSignedIn _user;

        public ExpenditureController(ApplicationDbContext db, UserSignedIn user)
        {
            _db = db;
            _user = user;
        }
        public IActionResult Index()
        {
            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            var expenditure = _db.Expenditures.Where(x => x.EmployeeId == employee.Id).Include(x => x.ExpenditureType).OrderByDescending(x => x.RequestDate).ToList();
            ViewBag.Employee = employee;
            return View(expenditure);
        }
        public IActionResult Create()
        {
            var vm = new CreateExpenditureViewModel();
            vm.ExpenditureTypes = _db.ExpenditureTypes.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            if (_user.GetUserName() == null || _db.Employees == null)
                return NotFound();
            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            if (employee == null)
                return NotFound();

            vm.EmployeeId = employee.Id;
            ViewBag.Employee = employee;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateExpenditureViewModel vm)
        {
            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            vm.EmployeeId = employee.Id;
            if (ModelState.IsValid)
            {
                var expenditure = new Expenditure()
                {
                    EmployeeId = vm.EmployeeId,
                    ExpenditureDate = vm.ExpenditureDate,
                    RequestDate = vm.RequestDate,
                    ExpenditureType = _db.ExpenditureTypes.Find(vm.ExpenditureType),
                    Amount = vm.Amount,
                    Description = vm.Description,
                    Document = vm.Document,
                    //TypesOfPermissionId = vm.TypesOfPermissionId,
                    Status = vm.Status,
                    Unit = vm.Unit,
                };

                _db.Expenditures.Add(expenditure);
                _db.SaveChanges();
                employee.ExpenditureId = expenditure.Id;
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            var expenditure = _db.Expenditures.Where(x => x.Id == id).FirstOrDefault();
            _db.Expenditures.Remove(expenditure);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AcceptPermit(int? id)
        {
            var expenditure = _db.Expenditures.Where(x => x.Id == id).FirstOrDefault();
            expenditure.Status = Status.Accepted;
            _db.Expenditures.Update(expenditure);
            _db.SaveChanges();
            return RedirectToAction("DemandList", "Manager", new { area = "Admin" });
        }
        public IActionResult RejectPermit(int? id)
        {
            var expenditure = _db.Expenditures.Where(x => x.Id == id).FirstOrDefault();
            expenditure.Status = Status.Rejected;
            _db.Expenditures.Update(expenditure);
            _db.SaveChanges();
            return RedirectToAction("DemandList", "Manager", new { area = "Admin" });
        }
    }
}
