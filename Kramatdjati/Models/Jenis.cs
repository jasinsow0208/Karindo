using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kramatdjati.Infrastructure;

namespace Kramatdjati.Models
{
    public class Jenis
    {
        public int JenisID { get; set; }
        [Display(Name = "Kode")]
        public string KodeJenis { get; set; }
        [Display(Name = "Tgl")]
        public DateTime TglKomputer { get; set; }
        public string Pembuat { get; set; }
        public string Keterangan { get; set; }

        public virtual List<JenisDetail> JenisDetails { get; set; }

        public Jenis()
        {
            TglKomputer = DateTime.Now;
        }
    }

    public class JenisDetail
    {
        public int JenisDetailID { get; set; }
        public int JenisID { get; set; }

        [Display(Name = "Bahan")]
        public int BahanID { get; set; }

        [Display(Name = "Bahan Baku")]
        public int BahanBakuID { get; set; }

        public decimal Berat { get; set; }
        public string Keterangan { get; set; }

        public virtual Jenis Jenis { get; set; }
        public virtual Bahan Bahan { get; set; }
        public virtual BahanBaku BahanBaku { get; set; }
    }

    public class Bahan
    {
        public int BahanID { get; set; }

        [Display(Name = "Bahan")]
        public string Keterangan { get; set; }

        public virtual List<JenisDetail> JenisDetails { get; set; }
    }

    public class JenisPersediaan
    {
        public int JenisPersediaanID { get; set; }
        public string Keterangan { get; set; }

        public virtual List<BahanBaku> BahanBakus { get; set; }
        public virtual List<tblDefault> compounds { get; set; }
        public virtual List<tblDefault> rajangans { get; set; }
        public virtual List<tblDefault> jenisBahanBakus { get; set; }
        public virtual List<tblDefault> jenisBarangJadis { get; set; }
    }

    public class Divisi
    {
        public int DivisiId { get; set; }
        [Display(Name = "Divisi")]
        public string Keterangan { get; set; }

        public virtual List<BahanBaku> BahanBakus { get; set; }
        public virtual List<Departemen> Departemens { get; set; }
    }

    public class Departemen
    {
        public int DepartemenID { get; set; }
        public int DivisiId { get; set; }
        [Display(Name = "Departemen")]
        public string Keterangan { get; set; }

        public virtual Divisi Divisi { get; set; }
        public virtual List<BahanBaku> BahanBakus { get; set; }
    }
    public class BahanBaku
    {
        public int BahanBakuID { get; set; }

        [Display(Name = "Kode")]
        public string KodeBahanBaku { get; set; }
        [Display(Name = "Nama Barang")]
        public string Keterangan { get; set; }

        [DecimalPrecision(20, 5)]
        public decimal Stok { get; set; }

        [Display(Name = "Satuan")]
        public int SatuanID { get; set; }

        [Display(Name = "Harga Rata2")]
        [DataType(DataType.Currency)]
        public decimal HargaRata2 { get; set; }

        [Display(Name = "Harga Terakhir")]
        [DataType(DataType.Currency)]
        public decimal HargaTerakhir { get; set; }

        [Display(Name = "Harga Jual")]
        [DataType(DataType.Currency)]
        public decimal HargaJual { get; set; }

        [Display(Name = "Non Aktif")]
        public bool Discontinue { get; set; }

        [Display(Name = "Barang yang distok")]
        public bool StokItem { get; set; }

        [Display(Name = "Barang yang dibeli")]
        public bool PurchaseItem { get; set; }

        [Display(Name = "Barang yang dijual")]
        public bool SaleItem { get; set; }

        [Display(Name = "No. Rek COGS")]
        public int NoRekCOGSID { get; set; }

        [Display(Name = "No. Rek Penjualan")]
        public int NoRekSaleID { get; set; }

        [Display(Name = "No. Rek Persediaan")]
        public int NoRekInventoryID { get; set; }

        [Display(Name = "Jenis Persediaan")]
        public int JenisPersediaanID { get; set; }

        [Display(Name = "Divisi")]
        public int DivisiId { get; set; }

        [Display(Name = "Departemen")]
        public int DepartemenId { get; set; }

        [Display(Name = "BarangJadi")]
        public Boolean BarangJadi { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "KodeBarangJadi")]
        public string KodeBarangJadi { get; set; }

