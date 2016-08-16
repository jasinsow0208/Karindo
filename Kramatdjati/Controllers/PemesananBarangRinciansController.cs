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
    public class PemesananBarangRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PemesananBarangRincians
        public ActionResult Index(int id)
        {
            var pemesananBarang = db.PemesananBarangs.Where(x => x.PemesananBarangID == id).Include(p=>p.contact).FirstOrDefault();

            ViewBag.TglKirim = pemesananBarang.TglPengiriman.ToShortDateString ();
            ViewBag.TglTransaksi = pemesananBarang.TglPesan.ToShortDateString ();
            ViewBag.NoPO = pemesananBarang.NoPemesananBarang.ToString();
            ViewBag.Supplier = pemesananBarang.contact.Perusahaan;
            ViewBag.PemesananBarangID = id;
            ViewBag.Catatan = pemesananBarang.Catatan;
            ViewBag.MataUang = pemesananBarang.mataUang.Kode;
            var pemesananBarangRincians = db.PemesananBarangRincians.Where(x=>x.PemesananBarangID ==id).Include(p => p.bahanbaku).Include(p => p.pemesananBarang);
            return View(pemesananBarangRincians.ToList());
        }

        // GET: PemesananBarangRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarangRincian pemesananBarangRincian = db.PemesananBarangRincians.Find(id);
            if (pemesananBarangRincian == null)
            {
                return HttpNotFound();
            }
            return View(pemesananBarangRincian);
        }

        // GET: PemesananBarangRincians/Create
        public ActionResult Create(int id)
        {
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus.Where(x=>x.BarangJadi==false).OrderBy(x=>x.KodeBahanBaku), "BahanBakuID", "KodeBahanBaku");
            ViewBag.PemesananBarangID =id;
            return View();
        }

        // POST: PemesananBarangRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PemesananBarangRincianID,PemesananBarangID,BahanBakuID,Jumlah,HargaSatuan,JmlYangSudahDiterima,statusLengkap")] PemesananBarangRincian pemesananBarangRincian)
        {
            if (ModelState.IsValid)
            {
                db.PemesananBarangRincians.Add(pemesananBarangRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = pemesananBarangRincian.PemesananBarangID });
            }

            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", pemesananBarangRincian.BahanBakuID);
            ViewBag.PemesananBarangID = pemesananBarangRincian.PemesananBarangID ;
            return View(pemesananBarangRincian);
        }

        // GET: PemesananBarangRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarangRincian pemesananBarangRincian = db.PemesananBarangRincians.Find(id);
            if (pemesananBarangRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", pemesananBarangRincian.BahanBakuID);
            ViewBag.PemesananBarangID = pemesananBarangRincian.PemesananBarangID ;
            return View(pemesananBarangRincian);
        }

        // POST: PemesananBarangRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PemesananBarangRincianID,PemesananBarangID,BahanBakuID,Jumlah,HargaSatuan,JmlYangSudahDiterima,statusLengkap")] PemesananBarangRincian pemesananBarangRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pemesananBarangRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = pemesananBarangRincian.PemesananBarangID });
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", pemesananBarangRincian.BahanBakuID);
            ViewBag.PemesananBarangID =pemesananBarangRincian.PemesananBarangID;
            return View(pemesananBarangRincian);
        }

        // GET: PemesananBarangRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarangRincian pemesananBarangRincian = db.PemesananBarangRincians.Find(id);
            if (pemesananBarangRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.PemesananBarangID = pemesananBarangRincian.PemesananBarangID;
            return View(pemesananBarangRincian);
        }

        // POST: PemesananBarangRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PemesananBarangRincian pemesananBarangRincian = db.PemesananBarangRincians.Find(id);
            db.PemesananBarangRincians.Remove(pemesananBarangRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = pemesananBarangRincian.PemesananBarangID });
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
