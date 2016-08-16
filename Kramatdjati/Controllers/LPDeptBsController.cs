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
    public class LPDeptBsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: LPDeptBs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            return View(db.LPDeptBs.Where(x => x.Posting == Posting).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "LPDeptBID,TglProduksi,Dilaporkan,Diketahui,JamKerjaAwal,JamKerjaAkhir,Catatan, Posting")] LPDeptB lPDeptB)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = lPDeptB.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: LPDeptBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptB lPDeptB = db.LPDeptBs.Find(id);
            if (lPDeptB == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptB);
        }

        // GET: LPDeptBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LPDeptBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LPDeptBID,TglProduksi,Dilaporkan,Diketahui,JamKerjaAwal,JamKerjaAkhir,Catatan, Posting")] LPDeptB lPDeptB)
        {
            if (ModelState.IsValid)
            {
                db.LPDeptBs.Add(lPDeptB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lPDeptB);
        }

        // GET: LPDeptBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptB lPDeptB = db.LPDeptBs.Find(id);
            if (lPDeptB == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptB);
        }

        // POST: LPDeptBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LPDeptBID,TglProduksi,Dilaporkan,Diketahui,JamKerjaAwal,JamKerjaAkhir,Catatan, Posting")] LPDeptB lPDeptB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lPDeptB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lPDeptB);
        }

        // GET: LPDeptBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LPDeptB lPDeptB = db.LPDeptBs.Find(id);
            if (lPDeptB == null)
            {
                return HttpNotFound();
            }
            return View(lPDeptB);
        }

        // POST: LPDeptBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LPDeptB lPDeptB = db.LPDeptBs.Find(id);
            db.LPDeptBs.Remove(lPDeptB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cetak(int id)
        {
            var lpDeptB = db.LPDeptBs.Where(x => x.LPDeptBID == id).FirstOrDefault();

            ViewBag.TglProduksi = lpDeptB.TglProduksi.ToShortDateString();
            ViewBag.Catatan = lpDeptB.Catatan;
            ViewBag.LPDeptBID = id;
            ViewBag.DibuatOleh = lpDeptB.Dilaporkan;
            var lPDeptBRincians = db.LPDeptBRincians.Where(x => x.LPDeptBID == id).Include(j => j.lpDeptB);
            return View(lPDeptBRincians.ToList());

        }

        public ActionResult PostingOld(int id)
        {


            ViewBag.LPDeptBID = id;
            var LpDeptB = db.LPDeptBs.Find(id);

            if (User.Identity.Name == "")
            {
                LpDeptB.Dilaporkan = "Jasin";
            }
            else
            {
                LpDeptB.Dilaporkan = User.Identity.Name;
            }

            LpDeptB.Posting = true;

            db.Entry(LpDeptB).State = EntityState.Modified;

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
            ViewBag.LPDeptBID = id;

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spLPDeptBPosting @LPDeptBID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@LPDeptBID", id),
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
