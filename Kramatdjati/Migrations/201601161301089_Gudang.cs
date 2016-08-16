namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gudang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GudangBahanBakus",
                c => new
                    {
                        GudangBahanBakuID = c.Int(nullable: false, identity: true),
                        GudangID = c.Int(nullable: false),
                        BahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.GudangBahanBakuID)
                .ForeignKey("dbo.BahanBakus", t => t.BahanBakuID, cascadeDelete: true)
                .ForeignKey("dbo.Gudangs", t => t.GudangID, cascadeDelete: true)
                .Index(t => t.GudangID)
                .Index(t => t.BahanBakuID);
            
            CreateTable(
                "dbo.Gudangs",
                c => new
                    {
                        GudangID = c.Int(nullable: false, identity: true),
                        Lokasi = c.String(),
                    })
                .PrimaryKey(t => t.GudangID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GudangBahanBakus", "GudangID", "dbo.Gudangs");
            DropForeignKey("dbo.GudangBahanBakus", "BahanBakuID", "dbo.BahanBakus");
            DropIndex("dbo.GudangBahanBakus", new[] { "BahanBakuID" });
            DropIndex("dbo.GudangBahanBakus", new[] { "GudangID" });
            DropTable("dbo.Gudangs");
            DropTable("dbo.GudangBahanBakus");
        }
    }
}
