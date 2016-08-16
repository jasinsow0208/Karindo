namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesOrders", "NoPO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesOrders", "NoPO");
        }
    }
}
