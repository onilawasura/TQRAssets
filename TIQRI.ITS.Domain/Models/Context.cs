using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using TIQRI.ITS.Domain.AuditTrail;

namespace TIQRI.ITS.Domain.Models
{
    public class Context : DbContext, IAuditDbContext
    {
        public Context()
            : base("DefaultConnection")
        {
            
        }

        //public override int SaveChanges()
        //{
        //    throw new InvalidOperationException("User ID must be provided");
        //}
        public int SaveChanges(string userName)
        {
            new AuditHelper(this).AddAuditLogs(userName);
            var result = base.SaveChanges();
            return result;
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetOwnerHistory> AssetOwnerHistories { get; set; }
        public DbSet<UserMapping> UserMappings { get; set; }
        public DbSet<AssetStatusTbl> AssetStatuses { get; set; }
        public DbSet<HardDisk> HardDisks { get; set; }
        public DbSet<LeasePeriod> LeasePeriods { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<WarrantyPeriod> WarrantyPeriods { get; set; }
        public DbSet<ScreenSize> ScreenSizes { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
