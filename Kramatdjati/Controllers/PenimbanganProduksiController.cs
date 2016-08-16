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
    public class PenimbanganProduksiController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PenimbanganProduksi
        public ActionResult Index(DateTime? TglProduksi)
        {
            if (TglProduksi == null)
            {
                TglProduksi = DateTime.Now ;
            };
            ViewBag.TglProduksi = string.Format("{0:yyyy-MM-dd}",TglProduksi ) ;
            var jPDeptARincians = db.JPDeptARincians.Where(x=>x.jpDeptA.TglProduksi == TglProduksi).Include(j => j.jpDeptA).Include(j => j.resep);
            return View(jPDeptARincians.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime TglProduksi)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { TglProduksi = TglProduksi });
            }
            return RedirectToAction("Index");
        }



        // GET: PenimbanganProduksi/Details/5
        public ActionResult Details(int? id, DateTime TglProduksi)
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

            ViewBag.TglProduksi = string.Format("{0:yyyy-MM-dd}", TglProduksi);
            return View(jPDeptARincian);
        }

        // GET: PenimbanganProduksi/Details/5
        public ActionResult DetailUndo(int? id, DateTime TglProduksi)
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

            ViewBag.TglProduksi = string.Format("{0:yyyy-MM-dd}", TglProduksi);
            return View(jPDeptARincian);
        }

        // GET: PenimbanganProduksiRincians/Edit/5
        public ActionResult Edit(int? id, DateTime TglProduksi)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptARincian  jPDeptARincian = db.JPDeptARincians.Find(id) ;
            if (jPDeptARincian == null)
            {
                return HttpNotFound();
            }

            ViewBag.TglProduksi = string.Format("{0:yyyy-MM-dd}", TglProduksi);
             return View(jPDeptARincian);
        }

        // POST: PenimbanganProduksiRincians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPDeptAID,JPDeptARincianID,KodeBarangJadi,Batch,Lembar,Keterangan,ResepID,NoLot,Posting,Penimbang")] JPDeptARincian  jPDeptARincian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPDeptARincian).State = EntityState.Modified;
                db.SaveChanges();

                var jPDeptA = db.JPDeptAs.Find(jPDeptARincian.JPDeptAID);
                DateTime TglProduksi = jPDeptA.TglProduksi;

                return RedirectToAction("Index", new { TglProduksi = TglProduksi });
            }
             return View(jPDeptARincian);
        }

        public ActionResult UndoPosting(int id)
        {
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spPenimbanganProduksiUndoPosting @JPDeptARincianID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@JPDeptARincianID", id),
                                                        new SqlParameter("@User", User.Identity.Name),
                                                        ReturnParameter);

            if (ReturnParameter.Value.ToString() == "0")
            {
                ViewBag.Info = "Undo Posting Berhasil";
            }
            else
            {
                ViewBag.Info = string.Format("Undo Posting Gagal. error: {0}", ReturnParameter.Value);
            };

            var jPDeptARincian = db.JPDeptARincians.Find(id);
            DateTime TglProduksi = jPDeptARincian.jpDeptA.TglProduksi;
            ViewBag.TglProduksi = string.Format("{0:yyyy-MM-dd}", TglProduksi);

            return View("Informasi");

        }
        public ActionResult Posting(int id)
        {
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spPenimbanganProduksiPosting @JPDeptARincianID, @User, @ReturnValue OUT",
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

            var jPDeptARincian = db.JPDeptARincians.Find(id);
            DateTime TglProduksi = jPDeptARincian.jpDeptA.TglProduksi;
            ViewBag.TglProduksi = string.Format("{0:yyyy-MM-dd}", TglProduksi);

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
