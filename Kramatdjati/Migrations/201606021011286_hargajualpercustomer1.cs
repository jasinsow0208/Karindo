namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hargajualpercustomer1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.HargaJualPerCustomers", "ContactID");
            AddForeignKey("dbo.HargaJualPerCustomers", "ContactID", "dbo.Contacts", "ContactID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HargaJualPerCustomers", "ContactID", "dbo.Contacts");
            DropIndex("dbo.HargaJualPerCustomers", new[] { "ContactID" });
        }
    }
}
