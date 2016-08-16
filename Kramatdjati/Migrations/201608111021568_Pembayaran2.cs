namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pembayaran2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PembayaranSOes", "Posting", c => c.Boolean(nullable: false));
            CreateIndex("dbo.PembayaranSOes", "ContactID");
            AddForeignKey("dbo.PembayaranSOes", "ContactID", "dbo.Contacts", "ContactID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PembayaranSOes", "ContactID", "dbo.Contacts");
            DropIndex("dbo.PembayaranSOes", new[] { "ContactID" });
            DropColumn("dbo.PembayaranSOes", "Posting");
        }
    }
}
