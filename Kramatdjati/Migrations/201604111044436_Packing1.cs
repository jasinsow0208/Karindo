namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Packing1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packings", "Size", c => c.String());
            DropColumn("dbo.Packings", "Jumlah");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packings", "Jumlah", c => c.String());
            DropColumn("dbo.Packings", "Size");
        }
    }
}
