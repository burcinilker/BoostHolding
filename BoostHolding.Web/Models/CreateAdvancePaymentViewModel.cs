using BoostHolding.Entities.Data;

namespace BoostHolding.Web.Models
{
    public class CreateAdvancePaymentViewModel
    {
       

        public int Id { get; set; }

        
        public decimal Amount { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public string Description { get; set; }

        public Status Status { get; set; }

        public Unit Unit { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public decimal Salary { get; set; } = 10000m;
    }
}
