namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class corregirColorStock : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductColors", "Picture_ID", "dbo.Pictures");
            DropIndex("dbo.ProductColors", new[] { "Picture_ID" });
            RenameColumn(table: "dbo.ProductColors", name: "Picture_ID", newName: "PictureID");
            AlterColumn("dbo.ProductColors", "PictureID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductColors", "PictureID");
            AddForeignKey("dbo.ProductColors", "PictureID", "dbo.Pictures", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductColors", "PictureID", "dbo.Pictures");
            DropIndex("dbo.ProductColors", new[] { "PictureID" });
            AlterColumn("dbo.ProductColors", "PictureID", c => c.Int());
            RenameColumn(table: "dbo.ProductColors", name: "PictureID", newName: "Picture_ID");
            CreateIndex("dbo.ProductColors", "Picture_ID");
            AddForeignKey("dbo.ProductColors", "Picture_ID", "dbo.Pictures", "ID");
        }
    }
}
