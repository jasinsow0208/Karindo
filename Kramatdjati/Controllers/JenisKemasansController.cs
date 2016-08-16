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
    public class JenisKemasansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JenisKemasans
        public ActionResult Index(int id)
        {
            var jenisKemasans = db.JenisKemasans.Where (x=>x.BahanBakuID ==id).Include(j => j.BahanBaku);
            var tblBahanBaku=db.BahanBakus .Where(x=>x.BahanBakuID ==id).SingleOrDefault ();

            ViewBag.BahanBakuID = tblBahanBaku.BahanBakuID;
            ViewBag.BahanBaku = string.Format("{0} ({1})", tblBahanBaku.Keterangan, tblBahanBaku.KodeBahanBaku);
            
            return View(jenisKemasans.ToList());
        }

        // GET: JenisKemasans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisKemasan jenisKemasan = db.JenisKemasans.Find(id);
            if (jenisKemasan == null)
            {
                return HttpNotFound();
            }
            return View(jenisKemasan);
        }

        // GET: JenisKemasans/Create
        public ActionResult Create(int id, string BahanBaku)
        {
            ViewBag.BahanBakuID = id;
            ViewBag.BahanBaku = BahanBaku;
            BahanBaku rwBahanBaku = db.BahanBakus.Find(id);
            ViewBag.Satuan = rwBahanBaku.satuan.Keterangan;
            
            return View();
        }

        // POST: JenisKemasans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JenisKemasanID,BahanBakuID,Keterangan,Berat, Default")] JenisKemasan jenisKemasan)
        {
            if (ModelState.IsValid)
            {
                db.JenisKemasans.Add(jenisKemasan);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jenisKemasan.BahanBakuID });
            }

            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", jenisKemasan.BahanBakuID);
            return View(jenisKemasan);
        }

        // GET: JenisKemasans/Edit/5
        public ActionResult Edit(int? id, string BahanBaku)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisKemasan jenisKemasan = db.JenisKemasans.Find(id);
            if (jenisKemasan == null)
            {
                return HttpNotFound();
            }
            ViewBag.BahanBakuID = jenisKemasan.BahanBakuID;
            ViewBag.BahanBaku = BahanBaku;

            return View(jenisKemasan);
        }

        // POST: JenisKemasans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JenisKemasanID,BahanBakuID,Keterangan,Berat, Default")] JenisKemasan jenisKemasan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenisKemasan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jenisKemasan.BahanBakuID });
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", jenisKemasan.BahanBakuID);
            //ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan", jenisKemasan.SatuanID );
            return View(jenisKemasan);
        }

        // GET: JenisKemasans/Delete/5
        public ActionResult Delete(int? id, string BahanBaku)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisKemasan jenisKemasan = db.JenisKemasans.Find(id);
            if (jenisKemasan == null)
            {
                return HttpNotFound();
            }
            ViewBag.BahanBaku = BahanBaku;
            return View(jenisKemasan);
        }

        // POST: JenisKemasans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JenisKemasan jenisKemasan = db.JenisKemasans.Find(id);
            int BahanBakuID = jenisKemasan.BahanBakuID;
            db.JenisKemasans.Remove(jenisKemasan);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = BahanBakuID });
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
