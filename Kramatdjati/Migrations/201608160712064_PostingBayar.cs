namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostingBayar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PembayaranSOes", "TglJatuhTempo", c => c.DateTime(nullable: false));
            AddColumn("dbo.PembayaranSOes", "NoGiro", c => c.String());
            AddColumn("dbo.PembayaranSOes", "PostingBayar", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PembayaranSOes", "PostingBayar");
            DropColumn("dbo.PembayaranSOes", "NoGiro");
            DropColumn("dbo.PembayaranSOes", "TglJatuhTempo");
        }
    }
}
