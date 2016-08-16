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
    public class PenerimaanBarangRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PenerimaanBarangRincians
        public ActionResult Index(int Id)
        {
            PenerimaanBarang penerimaanBarang = db.PenerimaanBarangs.Where(x => x.PenerimaanBarangID == Id).Include(p => p.pemesananBarang).Include(q => q.pemesananBarang.contact).FirstOrDefault();

            ViewBag.NoPO = penerimaanBarang.pemesananBarang.NoPemesananBarang;
            ViewBag.TglTransaksi = penerimaanBarang.TglTransaksi.ToShortDateString();
            ViewBag.NoSuratJalan = penerimaanBarang.NoSuratJalan;
            ViewBag.TglSuratJalan = penerimaanBarang.tglSuratJalan.ToShortDateString();
            ViewBag.Perusahaan = penerimaanBarang.pemesananBarang.contact.Perusahaan;

            ViewBag.PenerimaanBarangID = Id;
            var penerimaanBarangRincians = db.PenerimaanBarangRincians.Where(x => x.PenerimaanBarangID == Id).Include(p => p.pemesananBarangRincian).Include(p => p.penerimaanBarang);
            return View(penerimaanBarangRincians.ToList());
        }

        // GET: PenerimaanBarangRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenerimaanBarangRincian penerimaanBarangRincian = db.PenerimaanBarangRincians.Find(id);
            if (penerimaanBarangRincian == null)
            {
                return HttpNotFound();
            }
            return View(penerimaanBarangRincian);
        }

        // GET: PenerimaanBarangRincians/Create
        public ActionResult Create(int Id)
        {
            ViewBag.PenerimaanBarangID = Id;
            ViewBag.PemesananBarangRincianID = slBahanBaku(Id,0);
            return View();
        }

        private SelectList slBahanBaku(int Id, int pemesananBarangRincianID)
        {
            int pemesananBarangID = db.PenerimaanBarangs.Find(Id).PemesananBarangID;

            var bahanBaku = from a in db.PemesananBarangRincians
                            where a.PemesananBarangID == pemesananBarangID
                            select new { pemesananBarangRincianID = a.PemesananBarangRincianID, KodeBarang = a.bahanbaku.KodeBahanBaku };

            if (pemesananBarangRincianID == 0)
            {
                return new SelectList(bahanBaku, "PemesananBarangRincianID", "KodeBarang");
            }
            else
            {
                return new SelectList(bahanBaku, "PemesananBarangRincianID", "KodeBarang", pemesananBarangRincianID);
            }
        }

        // POST: PenerimaanBarangRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PenerimaanBarangRincianID,PenerimaanBarangID,PemesananBarangRincianID,JumlahDiTerima")] PenerimaanBarangRincian penerimaanBarangRincian)
        {
            if (ModelState.IsValid)
            {
                db.PenerimaanBarangRincians.Add(penerimaanBarangRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = penerimaanBarangRincian.PenerimaanBarangID });
            }

            ViewBag.PemesananBarangRincianID = slBahanBaku(penerimaanBarangRincian.PenerimaanBarangID, penerimaanBarangRincian.PemesananBarangRincianID );
            ViewBag.PenerimaanBarangID = penerimaanBarangRincian.PenerimaanBarangID;
            return View(penerimaanBarangRincian);
        }

        // GET: PenerimaanBarangRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenerimaanBarangRincian penerimaanBarangRincian = db.PenerimaanBarangRincians.Find(id);
            if (penerimaanBarangRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.PemesananBarangRincianID = slBahanBaku(penerimaanBarangRincian.PenerimaanBarangID,penerimaanBarangRincian.PemesananBarangRincianID);
            ViewBag.PenerimaanBarangID = penerimaanBarangRincian.PenerimaanBarangID;
            return View(penerimaanBarangRincian);
        }

        // POST: PenerimaanBarangRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PenerimaanBarangRincianID,PenerimaanBarangID,PemesananBarangRincianID,JumlahDiTerima")] PenerimaanBarangRincian penerimaanBarangRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penerimaanBarangRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = penerimaanBarangRincian.PenerimaanBarangID });
            }
            ViewBag.PemesananBarangRincianID = slBahanBaku(penerimaanBarangRincian.PenerimaanBarangID,penerimaanBarangRincian.PemesananBarangRincianID);
            ViewBag.PenerimaanBarangID = penerimaanBarangRincian.PenerimaanBarangID;
            return View(penerimaanBarangRincian);
        }

        // GET: PenerimaanBarangRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenerimaanBarangRincian penerimaanBarangRincian = db.PenerimaanBarangRincians.Find(id);
            if (penerimaanBarangRincian == null)
            {
                return HttpNotFound();
            }
            return View(penerimaanBarangRincian);
        }

        // POST: PenerimaanBarangRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PenerimaanBarangRincian penerimaanBarangRincian = db.PenerimaanBarangRincians.Find(id);
            db.PenerimaanBarangRincians.Remove(penerimaanBarangRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = penerimaanBarangRincian.PenerimaanBarangID });
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
