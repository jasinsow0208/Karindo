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
    public class LPDeptAsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: LPDeptAs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            return View(db.LPDeptAs.Where(x => x.Posting == Posting).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "LPDeptAID,TglProduksi,PenimbanganAwal,PenimbanganAkhir,KneaderAwal,KneaderAkhir,BoilerAwal,BoilerAkhir,RollAwal,RollAkhir,HotPressBAwal,HotPressBAkhir,HotPressKAwal,HotPressKAkhir,Dilaporkan,Diketahui,Catatan, Posting")] LPDeptA lPDeptA)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = lPDeptA.Posting });
            }
            return RedirectToAction("Index");
        }
        // GET: LPDeptAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptA lPDeptA = db.LPDeptAs.Find(id);
            if (lPDeptA == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptA);
        }

        // GET: LPDeptAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LPDeptAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LPDeptAID,TglProduksi,PenimbanganAwal,PenimbanganAkhir,KneaderAwal,KneaderAkhir,BoilerAwal,BoilerAkhir,RollAwal,RollAkhir,HotPressBAwal,HotPressBAkhir,HotPressKAwal,HotPressKAkhir,Dilaporkan,Diketahui,Catatan")] LPDeptA lPDeptA)
        {
            if (ModelState.IsValid)
            {
                db.LPDeptAs.Add(lPDeptA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lPDeptA);
        }

        // GET: LPDeptAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptA lPDeptA = db.LPDeptAs.Find(id);
            if (lPDeptA == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptA);
        }

        // POST: LPDeptAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LPDeptAID,TglProduksi,PenimbanganAwal,PenimbanganAkhir,KneaderAwal,KneaderAkhir,BoilerAwal,BoilerAkhir,RollAwal,RollAkhir,HotPressBAwal,HotPressBAkhir,HotPressKAwal,HotPressKAkhir,Dilaporkan,Diketahui,Catatan")] LPDeptA lPDeptA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lPDeptA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lPDeptA);
        }

        // GET: LPDeptAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptA lPDeptA = db.LPDeptAs.Find(id);
            if (lPDeptA == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptA);
        }

        // POST: LPDeptAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LPDeptA lPDeptA = db.LPDeptAs.Find(id);
            db.LPDeptAs.Remove(lPDeptA);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cetak(int id)
        {
            var lpDeptA = db.LPDeptAs.Where(x => x.LPDeptAID == id).FirstOrDefault();

            ViewBag.TglProduksi = lpDeptA.TglProduksi.ToShortDateString();
            ViewBag.Catatan = lpDeptA.Catatan;
            ViewBag.LPDeptAID = id;
            ViewBag.DibuatOleh = lpDeptA.Dilaporkan ;
            var lPDeptARincians = db.LPDeptARincians.Where(x => x.LPDeptAID == id).Include(j => j.lpDeptA);
            return View(lPDeptARincians.ToList());

        }

        public ActionResult PostingOld(int id)
        {


            ViewBag.LPDeptAID = id;
            var LpDeptA = db.LPDeptAs.Find(id);

            if (User.Identity.Name == null)
            {
                LpDeptA.Dilaporkan  = "Jasin";
            }
            else
            {
                LpDeptA.Dilaporkan  = User.Identity.Name;
            }

            LpDeptA.Posting = true;

            db.Entry(LpDeptA).State = EntityState.Modified;

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
        public ActionResult Posting(int id)
        {
            ViewBag.LPDeptAID = id;
            var LpDeptA = db.LPDeptAs.Find(id);

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spLPDeptAPosting @LPDeptAID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@LPDeptAID", id),
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
