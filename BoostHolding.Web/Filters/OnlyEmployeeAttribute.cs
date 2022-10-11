using Microsoft.AspNetCore.Mvc.Filters;

namespace BoostHolding.Web.Filters
{
    public class OnlyEmployeeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var roller = context.HttpContext.Session.GetString("employeerole");

            if (string.IsNullOrEmpty(roller) || !roller.Split(",").Contains("Employee"))
                context.Result = new RedirectToActionResult("Index", "Login", new { area = "Admin" });

        }
    }
}
