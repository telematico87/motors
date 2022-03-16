namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Caracteristica", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Caracteristica");
        }
    }
}
