namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gudang2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KartuStoks", "GudangBahanBakuID", c => c.Int(nullable: false));
            CreateIndex("dbo.KartuStoks", "GudangBahanBakuID");
            //AddForeignKey("dbo.KartuStoks", "GudangBahanBakuID", "dbo.GudangBahanBakus", "GudangBahanBakuID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.KartuStoks", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropIndex("dbo.KartuStoks", new[] { "GudangBahanBakuID" });
            DropColumn("dbo.KartuStoks", "GudangBahanBakuID");
        }
    }
}
