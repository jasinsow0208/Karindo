namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderProduksi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderBahanBakus",
                c => new
                    {
                        OrderBahanBakuID = c.Int(nullable: false, identity: true),
                        ResepID = c.Int(nullable: false),
                        SalesOrderRincianID = c.Int(nullable: false),
                        JmlResep = c.Decimal(nullable: false, precision: 20, scale: 5),
                        Keterangan = c.String(),
                        Posting = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderBahanBakuID)
                .ForeignKey("dbo.Reseps", t => t.ResepID, cascadeDelete: false)
                .ForeignKey("dbo.SalesOrderRincians", t => t.SalesOrderRincianID, cascadeDelete: false)
                .Index(t => t.ResepID)
                .Index(t => t.SalesOrderRincianID);
            
            AddColumn("dbo.PindahGudangs", "Source", c => c.String());
            AddColumn("dbo.PindahGudangs", "SourceID", c => c.Int(nullable: false));
            CreateIndex("dbo.PindahGudangRincians", "GudangBahanBakuID");
            AddForeignKey("dbo.PindahGudangRincians", "GudangBahanBakuID", "dbo.GudangBahanBakus", "GudangBahanBakuID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderBahanBakus", "SalesOrderRincianID", "dbo.SalesOrderRincians");
            DropForeignKey("dbo.OrderBahanBakus", "ResepID", "dbo.Reseps");
            DropForeignKey("dbo.PindahGudangRincians", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropIndex("dbo.OrderBahanBakus", new[] { "SalesOrderRincianID" });
            DropIndex("dbo.OrderBahanBakus", new[] { "ResepID" });
            DropIndex("dbo.PindahGudangRincians", new[] { "GudangBahanBakuID" });
            DropColumn("dbo.PindahGudangs", "SourceID");
            DropColumn("dbo.PindahGudangs", "Source");
            DropTable("dbo.OrderBahanBakus");
        }
    }
}
