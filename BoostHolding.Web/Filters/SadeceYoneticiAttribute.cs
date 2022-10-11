using Microsoft.AspNetCore.Mvc.Filters;

namespace BoostHolding.Web.Filters
{
    public class SadeceYoneticiAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var roller = context.HttpContext.Session.GetString("roller");

            if (string.IsNullOrEmpty(roller) || !roller.Split(",").Contains("Manager"))
                context.Result = new RedirectToActionResult("Index", "Login", new { area = "Admin" });

        }
    }
}
