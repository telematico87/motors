namespace eCommerce.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiosfinanciamiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financiamientoes", "IDSituacionLaboral", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financiamientoes", "IDSituacionLaboral");
        }
    }
}
