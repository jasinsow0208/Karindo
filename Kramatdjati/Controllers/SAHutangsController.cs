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
using System.Data.SqlClient;

namespace Kramatdjati.Controllers
{
    public class SAHutangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SAHutangs
        public ActionResult Index()
        {
            var sAHutangs = db.SAHutangs.Include(s => s.contact);
            return View(sAHutangs.ToList());
        }

        // GET: SAHutangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAHutang sAHutang = db.SAHutangs.Find(id);
            if (sAHutang == null)
            {
                return HttpNotFound();
            }
            return View(sAHutang);
        }

        public ActionResult Posting(int id)
        {
            SAHutang saHutang = db.SAHutangs.Find(id);
            if (saHutang == null)
            {
                return HttpNotFound();
            }

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spSAHutangPosting @SAHutangID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@SAHutangID", id),
                                                        new SqlParameter("@User", User.Identity.Name),
                                                        ReturnParameter);

            if (ReturnParameter.Value.ToString() == "0")
            {
                ViewBag.Info = "Posting Berhasil";
            }
            else
            {
                ViewBag.Info = string.Format("Posting Gagal. error: {0}", ReturnParameter.Value);
            };
            return View("Informasi");
        }

        // GET: SAHutangs/Create
        public ActionResult Create()
        {
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Supplier == true), "ContactID", "Perusahaan");
            return View();
        }

        // POST: SAHutangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SAHutangID,ContactID,TglInvoice,NoInvoice,TglJatuhTempo,Jumlah,TglBayar,StatusBayar,Posting")] SAHutang sAHutang)
        {
            if (ModelState.IsValid)
            {
                sAHutang.TglBayar = DateTime.Parse("01/01/01");
                sAHutang.StatusBayar = false;
                sAHutang.Posting = false;
                db.SAHutangs.Add(sAHutang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Supplier == true), "ContactID", "Perusahaan", sAHutang.ContactID);
            return View(sAHutang);
        }

        // GET: SAHutangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAHutang sAHutang = db.SAHutangs.Find(id);
            if (sAHutang == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Supplier == true), "ContactID", "Perusahaan", sAHutang.ContactID);
            return View(sAHutang);
        }

        // POST: SAHutangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SAHutangID,ContactID,TglInvoice,NoInvoice,TglJatuhTempo,Jumlah,TglBayar,StatusBayar,Posting")] SAHutang sAHutang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sAHutang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Supplier == true), "ContactID", "Perusahaan", sAHutang.ContactID);
            return View(sAHutang);
        }

        // GET: SAHutangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAHutang sAHutang = db.SAHutangs.Find(id);
            if (sAHutang == null)
            {
                return HttpNotFound();
            }
            return View(sAHutang);
        }

        // POST: SAHutangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SAHutang sAHutang = db.SAHutangs.Find(id);
            db.SAHutangs.Remove(sAHutang);
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
