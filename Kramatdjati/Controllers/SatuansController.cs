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
     [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDSatuan")]
    public class SatuansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Satuans
        public ActionResult Index()
        {
            return View(db.Satuan.ToList());
        }

        // GET: Satuans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Satuan satuan = db.Satuan.Find(id);
            if (satuan == null)
            {
                return HttpNotFound();
            }
            return View(satuan);
        }

        // GET: Satuans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Satuans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SatuanID,Keterangan")] Satuan satuan)
        {
            if (ModelState.IsValid)
            {
                db.Satuan.Add(satuan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(satuan);
        }

        // GET: Satuans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Satuan satuan = db.Satuan.Find(id);
            if (satuan == null)
            {
                return HttpNotFound();
            }
            return View(satuan);
        }

        // POST: Satuans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SatuanID,Keterangan")] Satuan satuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(satuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(satuan);
        }

        // GET: Satuans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Satuan satuan = db.Satuan.Find(id);
            if (satuan == null)
            {
                return HttpNotFound();
            }
            return View(satuan);
        }

        // POST: Satuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Satuan satuan = db.Satuan.Find(id);
            db.Satuan.Remove(satuan);
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
