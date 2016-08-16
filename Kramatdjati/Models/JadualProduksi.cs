using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kramatdjati.Models
{
    public class JPDeptA
    {
        public int JPDeptAID { get; set; }

        [Display (Name="Tgl Produksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType (DataType.Date)]
        public DateTime TglProduksi { get; set; }

        [Display(Name = "Tgl Penimbangan")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglPenimbangan { get; set; }
        
        [Display (Name="Dibuat Oleh")]
        public string DibuatOleh { get; set; }
        public string Catatan { get; set; }
        public Boolean  Posting { get; set; }

        public virtual List<JPDeptARincian> jpDeptARincians { get; set; }
        public virtual List<JPDeptACompound> jPDeptACompounds { get; set; }
        public virtual List<BPPB> bPPBs { get; set; }
    }

    public class JPDeptACompound
    {
        public int JPDeptACompoundID { get; set; }
        public int JPDeptAID { get; set; }

        [Display (Name="Kode Barang Compound")]
        public int GudangBahanBakuID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N3}", ApplyFormatInEditMode = true)]
        public decimal Jumlah { get; set; }

        public virtual JPDeptA jPDeptA { get; set; }
        public virtual GudangBahanBaku gudangBahanBaku { get; set; }
    }

    public class JPDeptARincian
    {
        public int JPDeptARincianID { get; set; }
        public int JPDeptAID { get; set; }

        [Display(Name="Kode barang Jadi")]
        public string KodeBarangJadi { get; set; }
        public int Batch { get; set; }
        public int Lembar { get; set; }
        public string Keterangan { get; set; }

        [Display(Name = "BOM")]
        public int ResepID { get; set; }

        [Display(Name="No. Lot")]
        public string NoLot { get; set; }

        public Boolean Posting { get; set; }
        public string Penimbang { get; set; }

        public virtual JPDeptA  jpDeptA { get; set; }
        public virtual List<JPDeptASO> jpDeptASOs { get; set; }
        public virtual List<OrderBahanBaku> orderBahanBakus {get;set;}
        public virtual List<LPDeptARincian> lpDeptARincians { get; set; }
        public virtual Resep resep { get; set; }
        public virtual List<PenimbanganProduksi> penimbanganProduksis { get; set; }
    }

    public class JPDeptASO
    {
        public int JPDeptASOID { get; set; }
        public int JPDeptARincianID { get; set; }
        public int SalesOrderRincianID { get; set; }

        public virtual JPDeptARincian jpDeptARincian { get; set; }
        public virtual SalesOrderRincian salesOrderRincian { get; set; }
    }

    public class PenimbanganProduksi
    {
        public int PenimbanganProduksiID { get; set; }
        public int JPDeptARincianID { get; set; }

        [Display(Name="Kode Barang")]
        public int GudangBahanBakuID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }
        public string Keterangan { get; set; }
        public bool Posting { get; set; }

        public virtual JPDeptARincian  jpDeptARincian { get; set; }
        public virtual GudangBahanBaku gudangBahanBaku { get; set; }
    }

    public class LPDeptA
    {
        public int LPDeptAID { get; set; }

        [Display(Name="Tgl Produksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date )]
        public DateTime TglProduksi { get; set; }

        [Display (Name="Penimbangan")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? PenimbanganAwal { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? PenimbanganAkhir { get; set; }

        [Display(Name = "Kneader")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? KneaderAwal { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? KneaderAkhir { get; set; }

        [Display(Name = "Boiler")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? BoilerAwal { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? BoilerAkhir { get; set; }

        [Display(Name = "Roll")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? RollAwal { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? RollAkhir { get; set; }

        [Display(Name = "Hot Press B")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? HotPressBAwal { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? HotPressBAkhir { get; set; }

        [Display(Name = "Hot Press K")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? HotPressKAwal { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? HotPressKAkhir { get; set; }

        public string Dilaporkan { get; set; }
        public string Diketahui { get; set; }
        public string Catatan { get; set; }
        public Boolean Posting { get; set; }

        public virtual List<LPDeptARincian> lpDeptARincians { get; set; }
    }

    public class LPDeptARincian
    {
        public int LPDeptARincianID { get; set; }
        public int LPDeptAID { get; set; }

        [Display(Name = "Tgl Produksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglProduksi { get; set; }

        [Display(Name="Kode Barang")]
        public int JPDeptARincianID { get; set; }

        public int Hasil { get; set; }
        public int Cacat { get; set; }

        [Display (Name="Sisa Compound (Kg)")]
        [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal SisaCompound { get; set; }

        [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Rajangan { get; set; }

        public string Keterangan { get; set; }

        [Display (Name="Jml Transfer")]
        public int JmlTransfer { get; set; }

        public virtual LPDeptA lpDeptA { get; set; }
        public virtual JPDeptARincian jpDeptARincian { get; set; }
        public virtual List<JPDeptBRincian> jpDeptBRincians {get;set;}
    }

    public class LPDeptARajangan
    {
        public int LPDeptARajanganID { get; set; }
        public int LPDeptAID { get; set; }

        [Display(Name = "Kode Barang Rajangan")]
        public int GudangBahanBakuID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N4}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }

        public virtual LPDeptA lPDeptA { get; set; }
        public virtual GudangBahanBaku gudangBahanBaku { get; set; }
    }

    public class JPDeptB
    {
        public int JPDeptBID { get; set; }
        [Display(Name="Tgl Produksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType (DataType.Date )]
        public DateTime TglProduksiDeptB { get; set; }

        [Display (Name="Tgl Produksi Dept A")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglProduksiDeptA { get; set; }

        [Display (Name="Dibuat Oleh")]
        public string  DibuatOleh { get; set; }

        public string  Catatan { get; set; }
        public Boolean Posting { get; set; }
        public virtual List<JPDeptBRincian> jpDeptBRincians {get;set;}
    }

    public class JPDeptBRincian
    {
        public int JPDeptBRincianID { get; set; }
        public int JPDeptBID { get; set; }

        [Display(Name="Kode Barang")]
        public int LPDeptARincianID { get; set; }

        [Display (Name="Tebal (mm)")]
        public string UkuranTebal { get; set; }
        [Display(Name = "Banyaknya (lbr)")]
        public int Banyaknya { get; set; }

        [Display (Name="Bhn Dipakai (lbr)")]
        public int JmlBahan { get; set; }
        public string Keterangan { get; set; }

        public virtual JPDeptB jpDeptB {get;set;}
        public virtual LPDeptARincian lpDeptARincian {get;set;}
        public virtual List<LPDeptBRincian> lpDeptBRincians {get;set;}

    }

    public class LPDeptB
    {
        public int LPDeptBID { get; set; }

        [Display (Name="Tgl Produksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType (DataType.Date )]
        public DateTime TglProduksi { get; set; }
        public string Dilaporkan { get; set; }
        public string Diketahui { get; set; }

        [Display(Name="Jam Kerja")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime  JamKerjaAwal { get; set; }

        [Display (Name="s/d")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime JamKerjaAkhir { get; set; }

        public string Catatan { get; set; }
        public Boolean Posting { get; set; }

        public virtual List<LPDeptBRincian > lpDeptBRincians {get;set;}
    }

    public class LPDeptBRincian
    {
        public int LPDeptBRincianID { get; set; }
        public int LPDeptBID { get; set; }

        [Display(Name="Kode Barang")]
        public int JPDeptBRincianID { get; set; }

        [Display (Name="Hasil (lbr)")]
        public int Hasil { get; set; }

        [Display (Name="Cacat (lbr)")]
        public int Cacat { get; set; }

        public string Keterangan { get; set; }

        public virtual LPDeptB lpDeptB {get;set;}
        public virtual JPDeptBRincian jpDeptBRincian {get;set;}
    }

     public class HariRaya
    {
        public int HariRayaID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime Tgl { get; set; }

        public string Keterangan { get; set; }
    }

    public class BPPB
    {
        public int BPPBID { get; set; }

        [Display(Name = "Tgl Penimbangan")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime  TglPenimbangan { get; set; }

        [Display(Name = "Tgl Produksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime  TglProduksi { get; set; }

        public int NoBPPB { get; set; }
        public string Keterangan { get; set; }
        public string Diminta { get; set; }
        public string Diserahkan { get; set; }
        public string  Diterima { get; set; }
        public bool Posting { get; set; }

        public int JPDeptAID { get; set; }

        public virtual List<BPPBRincian> bPPBRincians { get; set; }
        public virtual JPDeptA jPDeptA { get; set; }

    }

    public class BPPBView
    {
        public int BPPBID { get; set; }

        [Display(Name = "Tgl Penimbangan")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglPenimbangan { get; set; }

        [Display(Name = "Tgl Produksi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TglProduksi { get; set; }

        public int NoBPPB { get; set; }
        public string Keterangan { get; set; }
        public string Diminta { get; set; }
        public string Diserahkan { get; set; }
        public string Diterima { get; set; }
        public bool Posting { get; set; }
        public bool PostingOK { get; set; }

    }

    public class BPPBRincian
    {
        public int BPPBRincianID { get; set; }
        public int BPPBID { get; set; }
        public int GudangBahanBakuID { get; set; }

        [Display(Name = "Kebutuhan")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal  Kebutuhan { get; set; }
        [Display(Name = "Pembulatan")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Pembulatan { get; set; }

        public decimal SatuanZak { get; set; }
        public int JmlZak { get; set; }
        public bool PostingDiserahkan { get; set; }
        public string Diserahkan { get; set; }
        public bool PostingDiterima { get; set; }
        public string Diterima { get; set; }
        public decimal PenerimaanReal { get; set; }
        public bool PostingPenerimaanReal { get; set; }

        public virtual GudangBahanBaku gudangBahanBaku { get; set; }
        public virtual BPPB bPPB { get; set; }
    }

    public class PasswordSerahTerima
    {
        public int PasswordSerahTerimaID { get; set; }
        public string Operator { get; set; }
        public string Password { get; set; }
    }

    public class Packing
    {
        public int PackingID { get; set; }
        public string Size { get; set; }
        [Display (Name="Jml/Pack")]
        public int JmlPerPacking { get; set; }
    }





}