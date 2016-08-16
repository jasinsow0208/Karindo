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
using System.Data.SqlClient;

namespace Kramatdjati.Controllers
{
    public class GudangBahanBakusController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: GudangBahanBakus
        public ActionResult Index(int id)
        {
            ViewBag.GudangID = id;
            ViewBag.Gudang = db.Gudangs.Where(x => x.GudangID == id).FirstOrDefault().Lokasi;
            var gudangBahanBakus = db.GudangBahanBakus.Where(x => x.GudangID == id).Include(g => g.bahanBaku).Include(g => g.gudang);
            return View(gudangBahanBakus.ToList());
        }

        // GET: GudangBahanBakus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GudangBahanBaku gudangBahanBaku = db.GudangBahanBakus.Find(id);
            if (gudangBahanBaku == null)
            {
                return HttpNotFound();
            }
            return View(gudangBahanBaku);
        }

        // GET: GudangBahanBakus/Create
        public ActionResult Create(int id)
        {
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus.OrderBy(x=>x.KodeBahanBaku), "BahanBakuID", "KodeBahanBaku");
            ViewBag.Gudang = db.Gudangs.Where(x => x.GudangID == id).FirstOrDefault().Lokasi;
            ViewBag.GudangID = id;
            return View();
        }

        // POST: GudangBahanBakus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GudangBahanBakuID,GudangID,BahanBakuID,Jumlah")] GudangBahanBaku gudangBahanBaku)
        {
            if (ModelState.IsValid)
            {
                db.GudangBahanBakus.Add(gudangBahanBaku);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = gudangBahanBaku.GudangID });
            }

            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", gudangBahanBaku.BahanBakuID);
            ViewBag.Gudang = db.Gudangs.Where(x => x.GudangID == gudangBahanBaku.BahanBakuID).FirstOrDefault().Lokasi;
            ViewBag.GudangID = gudangBahanBaku.GudangID;
            return View(gudangBahanBaku);
        }

        // GET: GudangBahanBakus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GudangBahanBaku gudangBahanBaku = db.GudangBahanBakus.Find(id);
            if (gudangBahanBaku == null)
            {
                return HttpNotFound();
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", gudangBahanBaku.BahanBakuID);
            ViewBag.Gudang = db.Gudangs.Where(x => x.GudangID == gudangBahanBaku.GudangID).FirstOrDefault().Lokasi;
            ViewBag.GudangID = gudangBahanBaku.GudangID;
            return View(gudangBahanBaku);
        }

        // POST: GudangBahanBakus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GudangBahanBakuID,GudangID,BahanBakuID,Jumlah")] GudangBahanBaku gudangBahanBaku)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gudangBahanBaku).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = gudangBahanBaku.GudangID });
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", gudangBahanBaku.BahanBakuID);
            ViewBag.Gudang = db.Gudangs.Where(x => x.GudangID == gudangBahanBaku.GudangID).FirstOrDefault().Lokasi;
            ViewBag.GudangID = gudangBahanBaku.GudangID;
            return View(gudangBahanBaku);
        }

        // GET: GudangBahanBakus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GudangBahanBaku gudangBahanBaku = db.GudangBahanBakus.Find(id);
            if (gudangBahanBaku == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gudang = db.Gudangs.Where(x => x.GudangID == gudangBahanBaku.GudangID).FirstOrDefault().Lokasi;
            ViewBag.GudangID = gudangBahanBaku.GudangID;
            ViewBag.GudangBahanBakuID = id;

            return View(gudangBahanBaku);
        }

        // POST: GudangBahanBakus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GudangBahanBaku gudangBahanBaku = db.GudangBahanBakus.Find(id);
            if (gudangBahanBaku.Jumlah > 0)
            {
                db.GudangBahanBakus.Remove(gudangBahanBaku);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { id = gudangBahanBaku.GudangID });
        }

        public ActionResult KalkulasiKartuStok(int id)
        {
            var gudangBahanBaku = db.GudangBahanBakus.Find(id);
            ViewBag.GudangBahanBakuID = id ;

            var Data = db.Database.ExecuteSqlCommand("spKalkulasiSaldoKartuStok @GudangBahanBakuID",
                                                        new SqlParameter("@GudangBahanBakuID", id));
 
            ViewBag.Info = "Kalkulasi Kartu Stok Berhasil";
            return View("Informasi");
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
