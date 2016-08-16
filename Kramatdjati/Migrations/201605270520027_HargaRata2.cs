namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HargaRata2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KartuStoks", "HargaRata2", c => c.Decimal(nullable: false, precision: 20, scale: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KartuStoks", "HargaRata2");
        }
    }
}
