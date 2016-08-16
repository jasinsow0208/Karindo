namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Penimbangan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PenimbanganProduksis", "Keterangan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PenimbanganProduksis", "Keterangan");
        }
    }
}
