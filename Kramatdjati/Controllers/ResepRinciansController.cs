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
    public class ResepRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: ResepRincians
        public ActionResult Index(int id)
        {
            var resep = db.Reseps.Where(x => x.ResepID == id).FirstOrDefault() ;

            ViewBag.ResepID = id;
            ViewBag.KodeResep = resep.KodeResep;
            ViewBag.Keterangan = resep.Keterangan;
            ViewBag.TglBuat = resep.TglBuat.ToShortDateString();

            var resepRincians = db.ResepRincians.Where(x=>x.ResepID == id).Include(r => r.bahanBaku).Include(r => r.resep);
            return View(resepRincians.ToList());
        }

        // GET: ResepRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResepRincian resepRincian = db.ResepRincians.Find(id);
            if (resepRincian == null)
            {
                return HttpNotFound();
            }
            return View(resepRincian);
        }

        // GET: ResepRincians/Create
        public ActionResult Create(int id)
        {
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus.Where(x => x.BarangJadi == false).OrderBy(x => x.KodeBahanBaku), "BahanBakuID", "KodeBahanBaku");
            ViewBag.ResepID = id;
            return View();
        }

        // POST: ResepRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResepRincianID,ResepID,BahanBakuID,Jumlah")] ResepRincian resepRincian)
        {
            if (ModelState.IsValid)
            {
                db.ResepRincians.Add(resepRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = resepRincian.ResepID  });
            }

            ViewBag.BahanBakuID = new SelectList(db.BahanBakus.Where(x => x.BarangJadi == false).OrderBy(x => x.KodeBahanBaku), "BahanBakuID", "KodeBahanBaku", resepRincian.BahanBakuID);
            ViewBag.ResepID =  resepRincian.ResepID;
            return View(resepRincian);
        }

        // GET: ResepRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResepRincian resepRincian = db.ResepRincians.Find(id);
            if (resepRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus.Where(x => x.BarangJadi == false).OrderBy(x => x.KodeBahanBaku), "BahanBakuID", "KodeBahanBaku", resepRincian.BahanBakuID);
            ViewBag.ResepID = resepRincian.ResepID  ;
            return View(resepRincian);
        }

        // POST: ResepRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResepRincianID,ResepID,BahanBakuID,Jumlah")] ResepRincian resepRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resepRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = resepRincian.ResepID });
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus.Where(x => x.BarangJadi == false).OrderBy(x => x.KodeBahanBaku), "BahanBakuID", "KodeBahanBaku", resepRincian.BahanBakuID);
            ViewBag.ResepID = resepRincian.ResepID;
            return View(resepRincian);
        }

        // GET: ResepRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResepRincian resepRincian = db.ResepRincians.Find(id);
            if (resepRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResepID = resepRincian.ResepID;
            return View(resepRincian);
        }

        // POST: ResepRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResepRincian resepRincian = db.ResepRincians.Find(id);
            db.ResepRincians.Remove(resepRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = resepRincian.ResepID });
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
