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
    public class PindahGudangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PindahGudangs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            var pindahGudangs = db.PindahGudangs.Where(x => x.Posting == Posting).Include(p => p.gudangAsal).Include(p => p.gudangTujuan);
            return View(pindahGudangs.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PindahGudangID,BuktiPindahGudang,GudangAsalID,GudangTujuanID,TglTransaksi,UserGudangAsal,UserGudangTujuan,Posting,TglPosting, Keterangan")] PindahGudang pindahGudang)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = pindahGudang.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: PindahGudangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PindahGudang pindahGudang = db.PindahGudangs.Find(id);
            if (pindahGudang == null)
            {
                return HttpNotFound();
            }
            return View(pindahGudang);
        }

        // GET: PindahGudangs/Create
        public ActionResult Create()
        {
            ViewBag.GudangAsalID = new SelectList(db.Gudangs, "GudangID", "Lokasi");
            ViewBag.GudangTujuanID = new SelectList(db.Gudangs, "GudangID", "Lokasi");
            return View();
        }

        // POST: PindahGudangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PindahGudangID,BuktiPindahGudang,GudangAsalID,GudangTujuanID,TglTransaksi,UserGudangAsal,UserGudangTujuan,Posting,TglPosting, Keterangan")] PindahGudang pindahGudang)
        {
            if (ModelState.IsValid)
            {
                pindahGudang.TglPosting = DateTime.Parse("2001-01-01");
                pindahGudang.KeluarTglPosting = DateTime.Parse("2001-01-01");
                pindahGudang.MasukTglPosting  = DateTime.Parse("2001-01-01");
                db.PindahGudangs.Add(pindahGudang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GudangAsalID = new SelectList(db.Gudangs, "GudangID", "Lokasi", pindahGudang.GudangAsalID);
            ViewBag.GudangTujuanID = new SelectList(db.Gudangs, "GudangID", "Lokasi", pindahGudang.GudangTujuanID);
            return View(pindahGudang);
        }

        // GET: PindahGudangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PindahGudang pindahGudang = db.PindahGudangs.Find(id);
            if (pindahGudang == null)
            {
                return HttpNotFound();
            }
            ViewBag.GudangAsalID = new SelectList(db.Gudangs, "GudangID", "Lokasi", pindahGudang.GudangAsalID);
            ViewBag.GudangTujuanID = new SelectList(db.Gudangs, "GudangID", "Lokasi", pindahGudang.GudangTujuanID);
            return View(pindahGudang);
        }

        // POST: PindahGudangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PindahGudangID,BuktiPindahGudang,GudangAsalID,GudangTujuanID,TglTransaksi,UserGudangAsal,UserGudangTujuan,Posting,TglPosting, Keterangan")] PindahGudang pindahGudang)
        {
            if (ModelState.IsValid)
            {
                pindahGudang.TglPosting = DateTime.Parse("2001-01-01");
                db.Entry(pindahGudang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GudangAsalID = new SelectList(db.Gudangs, "GudangID", "Lokasi", pindahGudang.GudangAsalID);
            ViewBag.GudangTujuanID = new SelectList(db.Gudangs, "GudangID", "Lokasi", pindahGudang.GudangTujuanID);
            return View(pindahGudang);
        }

        // GET: PindahGudangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PindahGudang pindahGudang = db.PindahGudangs.Find(id);
            if (pindahGudang == null)
            {
                return HttpNotFound();
            }
            return View(pindahGudang);
        }

        // POST: PindahGudangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PindahGudang pindahGudang = db.PindahGudangs.Find(id);
            db.PindahGudangs.Remove(pindahGudang);
            db.SaveChanges();
            return RedirectToAction("Index");
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
