namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "UserDisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assets", "UserDisplayName");
        }
    }
}
