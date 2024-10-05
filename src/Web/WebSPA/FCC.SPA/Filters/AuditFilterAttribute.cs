using FCC.SPA.Abstractions;
using FCC.SPA.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FCC.SPA.Filters
{
    public class AuditFilterAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        private readonly ILogService _logService;
        private readonly string _action;
        public AuditFilterAttribute(IConfiguration configuration, ILogService logService, string action)
        {
            _configuration = configuration;
            _logService = logService;
            _action = action;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                bool SaveAllActions = bool.Parse(_configuration["IsAuditAllActions"]);
                if (_action == Constants.Login || _action == Constants.Logout || SaveAllActions == true)
                {
                    var objaudit = new AuditModel(); // Getting Action Name 

                    InputParameters inputParam = null;


                    if (SaveAllActions && _action == Constants.Others)
                    {
                        var requestParams = filterContext.ActionArguments["param"];

                        if (requestParams != null)
                        {
                            inputParam = (InputParameters)requestParams;

                            objaudit.Area = inputParam.apiType;

                            var endpointArr = inputParam.endpoint.Split("/");

                            objaudit.ControllerName = endpointArr[0] != null ? endpointArr[0] : string.Empty;
                            objaudit.Action = endpointArr[1] != null ? endpointArr[1] : string.Empty;


                        }
                    }
                    else
                    {
                        objaudit.Action = _action;
                    }

                    string username = filterContext.HttpContext.Session.GetString(Constants.ADUserName);
                    if (username == null)
                    {
                        var claims = ((ClaimsIdentity)filterContext.HttpContext.User.Identity).Claims.Select(c =>
                        new { type = c.Type, value = c.Value })
                        .ToArray();

                        var userNameClaim = claims.Where(x => x.type == Constants.claimName).FirstOrDefault();

                        if (userNameClaim != null)
                        {
                            filterContext.HttpContext.Session.SetString(Constants.ADUserName, userNameClaim.value);
                            username = userNameClaim.value;
                        }
                    }

                    objaudit.UserId = username;

                    if (filterContext.HttpContext != null)
                        objaudit.IPAddress = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();

                    objaudit.SessionId = filterContext.HttpContext.Session.Id;
                    objaudit.LoginStatus = "A";
                    objaudit.LoggedInAt = DateTime.UtcNow;

                    if (_action == Constants.Logout)
                    {
                        objaudit.LoggedOutAt = DateTime.UtcNow;
                        objaudit.LoginStatus = "I";
                    }
                    else if (_action == Constants.Login)
                    {
                        objaudit.IsFirstLogin = true;
                    }

                    _logService.SaveAuditLog(objaudit);

                }
            }

            catch (Exception ex)
            {
                // Facing Index was outside the bounds of the array Exception so added exception handle.
            }


        }

    }
}
