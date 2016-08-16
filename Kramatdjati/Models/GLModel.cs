using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Kramatdjati.Models
{

    public class tblGLAccount
    {
        public int tblGLAccountId { get; set; }

        [Display(Name = "Acc Code")]
        [Required]
        public string AccCode { get; set; }

        [Display(Name = "Acc Name")]
        [Required]
        public string AccName { get; set; }

        [Display(Name = "Acc Description")]
        public string AccDescription { get; set; }

        [Display(Name = "Acc Type")]
        [Required]
        public int tblGLAccountTypeId { get; set; }

        [Display(Name = "Retained Earning")]
        public bool RetainedEarnings { get; set; }

        [Display(Name = "Deactivated Account")]
        public bool Active { get; set; }

        [Display(Name = "Bank Account")]
        public bool AccKas { get; set; }

        public tblGLAccount()
        {
            Active = false;
        }

        public virtual tblGLAccountType tblGLAccountType { get; set; }

        public virtual List<Contact> NoRekPiutangs { get; set; }
        public virtual List<Contact> NoRekHutangs { get; set; }

        public virtual List<KasBank> KasBanks { get; set; }

        public virtual List<BahanBaku> NoRekCOGSs { get; set; }
        public virtual List<BahanBaku> NoRekSales { get; set; }
        public virtual List<BahanBaku> NoRekInventories { get; set; }
        public virtual List<tblGLAccPeriod> tblGLAccPeriods { get; set; }
        public virtual List<tblGLBatchDetail> tblGLBatchDetails { get; set; }

        public virtual List<tblDefault> AccHutangBelumFakturs { get; set; }
        public virtual List<tblDefault> AccPPNMasukans { get; set; }
        public virtual List<tblDefault> AccUangMukaPembelians { get; set; }
        public virtual List<tblDefault> AccSelisihKurss { get; set; }
        public virtual List<tblDefault> AccPiutangBelumFakturs { get; set; }
        public virtual List<tblDefault> AccPPNKeluarans { get; set; }
        public virtual List<tblDefault> AccUangMukaPenjualans { get; set; }
    }

    public class tblGLAccountType
    {
        public int tblGLAccountTypeId { get; set; }
        public string Type { get; set; }

        public virtual List<tblGLAccount> tblGLAccounts { get; set; }
    }

    public class tblDefault
    {
        public int tblDefaultId { get; set; }
        [Display(Name="Perioda Berjalan")]
        public int CurrentPeriod { get; set; }

        [Display(Name="Tahun Berjalan")]
        public int CurrentYear { get; set; }

        [Display(Name="Perioda Awal")]
        public int OpeningPeriod { get; set; }

        [Display(Name="Tahun Awal")]
        public int OpeningYear { get; set; }

        [Display(Name = "Account hutang yang belum difakturkan")]
        public int AccHutangBelumFakturID { get; set; }

        [Display(Name = "Account PPN Masukan")]
        public int AccPPNMasukanID { get; set; }

        [Display(Name = "Account uang muka pembelian")]
        public int AccUangMukaPembelianID { get; set; }

        [Display(Name = "Account Selisih Kurs")]
        public int AccSelisihKursID { get; set; }

        [Display(Name = "Account piutang yang belum difakturkan")]
        public int AccPiutangBelumFakturID { get; set; }

        [Display(Name="Account PPN Keluaran")]
        public int AccPPNKeluaranID { get; set; }

        [Display(Name="Account Uang Muka Penjualan")]
        public int AccUangMukaPenjualanID { get; set; }

        [Display(Name="No. PO")]
        public int NoPO { get; set; }

        [Display(Name="No. SO")]
        public int NoSO { get; set; }

        [Display(Name="Surat Jalan")]
        public int NoSuratJalan { get; set; }

        [Display(Name = "S Jalan PPN")]
        public int NoSuratJalanPPN { get; set; }

        [Display(Name = "No. BPPB")]
        public int NoBPPB { get; set; }

        [Display(Name = "Gudang Pembelian Bahan Baku")]
        public int GudangBeliID { get; set; }

        [Display(Name = "Gudang barang Jadi")]
        public int GudangJualID { get; set; }

        [Display(Name = "Gudang Produksi")]
        public int GudangProduksiID { get; set; }

        [Display(Name="Jenis Persediaan Rajangan")]
        public int JenisPersediaanRajanganID { get; set; }

        [Display(Name="Jns Prsdiaan Compound")]
        public int JenisPersediaanCompoundID { get; set; }

        [Display(Name="Jns Prsdiaan Barang Jadi")]
        public int JenisPersediaanBarangJadiID { get; set; }

        [Display(Name="Jns Prsdiaan Bahan Baku")]
        public int JenisPersediaanBahanBakuID { get; set; }

        public virtual tblGLAccount AccHutangBelumFaktur { get; set; }
        public virtual tblGLAccount AccPPNMasukan { get; set; }
        public virtual tblGLAccount AccUangMukaPembelian { get; set; } 
        public virtual tblGLAccount AccSelisihKurs { get; set; }
        public virtual tblGLAccount AccPiutangBelumFaktur { get; set; }
        public virtual tblGLAccount AccPPNKeluaran { get; set; }
        public virtual tblGLAccount AccUangMukaPenjualan { get; set; }
        public virtual Gudang gudangBeli { get; set; }
        public virtual Gudang gudangJual { get; set; }
        public virtual Gudang gudangProduksi { get; set; }
        public virtual JenisPersediaan compound { get; set; }
        public virtual JenisPersediaan rajangan { get; set; }
        public virtual JenisPersediaan barangJadi { get; set; }
        public virtual JenisPersediaan bahanBaku { get; set; }

    }

    public class tblGLGnlJnl
    {
        public int tblGLGnlJnlID { get; set; }
        public string NoBatch { get; set; }
        public string Keterangan { get; set; }
        public DateTime BatchDate { get; set; }
        public int FiscalYear { get; set; }
        public int Period { get; set; }
        public Boolean Posting { get; set; }
    }

    public class tblGLAccPeriod
    {
        public int tblGLAccPeriodId { get; set; }
        public int AccYear { get; set; }
        public int tblGLAccountId { get; set; }

        //Kalau positif debet, negatif kredit
        //Opening berisi saldo untuk setiap awal tahun
        public decimal AccOpening { get; set; }

        //Akumulasi perbulan
        public decimal? AccPeriod1 { get; set; }
        public decimal? AccPeriod2 { get; set; }
        public decimal? AccPeriod3 { get; set; }
        public decimal? AccPeriod4 { get; set; }
        public decimal? AccPeriod5 { get; set; }
        public decimal? AccPeriod6 { get; set; }
        public decimal? AccPeriod7 { get; set; }
        public decimal? AccPeriod8 { get; set; }
        public decimal? AccPeriod9 { get; set; }
        public decimal? AccPeriod10 { get; set; }
        public decimal? AccPeriod11 { get; set; }
        public decimal? AccPeriod12 { get; set; }

        public virtual tblGLAccount tblGLAccount { get; set; }
    }

    public class tblGLOpeningBalance
    {
        public int tblGLOpeningBalanceId { get; set; }
        public int AccPeriod { get; set; }
        public int AccYear { get; set; }
        public bool Posting { get; set; }

        public virtual List<tblGLOpeningBalanceDetail> tblGLOpeningBalanceDetails { get; set; }
    }

    public class tblGLOpeningBalanceDetail
    {
        public int tblGLOpeningBalanceDetailId { get; set; }
        public int tblGLOpeningBalanceId { get; set; }

        [Display(Name = "No. Perkiraan")]
        public int tblGLAccountId { get; set; }

        public decimal Debet { get; set; }

        public decimal Kredit { get; set; }

        public string Keterangan { get; set; }

        public virtual tblGLAccount tblGLAccount { get; set; }
        public virtual tblGLOpeningBalance tblGLOpeningBalance { get; set; }
    }

    public class tblGLBatch
    {
        [Display(Name = "No.Batch")]
        public int tblGLBatchId { get; set; }
        [Display(Name = "Tgl Batch")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglKerja { get; set; }

        [Display(Name = "Tahun Fiskal")]
        public int AccYear { get; set; }

        [Display(Name = "Perioda")]
        public int AccPeriod { get; set; }
        public bool Posting { get; set; }
        public string User { get; set; }

        [Display(Name = "Tgl Posting")]
        public DateTime TglKomputer { get; set; }
        public string Keterangan { get; set; }

        public string Source { get; set; }
        public int? SourceID { get; set; }

        public virtual List<tblGLBatchDetail> tblGLBatchDetails { get; set; }

    }

    public class tblGLBatchDetail
    {
        public int tblGLBatchDetailId { get; set; }
        public int tblGLBatchId { get; set; }

        [Display(Name = "No. Ref")]
        public string NoRef { get; set; }

        [Display(Name = "No. Rekening")]
        public int tblGLAccountId { get; set; }

        [Display(Name = "Tgl Transaksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglTransaksi { get; set; }

        public string Keterangan { get; set; }
        public decimal Debet { get; set; }
        public decimal Kredit { get; set; }

        public virtual tblGLBatch tblGLBatch { get; set; }
        public virtual tblGLAccount tblGLAccount { get; set; }
    }
}