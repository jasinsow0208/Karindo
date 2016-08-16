namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PindahGudang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PindahGudangs",
                c => new
                    {
                        PindahGudangID = c.Int(nullable: false, identity: true),
                        BuktiPindahGudang = c.String(),
                        GudangAsalID = c.Int(nullable: false),
                        GudangTujuanID = c.Int(nullable: false),
                        TglTransaksi = c.DateTime(nullable: false),
                        UserGudangAsal = c.String(),
                        UserGudangTujuan = c.String(),
                        Posting = c.Boolean(nullable: false),
                        TglPosting = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PindahGudangID)
                .ForeignKey("dbo.Gudangs", t => t.GudangAsalID)
                .ForeignKey("dbo.Gudangs", t => t.GudangTujuanID)
                .Index(t => t.GudangAsalID)
                .Index(t => t.GudangTujuanID);
            
            CreateTable(
                "dbo.PindahGudangRincians",
                c => new
                    {
                        PindahGudangRincianID = c.Int(nullable: false, identity: true),
                        PindahGudangID = c.Int(nullable: false),
                        GudangBahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.PindahGudangRincianID)
                .ForeignKey("dbo.PindahGudangs", t => t.PindahGudangID, cascadeDelete: true)
                .Index(t => t.PindahGudangID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PindahGudangRincians", "PindahGudangID", "dbo.PindahGudangs");
            DropForeignKey("dbo.PindahGudangs", "GudangTujuanID", "dbo.Gudangs");
            DropForeignKey("dbo.PindahGudangs", "GudangAsalID", "dbo.Gudangs");
            DropIndex("dbo.PindahGudangRincians", new[] { "PindahGudangID" });
            DropIndex("dbo.PindahGudangs", new[] { "GudangTujuanID" });
            DropIndex("dbo.PindahGudangs", new[] { "GudangAsalID" });
            DropTable("dbo.PindahGudangRincians");
            DropTable("dbo.PindahGudangs");
        }
    }
}
