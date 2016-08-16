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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDAccountType")]
    public class GLAccountTypesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: GLAccountTypes
        public ActionResult Index()
        {
            return View(db.tblGLAccountTypes.ToList());
        }

        // GET: GLAccountTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccountType tblGLAccountType = db.tblGLAccountTypes.Find(id);
            if (tblGLAccountType == null)
            {
                return HttpNotFound();
            }
            return View(tblGLAccountType);
        }

        #region "Create"
        // GET: GLAccountTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GLAccountTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblGLAccountTypeId,Type")] tblGLAccountType tblGLAccountType)
        {
            if (ModelState.IsValid)
            {
                db.tblGLAccountTypes.Add(tblGLAccountType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblGLAccountType);
        }


        #endregion

        #region "Create Modal Windows"
        public ActionResult ModalCreate()
        {
            return PartialView("_Create");
        }
        // POST: GLAccountTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MOdalCreate([Bind(Include = "tblGLAccountTypeId,Type")] tblGLAccountType tblGLAccountType)
        {
            if (ModelState.IsValid)
            {
                db.tblGLAccountTypes.Add(tblGLAccountType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblGLAccountType);
        }

        #endregion


        #region "Edit"
        // GET: GLAccountTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccountType tblGLAccountType = db.tblGLAccountTypes.Find(id);
            if (tblGLAccountType == null)
            {
                return HttpNotFound();
            }
            return View(tblGLAccountType);
        }

        // POST: GLAccountTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblGLAccountTypeId,Type")] tblGLAccountType tblGLAccountType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLAccountType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblGLAccountType);
        }
        #endregion

        #region "Edit Modal Window"
        // GET: GLAccountTypes/Edit/5
        public ActionResult ModalEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccountType tblGLAccountType = db.tblGLAccountTypes.Find(id);
            if (tblGLAccountType == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Edit", tblGLAccountType);
        }

        // POST: GLAccountTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModalEdit([Bind(Include = "tblGLAccountTypeId,Type")] tblGLAccountType tblGLAccountType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLAccountType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblGLAccountType);
        }
        #endregion


        #region "Delete"
        // GET: GLAccountTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccountType tblGLAccountType = db.tblGLAccountTypes.Find(id);
            if (tblGLAccountType == null)
            {
                return HttpNotFound();
            }
            return View(tblGLAccountType);
        }

        // POST: GLAccountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGLAccountType tblGLAccountType = db.tblGLAccountTypes.Find(id);
            db.tblGLAccountTypes.Remove(tblGLAccountType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region "Delete modal window"
        // GET: GLAccountTypes/Delete/5
        public ActionResult ModalDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccountType tblGLAccountType = db.tblGLAccountTypes.Find(id);
            if (tblGLAccountType == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", tblGLAccountType); 
        }

        // POST: GLAccountTypes/Delete/5
        [HttpPost, ActionName("ModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ModalDeleteConfirmed(int id)
        {
            tblGLAccountType tblGLAccountType = db.tblGLAccountTypes.Find(id);
            db.tblGLAccountTypes.Remove(tblGLAccountType);
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
