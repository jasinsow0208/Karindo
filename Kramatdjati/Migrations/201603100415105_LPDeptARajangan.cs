namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LPDeptARajangan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LPDeptARajangans",
                c => new
                    {
                        LPDeptARajanganID = c.Int(nullable: false, identity: true),
                        LPDeptAID = c.Int(nullable: false),
                        GudangBahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.LPDeptARajanganID)
                .ForeignKey("dbo.GudangBahanBakus", t => t.GudangBahanBakuID, cascadeDelete: true)
                .ForeignKey("dbo.LPDeptAs", t => t.LPDeptAID, cascadeDelete: true)
                .Index(t => t.LPDeptAID)
                .Index(t => t.GudangBahanBakuID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LPDeptARajangans", "LPDeptAID", "dbo.LPDeptAs");
            DropForeignKey("dbo.LPDeptARajangans", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropIndex("dbo.LPDeptARajangans", new[] { "GudangBahanBakuID" });
            DropIndex("dbo.LPDeptARajangans", new[] { "LPDeptAID" });
            DropTable("dbo.LPDeptARajangans");
        }
    }
}
