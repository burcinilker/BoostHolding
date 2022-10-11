using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoostHolding.Entities.Data
{
    public class ExpenditureType
    {
        public int Id { get; set; }
       
        [Required]
        public string Name { get; set; }
        public List<Expenditure> Expenditures { get; set; }
    }
}
