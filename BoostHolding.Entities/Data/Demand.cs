using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoostHolding.Entities.Data
{
    public class Demand
    {
        public int Id { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public DateTime DateOfDemand { get; set; }
        public Status Status { get; set; }
    }
}
