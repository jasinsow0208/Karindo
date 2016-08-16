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
    public class PenerimaanBarangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PenerimaanBarangs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            var penerimaanBarangs = db.PenerimaanBarangs.Where(x=>x.Posting ==Posting).Include(p => p.pemesananBarang);
            return View(penerimaanBarangs.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PenerimaanBarangID,PemesananBarangID,NoSuratJalan,tglSuratJalan,Posting,TglPosting")] PenerimaanBarang penerimaanBarang)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = penerimaanBarang.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: PenerimaanBarangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenerimaanBarang penerimaanBarang = db.PenerimaanBarangs.Find(id);
            if (penerimaanBarang == null)
            {
                return HttpNotFound();
            }
            return View(penerimaanBarang);
        }

        // GET: PenerimaanBarangs/Create
        public ActionResult Create()
        {
            ViewBag.PemesananBarangID = new SelectList(db.PemesananBarangs.Where(x=>x.Posting==true && x.Closed!=true), "PemesananBarangID", "NoPemesananBarang");
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x=>x.Lokasi), "GudangID", "Lokasi");

            return View();
        }

        // POST: PenerimaanBarangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PenerimaanBarangID,PemesananBarangID,NoSuratJalan,tglSuratJalan,Posting,TglPosting, TglTransaksi, GudangID")] PenerimaanBarang penerimaanBarang)
        {
            if (ModelState.IsValid)
            {
                penerimaanBarang.TglPosting = DateTime.Parse("2001-01-01");
                db.PenerimaanBarangs.Add(penerimaanBarang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PemesananBarangID = new SelectList(db.PemesananBarangs.Where(x => x.Posting == true && x.Closed != true), "PemesananBarangID", "NoPemesananBarang", penerimaanBarang.PemesananBarangID);
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi", penerimaanBarang.GudangID);

            return View(penerimaanBarang);
        }

        // GET: PenerimaanBarangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenerimaanBarang penerimaanBarang = db.PenerimaanBarangs.Find(id);
            if (penerimaanBarang == null)
            {
                return HttpNotFound();
            }
            ViewBag.PemesananBarangID = new SelectList(db.PemesananBarangs.Where(x => x.Posting == true && x.Closed != true), "PemesananBarangID", "NoPemesananBarang", penerimaanBarang.PemesananBarangID);
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi", penerimaanBarang.GudangID );

            return View(penerimaanBarang);
        }

        // POST: PenerimaanBarangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PenerimaanBarangID,PemesananBarangID,NoSuratJalan,tglSuratJalan,Posting,TglPosting, TglTransaksi")] PenerimaanBarang penerimaanBarang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penerimaanBarang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PemesananBarangID = new SelectList(db.PemesananBarangs.Where(x => x.Posting == true && x.Closed != true), "PemesananBarangID", "NoPemesananBarang", penerimaanBarang.PemesananBarangID);
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi", penerimaanBarang.GudangID);

            return View(penerimaanBarang);
        }

        // GET: PenerimaanBarangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenerimaanBarang penerimaanBarang = db.PenerimaanBarangs.Find(id);
            if (penerimaanBarang == null)
            {
                return HttpNotFound();
            }
            return View(penerimaanBarang);
        }

        // POST: PenerimaanBarangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PenerimaanBarang penerimaanBarang = db.PenerimaanBarangs.Find(id);
            db.PenerimaanBarangs.Remove(penerimaanBarang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cetak(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PenerimaanBarang penerimaanBarang = db.PenerimaanBarangs.Where(x => x.PenerimaanBarangID == Id).Include(p => p.pemesananBarang).Include(q => q.pemesananBarang.contact).FirstOrDefault();

            ViewBag.NoPO = penerimaanBarang.pemesananBarang.NoPemesananBarang;
            ViewBag.TglTransaksi = penerimaanBarang.TglTransaksi.ToShortDateString();
            ViewBag.NoSuratJalan = penerimaanBarang.NoSuratJalan;
            ViewBag.TglSuratJalan = penerimaanBarang.tglSuratJalan.ToShortDateString();
            ViewBag.Perusahaan = penerimaanBarang.pemesananBarang.contact.Perusahaan;
            ViewBag.User = penerimaanBarang.User;

            ViewBag.PenerimaanBarangID = Id;
            var penerimaanBarangRincians = db.PenerimaanBarangRincians.Where(x => x.PenerimaanBarangID == Id).Include(p => p.pemesananBarangRincian).Include(p => p.penerimaanBarang);
            return View(penerimaanBarangRincians.ToList());
        }

        public ActionResult Posting(int id)
        {
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spPenerimaanBarangPosting @PenerimaanBarangID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@PenerimaanBarangID", id),
                                                        new SqlParameter("@User", User.Identity.Name),
                                                        ReturnParameter );

            if (ReturnParameter.Value.ToString() == "0")
            {
                ViewBag.Info = "Posting Berhasil";
            }
            else
            {
                ViewBag.Info = string.Format("Posting Gagal. error: {0}", ReturnParameter.Value);
            };

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
