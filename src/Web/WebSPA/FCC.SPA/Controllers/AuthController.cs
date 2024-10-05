using FCC.SPA.Extensions;
using FCC.SPA.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using FCC.SPA;
using FCC.SPA.Controllers;

namespace FCC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        [TypeFilter(typeof(AuditFilterAttribute), Arguments = new object[] { Constants.Logout })]
        public async Task Logout(bool signout = true)
        {
            if (signout)
            {
                await HttpContext.SignOutAsync("Cookies");
                await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

                var prop = new AuthenticationProperties
                {
                    RedirectUri = Url.Action("Index", "Home")
                };

                // after signout this will redirect to your provided target
                await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, prop);
            }

        }

        [CustomAuthorize]
        public JsonResult GetUser()
        {
            var currentUserId = User.GetUserId();
            var currentUserName = User.GetUserEmail();


            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var claims = ((ClaimsIdentity)this.User.Identity).Claims.Select(c =>
                    new { type = c.Type, value = c.Value })
                    .ToArray();

                var userToken = Softura.Web.Extensions.HttpContextExtensions.GetAADToken(HttpContext);

                return new JsonResult(new { isAuthenticated = true, currentUserId, claims = claims, idUserToken = userToken, currentUserName });
            }

            return new JsonResult(new { isAuthenticated = false });
        }

    }
}
