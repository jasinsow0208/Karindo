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
    public class InformasiPemesananBarangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: InformasiPemesananBarangs
        public ActionResult Index()
        {
            var pemesananBarangs = db.PemesananBarangs.Where(x=>x.Closed == false && x.Posting == true).Include(p => p.contact);
            return View(pemesananBarangs.ToList());
        }

        public ActionResult Cetak(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pemesananBarang = db.PemesananBarangs.Where(x => x.PemesananBarangID == id).Include(p => p.contact).FirstOrDefault();

            ViewBag.TglKirim = pemesananBarang.TglPengiriman.ToShortDateString();
            ViewBag.TglTransaksi = pemesananBarang.TglPesan.ToShortDateString();
            ViewBag.NoPO = pemesananBarang.NoPemesananBarang.ToString();
            ViewBag.Supplier = pemesananBarang.contact.Perusahaan;
            ViewBag.PemesananBarangID = id;
            ViewBag.Catatan = pemesananBarang.Catatan;
            var pemesananBarangRincians = db.PemesananBarangRincians.Where(x => x.PemesananBarangID == id).Include(p => p.bahanbaku).Include(p => p.pemesananBarang);

            return View(pemesananBarangRincians.ToList());

        }
        // GET: InformasiPemesananBarangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            if (pemesananBarang == null)
            {
                return HttpNotFound();
            }
            return View(pemesananBarang);
        }

        // GET: InformasiPemesananBarangs/Create
        public ActionResult Create()
        {
            ViewBag.ContactID = new SelectList(db.Contacts, "ContactID", "Perusahaan");
            return View();
        }

        // POST: InformasiPemesananBarangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PemesananBarangID,NoPemesananBarang,TglPesan,ContactID,TglPengiriman,User,Catatan,Posting,Closed")] PemesananBarang pemesananBarang)
        {
            if (ModelState.IsValid)
            {
                db.PemesananBarangs.Add(pemesananBarang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactID = new SelectList(db.Contacts, "ContactID", "Perusahaan", pemesananBarang.ContactID);
            return View(pemesananBarang);
        }

        // GET: InformasiPemesananBarangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            if (pemesananBarang == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactID = new SelectList(db.Contacts, "ContactID", "Perusahaan", pemesananBarang.ContactID);
            return View(pemesananBarang);
        }

        // POST: InformasiPemesananBarangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PemesananBarangID,NoPemesananBarang,TglPesan,ContactID,TglPengiriman,User,Catatan,Posting,Closed")] PemesananBarang pemesananBarang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pemesananBarang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactID = new SelectList(db.Contacts, "ContactID", "Perusahaan", pemesananBarang.ContactID);
            return View(pemesananBarang);
        }

        // GET: InformasiPemesananBarangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            if (pemesananBarang == null)
            {
                return HttpNotFound();
            }
            return View(pemesananBarang);
        }

        // POST: InformasiPemesananBarangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            db.PemesananBarangs.Remove(pemesananBarang);
            db.SaveChanges();
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
