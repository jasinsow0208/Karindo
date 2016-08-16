namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JadualProduksi1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JPDeptARincians", "Posting", c => c.Boolean(nullable: false));
            AddColumn("dbo.JPDeptAs", "Posting", c => c.Boolean(nullable: false));
            AddColumn("dbo.JPDeptBs", "Posting", c => c.Boolean(nullable: false));
            AddColumn("dbo.LPDeptBs", "Posting", c => c.Boolean(nullable: false));
            AddColumn("dbo.LPDeptAs", "Posting", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LPDeptAs", "Posting");
            DropColumn("dbo.LPDeptBs", "Posting");
            DropColumn("dbo.JPDeptBs", "Posting");
            DropColumn("dbo.JPDeptAs", "Posting");
            DropColumn("dbo.JPDeptARincians", "Posting");
        }
    }
}
