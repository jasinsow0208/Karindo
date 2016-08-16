using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Kramatdjati.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using Kramatdjati.Infrastructure;
using System.Text;

namespace Kramatdjati.Controllers
{
    public class LaporanStokController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: LaporanStok
        public ActionResult Index()
        {
            List<JenisLaporan> jenisLaporans = new List<JenisLaporan>();
            jenisLaporans.Add(new JenisLaporan { JenisLaporanID = 1, Laporan = "Laporan Stok"});
            jenisLaporans.Add(new JenisLaporan { JenisLaporanID = 2, Laporan = "Isian Stok Opname"});

            int InitDivisiId = 0;
            if (db.Divisis.Count() > 0)
            {
                InitDivisiId = db.Divisis.OrderBy(x => x.DivisiId).First().DivisiId;
            }
            else
            {
                InitDivisiId = 0;
            };

            ViewReportStokParam rptStokParam = new ViewReportStokParam();
            rptStokParam.Seluruhnya = true;
            ViewBag.JenisLaporanID = new SelectList(jenisLaporans , "JenisLaporanID", "Laporan");
            ViewBag.DivisiID = new SelectList(db.Divisis, "DivisiId", "Keterangan");
            ViewBag.DepartemenID = new SelectList(db.Departemen.Where(x => x.DivisiId == InitDivisiId), "DepartemenId", "Keterangan");

            return View(rptStokParam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "JenisLaporanID,DivisiId,DepartemenId,Seluruhnya")] ViewReportStokParam  rptStokParam)
        {
            if (ModelState.IsValid)
            {
                if (rptStokParam.Seluruhnya == true )
                {
                    return RedirectToAction("LaporanStok", new {DivisiID=0, DepartemenID=0 , JenisLaporanID=rptStokParam.JenisLaporanID });
                }
                if (rptStokParam.Seluruhnya == false )
                {
                    return RedirectToAction("LaporanStok", new { DivisiID = rptStokParam.DivisiId, DepartemenID = rptStokParam.DepartemenId, JenisLaporanID = rptStokParam.JenisLaporanID });
                }
            }

            List<JenisLaporan> jenisLaporans = new List<JenisLaporan>();
            jenisLaporans.Add(new JenisLaporan { JenisLaporanID = 1, Laporan = "Laporan Stok" });
            jenisLaporans.Add(new JenisLaporan { JenisLaporanID = 2, Laporan = "Isian Stok Opname" });

            int InitDivisiId = 0;
            if (db.Divisis.Count() > 0)
            {
                InitDivisiId = db.Divisis.OrderBy(x => x.DivisiId).First().DivisiId;
            }
            else
            {
                InitDivisiId = 0;
            };

            ViewBag.JenisLaporanID = new SelectList(jenisLaporans, "JenisLaporanID", "Laporan");
            ViewBag.DivisiID = new SelectList(db.Divisis, "DivisiId", "Keterangan");
            ViewBag.DepartemenID = new SelectList(db.Departemen.Where(x => x.DivisiId == InitDivisiId), "DepartemenId", "Keterangan");

            return View(rptStokParam);
        }

        public ActionResult LaporanStok(int DivisiID, int DepartemenID, int JenisLaporanId)
        {
            List<Kramatdjati.Models.rptStok> stokbarangs = new List<Kramatdjati.Models.rptStok>();
            List<Kramatdjati.Models.BahanBaku> bahanbakus = new List<Kramatdjati.Models.BahanBaku>();

            using (Kramatdjati.Infrastructure.AppIdentityDbContext db = new Kramatdjati.Infrastructure.AppIdentityDbContext())
            {
                if (DivisiID == 0)
                {
                    bahanbakus = db.BahanBakus.ToList();
                }
                else
                {
                    bahanbakus = db.BahanBakus.Where(x=>x.DivisiId == DivisiID && x.DepartemenId == DepartemenID).ToList();
                }

                foreach (Kramatdjati.Models.BahanBaku rw in bahanbakus)
                {
                    Kramatdjati.Models.rptStok stokbarang = new Kramatdjati.Models.rptStok();
                    stokbarang.Departemen = rw.Departemen.Keterangan;
                    stokbarang.Divisi = rw.Divisi.Keterangan;
                    stokbarang.HargaJual = rw.HargaJual;
                    stokbarang.HargaRata2 = rw.HargaRata2;
                    stokbarang.HargaTerakhir = rw.HargaTerakhir;
                    stokbarang.KodeBahanBaku = rw.KodeBahanBaku;
                    stokbarang.NamaBarang = rw.Keterangan;
                    stokbarang.Satuan = rw.satuan.Keterangan;
                    stokbarang.Stok = rw.Stok;

                    stokbarangs.Add(stokbarang);
                };
            };
            ReportDataSource rds = new ReportDataSource("StokBarang", stokbarangs);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.AsyncRendering = false;
            reportViewer.ZoomMode = ZoomMode.PageWidth;
            reportViewer.SizeToReportContent = true;
            reportViewer.ProcessingMode = ProcessingMode.Local;

            if (JenisLaporanId == 1)
            {
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"RPTReports\rptdaftarBarang.rdlc";
            }
            if (JenisLaporanId == 2)
            {
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"RPTReports\rptdaftarBarangIsian.rdlc";
            }

            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.LocalReport.Refresh();

            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}