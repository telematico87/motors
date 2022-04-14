namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v53_catalogo_intocateg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CatalogoID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CatalogoID");
        }
    }
}
