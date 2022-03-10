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
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var assets = from a in excelFile.Worksheet<Asset>(sheetName) select a;
                    string reportPath = "../../Content/BulkUploadStatusReport/bulkUploadResults.txt";
                    var count = 0;

                    foreach (var a in assets)
                    {
                        count++;
                        try
                        {
                            AssetType at = (AssetType)Enum.Parse(typeof(AssetType), assetType);
                            int atNo = (int)at;
                            if (isValid(a))
                            {
                                Asset TA = new Asset();
                                TA.AssetNumber = CreateAssetNumber(at, out int? incrementNumber);
                                TA.AssetType = (AssetType)Enum.Parse(typeof(AssetType), assetType);
                                if (atNo == 0 || atNo == 1)
                                {
                                    TA.AssetOwner = a.AssetOwner;
                                    TA.Model = a.Model;
                                    TA.UserDisplayName = a.UserDisplayName;
                                    TA.UserId = a.UserId;
                                    TA.Processor = a.Processor;
                                    TA.Memory = a.Memory;
                                    TA.Manufacture = a.Manufacture;
                                    TA.ManufactureSN = a.ManufactureSN;
                                    TA.AssetStatus = a.AssetStatus;
                                    TA.Screen = a.Screen;
                                    TA.HDD = a.HDD;
                                    TA.ProblemReported = a.ProblemReported;
                                    TA.Vendor = a.Vendor;
                                    TA.WarrantyPeriod = a.WarrantyPeriod;
                                    TA.Cost = a.Cost;
                                    TA.DatePurchasedOrleased = a.DatePurchasedOrleased;
                                    TA.LeasePeriod = a.LeasePeriod;
                                    TA.NOTE = a.NOTE;
                                }
                                else if (atNo == 2)
                                {
                                    TA.AssetOwner = a.AssetOwner;
                                    TA.Model = a.Model;
                                    TA.Processor = a.Processor;
                                    TA.Memory = a.Memory;
                                    TA.Manufacture = a.Manufacture;
                                    TA.ManufactureSN = a.ManufactureSN;
                                    TA.Screen = a.Screen;
                                    TA.HDD = a.HDD;
                                    TA.ProblemReported = a.ProblemReported;
                                    TA.Vendor = a.Vendor;
                                    TA.WarrantyPeriod = a.WarrantyPeriod;
                                    TA.Cost = a.Cost;
                                    TA.DatePurchasedOrleased = a.DatePurchasedOrleased;
                                    TA.LeasePeriod = a.LeasePeriod;
                                    TA.ConferenceRoom = a.ConferenceRoom;
                                    TA.Building = a.Building;
                                    TA.NOTE = a.NOTE;
                                }
                                else if (atNo == 3 || atNo == 6 || atNo == 8)
                                {
                                    TA.AssetOwner = a.AssetOwner;
                                    TA.Model = a.Model;
                                    TA.UserDisplayName = a.UserDisplayName;
                                    TA.UserId = a.UserId;
                                    TA.Group = a.Group;
                                    TA.Building = a.Building;
                                    TA.Availability = a.Availability;
                                    TA.Manufacture = a.Manufacture;
                                    TA.ManufactureSN = a.ManufactureSN;
                                    TA.Customer = a.Customer;
                                    TA.Floor = a.Floor;
                                    TA.AssetStatus = a.AssetStatus;
                                    TA.NOTE = a.NOTE;

                                }
                                else if (atNo == 4 || atNo == 7 || atNo == 9)
                                {
                                    TA.AssetOwner = a.AssetOwner;
                                    TA.Model = a.Model;
                                    TA.DeviceType = a.DeviceType;
                                    TA.UserDisplayName = a.UserDisplayName;
                                    TA.UserId = a.UserId;
                                    TA.Group = a.Group;
                                    TA.Availability = a.Availability;
                                    TA.Manufacture = a.Manufacture;
                                    TA.ManufactureSN = a.ManufactureSN;
                                    TA.ConferenceRoom = a.ConferenceRoom;
                                    TA.Customer = a.Customer;
                                    TA.AssetStatus = a.AssetStatus;
                                    TA.Location = a.Location;
                                    TA.Rating = a.Rating;
                                    TA.Vendor = a.Vendor;
                                    TA.WarrantyPeriod = a.WarrantyPeriod;
                                    TA.DatePurchasedOrleased = a.DatePurchasedOrleased;
                                    TA.ProblemReported = a.ProblemReported;
                                    TA.NOTE = a.NOTE;

                                }
                                else if (atNo == 5)
                                {
                                    TA.AssetOwner = a.AssetOwner;
                                    TA.Model = a.Model;
                                    TA.MobileName = a.MobileName;
                                    TA.UserDisplayName = a.UserDisplayName;
                                    TA.UserId = a.UserId;
                                    TA.Group = a.Group;
                                    TA.Availability = a.Availability;
                                    TA.Manufacture = a.Manufacture;
                                    TA.ManufactureSN = a.ManufactureSN;
                                    TA.Customer = a.Customer;
                                    TA.AssetStatus = a.AssetStatus;
                                    TA.Vendor = a.Vendor;
                                    TA.WarrantyPeriod = a.WarrantyPeriod;
                                    TA.Cost = a.Cost;
                                    TA.DatePurchasedOrleased = a.DatePurchasedOrleased;
                                    TA.Location = a.Location;
                                    TA.Rating = a.Rating;
                                    TA.ProblemReported = a.ProblemReported;
                                    TA.NOTE = a.NOTE;

                                }

                                TA.DateLastUpdated = DateTime.UtcNow;
                                ClaimsIdentity userClaims;
                                userClaims = User.Identity as ClaimsIdentity;
                                var claimEmail = userClaims?.FindFirst("preferred_username")?.Value;
                                TA.UserLastUpdated = claimEmail;

                                string ApproverName = "";
                                if (!String.IsNullOrEmpty(a.AssetApproveId))
                                {
                                    ApproverName = Helpers.HRDataHelper.GetEmployee(a.AssetApproveId).Name;
                                }
                                var status = new AssetService().SaveAsset(TA, ApproverName, new UserProfile()
                                {
                                    UserName = claimEmail
                                });

                                bulkUploadResult = bulkUploadResult + "Data row No " + count + ": Success! Asset Id:"+TA.AssetNumber+"\n ";

                            }
                            else
                            {

                                data.Add("Adding data in line No " + count + " was unsuccessful");
                                bulkUploadResult = bulkUploadResult + "Data row No " + count + ": Failed with the error - " + errorMsg + "\r\n";
                                //data.Add("Please choose Excel file");
                                //return Json(data, JsonRequestBehavior.AllowGet);
                                continue;
                            }

                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }
                    //deleting excel file from folder
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
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
                        string error = "Adding data in line No " + count + " was unsuccessful";
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


                    return Json(data, JsonRequestBehavior.AllowGet);



                }
                else
                {
                    //alert message for invalid file format
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
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
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
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