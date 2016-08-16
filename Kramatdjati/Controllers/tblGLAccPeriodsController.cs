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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDLaporanKeuangan")]
    public class tblGLAccPeriodsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblGLAccPeriods
        public ActionResult Index()
        {
            ViewReportParam ReportParam = new ViewReportParam();
          ViewBag.Period = DropDownListUtility.PeriodDropDown(1);
          ViewBag.Tahun = DropDownListUtility.YearDropDown(DateTime.Now.Year);
          ViewBag.JenisLaporan = DropDownListUtility.LaporanKeuangan(1);

            return View(ReportParam);
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Period, Tahun, JenisLaporan")] ViewReportParam  ReportParam)
        {
            int AccYear = ReportParam.Tahun ;
            int AccPeriod = ReportParam.Period ;

            var tblGLAccPeriods = db.tblGLAccPeriods.Where(x => x.AccYear == AccYear).Include(t => t.tblGLAccount).Include(t=>t.tblGLAccount.tblGLAccountType).OrderBy (x=>x.tblGLAccount.tblGLAccountTypeId ) ;
            List<ViewReportModel> FinancialReports = new List<ViewReportModel>();

            decimal TotalDebetKredit = 0;

            foreach (tblGLAccPeriod rw in tblGLAccPeriods)
            {
                ViewReportModel FinancialReport = new ViewReportModel();
                if (AccPeriod == 1)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault();
                }
                if (AccPeriod == 2)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault();
                }
                if (AccPeriod == 3)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault();
                }
                if (AccPeriod == 4)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault();
                }
                if (AccPeriod == 5)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault();
                }
                if (AccPeriod == 6)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault();
                }
                if (AccPeriod == 7)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault();
                }
                if (AccPeriod == 8)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault();
                }
                if (AccPeriod == 9)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault() +
                                       rw.AccPeriod9.GetValueOrDefault();
                }
                if (AccPeriod == 10)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault() +
                                       rw.AccPeriod9.GetValueOrDefault() +
                                       rw.AccPeriod10.GetValueOrDefault();
                }
                if (AccPeriod == 11)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault() +
                                       rw.AccPeriod9.GetValueOrDefault() +
                                       rw.AccPeriod10.GetValueOrDefault() +
                                       rw.AccPeriod11.GetValueOrDefault();
                }
                if (AccPeriod == 12)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault() +
                                       rw.AccPeriod9.GetValueOrDefault() +
                                       rw.AccPeriod10.GetValueOrDefault() +
                                       rw.AccPeriod11.GetValueOrDefault() +
                                       rw.AccPeriod12.GetValueOrDefault();
                }

                FinancialReport.tblGLAccountId = rw.tblGLAccountId;
                FinancialReport.AccPeriod = AccPeriod;
                FinancialReport.AccYear = AccYear;
                FinancialReport.tblGLAccount = db.tblGLAccounts.Find(rw.tblGLAccountId); 
                if (TotalDebetKredit < 0)
                {
                    FinancialReport.Debet = 0;
                    FinancialReport.Kredit = -1 * TotalDebetKredit;
                }
                else
                {
                    FinancialReport.Debet = TotalDebetKredit;
                    FinancialReport.Kredit = 0;
                }

                FinancialReports.Add(FinancialReport);
            }

            if (ReportParam.JenisLaporan == 1)
            {
                return View("ProfitAndLoss", FinancialReports.ToList());
            }
            else
            {
                return View("Details",FinancialReports.ToList());
            };
        }

        // GET: tblGLAccPeriods/Details/5
        public ActionResult Details(string Period, string Tahun)
        {
            int AccYear=Convert.ToInt32(Tahun);
            int AccPeriod = Convert.ToInt32(Period);

            var tblGLAccPeriods = db.tblGLAccPeriods.Where(x => x.AccYear == AccYear).Include(t => t.tblGLAccount);
            ViewReportModel FinancialReport = new ViewReportModel();
            List<ViewReportModel> FinancialReports = new List<ViewReportModel>();

            decimal TotalDebetKredit = 0;

            foreach (tblGLAccPeriod rw in tblGLAccPeriods)
            {
                if (AccPeriod == 1)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault();
                }
                if (AccPeriod == 2)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault();
                }
                if (AccPeriod == 3)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault();
                }
                if (AccPeriod == 4)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault();
                }
                if (AccPeriod == 5)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault();
                }
                if (AccPeriod == 6)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault();
                }
                if (AccPeriod == 7)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault();
                }
                if (AccPeriod == 8)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault();
                }
                if (AccPeriod == 9)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault() +
                                       rw.AccPeriod9.GetValueOrDefault();
                }
                if (AccPeriod == 10)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault() +
                                       rw.AccPeriod9.GetValueOrDefault() +
                                       rw.AccPeriod10.GetValueOrDefault();
                }
                if (AccPeriod == 11)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault() +
                                       rw.AccPeriod9.GetValueOrDefault() +
                                       rw.AccPeriod10.GetValueOrDefault() +
                                       rw.AccPeriod11.GetValueOrDefault();
                }
                if (AccPeriod == 12)
                {
                    TotalDebetKredit = rw.AccOpening + rw.AccPeriod1.GetValueOrDefault() +
                                       rw.AccPeriod2.GetValueOrDefault() +
                                       rw.AccPeriod3.GetValueOrDefault() +
                                       rw.AccPeriod4.GetValueOrDefault() +
                                       rw.AccPeriod5.GetValueOrDefault() +
                                       rw.AccPeriod6.GetValueOrDefault() +
                                       rw.AccPeriod7.GetValueOrDefault() +
                                       rw.AccPeriod8.GetValueOrDefault() +
                                       rw.AccPeriod9.GetValueOrDefault() +
                                       rw.AccPeriod10.GetValueOrDefault() +
                                       rw.AccPeriod11.GetValueOrDefault() +
                                       rw.AccPeriod12.GetValueOrDefault();
                }

                FinancialReport.tblGLAccountId = rw.tblGLAccountId;
                FinancialReport.AccPeriod = AccPeriod;
                FinancialReport.AccYear = AccYear;
                if (TotalDebetKredit < 0)
                {
                    FinancialReport.Debet = 0;
                    FinancialReport.Kredit = -1 * TotalDebetKredit;
                }
                else
                {
                    FinancialReport.Debet = TotalDebetKredit;
                    FinancialReport.Kredit = 0;
                }

                FinancialReports.Add(FinancialReport);
            }

            return View(FinancialReports.ToList());
        }

        // GET: tblGLAccPeriods/Create
        public ActionResult Create()
        {
            ViewBag.tblGLAccountId = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode");
            return View();
        }

        // POST: tblGLAccPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblGLAccPeriodId,AccYear,tblGLAccountId,AccOpening,AccPeriod1,AccPeriod2,AccPeriod3,AccPeriod4,AccPeriod5,AccPeriod6,AccPeriod7,AccPeriod8,AccPeriod9,AccPeriod10,AccPeriod11,AccPeriod12")] tblGLAccPeriod tblGLAccPeriod)
        {
            if (ModelState.IsValid)
            {
                db.tblGLAccPeriods.Add(tblGLAccPeriod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tblGLAccountId = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", tblGLAccPeriod.tblGLAccountId);
            return View(tblGLAccPeriod);
        }

        // GET: tblGLAccPeriods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccPeriod tblGLAccPeriod = db.tblGLAccPeriods.Find(id);
            if (tblGLAccPeriod == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblGLAccountId = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", tblGLAccPeriod.tblGLAccountId);
            return View(tblGLAccPeriod);
        }

        // POST: tblGLAccPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblGLAccPeriodId,AccYear,tblGLAccountId,AccOpening,AccPeriod1,AccPeriod2,AccPeriod3,AccPeriod4,AccPeriod5,AccPeriod6,AccPeriod7,AccPeriod8,AccPeriod9,AccPeriod10,AccPeriod11,AccPeriod12")] tblGLAccPeriod tblGLAccPeriod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLAccPeriod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tblGLAccountId = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", tblGLAccPeriod.tblGLAccountId);
            return View(tblGLAccPeriod);
        }

        // GET: tblGLAccPeriods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLAccPeriod tblGLAccPeriod = db.tblGLAccPeriods.Find(id);
            if (tblGLAccPeriod == null)
            {
                return HttpNotFound();
            }
            return View(tblGLAccPeriod);
        }

        // POST: tblGLAccPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGLAccPeriod tblGLAccPeriod = db.tblGLAccPeriods.Find(id);
            db.tblGLAccPeriods.Remove(tblGLAccPeriod);
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
