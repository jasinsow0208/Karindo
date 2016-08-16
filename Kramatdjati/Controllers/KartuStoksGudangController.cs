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
    public class KartuStoksGudangController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: KartuStoksGudang
        public ActionResult Index(int id, int? GudangID, int? Period, int? thn)
        {
            if (GudangID == null)
            {
                GudangID = 0;
            }
            if (Period == null)
            {
                Period = 1;
            }
            if (thn == null)
            {
                thn = DateTime.Now.Year ;
            }

            ViewBag.GudangBahanBakuID = id;
            ViewBag.GudangID = GudangID;
            ViewBag.Period = DropDownListUtility.PeriodDropDown(1);
            ViewBag.Tahun = DropDownListUtility.YearDropDown(DateTime.Now.Year);

            ViewBag.PeriodSelect = Period;
            ViewBag.TahunSelect = thn;
            var kartuStoks = db.KartuStoks.Where(x => x.GudangBahanBakuID == id && x.TglKomputer.Month == Period && x.TglKomputer.Year == thn).OrderBy(x => x.TglKomputer).Include(k => k.bahanBaku).Include(k => k.gudangBahanBaku);
 
            List<ViewKartuStok> viewKartuStoks = new List<ViewKartuStok>();
            foreach (KartuStok rw in kartuStoks)
            {
                ViewKartuStok viewKartuStok = new ViewKartuStok();
                viewKartuStok.GudangID = rw.gudangBahanBaku.GudangID;
                viewKartuStok.KodeBarang = rw.bahanBaku.KodeBahanBaku;
                viewKartuStok.Masuk = rw.Masuk;
                viewKartuStok.Keluar = rw.Keluar;
                viewKartuStok.Keterangan = rw.Keterangan;
                viewKartuStok.TglKomputer = rw.TglKomputer;
                viewKartuStok.Source = rw.Source;
                viewKartuStok.SourceID = rw.SourceID;
                viewKartuStok.Saldo = rw.Saldo;
                viewKartuStok.BahanBakuID = id;
                viewKartuStok.HargaRata2 = rw.HargaRata2;
                viewKartuStok.HargaSatuan = rw.HargaSatuan;

                viewKartuStoks.Add(viewKartuStok);
            }

            decimal jmlStok;
            string keterangan;
            var gudangBahanBaku = db.GudangBahanBakus.Where(x => x.GudangBahanBakuID == id);

            if (kartuStoks.Count() == 0)
            {
                jmlStok = 0;
            }
            else
            {
                jmlStok = kartuStoks.FirstOrDefault().gudangBahanBaku.Jumlah;
            }

            keterangan = gudangBahanBaku.FirstOrDefault().bahanBaku.satuan.Keterangan;
            string stok = string.Format("{0:N3} {1}", jmlStok, keterangan);

            ViewBag.GudangBahanBakuID = id;
            ViewBag.KodeBarang = gudangBahanBaku.FirstOrDefault().bahanBaku.KodeBahanBaku;
            ViewBag.NamaBarang = gudangBahanBaku.FirstOrDefault().bahanBaku.Keterangan;
            ViewBag.Stok = stok;

            if (gudangBahanBaku.FirstOrDefault().bahanBaku.JenisKemasans.Count() > 0)
            {
                //periksa kalau jumlahnya hanya 1
                if (gudangBahanBaku.FirstOrDefault().bahanBaku.JenisKemasans.Count() == 1)
                {
                    decimal berat = gudangBahanBaku.FirstOrDefault().bahanBaku.JenisKemasans.FirstOrDefault().Berat;
                    string lcketerangan = gudangBahanBaku.FirstOrDefault().bahanBaku.JenisKemasans.FirstOrDefault().Keterangan;

                    decimal jmlSak = jmlStok / berat;

                    ViewBag.Zak = string.Format("/ {0:N0} {1}", jmlSak, lcketerangan);
                }
                else //kalau lebih dari 1
                {
                    var jenisKemasanDefault = gudangBahanBaku.FirstOrDefault().bahanBaku.JenisKemasans.Where(x => x.Default == true);
                    //cari yang default
                    if (jenisKemasanDefault.Count() > 0)
                    {
                        decimal berat = jenisKemasanDefault.FirstOrDefault().Berat;
                        string lcketerangan = jenisKemasanDefault.FirstOrDefault().Keterangan;

                        decimal jmlSak = jmlStok / berat;

                        ViewBag.Zak = string.Format("/ {0:N0} {1}", jmlSak, lcketerangan);
                    }
                    else //kalau engga ada cari yang pertama
                    {
                        decimal berat = gudangBahanBaku.FirstOrDefault().bahanBaku.JenisKemasans.OrderBy(x => x.JenisKemasanID).FirstOrDefault().Berat;
                        string lcketerangan = gudangBahanBaku.FirstOrDefault().bahanBaku.JenisKemasans.OrderBy(x => x.JenisKemasanID).FirstOrDefault().Keterangan;

                        decimal jmlSak = jmlStok / berat;

                        ViewBag.Zak = string.Format("/ {0:N0} {1}", jmlSak, lcketerangan);
                    }
                }
            }
            else //kalua tidak ada jenis kemasan
            {
                ViewBag.Zak = "";
            }
            ViewBag.GudangID = gudangBahanBaku.FirstOrDefault().GudangID;
            return View(viewKartuStoks);
        }

        [HttpPost]
        public ActionResult Index(string GudangBahanBakuID, string GudangID, int Period, int Tahun)
        {

            int intGudangBahanBakuID = int.Parse(GudangBahanBakuID);
            int intGudangID = int.Parse(GudangID);

            return RedirectToAction("Index", new { id = intGudangBahanBakuID, GudangID = intGudangID, Period = Period, thn = Tahun });

        }
        // GET: KartuStoksGudang/Details/5
        public ActionResult Details(int? id, int Period, int Tahun)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GudangBahanBaku gudangBahanBaku = db.GudangBahanBakus.Find(id);
            ViewBag.KodeBarang = gudangBahanBaku.bahanBaku.KodeBahanBaku;
            ViewBag.NamaBarang = gudangBahanBaku.bahanBaku.Keterangan;
            ViewBag.GudangBahanBakuID = id;
            ViewBag.Period = Period;
            ViewBag.Tahun = Tahun;

            return View();
        }

 
        public ActionResult KalkulasiKartuStok(int id, int Period, int Tahun)
        {
            var gudangBahanBaku = db.GudangBahanBakus.Find(id);
            ViewBag.GudangID = gudangBahanBaku.GudangID;
            ViewBag.GudangBahanBakuID = id;

            var Data = db.Database.ExecuteSqlCommand("spKalkulasiSaldoKartuStok @GudangBahanBakuID",
                                                        new SqlParameter("@GudangBahanBakuID", id));

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var HargaRata2= db.Database.ExecuteSqlCommand ("spKalkulasiHargaRata2 @GudangBahanBakuID,@Bulan,@Tahun,@User,@ReturnValue OUT",
                                                            new SqlParameter("@GudangBahanBakuID", id),
                                                            new SqlParameter("@Bulan", Period),
                                                            new SqlParameter("@Tahun", Tahun),
                                                            new SqlParameter("@User", "Jasin"),
                                                            ReturnParameter);
                
            ViewBag.Info = "Kalkulasi Kartu Stok Berhasil";
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
