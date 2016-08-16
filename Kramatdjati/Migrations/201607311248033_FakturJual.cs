namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FakturJual : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FakturJuals",
                c => new
                    {
                        FakturJualID = c.Int(nullable: false, identity: true),
                        ContactID = c.Int(nullable: false),
                        TglFaktur = c.DateTime(nullable: false),
                        NoFaktur = c.String(),
                        NomorSeri = c.String(),
                        TglJatuhTempo = c.DateTime(nullable: false),
                        PPN = c.Boolean(nullable: false),
                        Posting = c.Boolean(nullable: false),
                        SuratJalanCetakID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FakturJualID)
                .ForeignKey("dbo.Contacts", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("dbo.SuratJalanCetaks", t => t.SuratJalanCetakID, cascadeDelete: true)
                .Index(t => t.ContactID)
                .Index(t => t.SuratJalanCetakID);
            
            CreateTable(
                "dbo.FakturJualRincians",
                c => new
                    {
                        FakturJualRincianID = c.Int(nullable: false, identity: true),
                        FakturJualID = c.Int(nullable: false),
                        GudangBahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Int(nullable: false),
                        Keterangan = c.String(),
                        HargaSatuan = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.FakturJualRincianID)
                .ForeignKey("dbo.FakturJuals", t => t.FakturJualID, cascadeDelete: true)
                .ForeignKey("dbo.GudangBahanBakus", t => t.GudangBahanBakuID, cascadeDelete: true)
                .Index(t => t.FakturJualID)
                .Index(t => t.GudangBahanBakuID);
            
            //AddColumn("dbo.SAPiutangs", "Posting", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FakturJuals", "SuratJalanCetakID", "dbo.SuratJalanCetaks");
            DropForeignKey("dbo.FakturJualRincians", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropForeignKey("dbo.FakturJualRincians", "FakturJualID", "dbo.FakturJuals");
            DropForeignKey("dbo.FakturJuals", "ContactID", "dbo.Contacts");
            DropIndex("dbo.FakturJualRincians", new[] { "GudangBahanBakuID" });
            DropIndex("dbo.FakturJualRincians", new[] { "FakturJualID" });
            DropIndex("dbo.FakturJuals", new[] { "SuratJalanCetakID" });
            DropIndex("dbo.FakturJuals", new[] { "ContactID" });
           // DropColumn("dbo.SAPiutangs", "Posting");
            DropTable("dbo.FakturJualRincians");
            DropTable("dbo.FakturJuals");
        }
    }
}
