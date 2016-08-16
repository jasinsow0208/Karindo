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
    public class StokOpnameRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: StokOpnameRincians
        public ActionResult Index(int Id)
        {
            StokOpname  stokOpname = db.StokOpnames.Find(Id);

            ViewBag.TglBuat = stokOpname.TglBuat.ToShortDateString ();

            ViewBag.StokOpnameID = Id;
            var stokOpnameRincians = db.StokOpnameRincians.Where(x=>x.StokOpnameID ==Id).Include(s => s.bahanBaku).Include(s => s.stokOpname);
            return View(stokOpnameRincians.ToList());
        }

        // GET: StokOpnameRincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokOpnameRincian stokOpnameRincian = db.StokOpnameRincians.Find(id);
            if (stokOpnameRincian == null)
            {
                return HttpNotFound();
            }
            return View(stokOpnameRincian);
        }

        // GET: StokOpnameRincians/Create
        public ActionResult Create(int Id)
        {
            ViewBag.UserInput = User.Identity.Name;
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus.OrderBy(x=>x.KodeBahanBaku) , "BahanBakuID", "KodeBahanBaku");
            ViewBag.StokOpnameID = Id;
            return View();
        }

        // POST: StokOpnameRincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StokOpnameRincianID,StokOpnameID,BahanBakuID,Jumlah,HargaSatuan,UserInput")] StokOpnameRincian stokOpnameRincian)
        {
            if (ModelState.IsValid)
            {
                db.StokOpnameRincians.Add(stokOpnameRincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = stokOpnameRincian.StokOpnameID  });
            }

            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", stokOpnameRincian.BahanBakuID);
            ViewBag.StokOpnameID = stokOpnameRincian.StokOpnameID;
            return View(stokOpnameRincian);
        }

        // GET: StokOpnameRincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokOpnameRincian stokOpnameRincian = db.StokOpnameRincians.Find(id);
            if (stokOpnameRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus, "BahanBakuID", "KodeBahanBaku", stokOpnameRincian.BahanBakuID);
            ViewBag.StokOpnameID =  stokOpnameRincian.StokOpnameID;
            return View(stokOpnameRincian);
        }

        // POST: StokOpnameRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StokOpnameRincianID,StokOpnameID,BahanBakuID,Jumlah,HargaSatuan,UserInput")] StokOpnameRincian stokOpnameRincian)
        {
            if (ModelState.IsValid)
            {
                stokOpnameRincian.UserInput = User.Identity.Name;
                db.Entry(stokOpnameRincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = stokOpnameRincian.StokOpnameID });
            }
            ViewBag.BahanBakuID = new SelectList(db.BahanBakus.OrderBy(x => x.KodeBahanBaku), "BahanBakuID", "KodeBahanBaku", stokOpnameRincian.BahanBakuID);
            ViewBag.StokOpnameID =  stokOpnameRincian.StokOpnameID;
            return View(stokOpnameRincian);
        }

        // GET: StokOpnameRincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokOpnameRincian stokOpnameRincian = db.StokOpnameRincians.Find(id);
            if (stokOpnameRincian == null)
            {
                return HttpNotFound();
            }
            return View(stokOpnameRincian);
        }

        // POST: StokOpnameRincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StokOpnameRincian stokOpnameRincian = db.StokOpnameRincians.Find(id);
            db.StokOpnameRincians.Remove(stokOpnameRincian);
            db.SaveChanges();
            return RedirectToAction("Index",new { id = stokOpnameRincian.StokOpnameID });
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
