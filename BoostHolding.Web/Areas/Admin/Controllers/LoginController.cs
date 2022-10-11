using BoostHolding.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using BoostHolding.Entities.Data;
using BoostHolding.Web.Filters;

namespace BoostHolding.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var users = _db.Users.Include(x=>x.Roles).ToList();
            var loginrole = _db.Roles.Where(x => x.RoleName == "Login").FirstOrDefault();
            foreach (var user in users)
            {
                user.Roles.Remove(loginrole);
                _db.SaveChanges();
            }
            var currentuser = _db.Users.OrderByDescending(x => x.SignedInTime).FirstOrDefault();
            HttpContext.Session.SetString("loginrole", string.Join(",", currentuser.Roles.Select(x => x.RoleName)));
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(User userr)
        {
            var user = _db.Users.Include(x => x.Roles).Where(x => x.UserName == userr.UserName).FirstOrDefault();
            if (user==null)
            {
                ViewBag.FalsePassword = "Please check the e-mail!";
                return View();
            }
            if (!user.IsActive)
            {
                if (user.Password==userr.Password)
                {
                    return RedirectToAction("CreatePassword", new {id=user.Id});
                }
            }
            else
            {
                string hash = sha256_hash(userr.Password);
                
                if (user == null)
                {
                    ViewBag.FalsePassword = "Please check the e-mail!";
                    return View();
                }
                if (user.Password==hash)
                {
                    // burada sessiona kullacının adını kaydet ki giriş yapmış sayılsın
                    // burada sessiona rolünü de kaydet
                    var employee = _db.Employees.Where(x => x.Email == user.UserName).FirstOrDefault();
                    var loginrole = _db.Roles.Where(x => x.RoleName == "Login").FirstOrDefault();
                    user.Roles.Add(loginrole);
                    _db.SaveChanges();
                    HttpContext.Session.SetString("roller", string.Join(",", user.Roles.Select(x => x.RoleName)));
                    HttpContext.Session.SetString("loginrole", string.Join(",", user.Roles.Select(x => x.RoleName)));
                    HttpContext.Session.SetString("employeerole", string.Join(",", user.Roles.Select(x => x.RoleName)));
                    if (!employee.IsActive)
                    {
                        ViewBag.FalsePassword = "Employee is not active anymore!";
                        return View();

                    }
                    if (user.UserName== "gamze.altınelli@boostholding.com")
                    {
                        user.SignedInTime = DateTime.Now;
                        _db.Users.Update(user);
                        _db.SaveChanges();
                        return RedirectToAction("Index","Manager");
                    }
                    else
                    {
                        user.SignedInTime = DateTime.Now;
                        _db.Users.Update(user);
                        _db.SaveChanges();
                        return RedirectToAction("Index", "User", new { area = "Yonetim" } );
                    }
                }
                else
                {
                    ViewBag.FalsePassword = "Please check the password!";
                    return View();
                }
            }
            return View();
        }

        public IActionResult CreatePassword(int id)
        {
            CreatePasswordViewModel vm = new CreatePasswordViewModel();
            vm.Id = id;
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreatePassword(CreatePasswordViewModel vm)
        {
            if (vm.Password==vm.PasswordAgain)
            {
                var user = _db.Users.Where(x => x.Id == vm.Id).FirstOrDefault();
                user.Password = sha256_hash(vm.Password);
                user.IsActive = true;
                _db.Users.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Index", "Manager");
            }
            else
            {
                ViewBag.AgainPassword = "Password is not same!";
                return RedirectToAction("CreatePassword", new { id = vm.Id });
            }
            return View(vm);
        }

        
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(User user)
        {
            var userr = _db.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
            Employee employee = _db.Employees.Where(x=>x.Email==userr.UserName).FirstOrDefault();
            if (_db.Users.Where(x=> x.UserName==user.UserName).Count()>0)
            {
                userr.Password = CreatePassword(employee.Name,employee.LastName);
                user.IsActive = false;
                _db.Update(userr);
                _db.SaveChanges();
                string content = "Email : " + employee.Email + "\n" + "Password : " + userr.Password;
                SendEmail("welel123321@gmail.com", content);
                ViewBag.ForgetPassword = "The Email has sent!";
                return RedirectToAction("CreatePassword", new { id = userr.Id });
            }
            else if (_db.Users.Where(x => x.UserName == user.UserName).Count() == 0)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
            
        }

        private string sha256_hash(string sifre)
        {
            using (SHA256 hash = SHA256Managed.Create())
            { return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(sifre)).Select(l => l.ToString("X2"))); }
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
            string pass = name[0].ToString().ToUpper() + lastname[0].ToString().ToLower() + "." + rnd.Next(10000, 15000).ToString();
            return pass;
        }
    }
}
