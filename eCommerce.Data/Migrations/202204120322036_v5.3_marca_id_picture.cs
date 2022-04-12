namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v53_marca_id_picture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Marcas", "PictureID", c => c.Int());
            CreateIndex("dbo.Marcas", "PictureID");
            AddForeignKey("dbo.Marcas", "PictureID", "dbo.Pictures", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marcas", "PictureID", "dbo.Pictures");
            DropIndex("dbo.Marcas", new[] { "PictureID" });
            DropColumn("dbo.Marcas", "PictureID");
        }
    }
}
