using BoostHolding.Entities.Attributes;
using BoostHolding.Web.Attribute;

namespace BoostHolding.Web.Models
{
    public class UpdateEmployeeViewModel
    {
        public int Id { get; set; }
        [PhoneNumber(ErrorMessage = "Enter your phone number as 11 digits with a leading 05 (05*********)")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [ImageFile(AllowedExtensions = ".jpg,.png,.gif", MaxFileSizeMb = 1, ErrorMessage = "The File is invalid")]
        public IFormFile File { get; set; }
        public string ImgUrl { get; set; }

    }
}
