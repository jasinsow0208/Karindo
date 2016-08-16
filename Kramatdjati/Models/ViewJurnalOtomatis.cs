using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kramatdjati.Models
{
    public class ViewJurnalOtomatis
    {
        public int ViewJurnalOtomatisId { get; set; }
        public int tblGLBatchId { get; set; }

        [Display(Name = "No. Ref")]
        public string NoRef { get; set; }

        [Display(Name = "No. Rek Debet")]
        public int tblGLAccountIdDebet { get; set; }

        [Display(Name = "No. Rek Kredit")]
        public int tblGLAccountIdKredit { get; set; }

        [Display(Name = "Tgl Transaksi")]
        [DataType(DataType.Date)]
        public DateTime TglTransaksi { get; set; }

        public string Keterangan { get; set; }
        public decimal Jumlah { get; set; }

    }
}