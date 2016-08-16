using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kramatdjati.Models
{
    public class ViewBarangJadi
    {
        [Display(Name = "Kode")]
        public string KodeBahanBaku { get; set; }

        [Display(Name = "Nama Barang")]
        public string Keterangan { get; set; }

        [Display(Name = "Satuan")]
        public int SatuanID { get; set; }

        [Display(Name = "Barang yang distok")]
        public bool StokItem { get; set; }

        [Display(Name="Barang yang dibeli")]
        public bool PurchaseItem { get; set; }

         [Display(Name="Barang yang dijual")]
        public bool SaleItem { get; set; }

        [Display(Name="No. Rek COGS")]
        public int NoRekCOGSID { get; set; }

        [Display (Name="No. Rek Penjualan")]
        public int NoRekSaleID { get; set; }

        [Display (Name="No. Rek Persediaan")]
        public int NoRekInventoryID { get; set; }

        [Display (Name="Jenis Persediaan")]
        public int JenisPersediaanID { get; set; }

        [Display(Name = "Divisi")]
        public int DivisiId { get; set; }

        [Display(Name = "Departemen")]
        public int DepartemenId { get; set; }

        [Display(Name = "Ukuran")]
        public string Size { get; set; }

        public virtual Divisi Divisi { get; set; }
        public virtual Departemen Departemen { get; set; }
        public virtual JenisPersediaan JenisPersediaan { get; set; }
  
        public virtual Satuan satuan { get; set; }

        public virtual tblGLAccount NoRekCOGS { get; set; }
        public virtual tblGLAccount NoRekSale { get; set; }
        public virtual tblGLAccount NoRekInventory { get; set; }

      }

    public class ViewSalesOrderRincian
    {
        public int SalesOrderID { get; set; }

        [Display(Name = "Kode")]
        public string KodeBahanBaku { get; set; }

        [Display(Name = "Ukuran")]
        public string Size { get; set; }

        public decimal HargaJual { get; set; }
        public decimal  Jumlah { get; set; }
        public string Keterangan { get; set; }

    }

    public class ViewPindahGudangRincian
    {
        public int PindahGudangRincianID { get; set; }
         public int PindahGudangID { get; set; }

        [Display(Name = "Kode Barang")]
        public int BahanBakuID { get; set; }

        public decimal Jumlah { get; set; }
    }

    public class ContohCetak
    {
        public int Counter { get; set; }
        public string Data { get; set; }
    }
}