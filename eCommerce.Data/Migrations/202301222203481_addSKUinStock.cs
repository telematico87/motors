namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSKUinStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductStocks", "SKU", c => c.String(maxLength: 16));
            CreateIndex("dbo.ProductStocks", "SKU", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductStocks", new[] { "SKU" });
            DropColumn("dbo.ProductStocks", "SKU");
        }
    }
}
