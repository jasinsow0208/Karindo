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
    public class MataUangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: MataUangs
        public ActionResult Index()
        {
            return View(db.MataUangs.ToList());
        }

        // GET: MataUangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MataUang mataUang = db.MataUangs.Find(id);
            if (mataUang == null)
            {
                return HttpNotFound();
            }
            return View(mataUang);
        }

        // GET: MataUangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MataUangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MataUangID,Kode,Keterangan")] MataUang mataUang)
        {
            if (ModelState.IsValid)
            {
                db.MataUangs.Add(mataUang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mataUang);
        }

        // GET: MataUangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MataUang mataUang = db.MataUangs.Find(id);
            if (mataUang == null)
            {
                return HttpNotFound();
            }
            return View(mataUang);
        }

        // POST: MataUangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MataUangID,Kode,Keterangan")] MataUang mataUang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mataUang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mataUang);
        }

        // GET: MataUangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MataUang mataUang = db.MataUangs.Find(id);
            if (mataUang == null)
            {
                return HttpNotFound();
            }
            return View(mataUang);
        }

        // POST: MataUangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MataUang mataUang = db.MataUangs.Find(id);
            db.MataUangs.Remove(mataUang);
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
