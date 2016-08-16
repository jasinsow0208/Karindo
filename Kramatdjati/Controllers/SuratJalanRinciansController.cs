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
    public class SuratJalanRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SuratJalanRincians
        public ActionResult Index(int id)
        {
            var suratJalan = db.SuratJalans.Where(x => x.SuratJalanID == id).Include(p => p.contact ).FirstOrDefault();

            ViewBag.TglSuratJalan = suratJalan.tglSuratJalan.ToShortDateString();
            ViewBag.TglTransaksi = suratJalan.TglTransaksi.ToShortDateString();
            ViewBag.NoSO = suratJalan.NoSuratJalan ;
            ViewBag.Customer = suratJalan.contact.Perusahaan;
            ViewBag.Alamat = suratJalan.contact.Alamat;
            ViewBag.Kota = suratJalan.contact.Kota;
            ViewBag.SuratJalanID = id;
            ViewBag.Catatan = suratJalan.Catatan;
            var suratJalanRincians = db.SuratJalanRincians.Where(x => x.SuratJalanID == id).Include(s => s.salesOrderRincian).Include(s => s.suratJalan);
            return View(suratJalanRincians.ToList());
        }

        public ActionResult Cetak(int id)
        {
            var suratJalan = db.SuratJalans.Where(x => x.SuratJalanID == id).Include(p => p.contact ).FirstOrDefault();

            ViewBag.TglSuratJalan = suratJalan.tglSuratJalan.ToShortDateString();
            ViewBag.TglTransaksi = suratJalan.TglTransaksi.ToShortDateString();
            ViewBag.NoSO = suratJalan.NoSuratJalan ;
            ViewBag.Customer = suratJalan.contact.Perusahaan;
            ViewBag.Alamat = suratJalan.contact.Alamat;
            ViewBag.Kota = suratJalan.contact.Kota;
            ViewBag.SuratJalanID = id;
            ViewBag.Catatan = suratJalan.Catatan;
            var suratJalanRincians = db.SuratJalanRincians.Where(x => x.SuratJalanID == id).Include(s => s.salesOrderRincian).Include(s => s.suratJalan);

            foreach (SuratJalanRincian rw in suratJalanRincians)
            {
                int JmlPerPak = JmlPcsPerMm(rw.salesOrderRincian.bahanBaku.Size);
                if (JmlPerPak == 0)
                {
                    JmlPerPak = 1;
                };

                decimal decJmlPak = rw.JumlahDikirim / (decimal)JmlPerPak;
                int intJmlPak = (int)Math.Floor(decJmlPak);
                int intSisa = (int)rw.JumlahDikirim - JmlPerPak * intJmlPak;

                if (intSisa == 0)
                {
                    if (JmlPerPak == 1)
                    {
                        rw.salesOrderRincian.Keterangan = "-";
                    }
                    else
                    {
                        rw.salesOrderRincian.Keterangan = string.Format("{0} pak", intJmlPak);
                    };
                }
                else
                {
                    if (JmlPerPak == 1)
                    {
                        rw.salesOrderRincian.Keterangan = "-";
                    }
                    else
                    {
                        if (intJmlPak == 0)
                        {
                            rw.salesOrderRincian.Keterangan = "-";
                        }
                        else
                        {
                            rw.salesOrderRincian.Keterangan = string.Format("{0} pak + {1} lbr", intJmlPak, intSisa);
                        }

                    }

                };
            }
            return View(suratJalanRincians.ToList());
        }

        private int JmlPcsPerMm(string size)
        {
            var packing = db.Packings.Where(x => x.Size == size).FirstOrDefault();
            if (packing == null)
            {
                return 1;
            }
            else
            {
                return packing.JmlPerPacking;
            }
        }


        // GET: SuratJalanRincians/Details/5
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
            return View(suratJalanRincian);
        }

        // GET: SuratJalanRincians/Create
        public ActionResult Create(int id)
        {

            ViewBag.SalesOrderRincianID = slBahanBaku(id, 0);
            ViewBag.SuratJalanID = id;
            return View();
        }

        private SelectList slBahanBaku(int Id, int SalesOrderRincianID)
        {

            int contactID = db.SuratJalans.Find(Id).ContactID;

            var bahanBaku = from a in db.SalesOrderRincians
                            where a.SalesOrder.contact.ContactID  == contactID && a.statusLengkap != true
                            orderby a.bahanBaku.KodeBahanBaku 
                            select new { SalesOrderRincianID = a.SalesOrderRincianID, KodeBarang =  a.bahanBaku.KodeBahanBaku + " (" + a.SalesOrder.NoSO.ToString().Trim() + ")" };

            if (SalesOrderRincianID == 0)
            {
                return new SelectList(bahanBaku, "SalesOrderRincianID", "KodeBarang");
            }
            else
            {
                return new SelectList(bahanBaku, "SalesOrderRincianID", "KodeBarang", SalesOrderRincianID);
            }
        }
        // POST: SuratJalanRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuratJalanRincianID,SuratJalanID,SalesOrderRincianID,JumlahDikirim,Kirim")] SuratJalanRincian suratJalanRincian)
        {
            if (ModelState.IsValid)
            {
                db.SuratJalanRincians.Add(suratJalanRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = suratJalanRincian.SuratJalanID });
            }

            ViewBag.SuratJalanID = suratJalanRincian.SuratJalanID;
            ViewBag.SalesOrderRincianID = slBahanBaku(suratJalanRincian.SuratJalanID, 0);
            return View(suratJalanRincian);
        }

        // GET: SuratJalanRincians/Edit/5
        public ActionResult Edit(int id)
        {
            SuratJalanRincian suratJalanRincian = db.SuratJalanRincians.Find(id);
            if (suratJalanRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.SuratJalanID = suratJalanRincian.SuratJalanID;
            ViewBag.SalesOrderRincianID = slBahanBaku(suratJalanRincian.SuratJalanID, id);
            return View(suratJalanRincian);
        }

        // POST: SuratJalanRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuratJalanRincianID,SuratJalanID,SalesOrderRincianID,JumlahDikirim,Kirim")] SuratJalanRincian suratJalanRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suratJalanRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = suratJalanRincian.SuratJalanID });
            }
            ViewBag.SuratJalanID = suratJalanRincian.SuratJalanID;
            ViewBag.SalesOrderRincianID = slBahanBaku(suratJalanRincian.SuratJalanID, suratJalanRincian.SalesOrderRincianID);
            return View(suratJalanRincian);
        }

        // GET: SuratJalanRincians/Delete/5
        public ActionResult Delete(int? id)
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
            return View(suratJalanRincian);
        }

        // POST: SuratJalanRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuratJalanRincian suratJalanRincian = db.SuratJalanRincians.Find(id);
            db.SuratJalanRincians.Remove(suratJalanRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = suratJalanRincian.SuratJalanID });
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
