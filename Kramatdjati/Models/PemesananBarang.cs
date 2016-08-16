using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kramatdjati.Models
{
    public class PemesananBarang
    {
        public int PemesananBarangID { get; set; }

        [Display (Name="No. PO")]
        public string NoPemesananBarang { get; set; }

        [Display (Name="Tgl Pesan")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime  TglPesan { get; set; }

        [Display (Name ="Supplier")]
        public int ContactID { get; set; }

        [Display (Name="Tgl Kirim")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime  TglPengiriman { get; set; }

        [Display(Name="Mata Uang")]
        public int MataUangID { get; set; }

        [Display (Name="Tgl Kurs")]
        public DateTime  TglKurs { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Kurs { get; set; }

        [Display (Name="Pembuat")]
        public string User { get; set; }

        public string Catatan { get; set; }
        public Boolean Posting { get; set; }
        public Boolean Closed { get; set; }

        public virtual Contact contact { get; set; }
        public virtual List<PemesananBarangRincian> PemesananBarangRincians { get; set; }
        public virtual List<PenerimaanBarang> PenerimaanBarangs { get; set; }
        public virtual MataUang mataUang { get; set; }
    }

    public class PemesananBarangRincian
    {
        public int PemesananBarangRincianID { get; set; }
        public int PemesananBarangID { get; set; }

        [Display (Name="Kode Barang")]
        public int BahanBakuID { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }

        [Display(Name="Harga Satuan")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal  HargaSatuan { get; set; }

        [Display(Name="Jumlah diterima")]
        public decimal  JmlYangSudahDiterima { get; set; }

        [Display (Name="Status Lengkap")]
        public Boolean statusLengkap { get; set; }

        public virtual BahanBaku bahanbaku { get; set; }
        public virtual PemesananBarang pemesananBarang { get; set; }
    }

    public class PenerimaanBarang
    {
        public int PenerimaanBarangID { get; set; }

        [Display (Name="No. PO")]
        public int PemesananBarangID { get; set; }

        [Display(Name = "Gudang")]
        public int GudangID { get; set; }

        [Display (Name="No. Surat Jalan")]
        public string NoSuratJalan { get; set; }

        [Display (Name="Tgl Surat Jalan")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime tglSuratJalan { get; set; }

        [Display (Name="Tgl Terima")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglTransaksi { get; set; }

        public Boolean  Posting { get; set; }
        public DateTime  TglPosting { get; set; }

        [Display (Name="Pembuat")]
        public string User { get; set; }

        public virtual PemesananBarang pemesananBarang { get; set; }   
        public virtual List<PenerimaanBarangRincian> penerimaanBarangRincians { get; set; }
        public virtual Gudang gudang { get; set; }

    }

    public class PenerimaanBarangRincian
    {
        public int PenerimaanBarangRincianID { get; set; }
        public int PenerimaanBarangID { get; set; }

        [Display(Name="Kode Barang")]
        public int PemesananBarangRincianID { get; set; }
        [Display(Name="Jml Diterima")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal  JumlahDiTerima { get; set; }

        public virtual PenerimaanBarang penerimaanBarang { get; set; }
        public virtual PemesananBarangRincian pemesananBarangRincian { get; set; }
    }

    public class KartuStok
    {
        public int KartuStokID { get; set; }
        [Display (Name="Kode Barang")]
        public int BahanBakuID { get; set; }
         [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Masuk { get; set; }
         [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Keluar { get; set; }
         [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Saldo { get; set; }
        public string Keterangan { get; set; }
        public string Source { get; set; }
        public int SourceID { get; set; }
        [Display(Name="Tgl Transaksi")]
        public DateTime  TglKomputer { get; set; }
        public int GudangBahanBakuID { get; set; }

        [Display (Name="No. Lot")]
        public string NoLot { get; set; }
        public decimal HargaSatuan { get; set; }
        public decimal HargaRata2 { get; set; }

        public virtual BahanBaku bahanBaku { get; set; }
        public virtual GudangBahanBaku gudangBahanBaku { get; set; }
    }

    public class ViewKartuStok
    {
        public int GudangID { get; set; }
        public int BahanBakuID { get; set; }
        [Display(Name = "Kode Barang")]
        public string KodeBarang { get; set; }
         [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Masuk { get; set; }
         [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Keluar { get; set; }
         [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Saldo { get; set; }
        public string Keterangan { get; set; }
        public string Source { get; set; }
        public int SourceID { get; set; }
        public decimal  HargaSatuan { get; set; }
        public decimal HargaRata2 { get; set; }

        [Display(Name = "Tgl Transaksi")]
        public DateTime TglKomputer { get; set; }
    }

    public class StokOpname
    {
        public int StokOpnameID { get; set; }
        [Display(Name="Tgl Buat")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime  TglBuat { get; set; }

        [Display (Name="Gudang")]
        public int GudangID { get; set; }

        [Display(Name="Tgl Posting")]
        public DateTime TglPosting { get; set; }

        [Display(Name="User Posting")]
        public string UserPosting { get; set; }

        public bool Posting { get; set; }

        public virtual List<StokOpnameRincian> stokOpnameRincians { get; set; }
        public virtual Gudang gudang { get; set; }
    }

    public class StokOpnameRincian
    {
        public int StokOpnameRincianID { get; set; }
        public int StokOpnameID { get; set; }
        [Display(Name="Kode Barang")]
        public int BahanBakuID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public Decimal Jumlah { get; set; }

        [Display (Name="Harga Total")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal HargaSatuan { get; set; }

        [Display(Name="Operator")]
        public string UserInput { get; set; }

        public virtual StokOpname stokOpname { get; set; }
        public virtual BahanBaku bahanBaku { get; set; }
    }

    public class PemakaianPengembalianBarang
    {
        public int PemakaianPengembalianBarangID { get; set; }

        [Display(Name = "Gudang")]
        public int GudangID { get; set; }

        [Display(Name = "No. Surat Pemakaian")]
        public string NoSuratPemakaian { get; set; }

        public string Status { get; set; }

        [Display(Name = "Tgl Keluar Barang")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime tglKeluarBarang { get; set; }

        public Boolean Posting { get; set; }
        public DateTime TglPosting { get; set; }

        [Display(Name = "Bag. Gudang")]
        public string User { get; set; }

        [Display(Name = "Penerima/Pengembali")]
        public string Penerima { get; set; }

        public virtual List<PemakaianPengembalianBarangRincian> pemakaianPengembalianBarangRincians { get; set; }
        public virtual Gudang gudang { get; set; }
        
    }

    public class PemakaianPengembalianBarangRincian
    {
        public int PemakaianPengembalianBarangRincianID { get; set; }
        public int PemakaianPengembalianBarangID { get; set; }

        [Display(Name = "Kode Barang")]
        public int GudangBahanBakuID { get; set; }
        [Display(Name = "Jml")]
        [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }

        public virtual PemakaianPengembalianBarang pemakaianPengembalianBarang { get; set; }
        public virtual GudangBahanBaku gudangBahaBaku { get; set; }
    }

    public class MataUang
    {
        public int MataUangID { get; set; }
        public string Kode { get; set; }
        public string Keterangan { get; set; }

        public virtual List<PemesananBarang> pemesananBarang { get; set; }
    }

    public class PembayaranPO
    {
        public int PembayaranPOID { get; set; }
        public DateTime TglBayar { get; set; }
        public int ContactID { get; set; }
        public string NoKwitansi { get; set; }

        [Display(Name = "Kas")]
        public int tblGLAccountId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }

        public virtual List<PembayaranPODetail> pembayaranDetails { get; set; }
        public virtual tblGLAccount glAccount { get; set; }

        [Display(Name = "Jatuh Tempo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglJatuhTempo { get; set; }

        [Display(Name = "No. Giro")]
        public string NoGiro { get; set; }
    }

    public class PembayaranPODetail
    {
        public int PembayaranPODetailID { get; set; }
        public int PembayaranPOID { get; set; }
        public int PemesananBarangID { get; set; }
        public decimal Jumlah { get; set; }
        public bool Posting { get; set; }

        public virtual PemesananBarang pemesananBarang { get; set; }
        public virtual PembayaranPO pembayaranPO { get; set; }
    }
}