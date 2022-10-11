using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoostHolding.Entities.Data
{
    public class Expenditure
    {
        public Expenditure()
        {
            RequestDate = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExpenditureDate { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public Unit Unit { get; set; }

        public string Description { get; set; }
        public string Document { get; set; }
        public Employee Employee { get; set; }
        public Status Status { get; set; }
        public ExpenditureType ExpenditureType { get; set; }

        public int EmployeeId { get; set; }

        //public List<Employee> Employees { get; set; }
    }
}
