namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v54_edit_entidad_financiera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catalogoes", "Category_ID", c => c.Int());
            AddColumn("dbo.Financiamientoes", "Celular", c => c.String());
            AddColumn("dbo.Financiamientoes", "TipoDocumento", c => c.Int(nullable: false));
            AddColumn("dbo.Financiamientoes", "NroDocumento", c => c.String());
            AddColumn("dbo.Financiamientoes", "InteresCompra", c => c.Int(nullable: false));
            AddColumn("dbo.Financiamientoes", "Provincia", c => c.String());
            AddColumn("dbo.Financiamientoes", "Distrito", c => c.String());
            AddColumn("dbo.Financiamientoes", "TipoVivienda", c => c.Int(nullable: false));
            AddColumn("dbo.Financiamientoes", "PoliticaPrivacidad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Financiamientoes", "AceptoComunicaciones", c => c.Boolean(nullable: false));
            AddColumn("dbo.Financiamientoes", "MontoFinanciar", c => c.Int(nullable: false));
            AlterColumn("dbo.Financiamientoes", "RangoIngreso", c => c.Int(nullable: false));
            CreateIndex("dbo.Catalogoes", "Category_ID");
            AddForeignKey("dbo.Catalogoes", "Category_ID", "dbo.Categories", "ID");
            DropColumn("dbo.Financiamientoes", "FechaNacimiento");
            DropColumn("dbo.Financiamientoes", "NroTelefono");
            DropColumn("dbo.Financiamientoes", "Documento");
            DropColumn("dbo.Financiamientoes", "Perfil");
            DropColumn("dbo.Financiamientoes", "AceptaTermino");
            DropColumn("dbo.Financiamientoes", "CuotaInicial");
            DropColumn("dbo.Financiamientoes", "ActividadLaboral");
            DropColumn("dbo.Financiamientoes", "Direccion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Financiamientoes", "Direccion", c => c.String());
            AddColumn("dbo.Financiamientoes", "ActividadLaboral", c => c.String());
            AddColumn("dbo.Financiamientoes", "CuotaInicial", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Financiamientoes", "AceptaTermino", c => c.Boolean(nullable: false));
            AddColumn("dbo.Financiamientoes", "Perfil", c => c.String());
            AddColumn("dbo.Financiamientoes", "Documento", c => c.String());
            AddColumn("dbo.Financiamientoes", "NroTelefono", c => c.String());
            AddColumn("dbo.Financiamientoes", "FechaNacimiento", c => c.String());
            DropForeignKey("dbo.Catalogoes", "Category_ID", "dbo.Categories");
            DropIndex("dbo.Catalogoes", new[] { "Category_ID" });
            AlterColumn("dbo.Financiamientoes", "RangoIngreso", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Financiamientoes", "MontoFinanciar");
            DropColumn("dbo.Financiamientoes", "AceptoComunicaciones");
            DropColumn("dbo.Financiamientoes", "PoliticaPrivacidad");
            DropColumn("dbo.Financiamientoes", "TipoVivienda");
            DropColumn("dbo.Financiamientoes", "Distrito");
            DropColumn("dbo.Financiamientoes", "Provincia");
            DropColumn("dbo.Financiamientoes", "InteresCompra");
            DropColumn("dbo.Financiamientoes", "NroDocumento");
            DropColumn("dbo.Financiamientoes", "TipoDocumento");
            DropColumn("dbo.Financiamientoes", "Celular");
            DropColumn("dbo.Catalogoes", "Category_ID");
        }
    }
}
