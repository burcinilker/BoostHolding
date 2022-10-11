using BoostHolding.Entities.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoostHolding.Web.Models
{
    public class CreateExpenditureViewModel
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public decimal Amount { get; set; }
        public Unit Unit { get; set; }
        public string Description { get; set; }
        public string Document { get; set; }
        public Status Status { get; set; }
        public ExpenditureType ExpenditureType { get; set; }
        public int EmployeeId { get; set; }
        public List<Employee> Employees { get; set; }
        public List<SelectListItem> ExpenditureTypes { get; set; } = new();
    }
}
