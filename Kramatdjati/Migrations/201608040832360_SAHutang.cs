namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SAHutang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SAHutangs",
                c => new
                    {
                        SAHutangID = c.Int(nullable: false, identity: true),
                        ContactID = c.Int(nullable: false),
                        TglInvoice = c.DateTime(nullable: false),
                        NoInvoice = c.String(),
                        TglJatuhTempo = c.DateTime(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                        TglBayar = c.DateTime(nullable: false),
                        StatusBayar = c.Boolean(nullable: false),
                        Posting = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SAHutangID)
                .ForeignKey("dbo.Contacts", t => t.ContactID, cascadeDelete: true)
                .Index(t => t.ContactID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SAHutangs", "ContactID", "dbo.Contacts");
            DropIndex("dbo.SAHutangs", new[] { "ContactID" });
            DropTable("dbo.SAHutangs");
        }
    }
}
