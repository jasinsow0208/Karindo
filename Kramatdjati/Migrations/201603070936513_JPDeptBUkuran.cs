namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JPDeptBUkuran : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JPDeptBRincians", "UkuranTebal", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JPDeptBRincians", "UkuranTebal", c => c.Int(nullable: false));
        }
    }
}
