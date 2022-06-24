namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10_tabla_contacto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contactoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Asunto = c.String(maxLength: 100),
                        Mensaje = c.String(maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contactoes");
        }
    }
}
