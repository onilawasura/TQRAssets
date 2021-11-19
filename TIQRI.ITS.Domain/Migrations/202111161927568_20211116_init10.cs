namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20211116_init10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "AssetOwner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assets", "AssetOwner");
        }
    }
}
