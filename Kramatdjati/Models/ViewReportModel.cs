using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kramatdjati.Models
{
    public class ViewReportModel
    {
        public int ViewReportModelId { get; set; }
        public int tblGLAccountId { get; set; }
        public int AccYear { get; set; }
        public int AccPeriod { get; set; }
        public decimal Debet { get; set; }
        public decimal Kredit { get; set; }

        public virtual tblGLAccount tblGLAccount { get; set; }
    }

    public class ViewReportParam
    {
        public int Period { get; set; }
        public int Tahun { get; set; }
        public int JenisLaporan { get; set; }
    }

    public class rptStok
    {
        public string KodeBahanBaku { get; set; }
        public decimal Stok { get; set; }
        public decimal HargaRata2 { get; set; }
        public decimal HargaTerakhir { get; set; }
        public decimal HargaJual { get; set; }
        public string Divisi { get; set; }
        public string Departemen { get; set; }
        public string NamaBarang { get; set; }
        public string Satuan { get; set; }

    }

    public class ViewReportStokParam
    {
        public int JenisLaporanID { get; set; }
        [Display(Name = "Divisi")]
        public int DivisiId { get; set; }
        [Display(Name = "Departemen")]
        public int DepartemenId { get; set; }
        public bool Seluruhnya { get; set; }

        public virtual JenisLaporan jenisLaporan { get; set; }
    }

    public class JenisLaporan
    {
        public int JenisLaporanID { get; set; }
        public string Laporan { get; set; }

        public virtual List<ViewReportStokParam> viewReportStokParam { get; set; }
    }

    public class StatusPemakaian
    {
        public string StatusID { get; set; }
        public string Status { get; set; }
    }

    public class rptLaporanPemakaian
    {
        public int BahanBakuID { get; set; }
        [Display(Name = "Kode Barang")]
        public string KodeBarang { get; set; }

        [Display(Name = "Qty Awal")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal JumlahAwal { get; set; }
        [Display(Name = "Rp Awal")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal HargaAwal { get; set; }

        [Display(Name = "Qty Beli")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal JumlahBeli { get; set; }
        [Display(Name = "Rp Beli")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal HargaBeli { get; set; }

        [Display(Name = "Qty Pakai")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal JumlahPakai { get; set; }
        [Display(Name = "Rp Pakai")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal HargaPakai { get; set; }

        [Display(Name = "Qty Akhir")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal JumlahAkhir { get; set; }
        [Display(Name = "Rp Akhir")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal HargaAkhir { get; set; }

    }

    public class rptSaldoAwal
    {
        public int rptSaldoAwalID { get; set; }
        public int GudangID { get; set; }
        [Display (Name="Gudang")]
        public string Lokasi { get; set; }

        [Display(Name = "Qty Awal")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Jumlah { get; set; }

        [Display(Name = "Harga Rata2")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal HargaRata2 { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Total { get; set; }
    }


}