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
    public class UserRolesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: UserRoles
        public ActionResult Index(Guid id, string UserName)
        {
            var userRoles = db.UserRoles.Where(x=>x.UserID == id).Include(u => u.MasterRole);
            ViewBag.UserName = UserName;
            ViewBag.id = id;

            
            List<UserRoleView> userRoleViews = new List<UserRoleView>();

            foreach (UserRole item in userRoles)
            {
                UserRoleView userRoleView = new UserRoleView();
                userRoleView.MasterRoleID = item.MasterRoleID;
                userRoleView.Nama = UserName;
                userRoleView.UserID = item.UserID;
                userRoleView.UserRoleID = item.UserRoleID;
                userRoleView.MasterRole = item.MasterRole;

                userRoleViews.Add(userRoleView);
            }

            return View( userRoleViews.ToList());
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create(Guid id, string UserName)
        {
            ViewBag.MasterRoleID = new SelectList(db.MasterRoles, "MasterRoleID", "NamaRole");
            ViewBag.UserName = UserName;
            UserRoleView UserRoleView = new UserRoleView
            {
                UserID = id,
                Nama =UserName
            };
            return View(UserRoleView);
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserRoleID,MasterRoleID,UserID, Nama")] UserRoleView userRoleView)
        {
            if (ModelState.IsValid)
            {
                UserRole userRole = new UserRole
                {
                    UserRoleID = userRoleView.UserRoleID,
                    UserID = userRoleView.UserID,
                    MasterRoleID = userRoleView.MasterRoleID
                };

                db.UserRoles.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = userRole.UserID , UserName=userRoleView.Nama  });
            }

            ViewBag.MasterRoleID = new SelectList(db.MasterRoles, "MasterRoleID", "NamaRole", userRoleView.MasterRoleID);
            return View(userRoleView);
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int? id, string UserName)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterRoleID = new SelectList(db.MasterRoles, "MasterRoleID", "NamaRole", userRole.MasterRoleID);

            UserRoleView userRoleView = new UserRoleView();

            userRoleView.Nama = UserName;
            userRoleView.MasterRoleID = userRole.MasterRoleID;
            userRoleView.UserID = userRole.UserID;
            userRoleView.UserRoleID = userRole.UserRoleID;

            return View(userRoleView);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRoleID,MasterRoleID,UserID, Nama")] UserRoleView userRoleView)
        {
            if (ModelState.IsValid)
            {
                UserRole userRole = new UserRole();
                userRole.UserRoleID = userRoleView.UserRoleID;
                userRole.UserID = userRoleView.UserID;
                userRole.MasterRoleID = userRoleView.MasterRoleID;

                db.Entry(userRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = userRoleView.UserID, UserName = userRoleView.Nama });
            }
            ViewBag.MasterRoleID = new SelectList(db.MasterRoles, "MasterRoleID", "NamaRole", userRoleView.MasterRoleID);
            return View(userRoleView);
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int? id,string UserName)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }

           
            UserRoleView userRoleView = new UserRoleView();
            userRoleView.Nama = UserName;
            userRoleView.MasterRoleID = userRole.MasterRoleID;
            userRoleView.UserID = userRole.UserID;
            userRoleView.UserRoleID = userRole.UserRoleID;
            userRoleView.MasterRole = userRole.MasterRole;

            return View(userRoleView);
        }

         //POST: UserRoles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int? id)
        //{
            
        //    UserRole userRole = db.UserRoles.Find(id);
        //    db.UserRoles.Remove(userRole);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "UserRoleID,MasterRoleID,UserID, Nama")] UserRoleView userRoleView)
        {
            int id = userRoleView.UserRoleID;
            UserRole userRole = db.UserRoles.Find(id);
            db.UserRoles.Remove(userRole);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = userRole.UserID, UserName = userRoleView.Nama });
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
