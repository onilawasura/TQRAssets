﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIQRI.ITS.Domain.Enums;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Web.Helpers
{
    public static class AssetHelper
    {
        public static IList<Manufacture> GetManufactureList()
        {
            var manufactureDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[Manufactures] order by [Name]", "DefaultConnection");

            var manufactureList = new List<Manufacture>();
            for (int i = 0; i < manufactureDT.Rows.Count; i++)
            {
                manufactureList.Add(new Manufacture()
                {
                    Id = int.Parse(manufactureDT.Rows[i]["Id"].ToString()),
                   Name = manufactureDT.Rows[i]["Name"].ToString()
                });
            } 

            return manufactureList;
        }

         public static IList<Model> GetModelList()
        {
            var modelDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[Models] order by [Name]", "DefaultConnection");

            var modelList = new List<Model>();
            for (int i = 0; i < modelDT.Rows.Count; i++)
            {
                modelList.Add(new Model()
                {
                    Id = int.Parse(modelDT.Rows[i]["Id"].ToString()),
                   Name = modelDT.Rows[i]["Name"].ToString()
                });
            } 

            return modelList;
        }

        public static IList<AssetStatusTbl> GetAssetStatusList()
        {
            var assetStatusDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[AssetStatusTbls] order by [Name]", "DefaultConnection");

            var assetStatusList = new List<AssetStatusTbl>();
            for (int i = 0; i < assetStatusDT.Rows.Count; i++)
            {
                assetStatusList.Add(new AssetStatusTbl()
                {
                    Id = int.Parse(assetStatusDT.Rows[i]["Id"].ToString()),
                    Name = assetStatusDT.Rows[i]["Name"].ToString()
                });
            }

            return assetStatusList;
        }

        public static IList<Processor> GetProcessorList()
        {
            var processorsDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[Processors] order by [Name]", "DefaultConnection");

            var processorsList = new List<Processor>();
            for (int i = 0; i < processorsDT.Rows.Count; i++)
            {
                processorsList.Add(new Processor()
                {
                    Id = int.Parse(processorsDT.Rows[i]["Id"].ToString()),
                    Name = processorsDT.Rows[i]["Name"].ToString()
                });
            }

            return processorsList;
        }
        public static IList<Memory> GetMemoryList()
        {
            var memoriesDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[Memories] order by [Name]", "DefaultConnection");

            var memoriesList = new List<Memory>();
            for (int i = 0; i < memoriesDT.Rows.Count; i++)
            {
                memoriesList.Add(new Memory()
                {
                    Id = int.Parse(memoriesDT.Rows[i]["Id"].ToString()),
                    Name = memoriesDT.Rows[i]["Name"].ToString()
                });
            }

            return memoriesList;
        }
        public static int? GetMaxAssetNumber(int assetType)
        {
            int? incrementNumber = null;
            //var assetNumberDT =
            //    SqlHelper.ExecuteStatement(
            //        "SELECT MAX(IncrementNumber) AS IncrementNumber FROM [dbo].[Assets] WHERE assettype= " + assetType, "DefaultConnection");

            string sqlQuery = @"DECLARE @ModelID NVARCHAR(MAX)
                                SELECT @ModelID = AssetNumber FROM [dbo].[Assets] WHERE id = (SELECT MAX(Id) FROM [dbo].[Assets])
                                SELECT CAST(SUBSTRING(@ModelID, PATINDEX('%[0-9]%', @ModelID), LEN(@ModelID)) AS INT) AS IncrementNumber";



            var assetNumberDT = SqlHelper.ExecuteStatement(sqlQuery, "DefaultConnection");
            if (assetNumberDT.Rows.Count > 0)
            {
                if (assetNumberDT.Rows[0]["IncrementNumber"].ToString() != string.Empty)
                {
                    incrementNumber = int.Parse(assetNumberDT.Rows[0]["IncrementNumber"].ToString());
                }
            }
            return incrementNumber;
        }

        public static IList<HardDisk> GetHDDList()
        {
            var hddsDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[HardDisks] order by [Name]", "DefaultConnection");

            var memoriesList = new List<HardDisk>();
            for (int i = 0; i < hddsDT.Rows.Count; i++)
            {
                memoriesList.Add(new HardDisk()
                {
                    Id = int.Parse(hddsDT.Rows[i]["Id"].ToString()),
                    Name = hddsDT.Rows[i]["Name"].ToString()
                });
            }

            return memoriesList;
        }
        public static IList<ScreenSize> GetScreensList()
        {
            var screensDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[ScreenSizes] order by [Name]", "DefaultConnection");

            var screensList = new List<ScreenSize>();
            for (int i = 0; i < screensDT.Rows.Count; i++)
            {
                screensList.Add(new ScreenSize()
                {
                    Id = int.Parse(screensDT.Rows[i]["Id"].ToString()),
                    Name = screensDT.Rows[i]["Name"].ToString()
                });
            }

            return screensList;
        }
        public static IList<Vendor> GetVendorsList()
        {
            var vendorsDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[Vendors] order by [Name]", "DefaultConnection");

            var vendorsList = new List<Vendor>();
            for (int i = 0; i < vendorsDT.Rows.Count; i++)
            {
                vendorsList.Add(new Vendor()
                {
                    Id = int.Parse(vendorsDT.Rows[i]["Id"].ToString()),
                    Name = vendorsDT.Rows[i]["Name"].ToString()
                });
            }

            return vendorsList;
        }
        public static IList<Vendor> GetWarrantyPeriodList()
        {
            var warrantyPeriodDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[WarrantyPeriods] order by [Name]", "DefaultConnection");

            var warrantyPeriodList = new List<Vendor>();
            for (int i = 0; i < warrantyPeriodDT.Rows.Count; i++)
            {
                warrantyPeriodList.Add(new Vendor()
                {
                    Id = int.Parse(warrantyPeriodDT.Rows[i]["Id"].ToString()),
                    Name = warrantyPeriodDT.Rows[i]["Name"].ToString()
                });
            }

            return warrantyPeriodList;
        }
        public static IList<LeasePeriod> GetLeasePeriodList()
        {
            var leasePeriodDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [Id],[Name] FROM [dbo].[LeasePeriods] order by [Name]", "DefaultConnection");

            var leasePeriodList = new List<LeasePeriod>();
            for (int i = 0; i < leasePeriodDT.Rows.Count; i++)
            {
                leasePeriodList.Add(new LeasePeriod()
                {
                    Id = int.Parse(leasePeriodDT.Rows[i]["Id"].ToString()),
                    Name = leasePeriodDT.Rows[i]["Name"].ToString()
                });
            }

            return leasePeriodList;
        }
    }
}