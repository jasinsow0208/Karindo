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
    public class HargaJualsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: HargaJuals
        public ActionResult Index()
        {
            return View(db.HargaJuals.ToList());
        }

        // GET: HargaJuals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HargaJual hargaJual = db.HargaJuals.Find(id);
            if (hargaJual == null)
            {
                return HttpNotFound();
            }
            return View(hargaJual);
        }

        // GET: HargaJuals/Create
        public ActionResult Create()
        {
            var BarangJadi = db.BahanBakus.Where(x => x.BarangJadi == true).Select(x => new { x.KodeBarangJadi }).Distinct().OrderBy(x => x.KodeBarangJadi);
            ViewBag.JenisSpon = new SelectList(BarangJadi.ToList() , "KodeBarangJadi", "KodeBarangJadi");

            return View();
        }

        // POST: HargaJuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HargaJualID,TglBerlaku,JenisSpon,Harga,mm")] HargaJual hargaJual)
        {
            if (ModelState.IsValid)
            {
                db.HargaJuals.Add(hargaJual);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var BarangJadi = db.BahanBakus.Where(x => x.BarangJadi == true).Select(x => new { x.KodeBarangJadi }).Distinct().OrderBy(x => x.KodeBarangJadi);
            ViewBag.JenisSpon = new SelectList(BarangJadi.ToList(), "KodeBarangJadi", "KodeBarangJadi", hargaJual.JenisSpon);
            return View(hargaJual);
        }

        // GET: HargaJuals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HargaJual hargaJual = db.HargaJuals.Find(id);
            if (hargaJual == null)
            {
                return HttpNotFound();
            }
            var BarangJadi = db.BahanBakus.Where(x => x.BarangJadi == true).Select(x => new { x.KodeBarangJadi }).Distinct().OrderBy(x => x.KodeBarangJadi);
            ViewBag.JenisSpon = new SelectList(BarangJadi.ToList(), "KodeBarangJadi", "KodeBarangJadi", hargaJual.JenisSpon);

            return View(hargaJual);
        }

        // POST: HargaJuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HargaJualID,TglBerlaku,JenisSpon,Harga,mm")] HargaJual hargaJual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hargaJual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var BarangJadi = db.BahanBakus.Where(x => x.BarangJadi == true).Select(x => new { x.KodeBarangJadi }).Distinct().OrderBy(x=>x.KodeBarangJadi );
            ViewBag.JenisSpon = new SelectList(BarangJadi.ToList(), "KodeBarangJadi", "KodeBarangJadi", hargaJual.JenisSpon);

            return View(hargaJual);
        }

        // GET: HargaJuals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HargaJual hargaJual = db.HargaJuals.Find(id);
            if (hargaJual == null)
            {
                return HttpNotFound();
            }
            return View(hargaJual);
        }

        // POST: HargaJuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HargaJual hargaJual = db.HargaJuals.Find(id);
            db.HargaJuals.Remove(hargaJual);
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
