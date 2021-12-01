using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TIQRI.ITS.Domain.Models;
using TIQRI.ITS.Domain.Services;
using TIQRI.ITS.Web.ViewModels;

namespace TIQRI.ITS.Web.Controllers.Api
{
    public class AssetController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> SaveAsset(AssetViewModel model)
        {
            var status = await new AssetService().SaveAsset(model.MapToAsset(), new UserProfile()
            {
                UserName = model.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveModel(AddNewDataViewModel model)
        {
            var status = await new AssetService().SaveModel(model.MapToAsset(), new UserProfile()
            {
                UserName = model.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveManufacturer(AddNewDataViewModel manufacturer)
        {
            var status = await new AssetService().SaveManufacturer(manufacturer.MapToManufacturer(), new UserProfile()
            {
                UserName = manufacturer.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveMemory(AddNewDataViewModel memory)
        {
            var status = await new AssetService().SaveMemory(memory.MapToMemory(), new UserProfile()
            {
                UserName = memory.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SaveProcessor(AddNewDataViewModel processor)
        {
            var status = await new AssetService().SaveProcessor(processor.MapToProcessor(), new UserProfile()
            {
                UserName = processor.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveHardDisk(AddNewDataViewModel hardDisk)
        {
            var status = await new AssetService().SaveHardDisk(hardDisk.MapToHardDisk(), new UserProfile()
            {
                UserName = hardDisk.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveScreenSize(AddNewDataViewModel screenSize)
        {
            var status = await new AssetService().SaveScreenSize(screenSize.MapToScreenSize(), new UserProfile()
            {
                UserName = screenSize.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SaveVendor(AddNewDataViewModel vendor)
        {
            var status = await new AssetService().SaveVendor(vendor.MapToVendor(), new UserProfile()
            {
                UserName = vendor.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SaveLeasePeriod(AddNewDataViewModel leasePeriod)
        {
            var status = await new AssetService().SaveLeasePeriod(leasePeriod.MapToLeasePeriod(), new UserProfile()
            {
                UserName = leasePeriod.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveWarrantyPeriod(AddNewDataViewModel leasePeriod)
        {
            var status = await new AssetService().SaveWarrantyPeriod(leasePeriod.MapToWarrantyPeriod(), new UserProfile()
            {
                UserName = leasePeriod.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveAssetOwner(AddNewDataViewModel assetOwner)
        {
            var status = await new AssetService().SaveAssetOwner(assetOwner.MapToAssetOwner(), new UserProfile()
            {
                UserName = assetOwner.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
