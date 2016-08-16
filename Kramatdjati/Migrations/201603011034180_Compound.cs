namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Compound : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JPDeptACompounds",
                c => new
                    {
                        JPDeptACompoundID = c.Int(nullable: false, identity: true),
                        JPDeptAID = c.Int(nullable: false),
                        BahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.JPDeptACompoundID)
                .ForeignKey("dbo.BahanBakus", t => t.BahanBakuID, cascadeDelete: true)
                .ForeignKey("dbo.JPDeptAs", t => t.JPDeptAID, cascadeDelete: true)
                .Index(t => t.JPDeptAID)
                .Index(t => t.BahanBakuID);
            
            AddColumn("dbo.JPDeptAs", "TglPenimbangan", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JPDeptACompounds", "JPDeptAID", "dbo.JPDeptAs");
            DropForeignKey("dbo.JPDeptACompounds", "BahanBakuID", "dbo.BahanBakus");
            DropIndex("dbo.JPDeptACompounds", new[] { "BahanBakuID" });
            DropIndex("dbo.JPDeptACompounds", new[] { "JPDeptAID" });
            DropColumn("dbo.JPDeptAs", "TglPenimbangan");
            DropTable("dbo.JPDeptACompounds");
        }
    }
}
