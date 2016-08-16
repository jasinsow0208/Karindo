using Kramatdjati.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kramatdjati.Models;

namespace Kramatdjati.Controllers
{
    public class DDLController : Controller
    {
        // GET: DDL
        private AppIdentityDbContext db = new AppIdentityDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DepartemenList(int Id)
        {
            var district = from s in db.Departemen
                           where s.DivisiId == Id
                           select s;


            return Json(new SelectList(district.ToArray(), "DepartemenId", "Keterangan"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult NamaBarang(int Id)
        {
            var district = from s in db.BahanBakus
                           where s.BahanBakuID == Id
                           select new { Barang = s.Keterangan, satuan = s.satuan.Keterangan };


            return Json(new SelectList(district.ToArray(), "Satuan", "Barang"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GudangNamaBarang(int Id)
        {
            var district = from s in db.GudangBahanBakus
                           where s.GudangBahanBakuID == Id
                           select new { Barang = s.bahanBaku.Keterangan , satuan = s.bahanBaku.satuan.Keterangan };


            return Json(new SelectList(district.ToArray(), "Satuan", "Barang"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult PemesananNamaBarang(int Id)
        {

            int intBahanBaku = db.PemesananBarangRincians.Find(Id).BahanBakuID;

            var district = from s in db.BahanBakus
                           where s.BahanBakuID == intBahanBaku
                           select new { Barang = s.Keterangan, satuan = s.satuan.Keterangan };

            return Json(new SelectList(district.ToArray(), "Satuan", "Barang"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult PemesananNamaSupplier(int Id)
        {
            var district = from s in db.PemesananBarangs
                           where s.PemesananBarangID == Id
                           select new { idSupplier = s.ContactID, Perusahaan = s.contact.Perusahaan };


            return Json(new SelectList(district.ToArray(), "idSupplier", "Perusahaan"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Size(string id)
        {
            var Size = from s in db.BahanBakus
                       where s.KodeBarangJadi == id
                       select new { KodeBahanBaku = s.KodeBahanBaku, Size = s.Size };

            return Json(new SelectList(Size.ToArray(), "Size", "Size"), JsonRequestBehavior.AllowGet);

        }

        public JsonResult SalesOrderNamaBarang(int Id)
        {

            int intBahanBaku = db.SalesOrderRincians.Find(Id).BahanBakuID;

            var district = from s in db.BahanBakus
                           where s.BahanBakuID == intBahanBaku
                           select new { Barang = s.Keterangan, satuan = s.satuan.Keterangan };

            return Json(new SelectList(district.ToArray(), "Satuan", "Barang"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SuratJalanCetak(int id)
        {
            var suratJalan = db.SuratJalanCetaks.
                                Where(x=>x.suratJalan.ContactID==id && x.StatusFaktur==false).
                                Select(x => new { Value = x.SuratJalanCetakID, Text = x.suratJalan.NoSuratJalan }).
                                OrderBy(x => x.Text);

            return Json(new SelectList(suratJalan.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult JPDeptARincian(string id)
        {
            DateTime Tgl;
            if (DateTime.TryParse(id, out Tgl))
            {
                var JPDeptARincian = db.JPDeptARincians.Where(x => x.jpDeptA.TglProduksi == Tgl && x.jpDeptA.Posting == true)
                                                  .Select(x => new { Value = x.JPDeptARincianID, Text = x.KodeBarangJadi })
                                                  .OrderBy(x => x.Text);

                return Json(new SelectList(JPDeptARincian.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            else 
            {
                string[] weekDays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
                return Json(weekDays , JsonRequestBehavior.AllowGet);
                };

            
        }
    }
}