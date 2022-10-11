using BoostHolding.Entities.Data;
using BoostHolding.Web.Filters;
using BoostHolding.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoostHolding.Web.Areas.Yonetim.Controllers
{
    [Area("Yonetim")]
    [Login]
    
    public class AdvancePaymentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserSignedIn _user;

        public AdvancePaymentController(ApplicationDbContext db, UserSignedIn user)
        {
            _db = db;
            _user = user;
        }
        public IActionResult Index()
        {
            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            var advancePayment = _db.AdvancePayments.Where(x => x.EmployeeId == employee.Id).Include(x => x.Employee).OrderByDescending(x => x.RequestDate).ToList();
            ViewBag.Employee = employee;
            return View(advancePayment);
           
        }

        public IActionResult Create()
        {
            var vm = new CreateAdvancePaymentViewModel();


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
        public IActionResult Create(CreateAdvancePaymentViewModel vm)
        {
            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            vm.EmployeeId = employee.Id;
            var totalAdvance = _db.AdvancePayments.Where(x => x.EmployeeId == employee.Id && x.Status == Status.Accepted).Sum(x => x.Amount);
            if (totalAdvance>=10000m)
            {
                ModelState.AddModelError("error", "You have reached your advance limit.Please try another amount.");
                return View(vm);
            }
            if (vm.Amount<150)
            {
                ModelState.AddModelError("error", "The amount of advance can not be lower than 150!");
                return View(vm);
            }
            if ((10000m - totalAdvance) < vm.Amount)
            {
                ModelState.AddModelError("error", "You have reached your permission limit.Please try another amount.");
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                var advancePayment = new AdvancePayment()
                {
                    EmployeeId = vm.EmployeeId,
                    RequestDate = vm.RequestDate,
                    Description = vm.Description,
                    Unit = vm.Unit,
                    Amount = vm.Amount,
                    Status = vm.Status,


                };

                _db.AdvancePayments.Add(advancePayment);
                _db.SaveChanges();
                employee.AdvancePaymentId = advancePayment.Id;

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            var advancePayment = _db.AdvancePayments.Where(x => x.Id == id).FirstOrDefault();
            _db.AdvancePayments.Remove(advancePayment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AcceptAdvance(int? id)
        {

            var advancePayment = _db.AdvancePayments.Where(x => x.Id == id).FirstOrDefault();
            Employee employee = _db.Employees.Where(x => x.Id == advancePayment.EmployeeId).FirstOrDefault();
            var list =_db.AdvancePayments.Where(x => x.Status==Status.Pending).ToList();
            var totalAdvance = _db.AdvancePayments.Where(x => x.EmployeeId == employee.Id && x.Status == Status.Accepted).Sum(x => x.Amount);
            if ((advancePayment.Amount + totalAdvance)>10000m)
            {
                foreach (var item in list)
                {
                    if ((10000m-totalAdvance)<item.Amount)
                    {
                        item.Status = Status.Rejected;
                        _db.AdvancePayments.Update(item);
                        _db.SaveChanges();
                    }
                    
                }
                return RedirectToAction("AdvancePaymentList", "Manager", new { area = "Admin" });
            }
            advancePayment.Status = Status.Accepted;
            _db.AdvancePayments.Update(advancePayment);
            _db.SaveChanges();
            return RedirectToAction("AdvancePaymentList", "Manager", new { area = "Admin" });
        }
        public IActionResult RejectAdvance(int? id)
        {
            var advancePayment = _db.AdvancePayments.Where(x => x.Id == id).FirstOrDefault();
            advancePayment.Status = Status.Rejected;
            _db.AdvancePayments.Update(advancePayment);
            _db.SaveChanges();
            return RedirectToAction("AdvancePaymentList", "Manager", new { area = "Admin" });
        }

    }

}