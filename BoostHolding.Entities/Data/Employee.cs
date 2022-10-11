using BoostHolding.Entities.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace BoostHolding.Entities.Data
{
    public class Employee 
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
       
        public int? TitleId { get; set; }
        public string Role { get; set; }
        [Required, MaxLength(50)]
        [TextCheck(ErrorMessage = "Please do not enter numbers and special characters")]
        public string Name { get; set; }
        [TextCheck(ErrorMessage = "Please do not enter numbers and special characters")]
        public string LastName { get; set; }
        [Required]
        [TcCheckValidation(ErrorMessage = "Please type a correct TC number")]
        public string TcNumber { get; set; }
        public string ImageUrl { get; set; }
        [Required]
       
        [PhoneNumber(ErrorMessage = "Enter your phone number as 11 digits with a leading 05 (05*********)")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required]
        [MailCheck(ErrorMessage = "Please type a correct e-mail.The email you entered must end with 'bilgeadamboost.com'")]
        public string Email { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        [Required]
        [AgeLimit(18,65,ErrorMessage = "Age range must be between 18-65")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        
        public DateTime DateOfStart { get; set; } = DateTime.Now;
     
        public DateTime? DateOfLeave { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public Department Department { get; set; }
        public Title Title { get; set; }

        public int? AdvancePaymentId { get; set; }
        public int? ExpenditureId { get; set; }
        public List<AdvancePayment> AdvancePayments { get; set; }
        public List<Expenditure> Expenditures { get; set; }
        public int? PermissionId { get; set; }
        public List<Permission> Permission { get; set; }
    }
}
