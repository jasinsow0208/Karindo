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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Kramatdjati.Controllers
{
    public class AppRoleFormsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: AppRoleForms
        public ActionResult Index(string appRoleID)
        {
            var appRoleForms = db.AppRoleForms.Include(a => a.AppRole).Include(a => a.MasterForm).Where (x=>x.AppRoleID == appRoleID);
            ViewBag.id = appRoleID ;
            ViewBag.Role = RoleManager.Roles.Where(x => x.Id == appRoleID).SingleOrDefault().Name;
            return View(appRoleForms.ToList());
        }

        // GET: AppRoleForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRoleForms appRoleForms = db.AppRoleForms.Find(id);
            if (appRoleForms == null)
            {
                return HttpNotFound();
            }
            return View(appRoleForms);
        }

        // GET: AppRoleForms/Create
        public ActionResult Create(string appRoleID)
        {
            ViewBag.AppRoleID = new SelectList(RoleManager.Roles , "Id", "Name");
            ViewBag.MasterFormID = new SelectList(db.MasterForms, "MasterFormID", "KodeForm");

            AppRole  Role = RoleManager.Roles.SingleOrDefault (m=> m.Id == appRoleID);

            string RoleName = Role.Name ;

            AppRoleFormsView appRoleFormsView = new AppRoleFormsView 
            {
                MasterRole = RoleName,
                AppRoleID  = appRoleID,
                MasterFormID = db.MasterForms.FirstOrDefault().MasterFormID
            };

            return View(appRoleFormsView);
        }

        // POST: AppRoleForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppRoleFormsID,AppRoleID,MasterFormID")] AppRoleFormsView appRoleFormsView)
        {
            if (ModelState.IsValid)
            {
                AppRoleForms  appRoleForms = new AppRoleForms
                {
                    MasterFormID = appRoleFormsView.MasterFormID,
                    AppRoleID =appRoleFormsView .AppRoleID 
                 };
                db.AppRoleForms.Add(appRoleForms);
                db.SaveChanges();
                return RedirectToAction("Index", new { appRoleID = appRoleForms.AppRoleID  });
            }

            ViewBag.AppRoleID = new SelectList(RoleManager.Roles, "Id", "Name", appRoleFormsView.AppRoleID);
            ViewBag.MasterFormID = new SelectList(db.MasterForms, "MasterFormID", "KodeForm", appRoleFormsView.MasterFormID);
            return View(appRoleFormsView);
        }

        // GET: AppRoleForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRoleForms appRoleForms = db.AppRoleForms.Find(id);
            if (appRoleForms == null)
            {
                return HttpNotFound();
            }

            //Ambil informasi nama Role
            AppRole  Role = RoleManager.Roles.SingleOrDefault(m => m.Id == appRoleForms.AppRoleID );
            string RoleName = Role.Name ;

            AppRoleFormsView  appRoleFormsView = new AppRoleFormsView 
            {
                MasterRole = RoleName,
                AppRoleID  = appRoleForms .AppRoleID ,
                MasterFormID = appRoleForms.MasterFormID,
                AppRoleFormsID  = id
            };

            ViewBag.AppRoleID = new SelectList(RoleManager.Roles, "Id", "Name", appRoleForms.AppRoleID);
            ViewBag.MasterFormID = new SelectList(db.MasterForms, "MasterFormID", "KodeForm", appRoleForms.MasterFormID);
            return View(appRoleFormsView);
        }

        // POST: AppRoleForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppRoleFormsID,AppRoleID,MasterFormID")] AppRoleFormsView appRoleFormsView)
        {
            if (ModelState.IsValid)
            {
                AppRoleForms appRoleForms = new AppRoleForms 
                {
                    MasterFormID = appRoleFormsView.MasterFormID,
                    AppRoleID = appRoleFormsView.AppRoleID ,
                    AppRoleFormsID =Convert.ToInt32 (appRoleFormsView.AppRoleFormsID )
                };

                db.Entry(appRoleForms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { appRoleID = appRoleFormsView.AppRoleID  });
            }
            ViewBag.AppRoleID = new SelectList(RoleManager.Roles, "Id", "Name", appRoleFormsView.AppRoleID);
            ViewBag.MasterFormID = new SelectList(db.MasterForms, "MasterFormID", "KodeForm", appRoleFormsView.MasterFormID);
            return View(appRoleFormsView);
        }

        // GET: AppRoleForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRoleForms appRoleForms = db.AppRoleForms.Find(id);
            if (appRoleForms == null)
            {
                return HttpNotFound();
            }
            return View(appRoleForms);
        }

        // POST: AppRoleForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppRoleForms appRoleForms = db.AppRoleForms.Find(id);
            string strAppRoleID = appRoleForms.AppRoleID;
            db.AppRoleForms.Remove(appRoleForms);
            db.SaveChanges();
            return RedirectToAction("Index", new { appRoleID = strAppRoleID  });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
    }
}
