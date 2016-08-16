namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LaporanProduksi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LPDeptAs", "PenimbanganAwal", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "PenimbanganAkhir", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "KneaderAwal", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "KneaderAkhir", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "BoilerAwal", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "BoilerAkhir", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "RollAwal", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "RollAkhir", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "HotPressBAwal", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "HotPressBAkhir", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "HotPressKAwal", c => c.DateTime());
            AlterColumn("dbo.LPDeptAs", "HotPressKAkhir", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LPDeptAs", "HotPressKAkhir", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "HotPressKAwal", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "HotPressBAkhir", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "HotPressBAwal", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "RollAkhir", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "RollAwal", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "BoilerAkhir", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "BoilerAwal", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "KneaderAkhir", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "KneaderAwal", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "PenimbanganAkhir", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LPDeptAs", "PenimbanganAwal", c => c.DateTime(nullable: false));
        }
    }
}
