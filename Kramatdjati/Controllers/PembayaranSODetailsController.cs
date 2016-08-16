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
    public class PembayaranSODetailsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PembayaranSODetails
        public ActionResult Index(int id)
        {
            ViewBag.PembayaranSOID = id;
            var pembayaranSODetails = db.PembayaranSODetails.Where(x=>x.PembayaranSOID==id).Include(p => p.fakturJual).Include(p => p.pembayaranSO);
            return View(pembayaranSODetails.ToList());
        }

        // GET: PembayaranSODetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PembayaranSODetail pembayaranSODetail = db.PembayaranSODetails.Find(id);
            if (pembayaranSODetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PembayaranSOID = pembayaranSODetail.PembayaranSOID;
            return View(pembayaranSODetail);
        }

        // GET: PembayaranSODetails/Create
        public ActionResult Create(int id)
        {
            PembayaranSO pembayaranSO = db.PembayaranSOes.Find(id);
            decimal saldoAwal = pembayaranSO.Jumlah;
            decimal TerpakaiFinal = 0;
            var pembayaranDetails = db.PembayaranSODetails.Where(x => x.PembayaranSOID == id);
            if (pembayaranDetails.Count() != 0)
            {
                TerpakaiFinal = pembayaranDetails.Sum(x => x.Jumlah);
            }
            decimal sisa = saldoAwal - TerpakaiFinal;
            ViewBag.Sisa = string.Format("{0:N2}",sisa);

            var fakturJuals = db.FakturJuals.Where(x => x.ContactID == pembayaranSO.ContactID).ToList().Where (x=>x.StatusLunas == false);
            if (fakturJuals.Count() == 0)
            {
                 ViewBag.Info = "Tidak ada faktur piutang untuk customer ini";
                 return View("Informasi");
            }
            else
            {
               ViewBag.FakturJualID = new SelectList(fakturJuals.ToList(), "FakturJualID", "NoFaktur");
                ViewBag.PembayaranSOID = id;
                return View();
            }
         }

        // POST: PembayaranSODetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PembayaranSODetailID,PembayaranSOID,FakturJualID,Jumlah,Posting")] PembayaranSODetail pembayaranSODetail)
        {
            if (ModelState.IsValid)
            {
                db.PembayaranSODetails.Add(pembayaranSODetail);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = pembayaranSODetail.PembayaranSOID });
            }

            PembayaranSO pembayaranSO = db.PembayaranSOes.Find(pembayaranSODetail.PembayaranSOID);
            var fakturJuals = db.FakturJuals.Where(x => x.ContactID == pembayaranSO.ContactID).ToList().Where (x=>x.StatusLunas == false);
            ViewBag.FakturJualID = new SelectList(fakturJuals.ToList(), "FakturJualID", "NoFaktur", pembayaranSODetail.FakturJualID);
            ViewBag.PembayaranSOID = pembayaranSODetail.PembayaranSOID;

            return View(pembayaranSODetail);
        }

        // GET: PembayaranSODetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PembayaranSODetail pembayaranSODetail = db.PembayaranSODetails.Find(id);
            if (pembayaranSODetail == null)
            {
                return HttpNotFound();
            }

            PembayaranSO pembayaranSO = db.PembayaranSOes.Find(pembayaranSODetail.PembayaranSOID);

            var fakturJuals = db.FakturJuals.Where(x => x.ContactID == pembayaranSO.ContactID).ToList().Where(x => x.StatusLunas == false);
            ViewBag.FakturJualID = new SelectList(fakturJuals.ToList(), "FakturJualID", "NoFaktur", pembayaranSODetail.FakturJualID);
            ViewBag.PembayaranSOID = pembayaranSODetail.PembayaranSOID;

            return View(pembayaranSODetail);
        }

        // POST: PembayaranSODetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PembayaranSODetailID,PembayaranSOID,FakturJualID,Jumlah,Posting")] PembayaranSODetail pembayaranSODetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pembayaranSODetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = pembayaranSODetail.PembayaranSOID });
            }

            PembayaranSO pembayaranSO = db.PembayaranSOes.Find(pembayaranSODetail.PembayaranSOID);

            var fakturJuals = db.FakturJuals.Where(x => x.ContactID == pembayaranSO.ContactID).ToList().Where(x => x.StatusLunas == false);
            ViewBag.FakturJualID = new SelectList(fakturJuals.ToList(), "FakturJualID", "NoFaktur", pembayaranSODetail.FakturJualID);
            ViewBag.PembayaranSOID = pembayaranSODetail.PembayaranSOID;

            return View(pembayaranSODetail);
        }

        // GET: PembayaranSODetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PembayaranSODetail pembayaranSODetail = db.PembayaranSODetails.Find(id);
            if (pembayaranSODetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PembayaranSOID = pembayaranSODetail.PembayaranSOID;
            return View(pembayaranSODetail);
        }

        // POST: PembayaranSODetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PembayaranSODetail pembayaranSODetail = db.PembayaranSODetails.Find(id);
            db.PembayaranSODetails.Remove(pembayaranSODetail);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = pembayaranSODetail.PembayaranSOID });
        }

        public ActionResult Posting(int id)
        {

            ViewBag.PembayaranSODetailID = id;
            var pembayaranSODetail = db.PembayaranSODetails.Find(id);
            ViewBag.PembayaranSOID = pembayaranSODetail.PembayaranSOID;

            pembayaranSODetail.Posting = true;

            db.Entry(pembayaranSODetail).State = EntityState.Modified;

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
