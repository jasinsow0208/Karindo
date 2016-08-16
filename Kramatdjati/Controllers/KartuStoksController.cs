using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kramatdjati.Infrastructure;
using Kramatdjati.Models;

namespace Kramatdjati.Controllers
{
    public class KartuStoksController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: KartuStoks
        public ActionResult Index(int id, int? GudangID)
        {
            if (GudangID == null)
            {
                GudangID = 0;
            }

            BahanBaku bahanBaku = db.BahanBakus.Find(id);

            var kartuStoks =  db.KartuStoks.Where(x => x.gudangBahanBaku.GudangID == GudangID && x.gudangBahanBaku.BahanBakuID == id).OrderBy(x => x.TglKomputer);

            List<ViewKartuStok> viewKartuStoks = new List<ViewKartuStok>();
            foreach (KartuStok rw in kartuStoks)
            {
                ViewKartuStok viewKartuStok = new ViewKartuStok();
                viewKartuStok.GudangID = rw.gudangBahanBaku.GudangID;
                viewKartuStok.KodeBarang = bahanBaku.KodeBahanBaku ;
                viewKartuStok.Masuk = rw.Masuk;
                viewKartuStok.Keluar = rw.Keluar;
                viewKartuStok.Keterangan  = rw.Keterangan;
                viewKartuStok.TglKomputer = rw.TglKomputer;
                viewKartuStok.Source = rw.Source;
                viewKartuStok.SourceID = rw.SourceID;
                viewKartuStok.Saldo = rw.Saldo;
                viewKartuStok.BahanBakuID = id;

                viewKartuStoks.Add(viewKartuStok);
            }

            string stok = bahanBaku.Stok.ToString()  + " "  + bahanBaku.satuan.Keterangan;
 
            ViewBag.BahanBakuID = id;
            ViewBag.KodeBarang = bahanBaku.KodeBahanBaku;
            ViewBag.NamaBarang = bahanBaku.Keterangan;
            ViewBag.Stok = stok;
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi");
            return View(viewKartuStoks );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "TglKomputer, Keterangan, Masuk, Keluar, Saldo, Source, GudangID, BahanBakuID")] ViewKartuStok kartuStok)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { id = kartuStok.BahanBakuID , GudangID=kartuStok.GudangID });
            }
            return RedirectToAction("Index");
        }
    

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
