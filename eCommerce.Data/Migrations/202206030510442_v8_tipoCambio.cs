namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8_tipoCambio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoCambios", "Venta", c => c.Double(nullable: false));
            AddColumn("dbo.TipoCambios", "Compra", c => c.Double(nullable: false));
            AddColumn("dbo.TipoCambios", "Fecha", c => c.DateTime(nullable: false));
            DropColumn("dbo.TipoCambios", "TipoCambioSunat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TipoCambios", "TipoCambioSunat", c => c.Double(nullable: false));
            DropColumn("dbo.TipoCambios", "Fecha");
            DropColumn("dbo.TipoCambios", "Compra");
            DropColumn("dbo.TipoCambios", "Venta");
        }
    }
}
