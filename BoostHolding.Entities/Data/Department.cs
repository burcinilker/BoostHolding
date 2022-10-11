﻿using System.ComponentModel.DataAnnotations;

namespace BoostHolding.Entities.Data
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //public List<Employee> Employees { get; set; }

    }
}
