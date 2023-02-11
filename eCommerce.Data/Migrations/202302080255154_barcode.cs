namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class barcode : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductStocks", new[] { "SKU" });
            AddColumn("dbo.Tallas", "Barcode", c => c.String());
            AddColumn("dbo.Tallas", "Orden", c => c.Int(nullable: false));
            AddColumn("dbo.ProductStocks", "Precio", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductStocks", "Precio");
            DropColumn("dbo.Tallas", "Orden");
            DropColumn("dbo.Tallas", "Barcode");
            CreateIndex("dbo.ProductStocks", "SKU", unique: true);
        }
    }
}
