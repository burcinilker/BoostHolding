using BoostHolding.Entities.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoostHolding.Web.Models
{
    public class CreatePermissionViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TypesOfPermissionId { get; set; }
        //public int TotalDaysOff { get; set; }
        public DateTime DateOfRequest { get; set; } = DateTime.UtcNow;
        public DateTime DateOfStart { get; set; } = DateTime.UtcNow.AddDays(1);
        public DateTime DateOfEnd { get; set; }
        public string ApprovalStatus { get; set; }
        public List<Employee> Employees { get; set; }
        public TypesOfPermission TypesOfPermission { get; set; }
        public List<SelectListItem> TypeOfPermissions { get; set; } = new();
    }
}
