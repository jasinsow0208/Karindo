namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gudang1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PenerimaanBarangs", "GudangID", c => c.Int(nullable: false));
            AddColumn("dbo.SuratJalans", "GudangID", c => c.Int(nullable: false));
            AddColumn("dbo.StokOpnames", "GudangID", c => c.Int(nullable: false));
            CreateIndex("dbo.PenerimaanBarangs", "GudangID");
            CreateIndex("dbo.StokOpnames", "GudangID");
            //AddForeignKey("dbo.PenerimaanBarangs", "GudangID", "dbo.Gudangs", "GudangID", cascadeDelete: false);
            //AddForeignKey("dbo.StokOpnames", "GudangID", "dbo.Gudangs", "GudangID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.StokOpnames", "GudangID", "dbo.Gudangs");
            //DropForeignKey("dbo.PenerimaanBarangs", "GudangID", "dbo.Gudangs");
            DropIndex("dbo.StokOpnames", new[] { "GudangID" });
            DropIndex("dbo.PenerimaanBarangs", new[] { "GudangID" });
            DropColumn("dbo.StokOpnames", "GudangID");
            DropColumn("dbo.SuratJalans", "GudangID");
            DropColumn("dbo.PenerimaanBarangs", "GudangID");
        }
    }
}
