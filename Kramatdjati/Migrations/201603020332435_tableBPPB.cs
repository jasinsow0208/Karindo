namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableBPPB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BPPBRincians",
                c => new
                    {
                        BPPBRincianID = c.Int(nullable: false, identity: true),
                        BPPBID = c.Int(nullable: false),
                        GudangBahanBakuID = c.Int(nullable: false),
                        Kebutuhan = c.Decimal(nullable: false, precision: 20, scale: 5),
                        Pembulatan = c.Decimal(nullable: false, precision: 20, scale: 5),
                        SatuanZak = c.Decimal(nullable: false, precision: 20, scale: 5),
                        JmlZak = c.Int(nullable: false),
                        PostingDiserahkan = c.Boolean(nullable: false),
                        Diserahkan = c.String(),
                        PostingDiterima = c.Boolean(nullable: false),
                        Diterima = c.String(),
                    })
                .PrimaryKey(t => t.BPPBRincianID)
                .ForeignKey("dbo.BPPBs", t => t.BPPBID, cascadeDelete: true)
                .ForeignKey("dbo.GudangBahanBakus", t => t.GudangBahanBakuID, cascadeDelete: true)
                .Index(t => t.BPPBID)
                .Index(t => t.GudangBahanBakuID);
            
            CreateTable(
                "dbo.BPPBs",
                c => new
                    {
                        BPPBID = c.Int(nullable: false, identity: true),
                        TglPenimbangan = c.DateTime(nullable: false),
                        TglProduksi = c.DateTime(nullable: false),
                        NoBPPB = c.Int(nullable: false),
                        Keterangan = c.String(),
                        Diminta = c.String(),
                        Diserahkan = c.String(),
                        Diterima = c.String(),
                    })
                .PrimaryKey(t => t.BPPBID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BPPBRincians", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropForeignKey("dbo.BPPBRincians", "BPPBID", "dbo.BPPBs");
            DropIndex("dbo.BPPBRincians", new[] { "GudangBahanBakuID" });
            DropIndex("dbo.BPPBRincians", new[] { "BPPBID" });
            DropTable("dbo.BPPBs");
            DropTable("dbo.BPPBRincians");
        }
    }
}
