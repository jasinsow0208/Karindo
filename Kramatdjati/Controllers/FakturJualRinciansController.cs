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
    public class FakturJualRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: FakturJualRincians
        public ActionResult Index(int id)
        {
            FakturJual fakturJual = db.FakturJuals.Find(id);
            ViewBag.Perusahaan = fakturJual.Nama;
            ViewBag.TglFakur = fakturJual.TglFaktur;
            ViewBag.NoFaktur = fakturJual.NoFaktur;
            ViewBag.NoSuratJalan = fakturJual.suratJalanCetak.suratJalan.NoSuratJalan;
            ViewBag.Diskon = fakturJual.Diskon;
            ViewBag.FakturJualID = id;

            var fakturJualRincians = db.FakturJualRincians.Where(x=>x.FakturJualID==id).Include(f => f.fakturJual).Include(f => f.gudangBahanBaku);
            return View(fakturJualRincians.ToList());
        }

      
         // GET: FakturJualRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturJualRincian fakturJualRincian = db.FakturJualRincians.Find(id);
            if (fakturJualRincian == null)
            {
                return HttpNotFound();
            }
            return View(fakturJualRincian);
        }

        // POST: FakturJualRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FakturJualRincianID,FakturJualID,GudangBahanBakuID,Jumlah,Keterangan,HargaSatuan")] FakturJualRincian fakturJualRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakturJualRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = fakturJualRincian.FakturJualID });
            }
             return View(fakturJualRincian);
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
