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
    public class BPPBRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: BPPBRincians
        public ActionResult Index(int id)
        {
            var bPPB = db.BPPBs.Where(x => x.BPPBID == id).FirstOrDefault();

            ViewBag.TglProduksi = bPPB.TglProduksi.ToShortDateString();
            ViewBag.TglPenimbangan = bPPB.TglPenimbangan.ToShortDateString();
            ViewBag.Keterangan = bPPB.Keterangan;
            ViewBag.BPPBID = id;
            ViewBag.NoBPPB = bPPB.NoBPPB;

            var bPPBRincians = db.BPPBRincians.Where(x => x.BPPBID == id).Include(b => b.bPPB).Include(b => b.gudangBahanBaku);
            return View(bPPBRincians.ToList());
        }

        // GET: BPPBRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BPPBRincian bPPBRincian = db.BPPBRincians.Find(id);
            if (bPPBRincian == null)
            {
                return HttpNotFound();
            }
            return View(bPPBRincian);
        }

        // GET: BPPBRincians/Create
        public ActionResult Create(int id)
        {
            ViewBag.BPPBID = id;
            var tblDefault = db.tblDefaults.FirstOrDefault();
            int gudangBeliID = tblDefault.GudangBeliID;

            var BahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangBeliID).
                                  Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                                  OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(BahanBaku, "Value", "Text");
            return View();
        }

        // POST: BPPBRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BPPBRincianID,BPPBID,GudangBahanBakuID,Kebutuhan,Pembulatan,SatuanZak,JmlZak,PostingDiserahkan,Diserahkan,PostingDiterima,Diterima")] BPPBRincian bPPBRincian)
        {
            if (ModelState.IsValid)
            {
                db.BPPBRincians.Add(bPPBRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = bPPBRincian.BPPBID });
            }

            ViewBag.BPPBID = bPPBRincian.BPPBID ;
            var tblDefault = db.tblDefaults.FirstOrDefault();
            int gudangBeliID = tblDefault.GudangBeliID;

            var BahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangBeliID).
                                  Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                                  OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(BahanBaku, "Value", "Text", bPPBRincian.GudangBahanBakuID ); 
            return View(bPPBRincian);
        }

        // GET: BPPBRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BPPBRincian bPPBRincian = db.BPPBRincians.Find(id);
            if (bPPBRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.BPPBID = bPPBRincian.BPPBID;
            var tblDefault = db.tblDefaults.FirstOrDefault();
            int gudangBeliID = tblDefault.GudangBeliID;

            var BahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangBeliID).
                                  Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                                  OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(BahanBaku, "Value", "Text", bPPBRincian.GudangBahanBakuID); 
            return View(bPPBRincian);
        }

        // POST: BPPBRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BPPBRincianID,BPPBID,GudangBahanBakuID,Kebutuhan,Pembulatan,SatuanZak,JmlZak,PostingDiserahkan,Diserahkan,PostingDiterima,Diterima")] BPPBRincian bPPBRincian)
        {
            if (ModelState.IsValid)
            {
                AppIdentityDbContext dbSatuan = new AppIdentityDbContext();
                decimal satuan = dbSatuan.BPPBRincians.Where(x => x.BPPBRincianID == bPPBRincian.BPPBRincianID).FirstOrDefault().gudangBahanBaku.bahanBaku.JenisKemasans.Where(x => x.Default == true).FirstOrDefault().Berat;

                db.Entry(bPPBRincian).State = EntityState.Modified;
                bPPBRincian.SatuanZak = satuan;
                bPPBRincian.Pembulatan = bPPBRincian.JmlZak * satuan; 
                
                db.SaveChanges();
                return RedirectToAction("Index", new { id = bPPBRincian.BPPBID });
            }
            ViewBag.BPPBID = bPPBRincian.BPPBID;
            var tblDefault = db.tblDefaults.FirstOrDefault();
            int gudangBeliID = tblDefault.GudangBeliID;

            var BahanBaku = db.GudangBahanBakus.Where(x => x.GudangID == gudangBeliID).
                                  Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                                  OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(BahanBaku, "Value", "Text", bPPBRincian.GudangBahanBakuID); 
            return View(bPPBRincian);
        }

        // GET: BPPBRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BPPBRincian bPPBRincian = db.BPPBRincians.Find(id);
            if (bPPBRincian == null)
            {
                return HttpNotFound();
            }
            return View(bPPBRincian);
        }

        // POST: BPPBRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BPPBRincian bPPBRincian = db.BPPBRincians.Find(id);
            db.BPPBRincians.Remove(bPPBRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = bPPBRincian.BPPBID });
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
