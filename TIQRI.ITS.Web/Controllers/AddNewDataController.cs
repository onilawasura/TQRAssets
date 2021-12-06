using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIQRI.ITS.Domain.Enums;
using TIQRI.ITS.Domain.Models;
using TIQRI.ITS.Domain.SearchQueries;
using TIQRI.ITS.Domain.Services;
using TIQRI.ITS.Web.ViewModels;

namespace TIQRI.ITS.Web.Controllers
{
    public class AddNewDataController : BaseController
    {
        // GET: AddNewData
        public ActionResult Index()
        { 
            return View();
            
        }
        public ActionResult NewDataView(int? id)
        {
            AddNewDataViewModel addNewDataViewModel = new AddNewDataViewModel();

            if (id != null)
            {
                addNewDataViewModel = GetModel(id);
                addNewDataViewModel.isEdit = true;
            }
            else
            {
                addNewDataViewModel.isEdit = false;
            }
            return View(addNewDataViewModel);
        }

        public ActionResult EditDataView(int? id,string type)
        {
            AddNewDataViewModel addNewDataViewModel = new AddNewDataViewModel();

            if (type == "Model")
            {
                addNewDataViewModel = GetModel(id);
            }
            else if (type == "Manuf")
            {
                addNewDataViewModel = GetManufacturer(id);
            }
            else if (type == "Memory")
            {
                addNewDataViewModel = GetMemory(id);
            }
            else if (type == "Processor")
            {
                addNewDataViewModel = GetProcessor(id);
            }
            else if (type == "HardDisk")
            {
                addNewDataViewModel = GetHardDisk(id);
            }
            else if (type == "ScreenSize")
            {
                addNewDataViewModel = GetScreenSize(id);
            }
            else if (type == "Vendor")
            {
                addNewDataViewModel = GetVendor(id);
            }
            else if (type == "LeasePeriod")
            {
                addNewDataViewModel = GetLeasePeriod(id);
            }
            else if (type == "WarrantyPeriod")
            {
                addNewDataViewModel = GetWarrantyPeriod(id);
            }
            else if (type == "AssetOwner")
            {
                addNewDataViewModel = GetAssetOwner(id);
            }
            return View(addNewDataViewModel);
        }


        private AddNewDataViewModel GetModel(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromModel(context.Models.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }

        private AddNewDataViewModel GetManufacturer(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromManufacturer(context.Manufactures.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }

        private AddNewDataViewModel GetMemory(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromMemory(context.Memories.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }

        private AddNewDataViewModel GetHardDisk(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromHardDisk(context.HardDisks.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }

        private AddNewDataViewModel GetProcessor(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromProcessor(context.Processors.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }
        private AddNewDataViewModel GetScreenSize(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromScreenSize(context.ScreenSizes.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }
        private AddNewDataViewModel GetVendor(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromVendor(context.Vendors.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }
        private AddNewDataViewModel GetLeasePeriod(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromLeasePeriod(context.LeasePeriods.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }

        private AddNewDataViewModel GetWarrantyPeriod(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromWarrantyPeriod(context.WarrantyPeriods.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }

        private AddNewDataViewModel GetAssetOwner(int? id)
        {
            var AddNewDataViewModel = new AddNewDataViewModel();

            var context = new Context();
            if (id != null && id != 0)
            {
                AddNewDataViewModel.MapFromAssetOwner(context.AssetOwners.Single(a => a.Id == id.Value));
            }
            else
            {

            }
            return AddNewDataViewModel;
        }

        public ActionResult SearchModelList(string searchText)
        {
            var modelList = new AssetService().SearchModel(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            }) ;

            return View(modelList);
        }

        public ActionResult SearchManufList(string searchText)
        {
            var ManufacturerList = new AssetService().SearchManufaturer(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(ManufacturerList);
        }

        public ActionResult SearchMemoryList(string searchText)
        {
            var memoryList = new AssetService().SearchMemory(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(memoryList);
        }

        public ActionResult SearchProcessorList(string searchText)
        {
            var processorList = new AssetService().SearchProcessor(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(processorList);
        }

        public ActionResult SearchHardDiskList(string searchText)
        {
            var processorList = new AssetService().SearchHardDisk(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(processorList);
        }

        public ActionResult SearchScreenSizeList(string searchText)
        {
            var processorList = new AssetService().SearchScreenSize(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(processorList);
        }

        public ActionResult SearchVendorList(string searchText)
        {
            var processorList = new AssetService().SearchVendor(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(processorList);
        }

        public ActionResult SearchLeasePeriodList(string searchText)
        {
            var processorList = new AssetService().SearchLeasePeriod(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(processorList);
        }

        public ActionResult SearchWarrantyPeriodList(string searchText)
        {
            var processorList = new AssetService().SearchWarrantyPeriod(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(processorList);
        }

        public ActionResult SearchAssetOwnerList(string searchText)
        {
            var processorList = new AssetService().SearchAssetOwner(new ModelOrManufSearchQuery()
            {
                GlobalText = searchText
            });

            return View(processorList);
        }
    }
}