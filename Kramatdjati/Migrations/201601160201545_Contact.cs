namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "NamaFaktur1", c => c.String());
            AddColumn("dbo.Contacts", "AlamatFaktur1", c => c.String());
            AddColumn("dbo.Contacts", "KotaFaktur1", c => c.String());
            AddColumn("dbo.Contacts", "NPWPFaktur1", c => c.String());
            AddColumn("dbo.Contacts", "NamaFaktur2", c => c.String());
            AddColumn("dbo.Contacts", "AlamatFaktur2", c => c.String());
            AddColumn("dbo.Contacts", "KotaFaktur2", c => c.String());
            AddColumn("dbo.Contacts", "NPWPFaktur2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "NPWPFaktur2");
            DropColumn("dbo.Contacts", "KotaFaktur2");
            DropColumn("dbo.Contacts", "AlamatFaktur2");
            DropColumn("dbo.Contacts", "NamaFaktur2");
            DropColumn("dbo.Contacts", "NPWPFaktur1");
            DropColumn("dbo.Contacts", "KotaFaktur1");
            DropColumn("dbo.Contacts", "AlamatFaktur1");
            DropColumn("dbo.Contacts", "NamaFaktur1");
        }
    }
}
