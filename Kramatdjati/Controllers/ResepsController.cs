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
    public class ResepsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Reseps
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };

            return View(db.Reseps.Where(x => x.Posting == Posting).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ResepID,KodeResep,Keterangan,TglBuat, Posting")] Resep resep)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = resep.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: Reseps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resep resep = db.Reseps.Find(id);
            if (resep == null)
            {
                return HttpNotFound();
            }
            return View(resep);
        }

        // GET: Reseps/Details/5
        public ActionResult DetailBukaPosting(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resep resep = db.Reseps.Find(id);
            if (resep == null)
            {
                return HttpNotFound();
            }
            return View(resep);
        }

        public ActionResult Cetak(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resep = db.Reseps.Where(x => x.ResepID == id).FirstOrDefault();

            ViewBag.ResepID = id;
            ViewBag.KodeResep = resep.KodeResep;
            ViewBag.Keterangan = resep.Keterangan;
            ViewBag.TglBuat = resep.TglBuat.ToShortDateString();

            var resepRincians = db.ResepRincians.Where(x => x.ResepID == id).Include(r => r.bahanBaku).Include(r => r.resep);
            return View(resepRincians.ToList());

        }


        // GET: Reseps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reseps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResepID,KodeResep,Keterangan,TglBuat")] Resep resep)
        {
            if (ModelState.IsValid)
            {
                db.Reseps.Add(resep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resep);
        }

        // GET: Reseps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resep resep = db.Reseps.Find(id);
            if (resep == null)
            {
                return HttpNotFound();
            }
            return View(resep);
        }

        // POST: Reseps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResepID,KodeResep,Keterangan,TglBuat")] Resep resep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resep);
        }

        // GET: Reseps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resep resep = db.Reseps.Find(id);
            if (resep == null)
            {
                return HttpNotFound();
            }
            return View(resep);
        }

        // POST: Reseps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resep resep = db.Reseps.Find(id);
            db.Reseps.Remove(resep);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {
            Resep resep = db.Reseps.Find(id);
            if (resep == null)
            {
                return HttpNotFound();
            }

            tblDefault Default = db.tblDefaults.First();

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    resep.Posting = true;
                    db.Entry(resep).State = EntityState.Modified;
                    db.SaveChanges();

                    transaction.Commit();

                    ViewBag.Info = "Posting Berhasil";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.Info = "Posting Tidak berhasil " + e.Message.Trim();

                }
            }
            return View("Informasi");
        }

        public ActionResult BukaPosting(int id)
        {
            Resep resep = db.Reseps.Find(id);
            if (resep == null)
            {
                return HttpNotFound();
            }

            tblDefault Default = db.tblDefaults.First();

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    resep.Posting = false;
                    db.Entry(resep).State = EntityState.Modified;
                    db.SaveChanges();

                    transaction.Commit();

                    ViewBag.Info = "Buka Posting Berhasil";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.Info = "Buka Posting Tidak berhasil " + e.Message.Trim();

                }
            }
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
