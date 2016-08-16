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
using System.Data.SqlClient;

namespace Kramatdjati.Controllers
{
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDBahanBaku")]
    public class BahanBakusController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: BahanBakus
        public ActionResult Index()
        {
            return View(db.BahanBakus.ToList());
        }

        // GET: BahanBakus/Details/5
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

        // GET: BahanBakus/Create
        public ActionResult Create()
        {
            int InitDivisiId = 0;
            if (db.Divisis.Count() > 0)
            {
                InitDivisiId = db.Divisis.OrderBy(x => x.DivisiId).First().DivisiId;
            }
            else
            {
                InitDivisiId = 0;
            };

            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan");
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan");
            ViewBag.NoRekCOGSID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.NoRekSaleID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.NoRekInventoryID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan");
            ViewBag.DepartemenId = new SelectList(db.Departemen.Where(x => x.DivisiId == InitDivisiId), "DepartemenId", "Keterangan");
            return View();
        }

        // POST: BahanBakus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BahanBakuID,KodeBahanBaku,Keterangan,Stok, SatuanID, HargaJual, StokItem, PurchaseItem, SaleItem, NoRekCOGSID, NoRekSaleID, NoRekInventoryID, JenisPersediaanID, DivisiId, DepartemenId, Size, BarangJadi")] BahanBaku bahanBaku)
        {
            if (ModelState.IsValid)
            {
               

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.BahanBakus.Add(bahanBaku);
                        db.SaveChanges();

                        int bahanBakuID = bahanBaku.BahanBakuID;

                        var tblDefault = db.tblDefaults.FirstOrDefault();
                        int compoundID = tblDefault.JenisPersediaanCompoundID;
                        int gudangProduksiID = tblDefault.GudangProduksiID;
                        int gudangBahanID = tblDefault.GudangBeliID;

                        int gudangID;
                        if (bahanBaku.JenisPersediaanID == compoundID)
                        {
                            gudangID = gudangProduksiID;
                        }
                        else
                        {
                            gudangID = gudangBahanID;
                        };

                        GudangBahanBaku gudangBahanBaku = new GudangBahanBaku();
                        gudangBahanBaku.BahanBakuID = bahanBakuID;
                        gudangBahanBaku.GudangID = gudangID;
                        gudangBahanBaku.Jumlah = 0;

                        db.GudangBahanBakus.Add(gudangBahanBaku);
                        db.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan");
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan");
            ViewBag.NoRekCOGSID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.NoRekSaleID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.NoRekInventoryID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan");
            ViewBag.DepartemenId = new SelectList(db.Departemen.Where(x => x.DivisiId == 1), "DepartemenId", "Keterangan");
            return View(bahanBaku);
        }

        //GET:BahanBakus/BarangJadi
        public ActionResult BarangJadi()
        {
            int InitDivisiId = 0;
            if (db.Divisis.Count() > 0)
            {
                InitDivisiId = db.Divisis.OrderBy(x => x.DivisiId).First().DivisiId;
            }
            else
            {
                InitDivisiId = 0;
            };

            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan");
            //ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan");
            ViewBag.NoRekCOGSID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.NoRekSaleID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.NoRekInventoryID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan");
            ViewBag.DepartemenId = new SelectList(db.Departemen.Where(x => x.DivisiId == InitDivisiId), "DepartemenId", "Keterangan");

            ViewBarangJadi BarangJadi = new ViewBarangJadi();
            return View(BarangJadi);
        }

        // POST: BahanBakus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BarangJadi([Bind(Include = "KodeBahanBaku,Keterangan, SatuanID, StokItem, PurchaseItem, SaleItem, NoRekCOGSID, NoRekSaleID, NoRekInventoryID, JenisPersediaanID, DivisiId, DepartemenId, Size")] ViewBarangJadi barangJadi)
        {
            if (ModelState.IsValid)
            {
                string ukuran;
                string[] ukurans;
                if (barangJadi.Size.Length > 0)
                {
                    ukuran = barangJadi.Size.ToString().Trim();
                    ukurans = ukuran.Split(',');
                }
                else
                {
                    ukuran = "";
                    ukurans = ukuran.Split(',');
                }

                var tblDefault = db.tblDefaults.FirstOrDefault();
                int jenisPersediaanBarangJadiID = tblDefault.JenisPersediaanBarangJadiID;
                int gudangBarangJadiID = tblDefault.GudangJualID;

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (string r in ukurans)
                        {
                            BahanBaku bahanBaku = new BahanBaku();
                            bahanBaku.KodeBahanBaku = barangJadi.KodeBahanBaku.ToString().Trim() + '-' + r.Trim();
                            if (barangJadi.Keterangan == null)
                            {
                                bahanBaku.Keterangan = barangJadi.KodeBahanBaku.ToString().Trim() + "-" + r.Trim();
                            }
                            else
                            {
                                bahanBaku.Keterangan = barangJadi.Keterangan.ToString().Trim() + "-" + r.Trim();
                            };
                            bahanBaku.SatuanID = barangJadi.SatuanID;
                            bahanBaku.StokItem = barangJadi.StokItem;
                            bahanBaku.PurchaseItem = barangJadi.PurchaseItem;
                            bahanBaku.SaleItem = barangJadi.SaleItem;
                            bahanBaku.NoRekCOGSID = barangJadi.NoRekCOGSID;
                            bahanBaku.NoRekSaleID = barangJadi.NoRekSaleID;
                            bahanBaku.NoRekInventoryID = barangJadi.NoRekInventoryID;
                            bahanBaku.JenisPersediaanID = jenisPersediaanBarangJadiID;
                            bahanBaku.DivisiId = barangJadi.DivisiId;
                            bahanBaku.DepartemenId = barangJadi.DepartemenId;
                            bahanBaku.Size = r.Trim();
                            bahanBaku.KodeBarangJadi = barangJadi.KodeBahanBaku;
                            bahanBaku.BarangJadi = true;

                            db.BahanBakus.Add(bahanBaku);
                            db.SaveChanges();

                            int bahanBakuID = bahanBaku.BahanBakuID;

                            GudangBahanBaku gudangBahanBaku = new GudangBahanBaku();
                            gudangBahanBaku.BahanBakuID = bahanBakuID;
                            gudangBahanBaku.GudangID = gudangBarangJadiID;

                            db.GudangBahanBakus.Add(gudangBahanBaku);
                            db.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
               
                return RedirectToAction("Index");
            }

            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan");
            //ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan");
            ViewBag.NoRekCOGSID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.NoRekSaleID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.NoRekInventoryID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc");
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan");
            ViewBag.DepartemenId = new SelectList(db.Departemen.Where(x => x.DivisiId == 1), "DepartemenId", "Keterangan");
            return View(barangJadi);
        }
        // GET: BahanBakus/Edit/5
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

            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan", bahanBaku.SatuanID);
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan", bahanBaku.JenisPersediaanID);
            ViewBag.NoRekCOGSID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc", bahanBaku.NoRekCOGSID);
            ViewBag.NoRekSaleID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc", bahanBaku.NoRekSaleID);
            ViewBag.NoRekInventoryID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc", bahanBaku.NoRekInventoryID);
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan", bahanBaku.DivisiId);
            ViewBag.DepartemenId = new SelectList(db.Departemen.Where(x => x.DivisiId == bahanBaku.DivisiId), "DepartemenId", "Keterangan", bahanBaku.DepartemenId);

            return View(bahanBaku);
        }

        // POST: BahanBakus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BahanBakuID,KodeBahanBaku,Keterangan,Stok, SatuanID, Discontinue, HargaJual, StokItem, PurchaseItem, SaleItem, NoRekCOGSID, NoRekSaleID, NoRekInventoryID, JenisPersediaanID, DivisiId, DepartemenId, Size, BarangJadi")] BahanBaku bahanBaku)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bahanBaku).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SatuanID = new SelectList(db.Satuan, "SatuanID", "Keterangan", bahanBaku.SatuanID);
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan", bahanBaku.JenisPersediaanID);
            ViewBag.NoRekCOGSID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc", bahanBaku.NoRekCOGSID);
            ViewBag.NoRekSaleID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc", bahanBaku.NoRekSaleID);
            ViewBag.NoRekInventoryID = new SelectList(tblViewNoReks(), "tblGLAccountID", "AccCodeDesc", bahanBaku.NoRekInventoryID);
            ViewBag.DivisiId = new SelectList(db.Divisis, "DivisiId", "Keterangan", bahanBaku.DivisiId);
            ViewBag.DepartemenId = new SelectList(db.Departemen.Where(x => x.DivisiId == bahanBaku.DivisiId), "DepartemenId", "Keterangan", bahanBaku.DepartemenId);

            return View(bahanBaku);
        }

        // GET: BahanBakus/Delete/5
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

        // POST: BahanBakus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BahanBaku bahanBaku = db.BahanBakus.Find(id);
            db.BahanBakus.Remove(bahanBaku);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult InfoKalkulasi()
        {
            return View();
        }

        public ActionResult KalkulasiKartuStok()
        {
  
            var Data = db.Database.ExecuteSqlCommand("spKalkulasiSaldoKartuStokAll");

            ViewBag.Info = "Kalkulasi Kartu Stok Berhasil";
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
