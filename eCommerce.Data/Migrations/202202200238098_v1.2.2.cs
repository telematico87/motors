namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v122 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ProductsAroes", "ProductID");
            CreateIndex("dbo.ProductColors", "ProductID");
            CreateIndex("dbo.ProductLitros", "ProductID");
            CreateIndex("dbo.ProductMedidas", "ProductID");
            CreateIndex("dbo.ProductModeloes", "ProductID");
            CreateIndex("dbo.ProductTallas", "ProductID");
            AddForeignKey("dbo.ProductsAroes", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductColors", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductLitros", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductMedidas", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductModeloes", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductTallas", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTallas", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductModeloes", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductMedidas", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductLitros", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductColors", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductsAroes", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductTallas", new[] { "ProductID" });
            DropIndex("dbo.ProductModeloes", new[] { "ProductID" });
            DropIndex("dbo.ProductMedidas", new[] { "ProductID" });
            DropIndex("dbo.ProductLitros", new[] { "ProductID" });
            DropIndex("dbo.ProductColors", new[] { "ProductID" });
            DropIndex("dbo.ProductsAroes", new[] { "ProductID" });
        }
    }
}
