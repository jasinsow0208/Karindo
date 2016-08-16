namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PenimbanganProduksi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PenimbanganProduksis",
                c => new
                    {
                        PenimbanganProduksiID = c.Int(nullable: false, identity: true),
                        JPDeptARincianID = c.Int(nullable: false),
                        GudangBahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                        Posting = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PenimbanganProduksiID)
                .ForeignKey("dbo.GudangBahanBakus", t => t.GudangBahanBakuID, cascadeDelete: true)
                .ForeignKey("dbo.JPDeptARincians", t => t.JPDeptARincianID, cascadeDelete: true)
                .Index(t => t.JPDeptARincianID)
                .Index(t => t.GudangBahanBakuID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PenimbanganProduksis", "JPDeptARincianID", "dbo.JPDeptARincians");
            DropForeignKey("dbo.PenimbanganProduksis", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropIndex("dbo.PenimbanganProduksis", new[] { "GudangBahanBakuID" });
            DropIndex("dbo.PenimbanganProduksis", new[] { "JPDeptARincianID" });
            DropTable("dbo.PenimbanganProduksis");
        }
    }
}
