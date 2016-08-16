using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kramatdjati.Models
{
    public class ViewJenisKemasan
    {
        public int JenisKemasanID { get; set; }
        public int BahanBakuID { get; set; }
        public string BahanBaku { get; set; }

        [Display(Name="Kemasan")]
        public string Keterangan { get; set; }

        [Display(Name="Berat (Kg)")]
        public decimal Berat { get; set; }

     }
}