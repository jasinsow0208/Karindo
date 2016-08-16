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
     [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDBahan")]
    public class BahansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Bahans
        public ActionResult Index()
        {
            return View(db.Bahans.ToList());
        }

        // GET: Bahans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bahan bahan = db.Bahans.Find(id);
            if (bahan == null)
            {
                return HttpNotFound();
            }
            return View(bahan);
        }

        // GET: Bahans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bahans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BahanID,Keterangan")] Bahan bahan)
        {
            if (ModelState.IsValid)
            {
                db.Bahans.Add(bahan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bahan);
        }

        // GET: Bahans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bahan bahan = db.Bahans.Find(id);
            if (bahan == null)
            {
                return HttpNotFound();
            }
            return View(bahan);
        }

        // POST: Bahans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BahanID,Keterangan")] Bahan bahan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bahan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bahan);
        }

        // GET: Bahans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bahan bahan = db.Bahans.Find(id);
            if (bahan == null)
            {
                return HttpNotFound();
            }
            return View(bahan);
        }

        // POST: Bahans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bahan bahan = db.Bahans.Find(id);
            db.Bahans.Remove(bahan);
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
