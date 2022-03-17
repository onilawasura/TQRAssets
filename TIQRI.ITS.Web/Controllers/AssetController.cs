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
using LinqToExcel;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Security.Claims;
using System.Web;
using context = System.Web.HttpContext;
using ExcelDataReader;

namespace TIQRI.ITS.Web.Controllers
{
    public class AssetController : BaseController
    {

        string bulkUploadResult = "";
        string errorMsg = "";
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


        public ActionResult BulkImportAsset(string assetType, int? id)
        {
            return View();
        }

        public FileResult DownloadExcel()
        {
            string path = "../../Content/Sample/assetBulk.xlsx";
            return File(path, "application/vnd.ms-excel", "AssetBulk.xlsx");
        }

        public FileResult DownloadBulkUploadResultsReport()
        {
            string path = "~/Content/BulkUploadStatusReport/bulkUploadResults.txt";
            return File(path, "application/vnd.ms-excel", "bulkUploadResults.txt");
        }


        [HttpPost]
        public JsonResult UploadExcel(HttpPostedFileBase FileUpload)
        {
            var assetType = Request["assetType"];
            List<string> data = new List<string>();
            string filepath = Server.MapPath("~/Content/BulkUploadStatusReport/");

            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Content/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var assetList = new List<Asset>();


                    IExcelDataReader reader = null;
                    ////Load file into a stream
                    FileStream stream = System.IO.File.Open(pathToExcelFile, FileMode.Open, FileAccess.Read);

                    if (System.IO.Path.GetExtension(pathToExcelFile).Equals(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateReader(stream);
                    }
                    else if (System.IO.Path.GetExtension(pathToExcelFile).Equals(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateReader(stream);
                    }

                    if (reader != null)
                    {
                        //Fill DataSet
                        System.Data.DataSet result = reader.AsDataSet();
                        try
                        {
                            //Loop through rows for the desired worksheet
                            //In this case I use the table index "0" to pick the first worksheet in the workbook
                            int counter = 0;
                            foreach (DataRow row in result.Tables[0].Rows)
                            {
                                if (counter == 0)
                                {
                                    counter++;
                                    continue;
                                }
                                string FirstColumn = row[0].ToString();

                                AssetType at = (AssetType)Enum.Parse(typeof(AssetType), assetType);
                                int atNo = (int)at;
                                Asset TA = new Asset();
                                TA.AssetNumber = CreateAssetNumber(at, out int? incrementNumber);
                                TA.AssetType = (AssetType)Enum.Parse(typeof(AssetType), assetType);

                                TA.Model = row[0].ToString();
                                TA.UserId = row[1].ToString();
                                if (!string.IsNullOrEmpty(row[1].ToString()))
                                {
                                    var userProfile = Helpers.HRDataHelper.GetEmployee(row[1].ToString());
                                    if (userProfile != null)
                                        TA.UserDisplayName = userProfile.Name;
                                }
                                TA.Group = row[3].ToString();
                                TA.Customer = row[4].ToString();
                                TA.Processor = row[5].ToString();
                                TA.Memory = row[6].ToString();
                                TA.Speed = row[7].ToString();
                                TA.Manufacture = row[8].ToString();
                                TA.ManufactureSN = row[9].ToString();
                                TA.ComputerName = row[10].ToString();
                                TA.MobileName = row[11].ToString();
                                if (!string.IsNullOrEmpty(row[12].ToString()))
                                {
                                    TA.Availability = (AvailabilityType)Enum.Parse(typeof(AvailabilityType), row[12].ToString());
                                }
                                TA.AssetStatus = row[13].ToString();
                                TA.AssetOwner = row[14].ToString();
                                TA.Screen = row[15].ToString();
                                TA.HDD = row[16].ToString();
                                TA.ProblemReported = Convert.ToBoolean(row[17].ToString());
                                TA.IsSSD = Convert.ToBoolean(row[18].ToString());
                                TA.Vendor = row[19].ToString();
                                TA.WarrantyPeriod = row[20].ToString();
                                TA.DeviceType = row[21].ToString();
                                TA.Cost = Convert.ToDecimal(row[22].ToString());
                                TA.DatePurchasedOrleased = Convert.ToDateTime(row[23].ToString());
                                TA.LeasePeriod = row[24].ToString();
                                TA.Location = row[25].ToString();
                                TA.ConferenceRoom = row[26].ToString();
                                TA.Building = row[27].ToString();
                                TA.Floor = row[28].ToString();
                                TA.Rating = row[29].ToString();
                                TA.NOTE = row[30].ToString();
                                TA.DateLastUpdated = DateTime.UtcNow;

                                ClaimsIdentity userClaims;
                                userClaims = User.Identity as ClaimsIdentity;
                                var claimEmail = userClaims?.FindFirst("preferred_username")?.Value;

                                TA.UserLastUpdated = claimEmail;
                                if (isValid(TA))
                                {
                                    var status = new AssetService().SaveAsset(TA, "", new UserProfile()
                                    {
                                        UserName = claimEmail
                                    });
                                    bulkUploadResult = bulkUploadResult + "Data row No " + counter + ": Success! Asset Id:" + TA.AssetNumber + "\n ";
                                }
                                else
                                {

                                    bulkUploadResult = bulkUploadResult + "Data row No " + counter + ": Failed with the error - " + errorMsg + "\r\n";
                                    //data.Add("Please choose Excel file");
                                    continue;
                                }

                                counter++;

                            }
                            try
                            {

                                if (!Directory.Exists(filepath))
                                {
                                    Directory.CreateDirectory(filepath);

                                }
                                //filepath = Request.PhysicalApplicationPath+"bulkUploadResults.txt";   //Text File Name
                                if (!System.IO.File.Exists(filepath))
                                {

                                }

                                StreamWriter sw = System.IO.File.CreateText(filepath + "bulkUploadResults.txt");
                                string error = "Adding data in line No " + counter + " was unsuccessful";
                                sw.WriteLine("-----------Bulk upload results report on " + " " + DateTime.Now.ToString() + "-----------------");
                                sw.WriteLine("-------------------------------------------------------------------------------------");
                                sw.WriteLine(bulkUploadResult);
                                sw.WriteLine("--------------------------------*End*------------------------------------------");
                                sw.Flush();
                                sw.Close();
                                string path = filepath + "\\bulkUploadResults.txt";
                            }
                            catch (Exception e)
                            {

                            }


                            
                        }
                        catch
                        {

                        }
                        data.Add("Executed successfully");
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        data.Add("Something went wrong");
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }

                    //deleting excel file from folder
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }


                }
                else
                {
                    //alert message for invalid file format
                    data.Add("Only Excel file format is allowed");
                    data.ToArray();
                    StreamWriter sw = System.IO.File.CreateText(filepath + "bulkUploadResults.txt");
                    sw.WriteLine("-----------Bulk upload results report on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("Only Excel file format is allowed");
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    string path1 = filepath + "/bulkUploadResults.txt";
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                if (FileUpload == null) data.Add("Please choose Excel file");
                data.ToArray();
                StreamWriter sw = System.IO.File.CreateText(filepath + "bulkUploadResults.txt");
                sw.WriteLine("-----------Bulk upload results report on " + " " + DateTime.Now.ToString() + "-----------------");
                sw.WriteLine("Please choose Excel file");
                sw.WriteLine("--------------------------------*End*------------------------------------------");
                string path1 = filepath + "/bulkUploadResults.txt";
                return Json(data, JsonRequestBehavior.AllowGet);

            }

            var aa = bulkUploadResult;



        }

        public bool isValid(Asset asset)
        {
            bool valid = true;
            IList<Model> models = Helpers.AssetHelper.GetModelList();
            IList<AssetStatusTbl> assetStatuses = Helpers.AssetHelper.GetAssetStatusList();
            IList<HardDisk> hardDisks = Helpers.AssetHelper.GetHDDList();
            IList<LeasePeriod> leasePeriods = Helpers.AssetHelper.GetLeasePeriodList();
            IList<Manufacture> manufactures = Helpers.AssetHelper.GetManufactureList();
            IList<Memory> memories = Helpers.AssetHelper.GetMemoryList();
            IList<Processor> processors = Helpers.AssetHelper.GetProcessorList();
            IList<ScreenSize> screenSizes = Helpers.AssetHelper.GetScreensList();
            IList<Vendor> vendors = Helpers.AssetHelper.GetVendorsList();
            IList<WarrantyPeriod> warrantyPeriods = Helpers.AssetHelper.GetWarrantyPeriodList();


            if (!asset.Model.Equals("") || asset.Model != null)
            {
                foreach (Model model1 in models)
                {
                    if (model1.Name.Equals(asset.Model))
                    {
                        break;
                    }
                    else
                    {
                        if (model1 == models.ElementAt(models.Count - 1))
                        {
                            errorMsg = "Specified Model Does not match with any";
                            return false;
                        }
                    }
                }
            }

            if (!asset.AssetStatus.Equals("") || asset.AssetStatus != null)
            {
                foreach (AssetStatusTbl assetstatus in assetStatuses)
                {
                    if (assetstatus.Name.Equals(asset.AssetStatus))
                    {
                        if (assetstatus.Name.Equals("Disposed") || assetstatus.Name.Equals("Lost/Missing") || assetstatus.Name.Equals("Donated"))
                        {
                            errorMsg = "Assets cannot be added with status [*Disposed,*Lost/Missing,*Donated]";
                            return false;
                        }

                        break;
                    }
                    else
                    {
                        if (assetstatus == assetStatuses.ElementAt(assetStatuses.Count - 1))
                        {
                            errorMsg = "Specified Asset Status Does not match with any";
                            return false;
                        }
                    }
                }
            }

            if (!asset.HDD.Equals("") || asset.HDD != null)
            {
                foreach (HardDisk hardDisk in hardDisks)
                {
                    if (hardDisk.Name.Equals(asset.HDD))
                    {

                        break;
                    }
                    else
                    {
                        if (hardDisk == hardDisks.ElementAt(hardDisks.Count - 1))
                        {
                            errorMsg = "Specified Hard Disk Does not match with any";
                            return false;
                        }
                    }
                }
            }

            if (!asset.LeasePeriod.Equals("") || asset.LeasePeriod != null)
            {
                foreach (LeasePeriod leasePeriod in leasePeriods)
                {
                    if (leasePeriod.Name.Equals(asset.LeasePeriod))
                    {
                        break;
                    }
                    else
                    {
                        if (leasePeriod == leasePeriods.ElementAt(leasePeriods.Count - 1))
                        {
                            errorMsg = "Specified Lease Period Does not match with any";
                            return false;
                        }
                    }
                }
            }

            if (!asset.Manufacture.Equals("") || asset.Manufacture != null)
            {
                foreach (Manufacture manufacture in manufactures)
                {
                    if (manufacture.Name.Equals(asset.Manufacture))
                    {
                        break;
                    }
                    else
                    {
                        if (manufacture == manufactures.ElementAt(manufactures.Count - 1))
                        {
                            errorMsg = "Specified Manufacture Does not match with any";
                            return false;
                        }
                    }
                }
            }

            if (!asset.Memory.Equals("") || asset.Memory != null)
            {
                foreach (Memory memory in memories)
                {
                    if (memory.Name.Equals(asset.Memory))
                    {
                        break;
                    }
                    else
                    {
                        if (memory == memories.ElementAt(memories.Count - 1))
                        {
                            errorMsg = "Specified Memory Does not match with any";
                            return false;
                        }
                    }
                }
            }
            if (!asset.Processor.Equals("") || asset.Processor != null)
            {
                foreach (Processor processor in processors)
                {
                    if (processor.Name.Equals(asset.Processor))
                    {
                        break;
                    }
                    else
                    {
                        if (processor == processors.ElementAt(processors.Count - 1))
                        {
                            errorMsg = "Specified Processor Does not match with any";
                            return false;
                        }
                    }
                }
            }
            if (asset.Screen.Equals("") || asset.Screen != null)
            {
                foreach (ScreenSize screen in screenSizes)
                {
                    if (screen.Name.Equals(asset.Screen))
                    {
                        break;
                    }
                    else
                    {
                        if (screen == screenSizes.ElementAt(screenSizes.Count - 1))
                        {
                            errorMsg = "Specified Screen Size Does not match with any";
                            return false;
                        }
                    }
                }
            }
            if (!asset.Vendor.Equals("") || asset.Vendor != null)
            {
                foreach (Vendor vendor in vendors)
                {
                    if (vendor.Name.Equals(asset.Vendor))
                    {
                        break;
                    }
                    else
                    {
                        if (vendor == vendors.ElementAt(vendors.Count - 1))
                        {
                            errorMsg = "Specified Vendor Does not match with any";
                            return false;
                        }
                    }
                }
            }
            if (!asset.WarrantyPeriod.Equals("") || asset.WarrantyPeriod != null)
            {
                foreach (WarrantyPeriod warrantyPeriod in warrantyPeriods)
                {
                    if (warrantyPeriod.Name.Equals(asset.WarrantyPeriod))
                    {
                        break;

                    }
                    else
                    {
                        if (warrantyPeriod == warrantyPeriods.ElementAt(warrantyPeriods.Count - 1))
                        {
                            errorMsg = "Specified Warranty Period Does not match with any";
                            return false;
                        }
                    }
                }
            }





            return valid;

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