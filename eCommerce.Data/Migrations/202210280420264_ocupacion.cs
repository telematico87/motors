namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ocupacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financiamientoes", "Ocupacion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financiamientoes", "Ocupacion");
        }
    }
}
