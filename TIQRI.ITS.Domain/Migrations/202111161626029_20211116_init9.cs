namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20211116_init9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetOwners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
            DropTable("dbo.AssetOwners");
        }
    }
}
