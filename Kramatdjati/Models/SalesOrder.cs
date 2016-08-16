using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Kramatdjati.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kramatdjati.Models
{
    public class SalesOrder
    {
        [Display(Name = "Sales Order")]
        public int SalesOrderID { get; set; }

        [Display(Name = "Customer")]
        public int ContactID { get; set; }

        [Display(Name = "No. SO")]
        public string NoSO { get; set; }

        [Display(Name = "Tgl Pesan")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglPesan { get; set; }

        [Display(Name = "Tgl Kirim")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglPengiriman { get; set; }

        [Display(Name = "Pembuat")]
        public string User { get; set; }

        public string Catatan { get; set; }
        public Boolean Posting { get; set; }
        public Boolean Closed { get; set; }

        [Display(Name = "No. PO")]
        public string NoPO { get; set; }

        public virtual Contact contact { get; set; }
        public virtual List<SalesOrderRincian> SalesOrderRincians { get; set; }
        public virtual List<SuratJalan> suratJalans { get; set; }
    }

    public class SalesOrderRincian
    {
        public int SalesOrderRincianID { get; set; }
        public int SalesOrderID { get; set; }

        [Display(Name = "Kode Barang")]
        public int BahanBakuID { get; set; }

        [Display(Name = "Harga Jual")]
        public decimal HargaJual { get; set; }
        public decimal Jumlah { get; set; }
        public string Keterangan { get; set; }

        public decimal JmlDiproduksi { get; set; }
        public int? StatusProduksiID { get; set; }

        [Display(Name = "Jumlah diterima")]
        public decimal JmlYangSudahDiKirim { get; set; }

        [Display(Name = "Status Lengkap")]
        public Boolean statusLengkap { get; set; }

        public virtual List<SuratJalanRincian> suratJalanRincians { get; set; }
        public virtual List<OrderBahanBaku> orderBahanBakus { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
        public virtual BahanBaku bahanBaku { get; set; }
        public virtual StatusProduksi statusProduksi { get; set; }

    }

    public class StatusProduksi
    {
        public int StatusProduksiID { get; set; }
        public string Status { get; set; }

        public virtual List<BahanBaku> bahanBakus { get; set; }
    }

    public class SuratJalan
    {
        public int SuratJalanID { get; set; }

        [Display(Name = "Gudang")]
        public int GudangID { get; set; }

        //[Display(Name = "No. SO")]
        //public int SalesOrderID { get; set; }

        public int ContactID { get; set; }

        [Display(Name = "No. Surat Jalan")]
        public string NoSuratJalan { get; set; }

        [Display(Name = "Tgl Surat Jalan")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime tglSuratJalan { get; set; }

        [Display(Name = "Tgl Transaksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglTransaksi { get; set; }

        [Display(Name = "Tgl Terima")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglTerima { get; set; }

        [Display(Name = "Nama Penerima")]
        public string Penerima { get; set; }

        public Boolean Posting { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglPosting { get; set; }

        public string Catatan { get; set; }
        public Boolean Closed { get; set; }
        public Boolean Cetak { get; set; }

        [Display(Name = "Adm Surat Jalan")]
        public string User { get; set; }

        [Display(Name = "Gudang")]
        public string Gudang { get; set; }

        //public virtual SalesOrder salesOrder { get; set; }
        public virtual List<SuratJalanRincian> suratJalanRincians { get; set; }
        public virtual Contact contact { get; set; }

        [NotMapped]
        public Boolean StatusTutup
        {
            get
            {

                if (suratJalanRincians == null)
                {
                    return false;
                };

                foreach (SuratJalanRincian rw in suratJalanRincians)
                {
                    if (rw.Kirim == false)
                    {
                        return false;
                    }
                };
                return true;
            }
        }

        [NotMapped]
        public Boolean StatusKirimGudang
        {
            get
            {
                if (suratJalanRincians == null)
                {
                    return false;
                }
                else
                {
                    if (suratJalanRincians.Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                };
            }
        }
    }

    public class SuratJalanRincian
    {
        public int SuratJalanRincianID { get; set; }
        public int SuratJalanID { get; set; }

        [Display(Name = "Kode Barang")]
        public int SalesOrderRincianID { get; set; }

        [Display(Name = "Jml Dikirim")]
        public decimal JumlahDikirim { get; set; }

        public bool Kirim { get; set; }

        public virtual SuratJalan suratJalan { get; set; }
        public virtual SalesOrderRincian salesOrderRincian { get; set; }
        public virtual List<SuratJalanLog> suratJalanLogs { get; set; }
    }

    public class SuratJalanLog
    {
        public int SuratJalanLogID { get; set; }
        public int SuratJalanRincianID { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglProses { get; set; }
        public string Operator { get; set; }
        public string Keterangan { get; set; }

        public virtual SuratJalanRincian suratJalanRincian { get; set; }
    }

    public class SuratJalanCetak
    {
        public int SuratJalanCetakID { get; set; }
        public int SuratJalanID { get; set; }
        public Boolean StatusFaktur { get; set; }

        public virtual SuratJalan suratJalan { get; set; }
        public virtual List<SuratJalanCetakRincian> suratJalanCetakRincians { get; set; }
        public virtual List<FakturJual> fakturJuals { get; set; }
    }

    public class SuratJalanCetakRincian
    {
        public int SuratJalanCetakRincianID { get; set; }
        public int SuratJalanCetakID { get; set; }
        public int GudangBahanBakuID { get; set; }

        public int Jumlah { get; set; }
        public string Keterangan { get; set; }

        public virtual SuratJalanCetak suratJalanCetak { get; set; }
        public virtual GudangBahanBaku gudangBahanBku { get; set; }

    }

    public class PindahGudang
    {
        public int PindahGudangID { get; set; }

        [Display(Name = "No Bukti")]
        public string BuktiPindahGudang { get; set; }

        public string Keterangan { get; set; }

        [Display(Name = "Gudang Asal")]
        public int GudangAsalID { get; set; }

        [Display(Name = "Gudang Tujuan")]
        public int GudangTujuanID { get; set; }

        [Display(Name = "Tgl Transaksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglTransaksi { get; set; }

        [Display(Name = "Posting Barang Keluar")]
        public string UserGudangAsal { get; set; }

        [Display(Name = "Posting Barang Masuk")]
        public string UserGudangTujuan { get; set; }

        [Display(Name = "No. Lot")]
        public string NoLot { get; set; }

        public Boolean Posting { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglPosting { get; set; }

        public Boolean KeluarPosting { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime KeluarTglPosting { get; set; }

        public Boolean MasukPosting { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime MasukTglPosting { get; set; }

        public string Source { get; set; }
        public int SourceID { get; set; }

        public virtual List<PindahGudangRincian> pindahGudangRincians { get; set; }
        public virtual Gudang gudangAsal { get; set; }
        public virtual Gudang gudangTujuan { get; set; }
    }

    public class PindahGudangRincian
    {
        public int PindahGudangRincianID { get; set; }
        public int PindahGudangID { get; set; }

        [Display(Name = "Kode Barang")]
        public int GudangBahanBakuID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }

        public virtual PindahGudang pindahGudang { get; set; }
        public virtual GudangBahanBaku gudangBahanBaku { get; set; }

    }

    public class Resep
    {
        public int ResepID { get; set; }

        [Display(Name = "Kode BOM")]
        public string KodeResep { get; set; }
        public string Keterangan { get; set; }

        [Display(Name = "Tgl Buat")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglBuat { get; set; }

        public Boolean Posting { get; set; }

        public virtual List<ResepRincian> resepRincians { get; set; }
        public virtual List<JPDeptARincian> jpDeptARincians { get; set; }
        public virtual List<OrderBahanBaku> orderBahanBakus { get; set; }
    }

    public class ResepRincian
    {
        public int ResepRincianID { get; set; }
        public int ResepID { get; set; }

        [Display(Name = "Kode Barang")]
        public int BahanBakuID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public Decimal Jumlah { get; set; }

        public virtual Resep resep { get; set; }
        public virtual BahanBaku bahanBaku { get; set; }
    }

    public class OrderBahanBaku
    {
        public int OrderBahanBakuID { get; set; }

        [Display(Name = "BOM")]
        public int ResepID { get; set; }
        public int JPDeptARincianID { get; set; }

        public string Keterangan { get; set; }
        public Boolean Posting { get; set; }
        [Display(Name = "Gudang Tujuan")]
        public int GudangID { get; set; }

        public virtual Resep resep { get; set; }
        public virtual JPDeptARincian jpDeptARincian { get; set; }
        public virtual Gudang gudang { get; set; }

    }

    public class HargaJual
    {
        public int HargaJualID { get; set; }

        [Display(Name = "Tgl Berlaku")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglBerlaku { get; set; }

        [Display(Name = "Jenis Spon")]
        public string JenisSpon { get; set; }

        [Display(Name = "Harga")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Harga { get; set; }

        [Display(Name = "/mm")]
        public Boolean mm { get; set; }
    }

    public class HargaJualPerCustomer
    {
        public int HargaJualPerCustomerID { get; set; }

        [Display(Name = "Customer")]
        public int ContactID { get; set; }

        [Display(Name = "Tgl Berlaku")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglBerlaku { get; set; }

        [Display(Name = "Jenis Spon")]
        public string JenisSpon { get; set; }

        [Display(Name = "Harga")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Harga { get; set; }

        [Display(Name = "/mm")]
        public Boolean mm { get; set; }

        public virtual Contact contact { get; set; }
    }

    public class FakturJual
    {
        public int FakturJualID { get; set; }

        [Display(Name = "Customer")]
        public int ContactID { get; set; }

        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Kota { get; set; }
        public string NPWP { get; set; }

        [Display(Name = "Tgl Faktur")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglFaktur { get; set; }

        [Display(Name = "No. Faktur")]
        public string NoFaktur { get; set; }

        [Display(Name = "No. Seri")]
        public string NomorSeri { get; set; }

        [Display(Name = "Tgl Jatuh Tempo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglJatuhTempo { get; set; }

        [Display(Name = "Tgl Pesan")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglPesan { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Diskon { get; set; }

        public Boolean PPN { get; set; }
        public Boolean Posting { get; set; }

        [Display(Name = "No. Surat Jalan")]
        public int SuratJalanCetakID { get; set; }

        public virtual Contact customer { get; set; }
        public virtual SuratJalanCetak suratJalanCetak { get; set; }
        public virtual List<FakturJualRincian> fakturJualRincians { get; set; }
        public virtual List<PembayaranSODetail> pembayaranDetails { get; set; }

        [NotMapped]
        public decimal Total
        {
            get
            {
                try
                {
                    decimal mTotal = fakturJualRincians.Sum(x => x.HargaSatuan * x.Jumlah);

                    if (PPN == true)
                    {
                        mTotal = (mTotal - Diskon) * 11 / 10;
                        return mTotal;
                    }
                    else
                    {
                        return (mTotal - Diskon);
                    };
                }
                catch (Exception)
                {

                    return 0;
                }
            }
        }

        [NotMapped]
        public decimal Pembayaran
        {
            get
            {
                try
                {
                    decimal mTotalBayar = pembayaranDetails.Where(x => x.Posting == true).Sum(x => x.Jumlah);
                    return mTotalBayar;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public bool StatusLunas 
        {
            get
            {
                try
                {
                    if (Pembayaran >= Total)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    };
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }

    public class FakturJualRincian
    {
        public int FakturJualRincianID { get; set; }
        public int FakturJualID { get; set; }
        public int GudangBahanBakuID { get; set; }

        public int Jumlah { get; set; }
        public string Keterangan { get; set; }

        [Display(Name = "Harga Satuan")]
        public decimal HargaSatuan { get; set; }

        public virtual FakturJual fakturJual { get; set; }
        public virtual GudangBahanBaku gudangBahanBaku { get; set; }
    }

    public class PembayaranSO
    {
        public int PembayaranSOID { get; set; }

        [Display(Name = "Tgl Bayar")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglBayar  { get; set; }

        [Display(Name="Customer")]
        public int ContactID { get; set; }

        [Display(Name = "No. Referensi")]
        public string NoKwitansi { get; set; }

        [Display(Name="Kas/Bank")]
        public int tblGLAccountId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }

        [Display(Name = "Jatuh Tempo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglJatuhTempo { get; set; }

        [Display(Name = "No. Giro")]
        public string NoGiro { get; set; }

        public bool Posting { get; set; }

        public bool PostingBayar { get; set; }

        public virtual List<PembayaranSODetail> pembayaranDetails { get; set; }
        public virtual Contact customer { get; set; }
        public virtual tblGLAccount glAccount { get; set; }
    }

    public class PembayaranSODetail
    {
        public int PembayaranSODetailID { get; set; }
        public int PembayaranSOID { get; set; }

        [Display(Name="No. Faktur")]
        public int FakturJualID { get; set; }

         [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }
        public bool Posting { get; set; }

        public virtual FakturJual fakturJual { get; set; }
        public virtual PembayaranSO pembayaranSO { get; set; }
    }

    
}