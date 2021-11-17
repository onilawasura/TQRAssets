using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TIQRI.ITS.Web.Providers;
using TIQRI.ITS.Web.Models;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Notifications;
using System.Threading.Tasks;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;

namespace TIQRI.ITS.Web
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        //public void ConfigureAuth(IAppBuilder app)
        //{
        //    // Configure the db context and user manager to use a single instance per request
        //    app.CreatePerOwinContext(ApplicationDbContext.Create);
        //    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

        //    // Enable the application to use a cookie to store information for the signed in user
        //    // and to use a cookie to temporarily store information about a user logging in with a third party login provider
        //    app.UseCookieAuthentication(new CookieAuthenticationOptions());
        //    app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

        //    // Configure the application for OAuth based flow
        //    PublicClientId = "self";
        //    OAuthOptions = new OAuthAuthorizationServerOptions
        //    {
        //        TokenEndpointPath = new PathString("/Token"),
        //        Provider = new ApplicationOAuthProvider(PublicClientId),
        //        AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
        //        AllowInsecureHttp = true
        //    };

        //    // Enable the application to use bearer tokens to authenticate users
        //    app.UseOAuthBearerTokens(OAuthOptions);

        //    // Uncomment the following lines to enable logging in with third party login providers
        //    //app.UseMicrosoftAccountAuthentication(
        //    //    clientId: "",
        //    //    clientSecret: "");

        //    //app.UseTwitterAuthentication(
        //    //    consumerKey: "",
        //    //    consumerSecret: "");

        //    //app.UseFacebookAuthentication(
        //    //    appId: "",
        //    //    appSecret: "");

        //    //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
        //    //{
        //    //    ClientId = "",
        //    //    ClientSecret = ""
        //    //});
        //}

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                CookieManager = new SystemWebCookieManager(),
                CookieHttpOnly = true,
                // We are expecting a different site to POST back to us,
                // so the ASP.Net Core default of Lax is not appropriate in this case
                CookieSameSite = SameSiteMode.None,
            });
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    // Sets the client ID, authority, and redirect URI as obtained from Web.config
                    ClientId = ConfigurationManager.AppSettings["ClientId"],
                    Authority = "https://login.microsoftonline.com/" + ConfigurationManager.AppSettings["Tenant"] + "/v2.0",
                    RedirectUri = ConfigurationManager.AppSettings["redirectUri"],
                    // PostLogoutRedirectUri is the page that users will be redirected to after sign-out. In this case, it's using the home page
                    PostLogoutRedirectUri = ConfigurationManager.AppSettings["postLogoutRedirectUri"],
                    Scope = OpenIdConnectScope.OpenIdProfile,
                    // ResponseType is set to request the code id_token, which contains basic information about the signed-in user
                    ResponseType = OpenIdConnectResponseType.CodeIdToken,
                    // ValidateIssuer set to false to allow personal and work accounts from any organization to sign in to your application
                    // To only allow users from a single organization, set ValidateIssuer to true and the 'tenant' setting in Web.config to the tenant name
                    // To allow users from only a list of specific organizations, set ValidateIssuer to true and use the ValidIssuers parameter

                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true, // Simplification (see note below)
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                        NameClaimType = "name"
                    },
                    // OpenIdConnectAuthenticationNotifications configures OWIN to send notification of failed authentications to the OnAuthenticationFailed method
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthenticationFailed = OnAuthenticationFailed
                    }
                }
            );
        }
        private Task OnAuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context)
        {
            context.HandleResponse();
            context.Response.Redirect("/Home/Index");
            return Task.FromResult(0);
        }
    }
}
