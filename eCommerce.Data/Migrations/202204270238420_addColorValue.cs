namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColorValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Colors", "Valor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Colors", "Valor");
        }
    }
}
