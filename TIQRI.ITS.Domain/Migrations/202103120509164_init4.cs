namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assets", "AssetStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assets", "AssetStatus", c => c.Int(nullable: false));
        }
    }
}
