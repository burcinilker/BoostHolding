
using BoostHolding.Entities.Attributes;
using BoostHolding.Entities.Data;
using BoostHolding.Web.Attribute;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BoostHolding.Web.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? TitleId { get; set; }
        [Required, MaxLength(50), TextCheck(ErrorMessage = "Please do not enter numbers and special characters")]
        
        public string Name { get; set; }
        [TextCheck(ErrorMessage = "Please do not enter numbers and special characters")]
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [TcCheckValidation(ErrorMessage = "Please type a correct TC number")]
        public string TcNumber { get; set; }
        [ImageFile(AllowedExtensions = ".jpg,.png,.gif", MaxFileSizeMb = 1, ErrorMessage = "The file is invalid")]
        public IFormFile File { get; set; } = null!;
        [Required]
        [PhoneNumber(ErrorMessage = "Please type a correct PhoneNumber.Enter by adding 05 to your number(05*********)")]
   
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        
        [MaxLength(10)]
        public string Gender { get; set; }
        [Required,AgeLimit(18,65,ErrorMessage ="The age must be between 18-65")]
        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfStart { get; set; }
        public DateTime? DateOfLeave { get; set; } = null!;

        public bool IsActive { get; set; }
        //public Department Department { get; set; }
        //public Title Title { get; set; }

        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Titles { get; set; } = new();
        public List<EmployeeViewModel> Employees { get; set; } = new();
    }
}
