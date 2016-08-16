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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDJurnalUmum")]
    public class tblGLBatchesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblGLBatches
        public ActionResult Index()
        {
            List<GLBatchView> GLBatches = new List<GLBatchView>();

            foreach (tblGLBatch  rw in db.tblGLBatches.Where(x => x.Posting == null || x.Posting == false))
            {
                GLBatchView GLBatch = new GLBatchView();
                GLBatch.tblGLBatchId = rw.tblGLBatchId;
                GLBatch.TglKerja = rw.TglKerja;
                GLBatch.AccYear = rw.AccYear;
                GLBatch.AccPeriod = rw.AccPeriod;
                GLBatch.Debet = rw.tblGLBatchDetails.Sum(x => x.Debet);
                GLBatch.Kredit = rw.tblGLBatchDetails.Sum(x => x.Kredit);
                GLBatch.Keterangan = rw.Keterangan;

                GLBatches.Add(GLBatch);
            }
            return View(GLBatches);
        }

        // GET: tblGLBatches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLBatch tblGLBatch = db.tblGLBatches.Find(id);
            if (tblGLBatch == null)
            {
                return HttpNotFound();
            }
            return View(tblGLBatch);
        }

        // GET: tblGLBatches/Create
        public ActionResult Create()
        {
            ViewBag.AccPeriod = DropDownListUtility.PeriodDropDown(11);
            ViewBag.AccYear = DropDownListUtility.YearDropDown(DateTime.Now.Year);
            return View();
        }

        // POST: tblGLBatches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblGLBatchId,TglKerja,AccYear,AccPeriod,Posting,User,TglKomputer,Keterangan")] tblGLBatch tblGLBatch)
        {
            if (ModelState.IsValid)
            {
                db.tblGLBatches.Add(tblGLBatch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblGLBatch);
        }

        // GET: tblGLBatches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLBatch tblGLBatch = db.tblGLBatches.Find(id);
            if (tblGLBatch == null)
            {
                return HttpNotFound();
            }

            ViewBag.AccPeriod = DropDownListUtility.PeriodDropDown(11);
            ViewBag.AccYear = DropDownListUtility.YearDropDown(DateTime.Now.Year);

            return View(tblGLBatch);
        }

        // POST: tblGLBatches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblGLBatchId,TglKerja,AccYear,AccPeriod,Posting,User,TglKomputer,Keterangan")] tblGLBatch tblGLBatch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLBatch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblGLBatch);
        }

        // GET: tblGLBatches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLBatch tblGLBatch = db.tblGLBatches.Find(id);
            if (tblGLBatch == null)
            {
                return HttpNotFound();
            }
            return View(tblGLBatch);
        }

        // POST: tblGLBatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGLBatch tblGLBatch = db.tblGLBatches.Find(id);
            db.tblGLBatches.Remove(tblGLBatch);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spJurnalPosting @tblGLBatchId, @User, @ReturnValue OUT",
                                                        new SqlParameter("@tblGLBatchId", id),
                                                        new SqlParameter("@User", User.Identity.Name ),
                                                        ReturnParameter);

            if (ReturnParameter.Value.ToString() == "0")
            {
                ViewBag.Info = "Posting Berhasil";
             }
            else
            {
                ViewBag.Info = string.Format( "Posting Gagal. error: {0}" , ReturnParameter.Value);
            };

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
