using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoostHolding.Entities.Attributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string phoneNumber = value.ToString();
            if (PhoneNumberControl(phoneNumber))
                return true;
            return false;
        }
        public static bool PhoneNumberControl(string number)
        {
            string format = @"^(05(\d{9}))$";
            Match match = Regex.Match(number, format, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
