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
using System.Globalization;
using System.Data.SqlClient;

namespace Kramatdjati.Controllers
{
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDOpeningBalance")]
    public class tblGLOpeningBalancesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblGLOpeningBalances
        public ActionResult Index()
        {

            int JmlRec = db.tblGLOpeningBalances.Count();

            if (JmlRec == 0)
            {
                return Create();
            }
            else
            {
                tblGLOpeningBalance rowOpeningBalance = db.tblGLOpeningBalances.FirstOrDefault();
                if (rowOpeningBalance.Posting)
                {
                    return InfoOpeningBalance(rowOpeningBalance.tblGLOpeningBalanceId);
                }
                else
                {
                    return Edit(rowOpeningBalance.tblGLOpeningBalanceId);
                }
            }
        }

        // GET: tblGLOpeningBalances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLOpeningBalance tblGLOpeningBalance = db.tblGLOpeningBalances.Find(id);
            if (tblGLOpeningBalance == null)
            {
                return HttpNotFound();
            }
            return View("Details", tblGLOpeningBalance);
        }

        // GET: tblGLOpeningBalances/Create
        public ActionResult Create()
        {
            tblGLOpeningBalance tblGLOpeningBalance = new tblGLOpeningBalance();
            ViewBag.AccPeriod = Months;
            GetYears();
            return View("Create", tblGLOpeningBalance);
        }

        // POST: tblGLOpeningBalances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblGLOpeningBalanceId,AccPeriod,AccYear,Posting")] tblGLOpeningBalance tblGLOpeningBalance)
        {
            if (ModelState.IsValid)
            {
                db.tblGLOpeningBalances.Add(tblGLOpeningBalance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccPeriod = Months;
            GetYears();
            return View(tblGLOpeningBalance);
        }

        // GET: tblGLOpeningBalances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLOpeningBalance tblGLOpeningBalance = db.tblGLOpeningBalances.Find(id);
            if (tblGLOpeningBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccPeriod = Months;
            GetYears();

            decimal TotalKredit = tblGLOpeningBalance.tblGLOpeningBalanceDetails.Sum(m => m.Kredit);
            decimal TotalDebet = tblGLOpeningBalance.tblGLOpeningBalanceDetails.Sum(m => m.Debet);

            if (TotalDebet != TotalKredit)
            {
                ViewBag.Info = string.Format("Total Debet {0:N2}, Total Kredit {1:N2}", TotalDebet, TotalKredit);
            }
            else
            {
                ViewBag.Info = "";
            };
            return View("Edit", tblGLOpeningBalance);
        }

        // POST: tblGLOpeningBalances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblGLOpeningBalanceId,AccPeriod,AccYear,Posting")] tblGLOpeningBalance tblGLOpeningBalance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLOpeningBalance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblGLOpeningBalance);
        }

        // GET: tblGLOpeningBalances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLOpeningBalance tblGLOpeningBalance = db.tblGLOpeningBalances.Find(id);
            if (tblGLOpeningBalance == null)
            {
                return HttpNotFound();
            }
            return View(tblGLOpeningBalance);
        }

        // POST: tblGLOpeningBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGLOpeningBalance tblGLOpeningBalance = db.tblGLOpeningBalances.Find(id);
            db.tblGLOpeningBalances.Remove(tblGLOpeningBalance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLOpeningBalance tblGLOpeningBalance = db.tblGLOpeningBalances.Find(id);
            if (tblGLOpeningBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccPeriod = Months;
            GetYears();


            return View("Details", tblGLOpeningBalance);
        }

        public ActionResult PostingConfirm(int id)
        {
           
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spOpeningBalancePosting @tblGLOpeningBalanceId, @ReturnValue OUT",
                                                        new SqlParameter("@tblGLOpeningBalanceId", id),
                                                        ReturnParameter);

            tblGLOpeningBalance tblGLOpeningBalance = db.tblGLOpeningBalances.Find(id);
            if (tblGLOpeningBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccPeriod = Months;
            GetYears();

            if (ReturnParameter.Value.ToString() == "0")
            {
                ViewBag.Info = "Posting Berhasil";
                ViewBag.id = id;
                return View("PostingBerhasil", tblGLOpeningBalance);
            }
            else
            {
                ViewBag.Info = "Posting Gagal";
            };
       
            return View("Details", tblGLOpeningBalance);
        }

        public ActionResult InfoOpeningBalance(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblGLOpeningBalance tblGLOpeningBalance = db.tblGLOpeningBalances.Find(id);
            if (tblGLOpeningBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccPeriod = Months;
            GetYears();

            return View("InfoOpeningBalance", tblGLOpeningBalance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public IEnumerable<SelectListItem> Months
        {
            get
            {
                return DateTimeFormatInfo
                       .InvariantInfo
                       .MonthNames
                       .Select((monthName, index) => new SelectListItem
                       {
                           Value = (index + 1).ToString(),
                           Text = monthName
                       });
            }
        }

        private void GetYears()
        {
            ViewBag.AccYear = Enumerable.Range(DateTime.Now.Year, 4);
        }

    }
}
