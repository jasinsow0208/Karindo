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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDJurnalUmum")]
    public class tblGLBatchDetailsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblGLBatchDetails
        public ActionResult Index(int id)
        {
            var tblGLBatchDetails = db.tblGLBatchDetails.Where(m=>m.tblGLBatchId == id).Include(t => t.tblGLAccount);
            ViewBag.tblGLBatchId = id;
            return View(tblGLBatchDetails.ToList());
        }

        // GET: tblGLBatchDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLBatchDetail tblGLBatchDetail = db.tblGLBatchDetails.Find(id);
            if (tblGLBatchDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblGLBatchDetail);
        }

        public ActionResult CreateOtomatis(int id)
        {
            ViewBag.tblGLAccountIdDebet = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.tblGLAccountIdKredit = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.tblGLBatchId = id;

            ViewJurnalOtomatis ViewJurnalOtomatis= new ViewJurnalOtomatis();
            ViewJurnalOtomatis.TglTransaksi = DateTime.Now;

            return View(ViewJurnalOtomatis);
        }

        // POST: tblGLBatchDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOtomatis([Bind(Include = "tblGLBatchDetailId,tblGLBatchId,tblGLAccountIdDebet, tblGLAccountIdKredit,Keterangan,Jumlah, TglTransaksi,NoRef")] ViewJurnalOtomatis  ViewJurnalOtomatis)
        {
            if (ModelState.IsValid)
            {
                tblGLBatchDetail tblGLBatchDetailDebet = new tblGLBatchDetail();
                tblGLBatchDetailDebet.tblGLBatchId = ViewJurnalOtomatis.tblGLBatchId;
                tblGLBatchDetailDebet.NoRef = ViewJurnalOtomatis.NoRef;
                tblGLBatchDetailDebet.tblGLAccountId = ViewJurnalOtomatis.tblGLAccountIdDebet;
                tblGLBatchDetailDebet.TglTransaksi = ViewJurnalOtomatis.TglTransaksi;
                tblGLBatchDetailDebet.Keterangan = ViewJurnalOtomatis.Keterangan;
                tblGLBatchDetailDebet.Debet = ViewJurnalOtomatis.Jumlah;
                tblGLBatchDetailDebet.Kredit = 0;

                tblGLBatchDetail tblGLBatchDetailKredit = new tblGLBatchDetail();
                tblGLBatchDetailKredit.tblGLBatchId = ViewJurnalOtomatis.tblGLBatchId;
                tblGLBatchDetailKredit.NoRef = ViewJurnalOtomatis.NoRef;
                tblGLBatchDetailKredit.tblGLAccountId = ViewJurnalOtomatis.tblGLAccountIdKredit;
                tblGLBatchDetailKredit.TglTransaksi = ViewJurnalOtomatis.TglTransaksi;
                tblGLBatchDetailKredit.Keterangan = ViewJurnalOtomatis.Keterangan;
                tblGLBatchDetailKredit.Debet = 0;
                tblGLBatchDetailKredit.Kredit = ViewJurnalOtomatis.Jumlah ;

                db.tblGLBatchDetails.Add(tblGLBatchDetailDebet);
                db.tblGLBatchDetails.Add(tblGLBatchDetailKredit);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = ViewJurnalOtomatis.tblGLBatchId  });
            }

            ViewBag.tblGLAccountIdDebet = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", ViewJurnalOtomatis.tblGLAccountIdDebet );
            ViewBag.tblGLAccountIdKredit = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", ViewJurnalOtomatis.tblGLAccountIdKredit );
            ViewBag.tblGLBatchId = ViewJurnalOtomatis.tblGLBatchId ;

            return View(ViewJurnalOtomatis );
        }

        // GET: tblGLBatchDetails/Create
        public ActionResult Create(int id)
        {
            ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.tblGLBatchId = id;
            return View();
        }

        // POST: tblGLBatchDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblGLBatchDetailId,tblGLBatchId,tblGLAccountId,Keterangan,Debet,Kredit, TglTransaksi,NoRef")] tblGLBatchDetail tblGLBatchDetail)
        {
            if (ModelState.IsValid)
            {
                db.tblGLBatchDetails.Add(tblGLBatchDetail);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tblGLBatchDetail.tblGLBatchId });
            }

            ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblGLBatchDetail.tblGLAccountId);
            ViewBag.tblGLBatchId =  tblGLBatchDetail.tblGLBatchId;
            return View(tblGLBatchDetail);
        }

        // GET: tblGLBatchDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLBatchDetail tblGLBatchDetail = db.tblGLBatchDetails.Find(id);
            if (tblGLBatchDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblGLBatchDetail.tblGLAccountId);
            ViewBag.tblGLBatchId = tblGLBatchDetail.tblGLBatchId ;
            return View(tblGLBatchDetail);
        }

        // POST: tblGLBatchDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblGLBatchDetailId,tblGLBatchId,tblGLAccountId,Keterangan,Debet,Kredit, TglTransaksi,NoRef")] tblGLBatchDetail tblGLBatchDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLBatchDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tblGLBatchDetail.tblGLBatchId });
            }
            ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblGLBatchDetail.tblGLAccountId);
            ViewBag.tblGLBatchId = tblGLBatchDetail.tblGLBatchId;
            return View(tblGLBatchDetail);
        }

        // GET: tblGLBatchDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLBatchDetail tblGLBatchDetail = db.tblGLBatchDetails.Find(id);
            if (tblGLBatchDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblGLBatchDetail);
        }

        // POST: tblGLBatchDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGLBatchDetail tblGLBatchDetail = db.tblGLBatchDetails.Find(id);
            db.tblGLBatchDetails.Remove(tblGLBatchDetail);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = tblGLBatchDetail.tblGLBatchId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<ViewNoRek> tblViewNoReks()
        {
            List<ViewNoRek> ViewNoReks = new List<ViewNoRek>();

            foreach (tblGLAccount rw in db.tblGLAccounts.OrderBy(x => x.AccCode))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(rw.AccCode.Trim().PadRight(15, ' '));
                if (rw.AccDescription != null)
                {
                    sb.Append(string.Format("{0} {1}", rw.AccName.Trim(), rw.AccDescription.Trim()));
                }
                else
                {
                    sb.Append(string.Format("{0}", rw.AccName.Trim()));
                };


                ViewNoRek ViewNoRek = new ViewNoRek
                {
                    tblGLAccountID = rw.tblGLAccountId,
                    AccCodeDesc = sb.ToString()
                };
                ViewNoReks.Add(ViewNoRek);
            }
            return ViewNoReks;
        }
    }
}
