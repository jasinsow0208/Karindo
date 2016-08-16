namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contact2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "StatusKredit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "StatusKredit");
        }
    }
}
