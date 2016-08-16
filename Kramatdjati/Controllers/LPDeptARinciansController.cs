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
    public class LPDeptARinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: LPDeptARincians
        public ActionResult Index(int id)
        {
            var lpDeptA = db.LPDeptAs.Where(x => x.LPDeptAID == id).FirstOrDefault();

            ViewBag.TglProduksi = lpDeptA.TglProduksi.ToShortDateString();
            ViewBag.Catatan = lpDeptA.Catatan;
            ViewBag.LPDeptAID = id;
            return View(db.LPDeptARincians.Where(x => x.LPDeptAID == id).ToList());
        }

        // GET: LPDeptARincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptARincian lPDeptARincian = db.LPDeptARincians.Find(id);
            if (lPDeptARincian == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptARincian);
        }

        // GET: LPDeptARincians/Create
        public ActionResult Create(int id)
        {
            ViewBag.LPDeptAID = id;

            var LPDeptA= db.LPDeptAs.Find(id);

            var JPDeptARincian = db.JPDeptARincians.Where(x => x.jpDeptA.TglProduksi == LPDeptA.TglProduksi && x.jpDeptA.Posting == true )
                                                   .Select(x=> new {Value=x.JPDeptARincianID , Text=x.KodeBarangJadi })
                                                   .OrderBy (x=>x.Text );

            ViewBag.JPDeptARincianID = new SelectList(JPDeptARincian, "Value", "Text");

            LPDeptARincian lPDeptARincian = new LPDeptARincian();
            lPDeptARincian.TglProduksi = LPDeptA.TglProduksi;

            return View(lPDeptARincian);
        }

        // POST: LPDeptARincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LPDeptARincianID,LPDeptAID,JPDeptARincianID,Hasil,Cacat,SisaCompound,Keterangan,JmlTransfer, TglProduksi, Rajangan")] LPDeptARincian lPDeptARincian)
        {
            if (ModelState.IsValid)
            {
                db.LPDeptARincians.Add(lPDeptARincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = lPDeptARincian.LPDeptAID });
            }
            ViewBag.LPDeptAID = lPDeptARincian.LPDeptAID;

            var LPDeptA = db.LPDeptAs.Find(lPDeptARincian.LPDeptAID);

            var JPDeptARincian = db.JPDeptARincians.Where(x => x.jpDeptA.TglProduksi == lPDeptARincian.TglProduksi  && x.jpDeptA.Posting == true)
                                                   .Select(x => new { Value = x.JPDeptARincianID, Text = x.KodeBarangJadi })
                                                   .OrderBy(x => x.Text);

            ViewBag.JPDeptARincianID = new SelectList(JPDeptARincian, "Value", "Text");

            return View(lPDeptARincian);
        }

        // GET: LPDeptARincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptARincian lPDeptARincian = db.LPDeptARincians.Find(id);
            if (lPDeptARincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.LPDeptAID = lPDeptARincian.LPDeptAID;

            var LPDeptA = db.LPDeptAs.Find(lPDeptARincian.LPDeptAID);

            var JPDeptARincian = db.JPDeptARincians.Where(x => x.jpDeptA.TglProduksi == lPDeptARincian.TglProduksi  && x.jpDeptA.Posting == true)
                                                   .Select(x => new { Value = x.JPDeptARincianID, Text = x.KodeBarangJadi })
                                                   .OrderBy(x => x.Text);

            ViewBag.JPDeptARincianID = new SelectList(JPDeptARincian, "Value", "Text", lPDeptARincian.JPDeptARincianID );
            return View(lPDeptARincian);
        }

        // POST: LPDeptARincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LPDeptARincianID,LPDeptAID,JPDeptARincianID,Hasil,Cacat,SisaCompound,Keterangan,JmlTransfer, TglProduksi, Rajangan")] LPDeptARincian lPDeptARincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lPDeptARincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {id= lPDeptARincian.LPDeptAID });
            }
            ViewBag.LPDeptAID = lPDeptARincian.LPDeptAID;

            var LPDeptA = db.LPDeptAs.Find(lPDeptARincian.LPDeptAID);

            var JPDeptARincian = db.JPDeptARincians.Where(x => x.jpDeptA.TglProduksi == lPDeptARincian.TglProduksi  && x.jpDeptA.Posting == true)
                                                   .Select(x => new { Value = x.JPDeptARincianID, Text = x.KodeBarangJadi })
                                                   .OrderBy(x => x.Text);

            ViewBag.JPDeptARincianID = new SelectList(JPDeptARincian, "Value", "Text", lPDeptARincian.JPDeptARincianID);
            return View(lPDeptARincian);
        }

        // GET: LPDeptARincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptARincian lPDeptARincian = db.LPDeptARincians.Find(id);
            if (lPDeptARincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.LPDeptAID = lPDeptARincian.LPDeptAID;
            return View(lPDeptARincian);
        }

        // POST: LPDeptARincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LPDeptARincian lPDeptARincian = db.LPDeptARincians.Find(id);
            db.LPDeptARincians.Remove(lPDeptARincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = lPDeptARincian.LPDeptAID });
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
