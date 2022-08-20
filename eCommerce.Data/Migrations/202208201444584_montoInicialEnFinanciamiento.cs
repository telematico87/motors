namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class montoInicialEnFinanciamiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financiamientoes", "TieneInicial", c => c.Boolean(nullable: false));
            AddColumn("dbo.Financiamientoes", "MontoInicial", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financiamientoes", "MontoInicial");
            DropColumn("dbo.Financiamientoes", "TieneInicial");
        }
    }
}
