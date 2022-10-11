using Microsoft.AspNetCore.Mvc.Filters;

namespace BoostHolding.Web.Services
{
    public class UserSignedIn
    {
        private readonly ApplicationDbContext _db;
        

        public UserSignedIn(ApplicationDbContext db)
        {
            _db = db;
        }
       public string GetUserPhoto()
        {
        
          var user =  _db.Users.OrderByDescending(x=>x.SignedInTime).FirstOrDefault();
            var employee = _db.Employees.Where(x => x.Email == user.UserName).FirstOrDefault();
            return employee.ImageUrl;
        }
        public string GetUserName()
        {

            var user = _db.Users.OrderByDescending(x => x.SignedInTime).FirstOrDefault();
            return user.UserName;
        }
        public string GetUserTitle()
        {

            var user = _db.Users.OrderByDescending(x => x.SignedInTime).FirstOrDefault();
            var employee = _db.Employees.Include(x=>x.Title).Where(x => x.Email == user.UserName).FirstOrDefault();
            return employee.Title.Name;
        }
        public string GetFullName()
        {

            var user = _db.Users.OrderByDescending(x => x.SignedInTime).FirstOrDefault();
            var employee = _db.Employees.Where(x => x.Email == user.UserName).FirstOrDefault();
            return employee.Name+ " "+employee.LastName;
        }
        public bool PhotoCont()
        {
            var user = _db.Users.OrderByDescending(x => x.SignedInTime).FirstOrDefault();
            var employee = _db.Employees.Where(x => x.Email == user.UserName).FirstOrDefault();
            if (employee.ImageUrl=="" || employee.ImageUrl==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsUserMale()
        {
            var user = _db.Users.OrderByDescending(x => x.SignedInTime).FirstOrDefault();
            var employee = _db.Employees.Where(x => x.Email == user.UserName).FirstOrDefault();
            if (employee.Gender=="Male")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
