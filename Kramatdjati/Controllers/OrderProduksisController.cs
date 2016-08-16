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
    public class OrderProduksisController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: OrderProduksis
        public ActionResult Index()
        {
            var salesOrderRincians = db.SalesOrderRincians.Where(x=>x.statusLengkap == false).Include(s => s.bahanBaku).Include(s => s.SalesOrder).Include(s => s.statusProduksi);
            return View(salesOrderRincians.ToList());
        }

        // GET: OrderProduksis/Details/5
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

        // GET: OrderProduksis/Create
        public ActionResult Create()
        {
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku");
            ViewBag.SalesOrderID = new SelectList(db.SalesOrders, "SalesOrderID", "NoSO");
            ViewBag.StatusProduksiID = new SelectList(db.StatusProduksis, "StatusProduksiID", "Status");
            return View();
        }

        // POST: OrderProduksis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesOrderRincianID,SalesOrderID,BahanBakuID,HargaJual,Jumlah,Keterangan,JmlDiproduksi,StatusProduksiID,JmlYangSudahDiKirim,statusLengkap")] SalesOrderRincian salesOrderRincian)
        {
            if (ModelState.IsValid)
            {
                db.SalesOrderRincians.Add(salesOrderRincian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", salesOrderRincian.BahanBakuID);
            ViewBag.SalesOrderID = new SelectList(db.SalesOrders, "SalesOrderID", "NoSO", salesOrderRincian.SalesOrderID);
            ViewBag.StatusProduksiID = new SelectList(db.StatusProduksis, "StatusProduksiID", "Status", salesOrderRincian.StatusProduksiID);
            return View(salesOrderRincian);
        }

        // GET: OrderProduksis/Edit/5
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
            ViewBag.SalesOrderID = new SelectList(db.SalesOrders, "SalesOrderID", "NoSO", salesOrderRincian.SalesOrderID);
            ViewBag.StatusProduksiID = new SelectList(db.StatusProduksis, "StatusProduksiID", "Status", salesOrderRincian.StatusProduksiID);
            return View(salesOrderRincian);
        }

        // POST: OrderProduksis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesOrderRincianID,SalesOrderID,BahanBakuID,HargaJual,Jumlah,Keterangan,JmlDiproduksi,StatusProduksiID,JmlYangSudahDiKirim,statusLengkap")] SalesOrderRincian salesOrderRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesOrderRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", salesOrderRincian.BahanBakuID);
            ViewBag.SalesOrderID = new SelectList(db.SalesOrders, "SalesOrderID", "NoSO", salesOrderRincian.SalesOrderID);
            ViewBag.StatusProduksiID = new SelectList(db.StatusProduksis, "StatusProduksiID", "Status", salesOrderRincian.StatusProduksiID);
            return View(salesOrderRincian);
        }

        // GET: OrderProduksis/Delete/5
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

        // POST: OrderProduksis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesOrderRincian salesOrderRincian = db.SalesOrderRincians.Find(id);
            db.SalesOrderRincians.Remove(salesOrderRincian);
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
