using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Kramatdjati.Models
{
    public class GLBatchView
    {
        [Display(Name = "No.Batch")]
        public int tblGLBatchId { get; set; }
        [Display(Name = "Tgl Batch")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime TglKerja { get; set; }

        [Display(Name = "Tahun Fiskal")]
        public int AccYear { get; set; }

        [Display(Name = "Perioda")]
        public int AccPeriod { get; set; }
        public decimal Debet { get; set; }
        public decimal Kredit { get; set; }
        
        public string Keterangan { get; set; }
        
    }
}