using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TIQRI.ITS.Domain.AuditTrail;
using TIQRI.ITS.Domain.Models;
using TIQRI.ITS.Domain.SearchQueries;
using TIQRI.ITS.Domain.Services;
using TIQRI.ITS.Web.ViewModels;

namespace TIQRI.ITS.Web.Controllers
{
    public class AssetReportController : BaseController
    {
        // GET: AssetReport
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
                return RedirectToAction("index", "home");
            if (!Helpers.SecurityHelper.IsAdminAccess(Session["UserName"].ToString()))
                return RedirectToAction("index", "home");

            return View(GetAssetModel());
        }
        public ActionResult SearchAssetList(AsseReportViewModel model)
        {
            return View(GetAssetList(model));
        }

        public IList<Asset> GetAssetList(AsseReportViewModel model) {
            var assetSearchQuery = new AssetSearchQuery();
            if (!string.IsNullOrEmpty(model.GlobalText))
                assetSearchQuery.GlobalText = model.GlobalText.Trim();

            if (model.AssetType != null)
                assetSearchQuery.AssetType = model.AssetType.Value;

            if (model.Availability != null)
                assetSearchQuery.AvailabilityType = model.Availability.Value;

            if (model.AssetStatus != null)
                assetSearchQuery.AssetStatus = model.AssetStatus;

            if (!string.IsNullOrEmpty(model.AssetNumber))
                assetSearchQuery.AssetNumber = model.AssetNumber.Trim();

            if (!string.IsNullOrEmpty(model.Manufacture))
                assetSearchQuery.Manufacture = model.Manufacture.Trim();

            if (!string.IsNullOrEmpty(model.AssetModel))
                assetSearchQuery.Model = model.AssetModel.Trim();

            if (!string.IsNullOrEmpty(model.ManufactureSN))
                assetSearchQuery.ManufactureSN = model.ManufactureSN.Trim();

            if (!string.IsNullOrEmpty(model.ComputerName))
                assetSearchQuery.ComputerName = model.ComputerName.Trim();

            if (!string.IsNullOrEmpty(model.UserId))
                assetSearchQuery.UserId = model.UserId.Trim();

            if (!string.IsNullOrEmpty(model.Group))
                assetSearchQuery.Group = model.Group.Trim();

            if (!string.IsNullOrEmpty(model.Customer))
                assetSearchQuery.Customer = model.Customer.Trim();

            if (!string.IsNullOrEmpty(model.ConferenceRoom))
                assetSearchQuery.ConferenceRoom = model.ConferenceRoom.Trim();

            if (!string.IsNullOrEmpty(model.Building))
                assetSearchQuery.Building = model.Building.Trim();

            if (model.DatePurchasedOrleasedFrom != null)
                assetSearchQuery.DatePurchasedOrleasedFrom = model.DatePurchasedOrleasedFrom.Value;

            if (model.DatePurchasedOrleasedTo != null)
                assetSearchQuery.DatePurchasedOrleasedTo = model.DatePurchasedOrleasedTo.Value;

            var assetList = new AssetService().SearchAsset(assetSearchQuery);
            return assetList;
        }

        private AsseReportViewModel GetAssetModel()
        {
            var AssetViewModel = new AsseReportViewModel();

            var context = new Context();
            AssetViewModel.UserProfileList = UserProfileListItems();
            AssetViewModel.OwnerList = OwnerListItems();
            AssetViewModel.AssetStatusList = AssetStatusList();
            AssetViewModel.ManufactureList = ManufactureList();
            AssetViewModel.ModelList = ModelList();
            return AssetViewModel;
        }

        public IList<UserProfile> UserProfileListItems()
        {
            return Helpers.HRDataHelper.GetBlankEmployeeList();
        }

        public IEnumerable<SelectListItem> OwnerListItems()
        {
            var userProfileList = Helpers.HRDataHelper.GetBlankEmployeeList();
            return userProfileList
                    .Select(userProfile =>
                        new SelectListItem
                        {
                            Text = userProfile.Name,
                            Value = userProfile.UserName
                        });
        }

        public IEnumerable<SelectListItem> AssetStatusList()
        {
            var assetStatusList = Helpers.AssetHelper.GetAssetStatusList();
            return assetStatusList
                    .Select(assetStatus =>
                        new SelectListItem
                        {
                            Text = assetStatus.Name,
                            Value = assetStatus.Name
                        });
        }

        public IEnumerable<SelectListItem> ManufactureList()
        {
            var manufactureList = Helpers.AssetHelper.GetManufactureList();
            return manufactureList
                    .Select(manufacture =>
                        new SelectListItem
                        {
                            Text = manufacture.Name,
                            Value = manufacture.Name
                        });
        }
        public IEnumerable<SelectListItem> ModelList()
        {
            var modelList = Helpers.AssetHelper.GetModelList();
            return modelList
                    .Select(model =>
                        new SelectListItem
                        {
                            Text = model.Name,
                            Value = model.Name
                        });
        }

        public JsonResult Audit(string id)
        {
            var AuditTrail = new AuditHelper(new Context()).GetAudit(id);
            return Json(AuditTrail, JsonRequestBehavior.AllowGet);
        }
    }
}