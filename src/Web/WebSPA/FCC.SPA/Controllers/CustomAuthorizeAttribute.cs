using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FCC.SPA.Controllers
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (context != null)
            {
                // Auth logic
                var isAuth = context.HttpContext.User.Identity.IsAuthenticated;
                if (!isAuth)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

            }
        }
    }
}
