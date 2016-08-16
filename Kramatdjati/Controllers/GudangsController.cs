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
    public class GudangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Gudangs
        public ActionResult Index()
        {
            return View(db.Gudangs.ToList());
        }

        // GET: Gudangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gudang gudang = db.Gudangs.Find(id);
            if (gudang == null)
            {
                return HttpNotFound();
            }
            return View(gudang);
        }

        // GET: Gudangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gudangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GudangID,Lokasi")] Gudang gudang)
        {
            if (ModelState.IsValid)
            {
                db.Gudangs.Add(gudang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gudang);
        }

        // GET: Gudangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gudang gudang = db.Gudangs.Find(id);
            if (gudang == null)
            {
                return HttpNotFound();
            }
            return View(gudang);
        }

        // POST: Gudangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GudangID,Lokasi")] Gudang gudang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gudang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gudang);
        }

        // GET: Gudangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gudang gudang = db.Gudangs.Find(id);
            if (gudang == null)
            {
                return HttpNotFound();
            }
            return View(gudang);
        }

        // POST: Gudangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gudang gudang = db.Gudangs.Find(id);
            db.Gudangs.Remove(gudang);
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
