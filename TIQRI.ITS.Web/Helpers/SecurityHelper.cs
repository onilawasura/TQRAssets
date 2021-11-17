using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Web.Helpers
{
    public static class SecurityHelper
    {
        public static bool IsAdminAccess(string userName)
        {
            var context = new Context();
            //var isAdmin = ConfigurationManager.AppSettings["SystemAdmin"].Equals(userName)
            //    || context.Administrators.Any(a => a.UserId == userName);
            var isAdmin = context.Administrators.Any(a => a.UserId == userName && a.Archived == false);
            return isAdmin;

            //return false;
        }

        public static void DeleteCookie(string cookieName)
        {
            var httpCookie = HttpContext.Current.Request.Cookies[cookieName];

            if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value))
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(httpCookie.Value);

                if (ticket != null)
                {
                    var newAuthentiacationTicket = new FormsAuthenticationTicket(1, ticket.Name,
                     Utility.GetDateTimeNow().AddDays(-1), Utility.GetDateTimeNow().AddDays(-1).AddMinutes(60), false, ticket.UserData);
                    string encriptedTicket = FormsAuthentication.Encrypt(newAuthentiacationTicket);
                    var loginCookie = new HttpCookie(cookieName, encriptedTicket);
                    HttpContext.Current.Response.SetCookie(loginCookie);
                    HttpContext.Current.Response.Cookies.Add(loginCookie);
                }
            }
        }
    }
}