using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.SearchQueries
{
    public class AssetSearchQuery : SearchQueryBase
    {
        public Enums.AssetType ? AssetType { get; set; }
        public string AssetStatus { get; set; }
        public Enums.AvailabilityType ? AvailabilityType { get; set; }
       
        public string AssetNumber { get; set; }
        public string Customer { get; set; }
        public string Model { get; set; }
        public string Manufacture { get; set; }
        public string ManufactureSN { get; set; }
        public string ComputerName { get; set; }
        public string Group { get; set; }
        public string ConferenceRoom { get; set; }
        public string Building { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string LeasePeriod { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? DatePurchasedOrleasedFrom { get; set; }
        public DateTime? DatePurchasedOrleasedTo { get; set; }
    }
}

