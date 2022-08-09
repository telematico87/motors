namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oferta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "EtiquetaOferta", c => c.String());
            AddColumn("dbo.Products", "EtiquetaSoat", c => c.String());
            AddColumn("dbo.Products", "IncluyeSoat", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IncluyeSoat");
            DropColumn("dbo.Products", "EtiquetaSoat");
            DropColumn("dbo.Products", "EtiquetaOferta");
        }
    }
}
