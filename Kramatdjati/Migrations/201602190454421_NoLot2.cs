namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoLot2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PindahGudangs", "NoLot", c => c.String());
            AddColumn("dbo.PindahGudangs", "KeluarPosting", c => c.Boolean(nullable: false));
            AddColumn("dbo.PindahGudangs", "KeluarTglPosting", c => c.DateTime(nullable: false));
            AddColumn("dbo.PindahGudangs", "MasukPosting", c => c.Boolean(nullable: false));
            AddColumn("dbo.PindahGudangs", "MasukTglPosting", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PindahGudangs", "MasukTglPosting");
            DropColumn("dbo.PindahGudangs", "MasukPosting");
            DropColumn("dbo.PindahGudangs", "KeluarTglPosting");
            DropColumn("dbo.PindahGudangs", "KeluarPosting");
            DropColumn("dbo.PindahGudangs", "NoLot");
        }
    }
}
