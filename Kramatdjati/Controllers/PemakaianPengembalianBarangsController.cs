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
    public class PemakaianPengembalianBarangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PemakaianPengembalianBarangs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            var pemakaianPengembalianBarangs = db.PemakaianPengembalianBarangs.Where(x => x.Posting == Posting).Include(p => p.gudang);
            return View(pemakaianPengembalianBarangs.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PemakaianPengembalianBarangID,GudangID,NoSuratPemakaian,Status,tglKeluarBarang,Posting,TglPosting,User,Penerima")] PemakaianPengembalianBarang pemakaianPengembalianBarang)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = pemakaianPengembalianBarang.Posting });
            }
            return RedirectToAction("Index");
                
           }

        // GET: PemakaianPengembalianBarangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemakaianPengembalianBarang pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(id);
            if (pemakaianPengembalianBarang == null)
            {
                return HttpNotFound();
            }
            return View(pemakaianPengembalianBarang);
        }

        // GET: PemakaianPengembalianBarangs/Create
        public ActionResult Create()
        {
            List<StatusPemakaian> statusPemakaians=new List<StatusPemakaian> ();
            statusPemakaians.Add( new StatusPemakaian  {StatusID="Pemakaian", Status ="Pemakaian"}) ;
            statusPemakaians.Add(new StatusPemakaian { StatusID = "Pengembalian", Status = "Pengembalian" });

            ViewBag.Status=new SelectList (statusPemakaians.ToList(), "StatusID", "Status");
            ViewBag.GudangID = new SelectList(db.Gudangs, "GudangID", "Lokasi");
            return View();
        }

        // POST: PemakaianPengembalianBarangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PemakaianPengembalianBarangID,GudangID,NoSuratPemakaian,Status,tglKeluarBarang,Posting,TglPosting,User,Penerima")] PemakaianPengembalianBarang pemakaianPengembalianBarang)
        {
            if (ModelState.IsValid)
            {
                pemakaianPengembalianBarang.TglPosting = DateTime.Parse("2001-01-01");
                db.PemakaianPengembalianBarangs.Add(pemakaianPengembalianBarang);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<StatusPemakaian> statusPemakaians=new List<StatusPemakaian> ();
            statusPemakaians.Add( new StatusPemakaian  {StatusID="Pemakaian", Status ="Pemakaian"}) ;
            statusPemakaians.Add(new StatusPemakaian { StatusID = "Pengembalian", Status = "Pengembalian" });

            ViewBag.Status=new SelectList (statusPemakaians.ToList(), "StatusID", "Status", pemakaianPengembalianBarang.Status );
            ViewBag.GudangID = new SelectList(db.Gudangs, "GudangID", "Lokasi", pemakaianPengembalianBarang.GudangID);
            return View(pemakaianPengembalianBarang);
        }

        // GET: PemakaianPengembalianBarangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemakaianPengembalianBarang pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(id);
            if (pemakaianPengembalianBarang == null)
            {
                return HttpNotFound();
            }

            List<StatusPemakaian> statusPemakaians=new List<StatusPemakaian> ();
            statusPemakaians.Add( new StatusPemakaian  {StatusID="Pemakaian", Status ="Pemakaian"}) ;
            statusPemakaians.Add(new StatusPemakaian { StatusID = "Pengembalian", Status = "Pengembalian" });

            ViewBag.Status=new SelectList (statusPemakaians.ToList(), "StatusID", "Status", pemakaianPengembalianBarang.Status );

             return View(pemakaianPengembalianBarang);
        }

        // POST: PemakaianPengembalianBarangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PemakaianPengembalianBarangID,GudangID,NoSuratPemakaian,Status,tglKeluarBarang,Posting,TglPosting,User,Penerima")] PemakaianPengembalianBarang pemakaianPengembalianBarang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pemakaianPengembalianBarang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<StatusPemakaian> statusPemakaians=new List<StatusPemakaian> ();
            statusPemakaians.Add( new StatusPemakaian  {StatusID="Pemakaian", Status ="Pemakaian"}) ;
            statusPemakaians.Add(new StatusPemakaian { StatusID = "Pengembalian", Status = "Pengembalian" });

            ViewBag.Status=new SelectList (statusPemakaians.ToList(), "StatusID", "Status", pemakaianPengembalianBarang.Status );
            return View(pemakaianPengembalianBarang);
        }

        // GET: PemakaianPengembalianBarangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemakaianPengembalianBarang pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(id);
            if (pemakaianPengembalianBarang == null)
            {
                return HttpNotFound();
            }
            return View(pemakaianPengembalianBarang);
        }

        // POST: PemakaianPengembalianBarangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PemakaianPengembalianBarang pemakaianPengembalianBarang = db.PemakaianPengembalianBarangs.Find(id);
            db.PemakaianPengembalianBarangs.Remove(pemakaianPengembalianBarang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {
            ViewBag.PemakaianPengembalianBarangID = id;

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spPemakaianPengembalianBarangPosting @PemakaianPengembalianBarangID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@PemakaianPengembalianBarangID", id),
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
