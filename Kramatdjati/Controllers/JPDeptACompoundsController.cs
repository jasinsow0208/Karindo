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
    public class JPDeptACompoundsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JPDeptACompounds
        public ActionResult Index(int id)
        {
            var jpDeptA = db.JPDeptAs.Where(x => x.JPDeptAID == id).FirstOrDefault();

            ViewBag.TglProduksi = jpDeptA.TglProduksi.ToShortDateString();
            ViewBag.Catatan = jpDeptA.Catatan;
            ViewBag.JPDeptAID = id;

            var jPDeptACompounds = db.JPDeptACompounds.Where(x=>x.JPDeptAID==id).Include(j => j.gudangBahanBaku ).Include(j => j.jPDeptA);
            return View(jPDeptACompounds.ToList());
        }

  
        // GET: JPDeptACompounds/Create
        public ActionResult Create(int id)
        {
            ViewBag.JPDeptAID = id;

            var jenisPersediaanCompound = db.tblDefaults.FirstOrDefault() ; 

            int jenisPersediaanCompoundID=(int)jenisPersediaanCompound.JenisPersediaanCompoundID;
            int gudangProduksiID=jenisPersediaanCompound.GudangProduksiID ; 

            var bahanCompound= db.GudangBahanBakus.Where (x=>x.bahanBaku.KodeBarangJadi ==null && x.bahanBaku.JenisPersediaanID == jenisPersediaanCompoundID  ).
                               Select(x=> new { Value = x.GudangBahanBakuID  , Text = x.bahanBaku.KodeBahanBaku  }).
                               OrderBy(x=>x.Text);
           
            ViewBag.GudangBahanBakuID = new SelectList(bahanCompound, "Value", "Text");

            return View();
        }

        // POST: JPDeptACompounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPDeptACompoundID,JPDeptAID,GudangBahanBakuID,Jumlah")] JPDeptACompound jPDeptACompound)
        {
            if (ModelState.IsValid)
            {
                db.JPDeptACompounds.Add(jPDeptACompound);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jPDeptACompound.JPDeptAID });
            }
            ViewBag.JPDeptAID = jPDeptACompound.JPDeptAID ;

            var jenisPersediaanCompound = db.tblDefaults.FirstOrDefault();

            int jenisPersediaanCompoundID = (int)jenisPersediaanCompound.JenisPersediaanCompoundID;
            int gudangProduksiID = jenisPersediaanCompound.GudangProduksiID;

            var bahanCompound = db.GudangBahanBakus.Where(x => x.GudangID == gudangProduksiID && x.bahanBaku.KodeBarangJadi == null && x.bahanBaku.JenisPersediaanID == jenisPersediaanCompoundID).
                               Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                               OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(bahanCompound, "Value", "Text",jPDeptACompound.GudangBahanBakuID );           
            return View(jPDeptACompound);
        }

        // GET: JPDeptACompounds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptACompound jPDeptACompound = db.JPDeptACompounds.Find(id);
            if (jPDeptACompound == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptAID = jPDeptACompound.JPDeptAID;
            var jenisPersediaanCompound = db.tblDefaults.FirstOrDefault();

            int jenisPersediaanCompoundID = (int)jenisPersediaanCompound.JenisPersediaanCompoundID;
            int gudangProduksiID = jenisPersediaanCompound.GudangProduksiID;

            var bahanCompound = db.GudangBahanBakus.Where(x => x.bahanBaku.KodeBarangJadi == null && x.bahanBaku.JenisPersediaanID == jenisPersediaanCompoundID).
                               Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                               OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(bahanCompound, "Value", "Text", jPDeptACompound.GudangBahanBakuID);
            return View(jPDeptACompound);
        }

        // POST: JPDeptACompounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPDeptACompoundID,JPDeptAID,GudangBahanBakuID,Jumlah")] JPDeptACompound jPDeptACompound)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPDeptACompound).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jPDeptACompound.JPDeptAID });
            }
            ViewBag.JPDeptAID = jPDeptACompound.JPDeptAID;
            var jenisPersediaanCompound = db.tblDefaults.FirstOrDefault();

            int jenisPersediaanCompoundID = (int)jenisPersediaanCompound.JenisPersediaanCompoundID;
            int gudangProduksiID = jenisPersediaanCompound.GudangProduksiID;

            var bahanCompound = db.GudangBahanBakus.Where(x => x.bahanBaku.KodeBarangJadi == null && x.bahanBaku.JenisPersediaanID == jenisPersediaanCompoundID).
                               Select(x => new { Value = x.GudangBahanBakuID, Text = x.bahanBaku.KodeBahanBaku }).
                               OrderBy(x => x.Text);

            ViewBag.GudangBahanBakuID = new SelectList(bahanCompound, "Value", "Text", jPDeptACompound.GudangBahanBakuID);
            return View(jPDeptACompound);
        }

        // GET: JPDeptACompounds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptACompound jPDeptACompound = db.JPDeptACompounds.Find(id);
            if (jPDeptACompound == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptAID = jPDeptACompound.JPDeptAID;
            return View(jPDeptACompound);
        }

        // POST: JPDeptACompounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPDeptACompound jPDeptACompound = db.JPDeptACompounds.Find(id);
            db.JPDeptACompounds.Remove(jPDeptACompound);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = jPDeptACompound.JPDeptAID });
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
