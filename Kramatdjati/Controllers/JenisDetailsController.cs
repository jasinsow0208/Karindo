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
using System.Text;

namespace Kramatdjati.Controllers
{
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDJenis")]
    public class JenisDetailsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JenisDetails
        public ActionResult Index(int id)
        {
            var jenisDetails = db.JenisDetails.Where(x=>x.JenisID ==id).Include(j => j.Bahan).Include(j => j.Jenis);
            var jenis = db.Jenis.Find(id);
            ViewBag.KodeJenis = string.Format("Kode:{0}", jenis.KodeJenis);
            ViewBag.JenisID = id;
            return View(jenisDetails.ToList());
        }

        // GET: JenisDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisDetail jenisDetail = db.JenisDetails.Find(id);
            if (jenisDetail == null)
            {
                return HttpNotFound();
            }
            return View(jenisDetail);
        }

        // GET: JenisDetails/Create
        public ActionResult Create(int id)
        {
            ViewBag.BahanID = new SelectList(db.Bahans, "BahanID", "Keterangan");
            //ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku");
            Jenis jenis = db.Jenis.Find(id);
            ViewBag.KodeJenis = jenis.KodeJenis;
            ViewBag.JenisID = id;
            return View();
        }

        // POST: JenisDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JenisDetailID,JenisID,BahanID,BahanBakuID,Berat,Keterangan")] JenisDetail jenisDetail)
        {
            if (ModelState.IsValid)
            {
                db.JenisDetails.Add(jenisDetail);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jenisDetail.JenisID });
            }

            ViewBag.BahanID = new SelectList(db.Bahans, "BahanID", "Keterangan", jenisDetail.BahanID);
            ViewBag.JenisID = new SelectList(db.Jenis, "JenisID", "KodeJenis", jenisDetail.JenisID);
            return View(jenisDetail);
        }

        // GET: JenisDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisDetail jenisDetail = db.JenisDetails.Find(id);
            if (jenisDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BahanID = new SelectList(db.Bahans, "BahanID", "Keterangan", jenisDetail.BahanID);
            ViewBag.JenisID = new SelectList(db.Jenis, "JenisID", "KodeJenis", jenisDetail.JenisID);
            ViewBag.KodeBahanBaku = jenisDetail.BahanBaku.KodeBahanBaku;
            ViewBag.Keterangan = jenisDetail.BahanBaku.Keterangan;
            ViewBag.Satuan = jenisDetail.BahanBaku.satuan.Keterangan;
            return View(jenisDetail);
        }

        // POST: JenisDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JenisDetailID,JenisID,BahanID,BahanBakuID,Berat,Keterangan")] JenisDetail jenisDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenisDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jenisDetail.JenisID });
            }
            ViewBag.BahanID = new SelectList(db.Bahans, "BahanID", "Keterangan", jenisDetail.BahanID);
            ViewBag.JenisID = new SelectList(db.Jenis, "JenisID", "KodeJenis", jenisDetail.JenisID);
            return View(jenisDetail);
        }

        // GET: JenisDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisDetail jenisDetail = db.JenisDetails.Find(id);
            if (jenisDetail == null)
            {
                return HttpNotFound();
            }
            return View(jenisDetail);
        }

        // POST: JenisDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JenisDetail jenisDetail = db.JenisDetails.Find(id);
            db.JenisDetails.Remove(jenisDetail);
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

        public JsonResult GetBahanBaku(string term)
        {
             //List<string> tblBahanBaku;

            var tblBahanBaku = db.BahanBakus.Where(x=>x.KodeBahanBaku .StartsWith (term)).Select(x => new { x.BahanBakuID, x.KodeBahanBaku, x.Keterangan,  Satuan=x.satuan.Keterangan  }).ToList();

            return Json(tblBahanBaku, JsonRequestBehavior.AllowGet);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }  

        public ActionResult ViewSample()
        {
                return View();
        }
    }
}
