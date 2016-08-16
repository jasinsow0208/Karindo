namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contact1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Kredit", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Kredit");
        }
    }
}
