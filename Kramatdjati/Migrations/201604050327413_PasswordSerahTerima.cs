namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PasswordSerahTerima : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PasswordSerahTerimas",
                c => new
                    {
                        PasswordSerahTerimaID = c.Int(nullable: false, identity: true),
                        Operator = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.PasswordSerahTerimaID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PasswordSerahTerimas");
        }
    }
}
