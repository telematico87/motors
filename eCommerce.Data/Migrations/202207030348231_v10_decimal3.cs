namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10_decimal3 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.TipoCambios", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            //AlterColumn("dbo.TipoCambios", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            //AlterColumn("dbo.TipoCambios", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 3));

        }
        
        public override void Down()
        {
            //AlterColumn("dbo.TipoCambios", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //AlterColumn("dbo.TipoCambios", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //AlterColumn("dbo.TipoCambios", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
