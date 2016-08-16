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
    public class tblDefaultsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblDefaults
        public ActionResult Index()
        {
            var tblDefaults = db.tblDefaults.Include(t => t.AccHutangBelumFaktur).Include(t => t.AccPiutangBelumFaktur).Include(t => t.AccPPNKeluaran).Include(t => t.AccPPNMasukan).Include(t => t.AccSelisihKurs).Include(t => t.AccUangMukaPembelian).Include(t => t.AccUangMukaPenjualan);
            if (tblDefaults.Count() > 0)
            {
                return RedirectToAction("Edit", new { id = tblDefaults.First().tblDefaultId });
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        // GET: tblDefaults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDefault tblDefault = db.tblDefaults.Find(id);
            if (tblDefault == null)
            {
                return HttpNotFound();
            }
            return View(tblDefault);
        }

        // GET: tblDefaults/Create
        public ActionResult Create()
        {
            ViewBag.CurrentPeriod = DropDownListUtility.PeriodDropDown(11);
            ViewBag.OpeningPeriod = DropDownListUtility.PeriodDropDown(11);
            ViewBag.CurrentYear = DropDownListUtility.YearDropDown(DateTime.Now.Year);
            ViewBag.OpeningYear = DropDownListUtility.YearDropDown(DateTime.Now.Year);

            ViewBag.AccHutangBelumFakturID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.AccPiutangBelumFakturID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.AccPPNKeluaranID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.AccPPNMasukanID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.AccSelisihKursID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.AccUangMukaPembelianID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.AccUangMukaPenjualanID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.JenisPersediaanCompoundID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan");
            ViewBag.JenisPersediaanRajanganID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan");
            ViewBag.GudangBeliID = new SelectList(db.Gudangs, "GudangID", "Lokasi");
            ViewBag.GudangJualID = new SelectList(db.Gudangs, "GudangID", "Lokasi");
            ViewBag.GudangProduksiID = new SelectList(db.Gudangs, "GudangID", "Lokasi");
            ViewBag.JenisPersediaanBahanBakuID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan");
            ViewBag.JenisPersediaanBarangJadiID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan");

            return View();
        }

        // POST: tblDefaults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblDefaultId,CurrentPeriod,CurrentYear,OpeningPeriod,OpeningYear,AccHutangBelumFakturID,AccPPNMasukanID,AccUangMukaPembelianID,AccSelisihKursID,AccPiutangBelumFakturID,AccPPNKeluaranID,AccUangMukaPenjualanID, GudangBeliID, GudangJualID, GudangProduksiID, JenisPersediaanRajanganID, JenisPersediaanCompoundID, JenisPersediaanBahanBakuID, JenisPersediaanBarangJadiID")] tblDefault tblDefault)
        {
            if (ModelState.IsValid)
            {
                db.tblDefaults.Add(tblDefault);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrentPeriod = DropDownListUtility.PeriodDropDown(tblDefault.CurrentPeriod );
            ViewBag.OpeningPeriod = DropDownListUtility.PeriodDropDown(tblDefault.OpeningPeriod );
            ViewBag.CurrentYear = DropDownListUtility.YearDropDown(tblDefault.CurrentYear );
            ViewBag.OpeningYear = DropDownListUtility.YearDropDown(tblDefault.OpeningYear );
            ViewBag.AccHutangBelumFakturID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccHutangBelumFakturID);
            ViewBag.AccPiutangBelumFakturID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPiutangBelumFakturID);
            ViewBag.AccPPNKeluaranID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPPNKeluaranID);
            ViewBag.AccPPNMasukanID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPPNMasukanID);
            ViewBag.AccSelisihKursID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccSelisihKursID);
            ViewBag.AccUangMukaPembelianID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccUangMukaPembelianID);
            ViewBag.AccUangMukaPenjualanID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccUangMukaPenjualanID);
            ViewBag.JenisPersediaanCompoundID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanCompoundID );
            ViewBag.JenisPersediaanRajanganID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanRajanganID );
            ViewBag.GudangBeliID = new SelectList(db.Gudangs, "GudangID", "Lokasi", tblDefault.GudangBeliID);
            ViewBag.GudangJualID = new SelectList(db.Gudangs, "GudangID", "Lokasi", tblDefault.GudangJualID);
            ViewBag.GudangProduksiID = new SelectList(db.Gudangs, "GudangID", "Lokasi", tblDefault.GudangProduksiID);
            ViewBag.JenisPersediaanBahanBakuID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanBahanBakuID );
            ViewBag.JenisPersediaanBarangJadiID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanBarangJadiID );
  
            return View(tblDefault);
        }

        // GET: tblDefaults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDefault tblDefault = db.tblDefaults.Find(id);
            if (tblDefault == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentPeriod = DropDownListUtility.PeriodDropDown(tblDefault.CurrentPeriod);
            ViewBag.OpeningPeriod = DropDownListUtility.PeriodDropDown(tblDefault.OpeningPeriod);
            ViewBag.CurrentYear = DropDownListUtility.YearDropDown(tblDefault.CurrentYear);
            ViewBag.OpeningYear = DropDownListUtility.YearDropDown(tblDefault.OpeningYear);
            ViewBag.AccHutangBelumFakturID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccHutangBelumFakturID);
            ViewBag.AccPiutangBelumFakturID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPiutangBelumFakturID);
            ViewBag.AccPPNKeluaranID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPPNKeluaranID);
            ViewBag.AccPPNMasukanID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPPNMasukanID);
            ViewBag.AccSelisihKursID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccSelisihKursID);
            ViewBag.AccUangMukaPembelianID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccUangMukaPembelianID);
            ViewBag.AccUangMukaPenjualanID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccUangMukaPenjualanID);
            ViewBag.GudangBeliID = new SelectList(db.Gudangs, "GudangID", "Lokasi", tblDefault.GudangBeliID);
            ViewBag.GudangJualID = new SelectList(db.Gudangs, "GudangID", "Lokasi", tblDefault.GudangJualID);
            ViewBag.GudangProduksiID = new SelectList(db.Gudangs, "GudangID", "Lokasi", tblDefault.GudangProduksiID);
            ViewBag.JenisPersediaanCompoundID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanCompoundID);
            ViewBag.JenisPersediaanRajanganID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanRajanganID);
            ViewBag.JenisPersediaanBahanBakuID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanBahanBakuID);
            ViewBag.JenisPersediaanBarangJadiID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanBarangJadiID);

            return View(tblDefault);
        }

        // POST: tblDefaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblDefaultId,CurrentPeriod,CurrentYear,OpeningPeriod,OpeningYear,AccHutangBelumFakturID,AccPPNMasukanID,AccUangMukaPembelianID,AccSelisihKursID,AccPiutangBelumFakturID,AccPPNKeluaranID,AccUangMukaPenjualanID, GudangBeliID, GudangJualID, GudangProduksiID, JenisPersediaanRajanganID, JenisPersediaanCompoundID, JenisPersediaanBahanBakuID, JenisPersediaanBarangJadiID, NoSuratJalan, NoSuratJalanPPN, NoSO, NoPO")] tblDefault tblDefault)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDefault).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPeriod = DropDownListUtility.PeriodDropDown(tblDefault.CurrentPeriod);
            ViewBag.OpeningPeriod = DropDownListUtility.PeriodDropDown(tblDefault.OpeningPeriod);
            ViewBag.CurrentYear = DropDownListUtility.YearDropDown(tblDefault.CurrentYear);
            ViewBag.OpeningYear = DropDownListUtility.YearDropDown(tblDefault.OpeningYear);
            ViewBag.AccHutangBelumFakturID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccHutangBelumFakturID);
            ViewBag.AccPiutangBelumFakturID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPiutangBelumFakturID);
            ViewBag.AccPPNKeluaranID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPPNKeluaranID);
            ViewBag.AccPPNMasukanID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccPPNMasukanID);
            ViewBag.AccSelisihKursID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccSelisihKursID);
            ViewBag.AccUangMukaPembelianID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccUangMukaPembelianID);
            ViewBag.AccUangMukaPenjualanID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblDefault.AccUangMukaPenjualanID);
            ViewBag.JenisPersediaanCompoundID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanCompoundID);
            ViewBag.JenisPersediaanRajanganID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanRajanganID);
            ViewBag.JenisPersediaanBahanBakuID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanBahanBakuID);
            ViewBag.JenisPersediaanBarangJadiID = new SelectList(db.JenisPersediaans.OrderBy(x => x.Keterangan), "JenisPersediaanID", "Keterangan", tblDefault.JenisPersediaanBarangJadiID);

            return View(tblDefault);
        }

        // GET: tblDefaults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDefault tblDefault = db.tblDefaults.Find(id);
            if (tblDefault == null)
            {
                return HttpNotFound();
            }
            return View(tblDefault);
        }

        // POST: tblDefaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDefault tblDefault = db.tblDefaults.Find(id);
            db.tblDefaults.Remove(tblDefault);
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

        public List<ViewNoRek> tblViewNoReks()
        {
            List<ViewNoRek> ViewNoReks = new List<ViewNoRek>();

            foreach (tblGLAccount rw in db.tblGLAccounts.OrderBy(x => x.AccCode))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(rw.AccCode.Trim().PadRight(10, ' '));
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
