using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.Models
{
    public class UserMapping : EntityBase
    {
        public string DataTab { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
    }
}
