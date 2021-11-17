using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.Models
{
    public class Administrator : EntityBase
    {
        public string UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
