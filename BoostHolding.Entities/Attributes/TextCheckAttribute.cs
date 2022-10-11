using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoostHolding.Entities.Attributes
{
    public class TextCheckAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string text = value.ToString().ToLower().Trim();
            char[] chars = text.ToCharArray();
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsControl(chars[i]) && !char.IsLetter(chars[i]) && (chars[i] != ' '))
                    return false;
            }
            return true;
        }

    }
}
