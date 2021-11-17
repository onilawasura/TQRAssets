namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataTab = c.String(),
                        Username = c.String(),
                        UserId = c.String(),
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
            DropTable("dbo.UserMappings");
        }
    }
}
