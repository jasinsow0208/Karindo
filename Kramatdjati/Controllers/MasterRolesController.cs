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
    public class MasterRolesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: MasterRoles
        public ActionResult Index()
        {
            return View(db.MasterRoles.ToList());
        }

        // GET: MasterRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterRole masterRole = db.MasterRoles.Find(id);
            if (masterRole == null)
            {
                return HttpNotFound();
            }
            return View(masterRole);
        }

        // GET: MasterRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MasterRoleID,NamaRole")] MasterRole masterRole)
        {
            if (ModelState.IsValid)
            {
                db.MasterRoles.Add(masterRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterRole);
        }

        // GET: MasterRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterRole masterRole = db.MasterRoles.Find(id);
            if (masterRole == null)
            {
                return HttpNotFound();
            }
            return View(masterRole);
        }

        // POST: MasterRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MasterRoleID,NamaRole")] MasterRole masterRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterRole);
        }

        // GET: MasterRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterRole masterRole = db.MasterRoles.Find(id);
            if (masterRole == null)
            {
                return HttpNotFound();
            }
            return View(masterRole);
        }

        // POST: MasterRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterRole masterRole = db.MasterRoles.Find(id);
            db.MasterRoles.Remove(masterRole);
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
