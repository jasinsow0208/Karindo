namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Penimbang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JPDeptARincians", "Penimbang", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JPDeptARincians", "Penimbang");
        }
    }
}
