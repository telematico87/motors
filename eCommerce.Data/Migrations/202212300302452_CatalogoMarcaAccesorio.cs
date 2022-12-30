namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatalogoMarcaAccesorio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CatalogoCategorias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CatalogoId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CatalogoMarcas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CatalogoId = c.Int(nullable: false),
                        MarcaId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CatalogoMarcas");
            DropTable("dbo.CatalogoCategorias");
        }
    }
}
