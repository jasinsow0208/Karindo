namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Log : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SuratJalanLogs", "SuratJalanRincianID", c => c.Int(nullable: false));
            CreateIndex("dbo.SuratJalanLogs", "SuratJalanRincianID");
            AddForeignKey("dbo.SuratJalanLogs", "SuratJalanRincianID", "dbo.SuratJalanRincians", "SuratJalanRincianID", cascadeDelete: true);
            DropColumn("dbo.SuratJalanLogs", "SuratJalanID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SuratJalanLogs", "SuratJalanID", c => c.Int(nullable: false));
            DropForeignKey("dbo.SuratJalanLogs", "SuratJalanRincianID", "dbo.SuratJalanRincians");
            DropIndex("dbo.SuratJalanLogs", new[] { "SuratJalanRincianID" });
            DropColumn("dbo.SuratJalanLogs", "SuratJalanRincianID");
        }
    }
}
