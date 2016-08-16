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
    public class PenerimaanBahanBakuRincianController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PenerimaanBahanBakuRincian
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

        // GET: PenerimaanBahanBakuRincian/Details/5
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

            var Data = db.Database.ExecuteSqlCommand("spPenerimaanBahanBakuPosting @BPPBRincianID, @User, @ReturnValue OUT",
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
