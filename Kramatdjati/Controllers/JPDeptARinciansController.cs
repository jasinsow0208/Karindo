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
    public class JPDeptARinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JPDeptARincians
        public ActionResult Index(int id)
        {
            var jpDeptA = db.JPDeptAs.Where(x => x.JPDeptAID  == id).FirstOrDefault() ;

            ViewBag.TglProduksi = jpDeptA.TglProduksi.ToShortDateString();
            ViewBag.Catatan = jpDeptA.Catatan;
            ViewBag.JPDeptAID = id;
           
            var jPDeptARincians = db.JPDeptARincians.Where(x=>x.JPDeptAID ==id).Include(j => j.jpDeptA);
            return View(jPDeptARincians.ToList());
        }

        // GET: JPDeptARincians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptARincian jPDeptARincian = db.JPDeptARincians.Find(id);
            if (jPDeptARincian == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptARincian);
        }

        // GET: JPDeptARincians/Create
        public ActionResult Create(int id)
        {
            ViewBag.JPDeptAID = id;

            var BarangJadi = db.BahanBakus.Where(x => x.KodeBarangJadi != null).
                                           Select(x => new { Value = x.KodeBarangJadi, Text = x.KodeBarangJadi }).
                                           Distinct().
                                           OrderBy(x=>x.Value) ;


            ViewBag.KodeBarangJadi = new SelectList(BarangJadi, "Value", "Text");
            ViewBag.ResepID = new SelectList(db.Reseps.Where(x=>x.Posting==true).OrderBy(x => x.KodeResep), "ResepID", "KodeResep");
            return View();
        }

        // POST: JPDeptARincians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPDeptARincianID,JPDeptAID,KodeBarangJadi,Batch,Lembar,Keterangan, ResepID, NoLot")] JPDeptARincian jPDeptARincian)
        {
            if (ModelState.IsValid)
            {
                db.JPDeptARincians.Add(jPDeptARincian);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jPDeptARincian.JPDeptAID });
            }

            ViewBag.JPDeptAID =  jPDeptARincian.JPDeptAID;

            var BarangJadi = db.BahanBakus.Where(x => x.KodeBarangJadi != null).
                                   Select(x => new { Value = x.KodeBarangJadi, Text = x.KodeBarangJadi }).
                                   Distinct().
                                   OrderBy(x => x.Value);

            ViewBag.KodeBarangJadi = new SelectList(BarangJadi, "Value", "Text" );
            ViewBag.ResepID = new SelectList(db.Reseps.Where(x => x.Posting == true).OrderBy(x => x.KodeResep), "ResepID", "KodeResep");

            return View(jPDeptARincian);
        }

        // GET: JPDeptARincians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptARincian jPDeptARincian = db.JPDeptARincians.Find(id);
            if (jPDeptARincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPDeptAID = jPDeptARincian.JPDeptAID;
            var BarangJadi = db.BahanBakus.Where(x => x.KodeBarangJadi != null).
                        Select(x => new { Value = x.KodeBarangJadi, Text = x.KodeBarangJadi }).
                        Distinct().
                        OrderBy(x => x.Value);

            ViewBag.KodeBarangJadi = new SelectList(BarangJadi, "Value", "Text", jPDeptARincian.KodeBarangJadi);
            ViewBag.ResepID = new SelectList(db.Reseps.Where(x => x.Posting == true).OrderBy(x => x.KodeResep), "ResepID", "KodeResep", jPDeptARincian.ResepID );

            return View(jPDeptARincian);
        }

        // POST: JPDeptARincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPDeptARincianID,JPDeptAID,KodeBarangJadi,Batch,Lembar,Keterangan, ResepID, NoLot")] JPDeptARincian jPDeptARincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPDeptARincian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = jPDeptARincian.JPDeptAID });
            }
            ViewBag.JPDeptAID =  jPDeptARincian.JPDeptAID;
            var BarangJadi = db.BahanBakus.Where(x => x.KodeBarangJadi != null).
             Select(x => new { Value = x.KodeBarangJadi, Text = x.KodeBarangJadi }).
             Distinct().
             OrderBy(x => x.Value);

            ViewBag.KodeBarangJadi = new SelectList(BarangJadi, "Value", "Text", jPDeptARincian.KodeBarangJadi);
            ViewBag.ResepID = new SelectList(db.Reseps.Where(x => x.Posting == true).OrderBy(x => x.KodeResep), "ResepID", "KodeResep", jPDeptARincian.ResepID);

            return View(jPDeptARincian);
        }

        // GET: JPDeptARincians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptARincian jPDeptARincian = db.JPDeptARincians.Find(id);
            if (jPDeptARincian == null)
            {
                return HttpNotFound();
            }

            ViewBag.JPDeptID = jPDeptARincian.JPDeptAID;
            ViewBag.ResepID = new SelectList(db.Reseps.Where(x => x.Posting == true).OrderBy(x => x.KodeResep), "ResepID", "KodeResep", jPDeptARincian.ResepID);

            return View(jPDeptARincian);
        }

        // POST: JPDeptARincians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPDeptARincian jPDeptARincian = db.JPDeptARincians.Find(id);
            db.JPDeptARincians.Remove(jPDeptARincian);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = jPDeptARincian.JPDeptAID });
        }

        public ActionResult Posting(int id)
        {
            var jpDeptARincian = db.JPDeptARincians.Find(id);
            ViewBag.JPDeptAID = jpDeptARincian.JPDeptAID ;

            ViewBag.JPDeptARincianID = id;
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spJPDeptARincianPosting @JPDeptARincianID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@JPDeptARincianID", id),
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
