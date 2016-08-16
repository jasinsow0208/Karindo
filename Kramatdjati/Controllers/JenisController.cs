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
     [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDJenis")]
    public class JenisController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Jenis
        public ActionResult Index()
        {
            return View(db.Jenis.ToList());
        }

        // GET: Jenis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jenis jenis = db.Jenis.Find(id);
            if (jenis == null)
            {
                return HttpNotFound();
            }
            return View(jenis);
        }

        // GET: Jenis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jenis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JenisID,KodeJenis,TglKomputer,Pembuat, Keterangan")] Jenis jenis)
        {
            if (ModelState.IsValid)
            {
                db.Jenis.Add(jenis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jenis);
        }

        // GET: Jenis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jenis jenis = db.Jenis.Find(id);
            if (jenis == null)
            {
                return HttpNotFound();
            }
            return View(jenis);
        }

        // POST: Jenis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JenisID,KodeJenis,TglKomputer,Pembuat, Keterangan")] Jenis jenis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jenis);
        }

        // GET: Jenis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jenis jenis = db.Jenis.Find(id);
            if (jenis == null)
            {
                return HttpNotFound();
            }
            return View(jenis);
        }

        // POST: Jenis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jenis jenis = db.Jenis.Find(id);
            db.Jenis.Remove(jenis);
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
