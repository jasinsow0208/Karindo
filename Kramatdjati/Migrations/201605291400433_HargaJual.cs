namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HargaJual : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HargaJuals",
                c => new
                    {
                        HargaJualID = c.Int(nullable: false, identity: true),
                        TglBerlaku = c.DateTime(nullable: false),
                        JenisSpon = c.String(),
                        Harga = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.HargaJualID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HargaJuals");
        }
    }
}
