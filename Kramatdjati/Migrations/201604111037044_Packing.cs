namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Packing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Packings",
                c => new
                    {
                        PackingID = c.Int(nullable: false, identity: true),
                        Jumlah = c.String(),
                        JmlPerPacking = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PackingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Packings");
        }
    }
}
