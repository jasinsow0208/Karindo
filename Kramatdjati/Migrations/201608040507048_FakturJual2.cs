namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FakturJual2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FakturJuals", "TglPesan", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FakturJuals", "TglPesan");
        }
    }
}
