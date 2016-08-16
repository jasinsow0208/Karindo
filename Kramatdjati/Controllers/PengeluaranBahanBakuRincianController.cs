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
    public class PengeluaranBahanBakuRincianController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PengeluaranBahanBakuRincian
        public ActionResult Index(int id)
        {
            var bPPB = db.BPPBs.Where(x => x.BPPBID == id).FirstOrDefault();

            ViewBag.TglProduksi = bPPB.TglProduksi.ToShortDateString();
            ViewBag.TglPenimbangan = bPPB.TglPenimbangan.ToShortDateString();
            ViewBag.Keterangan = bPPB.Keterangan;
            ViewBag.BPPBID = id;
            ViewBag.NoBPPB = bPPB.NoBPPB;

            var bPPBRincians = db.BPPBRincians.Where(x => x.BPPBID == id).Include(b => b.bPPB).Include(b => b.gudangBahanBaku);
            return View(bPPBRincians.ToList());
        }

        // GET: PengeluaranBahanBakuRincian/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BPPBRincian bPPBRincian = db.BPPBRincians.Find(id);
            if (bPPBRincian == null)
            {
                return HttpNotFound();
            }
            ViewBag.BPPBID = bPPBRincian.BPPBID;
            return View(bPPBRincian);
        }

        public ActionResult Posting(int id)
        {
            var bPPB = db.BPPBRincians.Find(id);
            ViewBag.BPPBID = bPPB.BPPBID;

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spPengeluaranBahanBakuPosting @BPPBRincianID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@BPPBRincianID", id),
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

        public ActionResult PasswordPenerimaan(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            ViewBag.BPPBID = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordPenerimaan(string Password, int id)
        {
            if (ModelState.IsValid)
            {
                var penerima = db.PasswordSerahTerimas.Where(x => x.Password == Password);

                string NamaPenerima = "";
                foreach (PasswordSerahTerima rw in penerima)
                {
                    NamaPenerima = rw.Operator;
                }

                if (NamaPenerima == "")
                {
                    ViewBag.Info = "Password salah";
                    return RedirectToAction("PasswordPenerimaan", new { id = id });
                }
                else
                {
                    ViewBag.Info = PostingPenerima(NamaPenerima, id);
                    var bppb = db.BPPBs.Find(id);
                    bppb.Diserahkan = User.Identity.Name;
                    db.Entry(bppb).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = id });
                }
            }

            ViewBag.Info = "Model salah";
            return RedirectToAction("Informasi"); 
        }

        private string PostingPenerima(string UserName, int id)
        {
            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spPenerimaanBahanBakuAllPosting @BPPBID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@BPPBID", id),
                                                        new SqlParameter("@User", UserName),
                                                        ReturnParameter);

            if (ReturnParameter.Value.ToString() == "0")
            {
                return "Posting Berhasil";
            }
            else
            {
                return string.Format("Posting Gagal. error: {0}", ReturnParameter.Value);
            };


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
