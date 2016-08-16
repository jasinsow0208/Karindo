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
    public class PasswordSerahTerimasController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PasswordSerahTerimas
        public ActionResult Index()
        {
            return View(db.PasswordSerahTerimas.ToList());
        }

         // GET: PasswordSerahTerimas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PasswordSerahTerimas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PasswordSerahTerimaID,Operator,Password")] PasswordSerahTerima passwordSerahTerima)
        {
            if (ModelState.IsValid)
            {
                db.PasswordSerahTerimas.Add(passwordSerahTerima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(passwordSerahTerima);
        }

        // GET: PasswordSerahTerimas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PasswordSerahTerima passwordSerahTerima = db.PasswordSerahTerimas.Find(id);
            if (passwordSerahTerima == null)
            {
                return HttpNotFound();
            }
            return View(passwordSerahTerima);
        }

        // POST: PasswordSerahTerimas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PasswordSerahTerimaID,Operator,Password")] PasswordSerahTerima passwordSerahTerima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passwordSerahTerima).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passwordSerahTerima);
        }

        // GET: PasswordSerahTerimas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PasswordSerahTerima passwordSerahTerima = db.PasswordSerahTerimas.Find(id);
            if (passwordSerahTerima == null)
            {
                return HttpNotFound();
            }
            return View(passwordSerahTerima);
        }

        // POST: PasswordSerahTerimas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PasswordSerahTerima passwordSerahTerima = db.PasswordSerahTerimas.Find(id);
            db.PasswordSerahTerimas.Remove(passwordSerahTerima);
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
