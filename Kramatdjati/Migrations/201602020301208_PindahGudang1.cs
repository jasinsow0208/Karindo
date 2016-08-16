namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PindahGudang1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PindahGudangs", "Keterangan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PindahGudangs", "Keterangan");
        }
    }
}
