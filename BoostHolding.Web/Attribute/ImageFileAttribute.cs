using System.ComponentModel.DataAnnotations;

namespace BoostHolding.Web.Attribute
{
    public class ImageFileAttribute : ValidationAttribute
    {
        public int MaxFileSizeMb { get; set; } = 1;
        public string AllowedExtensions { get; set; } = ".jpg,.png";
        public override bool IsValid(object value)
        {

            IFormFile formFile = value as IFormFile;
            if (formFile == null) return true;
            string[] exts = AllowedExtensions.Split(',', StringSplitOptions.TrimEntries);
            string ext = Path.GetExtension(formFile.FileName);
            if (!exts.Contains(ext))
            {
                ErrorMessage = $"Allowed extentions: {string.Join(",", exts)}";
                return false;
            }
            else if (formFile.Length > MaxFileSizeMb * 1024 * 1024)
            {
                ErrorMessage = $"Maximum file size: {MaxFileSizeMb}MB";
                return false;
            }
            return true;
        }
    }
}
