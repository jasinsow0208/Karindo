namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resep : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusProduksis",
                c => new
                    {
                        StatusProduksiID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.StatusProduksiID);
            
            CreateTable(
                "dbo.ResepRincians",
                c => new
                    {
                        ResepRincianID = c.Int(nullable: false, identity: true),
                        ResepID = c.Int(nullable: false),
                        BahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.ResepRincianID)
                .ForeignKey("dbo.BahanBakus", t => t.BahanBakuID, cascadeDelete: true)
                .ForeignKey("dbo.Reseps", t => t.ResepID, cascadeDelete: true)
                .Index(t => t.ResepID)
                .Index(t => t.BahanBakuID);
            
            CreateTable(
                "dbo.Reseps",
                c => new
                    {
                        ResepID = c.Int(nullable: false, identity: true),
                        KodeResep = c.String(),
                        Keterangan = c.String(),
                        TglBuat = c.DateTime(nullable: false),
                        Posting = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResepID);
            
            AddColumn("dbo.BahanBakus", "StatusProduksi_StatusProduksiID", c => c.Int());
            AddColumn("dbo.SalesOrderRincians", "JmlDiproduksi", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AddColumn("dbo.SalesOrderRincians", "StatusProduksiID", c => c.Int());
            CreateIndex("dbo.BahanBakus", "StatusProduksi_StatusProduksiID");
            CreateIndex("dbo.SalesOrderRincians", "StatusProduksiID");
            AddForeignKey("dbo.BahanBakus", "StatusProduksi_StatusProduksiID", "dbo.StatusProduksis", "StatusProduksiID");
            AddForeignKey("dbo.SalesOrderRincians", "StatusProduksiID", "dbo.StatusProduksis", "StatusProduksiID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResepRincians", "ResepID", "dbo.Reseps");
            DropForeignKey("dbo.ResepRincians", "BahanBakuID", "dbo.BahanBakus");
            DropForeignKey("dbo.SalesOrderRincians", "StatusProduksiID", "dbo.StatusProduksis");
            DropForeignKey("dbo.BahanBakus", "StatusProduksi_StatusProduksiID", "dbo.StatusProduksis");
            DropIndex("dbo.ResepRincians", new[] { "BahanBakuID" });
            DropIndex("dbo.ResepRincians", new[] { "ResepID" });
            DropIndex("dbo.SalesOrderRincians", new[] { "StatusProduksiID" });
            DropIndex("dbo.BahanBakus", new[] { "StatusProduksi_StatusProduksiID" });
            DropColumn("dbo.SalesOrderRincians", "StatusProduksiID");
            DropColumn("dbo.SalesOrderRincians", "JmlDiproduksi");
            DropColumn("dbo.BahanBakus", "StatusProduksi_StatusProduksiID");
            DropTable("dbo.Reseps");
            DropTable("dbo.ResepRincians");
            DropTable("dbo.StatusProduksis");
        }
    }
}
