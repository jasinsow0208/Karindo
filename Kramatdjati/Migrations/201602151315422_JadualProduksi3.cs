namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JadualProduksi3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblDefaults", "GudangProduksiID", c => c.Int(nullable: false));
            AddColumn("dbo.JPDeptARincians", "ResepID", c => c.Int(nullable: false));
            CreateIndex("dbo.tblDefaults", "GudangProduksiID");
            CreateIndex("dbo.JPDeptARincians", "ResepID");
            AddForeignKey("dbo.tblDefaults", "GudangProduksiID", "dbo.Gudangs", "GudangID");
            AddForeignKey("dbo.JPDeptARincians", "ResepID", "dbo.Reseps", "ResepID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JPDeptARincians", "ResepID", "dbo.Reseps");
            DropForeignKey("dbo.tblDefaults", "GudangProduksiID", "dbo.Gudangs");
            DropIndex("dbo.JPDeptARincians", new[] { "ResepID" });
            DropIndex("dbo.tblDefaults", new[] { "GudangProduksiID" });
            DropColumn("dbo.JPDeptARincians", "ResepID");
            DropColumn("dbo.tblDefaults", "GudangProduksiID");
        }
    }
}
