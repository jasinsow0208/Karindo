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
    public class PindahGudangRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PindahGudangRincians
        public ActionResult Index(int id)
        {
            var pindahGudang = db.PindahGudangs.Where(x => x.PindahGudangID == id).FirstOrDefault();

            ViewBag.PindahGudangID = id;
            ViewBag.BuktiPindahGudang = pindahGudang.BuktiPindahGudang;
            ViewBag.Keterangan = pindahGudang.Keterangan ;
            ViewBag.TglTransaksi = pindahGudang.TglTransaksi.ToShortDateString();
            ViewBag.GudangAsal =pindahGudang.gudangAsal.Lokasi  ;
            ViewBag.GudangTujuan = pindahGudang.gudangTujuan.Lokasi;

            return View(db.PindahGudangRincians.Where(x=>x.PindahGudangID == id).Include(r=>r.gudangBahanBaku).ToList());
        }

        // GET: PindahGudangRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PindahGudangRincian pindahGudangRincian = db.PindahGudangRincians.Find(id);
            if (pindahGudangRincian == null)
            {
                return HttpNotFound();
            }
            return View(pindahGudangRincian);
        }

        // GET: PindahGudangRincians/Create
        public ActionResult Create(int id)
        {
            ViewBag.PindahGudangID = id;
            var gudang = db.PindahGudangs.Where(x => x.PindahGudangID == id).FirstOrDefault();
            int gudangAsalID = gudang.GudangAsalID;

            var bahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangAsalID).OrderBy (x=>x.bahanBaku.KodeBahanBaku );

            ViewBag.BahanBakuID = new SelectList(bahanBaku, "BahanBakuID", "bahanbaku.KodeBahanBaku");
            ViewPindahGudangRincian viewPindahGudangRincian = new ViewPindahGudangRincian();
            return View(viewPindahGudangRincian );
        }

        // POST: PindahGudangRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PindahGudangID,BahanBakuID,Jumlah")] ViewPindahGudangRincian viewPindahGudangRincian)
        {
            int gudangAsalID = 0;

            if (ModelState.IsValid)
            {
                PindahGudangRincian pindahGudangRincian = new PindahGudangRincian();
                pindahGudangRincian.PindahGudangID = viewPindahGudangRincian.PindahGudangID;
                pindahGudangRincian.Jumlah = viewPindahGudangRincian.Jumlah;

                var gudang = db.PindahGudangs.Where(x => x.PindahGudangID == viewPindahGudangRincian.PindahGudangID ).FirstOrDefault();
                gudangAsalID = gudang.GudangAsalID;

                var gudangBahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangAsalID && x.BahanBakuID == viewPindahGudangRincian.BahanBakuID).FirstOrDefault() ;
                int gudangBahanBakuID = gudangBahanBaku.GudangBahanBakuID;

                pindahGudangRincian.GudangBahanBakuID = gudangBahanBakuID;

                db.PindahGudangRincians.Add(pindahGudangRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = viewPindahGudangRincian.PindahGudangID  });
            }

            var bahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangAsalID).OrderBy(x => x.bahanBaku.KodeBahanBaku);
            ViewBag.BahanBakuID = new SelectList(bahanBaku, "BahanBakuID", "bahanbaku.KodeBahanBaku");

            return View(viewPindahGudangRincian);
        }

        // GET: PindahGudangRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PindahGudangRincian pindahGudangRincian = db.PindahGudangRincians.Find(id);
            if (pindahGudangRincian == null)
            {
                return HttpNotFound();
            }

            ViewPindahGudangRincian viewPindahGudangRincian = new ViewPindahGudangRincian();
            viewPindahGudangRincian.PindahGudangID = pindahGudangRincian.PindahGudangID;
            viewPindahGudangRincian.Jumlah = pindahGudangRincian.Jumlah;
            viewPindahGudangRincian.PindahGudangRincianID = pindahGudangRincian.PindahGudangRincianID;

            var gudangBahanBaku = db.GudangBahanBakus.Where(x => x.GudangBahanBakuID == pindahGudangRincian.GudangBahanBakuID).FirstOrDefault ();
            viewPindahGudangRincian.BahanBakuID = gudangBahanBaku.BahanBakuID;

            var gudang = db.PindahGudangs.Where(x => x.PindahGudangID == viewPindahGudangRincian.PindahGudangID).FirstOrDefault();
            int gudangAsalID = gudang.GudangAsalID;
            var bahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangAsalID).OrderBy(x => x.bahanBaku.KodeBahanBaku);
            ViewBag.BahanBakuID = new SelectList(bahanBaku, "BahanBakuID", "bahanbaku.KodeBahanBaku");

            return View(viewPindahGudangRincian );
        }

        // POST: PindahGudangRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PindahGudangRincianID, PindahGudangID,BahanBakuID,Jumlah")] ViewPindahGudangRincian viewPindahGudangRincian)
        {
            int gudangAsalID = 0;

            if (ModelState.IsValid)
            {
                PindahGudangRincian pindahGudangRincian = new PindahGudangRincian();
                pindahGudangRincian.PindahGudangRincianID = viewPindahGudangRincian.PindahGudangRincianID;
                pindahGudangRincian.Jumlah = viewPindahGudangRincian.Jumlah;
                pindahGudangRincian.PindahGudangID = viewPindahGudangRincian.PindahGudangID;

                var gudang = db.PindahGudangs.Where(x => x.PindahGudangID == viewPindahGudangRincian.PindahGudangID).FirstOrDefault();
                gudangAsalID = gudang.GudangAsalID;

                var gudangBahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangAsalID && x.BahanBakuID == viewPindahGudangRincian.BahanBakuID).FirstOrDefault();
                int gudangBahanBakuID = gudangBahanBaku.GudangBahanBakuID;
                pindahGudangRincian.GudangBahanBakuID = gudangBahanBakuID;

                db.Entry(pindahGudangRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = viewPindahGudangRincian.PindahGudangID  });
            }

            var bahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangAsalID).OrderBy(x => x.bahanBaku.KodeBahanBaku);
            ViewBag.BahanBakuID = new SelectList(bahanBaku, "BahanBakuID", "bahanbaku.KodeBahanBaku", viewPindahGudangRincian.BahanBakuID );
            return View(viewPindahGudangRincian);
        }

        // GET: PindahGudangRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PindahGudangRincian pindahGudangRincian = db.PindahGudangRincians.Find(id);
            if (pindahGudangRincian == null)
            {
                return HttpNotFound();
            }
            return View(pindahGudangRincian);
        }

        // POST: PindahGudangRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PindahGudangRincian pindahGudangRincian = db.PindahGudangRincians.Find(id);
            db.PindahGudangRincians.Remove(pindahGudangRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = pindahGudangRincian.PindahGudangID  });
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
