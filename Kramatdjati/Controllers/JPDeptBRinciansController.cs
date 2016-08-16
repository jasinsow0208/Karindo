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
    public class JPDeptBRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JPDeptBRincians
        public ActionResult Index(int id)
        {
            var jpDeptB = db.JPDeptBs.Where(x => x.JPDeptBID == id).FirstOrDefault();

            ViewBag.TglProduksi = jpDeptB.TglProduksiDeptB.ToShortDateString();
            ViewBag.TglProduksiDeptA = jpDeptB.TglProduksiDeptA.ToShortDateString();
            ViewBag.Catatan = jpDeptB.Catatan;
            ViewBag.JPDeptBID = id;

            return View(db.JPDeptBRincians.Where(x => x.JPDeptBID == id).ToList());
        }

        // GET: JPDeptBRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptBRincian jPDeptBRincian = db.JPDeptBRincians.Find(id);
            if (jPDeptBRincian == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptBRincian);
        }

        // GET: JPDeptBRincians/Create
        public ActionResult Create(int id)
        {
            ViewBag.JPDeptBID = id;

            var jPDeptB= db.JPDeptBs.Find (id);

            var BarangProduksiA = db.LPDeptARincians.Where(x => x.lpDeptA.TglProduksi == jPDeptB.TglProduksiDeptA)
                                                   .Select(x => new { Value = x.LPDeptARincianID, Text = x.jpDeptARincian.KodeBarangJadi })
                                                   .OrderBy(x => x.Text);
            ViewBag.LPDeptARincianID = new SelectList(BarangProduksiA, "Value", "Text");             
            return View();
        }

        // POST: JPDeptBRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPDeptBRincianID,JPDeptBID,LPDeptARincianID,UkuranTebal,Banyaknya,JmlBahan,Keterangan")] JPDeptBRincian jPDeptBRincian)
        {
            if (ModelState.IsValid)
            {
                db.JPDeptBRincians.Add(jPDeptBRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jPDeptBRincian.JPDeptBID });
            }

            ViewBag.JPDeptBID = jPDeptBRincian.JPDeptBID;

            var jPDeptB = db.JPDeptBs.Find(jPDeptBRincian.JPDeptBID);

            var BarangProduksiA = db.LPDeptARincians.Where(x => x.lpDeptA.TglProduksi == jPDeptB.TglProduksiDeptA)
                                                   .Select(x => new { Value = x.LPDeptARincianID, Text = x.jpDeptARincian.KodeBarangJadi })
                                                   .OrderBy(x => x.Text);
            ViewBag.LPDeptARincianID = new SelectList(BarangProduksiA, "Value", "Text");    
            return View(jPDeptBRincian);
        }

        // GET: JPDeptBRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptBRincian jPDeptBRincian = db.JPDeptBRincians.Find(id);
            if (jPDeptBRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptBID = jPDeptBRincian.JPDeptBID;

            var jPDeptB = db.JPDeptBs.Find(jPDeptBRincian.JPDeptBID);

            var BarangProduksiA = db.LPDeptARincians.Where(x => x.lpDeptA.TglProduksi == jPDeptB.TglProduksiDeptA)
                                                   .Select(x => new { Value = x.LPDeptARincianID, Text = x.jpDeptARincian.KodeBarangJadi })
                                                   .OrderBy(x => x.Text);
            ViewBag.LPDeptARincianID = new SelectList(BarangProduksiA, "Value", "Text",jPDeptBRincian.LPDeptARincianID );   

            return View(jPDeptBRincian);
        }

        // POST: JPDeptBRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPDeptBRincianID,JPDeptBID,LPDeptARincianID,UkuranTebal,Banyaknya,JmlBahan,Keterangan")] JPDeptBRincian jPDeptBRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPDeptBRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jPDeptBRincian.JPDeptBID });
            }

            ViewBag.JPDeptBID = jPDeptBRincian.JPDeptBID;

            var jPDeptB = db.JPDeptBs.Find(jPDeptBRincian.JPDeptBID);

            var BarangProduksiA = db.LPDeptARincians.Where(x => x.lpDeptA.TglProduksi == jPDeptB.TglProduksiDeptA)
                                                   .Select(x => new { Value = x.LPDeptARincianID, Text = x.jpDeptARincian.KodeBarangJadi })
                                                   .OrderBy(x => x.Text);
            ViewBag.LPDeptARincianID = new SelectList(BarangProduksiA, "Value", "Text", jPDeptBRincian.LPDeptARincianID);   
            return View(jPDeptBRincian);
        }

        // GET: JPDeptBRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptBRincian jPDeptBRincian = db.JPDeptBRincians.Find(id);
            if (jPDeptBRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptBID = jPDeptBRincian.JPDeptBID;
            return View(jPDeptBRincian);
        }

        // POST: JPDeptBRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPDeptBRincian jPDeptBRincian = db.JPDeptBRincians.Find(id);
            db.JPDeptBRincians.Remove(jPDeptBRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = jPDeptBRincian.JPDeptBID });
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
