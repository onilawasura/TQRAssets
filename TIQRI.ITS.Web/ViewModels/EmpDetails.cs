using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIQRI.ITS.Web.ViewModels
{
    public class EmpDetails
    {
        public string EmpName { get; set; }
        public List<ProductDetails> ProductList { get; set; }
    }
}