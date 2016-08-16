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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDJurnalUmumPosting")]
    public class tblGLBatchesReadOnlyController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblGLBatchesReadOnly
        public ActionResult Index(int? Year, int? Period)
        {
            List<tblGLBatch> tblGLBatch = new List<tblGLBatch>();

            if (Year != null)
            {
                tblGLBatch = db.tblGLBatches.Where(x => x.AccYear == Year && x.AccPeriod == Period && x.Posting == true).ToList();
                ViewBag.AccPeriod = DropDownListUtility.PeriodDropDown(Period);
                ViewBag.AccYear = DropDownListUtility.YearDropDown(Year);
            }
            else
            {
                ViewBag.AccPeriod = DropDownListUtility.PeriodDropDown(11);
                ViewBag.AccYear = DropDownListUtility.YearDropDown(DateTime.Now.Year);
            }
          
            return View(tblGLBatch );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "tblGLBatchId,TglKerja,AccYear,AccPeriod,Posting,User,TglKomputer,Keterangan")] tblGLBatch tblGLBatch)
        {
            if (ModelState.IsValid)
            {
                 return RedirectToAction("Index", new {Year=tblGLBatch.AccYear ,Period=tblGLBatch.AccPeriod  });
            }
            return RedirectToAction ("Index");
        }

        // GET: tblGLBatchesReadOnly/Details/5
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
