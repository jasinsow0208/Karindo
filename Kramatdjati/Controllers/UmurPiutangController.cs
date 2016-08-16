using Kramatdjati.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kramatdjati.Models;

namespace Kramatdjati.Controllers
{
    public class UmurPiutangController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: UmurPiutang
        public ActionResult Index()
        {
            var fakturJual = db.FakturJuals.ToList();
            var fakturJualBelumLunas = fakturJual.Where(x => x.StatusLunas == false).OrderBy(x => x.ContactID);

            int contactID = 0;
            decimal current = 0;
            decimal piutang30 = 0;
            decimal piutang60 = 0;
            decimal piutang90 = 0;
            decimal piutangAlert = 0;
            string namaPerusahaan = "";

            List<UmurPiutang> umurPiutangs = new List<UmurPiutang>();
            UmurPiutang umurPiutang;
            foreach (FakturJual rw in fakturJualBelumLunas)
            {
                if (contactID != rw.ContactID) // Jika customer berbeda
                {
                    if (contactID != 0) //Jika customer ada
                    {
                        umurPiutang = new UmurPiutang()
                        {
                            ContactID = contactID,
                            Current = current,
                            Piutang30 = piutang30,
                            Piutang60 = piutang60,
                            Piutang90 = piutang90,
                            PiutangAlert = piutangAlert,
                            NamaPerusahaan = namaPerusahaan
                        };
                        umurPiutangs.Add(umurPiutang);
                    }

                    contactID = rw.ContactID;
                    namaPerusahaan = rw.customer.Perusahaan;
                    current = 0;
                    piutang30 = 0;
                    piutang60 = 0;
                    piutang90 = 0;
                    piutangAlert = 0;

                }
                
                if (DateTime.Now < rw.TglJatuhTempo)
                {
                    current = current + rw.Total - rw.Pembayaran;
                }
                else
                {
                    int JmlHari =(int)(rw.TglJatuhTempo - DateTime.Now).TotalDays;
                    if (JmlHari > 0 && JmlHari <= 30) { piutang30 = piutang30 + rw.Total - rw.Pembayaran; }
                    if (JmlHari > 30 && JmlHari <= 60) { piutang60 = piutang30 + rw.Total - rw.Pembayaran; }
                    if (JmlHari > 60 && JmlHari <= 90) { piutang90 = piutang30 + rw.Total - rw.Pembayaran; }
                    if (JmlHari > 90 ) { piutangAlert = piutangAlert + rw.Total - rw.Pembayaran; }
                }
            }

            umurPiutang = new UmurPiutang()
            {
                ContactID = contactID,
                Current = current,
                Piutang30 = piutang30,
                Piutang60 = piutang60,
                Piutang90 = piutang90,
                PiutangAlert = piutangAlert,
                NamaPerusahaan = namaPerusahaan
            };
            umurPiutangs.Add(umurPiutang );
            return View(umurPiutangs.ToList());
        }

        public ActionResult Details( int id, string time)
        {
            var fakturJual = db.FakturJuals.Where(x => x.ContactID == id).ToList();
            
            if (time=="0")
            {
               var fakturJualList = fakturJual.Where(x => x.StatusLunas == false && x.TglJatuhTempo > DateTime.Now);
                return View(fakturJualList);
            }
            if (time == "1")
            {
                var fakturJualList = fakturJual.Where(x => x.StatusLunas == false && ( DateTime.Now - x.TglJatuhTempo).TotalDays >0 && (DateTime.Now - x.TglJatuhTempo).TotalDays <=30);
                return View(fakturJualList);
            }
            if (time == "2")
            {
                var fakturJualList = fakturJual.Where(x => x.StatusLunas == false && (DateTime.Now - x.TglJatuhTempo).TotalDays > 30 && (DateTime.Now - x.TglJatuhTempo).TotalDays <= 60);
                return View(fakturJualList);
            }
            if (time == "3")
            {
                var fakturJualList = fakturJual.Where(x => x.StatusLunas == false && (DateTime.Now - x.TglJatuhTempo).TotalDays > 60 && (DateTime.Now - x.TglJatuhTempo).TotalDays <= 90);
                return View(fakturJualList);
            }
            if (time == "4")
            {
                var fakturJualList = fakturJual.Where(x => x.StatusLunas == false && (DateTime.Now - x.TglJatuhTempo).TotalDays > 90);
                return View(fakturJualList);
            }

            return View();

        }
    }
}