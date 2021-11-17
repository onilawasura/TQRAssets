namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        Archived = c.Boolean(nullable: false),
                        DateLastUpdated = c.DateTime(),
                        UserLastUpdated = c.String(),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssetOwnerHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssetNumber = c.String(),
                        AssetId = c.Int(nullable: false),
                        OwnerId = c.String(),
                        DateAssigned = c.DateTime(nullable: false),
                        Archived = c.Boolean(nullable: false),
                        DateLastUpdated = c.DateTime(),
                        UserLastUpdated = c.String(),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssetType = c.Int(nullable: false),
                        AssetNumber = c.String(),
                        Manufacture = c.String(),
                        Model = c.String(),
                        ManufactureSN = c.String(),
                        ComputerName = c.String(),
                        UserId = c.String(),
                        Group = c.String(),
                        Customer = c.String(),
                        Availability = c.Int(nullable: false),
                        AssetStatus = c.Int(nullable: false),
                        Vendor = c.String(),
                        DatePurchasedOrleased = c.DateTime(),
                        WarrantyPeriod = c.String(),
                        Location = c.String(),
                        Memory = c.String(),
                        Processor = c.String(),
                        Speed = c.String(),
                        HDD = c.String(),
                        IsSSD = c.Boolean(nullable: false),
                        ProblemReported = c.Boolean(nullable: false),
                        NOTE = c.String(),
                        Rating = c.String(),
                        Screen = c.String(),
                        ConferenceRoom = c.String(),
                        Building = c.String(),
                        Floor = c.String(),
                        DeviceType = c.String(),
                        MobileName = c.String(),
                        Archived = c.Boolean(nullable: false),
                        DateLastUpdated = c.DateTime(),
                        UserLastUpdated = c.String(),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        CurrentProject = c.String(),
                        Gender = c.String(),
                        Designation = c.String(),
                        ExpericeYears = c.String(),
                        Age = c.String(),
                        Expertise = c.String(),
                        YearsAtExilesoft = c.String(),
                        Archived = c.Boolean(nullable: false),
                        DateLastUpdated = c.DateTime(),
                        UserLastUpdated = c.String(),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Assets");
            DropTable("dbo.AssetOwnerHistories");
            DropTable("dbo.Administrators");
        }
    }
}
