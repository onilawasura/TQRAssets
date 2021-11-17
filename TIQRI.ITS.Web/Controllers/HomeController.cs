using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TIQRI.ITS.Domain.Enums;
using Context = TIQRI.ITS.Domain.Models.Context;
using System.Configuration;
using System.Web.Security;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Web.Script.Serialization;
using TIQRI.ITS.Web.Helpers;
using TIQRI.ITS.Web.AuthenticationAttribute;
using TIQRI.ITS.Web.ViewModels;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Cookies;

namespace TIQRI.ITS.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public static string CookieName
        {
            get { return ConfigurationManager.AppSettings["LoginCoockieName"]; }
        }
        public void SignIn()
        {

            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public void EndSession()
        {
            // If AAD sends a single sign-out message to the app, end the user's session, but don't redirect to AAD for sign out.
            Request.GetOwinContext().Authentication.SignOut();
            Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            this.HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            Session.RemoveAll();
            Response.Redirect("/");
        }
        public ActionResult Index()
        {

            SignIn();

            ClaimsIdentity userClaims;
            userClaims = User.Identity as ClaimsIdentity;
            var userName = userClaims?.FindFirst("preferred_username")?.Value;
            Session["UserName"] = userName;
            ViewBag.roles = userClaims?.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            if (!SecurityHelper.IsAdminAccess(userName))
            {
                return RedirectToAction("NotAuthorized");
            }
            else
            {
                return RedirectToAction("AdminHome");
            }

        }

        public void Logout()
        {
            //SecurityHelper.DeleteCookie(CookieName);
            if (Request.Cookies["username"] != null)
            {
                Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Clear();
            }

            Session.Abandon();

            HttpContext.GetOwinContext().Authentication.SignOut(
            new AuthenticationProperties { RedirectUri = ConfigurationManager.AppSettings["postLogoutRedirectUri"] },
            OpenIdConnectAuthenticationDefaults.AuthenticationType,
            CookieAuthenticationDefaults.AuthenticationType);
        }

        public ActionResult AdminHome()
        {
            if ((Session["UserName"]) != null)
            {
                var userName = Session["UserName"].ToString();
                if (!SecurityHelper.IsAdminAccess(userName))
                {
                    return RedirectToAction("NotAuthorized");
                }

                ViewBag.UserName = userName;
                var context = new Context();
                var model = new ViewModels.HomeViewModel() { };
                model.LoggedUserName = userName;
                model.IsAdmin = true;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult NotAuthorized()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> AdminDashboard()
        {
            //return RedirectToAction("Index", "AssetReport");
            var viewModel = await new PbiEmbeddedManager(ConfigurationManager.AppSettings["PBIClientID"], ConfigurationManager.AppSettings["PBIWorkspaceID"],
                ConfigurationManager.AppSettings["PBIUsername"], ConfigurationManager.AppSettings["PBIPassword"]).GetReports(ConfigurationManager.AppSettings["PBIReportID1"], string.Empty, false, string.Empty, string.Empty);
            viewModel.ReportName = "Property sales variation dashboard";
            viewModel.ShowFilterPane = false;
            viewModel.ShowReportTabs = false;

            return View(viewModel);
        }

        public ActionResult EmployeeHome()
        {
            return View();
        }

        public ActionResult EmployeeDashboard()
        {
            var userName = Session["UserName"];
            var userProfile = Helpers.HRDataHelper.GetEmployee(userName.ToString());

            var context = new Context();


            return View();
        }


        private static string Authenticate()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["ClientId"]);
            var clientId = Convert.ToBase64String(plainTextBytes);

            var authUrl = ConfigurationManager.AppSettings["AuthUrl"];
            var callbackUrl = ConfigurationManager.AppSettings["CallBackUrl"];

            var url = string.Format("{0}clientId={1}&responseType=code&redirectUri={2}", authUrl,
                Uri.EscapeDataString(clientId), callbackUrl);

            return url;
        }

        public ActionResult CallBack(string code)
        {
            var data = new StringBuilder();

            data.Append("code=" + code);
            data.Append("&clientId=" + Uri.EscapeDataString(Base64Encode(ConfigurationManager.AppSettings["ClientId"])));
            data.Append("&clientSecret=" + Uri.EscapeDataString(Base64Encode(ConfigurationManager.AppSettings["ClientSecret"])));
            data.Append("&redirectUri=" + "");

            HttpPost(data, ConfigurationManager.AppSettings["TokenkUrl"]);

            return RedirectToAction("Index", "Home");
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private static void HttpPost(StringBuilder data, string url)
        {
            HttpWebResponse response = null;

            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(data.ToString());

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                request.ClientCertificates.Add(new X509Certificate());

                Stream postStream = request.GetRequestStream();
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();

                response = (HttpWebResponse)request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine(json);

                    var ser = new JavaScriptSerializer();
                    var x = (Dictionary<string, object>)ser.DeserializeObject(json);

                    if (x != null)
                    {
                        string issuedOn = Base64Decode(x["IssuedOn"].ToString());
                        string expiresIn = Base64Decode(x["ExpiresIn"].ToString());
                        var idToken = x["IdToken"] as string;
                        string token = Base64Decode(x["AuthorizationToken"].ToString());

                        string[] id_token_payload = idToken.Split('.');

                        var payload = System.Web.Helpers.Json.Decode(Base64Decode(id_token_payload[1]));
                        var xx = (Dictionary<string, object>)ser.DeserializeObject(Base64Decode(id_token_payload[1]));
                        CookieHelper.SetCookie(xx["email"].ToString(), Convert.ToInt32(xx["employeeId"]), xx["roles"] as object[]);
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var err = (HttpWebResponse)e.Response)
                    {
                        Console.WriteLine("The server returned '{0}' with the status code '{1} ({2:d})'.",
                          err.StatusDescription, err.StatusCode, err.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }
        }

       
    }
}
