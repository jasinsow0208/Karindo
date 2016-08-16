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
    public class BahanBakuPerGudangController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: BahanBakuPerGudang
        public ActionResult Index(int id)
        {
            ViewBag.GudangID = id;
            ViewBag.Gudang = db.Gudangs.Where(x => x.GudangID == id).FirstOrDefault().Lokasi;
            var bahanBakus = db.BahanBakus.Where(x=>x.gudangBahanBaku.FirstOrDefault().GudangID ==id).Include(b => b.Departemen).Include(b => b.Divisi).Include(b => b.JenisPersediaan).Include(b => b.NoRekCOGS).Include(b => b.NoRekInventory).Include(b => b.NoRekSale).Include(b => b.satuan);
            return View(bahanBakus.ToList());
        }

        // GET: BahanBakuPerGudang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BahanBaku bahanBaku = db.BahanBakus.Find(id);
            if (bahanBaku == null)
            {
                return HttpNotFound();
            }
            return View(bahanBaku);
        }

        // GET: BahanBakuPerGudang/Create
        public ActionResult Create()
        {
            ViewBag.DepartemenId = new SelectList(db.Departemen, "DepartemenID", "Keterangan");
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan");
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan");
            ViewBag.NoRekCOGSID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode");
            ViewBag.NoRekInventoryID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode");
            ViewBag.NoRekSaleID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode");
            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan");
            return View();
        }

        // POST: BahanBakuPerGudang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BahanBakuID,KodeBahanBaku,Keterangan,Stok,SatuanID,HargaRata2,HargaTerakhir,HargaJual,Discontinue,StokItem,PurchaseItem,SaleItem,NoRekCOGSID,NoRekSaleID,NoRekInventoryID,JenisPersediaanID,DivisiId,DepartemenId,BarangJadi,Size,KodeBarangJadi")] BahanBaku bahanBaku)
        {
            if (ModelState.IsValid)
            {
                db.BahanBakus.Add(bahanBaku);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartemenId = new SelectList(db.Departemen, "DepartemenID", "Keterangan", bahanBaku.DepartemenId);
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan", bahanBaku.DivisiId);
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan", bahanBaku.JenisPersediaanID);
            ViewBag.NoRekCOGSID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekCOGSID);
            ViewBag.NoRekInventoryID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekInventoryID);
            ViewBag.NoRekSaleID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekSaleID);
            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan", bahanBaku.SatuanID);
            return View(bahanBaku);
        }

        // GET: BahanBakuPerGudang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BahanBaku bahanBaku = db.BahanBakus.Find(id);
            if (bahanBaku == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartemenId = new SelectList(db.Departemen, "DepartemenID", "Keterangan", bahanBaku.DepartemenId);
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan", bahanBaku.DivisiId);
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan", bahanBaku.JenisPersediaanID);
            ViewBag.NoRekCOGSID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekCOGSID);
            ViewBag.NoRekInventoryID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekInventoryID);
            ViewBag.NoRekSaleID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekSaleID);
            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan", bahanBaku.SatuanID);
            return View(bahanBaku);
        }

        // POST: BahanBakuPerGudang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BahanBakuID,KodeBahanBaku,Keterangan,Stok,SatuanID,HargaRata2,HargaTerakhir,HargaJual,Discontinue,StokItem,PurchaseItem,SaleItem,NoRekCOGSID,NoRekSaleID,NoRekInventoryID,JenisPersediaanID,DivisiId,DepartemenId,BarangJadi,Size,KodeBarangJadi")] BahanBaku bahanBaku)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bahanBaku).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartemenId = new SelectList(db.Departemen, "DepartemenID", "Keterangan", bahanBaku.DepartemenId);
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan", bahanBaku.DivisiId);
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan", bahanBaku.JenisPersediaanID);
            ViewBag.NoRekCOGSID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekCOGSID);
            ViewBag.NoRekInventoryID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekInventoryID);
            ViewBag.NoRekSaleID = new SelectList(db.tblGLAccounts, "tblGLAccountId", "AccCode", bahanBaku.NoRekSaleID);
            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan", bahanBaku.SatuanID);
            return View(bahanBaku);
        }

        // GET: BahanBakuPerGudang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BahanBaku bahanBaku = db.BahanBakus.Find(id);
            if (bahanBaku == null)
            {
                return HttpNotFound();
            }
            return View(bahanBaku);
        }

        // POST: BahanBakuPerGudang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BahanBaku bahanBaku = db.BahanBakus.Find(id);
            db.BahanBakus.Remove(bahanBaku);
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
