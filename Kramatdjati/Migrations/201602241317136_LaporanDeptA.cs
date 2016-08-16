namespace Kramatdjati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LaporanDeptA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LPDeptARincians", "TglProduksi", c => c.DateTime(nullable: false));
            AddColumn("dbo.LPDeptARincians", "Rajangan", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.BahanBakus", "Stok", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.BahanBakus", "HargaRata2", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.BahanBakus", "HargaTerakhir", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.BahanBakus", "HargaJual", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.GudangBahanBakus", "Jumlah", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.PindahGudangRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.KasBanks", "Saldo", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.Contacts", "KreditLimit", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.PemesananBarangRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.PemesananBarangRincians", "HargaSatuan", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.PemesananBarangRincians", "JmlYangSudahDiterima", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.PenerimaanBarangRincians", "JumlahDiTerima", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccOpening", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod1", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod2", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod3", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod4", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod5", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod6", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod7", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod8", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod9", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod10", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod11", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod12", c => c.Decimal(precision: 20, scale: 5));
            AlterColumn("dbo.tblGLBatchDetails", "Debet", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.tblGLBatchDetails", "Kredit", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.SalesOrderRincians", "HargaJual", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.SalesOrderRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.SalesOrderRincians", "JmlDiproduksi", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.SalesOrderRincians", "JmlYangSudahDiKirim", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.SuratJalanRincians", "JumlahDikirim", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.LPDeptARincians", "SisaCompound", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.ResepRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.StokOpnameRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.StokOpnameRincians", "HargaSatuan", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.KartuStoks", "Masuk", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.KartuStoks", "Keluar", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.KartuStoks", "Saldo", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.JenisKemasans", "Berat", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.JenisDetails", "Berat", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.tblGLOpeningBalanceDetails", "Debet", c => c.Decimal(nullable: false, precision: 20, scale: 5));
            AlterColumn("dbo.tblGLOpeningBalanceDetails", "Kredit", c => c.Decimal(nullable: false, precision: 20, scale: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblGLOpeningBalanceDetails", "Kredit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tblGLOpeningBalanceDetails", "Debet", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.JenisDetails", "Berat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.JenisKemasans", "Berat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.KartuStoks", "Saldo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.KartuStoks", "Keluar", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.KartuStoks", "Masuk", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StokOpnameRincians", "HargaSatuan", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StokOpnameRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResepRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.LPDeptARincians", "SisaCompound", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SuratJalanRincians", "JumlahDikirim", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SalesOrderRincians", "JmlYangSudahDiKirim", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SalesOrderRincians", "JmlDiproduksi", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SalesOrderRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SalesOrderRincians", "HargaJual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tblGLBatchDetails", "Kredit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tblGLBatchDetails", "Debet", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod12", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod11", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod10", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod9", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod8", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod7", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod6", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod5", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod4", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod3", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod2", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccPeriod1", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblGLAccPeriods", "AccOpening", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PenerimaanBarangRincians", "JumlahDiTerima", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PemesananBarangRincians", "JmlYangSudahDiterima", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PemesananBarangRincians", "HargaSatuan", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PemesananBarangRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Contacts", "KreditLimit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.KasBanks", "Saldo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PindahGudangRincians", "Jumlah", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.GudangBahanBakus", "Jumlah", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BahanBakus", "HargaJual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BahanBakus", "HargaTerakhir", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BahanBakus", "HargaRata2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BahanBakus", "Stok", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.LPDeptARincians", "Rajangan");
            DropColumn("dbo.LPDeptARincians", "TglProduksi");
        }
    }
}
