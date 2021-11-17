namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "LeasePeriod", c => c.String());
            AddColumn("dbo.Assets", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assets", "Cost");
            DropColumn("dbo.Assets", "LeasePeriod");
        }
    }
}
