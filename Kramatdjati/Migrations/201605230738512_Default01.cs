namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Default01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblDefaults", "NoSuratJalanPPN", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblDefaults", "NoSuratJalanPPN");
        }
    }
}
