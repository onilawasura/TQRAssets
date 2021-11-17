using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIQRI.ITS.Domain.Enums;
using TIQRI.ITS.Domain.Models;
using AssetStatus = TIQRI.ITS.Domain.Enums.AssetStatus;

namespace TIQRI.ITS.Web.ViewModels
{
    public class AsseReportViewModel
    {
        public string GlobalText { get; set; }
        public AssetType? AssetType { get; set; }
        public string AssetNumber { get; set; }
        public string Manufacture { get; set; }
        public IEnumerable<SelectListItem> ManufactureList { get; set; }
        public string AssetModel { get; set; }
        public IEnumerable<SelectListItem> ModelList { get; set; }
        public string ManufactureSN { get; set; }
        public string ComputerName { get; set; }
        public string UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string Group { get; set; }
        public string Customer { get; set; }
        public AvailabilityType? Availability { get; set; }
        public string AssetStatus { get; set; }
        public string Vendor { get; set; }
        public DateTime? DatePurchasedOrleasedFrom { get; set; }
        public DateTime? DatePurchasedOrleasedTo { get; set; }
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
        public string ConferenceRoom { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string DeviceType { get; set; }
        public string MobileName { get; set; }


        //------------ Binding Lists ------------//
        public IEnumerable<SelectListItem> OwnerList { get; set; }
        public IList<UserProfile> UserProfileList { get; set; }
        public IEnumerable<SelectListItem> AssetStatusList { get; set; }
        public string ProfileObjectString { get; set; }
    }
}