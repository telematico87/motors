namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fechaSolicitud : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financiamientoes", "FechaSolicitud", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financiamientoes", "FechaSolicitud");
        }
    }
}
