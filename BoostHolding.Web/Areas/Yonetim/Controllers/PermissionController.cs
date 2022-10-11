using BoostHolding.Entities.Data;
using BoostHolding.Web.Filters;
using BoostHolding.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoostHolding.Web.Areas.Yonetim.Controllers
{
    [Area("Yonetim")]
    [Login]
    
    public class PermissionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserSignedIn _user;

        public PermissionController(ApplicationDbContext db, UserSignedIn user)
        {
            _db = db;
            _user = user;
        }
        public IActionResult Index()
        {
            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            var permission = _db.Permissions.Where(x => x.EmployeeId == employee.Id).Include(x => x.TypesOfPermission).OrderByDescending(x=>x.DateOfRequest).ToList();
            ViewBag.Employee = employee;
            return View(permission);
        }

        public IActionResult Create()
        {
            var vm = new CreatePermissionViewModel();
            vm.TypeOfPermissions = _db.TypeOfPermissions.Select(x => new SelectListItem()
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
           //ViewBag.Employee = employee;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePermissionViewModel vm)
        {
            
            vm.TypeOfPermissions = _db.TypeOfPermissions.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            vm.EmployeeId = employee.Id;
            
            var totalPermission = _db.Permissions.Where(x => x.EmployeeId == employee.Id && x.ApprovalStatus == "Approved").Sum(x => x.TotalDaysOff);
            int newTotalPermission = 0;
            if ((DateTime.Now.Year - employee.DateOfStart.Year) >= 5)
            {
                newTotalPermission = 20 - totalPermission;
            }
            else if ((DateTime.Now.Year - employee.DateOfStart.Year) < 5)
            {
                newTotalPermission = 14 - totalPermission;
            }
            if (newTotalPermission<1)
            {
                ModelState.AddModelError("error", "You have reached your permission limit.Please try another amount.");
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                var permission = new Permission()
                {
                    EmployeeId = vm.EmployeeId,
                    DateOfStart = vm.DateOfStart,
                    DateOfRequest = vm.DateOfRequest,
                    DateOfEnd = vm.DateOfEnd,
                    TypesOfPermission = _db.TypeOfPermissions.Find(vm.TypesOfPermissionId),
                    TypesOfPermissionId =vm.TypesOfPermissionId,
                    ApprovalStatus = "Waiting For Approval"
                };
                var totalDay = (vm.DateOfEnd - vm.DateOfStart).Days;
                if (newTotalPermission < totalDay && !(permission.TypesOfPermission == _db.TypeOfPermissions.FirstOrDefault(x => x.Name == "Disease Permission")) && !(permission.TypesOfPermission == _db.TypeOfPermissions.FirstOrDefault(x => x.Name == "Maternity Permission")))
                {
                    ModelState.AddModelError("error", "You have reached your permission limit.Please try another amount.");
                    return View(vm);
                }
                permission.CalculateTotalDaysOff();
                int totalWorkingTime = employee.DateOfStart.Year - DateTime.UtcNow.Year;
                if (permission.TypesOfPermission==_db.TypeOfPermissions.FirstOrDefault(x=>x.Name == "Annual Permission") && permission.TotalDaysOff>14 && totalWorkingTime<=5)
                {
                    ModelState.AddModelError("error", "You have reached your permission limit.Please try another amount.");
                    return View(vm);
                }
                else if (permission.TypesOfPermission == _db.TypeOfPermissions.FirstOrDefault(x => x.Name == "Annual Permission") && permission.TotalDaysOff >= 20 && totalWorkingTime > 5)
                {
                    ModelState.AddModelError("error", "You have reached your permission limit.Please try another amount.");
                    return View(vm);
                }
                else if(permission.TypesOfPermission == _db.TypeOfPermissions.FirstOrDefault(x => x.Name == "Paternity Permission") && permission.TotalDaysOff>5 && !_user.IsUserMale())
                {
                    ModelState.AddModelError("error", "You have reached your permission limit.Please try another amount.");
                    return View(vm);
                }
                else if (permission.TypesOfPermission == _db.TypeOfPermissions.FirstOrDefault(x => x.Name == "Paternity Permission") && !_user.IsUserMale())
                {
                    ModelState.AddModelError("error", "You can not request permission for paternity! ");
                    return View(vm);
                }
                else if(permission.TypesOfPermission == _db.TypeOfPermissions.FirstOrDefault(x => x.Name == "Marriage Permission") && permission.TotalDaysOff>3)
                {
                    ModelState.AddModelError("error", "You have reached your permission limit.Please try another amount.");
                    return View(vm);
                } 
                else if(permission.TypesOfPermission == _db.TypeOfPermissions.FirstOrDefault(x => x.Name == "Maternity Permission") && permission.TotalDaysOff>180  )
                {
                    ModelState.AddModelError("error", "You have reached your permission limit.Please try another amount.");
                    return View(vm);
                }
                else if (permission.TypesOfPermission == _db.TypeOfPermissions.FirstOrDefault(x => x.Name == "Maternity Permission") && _user.IsUserMale())
                {
                    ModelState.AddModelError("error", "You can not request permission for maternity! ");
                    return View(vm);
                }
                           
                _db.Permissions.Add(permission);
                _db.SaveChanges();
                employee.PermissionId=permission.Id;
                return RedirectToAction("Index");
            }           
            return View(vm);
        }

        public IActionResult Delete(int? id)
        {
            Employee employee = _db.Employees.Where(x => x.Email == _user.GetUserName()).FirstOrDefault();
            var permission = _db.Permissions.Where(x=>x.Id ==id).FirstOrDefault();
            _db.Permissions.Remove(permission);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AcceptPermit(int? id)
        {
            var permission = _db.Permissions.Where(x => x.Id == id).FirstOrDefault();
            Employee employee = _db.Employees.Where(x => x.Id == permission.EmployeeId).FirstOrDefault();
            var list = _db.Permissions.Where(x => x.ApprovalStatus == "Waiting For Approval").ToList();


            var totalPermission = _db.Permissions.Where(x => x.EmployeeId == employee.Id && x.ApprovalStatus == "Approved").Sum(x => x.TotalDaysOff);
            int newTotalPermission = 0;
            if ((DateTime.Now.Year - employee.DateOfStart.Year) >= 5)
            {
                newTotalPermission = 20 - totalPermission;
            }
            else if ((DateTime.Now.Year - employee.DateOfStart.Year) < 5)
            {
                newTotalPermission = 14 - totalPermission;
            }
            if ((newTotalPermission-permission.TotalDaysOff)<1)
            {
                foreach (var item in list)
                {
                    if ((newTotalPermission - item.TotalDaysOff) < 1)
                    {
                        item.ApprovalStatus = "Rejected";
                        _db.Permissions.Update(item);
                        _db.SaveChanges();
                    }
                }
                        return RedirectToAction("PermissionList", "Manager", new { area = "Admin" });
            }


            permission.ApprovalStatus = "Approved";
            _db.Permissions.Update(permission);
            _db.SaveChanges();
            return RedirectToAction("PermissionList", "Manager", new { area = "Admin" });
        }
        public IActionResult RejectPermit(int? id)
        {
            var permission = _db.Permissions.Where(x => x.Id == id).FirstOrDefault();
            permission.ApprovalStatus = "Rejected";
            _db.Permissions.Update(permission);
            _db.SaveChanges();
            return RedirectToAction("PermissionList","Manager", new { area = "Admin" });
        }
    }
}
