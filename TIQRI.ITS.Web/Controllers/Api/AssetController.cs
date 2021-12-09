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
            string ApproverName = "";
            if (!String.IsNullOrEmpty(model.AssetApproveId))
            {
                ApproverName = Helpers.HRDataHelper.GetEmployee(model.AssetApproveId).Name;
            }

            var status = await new AssetService().SaveAsset(model.MapToAsset(), ApproverName ,new UserProfile()
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
        public async Task<HttpResponseMessage> UpdateAssetStatusAprroval(AssetViewModel model)
        {
            var status = await new AssetService().UpdateAssetStatusAprroval(model.MapToAsset(), new UserProfile()
            {
                UserName = model.UserName
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async void VerifyAssetAvailability()
        {
            //var allEmployeeList = Helpers.HRDataHelper.GetBlankEmployeeList();
            //foreach(var employee in allEmployeeList)
            //{
            //var empDetalsDT = Helpers.AssetHelper.GetEmployeeProductsDetails(employee.UserName, employee.Name);
            var empDetalsDT = Helpers.AssetHelper.GetEmployeeProductsDetails("tpr@tiqri.com", "Thushara Priyantha");
            //}
            string subject = "Verify Asset Availability";
            //string body = "Hi " + employee.Name +", ";
            string body = "Hi Onila Pemathunghe, ";
            body += "<br> <br> ";
            body += "According to the Asset System below item/s is/are " +
                "possessed by you. If the list is not accurate or even one " +
                "of the mentioned items is not possessed by you please let " +
                "us know immedietly to provided email below";
            body += "<br> <br> ";
            body += "<table border=1 >";
            body += "<tr>" +
                    "<th> Asset Type </th>" +
                    "<th> Manufacture </th>" +
                    "<th> Model </th>" +
                    "<th> Asset Number </th>" +
                    "<th> Manufacture serial Number </th>" +
                    "</tr>";
            for (int x = 0; x < empDetalsDT.ProductList.Count; x++)
            {
                body += "<tr>" +
                    "<td>" + empDetalsDT.ProductList[x].AssetType + "</td>" +
                    "<td>" + empDetalsDT.ProductList[x].Manufacture + "</td>" +
                    "<td>" + empDetalsDT.ProductList[x].Model + "</td>" +
                    "<td>" + empDetalsDT.ProductList[x].AssetNumber + "</td>"+
                    "<td>" + empDetalsDT.ProductList[x].ManufactureSN + "</td>";
            }
            body += "</table>";
            body += "<br> <br> ";
            body += "Email - IT@tiqri.com";
            body += "<br> <br> ";
            body += "Thanks and Regards From HR !";
            body += "<br> <br> ";
            body += "<br> <br> ";
            AssetService assetService = new AssetService();
            assetService.SendVerifyAssetDetails("onp@tiqri.com", subject, body);
            ///Thread.Sleep(5000);
        }

    }

    public class UpdateAssetStatus
    {
        public string AssetNumber { get; set; }
        public string UserId { get; set; }
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
