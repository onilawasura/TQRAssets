using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIQRI.ITS.Domain.Enums;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Web.ViewModels
{
    public class AsseHomeViewModel
    {
        public AsseHomeViewModel(string type)
        {
            AssetType = type;
            AssetTypeDisplay = Helpers.Utility.GetAssetTypeDisplayString((AssetType)Enum.Parse(typeof(AssetType), AssetType));
        }
        public string AssetType { get; set; }
        public string AssetTypeDisplay { get; set; }
    }
    public class AssetListViewModel
    {
        public IList<Asset> Assetes { get; set; }
    }

    public class AssetViewModel
    {
        public int Id { get; set; }

        public AssetType AssetType { get; set; }
        public string AssetNumber { get; set; }
        public string Manufacture { get; set; }
        public IEnumerable<SelectListItem> ManufactureList { get; set; }
        public string Model { get; set; }
        public IEnumerable<SelectListItem> ModelList { get; set; }
        public string ManufactureSN { get; set; }
        public string ComputerName { get; set; }
        public string UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string Group { get; set; }
        public string Customer { get; set; }
        public AvailabilityType Availability { get; set; }
        //public Domain.Enums.AssetStatus AssetStatus { get; set; }
        public string AssetStatus { get; set; }
        public IEnumerable<SelectListItem> AssetStatusList { get; set; }
        public string AssetOwner { get; set; }
        public IEnumerable<SelectListItem> AssetOwnerList { get; set; }
        public string Vendor { get; set; }
        public IEnumerable<SelectListItem> VendorList { get; set; }
        public DateTime? DatePurchasedOrleased { get; set; }
        public string WarrantyPeriod { get; set; }
        public IEnumerable<SelectListItem> WarrantyPeriodList { get; set; }
        public string Location { get; set; }
        public string Memory { get; set; }
        public IEnumerable<SelectListItem> MemoryList { get; set; }
        public string Processor { get; set; }
        public IEnumerable<SelectListItem> ProcessorList { get; set; }
        public string Speed { get; set; }
        public string HDD { get; set; }
        public IEnumerable<SelectListItem> HDDList { get; set; }
        public bool IsSSD { get; set; }
        public bool ProblemReported { get; set; }
        public string NOTE { get; set; }
        public string Rating { get; set; }
        public string Screen { get; set; }
        public IEnumerable<SelectListItem> ScreenSizeList { get; set; }
        public string ConferenceRoom { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string DeviceType { get; set; }
        public string MobileName { get; set; }
        public string LeasePeriod { get; set; }
        public IEnumerable<SelectListItem> LeasePeriodList { get; set; }
        public decimal? Cost { get; set; }
        //------------ Binding Lists ------------//
        public IEnumerable<SelectListItem> OwnerList { get; set; }
        public IList<UserProfile> UserProfileList { get; set; }
        public string ProfileObjectString { get; set; }

        public string AssetTypeDisplayText { get; set; }

        public string UserName { get; set; }
        public int? IncrementNumber { get; set; }
        public string AssetApproveId { get; set; }
        public IEnumerable<SelectListItem> AssetStatusApproveList { get; set; }
        public bool? IsApproved { get; set; }
        public string LoggedUser { get; set; }

        public void MapFromAsset(Asset asset)
        {
            this.Id = asset.Id;
            this.AssetType = asset.AssetType;
            this.AssetNumber = asset.AssetNumber;
            this.Manufacture = asset.Manufacture;
            this.Model = asset.Model;
            this.ManufactureSN = asset.ManufactureSN;
            this.ComputerName = asset.ComputerName;
            this.UserId = asset.UserId;
            this.Group = asset.Group;
            this.Customer = asset.Customer;
            this.Availability = asset.Availability;
            this.AssetStatus = asset.AssetStatus;
            this.AssetOwner = asset.AssetOwner;
            this.Vendor = asset.Vendor;
            this.DatePurchasedOrleased = asset.DatePurchasedOrleased;
            this.WarrantyPeriod = asset.WarrantyPeriod;
            this.Location = asset.Location;
            this.Memory = asset.Memory;
            this.Processor = asset.Processor;
            this.Speed = asset.Speed;
            this.HDD = asset.HDD;
            this.IsSSD = asset.IsSSD;
            this.ProblemReported = asset.ProblemReported;
            this.NOTE = asset.NOTE;
            this.Rating = asset.Rating;
            this.Screen = asset.Screen;
            this.ConferenceRoom = asset.ConferenceRoom;
            this.Building = asset.Building;
            this.Floor = asset.Floor;
            this.DeviceType = asset.DeviceType;
            this.MobileName = asset.MobileName;
            this.LeasePeriod = asset.LeasePeriod;
            this.Cost = asset.Cost;
            this.IncrementNumber = asset.IncrementNumber;
            this.AssetApproveId = asset.AssetApproveId;
            this.IsApproved = asset.IsApproved;
        }

        public Asset MapToAsset()
        {
            var asset = new Asset();
            asset.Id = this.Id;
            asset.AssetType = this.AssetType;
            asset.AssetNumber = this.AssetNumber;
            asset.Manufacture = this.Manufacture;
            asset.Model = this.Model;
            asset.ManufactureSN = this.ManufactureSN;
            asset.ComputerName = this.ComputerName;
            asset.UserId = this.UserId;
            asset.Group = this.Group;
            asset.Customer = this.Customer;
            asset.Availability = this.Availability;
            asset.AssetStatus = this.AssetStatus;
            asset.AssetOwner = this.AssetOwner;
            asset.Vendor = this.Vendor;
            asset.DatePurchasedOrleased = this.DatePurchasedOrleased;
            asset.WarrantyPeriod = this.WarrantyPeriod;
            asset.Location = this.Location;
            asset.Memory = this.Memory;
            asset.Processor = this.Processor;
            asset.Speed = this.Speed;
            asset.HDD = this.HDD;
            asset.IsSSD = this.IsSSD;
            asset.ProblemReported = this.ProblemReported;
            asset.NOTE = this.NOTE;
            asset.Rating = this.Rating;
            asset.Screen = this.Screen;
            asset.ConferenceRoom = this.ConferenceRoom;
            asset.Building = this.Building;
            asset.Floor = this.Floor;
            asset.DeviceType = this.DeviceType;
            asset.MobileName = this.MobileName;
            asset.LeasePeriod = this.LeasePeriod;
            asset.Cost = this.Cost;
            asset.IncrementNumber = this.IncrementNumber;
            asset.AssetApproveId = this.AssetApproveId;

            if (!string.IsNullOrEmpty(asset.UserId))
            {
                var userProfile = Helpers.HRDataHelper.GetEmployee(asset.UserId);
                if (userProfile != null)
                    asset.UserDisplayName = userProfile.Name;
            }

            return asset;
        }

        public string GetPurchasedDisplayText()
        {
            if (this.DatePurchasedOrleased != null)
                return DatePurchasedOrleased.Value.ToShortDateString();
            else
                return string.Empty;
        }
    }
}