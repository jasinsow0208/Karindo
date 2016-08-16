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
    public class SAPiutangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SAPiutangs
        public ActionResult Index()
        {
            var sAPiutangs = db.SAPiutangs.Include(s => s.contact);
            return View(sAPiutangs.ToList());
        }

        // GET: SAPiutangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAPiutang sAPiutang = db.SAPiutangs.Find(id);
            if (sAPiutang == null)
            {
                return HttpNotFound();
            }
            return View(sAPiutang);
        }

        public ActionResult Posting(int id)
        {
            SAPiutang saPiutang = db.SAPiutangs.Find(id);
            if (saPiutang == null)
            {
                return HttpNotFound();
            }

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spSAPiutangPosting @SAPiutangID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@SAPiutangID", id),
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

        // GET: SAPiutangs/Create
        public ActionResult Create()
        {
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan");
            return View();
        }

        // POST: SAPiutangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SAPiutangID,ContactID,TglInvoice,NoInvoice,TglJatuhTempo,Jumlah,TglBayar,StatusBayar")] SAPiutang sAPiutang)
        {
            if (ModelState.IsValid)
            {
                sAPiutang.TglBayar = DateTime.Parse("01/01/01");
                sAPiutang.StatusBayar = false;
                sAPiutang.Posting = false;
                db.SAPiutangs.Add(sAPiutang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", sAPiutang.ContactID);
            return View(sAPiutang);
        }

        // GET: SAPiutangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAPiutang sAPiutang = db.SAPiutangs.Find(id);
            if (sAPiutang == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x=>x.Customer==true).OrderBy(x=>x.Perusahaan) , "ContactID", "Perusahaan", sAPiutang.ContactID);
            return View(sAPiutang);
        }

        // POST: SAPiutangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SAPiutangID,ContactID,TglInvoice,NoInvoice,TglJatuhTempo,Jumlah,TglBayar,StatusBayar,Posting")] SAPiutang sAPiutang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sAPiutang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", sAPiutang.ContactID);
            return View(sAPiutang);
        }

        // GET: SAPiutangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAPiutang sAPiutang = db.SAPiutangs.Find(id);
            if (sAPiutang == null)
            {
                return HttpNotFound();
            }
            return View(sAPiutang);
        }

        // POST: SAPiutangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SAPiutang sAPiutang = db.SAPiutangs.Find(id);
            db.SAPiutangs.Remove(sAPiutang);
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
