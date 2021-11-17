namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "IncrementNumber", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assets", "IncrementNumber");
        }
    }
}
