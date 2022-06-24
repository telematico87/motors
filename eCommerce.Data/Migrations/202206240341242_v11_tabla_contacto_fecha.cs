namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11_tabla_contacto_fecha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contactoes", "Fecha", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contactoes", "Fecha");
        }
    }
}
