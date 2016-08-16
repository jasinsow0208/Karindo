using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Kramatdjati.Models
{
    public class ViewBukuBesar
    {
        [Display(Name = "No.Batch")]
        public int tblGLBatchId { get; set; }

        [Display(Name = "Tahun Fiskal")]
        public int AccYear { get; set; }

        [Display(Name = "Perioda")]
        public int AccPeriod { get; set; }

        [Display(Name = "Tgl Posting")]
        public DateTime TglKomputer { get; set; }

        [Display(Name = "No. Ref")]
        public string NoRef { get; set; }

        [Display(Name = "No. Rekening")]
        public int tblGLAccountId { get; set; }

        [Display(Name = "Acc Name")]
        [Required]
        public string AccName { get; set; }

        [Display(Name = "Tgl Transaksi")]
        [DataType(DataType.Date)]
        public DateTime TglTransaksi { get; set; }

        public string Keterangan { get; set; }
        public decimal Debet { get; set; }
        public decimal Kredit { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoAwal { get; set; }
        

    }
}