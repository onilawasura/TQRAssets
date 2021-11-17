using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace TIQRI.ITS.Web.Controllers
{
    public class BaseController : Controller
    {

        ClaimsIdentity userClaims;
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            userClaims = User.Identity as ClaimsIdentity;

            var roleclaims = userClaims?.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            if (userClaims != null)
            {
                for (int i = 0; i < roleclaims.Count(); i++)
                {
                    //var a = roleclaims.ElementAt(i)?.Value;
                    ViewBag.Roles = roleclaims.ElementAt(i).Value;

                }

            }

            base.OnActionExecuting(filterContext);
        }
    }
}