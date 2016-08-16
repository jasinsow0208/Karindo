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
    public class PembayaranSOesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PembayaranSOes
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };

            var pembayaranSOes = db.PembayaranSOes.Where(x=>x.Posting==Posting).Include(p => p.glAccount);
            return View(pembayaranSOes.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PembayaranSOID,TglBayar,ContactID,NoKwitansi,tblGLAccountId,Jumlah,Posting")] PembayaranSO pembayaranSO)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = pembayaranSO.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: PembayaranSOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PembayaranSO pembayaranSO = db.PembayaranSOes.Find(id);
            if (pembayaranSO == null)
            {
                return HttpNotFound();
            }
            return View(pembayaranSO);
        }

        // GET: PembayaranSOes/Create
        public ActionResult Create()
        {
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true), "ContactID", "Perusahaan");
            ViewBag.tblGLAccountId = new SelectList(db.tblGLAccounts.Where(x=>x.AccKas == true ).OrderBy(x=>x.AccDescription), "tblGLAccountId", "AccDescription");
            return View();
        }

        // POST: PembayaranSOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PembayaranSOID,TglBayar,ContactID,NoKwitansi,tblGLAccountId,Jumlah,Posting")] PembayaranSO pembayaranSO)
        {
            if (ModelState.IsValid)
            {
                db.PembayaranSOes.Add(pembayaranSO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true), "ContactID", "Perusahaan", pembayaranSO.ContactID);
            ViewBag.tblGLAccountId = new SelectList(db.tblGLAccounts.Where(x => x.AccKas == true).OrderBy(x => x.AccDescription), "tblGLAccountId", "AccDescription", pembayaranSO.tblGLAccountId);
            return View(pembayaranSO);
        }

        // GET: PembayaranSOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PembayaranSO pembayaranSO = db.PembayaranSOes.Find(id);
            if (pembayaranSO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true), "ContactID", "Perusahaan", pembayaranSO.ContactID);
            ViewBag.tblGLAccountId = new SelectList(db.tblGLAccounts.Where(x => x.AccKas == true).OrderBy(x => x.AccDescription), "tblGLAccountId", "AccDescription", pembayaranSO.tblGLAccountId);
            return View(pembayaranSO);
        }

        // POST: PembayaranSOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PembayaranSOID,TglBayar,ContactID,NoKwitansi,tblGLAccountId,Jumlah,Posting")] PembayaranSO pembayaranSO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pembayaranSO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true), "ContactID", "Perusahaan", pembayaranSO.ContactID);
            ViewBag.tblGLAccountId = new SelectList(db.tblGLAccounts.Where(x => x.AccKas == true).OrderBy(x => x.AccDescription), "tblGLAccountId", "AccDescription", pembayaranSO.tblGLAccountId);
            return View(pembayaranSO);
        }

        // GET: PembayaranSOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PembayaranSO pembayaranSO = db.PembayaranSOes.Find(id);
            if (pembayaranSO == null)
            {
                return HttpNotFound();
            }
            return View(pembayaranSO);
        }

        // POST: PembayaranSOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PembayaranSO pembayaranSO = db.PembayaranSOes.Find(id);
            db.PembayaranSOes.Remove(pembayaranSO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {

            ViewBag.PembayaranSOID = id;
            var pembayaranSO = db.PembayaranSOes.Find(id);
 
            pembayaranSO.Posting = true;

            db.Entry(pembayaranSO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                ViewBag.Info = "Posting Berhasil";
            }
            catch (Exception e)
            {
                ViewBag.Info = string.Format("Posting Gagal. error: {0}", e.Message);
            }
            return View("Informasi");
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
