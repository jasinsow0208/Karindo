namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderBahanBaku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderBahanBakus", "GudangID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderBahanBakus", "GudangID");
            AddForeignKey("dbo.OrderBahanBakus", "GudangID", "dbo.Gudangs", "GudangID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderBahanBakus", "GudangID", "dbo.Gudangs");
            DropIndex("dbo.OrderBahanBakus", new[] { "GudangID" });
            DropColumn("dbo.OrderBahanBakus", "GudangID");
        }
    }
}
