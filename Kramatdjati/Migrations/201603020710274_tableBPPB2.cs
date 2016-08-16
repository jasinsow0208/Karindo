namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableBPPB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BPPBs", "Posting", c => c.Boolean(nullable: false));
            AddColumn("dbo.BPPBs", "JPDeptAID", c => c.Int(nullable: false));
            CreateIndex("dbo.BPPBs", "JPDeptAID");
            AddForeignKey("dbo.BPPBs", "JPDeptAID", "dbo.JPDeptAs", "JPDeptAID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BPPBs", "JPDeptAID", "dbo.JPDeptAs");
            DropIndex("dbo.BPPBs", new[] { "JPDeptAID" });
            DropColumn("dbo.BPPBs", "JPDeptAID");
            DropColumn("dbo.BPPBs", "Posting");
        }
    }
}
