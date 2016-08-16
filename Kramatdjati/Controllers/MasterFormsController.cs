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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDAdministrator")]
    public class MasterFormsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: MasterForms
        public ActionResult Index()
        {
            return View(db.MasterForms.ToList());
        }

        // GET: MasterForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterForm masterForm = db.MasterForms.Find(id);
            if (masterForm == null)
            {
                return HttpNotFound();
            }
            return View(masterForm);
        }

        #region "Create"
        // GET: MasterForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MasterFormID,KodeForm,NamaForm,Keterangan")] MasterForm masterForm)
        {
            if (ModelState.IsValid)
            {
                db.MasterForms.Add(masterForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterForm);
        }
        #endregion


        #region "Create Modal Windows"
        // GET: MasterForms/Create
        public ActionResult ModalCreate()
        {
            return PartialView("_MasterFormsCreate");
        }

        // POST: MasterForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModalCreate([Bind(Include = "MasterFormID,KodeForm,NamaForm,Keterangan")] MasterForm masterForm)
        {
            if (ModelState.IsValid)
            {
                db.MasterForms.Add(masterForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterForm);
        }

        #endregion


        #region "Edit"

        // GET: MasterForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterForm masterForm = db.MasterForms.Find(id);
            if (masterForm == null)
            {
                return HttpNotFound();
            }
            return View(masterForm);
        }

        // POST: MasterForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MasterFormID,KodeForm,NamaForm,Keterangan")] MasterForm masterForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterForm);
        }
        #endregion

        #region "Edit Modal Windows"

        // GET: MasterForms/Edit/5
        public ActionResult ModalEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MasterForm masterForm = db.MasterForms.Find(id);
            if (masterForm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalEdit", masterForm);
        }

        // POST: MasterForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModalEdit([Bind(Include = "MasterFormID,KodeForm,NamaForm,Keterangan")] MasterForm masterForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_ModalEdit", masterForm);
        }
        #endregion

        #region "Delete"
        // GET: MasterForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterForm masterForm = db.MasterForms.Find(id);
            if (masterForm == null)
            {
                return HttpNotFound();
            }
            return View(masterForm);
        }

        // POST: MasterForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterForm masterForm = db.MasterForms.Find(id);
            db.MasterForms.Remove(masterForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region "Delete modal"
        // GET: MasterForms/Delete/5
        public ActionResult ModalDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterForm masterForm = db.MasterForms.Find(id);
            if (masterForm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDelete", masterForm);
        }

        // POST: MasterForms/Delete/5
        [HttpPost, ActionName("ModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedModal(int id)
        {
            MasterForm masterForm = db.MasterForms.Find(id);
            db.MasterForms.Remove(masterForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

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
