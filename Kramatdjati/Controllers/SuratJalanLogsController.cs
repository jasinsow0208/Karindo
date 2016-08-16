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
    public class SuratJalanLogsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SuratJalanLogs
        public ActionResult Index()
        {
            return View(db.SuratJalanLogs.ToList());
        }

        // GET: SuratJalanLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanLog suratJalanLog = db.SuratJalanLogs.Find(id);
            if (suratJalanLog == null)
            {
                return HttpNotFound();
            }
            return View(suratJalanLog);
        }

        // GET: SuratJalanLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuratJalanLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuratJalanLogID,SuratJalanID,TglProses,Operator,Keterangan")] SuratJalanLog suratJalanLog)
        {
            if (ModelState.IsValid)
            {
                db.SuratJalanLogs.Add(suratJalanLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suratJalanLog);
        }

        // GET: SuratJalanLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanLog suratJalanLog = db.SuratJalanLogs.Find(id);
            if (suratJalanLog == null)
            {
                return HttpNotFound();
            }
            return View(suratJalanLog);
        }

        // POST: SuratJalanLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuratJalanLogID,SuratJalanID,TglProses,Operator,Keterangan")] SuratJalanLog suratJalanLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suratJalanLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suratJalanLog);
        }

        // GET: SuratJalanLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanLog suratJalanLog = db.SuratJalanLogs.Find(id);
            if (suratJalanLog == null)
            {
                return HttpNotFound();
            }
            return View(suratJalanLog);
        }

        // POST: SuratJalanLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuratJalanLog suratJalanLog = db.SuratJalanLogs.Find(id);
            db.SuratJalanLogs.Remove(suratJalanLog);
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
