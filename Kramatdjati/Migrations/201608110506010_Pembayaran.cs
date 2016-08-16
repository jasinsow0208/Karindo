namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pembayaran : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PembayaranSODetails",
                c => new
                    {
                        PembayaranSODetailID = c.Int(nullable: false, identity: true),
                        PembayaranSOID = c.Int(nullable: false),
                        FakturJualID = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                        Posting = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PembayaranSODetailID)
                .ForeignKey("dbo.FakturJuals", t => t.FakturJualID, cascadeDelete: true)
                .ForeignKey("dbo.PembayaranSOes", t => t.PembayaranSOID, cascadeDelete: true)
                .Index(t => t.PembayaranSOID)
                .Index(t => t.FakturJualID);
            
            CreateTable(
                "dbo.PembayaranSOes",
                c => new
                    {
                        PembayaranSOID = c.Int(nullable: false, identity: true),
                        TglBayar = c.DateTime(nullable: false),
                        ContactID = c.Int(nullable: false),
                        NoKwitansi = c.String(),
                        tblGLAccountId = c.Int(nullable: false),
                        Jumlah = c.Decimal(nullable: false, precision: 20, scale: 5),
                    })
                .PrimaryKey(t => t.PembayaranSOID)
                .ForeignKey("dbo.tblGLAccounts", t => t.tblGLAccountId, cascadeDelete: true)
                .Index(t => t.tblGLAccountId);
            
            AddColumn("dbo.FakturJuals", "Nama", c => c.String());
            AddColumn("dbo.FakturJuals", "Alamat", c => c.String());
            AddColumn("dbo.FakturJuals", "Kota", c => c.String());
            AddColumn("dbo.FakturJuals", "NPWP", c => c.String());
            AddColumn("dbo.FakturJuals", "Diskon", c => c.Decimal(nullable: false, precision: 20, scale: 5));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PembayaranSODetails", "PembayaranSOID", "dbo.PembayaranSOes");
            DropForeignKey("dbo.PembayaranSOes", "tblGLAccountId", "dbo.tblGLAccounts");
            DropForeignKey("dbo.PembayaranSODetails", "FakturJualID", "dbo.FakturJuals");
            DropIndex("dbo.PembayaranSOes", new[] { "tblGLAccountId" });
            DropIndex("dbo.PembayaranSODetails", new[] { "FakturJualID" });
            DropIndex("dbo.PembayaranSODetails", new[] { "PembayaranSOID" });
            DropColumn("dbo.FakturJuals", "Diskon");
            DropColumn("dbo.FakturJuals", "NPWP");
            DropColumn("dbo.FakturJuals", "Kota");
            DropColumn("dbo.FakturJuals", "Alamat");
            DropColumn("dbo.FakturJuals", "Nama");
            DropTable("dbo.PembayaranSOes");
            DropTable("dbo.PembayaranSODetails");
        }
    }
}
