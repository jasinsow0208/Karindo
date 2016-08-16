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

namespace Kramatdjati.Controllers
{
    public class PenerimaanBahanBakuController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PenerimaanBahanBaku
        public ActionResult Index()
        {
            var bPPBs = db.BPPBs.Where(x => x.Posting == true && x.Diserahkan != null && x.Diterima == null).Include(b => b.jPDeptA);
            List<BPPBView> bppbViews = new List<BPPBView>();

            foreach (BPPB rw in bPPBs)
            {
                BPPBView bppbView = new BPPBView();
                bppbView.BPPBID = rw.BPPBID;
                bppbView.Diminta = rw.Diminta;
                bppbView.Diserahkan = rw.Diserahkan;
                bppbView.Diterima = rw.Diterima;
                bppbView.Keterangan = rw.Keterangan;
                bppbView.NoBPPB = rw.NoBPPB;
                bppbView.Posting = rw.Posting;
                bppbView.TglPenimbangan = rw.TglPenimbangan;
                bppbView.TglProduksi = rw.TglProduksi;

                int jmlBlmPosting = db.BPPBRincians.Where(x => x.BPPBID == rw.BPPBID && x.PostingDiterima  == false).Count();

                if (jmlBlmPosting > 0)
                {
                    bppbView.PostingOK = false;
                }
                else
                {
                    bppbView.PostingOK = true;
                }

                bppbViews.Add(bppbView);
            }

            return View(bppbViews.ToList());
        }

        // GET: PenerimaanBahanBaku/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BPPB bPPB = db.BPPBs.Find(id);
            if (bPPB == null)
            {
                return HttpNotFound();
            }
            return View(bPPB);
        }

        // GET: PenerimaanBahanBaku/Create
        public ActionResult Posting(int id)
        {

            ViewBag.BPPBID = id;
            var bPPB = db.BPPBs.Find(id);

            if (User.Identity.Name == null)
            {
                bPPB.Diterima  = "Jasin";
            }
            else
            {
                bPPB.Diterima  = User.Identity.Name;
            }

            db.Entry(bPPB).State = EntityState.Modified;

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
