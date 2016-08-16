namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pemakaian : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PemakaianPengembalianBarangRincians",
                c => new
                    {
                        PemakaianPengembalianBarangRincianID = c.Int(nullable: false, identity: true),
                        PemakaianPengembalianBarangID = c.Int(nullable: false),
                        GudangBahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.PemakaianPengembalianBarangRincianID)
                .ForeignKey("dbo.GudangBahanBakus", t => t.GudangBahanBakuID, cascadeDelete: true)
                .ForeignKey("dbo.PemakaianPengembalianBarangs", t => t.PemakaianPengembalianBarangID, cascadeDelete: true)
                .Index(t => t.PemakaianPengembalianBarangID)
                .Index(t => t.GudangBahanBakuID);
            
            CreateTable(
                "dbo.PemakaianPengembalianBarangs",
                c => new
                    {
                        PemakaianPengembalianBarangID = c.Int(nullable: false, identity: true),
                        GudangID = c.Int(nullable: false),
                        NoSuratPemakaian = c.String(),
                        Status = c.String(),
                        tglKeluarBarang = c.DateTime(nullable: false),
                        Posting = c.Boolean(nullable: false),
                        TglPosting = c.DateTime(nullable: false),
                        User = c.String(),
                        Penerima = c.String(),
                    })
                .PrimaryKey(t => t.PemakaianPengembalianBarangID)
                .ForeignKey("dbo.Gudangs", t => t.GudangID, cascadeDelete: false)
                .Index(t => t.GudangID);
            
            AddColumn("dbo.BPPBRincians", "PenerimaanReal", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AddColumn("dbo.BPPBRincians", "PostingPenerimaanReal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PemakaianPengembalianBarangRincians", "PemakaianPengembalianBarangID", "dbo.PemakaianPengembalianBarangs");
            DropForeignKey("dbo.PemakaianPengembalianBarangs", "GudangID", "dbo.Gudangs");
            DropForeignKey("dbo.PemakaianPengembalianBarangRincians", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropIndex("dbo.PemakaianPengembalianBarangs", new[] { "GudangID" });
            DropIndex("dbo.PemakaianPengembalianBarangRincians", new[] { "GudangBahanBakuID" });
            DropIndex("dbo.PemakaianPengembalianBarangRincians", new[] { "PemakaianPengembalianBarangID" });
            DropColumn("dbo.BPPBRincians", "PostingPenerimaanReal");
            DropColumn("dbo.BPPBRincians", "PenerimaanReal");
            DropTable("dbo.PemakaianPengembalianBarangs");
            DropTable("dbo.PemakaianPengembalianBarangRincians");
        }
    }
}
