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
    public class PemakaianPengembalianBarangRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PemakaianPengembalianBarangRincians
        public ActionResult Index(int id)
        {
            var pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(id);

            ViewBag.NoSuratPemakaian = pemakaianPengembalianBarang.NoSuratPemakaian;
            ViewBag.Gudang = pemakaianPengembalianBarang.gudang.Lokasi;
            ViewBag.Tanggal = pemakaianPengembalianBarang.tglKeluarBarang.ToShortDateString() ;
            ViewBag.User = pemakaianPengembalianBarang.Penerima;
            ViewBag.PemakaianPengembalianBarangID = id;
            var pemakaianPengembalianBarangRincians = db.PemakaianPengembalianBarangRincians.Where(x=>x.PemakaianPengembalianBarangID ==id).Include(p => p.gudangBahaBaku).Include(p => p.pemakaianPengembalianBarang);
            return View(pemakaianPengembalianBarangRincians.ToList());
        }

        // GET: PemakaianPengembalianBarangRincians/Create
        public ActionResult Create(int id)
        {
            var pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(id);
            ViewBag.GudangBahanBakuID = new SelectList(db.GudangBahanBakus.Where(x=>x.GudangID==pemakaianPengembalianBarang.GudangID ).OrderBy(x=>x.bahanBaku.KodeBahanBaku ) , "GudangBahanBakuID", "bahanBaku.KodeBahanBaku");
            ViewBag.PemakaianPengembalianBarangID = id;
            return View();
        }

        // POST: PemakaianPengembalianBarangRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PemakaianPengembalianBarangRincianID,PemakaianPengembalianBarangID,GudangBahanBakuID,Jumlah")] PemakaianPengembalianBarangRincian pemakaianPengembalianBarangRincian)
        {
            if (ModelState.IsValid)
            {
                db.PemakaianPengembalianBarangRincians.Add(pemakaianPengembalianBarangRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID });
            }

            var pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID );

            ViewBag.GudangBahanBakuID = new SelectList(db.GudangBahanBakus.Where(x => x.GudangID == pemakaianPengembalianBarang.GudangID).OrderBy(x => x.bahanBaku.KodeBahanBaku), "GudangBahanBakuID", "bahanBaku.KodeBahanBaku", pemakaianPengembalianBarangRincian.GudangBahanBakuID );
            ViewBag.PemakaianPengembalianBarangID = pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID;
            return View(pemakaianPengembalianBarangRincian);
        }

        // GET: PemakaianPengembalianBarangRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemakaianPengembalianBarangRincian pemakaianPengembalianBarangRincian = db.PemakaianPengembalianBarangRincians.Find(id);
            if (pemakaianPengembalianBarangRincian == null)
            {
                return HttpNotFound();
            }

            var pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID);

            ViewBag.GudangBahanBakuID = new SelectList(db.GudangBahanBakus.Where(x => x.GudangID == pemakaianPengembalianBarang.GudangID).OrderBy(x => x.bahanBaku.KodeBahanBaku), "GudangBahanBakuID", "bahanBaku.KodeBahanBaku", pemakaianPengembalianBarangRincian.GudangBahanBakuID);
            ViewBag.PemakaianPengembalianBarangID = pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID;

            return View(pemakaianPengembalianBarangRincian);
        }

        // POST: PemakaianPengembalianBarangRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PemakaianPengembalianBarangRincianID,PemakaianPengembalianBarangID,GudangBahanBakuID,Jumlah")] PemakaianPengembalianBarangRincian pemakaianPengembalianBarangRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pemakaianPengembalianBarangRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID );
            }
            var pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID);

            ViewBag.GudangBahanBakuID = new SelectList(db.GudangBahanBakus.Where(x => x.GudangID == pemakaianPengembalianBarang.GudangID).OrderBy(x => x.bahanBaku.KodeBahanBaku), "GudangBahanBakuID", "bahanBaku.KodeBahanBaku", pemakaianPengembalianBarangRincian.GudangBahanBakuID);
            ViewBag.PemakaianPengembalianBarangID = pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID;
            return View(pemakaianPengembalianBarangRincian);
        }

        // GET: PemakaianPengembalianBarangRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemakaianPengembalianBarangRincian pemakaianPengembalianBarangRincian = db.PemakaianPengembalianBarangRincians.Find(id);
            if (pemakaianPengembalianBarangRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.PemakaianPengembalianBarangID = pemakaianPengembalianBarangRincian.PemakaianPengembalianBarangID;
            return View(pemakaianPengembalianBarangRincian);
        }

        // POST: PemakaianPengembalianBarangRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PemakaianPengembalianBarangRincian pemakaianPengembalianBarangRincian = db.PemakaianPengembalianBarangRincians.Find(id);
            db.PemakaianPengembalianBarangRincians.Remove(pemakaianPengembalianBarangRincian);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cetak(int id)
        {
            var pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(id);

            ViewBag.NoSuratPemakaian = pemakaianPengembalianBarang.NoSuratPemakaian;
            ViewBag.Gudang = pemakaianPengembalianBarang.gudang.Lokasi;
            ViewBag.Tanggal = pemakaianPengembalianBarang.tglKeluarBarang.ToShortDateString();
            ViewBag.User = pemakaianPengembalianBarang.Penerima;
            ViewBag.PemakaianPengembalianBarangID = id;
            var pemakaianPengembalianBarangRincians = db.PemakaianPengembalianBarangRincians.Where(x => x.PemakaianPengembalianBarangID == id).Include(p => p.gudangBahaBaku).Include(p => p.pemakaianPengembalianBarang);
            return View(pemakaianPengembalianBarangRincians.ToList());
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
