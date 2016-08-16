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
    public class SalesOrderRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SalesOrderRincians
        public ActionResult Index(int id)
        {
            var salesOrder = db.SalesOrders.Where(x => x.SalesOrderID  == id).Include(p => p.contact).FirstOrDefault();

            ViewBag.TglKirim = salesOrder.TglPengiriman.ToShortDateString();
            ViewBag.TglTransaksi = salesOrder.TglPesan.ToShortDateString();
            //ViewBag.NoPO = salesOrder.NoSO.ToString();
            ViewBag.Supplier = salesOrder.contact.Perusahaan;
            ViewBag.SalesOrderID = id;
            ViewBag.Catatan = salesOrder.Catatan;
            var salesOrderRincians = db.SalesOrderRincians.Where(x => x.SalesOrderID  == id).Include(s => s.bahanBaku).Include(s => s.SalesOrder);
            return View(salesOrderRincians.ToList());
        }

        // GET: SalesOrderRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderRincian salesOrderRincian = db.SalesOrderRincians.Find(id);
            if (salesOrderRincian == null)
            {
                return HttpNotFound();
            }
            return View(salesOrderRincian);
        }

        // GET: SalesOrderRincians/Create
        public ActionResult Create(int id)
        {
            ViewSalesOrderRincian salesOrderRincian = new ViewSalesOrderRincian();
            var BarangJadi = from a in db.BahanBakus
                             where a.BarangJadi == true
                             select new { KodeBahanBaku = a.KodeBarangJadi };

            ViewBag.KodeBahanBaku = new SelectList(BarangJadi.Distinct(), "KodeBahanBaku", "KodeBahanBaku");
            ViewBag.Size = new SelectList(db.BahanBakus.Where(x => x.BarangJadi == true), "Size", "Size");
            ViewBag.SalesOrderID = id;

            return View(salesOrderRincian );
        }

        // POST: SalesOrderRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesOrderID,KodeBahanBaku,Size, HargaJual,Jumlah,Keterangan")] ViewSalesOrderRincian viewSalesOrderRincian)
        {
            if (ModelState.IsValid)
            {
                SalesOrderRincian salesOrderRincian = new SalesOrderRincian();
                salesOrderRincian.SalesOrderID = viewSalesOrderRincian.SalesOrderID;
                salesOrderRincian.HargaJual = viewSalesOrderRincian.HargaJual;
                salesOrderRincian.Jumlah = viewSalesOrderRincian.Jumlah;
                salesOrderRincian.Keterangan = viewSalesOrderRincian.Keterangan;

                string strKodeBahanBaku = viewSalesOrderRincian.KodeBahanBaku;
                string strSize = viewSalesOrderRincian.Size;
                int BahanBakuID = 0;
                IQueryable<BahanBaku> BahanBakus = db.BahanBakus.Where(x => x.KodeBarangJadi == strKodeBahanBaku  && x.Size == strSize );
                foreach (BahanBaku r in BahanBakus)
                {
                    BahanBakuID = r.BahanBakuID;
                };

                salesOrderRincian.BahanBakuID = BahanBakuID;

                db.SalesOrderRincians.Add(salesOrderRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = viewSalesOrderRincian.SalesOrderID });
            }

            var BarangJadi = from a in db.BahanBakus
                             where a.BarangJadi == true
                             select new { KodeBahanBaku = a.KodeBarangJadi };

            ViewBag.KodeBahanBaku = new SelectList(BarangJadi.Distinct(), "KodeBahanBaku", "KodeBahanBaku", viewSalesOrderRincian.KodeBahanBaku );
            ViewBag.Size = new SelectList(db.BahanBakus.Where(x => x.BarangJadi == true), "Size", "Size", viewSalesOrderRincian.Size );
            ViewBag.SalesOrderID = viewSalesOrderRincian.SalesOrderID ;

            return View(viewSalesOrderRincian);
        }

        // GET: SalesOrderRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderRincian salesOrderRincian = db.SalesOrderRincians.Find(id);
            if (salesOrderRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", salesOrderRincian.BahanBakuID);
            ViewBag.SalesOrderID =  salesOrderRincian.SalesOrderID;
            return View(salesOrderRincian);
        }

        // POST: SalesOrderRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesOrderRincianID,SalesOrderID,BahanBakuID,HargaJual,Jumlah,Keterangan,JmlYangSudahDiKirim,statusLengkap")] SalesOrderRincian salesOrderRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesOrderRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = salesOrderRincian.SalesOrderID });
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", salesOrderRincian.BahanBakuID);
            ViewBag.SalesOrderID = salesOrderRincian.SalesOrderID ;
            return View(salesOrderRincian);
        }

        // GET: SalesOrderRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderRincian salesOrderRincian = db.SalesOrderRincians.Find(id);
            if (salesOrderRincian == null)
            {
                return HttpNotFound();
            }
            return View(salesOrderRincian);
        }

        // POST: SalesOrderRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesOrderRincian salesOrderRincian = db.SalesOrderRincians.Find(id);
            db.SalesOrderRincians.Remove(salesOrderRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = salesOrderRincian.SalesOrderID });
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
