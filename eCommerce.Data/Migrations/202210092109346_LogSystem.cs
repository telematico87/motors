namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogSystem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogSystems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LogDescription = c.String(),
                        LogDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogSystems");
        }
    }
}