        public virtual Divisi Divisi { get; set; }
        public virtual Departemen Departemen { get; set; }
        public virtual JenisPersediaan JenisPersediaan { get; set; }
        public virtual List<KartuStok> KartuStoks { get; set; }
        public virtual List<StokOpnameRincian> stokOpnameRincians { get; set; }
        public virtual List<SalesOrderRincian> salesOrderRincians { get; set; }
        public virtual List<GudangBahanBaku> gudangBahanBaku { get; set; }

        public BahanBaku()
        {
            Stok = 0;
            HargaRata2 = 0;
            HargaTerakhir = 0;
            StokItem = false;
            HargaJual = 0;
        }
        public virtual List<JenisKemasan> JenisKemasans { get; set; }
        public virtual Satuan satuan { get; set; }

        public virtual tblGLAccount NoRekCOGS { get; set; }
        public virtual tblGLAccount NoRekSale { get; set; }
        public virtual tblGLAccount NoRekInventory { get; set; }

        public virtual List<PemesananBarangRincian> PemesananBarangRincians { get; set; }

        [NotMapped]
        public int Ukuran
        {
            get
            {

                int intUkuran;
                if (Size != null)
                {
                    if (int.TryParse(Size.Split(' ')[0], out intUkuran))
                    {
                        if (Size.Split(' ').Count() > 1)
                        {
                            intUkuran = intUkuran * 10 + Size.Split(' ').Count();
                        }
                        else
                        {
                            intUkuran = intUkuran * 10;
                        }
                        return intUkuran;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }

            }
        }
        [NotMapped]
        public string Warna
        {
            get
            {
                string strWarna = KodeBahanBaku.Split(' ')[0];
                return strWarna;
            }
        }
        [NotMapped]
        public string JenisSpon
        {
            get
            {
                if (Size.IndexOf(' ') < 0)
                {
                    return " ";
                }
                else
                {
                    return Size.Substring(Size.IndexOf(' '));
                }

            }
        }
    }
    public class Gudang
    {
        public int GudangID { get; set; }
        public string Lokasi { get; set; }

        public virtual List<GudangBahanBaku> gudangBahanBakus { get; set; }
        public virtual List<PenerimaanBarang> penerimaanBarangs { get; set; }
        public virtual List<StokOpname> stokOpnames { get; set; }
        public virtual List<tblDefault> gudangBelis { get; set; }
        public virtual List<tblDefault> gudangJuals { get; set; }
        public virtual List<tblDefault> gudangProduksis { get; set; }
        public virtual List<PemakaianPengembalianBarang> pemakaianPengembalianBarangs { get; set; }

        public virtual List<PindahGudang> gudangAsals { get; set; }
        public virtual List<PindahGudang> gudangTujuans { get; set; }

        public virtual List<OrderBahanBaku> orderBahanBakus { get; set; }

    }

    public class GudangBahanBaku
    {
        public int GudangBahanBakuID { get; set; }
        public int GudangID { get; set; }

        [Display(Name = "Persediaan")]
        public int BahanBakuID { get; set; }

        [DecimalPrecision(20, 5)]
        [Display(Name = "Stok")]
        public decimal Jumlah { get; set; }

        public virtual Gudang gudang { get; set; }
        public virtual BahanBaku bahanBaku { get; set; }
        public virtual List<KartuStok> kartuStoks { get; set; }
        public virtual List<PindahGudangRincian> pindahGudangRincians { get; set; }
        public virtual List<JPDeptACompound> jPDeptACompound { get; set; }
        public virtual List<BPPBRincian> bPPBRincians { get; set; }
        public virtual List<PenimbanganProduksi> penimbanganProduksis { get; set; }
        public virtual List<FakturJualRincian> fakturJualRincians { get; set; }

    }
    public class JenisKemasan
    {
        public int JenisKemasanID { get; set; }
        public int BahanBakuID { get; set; }
        [Display(Name = "Jenis Kemasan")]
        public string Keterangan { get; set; }

        [Display(Name = "Ukuran")]
        public decimal Berat { get; set; }

        public bool Default { get; set; }

        //[Display (Name ="Satuan")]
        //public int SatuanID { get; set; }

        //public virtual Satuan Satuan { get; set; }
        public virtual BahanBaku BahanBaku { get; set; }
    }

    public class Satuan
    {
        public int SatuanID { get; set; }

