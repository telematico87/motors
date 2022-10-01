namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productcolorstock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductColors", "ColorValue", c => c.String());
            AddColumn("dbo.ProductColors", "Stock", c => c.Double(nullable: false));
            AddColumn("dbo.ProductColors", "Picture_ID", c => c.Int());
            CreateIndex("dbo.ProductColors", "Picture_ID");
            AddForeignKey("dbo.ProductColors", "Picture_ID", "dbo.Pictures", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductColors", "Picture_ID", "dbo.Pictures");
            DropIndex("dbo.ProductColors", new[] { "Picture_ID" });
            DropColumn("dbo.ProductColors", "Picture_ID");
            DropColumn("dbo.ProductColors", "Stock");
            DropColumn("dbo.ProductColors", "ColorValue");
        }
    }
}
