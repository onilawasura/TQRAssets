using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.Models
{
    public class Asset : EntityBase
    {
        public Enums.AssetType AssetType { get; set; }
        public string AssetNumber { get; set; }
        public string Manufacture { get; set; }
        public string Model { get; set; }
        public string ManufactureSN { get; set; }
        public string ComputerName { get; set; }
        public string UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string Group { get; set; }
        public string Customer { get; set; }
        public Enums.AvailabilityType Availability { get; set; }
        public string AssetStatus { get; set; }
        public string AssetOwner { get; set; }
        public string Vendor { get; set; }
        public DateTime? DatePurchasedOrleased { get; set; }
        public string WarrantyPeriod { get; set; }
        public string Location { get; set; }
        public string Memory { get; set; }
        public string Processor { get; set; }
        public string Speed { get; set; }
        public string HDD { get; set; }
        public bool IsSSD { get; set; }
        public bool ProblemReported { get; set; }
        public string NOTE { get; set; }
        public string Rating { get; set; }
        public string Screen { get; set; }
        public string LeasePeriod { get; set; }
        public decimal? Cost { get; set; }
        public int? IncrementNumber { get; set; }
        public string AssetApproveId { get; set; }
        public bool? IsApproved { get; set; } = false;

        //----- Confirence Room -----//
        public string ConferenceRoom { get; set; }
        public string Building { get; set; }

        //----- Displays + Display Panels-----//
        public string Floor { get; set; }

        //----- Keyboards & Mouse -----//
        public string DeviceType { get; set; }

        //----- Mobile devices -----//
        public string MobileName { get; set; }
    }
}
