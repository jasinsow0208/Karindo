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
using System.Data.SqlClient;

namespace Kramatdjati.Controllers
{
    public class OrderBahanBakusController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: OrderBahanBakus
        public ActionResult Index(int id)
        {
            var JPDeptARincian = db.JPDeptARincians.Where(x => x.JPDeptARincianID == id).Include(x=>x.jpDeptA ).FirstOrDefault();

            ViewBag.KodeBarangJadi = JPDeptARincian.KodeBarangJadi   ;
            ViewBag.Catatan = JPDeptARincian.Keterangan    ;
            ViewBag.Batch = JPDeptARincian.Batch ;
            ViewBag.Lembar = JPDeptARincian.Lembar ;
            ViewBag.JPDeptARincianID = id;
            ViewBag.JPDeptAID = JPDeptARincian.JPDeptAID;
 
            var orderBahanBakus = db.OrderBahanBakus.Where(x=>x.JPDeptARincianID ==id).Include(o => o.resep).Include(o => o.jpDeptARincian  );
            return View(orderBahanBakus.ToList());
        }

        // GET: OrderBahanBakus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderBahanBaku orderBahanBaku = db.OrderBahanBakus.Find(id);
            if (orderBahanBaku == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptARincianID = orderBahanBaku.JPDeptARincianID;
            return View(orderBahanBaku);
        }

        // GET: OrderBahanBakus/Create
        public ActionResult Create(int id)
        {
            ViewBag.ResepID = new SelectList(db.Reseps.Where(x=>x.Posting==true).OrderBy(x => x.KodeResep),"ResepID", "KodeResep");
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi");
            ViewBag.JPDeptARincianID = id;
            return View();
        }

        // POST: OrderBahanBakus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderBahanBakuID,ResepID,JPDeptARincianID,Keterangan,Posting, GudangID")] OrderBahanBaku orderBahanBaku)
        {
            if (ModelState.IsValid)
            {
                db.OrderBahanBakus.Add(orderBahanBaku);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = orderBahanBaku.JPDeptARincianID });
            }

            ViewBag.ResepID = new SelectList(db.Reseps.Where(x => x.Posting == true).OrderBy(x => x.KodeResep), "ResepID", "KodeResep", orderBahanBaku.ResepID);
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi ), "GudangID", "Lokasi", orderBahanBaku.GudangID );
            ViewBag.JPDeptARincianID = orderBahanBaku.JPDeptARincianID;
            return View(orderBahanBaku);
        }

        // GET: OrderBahanBakus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderBahanBaku orderBahanBaku = db.OrderBahanBakus.Find(id);
            if (orderBahanBaku == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResepID = new SelectList(db.Reseps.Where(x => x.Posting == true).OrderBy(x => x.KodeResep), "ResepID", "KodeResep", orderBahanBaku.ResepID);
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi", orderBahanBaku.GudangID);
            ViewBag.JPDeptARincianID = orderBahanBaku.JPDeptARincianID;
            return View(orderBahanBaku);
        }

        // POST: OrderBahanBakus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderBahanBakuID,ResepID,JPDeptARincianID,Keterangan,Posting, GudangID")] OrderBahanBaku orderBahanBaku)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderBahanBaku).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = orderBahanBaku.JPDeptARincianID });
            }
            ViewBag.ResepID = new SelectList(db.Reseps.Where(x => x.Posting == true).OrderBy(x => x.KodeResep), "ResepID", "KodeResep", orderBahanBaku.ResepID);
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi", orderBahanBaku.GudangID);
            ViewBag.JPDeptARincianID = orderBahanBaku.JPDeptARincianID;
            return View(orderBahanBaku);
        }

        // GET: OrderBahanBakus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderBahanBaku orderBahanBaku = db.OrderBahanBakus.Find(id);
            if (orderBahanBaku == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptARincianID = orderBahanBaku.JPDeptARincianID;
            return View(orderBahanBaku);
        }

        // POST: OrderBahanBakus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderBahanBaku orderBahanBaku = db.OrderBahanBakus.Find(id);
            db.OrderBahanBakus.Remove(orderBahanBaku);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = orderBahanBaku.JPDeptARincianID });
        }

        public ActionResult Posting(int id)
        {
            var orderBahanbaku = db.OrderBahanBakus.Find(id);
            ViewBag.JPDeptARincianID = orderBahanbaku.JPDeptARincianID;
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spOrderBahanBakuPosting @OrderBahanBakuID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@OrderBahanBakuID", id),
                                                        new SqlParameter("@User", User.Identity.Name),
                                                        ReturnParameter);

            if (ReturnParameter.Value.ToString() == "0")
            {
                ViewBag.Info = "Posting Berhasil";
            }
            else
            {
                ViewBag.Info = string.Format("Posting Gagal. error: {0}", ReturnParameter.Value);
            };

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
