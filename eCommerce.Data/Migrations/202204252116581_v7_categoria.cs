namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7_categoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "PictureMovilID", c => c.Int());
            CreateIndex("dbo.Categories", "PictureMovilID");
            AddForeignKey("dbo.Categories", "PictureMovilID", "dbo.Pictures", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "PictureMovilID", "dbo.Pictures");
            DropIndex("dbo.Categories", new[] { "PictureMovilID" });
            DropColumn("dbo.Categories", "PictureMovilID");
        }
    }
}
