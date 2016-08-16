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
    public class LPDeptARajangansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: LPDeptARajangans
        public ActionResult Index(int id)
        {
            var lpDeptA = db.LPDeptAs.Where(x => x.LPDeptAID == id).FirstOrDefault();

            ViewBag.TglProduksi = lpDeptA.TglProduksi.ToShortDateString();
            ViewBag.Catatan = lpDeptA.Catatan;
            ViewBag.LPDeptAID = id;

            var lPDeptARajangans = db.LPDeptARajangans.Where(x=>x.lPDeptA.LPDeptAID==id).Include(l => l.gudangBahanBaku).Include(l => l.lPDeptA);
            return View(lPDeptARajangans.ToList());
        }

        // GET: LPDeptARajangans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptARajangan lPDeptARajangan = db.LPDeptARajangans.Find(id);
            if (lPDeptARajangan == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptARajangan);
        }

        // GET: LPDeptARajangans/Create
        public ActionResult Create(int id)
        {
            ViewBag.LPDeptAID = id;

            var jenisPersediaanRajangan = db.tblDefaults.FirstOrDefault();

            int jenisPersediaanRajanganID = (int)jenisPersediaanRajangan.JenisPersediaanRajanganID;
            int gudangProduksiID = jenisPersediaanRajangan.GudangProduksiID;

            var bahanRajangan = db.GudangBahanBakus.Where(x => x.bahanBaku.KodeBarangJadi == null && x.bahanBaku.JenisPersediaanID == jenisPersediaanRajanganID).
                               Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                               OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(bahanRajangan, "Value", "Text");

            return View();
        }

        // POST: LPDeptARajangans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LPDeptARajanganID,LPDeptAID,GudangBahanBakuID,Jumlah")] LPDeptARajangan lPDeptARajangan)
        {
            if (ModelState.IsValid)
            {
                db.LPDeptARajangans.Add(lPDeptARajangan);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = lPDeptARajangan.LPDeptAID });
            }

            ViewBag.LPDeptAID = lPDeptARajangan.LPDeptAID  ;

            var jenisPersediaanRajangan = db.tblDefaults.FirstOrDefault();

            int jenisPersediaanRajanganID = (int)jenisPersediaanRajangan.JenisPersediaanRajanganID;
            int gudangProduksiID = jenisPersediaanRajangan.GudangProduksiID;

            var bahanRajangan = db.GudangBahanBakus.Where(x => x.bahanBaku.KodeBarangJadi == null && x.bahanBaku.JenisPersediaanID == jenisPersediaanRajanganID).
                               Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                               OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(bahanRajangan, "Value", "Text", lPDeptARajangan.GudangBahanBakuID);

            return View(lPDeptARajangan);
        }

        // GET: LPDeptARajangans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptARajangan lPDeptARajangan = db.LPDeptARajangans.Find(id);
            if (lPDeptARajangan == null)
            {
                return HttpNotFound();
            }
            ViewBag.LPDeptAID = lPDeptARajangan.LPDeptAID;

            var jenisPersediaanRajangan = db.tblDefaults.FirstOrDefault();

            int jenisPersediaanRajanganID = (int)jenisPersediaanRajangan.JenisPersediaanRajanganID;
            int gudangProduksiID = jenisPersediaanRajangan.GudangProduksiID;

            var bahanRajangan = db.GudangBahanBakus.Where(x => x.bahanBaku.KodeBarangJadi == null && x.bahanBaku.JenisPersediaanID == jenisPersediaanRajanganID).
                               Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                               OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(bahanRajangan, "Value", "Text", lPDeptARajangan.GudangBahanBakuID);

            return View(lPDeptARajangan);
        }

        // POST: LPDeptARajangans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LPDeptARajanganID,LPDeptAID,GudangBahanBakuID,Jumlah")] LPDeptARajangan lPDeptARajangan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lPDeptARajangan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = lPDeptARajangan.LPDeptAID });
            }
            ViewBag.LPDeptAID = lPDeptARajangan.LPDeptAID;

            var jenisPersediaanRajangan = db.tblDefaults.FirstOrDefault();

            int jenisPersediaanRajanganID = (int)jenisPersediaanRajangan.JenisPersediaanRajanganID;
            int gudangProduksiID = jenisPersediaanRajangan.GudangProduksiID;

            var bahanRajangan = db.GudangBahanBakus.Where(x => x.bahanBaku.KodeBarangJadi == null && x.bahanBaku.JenisPersediaanID == jenisPersediaanRajanganID).
                               Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                               OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(bahanRajangan, "Value", "Text", lPDeptARajangan.GudangBahanBakuID);

            return View(lPDeptARajangan);
        }

        // GET: LPDeptARajangans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptARajangan lPDeptARajangan = db.LPDeptARajangans.Find(id);
            if (lPDeptARajangan == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptARajangan);
        }

        // POST: LPDeptARajangans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LPDeptARajangan lPDeptARajangan = db.LPDeptARajangans.Find(id);
            db.LPDeptARajangans.Remove(lPDeptARajangan);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = lPDeptARajangan.LPDeptAID });
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
