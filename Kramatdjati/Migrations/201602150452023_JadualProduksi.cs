namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JadualProduksi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderBahanBakus", "SalesOrderRincianID", "dbo.SalesOrderRincians");
            DropIndex("dbo.OrderBahanBakus", new[] { "SalesOrderRincianID" });
            RenameColumn(table: "dbo.OrderBahanBakus", name: "SalesOrderRincianID", newName: "SalesOrderRincian_SalesOrderRincianID");
            CreateTable(
                "dbo.JPDeptARincians",
                c => new
                    {
                        JPDeptARincianID = c.Int(nullable: false, identity: true),
                        JPDeptAID = c.Int(nullable: false),
                        KodeBarangJadi = c.String(),
                        Batch = c.Int(nullable: false),
                        Lembar = c.Int(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.JPDeptARincianID)
                .ForeignKey("dbo.JPDeptAs", t => t.JPDeptAID, cascadeDelete: true)
                .Index(t => t.JPDeptAID);
            
            CreateTable(
                "dbo.JPDeptAs",
                c => new
                    {
                        JPDeptAID = c.Int(nullable: false, identity: true),
                        TglProduksi = c.DateTime(nullable: false),
                        DibuatOleh = c.String(),
                        Catatan = c.String(),
                    })
                .PrimaryKey(t => t.JPDeptAID);
            
            CreateTable(
                "dbo.JPDeptASOes",
                c => new
                    {
                        JPDeptASOID = c.Int(nullable: false, identity: true),
                        JPDeptARincianID = c.Int(nullable: false),
                        SalesOrderRincianID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JPDeptASOID)
                .ForeignKey("dbo.JPDeptARincians", t => t.JPDeptARincianID, cascadeDelete: true)
                .ForeignKey("dbo.SalesOrderRincians", t => t.SalesOrderRincianID, cascadeDelete: true)
                .Index(t => t.JPDeptARincianID)
                .Index(t => t.SalesOrderRincianID);
            
            CreateTable(
                "dbo.LPDeptARincians",
                c => new
                    {
                        LPDeptARincianID = c.Int(nullable: false, identity: true),
                        LPDeptAID = c.Int(nullable: false),
                        JPDeptARincianID = c.Int(nullable: false),
                        Hasil = c.Int(nullable: false),
                        Cacat = c.Int(nullable: false),
                        SisaCompound = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Keterangan = c.String(),
                        JmlTransfer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LPDeptARincianID)
                .ForeignKey("dbo.JPDeptARincians", t => t.JPDeptARincianID, cascadeDelete: true)
                .ForeignKey("dbo.LPDeptAs", t => t.LPDeptAID, cascadeDelete: true)
                .Index(t => t.LPDeptAID)
                .Index(t => t.JPDeptARincianID);
            
            CreateTable(
                "dbo.JPDeptBRincians",
                c => new
                    {
                        JPDeptBRincianID = c.Int(nullable: false, identity: true),
                        JPDeptBID = c.Int(nullable: false),
                        LPDeptARincianID = c.Int(nullable: false),
                        UkuranTebal = c.Int(nullable: false),
                        Banyaknya = c.Int(nullable: false),
                        JmlBahan = c.Int(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.JPDeptBRincianID)
                .ForeignKey("dbo.JPDeptBs", t => t.JPDeptBID, cascadeDelete: true)
                .ForeignKey("dbo.LPDeptARincians", t => t.LPDeptARincianID, cascadeDelete: true)
                .Index(t => t.JPDeptBID)
                .Index(t => t.LPDeptARincianID);
            
            CreateTable(
                "dbo.JPDeptBs",
                c => new
                    {
                        JPDeptBID = c.Int(nullable: false, identity: true),
                        TglProduksi = c.DateTime(nullable: false),
                        DibuatOleh = c.String(),
                        Catatan = c.String(),
                    })
                .PrimaryKey(t => t.JPDeptBID);
            
            CreateTable(
                "dbo.LPDeptBRincians",
                c => new
                    {
                        LPDeptBRincianID = c.Int(nullable: false, identity: true),
                        LPDeptBID = c.Int(nullable: false),
                        JPDeptBRincianID = c.Int(nullable: false),
                        Hasil = c.Int(nullable: false),
                        Cacat = c.Int(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.LPDeptBRincianID)
                .ForeignKey("dbo.JPDeptBRincians", t => t.JPDeptBRincianID, cascadeDelete: true)
                .ForeignKey("dbo.LPDeptBs", t => t.LPDeptBID, cascadeDelete: true)
                .Index(t => t.LPDeptBID)
                .Index(t => t.JPDeptBRincianID);
            
            CreateTable(
                "dbo.LPDeptBs",
                c => new
                    {
                        LPDeptBID = c.Int(nullable: false, identity: true),
                        TglProduksi = c.DateTime(nullable: false),
                        Dilaporkan = c.String(),
                        Diketahui = c.String(),
                        JamKerjaAwal = c.DateTime(nullable: false),
                        JamKerjaAkhir = c.DateTime(nullable: false),
                        Catatan = c.String(),
                    })
                .PrimaryKey(t => t.LPDeptBID);
            
            CreateTable(
                "dbo.LPDeptAs",
                c => new
                    {
                        LPDeptAID = c.Int(nullable: false, identity: true),
                        TglProduksi = c.DateTime(nullable: false),
                        PenimbanganAwal = c.DateTime(nullable: false),
                        PenimbanganAkhir = c.DateTime(nullable: false),
                        KneaderAwal = c.DateTime(nullable: false),
                        KneaderAkhir = c.DateTime(nullable: false),
                        BoilerAwal = c.DateTime(nullable: false),
                        BoilerAkhir = c.DateTime(nullable: false),
                        RollAwal = c.DateTime(nullable: false),
                        RollAkhir = c.DateTime(nullable: false),
                        HotPressBAwal = c.DateTime(nullable: false),
                        HotPressBAkhir = c.DateTime(nullable: false),
                        HotPressKAwal = c.DateTime(nullable: false),
                        HotPressKAkhir = c.DateTime(nullable: false),
                        Dilaporkan = c.String(),
                        Diketahui = c.String(),
                        Catatan = c.String(),
                    })
                .PrimaryKey(t => t.LPDeptAID);
            
            AddColumn("dbo.OrderBahanBakus", "JPDeptARincianID", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderBahanBakus", "SalesOrderRincian_SalesOrderRincianID", c => c.Int());
            CreateIndex("dbo.OrderBahanBakus", "JPDeptARincianID");
            CreateIndex("dbo.OrderBahanBakus", "SalesOrderRincian_SalesOrderRincianID");
            AddForeignKey("dbo.OrderBahanBakus", "JPDeptARincianID", "dbo.JPDeptARincians", "JPDeptARincianID", cascadeDelete: true);
            AddForeignKey("dbo.OrderBahanBakus", "SalesOrderRincian_SalesOrderRincianID", "dbo.SalesOrderRincians", "SalesOrderRincianID");
            DropColumn("dbo.OrderBahanBakus", "JmlResep");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderBahanBakus", "JmlResep", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.OrderBahanBakus", "SalesOrderRincian_SalesOrderRincianID", "dbo.SalesOrderRincians");
            DropForeignKey("dbo.OrderBahanBakus", "JPDeptARincianID", "dbo.JPDeptARincians");
            DropForeignKey("dbo.LPDeptARincians", "LPDeptAID", "dbo.LPDeptAs");
            DropForeignKey("dbo.LPDeptBRincians", "LPDeptBID", "dbo.LPDeptBs");
            DropForeignKey("dbo.LPDeptBRincians", "JPDeptBRincianID", "dbo.JPDeptBRincians");
            DropForeignKey("dbo.JPDeptBRincians", "LPDeptARincianID", "dbo.LPDeptARincians");
            DropForeignKey("dbo.JPDeptBRincians", "JPDeptBID", "dbo.JPDeptBs");
            DropForeignKey("dbo.LPDeptARincians", "JPDeptARincianID", "dbo.JPDeptARincians");
            DropForeignKey("dbo.JPDeptASOes", "SalesOrderRincianID", "dbo.SalesOrderRincians");
            DropForeignKey("dbo.JPDeptASOes", "JPDeptARincianID", "dbo.JPDeptARincians");
            DropForeignKey("dbo.JPDeptARincians", "JPDeptAID", "dbo.JPDeptAs");
            DropIndex("dbo.LPDeptBRincians", new[] { "JPDeptBRincianID" });
            DropIndex("dbo.LPDeptBRincians", new[] { "LPDeptBID" });
            DropIndex("dbo.JPDeptBRincians", new[] { "LPDeptARincianID" });
            DropIndex("dbo.JPDeptBRincians", new[] { "JPDeptBID" });
            DropIndex("dbo.LPDeptARincians", new[] { "JPDeptARincianID" });
            DropIndex("dbo.LPDeptARincians", new[] { "LPDeptAID" });
            DropIndex("dbo.JPDeptASOes", new[] { "SalesOrderRincianID" });
            DropIndex("dbo.JPDeptASOes", new[] { "JPDeptARincianID" });
            DropIndex("dbo.JPDeptARincians", new[] { "JPDeptAID" });
            DropIndex("dbo.OrderBahanBakus", new[] { "SalesOrderRincian_SalesOrderRincianID" });
            DropIndex("dbo.OrderBahanBakus", new[] { "JPDeptARincianID" });
            AlterColumn("dbo.OrderBahanBakus", "SalesOrderRincian_SalesOrderRincianID", c => c.Int(nullable: false));
            DropColumn("dbo.OrderBahanBakus", "JPDeptARincianID");
            DropTable("dbo.LPDeptAs");
            DropTable("dbo.LPDeptBs");
            DropTable("dbo.LPDeptBRincians");
            DropTable("dbo.JPDeptBs");
            DropTable("dbo.JPDeptBRincians");
            DropTable("dbo.LPDeptARincians");
            DropTable("dbo.JPDeptASOes");
            DropTable("dbo.JPDeptAs");
            DropTable("dbo.JPDeptARincians");
            RenameColumn(table: "dbo.OrderBahanBakus", name: "SalesOrderRincian_SalesOrderRincianID", newName: "SalesOrderRincianID");
            CreateIndex("dbo.OrderBahanBakus", "SalesOrderRincianID");
            AddForeignKey("dbo.OrderBahanBakus", "SalesOrderRincianID", "dbo.SalesOrderRincians", "SalesOrderRincianID", cascadeDelete: true);
        }
    }
}
