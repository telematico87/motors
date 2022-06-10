namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v61_ubigeo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ubigeos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodUbigeo = c.String(),
                        NombreUbigeo = c.String(),
                        CodPais = c.String(),
                        CodDep = c.String(),
                        CodProv = c.String(),
                        CodDist = c.String(),
                        EstadoUbigeo = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ubigeos");
        }
    }
}
