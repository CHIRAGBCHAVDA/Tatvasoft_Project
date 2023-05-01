using Microsoft.AspNetCore.Mvc.Filters;

namespace CIPlatform.Authorization
{
    public class AuthAttribute : ActionFilterAttribute
    {
        private readonly string _allowedRole;

        public AuthAttribute(string allowedRole)
        {
            _allowedRole = allowedRole;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            // Check if user is logged in
            if (httpContext.Session.GetString("role") == null)
            {
                httpContext.Response.Redirect("/Home/Index");
                return;
            }

            // Check if user has the required role
            if (httpContext.Session.GetString("role") != _allowedRole)
            {
                httpContext.Response.StatusCode = 403; // Forbidden
                httpContext.Response.Redirect("/Home/Index");
                return;
            }

        }
    }
}
