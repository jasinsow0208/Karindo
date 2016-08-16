namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HargaSatuan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KartuStoks", "HargaSatuan", c => c.Decimal(nullable: false, precision: 20, scale: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KartuStoks", "HargaSatuan");
        }
    }
}
