namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v121 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTallas", "PrecioTalla", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Tallas", "PrecioTalla");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tallas", "PrecioTalla", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ProductTallas", "PrecioTalla");
        }
    }
}
