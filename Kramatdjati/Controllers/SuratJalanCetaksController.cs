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
    public class SuratJalanCetaksController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SuratJalanCetaks
        public ActionResult Index()
        {
            var suratJalanCetaks = db.SuratJalanCetaks.Include(s => s.suratJalan);
            return View(suratJalanCetaks.ToList());
        }

        // GET: SuratJalanCetaks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanCetak suratJalanCetak = db.SuratJalanCetaks.Find(id);
            if (suratJalanCetak == null)
            {
                return HttpNotFound();
            }
            return View(suratJalanCetak);
        }

        // GET: SuratJalanCetaks/Create
        public ActionResult Create()
        {
            ViewBag.SuratJalanID = new SelectList(db.SuratJalans, "SuratJalanID", "NoSuratJalan");
            return View();
        }

        // POST: SuratJalanCetaks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuratJalanCetakID,SuratJalanID")] SuratJalanCetak suratJalanCetak)
        {
            if (ModelState.IsValid)
            {
                db.SuratJalanCetaks.Add(suratJalanCetak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SuratJalanID = new SelectList(db.SuratJalans, "SuratJalanID", "NoSuratJalan", suratJalanCetak.SuratJalanID);
            return View(suratJalanCetak);
        }

        // GET: SuratJalanCetaks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanCetak suratJalanCetak = db.SuratJalanCetaks.Find(id);
            if (suratJalanCetak == null)
            {
                return HttpNotFound();
            }
            ViewBag.SuratJalanID = new SelectList(db.SuratJalans, "SuratJalanID", "NoSuratJalan", suratJalanCetak.SuratJalanID);
            return View(suratJalanCetak);
        }

        // POST: SuratJalanCetaks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuratJalanCetakID,SuratJalanID")] SuratJalanCetak suratJalanCetak)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suratJalanCetak).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SuratJalanID = new SelectList(db.SuratJalans, "SuratJalanID", "NoSuratJalan", suratJalanCetak.SuratJalanID);
            return View(suratJalanCetak);
        }

        // GET: SuratJalanCetaks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanCetak suratJalanCetak = db.SuratJalanCetaks.Find(id);
            if (suratJalanCetak == null)
            {
                return HttpNotFound();
            }
            return View(suratJalanCetak);
        }

        // POST: SuratJalanCetaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuratJalanCetak suratJalanCetak = db.SuratJalanCetaks.Find(id);
            db.SuratJalanCetaks.Remove(suratJalanCetak);
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
