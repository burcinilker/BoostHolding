using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BoostHolding.Entities.Attributes
{
  
    public class AgeLimitAttribute : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;

        
        public AgeLimitAttribute(int minAge,int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
            
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            DateTime date = (DateTime)value;
            int age = DateTime.Now.Year - date.Year;
            if (age<18 || age>65)
            return false;
            return true;          
        }

    }
}
