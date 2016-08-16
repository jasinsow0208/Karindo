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
    public class StokOpnamesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: StokOpnames
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };

            return View(db.StokOpnames.Where(x => x.Posting == Posting).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "StokOpnameID,TglBuat,TglPosting,UserPosting,Posting, GudangID")] StokOpname stokOpname)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = stokOpname.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: StokOpnames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokOpname stokOpname = db.StokOpnames.Find(id);
            if (stokOpname == null)
            {
                return HttpNotFound();
            }
            return View(stokOpname);
        }

        public ActionResult BatalPosting(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokOpname stokOpname = db.StokOpnames.Find(id);
            if (stokOpname == null)
            {
                return HttpNotFound();
            }
            return View(stokOpname);
        }

        public ActionResult Posting(int id)
        {
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spStokOpnamePosting @StokOpnameID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@StokOpnameID", id),
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

        public ActionResult UndoPosting(int id)
        {
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spStokOpnamePostingUndo @StokOpnameID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@StokOpnameID", id),
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

            return View("Informasi");
        }
        // GET: StokOpnames/Create
        public ActionResult Create()
        {
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi");
            return View();
        }

        // POST: StokOpnames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StokOpnameID,TglBuat,TglPosting,UserPosting,Posting, GudangID")] StokOpname stokOpname)
        {
            if (ModelState.IsValid)
            {
                stokOpname.TglPosting = DateTime.Parse("2001-01-01");
                db.StokOpnames.Add(stokOpname);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi");
            return View(stokOpname);
        }

        // GET: StokOpnames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokOpname stokOpname = db.StokOpnames.Find(id);
            if (stokOpname == null)
            {
                return HttpNotFound();
            }
            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi", stokOpname.GudangID );
            return View(stokOpname);
        }

        // POST: StokOpnames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StokOpnameID,TglBuat,TglPosting,UserPosting,Posting, GudangID")] StokOpname stokOpname)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stokOpname).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GudangID = new SelectList(db.Gudangs.OrderBy(x => x.Lokasi), "GudangID", "Lokasi", stokOpname.GudangID);

            return View(stokOpname);
        }

        // GET: StokOpnames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokOpname stokOpname = db.StokOpnames.Find(id);
            if (stokOpname == null)
            {
                return HttpNotFound();
            }
            return View(stokOpname);
        }

        // POST: StokOpnames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StokOpname stokOpname = db.StokOpnames.Find(id);
            db.StokOpnames.Remove(stokOpname);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cetak(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokOpname stokOpname = db.StokOpnames.Find(Id);

            ViewBag.TglBuat = stokOpname.TglBuat.ToShortDateString();

            ViewBag.StokOpnameID = Id;
            var stokOpnameRincians = db.StokOpnameRincians.Where(x => x.StokOpnameID == Id).Include(s => s.bahanBaku).Include(s => s.stokOpname).OrderBy (x=>x.bahanBaku.Divisi.Keterangan).OrderBy(x=>x.bahanBaku .Departemen.Keterangan );
            return View(stokOpnameRincians.ToList());
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
