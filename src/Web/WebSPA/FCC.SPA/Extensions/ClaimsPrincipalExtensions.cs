using System.Security.Claims;

namespace FCC.SPA.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get current user account oid from azure AD token
        /// </summary>
        public static string GetUserId(this ClaimsPrincipal This)
        {
            return This.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier");
        }

        /// <summary>
        /// Get current user account email id from azure AD token
        /// </summary>
        public static string GetUserEmail(this ClaimsPrincipal This)
        {
            return This.FindFirstValue("preferred_username");
        }
    }
}
