namespace TIQRI.ITS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AuditDateTimeUtc = c.DateTime(nullable: false),
                        AuditType = c.String(),
                        AuditUser = c.String(),
                        TableName = c.String(),
                        KeyValues = c.String(),
                        OldValues = c.String(),
                        NewValues = c.String(),
                        ChangedColumns = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}
