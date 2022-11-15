namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class financimientoNuevosCampos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financiamientoes", "FechaNacimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Financiamientoes", "AntiguedadLaboral", c => c.Int(nullable: false));
            AddColumn("dbo.Financiamientoes", "IDMarca", c => c.Int(nullable: false));
            AddColumn("dbo.Financiamientoes", "IDModelo", c => c.Int(nullable: false));
            AddColumn("dbo.Financiamientoes", "Precio", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Financiamientoes", "IngresoNeto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Financiamientoes", "MontoAFinanciar", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financiamientoes", "MontoAFinanciar");
            DropColumn("dbo.Financiamientoes", "IngresoNeto");
            DropColumn("dbo.Financiamientoes", "Precio");
            DropColumn("dbo.Financiamientoes", "IDModelo");
            DropColumn("dbo.Financiamientoes", "IDMarca");
            DropColumn("dbo.Financiamientoes", "AntiguedadLaboral");
            DropColumn("dbo.Financiamientoes", "FechaNacimiento");
        }
    }
}
