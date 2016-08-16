namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JadualDeptB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JPDeptBs", "TglProduksiDeptB", c => c.DateTime(nullable: false));
            AddColumn("dbo.JPDeptBs", "TglProduksiDeptA", c => c.DateTime(nullable: false));
            DropColumn("dbo.JPDeptBs", "TglProduksi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JPDeptBs", "TglProduksi", c => c.DateTime(nullable: false));
            DropColumn("dbo.JPDeptBs", "TglProduksiDeptA");
            DropColumn("dbo.JPDeptBs", "TglProduksiDeptB");
        }
    }
}
