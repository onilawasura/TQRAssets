using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using TIQRI.ITS.Domain.Enums;
using TIQRI.ITS.Domain.Helpers;
using TIQRI.ITS.Domain.Models;
using TIQRI.ITS.Domain.SearchQueries;
using TIQRI.ITS.Domain.Services;
using TIQRI.ITS.Web.ViewModels;

namespace TIQRI.ITS.Web.Controllers
{
    public class AssetController : BaseController
    {
        // GET: Asset
        public ActionResult Index(string assetType)
        {
            if (Session["UserName"] == null)
                return RedirectToAction("index", "home");
            if (!Helpers.SecurityHelper.IsAdminAccess(Session["UserName"].ToString()))
                return RedirectToAction("index", "home");

            return View(new AsseHomeViewModel(assetType));
        }

        public ActionResult SearchAssetList(string assetType, string searchText)
        {
            var AssetList = new AssetService().SearchAsset(new AssetSearchQuery()
            {
                GlobalText = searchText,
                AssetType = (AssetType)Enum.Parse(typeof(AssetType), assetType)
            });
            return View(AssetList);
        }
          
        

        public ActionResult AddEditAsset(string assetType,int? id)
        {
            var model = GetAssetModel(id, assetType);
            model.ProfileObjectString = Helpers.ScriptObjectHelper.GetProfileListObjectScript(model.UserProfileList);
            return View(model);
        }

