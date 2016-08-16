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
    public class SuratJalanCetakRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SuratJalanCetakRincians
        public ActionResult Index(int id)
        {
            var suratJalan = db.SuratJalans.Where(x => x.SuratJalanID == id).FirstOrDefault() ;

            ViewBag.TglSuratJalan = suratJalan.tglSuratJalan.ToShortDateString();
            ViewBag.TglTransaksi = suratJalan.TglTransaksi.ToShortDateString();
            ViewBag.NoSO = suratJalan.NoSuratJalan;
            ViewBag.Customer = suratJalan.contact.Perusahaan;
            ViewBag.Alamat = suratJalan.contact.Alamat;
            ViewBag.Kota = suratJalan.contact.Kota;
            ViewBag.SuratJalanID = id;
            ViewBag.Catatan = suratJalan.Catatan;

           var suratJalanCetakRincians = db.SuratJalanCetakRincians.Where(x=>x.suratJalanCetak.SuratJalanID == id).Include(s => s.gudangBahanBku).Include(s => s.suratJalanCetak).ToList().OrderBy(x=>x.gudangBahanBku.bahanBaku.KodeBahanBaku).ThenBy(x=> x.gudangBahanBku.bahanBaku.Ukuran) ;
            return View(suratJalanCetakRincians.ToList());
        }
        public ActionResult IndexNF(int id)
        {
            var suratJalan = db.SuratJalans.Where(x => x.SuratJalanID == id).FirstOrDefault();

            ViewBag.TglSuratJalan = suratJalan.tglSuratJalan.ToShortDateString();
            ViewBag.TglTransaksi = suratJalan.TglTransaksi.ToShortDateString();
            ViewBag.NoSO = suratJalan.NoSuratJalan;
            ViewBag.Customer = suratJalan.contact.Perusahaan;
            ViewBag.Alamat = suratJalan.contact.Alamat;
            ViewBag.Kota = suratJalan.contact.Kota;
            ViewBag.SuratJalanID = id;
            ViewBag.Catatan = suratJalan.Catatan;

            var suratJalanCetakRincians = db.SuratJalanCetakRincians.Where(x => x.suratJalanCetak.SuratJalanID == id).Include(s => s.gudangBahanBku).Include(s => s.suratJalanCetak).ToList().OrderBy(x => x.gudangBahanBku.bahanBaku.KodeBahanBaku).ThenBy(x => x.gudangBahanBku.bahanBaku.Ukuran);
            return View(suratJalanCetakRincians.ToList());
        }

        public ActionResult IndexNonPPN(int id)
        {
            var suratJalan = db.SuratJalans.Where(x => x.SuratJalanID == id).FirstOrDefault();

            ViewBag.TglSuratJalan = suratJalan.tglSuratJalan.ToShortDateString();
            ViewBag.TglTransaksi = suratJalan.TglTransaksi.ToShortDateString();
            ViewBag.NoSO = suratJalan.NoSuratJalan;
            ViewBag.Customer = suratJalan.contact.Perusahaan;
            ViewBag.Alamat = suratJalan.contact.Alamat;
            ViewBag.Kota = suratJalan.contact.Kota;
            ViewBag.SuratJalanID = id;
            ViewBag.Catatan = suratJalan.Catatan;

            var suratJalanCetakRincians = db.SuratJalanCetakRincians.Where(x => x.suratJalanCetak.SuratJalanID == id).Include(s => s.gudangBahanBku).Include(s => s.suratJalanCetak).ToList().OrderBy(x => x.gudangBahanBku.bahanBaku.Warna ).ThenBy(x => x.gudangBahanBku.bahanBaku.Ukuran);
            return View(suratJalanCetakRincians.ToList());
        }
        public ActionResult IndexNonPPNNF(int id)
        {
            var suratJalan = db.SuratJalans.Where(x => x.SuratJalanID == id).FirstOrDefault();

            ViewBag.TglSuratJalan = suratJalan.tglSuratJalan.ToShortDateString();
            ViewBag.TglTransaksi = suratJalan.TglTransaksi.ToShortDateString();
            ViewBag.NoSO = suratJalan.NoSuratJalan;
            ViewBag.Customer = suratJalan.contact.Perusahaan;
            ViewBag.Alamat = suratJalan.contact.Alamat;
            ViewBag.Kota = suratJalan.contact.Kota;
            ViewBag.SuratJalanID = id;
            ViewBag.Catatan = suratJalan.Catatan;

            var suratJalanCetakRincians = db.SuratJalanCetakRincians.Where(x => x.suratJalanCetak.SuratJalanID == id).Include(s => s.gudangBahanBku).Include(s => s.suratJalanCetak).ToList().OrderBy(x => x.gudangBahanBku.bahanBaku.Warna).ThenBy(x => x.gudangBahanBku.bahanBaku.Ukuran);
            return View(suratJalanCetakRincians.ToList());
        }
          // GET: SuratJalanCetakRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalanCetakRincian suratJalanCetakRincian = db.SuratJalanCetakRincians.Find(id);
            if (suratJalanCetakRincian == null)
            {
                return HttpNotFound();
            }

            ViewBag.SuratJalanID=suratJalanCetakRincian.suratJalanCetak.SuratJalanID  ;
             return View(suratJalanCetakRincian);
        }

        // POST: SuratJalanCetakRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuratJalanCetakRincianID,SuratJalanCetakID,GudangBahanBakuID,Jumlah,Keterangan")] SuratJalanCetakRincian suratJalanCetakRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suratJalanCetakRincian).State = EntityState.Modified;
                db.SaveChanges();
                var suratJalan=db.SuratJalanCetakRincians.Where(x=>x.SuratJalanCetakRincianID == suratJalanCetakRincian.SuratJalanCetakRincianID ).Include (x=>x.suratJalanCetak); 
                return RedirectToAction("Index", new {id = suratJalan.FirstOrDefault().suratJalanCetak.SuratJalanID  }  );
            }
             return View(suratJalanCetakRincian);
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
