namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tabelDefault : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblDefaults", "JenisPersediaanBarangJadiID", c => c.Int(nullable: false));
            AddColumn("dbo.tblDefaults", "JenisPersediaanBahanBakuID", c => c.Int(nullable: false));
            CreateIndex("dbo.tblDefaults", "JenisPersediaanBarangJadiID");
            CreateIndex("dbo.tblDefaults", "JenisPersediaanBahanBakuID");
            AddForeignKey("dbo.tblDefaults", "JenisPersediaanBahanBakuID", "dbo.JenisPersediaans", "JenisPersediaanID");
            AddForeignKey("dbo.tblDefaults", "JenisPersediaanBarangJadiID", "dbo.JenisPersediaans", "JenisPersediaanID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblDefaults", "JenisPersediaanBarangJadiID", "dbo.JenisPersediaans");
            DropForeignKey("dbo.tblDefaults", "JenisPersediaanBahanBakuID", "dbo.JenisPersediaans");
            DropIndex("dbo.tblDefaults", new[] { "JenisPersediaanBahanBakuID" });
            DropIndex("dbo.tblDefaults", new[] { "JenisPersediaanBarangJadiID" });
            DropColumn("dbo.tblDefaults", "JenisPersediaanBahanBakuID");
            DropColumn("dbo.tblDefaults", "JenisPersediaanBarangJadiID");
        }
    }
}
