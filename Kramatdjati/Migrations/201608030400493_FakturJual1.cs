namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FakturJual1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SuratJalanCetaks", "StatusFaktur", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SuratJalanCetaks", "StatusFaktur");
        }
    }
}
