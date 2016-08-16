namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SuratJalanUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SuratJalans", "SalesOrderID", "dbo.SalesOrders");
            DropIndex("dbo.SuratJalans", new[] { "SalesOrderID" });
            RenameColumn(table: "dbo.SuratJalans", name: "SalesOrderID", newName: "SalesOrder_SalesOrderID");
            AddColumn("dbo.SuratJalans", "ContactID", c => c.Int(nullable: false));
            AlterColumn("dbo.SuratJalans", "SalesOrder_SalesOrderID", c => c.Int());
            CreateIndex("dbo.SuratJalans", "ContactID");
            CreateIndex("dbo.SuratJalans", "SalesOrder_SalesOrderID");
            AddForeignKey("dbo.SuratJalans", "ContactID", "dbo.Contacts", "ContactID", cascadeDelete: false);
            AddForeignKey("dbo.SuratJalans", "SalesOrder_SalesOrderID", "dbo.SalesOrders", "SalesOrderID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuratJalans", "SalesOrder_SalesOrderID", "dbo.SalesOrders");
            DropForeignKey("dbo.SuratJalans", "ContactID", "dbo.Contacts");
            DropIndex("dbo.SuratJalans", new[] { "SalesOrder_SalesOrderID" });
            DropIndex("dbo.SuratJalans", new[] { "ContactID" });
            AlterColumn("dbo.SuratJalans", "SalesOrder_SalesOrderID", c => c.Int(nullable: false));
            DropColumn("dbo.SuratJalans", "ContactID");
            RenameColumn(table: "dbo.SuratJalans", name: "SalesOrder_SalesOrderID", newName: "SalesOrderID");
            CreateIndex("dbo.SuratJalans", "SalesOrderID");
            AddForeignKey("dbo.SuratJalans", "SalesOrderID", "dbo.SalesOrders", "SalesOrderID", cascadeDelete: true);
        }
    }
}
