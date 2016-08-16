namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoLot1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KartuStoks", "NoLot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KartuStoks", "NoLot");
        }
    }
}
