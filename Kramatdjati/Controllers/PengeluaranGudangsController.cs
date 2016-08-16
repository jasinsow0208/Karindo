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
    public class PengeluaranGudangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PengeluaranGudangs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            var suratJalans = db.SuratJalans.Where(x=>x.Posting ==true && x.Closed == Posting).Include(s => s.contact );
            return View(suratJalans.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "SuratJalanID,ContactID,NoSuratJalan,tglSuratJalan,TglTransaksi,Posting,TglPosting,User")] SuratJalan suratJalan)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = suratJalan.Closed  });
            }
            return RedirectToAction("Index");
        }

        // GET: PengeluaranGudangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuratJalan suratJalan = db.SuratJalans.Find(id);
            if (suratJalan == null)
            {
                return HttpNotFound();
            }
            return View(suratJalan);
        }

        public ActionResult PostingOld(int id)
        {
            SuratJalan suratJalan = db.SuratJalans.Find(id);
            if (suratJalan == null)
            {
                return HttpNotFound();
            }

             using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    suratJalan.Closed  = true;
                    suratJalan.Gudang = User.Identity.Name;
                    db.Entry(suratJalan).State = EntityState.Modified;
                    db.SaveChanges();

                    transaction.Commit();

                    ViewBag.Info = "Proses tutup surat jalan Berhasil";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.Info = "Proses tutup surat jalan Tidak berhasil " + e.Message.Trim();

                }
            }
            return View("Informasi");
        }

        public ActionResult Posting(int id)
        {
            ViewBag.SuratJalanID = id;

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spSuratJalanPosting @SuratJalanID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@SuratJalanID", id),
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
