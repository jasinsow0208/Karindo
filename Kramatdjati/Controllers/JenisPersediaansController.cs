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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDJenisPersediaan")]
    public class JenisPersediaansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JenisPersediaans
        public ActionResult Index()
        {
            return View(db.JenisPersediaans.ToList());
        }

        // GET: JenisPersediaans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisPersediaan jenisPersediaan = db.JenisPersediaans.Find(id);
            if (jenisPersediaan == null)
            {
                return HttpNotFound();
            }
            return View(jenisPersediaan);
        }

        // GET: JenisPersediaans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JenisPersediaans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JenisPersediaanID,Keterangan")] JenisPersediaan jenisPersediaan)
        {
            if (ModelState.IsValid)
            {
                db.JenisPersediaans.Add(jenisPersediaan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jenisPersediaan);
        }

        // GET: JenisPersediaans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisPersediaan jenisPersediaan = db.JenisPersediaans.Find(id);
            if (jenisPersediaan == null)
            {
                return HttpNotFound();
            }
            return View(jenisPersediaan);
        }

        // POST: JenisPersediaans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JenisPersediaanID,Keterangan")] JenisPersediaan jenisPersediaan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenisPersediaan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jenisPersediaan);
        }

        // GET: JenisPersediaans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisPersediaan jenisPersediaan = db.JenisPersediaans.Find(id);
            if (jenisPersediaan == null)
            {
                return HttpNotFound();
            }
            return View(jenisPersediaan);
        }

        // POST: JenisPersediaans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JenisPersediaan jenisPersediaan = db.JenisPersediaans.Find(id);
            db.JenisPersediaans.Remove(jenisPersediaan);
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
