namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JenisKemasan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JenisKemasans", "Default", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JenisKemasans", "Default");
        }
    }
}
