namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompoundGudangBahanBaku : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JPDeptACompounds", "BahanBakuID", "dbo.BahanBakus");
            DropIndex("dbo.JPDeptACompounds", new[] { "BahanBakuID" });
            AddColumn("dbo.JPDeptACompounds", "GudangBahanBakuID", c => c.Int(nullable: false));
            CreateIndex("dbo.JPDeptACompounds", "GudangBahanBakuID");
            AddForeignKey("dbo.JPDeptACompounds", "GudangBahanBakuID", "dbo.GudangBahanBakus", "GudangBahanBakuID", cascadeDelete: true);
            DropColumn("dbo.JPDeptACompounds", "BahanBakuID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JPDeptACompounds", "BahanBakuID", c => c.Int(nullable: false));
            DropForeignKey("dbo.JPDeptACompounds", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropIndex("dbo.JPDeptACompounds", new[] { "GudangBahanBakuID" });
            DropColumn("dbo.JPDeptACompounds", "GudangBahanBakuID");
            CreateIndex("dbo.JPDeptACompounds", "BahanBakuID");
            AddForeignKey("dbo.JPDeptACompounds", "BahanBakuID", "dbo.BahanBakus", "BahanBakuID", cascadeDelete: true);
        }
    }
}
