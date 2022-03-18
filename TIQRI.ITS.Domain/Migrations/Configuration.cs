namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TIQRI.ITS.Domain.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TIQRI.ITS.Domain.Models.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Manufactures.AddOrUpdate(
                m => m.Name,
                new Models.Manufacture { Name = "Apple" },
                new Models.Manufacture { Name = "Lenovo" },
                new Models.Manufacture { Name = "Dell" }
                );

            context.Models.AddOrUpdate(
              m => m.Name,
              new Models.Model { Name = "T530" },
              new Models.Model { Name = "T560" },
              new Models.Model { Name = "MacB" },
              new Models.Model { Name = "iMac" },
              new Models.Model { Name = "P51" }
              );

            context.AssetStatuses.AddOrUpdate(
             m => m.Name,
             new Models.AssetStatusTbl { Name = "Available" },
             new Models.AssetStatusTbl { Name = "Assigned " },
             new Models.AssetStatusTbl { Name = "Repair" },
             new Models.AssetStatusTbl { Name = "Defective" },
             new Models.AssetStatusTbl { Name = "Disposed" },
             new Models.AssetStatusTbl { Name = "Lost/Missing" },
             new Models.AssetStatusTbl { Name = "Donated" }
             );

            context.Processors.AddOrUpdate(
            m => m.Name,
            new Models.Processor { Name = "Intel Core i9" },
            new Models.Processor { Name = "Intel Core i7" },
            new Models.Processor { Name = "Intel Core i5" },
            new Models.Processor { Name = "Intel Core i3" }
            );

            context.Memories.AddOrUpdate(
            m => m.Name,
            new Models.Memory { Name = "8GB" },
            new Models.Memory { Name = "16GB" },
            new Models.Memory { Name = "24GB" },
            new Models.Memory { Name = "32GB" },
            new Models.Memory { Name = "64GB" }
            );

            context.HardDisks.AddOrUpdate(
            m => m.Name,
            new Models.HardDisk { Name = "256 GB" },
            new Models.HardDisk { Name = "512 GB" },
            new Models.HardDisk { Name = "1 TB" }
            );

            context.ScreenSizes.AddOrUpdate(
            m => m.Name,
            new Models.ScreenSize { Name = "15\"" },
            new Models.ScreenSize { Name = "16\"" },
            new Models.ScreenSize { Name = "17\"" },
            new Models.ScreenSize { Name = "18\"" },
            new Models.ScreenSize { Name = "19\"" },
            new Models.ScreenSize { Name = "20\"" },
            new Models.ScreenSize { Name = "21\"" },
            new Models.ScreenSize { Name = "22\"" },
            new Models.ScreenSize { Name = "23\"" },
            new Models.ScreenSize { Name = "24\"" },
            new Models.ScreenSize { Name = "25\"" },
            new Models.ScreenSize { Name = "26\"" },
            new Models.ScreenSize { Name = "27\"" },
            new Models.ScreenSize { Name = "28\"" },
            new Models.ScreenSize { Name = "29\"" },
            new Models.ScreenSize { Name = "30\"" },
            new Models.ScreenSize { Name = "31\"" },
            new Models.ScreenSize { Name = "32\"" },
            new Models.ScreenSize { Name = "33\"" },
            new Models.ScreenSize { Name = "34\"" },
            );

            context.Vendors.AddOrUpdate(
            m => m.Name,
            new Models.Vendor { Name = "DUSTIN" },
            new Models.Vendor { Name = "TCC" },
            new Models.Vendor { Name = "Thakral" },
            new Models.Vendor { Name = "TATA" }
         );

            context.WarrantyPeriods.AddOrUpdate(
            m => m.Name,
            new Models.WarrantyPeriod { Name = "No Warranty" },
            new Models.WarrantyPeriod { Name = "6 Months" },
            new Models.WarrantyPeriod { Name = "1 Year" },
            new Models.WarrantyPeriod { Name = "2 Years" },
            new Models.WarrantyPeriod { Name = "3 Years" },
            new Models.WarrantyPeriod { Name = "4 Years" }
            );

            context.LeasePeriods.AddOrUpdate(
            m => m.Name,
            new Models.LeasePeriod { Name = "No Lease" },
            new Models.LeasePeriod { Name = "6 Months" },
            new Models.LeasePeriod { Name = "1 Year" },
            new Models.LeasePeriod { Name = "2 Years" },
            new Models.LeasePeriod { Name = "3 Years" }
            );

            context.AssetOwners.AddOrUpdate(
           m => m.Name,
           new Models.AssetOwner { Name = "TIQRI" },
           new Models.AssetOwner { Name = "TIQRI AS" },
           new Models.AssetOwner { Name = "TIQRI AU" },
           new Models.AssetOwner { Name = "Customer" }
           );

           
        }
    }
}
