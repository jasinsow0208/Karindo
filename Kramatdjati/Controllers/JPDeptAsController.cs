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
    public class JPDeptAsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: JPDeptAs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            return View(db.JPDeptAs.Where(x => x.Posting == Posting).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "JPDeptAID,TglProduksi,DibuatOleh,Catatan,Posting, TglPenimbangan")] JPDeptA jPDeptA)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = jPDeptA.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: JPDeptAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptA jPDeptA = db.JPDeptAs.Find(id);
            if (jPDeptA == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptA);
        }

        // GET: JPDeptAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JPDeptAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPDeptAID,TglProduksi,DibuatOleh,Catatan,Posting, TglPenimbangan")] JPDeptA jPDeptA)
        {
            if (ModelState.IsValid)
            {
                db.JPDeptAs.Add(jPDeptA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jPDeptA);
        }

        // GET: JPDeptAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptA jPDeptA = db.JPDeptAs.Find(id);
            if (jPDeptA == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptA);
        }

        // POST: JPDeptAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPDeptAID,TglProduksi,DibuatOleh,Catatan, Posting, TglPenimbangan")] JPDeptA jPDeptA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPDeptA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jPDeptA);
        }

        // GET: JPDeptAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPDeptA jPDeptA = db.JPDeptAs.Find(id);
            if (jPDeptA == null)
            {
                return HttpNotFound();
            }
            return View(jPDeptA);
        }

 
        // POST: JPDeptAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPDeptA jPDeptA = db.JPDeptAs.Find(id);
            db.JPDeptAs.Remove(jPDeptA);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cetak(int id)
        {
            var jpDeptA = db.JPDeptAs.Where(x => x.JPDeptAID == id).FirstOrDefault();

            ViewBag.TglProduksi = jpDeptA.TglProduksi.ToShortDateString();
            ViewBag.Catatan = jpDeptA.Catatan;
            ViewBag.JPDeptAID = id;
            ViewBag.DibuatOleh = jpDeptA.DibuatOleh;
            var jPDeptARincians = db.JPDeptARincians.Where(x => x.JPDeptAID == id).Include(j => j.jpDeptA);
            return View(jPDeptARincians.ToList());
 
        }

        public ActionResult Posting(int id)
        {
            ViewBag.JPDeptAID = id;
            
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spJPDeptAPosting @JPDeptAID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@JPDeptAID", id),
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

       public ActionResult PostingOld(int id)
        {


            ViewBag.JPDeptAID = id;
            var jpDeptA = db.JPDeptAs.Find(id);

            if (User.Identity.Name == null)
            {
                jpDeptA.DibuatOleh = "Jasin";
            }
            else
            {
                jpDeptA.DibuatOleh = User.Identity.Name;
            }

            jpDeptA.Posting = true;

            db.Entry(jpDeptA).State = EntityState.Modified;

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
