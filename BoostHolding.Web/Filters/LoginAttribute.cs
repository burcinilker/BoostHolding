using Microsoft.AspNetCore.Mvc.Filters;

namespace BoostHolding.Web.Filters
{
    public class LoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var roller = context.HttpContext.Session.GetString("loginrole");

            if (string.IsNullOrEmpty(roller) || !roller.Split(",").Contains("Login"))
                context.Result = new RedirectToActionResult("Index", "Login", new { area = "Admin" });

        }
    }
}
