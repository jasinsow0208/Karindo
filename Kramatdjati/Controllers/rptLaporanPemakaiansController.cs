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
    public class rptLaporanPemakaiansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: rptLaporanPemakaians
        public ActionResult Index(int? Bulan, int? Tahun, int? jenisPersediaanID)
        {
            int bln, thn, jnsPersediaanID;

            if (Bulan == null)
            {
                bln = 1;
            }
            else
            {
                bln = (int)Bulan;
            };
            if (Tahun == null)
            {
                thn = DateTime.Now.Year;
            }
            else
            {
                thn = (int)Tahun;
            };
            if (jenisPersediaanID == null)
            {
                jnsPersediaanID = 1;
            }
            else
            {
                jnsPersediaanID =(int) jenisPersediaanID;
            };
            ViewBag.Period = DropDownListUtility.PeriodDropDown(bln);
            ViewBag.Tahun = DropDownListUtility.YearDropDown(thn);
            ViewBag.JenisPersediaanID = new SelectList(db.JenisPersediaans, "JenisPersediaanID", "Keterangan", jnsPersediaanID );
            ViewBag.PeriodSelect = bln;
            ViewBag.TahunSelect = thn;

            DateTime tglAwal = new DateTime(thn, bln, 1);
            DateTime tglAkhir = new DateTime(thn, bln, 1).AddMonths(1);

            tblDefault defa = db.tblDefaults.OrderBy(x => x.tblDefaultId).First();
            int intGudangBahanBakuID = defa.GudangBeliID;
            int intGudangProduksiID = defa.GudangProduksiID;

            List<rptLaporanPemakaian> rptLaps = new List<rptLaporanPemakaian>();

            var bahanbakus = db.BahanBakus.Where(x => x.JenisPersediaanID == jenisPersediaanID  ).OrderBy(x => x.KodeBahanBaku);
            foreach (BahanBaku rw in bahanbakus)
            {
                rptLaporanPemakaian rptLap = new rptLaporanPemakaian();
                rptLap.BahanBakuID = rw.BahanBakuID;
                rptLap.KodeBarang = rw.KodeBahanBaku;
                int intBahanBakuID = rw.BahanBakuID;

                decimal jmlAwal;
                decimal hargaAwal;
                CariJmlAwal(tglAwal, tglAkhir, intBahanBakuID, intGudangBahanBakuID, out jmlAwal, out hargaAwal);

                decimal jmlAwalProduksi;
                decimal hargaAwalProduksi;
                CariJmlAwal(tglAwal, tglAkhir, intBahanBakuID, intGudangProduksiID, out jmlAwalProduksi, out hargaAwalProduksi);

                rptLap.JumlahAwal = jmlAwal + jmlAwalProduksi;
                rptLap.HargaAwal = hargaAwal + hargaAwalProduksi;

                decimal jmlBeli;
                decimal hargaBeli;
                Pembelian(tglAwal, tglAkhir, intBahanBakuID, intGudangBahanBakuID, out jmlBeli, out hargaBeli);
                rptLap.HargaBeli = hargaBeli;
                rptLap.JumlahBeli = jmlBeli;

                decimal jmlPakai;
                decimal hargaPakai;
                Pemakaian(tglAwal, tglAkhir, intBahanBakuID, intGudangProduksiID, out jmlPakai, out hargaPakai);
                rptLap.HargaPakai = hargaPakai;
                rptLap.JumlahPakai = jmlPakai;

                rptLap.JumlahAkhir = jmlAwal + jmlAwalProduksi + jmlBeli - jmlPakai;
                rptLap.HargaAkhir = hargaAwal + hargaAwalProduksi + hargaBeli - hargaPakai;

                rptLaps.Add(rptLap);
            }

            return View(rptLaps.ToList());
        }

        [HttpPost]
        public ActionResult Index(int Period, int Tahun, int JenisPersediaanID)
        {

           
            return RedirectToAction("Index", new {  Bulan = Period, TAhun = Tahun , jenisPersediaanID= JenisPersediaanID});

        }

        public ActionResult SaldoAwal(int id, int Period, int Tahun)
        {
            ViewBag.Period = Period;
            ViewBag.Tahun = Tahun;

            var bahanBaku = db.BahanBakus.Find(id);
            ViewBag.JenisPersediaanID = bahanBaku.JenisPersediaanID;
            ViewBag.JenisPersediaan = bahanBaku.JenisPersediaan.Keterangan;
            ViewBag.KodeBarang = bahanBaku.KodeBahanBaku;

            DateTime tglAwal = new DateTime(Tahun , Period, 1);
            DateTime tglAkhir = new DateTime(Tahun, Period, 1).AddMonths(1);

            tblDefault defa = db.tblDefaults.OrderBy(x => x.tblDefaultId).First();
            int intGudangBahanBakuID = defa.GudangBeliID;
            int intGudangProduksiID = defa.GudangProduksiID;

            List<rptSaldoAwal> rptSaldoAwals = new List<rptSaldoAwal>();
            rptSaldoAwal rptSaldoGudangBahanBaku = new rptSaldoAwal();
            rptSaldoAwal rptSaldoGudangProduksi = new rptSaldoAwal();

            decimal jmlAwal;
            decimal hargaAwal;
            var gdgBahanBaku= db.Gudangs.Find(intGudangBahanBakuID );

            CariJmlAwal(tglAwal, tglAkhir, id, intGudangBahanBakuID, out jmlAwal, out hargaAwal);
            rptSaldoGudangBahanBaku.GudangID = intGudangBahanBakuID;
            rptSaldoGudangBahanBaku.Lokasi  = gdgBahanBaku.Lokasi;
            rptSaldoGudangBahanBaku.Jumlah = jmlAwal;
            rptSaldoGudangBahanBaku.HargaRata2 = hargaAwal;
            rptSaldoGudangBahanBaku.Total = hargaAwal * jmlAwal; 
            rptSaldoAwals.Add(rptSaldoGudangBahanBaku);
            
            decimal jmlAwalProduksi;
            decimal hargaAwalProduksi;
            var gdgProduksi = db.Gudangs.Find(intGudangProduksiID);
            CariJmlAwal(tglAwal, tglAkhir, id, intGudangProduksiID, out jmlAwalProduksi, out hargaAwalProduksi);
            rptSaldoGudangProduksi.GudangID = intGudangBahanBakuID;
            rptSaldoGudangProduksi.Lokasi = gdgProduksi.Lokasi;
            rptSaldoGudangProduksi.Jumlah = jmlAwalProduksi;
            rptSaldoGudangProduksi.HargaRata2 = hargaAwalProduksi;
            rptSaldoGudangProduksi.Total = jmlAwalProduksi * hargaAwalProduksi; 
            rptSaldoAwals.Add(rptSaldoGudangProduksi);

            return View(rptSaldoAwals.ToList());
        }

        public ActionResult PembelianDetail(int id, int Period, int Tahun)
        {
            ViewBag.Period = Period;
            ViewBag.Tahun = Tahun;

            var bahanBaku = db.BahanBakus.Find(id);
            ViewBag.JenisPersediaanID = bahanBaku.JenisPersediaanID;
            ViewBag.JenisPersediaan = bahanBaku.JenisPersediaan.Keterangan;
            ViewBag.KodeBarang = bahanBaku.KodeBahanBaku;

            DateTime tglAwal = new DateTime(Tahun, Period, 1);
            DateTime tglAkhir = new DateTime(Tahun, Period, 1).AddMonths(1);

            tblDefault defa = db.tblDefaults.OrderBy(x => x.tblDefaultId).First();
            int intGudangBahanBakuID = defa.GudangBeliID;
            int intGudangProduksiID = defa.GudangProduksiID;
   
            var kartuStoks = db.KartuStoks.Where(x => x.TglKomputer >= tglAwal && x.TglKomputer < tglAkhir && x.gudangBahanBaku.GudangID == intGudangBahanBakuID  && x.gudangBahanBaku.BahanBakuID ==id && x.Source == "PenerimaanBarang").OrderBy (x=>x.TglKomputer );

            return View(kartuStoks.ToList());
 
        }
        public ActionResult PemakaianDetail(int id, int Period, int Tahun)
        {
            ViewBag.Period = Period;
            ViewBag.Tahun = Tahun;

            var bahanBaku = db.BahanBakus.Find(id);
            ViewBag.JenisPersediaanID = bahanBaku.JenisPersediaanID;
            ViewBag.JenisPersediaan = bahanBaku.JenisPersediaan.Keterangan;
            ViewBag.KodeBarang = bahanBaku.KodeBahanBaku;

            DateTime tglAwal = new DateTime(Tahun, Period, 1);
            DateTime tglAkhir = new DateTime(Tahun, Period, 1).AddMonths(1);

            tblDefault defa = db.tblDefaults.OrderBy(x => x.tblDefaultId).First();
            int intGudangBahanBakuID = defa.GudangBeliID;
            int intGudangProduksiID = defa.GudangProduksiID;

            var kartuStoks = db.KartuStoks.Where(x => x.TglKomputer >= tglAwal && x.TglKomputer < tglAkhir && x.gudangBahanBaku.GudangID == intGudangProduksiID  && x.gudangBahanBaku.BahanBakuID == id && x.Source == "JPDeptARincian").OrderBy(x=>x.TglKomputer);

            return View(kartuStoks.ToList());

        }

        private void CariJmlAwal(DateTime tglAwal, DateTime tglAkhir, int bahanBakuID, int gudangID, out decimal jml, out decimal harga)
        {
            jml = 0;
            harga = 0;

            try
            {
                var kartuStok = db.KartuStoks.Where(x => x.TglKomputer >= tglAwal && x.TglKomputer < tglAkhir && x.gudangBahanBaku.GudangID == gudangID && x.gudangBahanBaku.BahanBakuID == bahanBakuID).OrderBy(x => x.TglKomputer).First();

                if (kartuStok != null)
                {
                    if (kartuStok.Source == "StokOpname")
                    {
                        jml = kartuStok.Saldo;
                        harga = jml * kartuStok.HargaRata2;
                    };
                    if (kartuStok.Source == "Penerimaan")
                    {
                        jml = kartuStok.Saldo - kartuStok.Masuk;
                        harga = jml * kartuStok.HargaRata2;
                    };
                    if (kartuStok.Source == "BPPBRincian")
                    {
                        jml = kartuStok.Saldo + kartuStok.Keluar - kartuStok.Masuk;
                        harga = jml * kartuStok.HargaRata2;
                    };
                    if (kartuStok.Source == "JPDeptARincian")
                    {
                        jml = kartuStok.Saldo + kartuStok.Keluar;
                        harga = jml * kartuStok.HargaRata2;
                    };
                }
                else
                {
                    var kartuStok2 = db.KartuStoks.Where(x => x.TglKomputer < tglAwal && x.gudangBahanBaku.GudangID == gudangID && x.gudangBahanBaku.BahanBakuID == bahanBakuID).OrderByDescending(x => x.TglKomputer).First();
                    if (kartuStok2 != null)
                    {
                        if (kartuStok2.Source == "StokOpname")
                        {
                            jml = kartuStok2.Saldo;
                            harga = jml * kartuStok2.HargaRata2;
                        };
                        if (kartuStok2.Source == "Penerimaan")
                        {
                            jml = kartuStok2.Saldo - kartuStok2.Masuk;
                            harga = jml * kartuStok2.HargaRata2;
                        };
                        if (kartuStok2.Source == "BPPBRincian")
                        {
                            jml = kartuStok2.Saldo + kartuStok2.Keluar - kartuStok2.Masuk;
                            harga = jml * kartuStok2.HargaRata2;
                        };
                        if (kartuStok2.Source == "JPDeptARincian")
                        {
                            jml = kartuStok2.Saldo + kartuStok2.Keluar;
                            harga = jml * kartuStok2.HargaRata2;
                        };
                    }
                    else
                    {
                        jml = 0;
                        harga = 0;
                    }
                }
            }
            catch (Exception)
            {

                jml = 0;
                harga = 0;
            }

        }

        private void Pembelian(DateTime tglAwal, DateTime tglAkhir, int bahanBakuID, int gudangID, out decimal jml, out decimal harga)
        {
            jml = 0;
            harga = 0;
            var kartuStok = db.KartuStoks.Where(x => x.TglKomputer >= tglAwal && x.TglKomputer < tglAkhir && x.gudangBahanBaku.GudangID == gudangID && x.gudangBahanBaku.BahanBakuID == bahanBakuID && x.Source == "PenerimaanBarang").ToList();
            if (kartuStok != null)
            {
                jml = kartuStok.Sum(x => x.Masuk);
                harga = kartuStok.Sum(x => x.Masuk * x.HargaSatuan);
            }
            else
            {
                jml = 0;
                harga = 0;
            };
        }
        private void Pemakaian(DateTime tglAwal, DateTime tglAkhir, int bahanBakuID, int gudangID, out decimal jml, out decimal harga)
        {
            jml = 0;
            harga = 0;
            try
            {
                var kartuStok = db.KartuStoks.Where(x => x.TglKomputer >= tglAwal && x.TglKomputer < tglAkhir && x.gudangBahanBaku.GudangID == gudangID && x.gudangBahanBaku.BahanBakuID == bahanBakuID && x.Source == "JPDeptARincian").ToList();
                if (kartuStok != null)
                {
                    jml = kartuStok.Sum(x => x.Keluar);
                    harga = kartuStok.Sum(x => x.Keluar * x.HargaSatuan);
                }
                else
                {
                    jml = 0;
                    harga = 0;
                };
            }
            catch (Exception)
            {
                
                jml = 0;
            harga = 0;
            }
        }

        // GET: rptLaporanPemakaians/Details/5


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
