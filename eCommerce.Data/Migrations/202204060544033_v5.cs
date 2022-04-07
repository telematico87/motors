namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Financiamientoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        FechaNacimiento = c.String(),
                        Correo = c.String(),
                        NroTelefono = c.String(),
                        Departamento = c.String(),
                        Documento = c.String(),
                        Marca = c.String(),
                        Modelo = c.String(),
                        Perfil = c.String(),
                        SituacionLaboral = c.String(),
                        RangoIngreso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AceptaTermino = c.Boolean(nullable: false),
                        TipoFinanciera = c.Int(nullable: false),
                        CuotaInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActividadLaboral = c.String(),
                        Direccion = c.String(),
                        SituacionSentimental = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Financiamientoes");
        }
    }
}
