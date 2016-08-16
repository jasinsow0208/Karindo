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
    public class PersiapanPembayaranController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PersiapanPembayaran
        public ActionResult Index()
        {
            var pemesananBarangs = db.PemesananBarangs.Where(x=>x.Closed == false).Include(p => p.contact);
            return View(pemesananBarangs.ToList());
        }

        // GET: PersiapanPembayaran/Details/5
        public ActionResult Details(int? id)
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

        // GET: InformasiPemesananBarangs/Delete/5
        public ActionResult Closing(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Closing(int id)
        {
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            pemesananBarang.Closed = true;
            db.Entry(pemesananBarang).State = EntityState.Modified;
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
