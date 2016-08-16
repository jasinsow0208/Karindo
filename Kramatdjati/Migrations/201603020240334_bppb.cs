namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bppb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblDefaults", "NoBPPB", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblDefaults", "NoBPPB");
        }
    }
}
