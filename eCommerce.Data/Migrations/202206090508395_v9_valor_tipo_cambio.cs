namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9_valor_tipo_cambio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TipoCambios", "Venta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TipoCambios", "Compra", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TipoCambios", "Compra", c => c.Double(nullable: false));
            AlterColumn("dbo.TipoCambios", "Venta", c => c.Double(nullable: false));
        }
    }
}
