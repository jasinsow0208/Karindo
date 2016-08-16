using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kramatdjati.Infrastructure;
using Kramatdjati.Models;
using System.Text;

namespace Kramatdjati.Controllers
{
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDBukuBesar")]
    public class BukuBesarController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: BukuBesar
        public ActionResult Index(int? Year, int? Period, int? tblGLAccountId)
        {
            List<ViewBukuBesar> BukuBesars = new List<ViewBukuBesar>();

            if (Year != null)
            {
                BukuBesars = CreateBukuBesar(Year, Period, tblGLAccountId);
                ViewBag.AccPeriod = DropDownListUtility.PeriodDropDown(Period);
                ViewBag.AccYear = DropDownListUtility.YearDropDown(Year);
                ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblGLAccountId);

                ViewBag.SaldoAwal = SaldoAwal((int)Year, (int)Period, (int)tblGLAccountId);
            }
            else
            {
                ViewBag.AccPeriod = DropDownListUtility.PeriodDropDown(11);
                ViewBag.AccYear = DropDownListUtility.YearDropDown(DateTime.Now.Year);
                ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
                ViewBag.SaldoAwal = 0;
            }

            return View(BukuBesars.ToList());
        }

        public ActionResult LihatBatch(int id)
        {
            var tblGLBatchDetails = db.tblGLBatchDetails.Where(m => m.tblGLBatchId == id);
            ViewBag.tblGLBatchId = id;
            return View(tblGLBatchDetails.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "AccYear,AccPeriod, tblGLAccountId")] ViewBukuBesar ViewBukuBesar)
        {

            return RedirectToAction("Index", new { Year = ViewBukuBesar.AccYear, Period = ViewBukuBesar.AccPeriod, tblGLAccountId = ViewBukuBesar.tblGLAccountId });

        }

        private List<ViewBukuBesar> CreateBukuBesar(int? Year, int? Period, int? tblGLAccountId)
        {
            List<ViewBukuBesar> BukuBesars = new List<ViewBukuBesar>();
            decimal SldAwal = SaldoAwal((int)Year, (int)Period, (int)tblGLAccountId);
            decimal Saldo = SldAwal;

            foreach (tblGLBatchDetail rw in db.tblGLBatchDetails.Where(x => x.tblGLBatch.Posting == true &&
                                                                          x.tblGLBatch.AccPeriod == Period &&
                                                                          x.tblGLBatch.AccYear == Year &&
                                                                          x.tblGLAccountId == tblGLAccountId).
                                                                OrderBy(x => x.TglTransaksi).
                                                                OrderBy(x => x.tblGLBatch.TglKomputer))
            {
                Saldo = Saldo + rw.Debet - rw.Kredit;
                ViewBukuBesar BukuBesar = new ViewBukuBesar();
                BukuBesar.tblGLBatchId = rw.tblGLBatchId;
                BukuBesar.AccYear = (int)Year;
                BukuBesar.AccPeriod = (int)Period;
                BukuBesar.TglKomputer = rw.tblGLBatch.TglKomputer;
                BukuBesar.NoRef = rw.NoRef;
                BukuBesar.tblGLAccountId = (int)tblGLAccountId;
                BukuBesar.AccName = rw.tblGLAccount.AccName;
                BukuBesar.TglTransaksi = rw.TglTransaksi;
                BukuBesar.Keterangan = rw.Keterangan;
                BukuBesar.Debet = rw.Debet;
                BukuBesar.Kredit = rw.Kredit;
                BukuBesar.Saldo = Saldo;
                BukuBesar.SaldoAwal = SldAwal;

                BukuBesars.Add(BukuBesar);
            }
            return BukuBesars;
        }

        private decimal SaldoAwal(int Year, int Perioda, int tblGLAccountId)
        {
            decimal SaldoAwal = 0;
            tblGLAccPeriod tblSaldoPeriod = db.tblGLAccPeriods.Where(x => x.tblGLAccountId == tblGLAccountId &&
                                                                          x.AccYear == Year).
                                                                SingleOrDefault();

            decimal Opening = (tblSaldoPeriod.AccOpening == null) ? 0 : (decimal)tblSaldoPeriod.AccOpening;
            decimal Period1 = (tblSaldoPeriod.AccPeriod1 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod1;
            decimal Period2 = (tblSaldoPeriod.AccPeriod2 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod2;
            decimal Period3 = (tblSaldoPeriod.AccPeriod3 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod3;
            decimal Period4 = (tblSaldoPeriod.AccPeriod4 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod4;
            decimal Period5 = (tblSaldoPeriod.AccPeriod5 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod5;
            decimal Period6 = (tblSaldoPeriod.AccPeriod6 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod6;
            decimal Period7 = (tblSaldoPeriod.AccPeriod7 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod7;
            decimal Period8 = (tblSaldoPeriod.AccPeriod8 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod8;
            decimal Period9 = (tblSaldoPeriod.AccPeriod9 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod9;
            decimal Period10 = (tblSaldoPeriod.AccPeriod10 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod10;
            decimal Period11 = (tblSaldoPeriod.AccPeriod11 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod11;
            decimal Period12 = (tblSaldoPeriod.AccPeriod12 == null) ? 0 : (decimal)tblSaldoPeriod.AccPeriod12;

            if (Perioda == 1)
            {
                SaldoAwal = Opening;
            };
            if (Perioda == 2)
            {
                SaldoAwal = Opening + Period1;
            };
            if (Perioda == 3)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2;
            };
            if (Perioda == 4)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2 +
                             Period3;
            };
            if (Perioda == 5)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2 +
                             Period3 +
                             Period4;
            };
            if (Perioda == 6)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2 +
                             Period3 +
                             Period4 +
                             Period5;
            };
            if (Perioda == 7)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2 +
                             Period3 +
                             Period4 +
                             Period5 +
                             Period6;
            };
            if (Perioda == 8)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2 +
                             Period3 +
                             Period4 +
                             Period5 +
                             Period6 +
                             Period7;
            };
            if (Perioda == 9)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2 +
                             Period3 +
                             Period4 +
                             Period5 +
                             Period6 +
                             Period7 +
                             Period8;
            };
            if (Perioda == 10)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2 +
                             Period3 +
                             Period4 +
                             Period5 +
                             Period6 +
                             Period7 +
                             Period8 +
                             Period9;
            };
            if (Perioda == 11)
            {
                SaldoAwal = (decimal)(tblSaldoPeriod.AccOpening) +
                             Period1 +
                             Period2 +
                             Period3 +
                             Period4 +
                             Period5 +
                             Period6 +
                             Period7 +
                             Period8 +
                             Period9 +
                             Period10;
            };
            if (Perioda == 12)
            {
                SaldoAwal = Opening +
                             Period1 +
                             Period2 +
                             Period3 +
                             Period4 +
                             Period5 +
                             Period6 +
                             Period7 +
                             Period8 +
                             Period9 +
                             Period10 +
                             Period11;
            };

            return SaldoAwal;

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