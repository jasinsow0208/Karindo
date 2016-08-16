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
     [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDGLAccount")]
    public class tblGLAccountsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblGLAccounts
        public ActionResult Index()
        {
            var tblGLAccounts = db.tblGLAccounts.Include(t => t.tblGLAccountType);
            return View(tblGLAccounts.ToList());
        }

        // GET: tblGLAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccount tblGLAccount = db.tblGLAccounts.Find(id);
            if (tblGLAccount == null)
            {
                return HttpNotFound();
            }
            return View(tblGLAccount);
        }

        #region "Create"

        // GET: tblGLAccounts/Create
        public ActionResult Create()
        {
           ViewBag.tblGLAccountTypeId = new SelectList(db.tblGLAccountTypes, "tblGLAccountTypeId", "Type") .OrderBy (x=>x.Text );
            return View();
        }

        // POST: tblGLAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblGLAccountId,AccCode,AccName,AccDescription,tblGLAccountTypeId,RetainedEarnings,Active,AccKas")] tblGLAccount tblGLAccount)
        {
            if (ModelState.IsValid)
            {
                db.tblGLAccounts.Add(tblGLAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tblGLAccountTypeId = new SelectList(db.tblGLAccountTypes, "tblGLAccountTypeId", "Type", tblGLAccount.tblGLAccountTypeId);
            return View(tblGLAccount);
        }
        #endregion

        #region "Modal Create"
        public ActionResult ModalCreate()
        {
            ViewBag.tblGLAccountTypeId = new SelectList(db.tblGLAccountTypes, "tblGLAccountTypeId", "Type");
            return PartialView("_Create");
        }

        // POST: tblGLAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModalCreate([Bind(Include = "tblGLAccountId,AccCode,AccName,AccDescription,tblGLAccountTypeId,RetainedEarnings,Active,AccKas")] tblGLAccount tblGLAccount)
        {
            if (ModelState.IsValid)
            {
                db.tblGLAccounts.Add(tblGLAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tblGLAccountTypeId = new SelectList(db.tblGLAccountTypes, "tblGLAccountTypeId", "Type", tblGLAccount.tblGLAccountTypeId);
            return PartialView("_Create", tblGLAccount);
            //return View(tblGLAccount);
        }
        #endregion

        #region "Edit"

        // GET: tblGLAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccount tblGLAccount = db.tblGLAccounts.Find(id);
            if (tblGLAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblGLAccountTypeId = new SelectList(db.tblGLAccountTypes, "tblGLAccountTypeId", "Type", tblGLAccount.tblGLAccountTypeId);
            return View(tblGLAccount);
        }

        // POST: tblGLAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblGLAccountId,AccCode,AccName,AccDescription,tblGLAccountTypeId,RetainedEarnings,Active,AccKas")] tblGLAccount tblGLAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tblGLAccountTypeId = new SelectList(db.tblGLAccountTypes, "tblGLAccountTypeId", "Type", tblGLAccount.tblGLAccountTypeId);
            return View(tblGLAccount);
        }

        #endregion

        #region "Modal Edit"
        // GET: tblGLAccounts/Edit/5
        public ActionResult ModalEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccount tblGLAccount = db.tblGLAccounts.Find(id);
            if (tblGLAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblGLAccountTypeId = new SelectList(db.tblGLAccountTypes, "tblGLAccountTypeId", "Type", tblGLAccount.tblGLAccountTypeId);
            return PartialView("_Edit", tblGLAccount);
        }

        // POST: tblGLAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModalEdit([Bind(Include = "tblGLAccountId,AccCode,AccName,AccDescription,tblGLAccountTypeId,RetainedEarnings,Active,AccKas")] tblGLAccount tblGLAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tblGLAccountTypeId = new SelectList(db.tblGLAccountTypes, "tblGLAccountTypeId", "Type", tblGLAccount.tblGLAccountTypeId);
            return View(tblGLAccount);
        }
        #endregion

        #region "Delete"

        // GET: tblGLAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccount tblGLAccount = db.tblGLAccounts.Find(id);
            if (tblGLAccount == null)
            {
                return HttpNotFound();
            }
            return View(tblGLAccount);
        }

        // POST: tblGLAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGLAccount tblGLAccount = db.tblGLAccounts.Find(id);
            db.tblGLAccounts.Remove(tblGLAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region "Modal Delete"
        // GET: tblGLAccounts/Delete/5
        public ActionResult ModalDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccount tblGLAccount = db.tblGLAccounts.Find(id);
            if (tblGLAccount == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", tblGLAccount);
        }

        // POST: tblGLAccounts/Delete/5
        [HttpPost, ActionName("ModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ModalDeleteConfirmed(int id)
        {
            tblGLAccount tblGLAccount = db.tblGLAccounts.Find(id);
            db.tblGLAccounts.Remove(tblGLAccount);
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
