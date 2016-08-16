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
    public class PengeluaranGudangRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PengeluaranGudangRincians
        public ActionResult Index(int id)
        {
            var suratJalan = db.SuratJalans.Where(x => x.SuratJalanID == id).Include(p => p.contact ).FirstOrDefault();

            ViewBag.TglSuratJalan = suratJalan.tglSuratJalan.ToShortDateString();
            ViewBag.TglTransaksi = suratJalan.TglTransaksi.ToShortDateString();
            //ViewBag.NoSO = suratJalan.NoSO.ToString();
            ViewBag.Customer = suratJalan.contact.Perusahaan ;
            ViewBag.NoSuratJalan = suratJalan.NoSuratJalan;
            ViewBag.SuratJalanID = id;
            ViewBag.Catatan = suratJalan.Catatan;
            var suratJalanRincians = db.SuratJalanRincians.Where(x => x.SuratJalanID == id).Include(s => s.salesOrderRincian).Include(s => s.suratJalan);
            return View(suratJalanRincians.ToList());
        }

        // GET: PengeluaranGudangRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanRincian suratJalanRincian = db.SuratJalanRincians.Find(id);
            if (suratJalanRincian == null)
            {
                return HttpNotFound();
            }
            string KodeBarang = suratJalanRincian.salesOrderRincian.bahanBaku.KodeBahanBaku.ToString();
            string NamaBarang = suratJalanRincian.salesOrderRincian.bahanBaku.Keterangan.ToString();
            ViewBag.Barang = string.Format("{0} ({1})", NamaBarang.Trim(), KodeBarang.Trim());

            return View(suratJalanRincian);
        }

        // GET: PengeluaranGudangRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanRincian suratJalanRincian = db.SuratJalanRincians.Find(id);
            if (suratJalanRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.SuratJalanID = suratJalanRincian.SuratJalanID;
            string KodeBarang = suratJalanRincian.salesOrderRincian.bahanBaku.KodeBahanBaku.ToString();
            string NamaBarang = suratJalanRincian.salesOrderRincian.bahanBaku.Keterangan.ToString();
            ViewBag.Barang = string.Format("{0} ({1})", NamaBarang.Trim(), KodeBarang.Trim());

            return View(suratJalanRincian);
        }

        // POST: PengeluaranGudangRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuratJalanRincianID,SuratJalanID,SalesOrderRincianID,JumlahDikirim,Kirim")] SuratJalanRincian suratJalanRincian)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var SuratJalanOri = db.SuratJalanRincians.AsNoTracking().Where(x=>x.SuratJalanRincianID ==suratJalanRincian.SuratJalanRincianID).FirstOrDefault() ;

                        if (SuratJalanOri.JumlahDikirim != suratJalanRincian.JumlahDikirim)
                        {
                            SuratJalanLog suratJalanLog = new SuratJalanLog();
                            suratJalanLog.Keterangan =  string.Format("No. Surat Jalan {2} tgl {3:dd/MM/yyyy} Ubah jml barang dari {0:N5} menjadi {1:N5}", SuratJalanOri.JumlahDikirim , suratJalanRincian.JumlahDikirim, SuratJalanOri.suratJalan.NoSuratJalan , SuratJalanOri.suratJalan.tglSuratJalan )   ;
                            suratJalanLog.Operator = User.Identity.Name;
                            suratJalanLog.TglProses = DateTime.Now;
                            suratJalanLog.SuratJalanRincianID = suratJalanRincian.SuratJalanRincianID ;

                            db.Entry(suratJalanLog).State = EntityState.Added;
                            db.SaveChanges();
                        }

                        SuratJalanOri = null;

                        db.Entry(suratJalanRincian).State = EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();

                        ViewBag.Info = "Proses tutup surat jalan Berhasil";
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        ViewBag.Info = "Proses tutup surat jalan Tidak berhasil " + e.Message.Trim();

                    }
                }
                return RedirectToAction("Index", new { id = suratJalanRincian.SuratJalanID });
            }

            ViewBag.SuratJalanID = suratJalanRincian.SuratJalanID;
            string KodeBarang = suratJalanRincian.salesOrderRincian.bahanBaku.KodeBahanBaku.ToString();
            string NamaBarang = suratJalanRincian.salesOrderRincian.bahanBaku.Keterangan.ToString();
            ViewBag.Barang = string.Format("{0} ({1})", NamaBarang.Trim(), KodeBarang.Trim());
            return View(suratJalanRincian);
        }

        public ActionResult Kirim(int id)
        {
            SuratJalanRincian suratJalanRincian = db.SuratJalanRincians.Find(id);
            if (suratJalanRincian == null)
            {
                return HttpNotFound();
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    suratJalanRincian.Kirim = true;
                    db.Entry(suratJalanRincian).State = EntityState.Modified;
                    db.SaveChanges();

                    transaction.Commit();
                    ViewBag.SuratJalanID = suratJalanRincian.SuratJalanID;
                    ViewBag.Info = "Pengiriman Berhasil";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.SuratJalanID = suratJalanRincian.SuratJalanID;
                    ViewBag.Info = "Pengiriman Tidak berhasil " + e.Message.Trim();
                }
            }
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
