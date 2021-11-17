namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assets", "Cost", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assets", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