        [Display(Name = "Satuan")]
        public string Keterangan { get; set; }
        public string Penjelasan { get; set; }

        public Satuan()
        {
            Penjelasan = "";
        }
    }

    public class Contact
    {
        public int ContactID { get; set; }
        public string Perusahaan { get; set; }
        public string Kontak { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public string Kota { get; set; }
        public string Propinsi { get; set; }
        public string Negara { get; set; }
        public string Telepon { get; set; }
        public string Fax { get; set; }
        [Display(Name = "No. Rek Piutang")]
        public int NoRekPiutangID { get; set; }

        [Display(Name = "No. Rek Hutang")]
        public int NoRekHutangID { get; set; }

        [Display(Name = "Batas Kredit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public decimal KreditLimit { get; set; }

        public Boolean PPN { get; set; }
        public Boolean Customer { get; set; }
        public Boolean Supplier { get; set; }

        [Display(Name = "Nama")]
        public string NamaFaktur1 { get; set; }
        [Display(Name = "Alamat")]
        public string AlamatFaktur1 { get; set; }
        [Display(Name = "Kota")]
        public string KotaFaktur1 { get; set; }
        [Display(Name = "NPWP")]
        public string NPWPFaktur1 { get; set; }
        [Display(Name = "Nama")]
        public string NamaFaktur2 { get; set; }
        [Display(Name = "Alamat")]
        public string AlamatFaktur2 { get; set; }
        [Display(Name = "Kota")]
        public string KotaFaktur2 { get; set; }
        [Display(Name = "NPWP")]
        public string NPWPFaktur2 { get; set; }
        [Display(Name="Status Kredit")]
        public string StatusKredit { get; set; }

        public bool Kredit { get; set; }
        public virtual tblGLAccount NoRekPiutang { get; set; }
        public virtual tblGLAccount NoRekHutang { get; set; }

        public virtual List<PemesananBarang> PemesananBarangs { get; set; }
        public virtual List<HargaJualPerCustomer> hargaJualPerCustomers { get; set; }
        public virtual List<SAPiutang> SAPiutangs { get; set; }
        public virtual List<SAHutang> SAHutangs { get; set; }
        public virtual List<FakturJual> fakturJuals { get; set; }

        public Contact()
        {
            PPN = true;
        }

    }

    public class KasBank
    {
        public int KasBankID { get; set; }
        [Display(Name = "Bank/Kas")]
        public string NamaKasBank { get; set; }
        [Display(Name = "Account/Keterangan")]
        public string NamaAccount { get; set; }
        [Display(Name = "No. Account")]
        public string NoAccount { get; set; }

        [DataType(DataType.Currency)]
        public decimal Saldo { get; set; }

        [Display(Name = "No. Rekening")]
        public int tblGLAccountID { get; set; }

        public KasBank()
        {
            Saldo = 0;
        }

        public virtual tblGLAccount tblGlAccount { get; set; }
    }

   
    public class SAPiutang {
        public int SAPiutangID { get; set; }

        [Display(Name="Customer")]
        public int ContactID { get; set; }

        [Display(Name = "Tgl Faktur")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglInvoice { get; set; }

        [Display(Name="No Faktur")]
        public string NoInvoice { get; set; }

        [Display(Name = "Tgl Jatuh Tempo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglJatuhTempo { get; set; }

        [Display(Name = "Jumlah")]
        [DisplayFormat(ApplyFormatInEditMode =false, DataFormatString = "{0:N0}")]
        public decimal Jumlah { get; set; }

        [Display(Name = "Tgl Bayar")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglBayar { get; set; }

        public Boolean StatusBayar { get; set; }

        public Boolean Posting { get; set; }

        public virtual Contact contact { get; set; }
    }

    public class SAHutang
    {
        public int SAHutangID { get; set; }

        [Display(Name = "Supplier")]
        public int ContactID { get; set; }

        [Display(Name = "Tgl Faktur")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglInvoice { get; set; }

        [Display(Name = "No Faktur")]
        public string NoInvoice { get; set; }

        [Display(Name = "Tgl Jatuh Tempo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglJatuhTempo { get; set; }

        [Display(Name = "Jumlah")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
        public decimal Jumlah { get; set; }

        [Display(Name = "Tgl Bayar")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglBayar { get; set; }

        public Boolean StatusBayar { get; set; }

        public Boolean Posting { get; set; }

        public virtual Contact contact { get; set; }
    }
}