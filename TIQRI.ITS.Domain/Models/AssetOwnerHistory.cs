using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.Models
{
    public class AssetOwnerHistory : EntityBase
    {
        public string AssetNumber { get; set; }
        public int AssetId { get; set; }
        public string OwnerId { get; set; }
        public DateTime DateAssigned { get; set; }
    }
}
