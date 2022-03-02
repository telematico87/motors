namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aroes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Litros",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Medidas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Modeloes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductsAroes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AroID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Aroes", t => t.AroID, cascadeDelete: true)
                .Index(t => t.AroID);
            
            CreateTable(
                "dbo.ProductColors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ColorID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Colors", t => t.ColorID, cascadeDelete: true)
                .Index(t => t.ColorID);
            
            CreateTable(
                "dbo.ProductLitros",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LitrosID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Litros", t => t.LitrosID, cascadeDelete: true)
                .Index(t => t.LitrosID);
            
            CreateTable(
                "dbo.ProductMedidas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MedidaID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Medidas", t => t.MedidaID, cascadeDelete: true)
                .Index(t => t.MedidaID);
            
            CreateTable(
                "dbo.ProductModeloes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModeloID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Modeloes", t => t.ModeloID, cascadeDelete: true)
                .Index(t => t.ModeloID);
            
            CreateTable(
                "dbo.ProductTallas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TallaID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tallas", t => t.TallaID, cascadeDelete: true)
                .Index(t => t.TallaID);
            
            CreateTable(
                "dbo.Tallas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        PrecioTalla = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTallas", "TallaID", "dbo.Tallas");
            DropForeignKey("dbo.ProductModeloes", "ModeloID", "dbo.Modeloes");
            DropForeignKey("dbo.ProductMedidas", "MedidaID", "dbo.Medidas");
            DropForeignKey("dbo.ProductLitros", "LitrosID", "dbo.Litros");
            DropForeignKey("dbo.ProductColors", "ColorID", "dbo.Colors");
            DropForeignKey("dbo.ProductsAroes", "AroID", "dbo.Aroes");
            DropIndex("dbo.ProductTallas", new[] { "TallaID" });
            DropIndex("dbo.ProductModeloes", new[] { "ModeloID" });
            DropIndex("dbo.ProductMedidas", new[] { "MedidaID" });
            DropIndex("dbo.ProductLitros", new[] { "LitrosID" });
            DropIndex("dbo.ProductColors", new[] { "ColorID" });
            DropIndex("dbo.ProductsAroes", new[] { "AroID" });
            DropTable("dbo.Tallas");
            DropTable("dbo.ProductTallas");
            DropTable("dbo.ProductModeloes");
            DropTable("dbo.ProductMedidas");
            DropTable("dbo.ProductLitros");
            DropTable("dbo.ProductColors");
            DropTable("dbo.ProductsAroes");
            DropTable("dbo.Modeloes");
            DropTable("dbo.Medidas");
            DropTable("dbo.Litros");
            DropTable("dbo.Colors");
            DropTable("dbo.Aroes");
        }
    }
}
