using System;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace TIQRI.ITS.Web.Helpers
{
	public class CookieHelper
	{
        public static string CookieName
        {
            get { return ConfigurationManager.AppSettings["LoginCoockieName"]; }
        }

		public static void SetCookie(string cookieName, string userName, int? employeeId)
		{
			string rolesForUser = string.Join("|", Roles.GetRolesForUser(userName));
			string userData = string.Format("{0},{1}", employeeId, rolesForUser);
            SetCookie(cookieName, userName, userData);
		}

        public static void SetCookie(string cookieName, string userName, string userData)
		{
			var authentiacationTicket = new FormsAuthenticationTicket (1, userName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);
			string encriptedTicket = FormsAuthentication.Encrypt(authentiacationTicket);
            var loginCookie = new HttpCookie(cookieName, encriptedTicket);
            
            DeleteCookie(cookieName);

			HttpContext.Current.Response.SetCookie(loginCookie);
			HttpContext.Current.Response.Cookies.Add(loginCookie);
		}

        public static void SetCookie(string userName, int? employeeId, object[] roles)
        {
            string rolesForUser = string.Join("|", roles);
            string userData = string.Format("{0},{1}", employeeId, rolesForUser);
            SetCookie(userName, userData);
        }

        public static void SetCookie(string userName, string userData)
        {
            var authentiacationTicket = new FormsAuthenticationTicket(1, userName, Utility.GetDateTimeNow(), Utility.GetDateTimeNow().AddMinutes(60), false, userData);
            string encriptedTicket = FormsAuthentication.Encrypt(authentiacationTicket);
            var loginCookie = new HttpCookie(CookieName, encriptedTicket);
            HttpContext.Current.Response.SetCookie(loginCookie);
            HttpContext.Current.Response.Cookies.Add(loginCookie);
        }

        public static void DeleteCookie(string cookieName)
		{
            HttpContext.Current.Response.Cookies.Remove(cookieName);
		}
	}
}
