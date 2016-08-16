namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TambahanSuratJalan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SuratJalans", "Closed", c => c.Boolean(nullable: false));
            AddColumn("dbo.SuratJalans", "Cetak", c => c.Boolean(nullable: false));
            AddColumn("dbo.SuratJalans", "Gudang", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SuratJalans", "Gudang");
            DropColumn("dbo.SuratJalans", "Cetak");
            DropColumn("dbo.SuratJalans", "Closed");
        }
    }
}
