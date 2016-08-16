namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suratJalanCetak1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuratJalanCetakRincians",
                c => new
                    {
                        SuratJalanCetakRincianID = c.Int(nullable: false, identity: true),
                        SuratJalanCetakID = c.Int(nullable: false),
                        GudangBahanBakuID = c.Int(nullable: false),
                        Jumlah = c.Int(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.SuratJalanCetakRincianID)
                .ForeignKey("dbo.GudangBahanBakus", t => t.GudangBahanBakuID, cascadeDelete: true)
                .ForeignKey("dbo.SuratJalanCetaks", t => t.SuratJalanCetakID, cascadeDelete: true)
                .Index(t => t.SuratJalanCetakID)
                .Index(t => t.GudangBahanBakuID);
            
            CreateTable(
                "dbo.SuratJalanCetaks",
                c => new
                    {
                        SuratJalanCetakID = c.Int(nullable: false, identity: true),
                        SuratJalanID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SuratJalanCetakID)
                .ForeignKey("dbo.SuratJalans", t => t.SuratJalanID, cascadeDelete: true)
                .Index(t => t.SuratJalanID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuratJalanCetakRincians", "SuratJalanCetakID", "dbo.SuratJalanCetaks");
            DropForeignKey("dbo.SuratJalanCetaks", "SuratJalanID", "dbo.SuratJalans");
            DropForeignKey("dbo.SuratJalanCetakRincians", "GudangBahanBakuID", "dbo.GudangBahanBakus");
            DropIndex("dbo.SuratJalanCetaks", new[] { "SuratJalanID" });
            DropIndex("dbo.SuratJalanCetakRincians", new[] { "GudangBahanBakuID" });
            DropIndex("dbo.SuratJalanCetakRincians", new[] { "SuratJalanCetakID" });
            DropTable("dbo.SuratJalanCetaks");
            DropTable("dbo.SuratJalanCetakRincians");
        }
    }
}
