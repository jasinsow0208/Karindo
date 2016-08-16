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
    public class LPDeptBRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: LPDeptBRincians
        public ActionResult Index(int id)
        {
            var lpDeptB = db.LPDeptBs.Where(x => x.LPDeptBID == id).FirstOrDefault();

            ViewBag.TglProduksi = lpDeptB.TglProduksi.ToShortDateString();
            ViewBag.Catatan = lpDeptB.Catatan;
            ViewBag.LPDeptBID = id;
            return View(db.LPDeptBRincians.Where(x => x.LPDeptBID == id).ToList());
        }

        // GET: LPDeptBRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptBRincian lPDeptBRincian = db.LPDeptBRincians.Find(id);
            if (lPDeptBRincian == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptBRincian);
        }

        // GET: LPDeptBRincians/Create
        public ActionResult Create(int id)
        {
            ViewBag.LPDeptBID = id;

            var LPDeptB = db.LPDeptBs.Find(id);

            var jPDeptBRincianID = db.JPDeptBRincians.Where(x => x.jpDeptB.TglProduksiDeptB == LPDeptB.TglProduksi && x.jpDeptB.Posting == true)
                                                   .Select(x => new { Value = x.JPDeptBRincianID , Text = x.lpDeptARincian.jpDeptARincian.KodeBarangJadi.Trim() + "-" +  x.UkuranTebal.ToString() + " mm"  })
                                                   .OrderBy(x => x.Text);

            ViewBag.JPDeptBRincianID = new SelectList(jPDeptBRincianID, "Value", "Text");

            return View();
        }

        // POST: LPDeptBRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LPDeptBRincianID,LPDeptBID,JPDeptBRincianID,Hasil,Cacat,Keterangan")] LPDeptBRincian lPDeptBRincian)
        {
            if (ModelState.IsValid)
            {
                db.LPDeptBRincians.Add(lPDeptBRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = lPDeptBRincian.LPDeptBID });
            }
            ViewBag.LPDeptBID = lPDeptBRincian.LPDeptBID;

            var LPDeptB = db.LPDeptBs.Find(lPDeptBRincian.LPDeptBID);

            var jPDeptBRincianID = db.JPDeptBRincians.Where(x => x.jpDeptB.TglProduksiDeptB == LPDeptB.TglProduksi && x.jpDeptB.Posting == true)
                                                   .Select(x => new { Value = x.JPDeptBRincianID, Text = x.lpDeptARincian.jpDeptARincian.KodeBarangJadi.Trim() + "-" + x.UkuranTebal.ToString() + " mm" })
                                                   .OrderBy(x => x.Text);

            ViewBag.JPDeptBRincianID = new SelectList(jPDeptBRincianID, "Value", "Text");
            return View(lPDeptBRincian);
        }

        // GET: LPDeptBRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptBRincian lPDeptBRincian = db.LPDeptBRincians.Find(id);
            if (lPDeptBRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.LPDeptBID = lPDeptBRincian.LPDeptBID;

            var LPDeptB = db.LPDeptBs.Find(lPDeptBRincian.LPDeptBID);

            var jPDeptBRincianID = db.JPDeptBRincians.Where(x => x.jpDeptB.TglProduksiDeptB == LPDeptB.TglProduksi && x.jpDeptB.Posting == true)
                                                   .Select(x => new { Value = x.JPDeptBRincianID, Text = x.lpDeptARincian.jpDeptARincian.KodeBarangJadi.Trim() + "-" + x.UkuranTebal.ToString() + " mm" })
                                                   .OrderBy(x => x.Text);

            ViewBag.JPDeptBRincianID = new SelectList(jPDeptBRincianID, "Value", "Text", lPDeptBRincian.LPDeptBRincianID );
            return View(lPDeptBRincian);
        }

        // POST: LPDeptBRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LPDeptBRincianID,LPDeptBID,JPDeptBRincianID,Hasil,Cacat,Keterangan")] LPDeptBRincian lPDeptBRincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lPDeptBRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = lPDeptBRincian.LPDeptBID });
            }
            ViewBag.LPDeptBID = lPDeptBRincian.LPDeptBID;

            var LPDeptB = db.LPDeptBs.Find(lPDeptBRincian.LPDeptBID);

            var jPDeptBRincianID = db.JPDeptBRincians.Where(x => x.jpDeptB.TglProduksiDeptB == LPDeptB.TglProduksi && x.jpDeptB.Posting == true)
                                                   .Select(x => new { Value = x.JPDeptBRincianID, Text = x.lpDeptARincian.jpDeptARincian.KodeBarangJadi.Trim() + "-" + x.UkuranTebal.ToString() + " mm" })
                                                   .OrderBy(x => x.Text);

            ViewBag.JPDeptBRincianID = new SelectList(jPDeptBRincianID, "Value", "Text", lPDeptBRincian.LPDeptBRincianID);

            return View(lPDeptBRincian);
        }

        // GET: LPDeptBRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptBRincian lPDeptBRincian = db.LPDeptBRincians.Find(id);
            if (lPDeptBRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.LPDeptAID = lPDeptBRincian.LPDeptBID;
            return View(lPDeptBRincian);
        }

        // POST: LPDeptBRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LPDeptBRincian lPDeptBRincian = db.LPDeptBRincians.Find(id);
            db.LPDeptBRincians.Remove(lPDeptBRincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = lPDeptBRincian.LPDeptBID });
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
