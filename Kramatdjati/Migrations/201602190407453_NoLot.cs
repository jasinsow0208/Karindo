namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoLot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JPDeptARincians", "NoLot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JPDeptARincians", "NoLot");
        }
    }
}
