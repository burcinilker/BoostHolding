using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoostHolding.Entities.Data
{
    public class AdvancePayment
    {
        public AdvancePayment()
        {
            RequestDate = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime RequestDate { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public Unit Unit { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public decimal Salary { get; set; } = 10000m;
    }
}
