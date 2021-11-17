using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIQRI.ITS.Web.ViewModels
{
    public class HomeViewModel
    {
        public string LoggedUserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}