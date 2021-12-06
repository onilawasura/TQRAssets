using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.Models
{
    public class AdministratorAssetStatus :EntityBase
    {
        public string AdminId { get; set; }
        public int? AssetStatusId { get; set; }
    }
}
