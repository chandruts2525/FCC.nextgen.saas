using FCC.SPA.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCC.SPA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [TypeFilter(typeof(AuditFilterAttribute), Arguments = new object[] { Constants.Login })]
        public IActionResult Index()
        {
            return Redirect("/RoleListing");
        }
    }
}