        [HttpPost]
        public ActionResult GetAssetStatusUpdateList(AssetViewModel assetViewModel)
        {
            assetViewModel.AssetStatusApproveList = GetAssetStatusApprovalAdminList(assetViewModel.AssetStatus);
            return Json(assetViewModel.AssetStatusApproveList, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> GetAssetStatusApprovalAdminList(string id)
        {
            var assetStatusId = Helpers.AssetHelper.GetAssetStatusId(id);
            var assetStatusApprovalAdminIdList = Helpers.AssetHelper.GetAssetApprovalAdminIdList(assetStatusId);
            var allEmployeeList = Helpers.HRDataHelper.GetBlankEmployeeList();
            return allEmployeeList.Where(x => assetStatusApprovalAdminIdList.
            Any(y => y.AdminId == x.UserName))
                .Select(z => new SelectListItem
                {
                    Text = z.Name,
                    Value = z.UserName
                }).ToList();
        }

        private AssetViewModel GetAssetModel(int? id, string assetType)
        {
            var AssetViewModel = new AssetViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AssetViewModel.MapFromAsset(context.Assets.Single(a => a.Id == id.Value));
                AssetViewModel.LoggedUser = Session["UserName"].ToString();
            }
            else
            {
                AssetViewModel.AssetType = (AssetType)Enum.Parse(typeof(AssetType), assetType);
                AssetViewModel.AssetNumber = CreateAssetNumber(AssetViewModel.AssetType, out int? incrementNumber);
                AssetViewModel.IncrementNumber = incrementNumber;

            }
            AssetViewModel.UserProfileList = UserProfileListItems();
            AssetViewModel.OwnerList = OwnerListItems();
            AssetViewModel.AssetStatusList = AssetStatusList();
            AssetViewModel.ManufactureList = ManufactureList();
            AssetViewModel.ModelList = ModelList();
            AssetViewModel.ProcessorList = ProcessorsList();
            AssetViewModel.MemoryList = MemoryList();
            AssetViewModel.HDDList = HardDisksList();
            AssetViewModel.ScreenSizeList = ScreensList();
            AssetViewModel.VendorList = VendorsList();
            AssetViewModel.WarrantyPeriodList = WarrantyPeriodList();
            AssetViewModel.LeasePeriodList = LeasePeriodList();
            AssetViewModel.AssetOwnerList = AssetOwnerList();
            AssetViewModel.AssetTypeDisplayText = Helpers.Utility.GetAssetTypeDisplayString(AssetViewModel.AssetType);
            return AssetViewModel;
        }

        private string CreateAssetNumber(AssetType assetType, out int? incrementNumber)
        {
            int assetTypeNumber = (int)assetType;
            int? maxAssetNumber = Helpers.AssetHelper.GetMaxAssetNumber(assetTypeNumber);
            string assetNumber = string.Empty;
            int? nextIncrementNumber = (maxAssetNumber == null ? 0 : maxAssetNumber) + 1;
            string formattedAssetNumber = String.Format("{0:000}", nextIncrementNumber);
            incrementNumber = nextIncrementNumber;
            switch (assetTypeNumber)
            {
                case 0:
                    assetNumber = "TI-NB-IT-";
                    break;
                case 2:
                    assetNumber = "TI-DP-IT-";
                    break;
                case 3:
                    assetNumber = "TI-MO-IT-";
                    break;
                case 4:
                    assetNumber = "TI-KBM-IT-";
                    break;
                case 5:
                    assetNumber = "TI-4G-IT-";
                    break;
                case 6:
                    assetNumber = "Need Format-";
                    break;
                case 7:
                    assetNumber = "TI-OD-IT-";
                    break;
                case 8:
                    assetNumber = "TI-PA-IT-";
                    break;
                case 9:
                    assetNumber = "TI-CHAIR-IT-";
                    break;
                default:
                    break;
            }
            assetNumber = string.Concat(assetNumber, formattedAssetNumber);
            return assetNumber;
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

        public IEnumerable<SelectListItem> AdminAssetStastusList()
        {
            var adminAssetStatusList = Helpers.AssetHelper.GetAdminAssetStatusList();
            var allEmployeeList = Helpers.HRDataHelper.GetBlankEmployeeList();
            return allEmployeeList.Where(x => adminAssetStatusList.
            Any(y => y.AdminId == x.UserName))
                .Select(z => new SelectListItem { 
                    Text = z.Name,
                    Value = z.UserName
                });
        }

        public IEnumerable<SelectListItem> ProcessorsList()
        {
            var processorsList = Helpers.AssetHelper.GetProcessorList();
            return processorsList
                    .Select(processor =>
                        new SelectListItem
                        {
                            Text = processor.Name,
                            Value = processor.Name
                        });
        }
        public IEnumerable<SelectListItem> MemoryList()
        {
            var memoriesList = Helpers.AssetHelper.GetMemoryList();
            return memoriesList
                    .Select(memory =>
                        new SelectListItem
                        {
                            Text = memory.Name,
                            Value = memory.Name
                        });
        }

        public IEnumerable<SelectListItem> HardDisksList()
        {
            var hddList = Helpers.AssetHelper.GetHDDList();
            return hddList
                    .Select(hdd =>
                        new SelectListItem
                        {
                            Text = hdd.Name,
                            Value = hdd.Name
                        });
        }
        public IEnumerable<SelectListItem> ScreensList()
        {
            var screensList = Helpers.AssetHelper.GetScreensList();
            return screensList
                    .Select(screen =>
                        new SelectListItem
                        {
                            Text = screen.Name,
                            Value = screen.Name
                        });
        }
        public IEnumerable<SelectListItem> VendorsList()
        {
            var vendorsList = Helpers.AssetHelper.GetVendorsList();
            return vendorsList
                    .Select(vendor =>
                        new SelectListItem
                        {
                            Text = vendor.Name,
                            Value = vendor.Name
                        });
        }
        public IEnumerable<SelectListItem> WarrantyPeriodList()
        {
            var warrantyPeriodList = Helpers.AssetHelper.GetWarrantyPeriodList();
            return warrantyPeriodList
                    .Select(wp =>
                        new SelectListItem
                        {
                            Text = wp.Name,
                            Value = wp.Name
                        });
        }
        public IEnumerable<SelectListItem> LeasePeriodList()
        {
            var leasePeriodList = Helpers.AssetHelper.GetLeasePeriodList();
            return leasePeriodList
                    .Select(lp =>
                        new SelectListItem
                        {
                            Text = lp.Name,
                            Value = lp.Name
                        });
        }

        public IEnumerable<SelectListItem> AssetOwnerList()
        {
            var assetOwnerList = Helpers.AssetHelper.GetAssetOwnerList();
            return assetOwnerList
                    .Select(ao =>
                        new SelectListItem
                        {
                            Text = ao.Name,
                            Value = ao.Name
                        });
        }
    }
}