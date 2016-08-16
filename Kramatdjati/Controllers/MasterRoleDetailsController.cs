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
    public class MasterRoleDetailsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: MasterRoleDetails
        public ActionResult Index(int id)
        {
            var mstrRoleForms = db.MasterRoleDetails.Include(m => m.MasterForm).Where(m => m.MasterRoleID == id);
            ViewBag.id = id;
            ViewBag.Role = db.MasterRoles.Where(x => x.MasterRoleID == id).SingleOrDefault().NamaRole;
            return View(mstrRoleForms.ToList());
        }

        // GET: MasterRoleDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterRoleDetail masterRoleDetail = db.MasterRoleDetails.Find(id);
            if (masterRoleDetail == null)
            {
                return HttpNotFound();
            }
            return View(masterRoleDetail);
        }

        // GET: MasterRoleDetails/Create
        public ActionResult Create(int MasterRoleID)
        {
            ViewBag.MasterFormID = new SelectList(db.MasterForms, "MasterFormID", "KodeForm");
            ViewBag.MasterRoleID = new SelectList(db.MasterRoles, "MasterRoleID", "NamaRole");

            MasterRole Role = db.MasterRoles.SingleOrDefault(m => m.MasterRoleID == MasterRoleID);

            string RoleName = Role.NamaRole;

            MasterRoleDetailView masterRoleDetailView = new MasterRoleDetailView
            {
                MasterRole = RoleName,
                MasterRoleID = MasterRoleID,
                MasterFormID = db.MasterForms.FirstOrDefault().MasterFormID
            };
            return View(masterRoleDetailView);
        }

        // POST: MasterRoleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MasterRoleDetailID,MasterRoleID,MasterFormID")] MasterRoleDetailView masterRoleDetailView)
        {
            if (ModelState.IsValid)
            {
                MasterRoleDetail masterRoleDetail = new MasterRoleDetail
                {
                    MasterFormID = masterRoleDetailView.MasterFormID,
                    MasterRoleID = masterRoleDetailView.MasterRoleID
                };
                db.MasterRoleDetails.Add(masterRoleDetail);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = masterRoleDetail.MasterRoleID });
            }

            ViewBag.MasterFormID = new SelectList(db.MasterForms, "MasterFormID", "KodeForm", masterRoleDetailView.MasterFormID);
            ViewBag.MasterRoleID = new SelectList(db.MasterRoles, "MasterRoleID", "NamaRole", masterRoleDetailView.MasterRoleID);
            return View(masterRoleDetailView);
        }

        // GET: MasterRoleDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
 
            MasterRoleDetail masterRoleDetail = db.MasterRoleDetails.Find(id);
            if (masterRoleDetail == null)
            {
                return HttpNotFound();
            }

            //Ambil informasi nama Role
            MasterRole Role = db.MasterRoles.SingleOrDefault(m => m.MasterRoleID == masterRoleDetail.MasterRoleID );
            string RoleName = Role.NamaRole;

            MasterRoleDetailView masterRoleDetailView = new MasterRoleDetailView
            {
                MasterRole = RoleName,
                MasterRoleID = masterRoleDetail.MasterRoleID ,
                MasterFormID = masterRoleDetail.MasterFormID ,
                MasterRoleDetailID =id
            };

            ViewBag.MasterFormID = new SelectList(db.MasterForms, "MasterFormID", "KodeForm", masterRoleDetail.MasterFormID);
            ViewBag.MasterRoleID = new SelectList(db.MasterRoles, "MasterRoleID", "NamaRole", masterRoleDetail.MasterRoleID);
            return View(masterRoleDetailView);
        }

        // POST: MasterRoleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MasterRoleDetailID,MasterRoleID,MasterFormID")] MasterRoleDetailView masterRoleDetailView)
        {
            if (ModelState.IsValid)
            {
                MasterRoleDetail masterRoleDetail = new MasterRoleDetail
                {
                    MasterFormID = masterRoleDetailView.MasterFormID,
                    MasterRoleID = masterRoleDetailView.MasterRoleID,
                    MasterRoleDetailID = Convert.ToInt32(masterRoleDetailView.MasterRoleDetailID )
                };

                db.Entry(masterRoleDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = masterRoleDetailView.MasterRoleID });
            }
            ViewBag.MasterFormID = new SelectList(db.MasterForms, "MasterFormID", "KodeForm", masterRoleDetailView.MasterFormID);
            ViewBag.MasterRoleID = new SelectList(db.MasterRoles, "MasterRoleID", "NamaRole", masterRoleDetailView.MasterRoleID);
            return View(masterRoleDetailView);
        }

        // GET: MasterRoleDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterRoleDetail masterRoleDetail = db.MasterRoleDetails.Find(id);
            if (masterRoleDetail == null)
            {
                return HttpNotFound();
            }
            return View(masterRoleDetail);
        }

        // POST: MasterRoleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterRoleDetail masterRoleDetail = db.MasterRoleDetails.Find(id);
            db.MasterRoleDetails.Remove(masterRoleDetail);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = masterRoleDetail.MasterRoleID });
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
