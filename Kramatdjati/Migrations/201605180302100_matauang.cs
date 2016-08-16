namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matauang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MataUangs",
                c => new
                    {
                        MataUangID = c.Int(nullable: false, identity: true),
                        Kode = c.String(),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.MataUangID);
            
            AddColumn("dbo.PemesananBarangs", "MataUangID", c => c.Int(nullable: false));
            AddColumn("dbo.PemesananBarangs", "TglKurs", c => c.DateTime(nullable: false));
            AddColumn("dbo.PemesananBarangs", "Kurs", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            CreateIndex("dbo.PemesananBarangs", "MataUangID");
            AddForeignKey("dbo.PemesananBarangs", "MataUangID", "dbo.MataUangs", "MataUangID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PemesananBarangs", "MataUangID", "dbo.MataUangs");
            DropIndex("dbo.PemesananBarangs", new[] { "MataUangID" });
            DropColumn("dbo.PemesananBarangs", "Kurs");
            DropColumn("dbo.PemesananBarangs", "TglKurs");
            DropColumn("dbo.PemesananBarangs", "MataUangID");
            DropTable("dbo.MataUangs");
        }
    }
}
