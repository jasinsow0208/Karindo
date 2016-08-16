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
    public class JPDeptBsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JPDeptBs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            return View(db.JPDeptBs.Where(x => x.Posting == Posting).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "JPDeptBID,TglProduksiDeptA, TglProduksiDeptB,DibuatOleh,Catatan, Posting")] JPDeptB jPDeptB)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = jPDeptB.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: JPDeptBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptB jPDeptB = db.JPDeptBs.Find(id);
            if (jPDeptB == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptB);
        }

        // GET: JPDeptBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JPDeptBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPDeptBID,TglProduksiDeptA, TglProduksiDeptB,DibuatOleh,Catatan, Posting")] JPDeptB jPDeptB)
        {
            if (ModelState.IsValid)
            {
                db.JPDeptBs.Add(jPDeptB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jPDeptB);
        }

        // GET: JPDeptBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptB jPDeptB = db.JPDeptBs.Find(id);
            if (jPDeptB == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptB);
        }

        // POST: JPDeptBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPDeptBID,TglProduksiDeptA, TglProduksiDeptB,DibuatOleh,Catatan, Posting")] JPDeptB jPDeptB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPDeptB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jPDeptB);
        }

        // GET: JPDeptBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptB jPDeptB = db.JPDeptBs.Find(id);
            if (jPDeptB == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptB);
        }

        // POST: JPDeptBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPDeptB jPDeptB = db.JPDeptBs.Find(id);
            db.JPDeptBs.Remove(jPDeptB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cetak(int id)
        {
            var jpDeptB = db.JPDeptBs.Where(x => x.JPDeptBID == id).FirstOrDefault();

            ViewBag.TglProduksi = jpDeptB.TglProduksiDeptB.ToShortDateString();
            ViewBag.TglProduksiDeptA = jpDeptB.TglProduksiDeptA.ToShortDateString();
            ViewBag.Catatan = jpDeptB.Catatan;
            ViewBag.JPDeptBID = id;
            ViewBag.DibuatOleh = jpDeptB.DibuatOleh;
            var jPDeptBRincians = db.JPDeptBRincians.Where(x => x.JPDeptBID == id).Include(j => j.jpDeptB);
            return View(jPDeptBRincians.ToList());

        }

        public ActionResult Posting(int id)
        {


            ViewBag.JPDeptBID = id;
            var jpDeptB = db.JPDeptBs.Find(id);

            if (User.Identity.Name == "")
            {
                jpDeptB.DibuatOleh = "Jasin";
            }
            else
            {
                jpDeptB.DibuatOleh = User.Identity.Name;
            }

            jpDeptB.Posting = true;

            db.Entry(jpDeptB).State = EntityState.Modified;

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
