using System.Web.Http;
using System.Web.Mvc;

namespace TIQRI.ITS.Web.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            
        }
    }
}