using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kramatdjati.Models
{
    public class ViewNoRek
    {
        public int tblGLAccountID { get; set; }
        public string AccCodeDesc { get; set; }
    }

    public class UmurPiutang
    {
        public int ContactID { get; set; }

        [Display(Name ="Nama Perusahaan")]
        public string NamaPerusahaan { get; set; }

        [Display(Name ="Belum Jatuh Tempo")]
        public decimal Current { get; set; }

        [Display(Name ="1-30 Hari")]
        public decimal Piutang30 { get; set; }

        [Display(Name = "31-60 Hari")]
        public decimal Piutang60 { get; set; }

        [Display(Name = "61-90 Hari")]
        public decimal Piutang90 { get; set; }

        [Display(Name = "Lebih dari 90 Hari")]
        public decimal PiutangAlert { get; set; }
    }
}