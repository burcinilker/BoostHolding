using BoostHolding.DataAccessLayer.Migrations;
using BoostHolding.Entities.Data;
using BoostHolding.Entities.Identity;
using BoostHolding.Web.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Mail;

namespace BoostHolding.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SadeceYonetici]
    [Login]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index(string find)
        {
            if (find == "" || find == null)
            {
                var employee = _db.Employees.Include(x => x.Department).Include(x => x.Title).OrderByDescending(x => x.Id).ToList();
                return View(employee);
            }
            var findEmployee = _db.Employees.Include(x => x.Department).Include(x => x.Title).Where(x => x.Name.Contains(find) || x.LastName.Contains(find)).ToList();
            return View(findEmployee);

        }

       
        public IActionResult Create()
        {
            var vm = new EmployeeViewModel();
            vm.Departments = _db.Departments.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            vm.Titles = _db.Titles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            return View(vm);
        }
        public IActionResult Update(int? id)
        {
            if (id == null || _db.Employees == null)
                return NotFound();
            Employee employee = _db.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee == null)
                return NotFound();
            var vm = new UpdateEmployeeViewModel();
            vm.Id = employee.Id;
            vm.PhoneNumber = employee.PhoneNumber;
            vm.Address = employee.Address;
            vm.ImgUrl = employee.ImageUrl;
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
            Employee employee = _db.Employees.Find(vm.Id);
            if (ModelState.IsValid)
            {
                try
                {
                    employee.PhoneNumber = vm.PhoneNumber;
                    employee.Address = vm.Address;
                    if (vm.File != null)
                        employee.ImageUrl = SaveImage(vm.File);
                    _db.Update(employee);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_db.Employees.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        public IActionResult Details(int id)
        {
            Employee employee = _db.Employees.Include(x => x.Department).Include(x => x.Title).Where(x => x.Id == id).FirstOrDefault();
            ViewBag.Employee = employee;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel vm)
        {
            vm.Departments = _db.Departments.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            vm.Titles = _db.Titles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();


            if (ModelState.IsValid)
            {
                Employee employee;
                if (vm.File==null)
                {
                    if (vm.Gender=="Male")
                    {
                        employee = new Employee()
                        {
                            Name = NameFirtLetterUpper(vm.Name),
                            LastName = vm.LastName.ToUpper(),
                            TcNumber = vm.TcNumber,
                            PhoneNumber = vm.PhoneNumber,
                            Address = vm.Address,
                            Gender = vm.Gender,
                            Email = CreateEmail(vm.Name, vm.LastName, _db),
                            DateOfBirth = vm.DateOfBirth,
                            DateOfStart = vm.DateOfStart,
                            DepartmentId = vm.DepartmentId,
                            TitleId = vm.TitleId,
                            Department = _db.Departments.Find(vm.DepartmentId),
                            Title = _db.Titles.Find(vm.TitleId),
                            ImageUrl = "male.jpg"
                        };
                    }
                    else
                    {
                        employee = new Employee()
                        {
                            Name = NameFirtLetterUpper(vm.Name),
                            LastName = vm.LastName.ToUpper(),
                            TcNumber = vm.TcNumber,
                            PhoneNumber = vm.PhoneNumber,
                            Address = vm.Address,
                            Gender = vm.Gender,
                            Email = CreateEmail(vm.Name, vm.LastName, _db),
                            DateOfBirth = vm.DateOfBirth,
                            DateOfStart = vm.DateOfStart,
                            DepartmentId = vm.DepartmentId,
                            TitleId = vm.TitleId,
                            Department = _db.Departments.Find(vm.DepartmentId),
                            Title = _db.Titles.Find(vm.TitleId),
                            ImageUrl = "female.jpg"
                        };
                    }
                     
                }
                else
                {
                    employee = new Employee()
                    {
                        Name = NameFirtLetterUpper(vm.Name),
                        LastName = vm.LastName.ToUpper(),
                        TcNumber = vm.TcNumber,
                        PhoneNumber = vm.PhoneNumber,
                        Address = vm.Address,
                        Gender = vm.Gender,
                        Email = CreateEmail(vm.Name, vm.LastName, _db),
                        DateOfBirth = vm.DateOfBirth,
                        DateOfStart = vm.DateOfStart,
                        DepartmentId = vm.DepartmentId,
                        TitleId = vm.TitleId,
                        Department = _db.Departments.Find(vm.DepartmentId),
                        Title = _db.Titles.Find(vm.TitleId),
                        ImageUrl = SaveImage(vm.File)
                    };
                }

                if (vm.DateOfLeave != null)
                {
                    employee.DateOfLeave = vm.DateOfLeave;
                    employee.IsActive = false;
                }
                else
                {
                    employee.DateOfLeave = null;
                    employee.IsActive = true;
                }

                _db.Employees.Add(employee);
                _db.SaveChanges();
                var employeerole = _db.Roles.Where(x => x.RoleName == "Employee").FirstOrDefault();
                User user = new User();
                user.UserName = employee.Email;
                user.IsActive = false;
                user.Password = CreatePassword(employee.Name, employee.LastName);
                _db.Users.Add(user);
                _db.SaveChanges();
                var userr = _db.Users.Include(x => x.Roles).Where(x => x.UserName == employee.Email).FirstOrDefault();
                userr.Roles.Add(employeerole);
                _db.SaveChanges();
                string content = "Email : " + employee.Email + "\n" + "Password : " + user.Password;
                SendEmail("welel123321@gmail.com", content);

                return RedirectToAction("Index");
            }

            return View(vm);
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

        private string CreateEmail(string name,string lastname, ApplicationDbContext db)
        {
            name = name.Trim().ToLower();
            lastname = lastname.Trim().ToLower();
            string fullname = name + lastname;
            Random rnd = new Random();
            do
            {
                fullname = fullname.Replace(" ", "");
            } while (fullname.Contains(" "));
            string email = fullname + "@bilgeadamboost.com";
            if (db.Employees.Count(x => x.Email == email) > 0)
            {
                do
                {
                    email = fullname + (rnd.Next(1, 99)) + "@bilgeadamboost.com";
                } while (db.Employees.Count(x => x.Email == email) != 0);
            }
            return email;
        }
        private void SendEmail(string mail, string content)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                string mailReciever = mail;

                message.From = new MailAddress("boostholding06@gmail.com");
                message.To.Add(new MailAddress(mailReciever));
                message.Subject = "Boost Holding";
                message.IsBodyHtml = true;
                message.Body = content;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("boostholding06@gmail.com", "lquyroynkssldpzy");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private string CreatePassword(string name, string lastname)
        {
            Random rnd = new Random();
            string pass = name[0].ToString().ToUpper() + lastname[0].ToString().ToLower()+"."+rnd.Next(10000,15000).ToString();
            return pass;
        }
        private string NameFirtLetterUpper(string name)
        {
            name = name.ToLower();
            string newName = "";
            for (int i = 0; i < name.Length; i++)
            {
                if (i==0)
                {
                newName += name[i].ToString().ToUpper();
                }
                else
                {
                    newName+= name[i];
                }
            }
            return newName;
        }
    }
}
