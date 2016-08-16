namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hargajualpercustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HargaJualPerCustomers",
                c => new
                    {
                        HargaJualPerCustomerID = c.Int(nullable: false, identity: true),
                        ContactID = c.Int(nullable: false),
                        TglBerlaku = c.DateTime(nullable: false),
                        JenisSpon = c.String(),
                        Harga = c.Decimal(nullable: false, precision: 20, scale: 5),
                        mm = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HargaJualPerCustomerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HargaJualPerCustomers");
        }
    }
}
