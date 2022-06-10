namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMarcaAndCatalogoInProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TablaMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TipoTabla = c.String(),
                        Codigo = c.Int(nullable: false),
                        Name = c.String(),
                        Value = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipoCambios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TipoCambioSunat = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "MarcaId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "CatalogoId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "TipoMoneda", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TipoMoneda");
            DropColumn("dbo.Products", "CatalogoId");
            DropColumn("dbo.Products", "MarcaId");
            DropTable("dbo.TipoCambios");
            DropTable("dbo.TablaMasters");
        }
    }
}
