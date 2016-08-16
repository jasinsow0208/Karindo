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
    public class BPPBsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: BPPBs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };

            var bppbs = db.BPPBs.Where(x => x.Posting == Posting);
            List<BPPBView> bppbViews = new List<BPPBView>();

            foreach (BPPB rw in bppbs)
            {
                BPPBView bppbView = new BPPBView();
                bppbView.BPPBID = rw.BPPBID;
                bppbView.Diminta = rw.Diminta;
                bppbView.Diserahkan  = rw.Diserahkan ;
                bppbView.Diterima  = rw.Diterima ;
                bppbView.Keterangan = rw.Keterangan;
                bppbView.NoBPPB = rw.NoBPPB;
                bppbView.Posting = rw.Posting;
                bppbView.TglPenimbangan = rw.TglPenimbangan;
                bppbView.TglProduksi = rw.TglProduksi;

                int jmlBlmPosting = db.BPPBRincians.Where(x => x.BPPBID == rw.BPPBID && x.PostingDiserahkan == false).Count();
 


            }
            return View(db.BPPBs.Where(x => x.Posting == Posting).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "BPPBID,TglPenimbangan,TglProduksi,NoBPPB,Keterangan,Diminta,Diserahkan,Diterima, Posting, JPDeptAID")] BPPB bPPB)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = bPPB.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: BPPBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BPPB bPPB = db.BPPBs.Find(id);
            if (bPPB == null)
            {
                return HttpNotFound();
            }
            return View(bPPB);
        }

        // GET: BPPBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BPPBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BPPBID,TglPenimbangan,TglProduksi,NoBPPB,Keterangan,Diminta,Diserahkan,Diterima, Posting")] BPPB bPPB)
        {
            if (ModelState.IsValid)
            {
                db.BPPBs.Add(bPPB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bPPB);
        }

        // GET: BPPBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BPPB bPPB = db.BPPBs.Find(id);
            if (bPPB == null)
            {
                return HttpNotFound();
            }
            return View(bPPB);
        }

        // POST: BPPBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BPPBID,TglPenimbangan,TglProduksi,NoBPPB,Keterangan,Diminta,Diserahkan,Diterima, Posting")] BPPB bPPB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bPPB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bPPB);
        }

        // GET: BPPBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BPPB bPPB = db.BPPBs.Find(id);
            if (bPPB == null)
            {
                return HttpNotFound();
            }
            return View(bPPB);
        }

        // POST: BPPBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BPPB bPPB = db.BPPBs.Find(id);
            db.BPPBs.Remove(bPPB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {


            ViewBag.BPPBID = id;
            var bPPB = db.BPPBs.Find(id);

            if (User.Identity.Name == null)
            {
                bPPB.Diminta  = "Jasin";
            }
            else
            {
                bPPB.Diminta = User.Identity.Name;
            }

            bPPB.Posting = true;

            db.Entry(bPPB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                ViewBag.Info = "Posting Berhasil";
            }
            catch (Exception e)
            {
                ViewBag.Info = string.Format("Posting Gagal. error: {0}", e.Message);
            }
            return View("Informasi");
        }

        public ActionResult Cetak(int id)
        {
            var bPPB = db.BPPBs.Where(x => x.BPPBID == id).FirstOrDefault();

            ViewBag.TglProduksi = bPPB.TglProduksi.ToShortDateString();
            ViewBag.TglPenimbangan = bPPB.TglPenimbangan.ToShortDateString();
            ViewBag.Keterangan = bPPB.Keterangan;
            ViewBag.BPPBID = id;
            ViewBag.NoBPPB = bPPB.NoBPPB;
            ViewBag.Diminta = bPPB.Diminta;
            ViewBag.Diserahkan = bPPB.Diserahkan;
            ViewBag.DiTerima = bPPB.Diterima;

            var bPPBRincians = db.BPPBRincians.Where(x => x.BPPBID == id).Include(b => b.bPPB).Include(b => b.gudangBahanBaku);
            return View(bPPBRincians.ToList());
   
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
