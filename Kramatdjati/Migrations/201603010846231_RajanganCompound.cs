namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RajanganCompound : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblDefaults", "JenisPersediaanRajanganID", c => c.Int(nullable: false));
            AddColumn("dbo.tblDefaults", "JenisPersediaanCompoundID", c => c.Int(nullable: false));
            CreateIndex("dbo.tblDefaults", "JenisPersediaanRajanganID");
            CreateIndex("dbo.tblDefaults", "JenisPersediaanCompoundID");
            AddForeignKey("dbo.tblDefaults", "JenisPersediaanCompoundID", "dbo.JenisPersediaans", "JenisPersediaanID");
            AddForeignKey("dbo.tblDefaults", "JenisPersediaanRajanganID", "dbo.JenisPersediaans", "JenisPersediaanID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblDefaults", "JenisPersediaanRajanganID", "dbo.JenisPersediaans");
            DropForeignKey("dbo.tblDefaults", "JenisPersediaanCompoundID", "dbo.JenisPersediaans");
            DropIndex("dbo.tblDefaults", new[] { "JenisPersediaanCompoundID" });
            DropIndex("dbo.tblDefaults", new[] { "JenisPersediaanRajanganID" });
            DropColumn("dbo.tblDefaults", "JenisPersediaanCompoundID");
            DropColumn("dbo.tblDefaults", "JenisPersediaanRajanganID");
        }
    }
}
