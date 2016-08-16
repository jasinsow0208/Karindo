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
    public class PenimbanganProduksiRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PenimbanganProduksiRincians
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                id = 0;
            };
            var jpDeptARincian = db.JPDeptARincians.Find(id);
            ViewBag.Batch = jpDeptARincian.Batch;
            ViewBag.KodeBarangJadi = jpDeptARincian.KodeBarangJadi;
            ViewBag.TanggalProduksi = string.Format("{0:dd/MM/yyyy}",jpDeptARincian.jpDeptA.TglProduksi);
            ViewBag.NoLot = jpDeptARincian.NoLot;
            ViewBag.JPDeptARincianID = id;
            ViewBag.TglProduksi = string.Format("{0:yyyy-MM-dd}", jpDeptARincian.jpDeptA.TglProduksi);
            var penimbanganProduksis = db.PenimbanganProduksis.Where (x=>x.JPDeptARincianID==id).Include(p => p.gudangBahanBaku).Include(p => p.jpDeptARincian);
            return View(penimbanganProduksis.ToList());
        }

        public ActionResult Cetak(int id)
        {
            var jpDeptARincian = db.JPDeptARincians.Find(id);
            ViewBag.Batch = jpDeptARincian.Batch;
            ViewBag.KodeBarangJadi = jpDeptARincian.KodeBarangJadi;
            ViewBag.TanggalProduksi = string.Format("{0:dd/MM/yyyy}", jpDeptARincian.jpDeptA.TglProduksi);
            ViewBag.TglProduksi = string.Format("{0:yyyy-MM-dd}", jpDeptARincian.jpDeptA.TglProduksi);
            ViewBag.NoLot = jpDeptARincian.NoLot;
            ViewBag.JPDeptARincianID = id;
            var penimbanganProduksis = db.PenimbanganProduksis.Where(x => x.JPDeptARincianID == id).Include(p => p.gudangBahanBaku).Include(p => p.jpDeptARincian);
            return View(penimbanganProduksis.ToList());
        }

        // GET: PenimbanganProduksiRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenimbanganProduksi penimbanganProduksi = db.PenimbanganProduksis.Find(id);
            if (penimbanganProduksi == null)
            {
                return HttpNotFound();
            }
            return View(penimbanganProduksi);
        }

        // GET: PenimbanganProduksiRincians/Create
        public ActionResult Create(int id)
        {
            int gudangProduksiID = db.tblDefaults.FirstOrDefault().GudangProduksiID;
 
             var gudangBahanBaku = from s in db.GudangBahanBakus 
                                   where s.GudangID  == gudangProduksiID 
                                   select new { GudangBahanBakuID = s.GudangBahanBakuID , KodeBarang = s.bahanBaku.KodeBahanBaku  };
             ViewBag.GudangBahanBakuID = new SelectList(gudangBahanBaku.OrderBy(x => x.KodeBarang), "GudangBahanBakuID", "KodeBarang");
            ViewBag.JPDeptARincianID = id;
            return View();
        }

        // POST: PenimbanganProduksiRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PenimbanganProduksiID,JPDeptARincianID,GudangBahanBakuID,Jumlah,Posting")] PenimbanganProduksi penimbanganProduksi)
        {
            if (ModelState.IsValid)
            {
                db.PenimbanganProduksis.Add(penimbanganProduksi);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = penimbanganProduksi.JPDeptARincianID });
            }

            int gudangProduksiID = db.tblDefaults.FirstOrDefault().GudangProduksiID;

            var gudangBahanBaku = from s in db.GudangBahanBakus
                                  where s.GudangID == gudangProduksiID
                                  select new { GudangBahanBakuID = s.GudangBahanBakuID, KodeBarang = s.bahanBaku.KodeBahanBaku };
            ViewBag.GudangBahanBakuID = new SelectList(gudangBahanBaku.OrderBy(x=>x.KodeBarang), "GudangBahanBakuID", "KodeBarang"); 
            ViewBag.JPDeptARincianID = penimbanganProduksi.JPDeptARincianID ;

            return View(penimbanganProduksi);
        }

        // GET: PenimbanganProduksiRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenimbanganProduksi penimbanganProduksi = db.PenimbanganProduksis.Find(id);
            if (penimbanganProduksi == null)
            {
                return HttpNotFound();
            }

            int gudangProduksiID = db.tblDefaults.FirstOrDefault().GudangProduksiID;
            ViewBag.JPDeptARincianID = penimbanganProduksi.JPDeptARincianID;
            var gudangBahanBaku = from s in db.GudangBahanBakus
                                  where s.GudangID == gudangProduksiID
                                  select new { GudangBahanBakuID = s.GudangBahanBakuID, KodeBarang = s.bahanBaku.KodeBahanBaku };
            ViewBag.GudangBahanBakuID = new SelectList(gudangBahanBaku.OrderBy(x => x.KodeBarang), "GudangBahanBakuID", "KodeBarang", penimbanganProduksi.GudangBahanBakuID );
            ViewBag.JPDeptARincianID = penimbanganProduksi.JPDeptARincianID;
            return View(penimbanganProduksi);
        }

        // POST: PenimbanganProduksiRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PenimbanganProduksiID,JPDeptARincianID,GudangBahanBakuID,Jumlah,Posting")] PenimbanganProduksi penimbanganProduksi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penimbanganProduksi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = penimbanganProduksi.JPDeptARincianID });
            }
            int gudangProduksiID = db.tblDefaults.FirstOrDefault().GudangProduksiID;

            var gudangBahanBaku = from s in db.GudangBahanBakus
                                  where s.GudangID == gudangProduksiID
                                  select new { GudangBahanBakuID = s.GudangBahanBakuID, KodeBarang = s.bahanBaku.KodeBahanBaku };
            ViewBag.GudangBahanBakuID = new SelectList(gudangBahanBaku.OrderBy(x => x.KodeBarang), "GudangBahanBakuID", "KodeBarang", penimbanganProduksi.GudangBahanBakuID );
            ViewBag.JPDeptARincianID = penimbanganProduksi.JPDeptARincianID ;
            return View(penimbanganProduksi);
        }

        // GET: PenimbanganProduksiRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenimbanganProduksi penimbanganProduksi = db.PenimbanganProduksis.Find(id);
            if (penimbanganProduksi == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptARincianID = penimbanganProduksi.JPDeptARincianID;
            return View(penimbanganProduksi);
        }

        // POST: PenimbanganProduksiRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PenimbanganProduksi penimbanganProduksi = db.PenimbanganProduksis.Find(id);
            db.PenimbanganProduksis.Remove(penimbanganProduksi);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = penimbanganProduksi.JPDeptARincianID });
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
