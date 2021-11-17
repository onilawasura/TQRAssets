using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Web.ViewModels
{
    public class AddNewDataViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string UserName { get; set; }
        public bool isEdit { get; set; }
        public string viewType { get; set; }
        public void MapFromModel (Model model)
        {
            this.Id = model.Id;
            this.name = model.Name;
            this.viewType = "Model";

        }

        public Model MapToAsset()
        {
            var model = new Model();
            model.Id = this.Id;
            model.Name = this.name;

            //if (!string.IsNullOrEmpty(asset.UserId))
            //{
            //    var userProfile = Helpers.HRDataHelper.GetEmployee(asset.UserId);
            //    if (userProfile != null)
            //        asset.UserDisplayName = userProfile.Name;
            //}

            return model;
        }

        public Manufacture MapToManufacturer()
        {
            var manufacture = new Manufacture();
            manufacture.Id = this.Id;
            manufacture.Name = this.name;


            return manufacture;
        }

        public void MapFromManufacturer(Manufacture manufacture)
        {
            this.Id = manufacture.Id;
            this.name = manufacture.Name;
            this.viewType = "Manufacturer";

        }

        public Memory MapToMemory()
        {
            var memory = new Memory();
            memory.Id = this.Id;
            memory.Name = this.name;


            return memory;
        }

        public void MapFromMemory(Memory memory)
        {
            this.Id = memory.Id;
            this.name = memory.Name;
            this.viewType = "Memory";

        }

        public Processor MapToProcessor()
        {
            var processor = new Processor();
            processor.Id = this.Id;
            processor.Name = this.name;


            return processor;
        }

        public void MapFromProcessor(Processor processor)
        {
            this.Id = processor.Id;
            this.name = processor.Name;
            this.viewType = "Processor";

        }

        public HardDisk MapToHardDisk()
        {
            var hardDisk = new HardDisk();
            hardDisk.Id = this.Id;
            hardDisk.Name = this.name;


            return hardDisk;
        }

        public void MapFromHardDisk(HardDisk hardDisk)
        {
            this.Id = hardDisk.Id;
            this.name = hardDisk.Name;
            this.viewType = "HardDisk";

        }

        public ScreenSize MapToScreenSize()
        {
            var screenSize = new ScreenSize();
            screenSize.Id = this.Id;
            screenSize.Name = this.name;


            return screenSize;
        }

        public void MapFromScreenSize(ScreenSize screenSize)
        {
            this.Id = screenSize.Id;
            this.name = screenSize.Name;
            this.viewType = "ScreenSize";

        }

        public Vendor MapToVendor()
        {
            var vendor = new Vendor();
            vendor.Id = this.Id;
            vendor.Name = this.name;


            return vendor;
        }

        public void MapFromVendor(Vendor vendor)
        {
            this.Id = vendor.Id;
            this.name = vendor.Name;
            this.viewType = "Vendor";

        }

        public LeasePeriod MapToLeasePeriod()
        {
            var leasePeriod = new LeasePeriod();
            leasePeriod.Id = this.Id;
            leasePeriod.Name = this.name;


            return leasePeriod;
        }

        public void MapFromLeasePeriod(LeasePeriod leasePeriod)
        {
            this.Id = leasePeriod.Id;
            this.name = leasePeriod.Name;
            this.viewType = "LeasePeriod";

        }

        public WarrantyPeriod MapToWarrantyPeriod()
        {
            var warrantyPeriod = new WarrantyPeriod();
            warrantyPeriod.Id = this.Id;
            warrantyPeriod.Name = this.name;


            return warrantyPeriod;
        }

        public void MapFromWarrantyPeriod(WarrantyPeriod warrantyPeriod)
        {
            this.Id = warrantyPeriod.Id;
            this.name = warrantyPeriod.Name;
            this.viewType = "WarrantyPeriod";

        }
    }
}