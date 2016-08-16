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
    public class DepartemenController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Departemen
        public ActionResult Index(int id)
        {
            ViewBag.Divisi = db.Divisis.Find(id).Keterangan;
            ViewBag.DivisiId = id;
            var departemen = db.Departemen.Where(x=>x.DivisiId==id).Include(d => d.Divisi);
            return View(departemen.ToList());
        }

     
        // GET: Departemen/Create
        public ActionResult Create(int id)
        {
            ViewBag.Divisi = db.Divisis.Find(id).Keterangan;
            ViewBag.DivisiId = id;
            return View();
        }

        // POST: Departemen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartemenID,DivisiId,Keterangan")] Departemen departemen)
        {
            if (ModelState.IsValid)
            {
                db.Departemen.Add(departemen);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = departemen.DivisiId });
            }

            ViewBag.DivisiId = departemen.DivisiId ;
            return View(departemen);
        }

        // GET: Departemen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departemen departemen = db.Departemen.Find(id);
            ViewBag.Divisi = db.Divisis.Find(departemen.DivisiId ).Keterangan;
            if (departemen == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisiId = departemen.DivisiId ;
            return View(departemen);
        }

        // POST: Departemen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartemenID,DivisiId,Keterangan")] Departemen departemen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departemen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = departemen.DivisiId });
            }
            ViewBag.DivisiId = departemen.DivisiId ;
            return View(departemen);
        }

        // GET: Departemen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departemen departemen = db.Departemen.Find(id);
            if (departemen == null)
            {
                return HttpNotFound();
            }
            ViewBag.Divisi = db.Divisis.Find(departemen.DivisiId ).Keterangan;
            return View(departemen);
        }

        // POST: Departemen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departemen departemen = db.Departemen.Find(id);
            db.Departemen.Remove(departemen);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = departemen.DivisiId });
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
