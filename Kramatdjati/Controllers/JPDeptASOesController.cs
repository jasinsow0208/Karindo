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
    public class JPDeptASOesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JPDeptASOes
        public ActionResult Index(int id)
        {
            var jpDeptARincian = db.JPDeptARincians.Where(x => x.JPDeptARincianID == id).FirstOrDefault();

            ViewBag.KodeBarangJadi = jpDeptARincian.KodeBarangJadi ;
            ViewBag.Catatan = jpDeptARincian.Keterangan;
            ViewBag.JPDeptARincianID = id;

            var jPDeptASOes = db.JPDeptASOes.Where(x=>x.JPDeptARincianID ==id).Include(j => j.jpDeptARincian).Include(j => j.salesOrderRincian);
            return View(jPDeptASOes.ToList());
        }

        // GET: JPDeptASOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptASO jPDeptASO = db.JPDeptASOes.Find(id);
            if (jPDeptASO == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptASO);
        }

        // GET: JPDeptASOes/Create
        public ActionResult Create(int id)
        {
            
            ViewBag.JPDeptARincianID = id;
            ViewBag.SalesOrderRincianID = new SelectList(db.SalesOrderRincians, "SalesOrderRincianID", "Keterangan");
            return View();
        }

        // POST: JPDeptASOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPDeptASOID,JPDeptARincianID,SalesOrderRincianID")] JPDeptASO jPDeptASO)
        {
            if (ModelState.IsValid)
            {
                db.JPDeptASOes.Add(jPDeptASO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JPDeptARincianID = new SelectList(db.JPDeptARincians, "JPDeptARincianID", "KodeBarangJadi", jPDeptASO.JPDeptARincianID);
            ViewBag.SalesOrderRincianID = new SelectList(db.SalesOrderRincians, "SalesOrderRincianID", "Keterangan", jPDeptASO.SalesOrderRincianID);
            return View(jPDeptASO);
        }

        // GET: JPDeptASOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptASO jPDeptASO = db.JPDeptASOes.Find(id);
            if (jPDeptASO == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptARincianID = new SelectList(db.JPDeptARincians, "JPDeptARincianID", "KodeBarangJadi", jPDeptASO.JPDeptARincianID);
            ViewBag.SalesOrderRincianID = new SelectList(db.SalesOrderRincians, "SalesOrderRincianID", "Keterangan", jPDeptASO.SalesOrderRincianID);
            return View(jPDeptASO);
        }

        // POST: JPDeptASOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPDeptASOID,JPDeptARincianID,SalesOrderRincianID")] JPDeptASO jPDeptASO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPDeptASO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JPDeptARincianID = new SelectList(db.JPDeptARincians, "JPDeptARincianID", "KodeBarangJadi", jPDeptASO.JPDeptARincianID);
            ViewBag.SalesOrderRincianID = new SelectList(db.SalesOrderRincians, "SalesOrderRincianID", "Keterangan", jPDeptASO.SalesOrderRincianID);
            return View(jPDeptASO);
        }

        // GET: JPDeptASOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptASO jPDeptASO = db.JPDeptASOes.Find(id);
            if (jPDeptASO == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptASO);
        }

        // POST: JPDeptASOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPDeptASO jPDeptASO = db.JPDeptASOes.Find(id);
            db.JPDeptASOes.Remove(jPDeptASO);
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
