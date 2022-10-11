using System.ComponentModel.DataAnnotations;

namespace BoostHolding.Entities.Attributes
{
    public class MailCheckAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string data = value.ToString();

            var mailAddressParts = data.Split('@');

            if (mailAddressParts.Length != 2 || (mailAddressParts.Length == 2 && (String.IsNullOrEmpty(mailAddressParts[0]) || mailAddressParts[0].Contains(" ") || mailAddressParts[1].ToLower() != "bilgeadamboost.com")))
                return false;
            else
                return true;
        }
    }
}
