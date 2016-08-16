namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _default : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblDefaults", "GudangBeliID", c => c.Int(nullable: true));
            AddColumn("dbo.tblDefaults", "GudangJualID", c => c.Int(nullable: true));
            CreateIndex("dbo.tblDefaults", "GudangBeliID");
            CreateIndex("dbo.tblDefaults", "GudangJualID");
            AddForeignKey("dbo.tblDefaults", "GudangBeliID", "dbo.Gudangs", "GudangID");
            AddForeignKey("dbo.tblDefaults", "GudangJualID", "dbo.Gudangs", "GudangID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblDefaults", "GudangJualID", "dbo.Gudangs");
            DropForeignKey("dbo.tblDefaults", "GudangBeliID", "dbo.Gudangs");
            DropIndex("dbo.tblDefaults", new[] { "GudangJualID" });
            DropIndex("dbo.tblDefaults", new[] { "GudangBeliID" });
            DropColumn("dbo.tblDefaults", "GudangJualID");
            DropColumn("dbo.tblDefaults", "GudangBeliID");
        }
    }
}
