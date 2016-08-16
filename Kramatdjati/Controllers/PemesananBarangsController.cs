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
using Postal;

namespace Kramatdjati.Controllers
{
    public class PemesananBarangsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: PemesananBarangs
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            var pemesananBarangs = db.PemesananBarangs.Where(x => x.Posting == Posting).Include(p => p.contact);
            return View(pemesananBarangs.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PemesananBarangID,NoPemesananBarang,TglPesan,ContactID,TglPengiriman,User,Posting,Closed, Catatan")] PemesananBarang pemesananBarang)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = pemesananBarang.Posting });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Email(int id)
        {
            var pemesananBarang = db.PemesananBarangs.Where(x => x.PemesananBarangID == id).Include(p => p.contact).FirstOrDefault();

            dynamic email = new Email("EmailPemesananBarang");

            if (pemesananBarang.contact.Email == null)
            {
                email.to = "jasinsowandi@gmail.com";
            }
            else
            {
                email.to = pemesananBarang.contact.Email;
            }

            email.from = "jasinsowandi@yahoo.com";
            email.subject = "PO Nomor" + pemesananBarang.NoPemesananBarang.ToString().Trim();
            email.TglKirim = pemesananBarang.TglPengiriman.ToShortDateString();
            email.TglTransaksi = pemesananBarang.TglPesan.ToShortDateString();
            email.NoPO = pemesananBarang.NoPemesananBarang.ToString();
            email.Supplier = pemesananBarang.contact.Perusahaan;
            email.PemesananBarangID = id;
            email.Catatan = pemesananBarang.Catatan;

            List<PemesananBarangRincian> pemesananBarangRincians = db.PemesananBarangRincians.Where(x => x.PemesananBarangID == id).Include(p => p.bahanbaku).Include(p => p.pemesananBarang).ToList();
            decimal Total = pemesananBarangRincians.Sum(x => x.HargaSatuan * x.Jumlah);
            email.Total = Total;
            email.pemesananBarangRincians = pemesananBarangRincians.ToList();

            try
            {
                email.Send();
                ViewBag.Info = "Kirim E-Mail berhasil.";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Info = e.Message;
                return View();
            }

            // return new Rotativa.ActionAsPdf("Cetak", new { id = Id });

        }

        public ActionResult Cetak(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pemesananBarang = db.PemesananBarangs.Where(x => x.PemesananBarangID == id).Include(p => p.contact).FirstOrDefault();

            ViewBag.TglKirim = pemesananBarang.TglPengiriman.ToShortDateString();
            ViewBag.TglTransaksi = pemesananBarang.TglPesan.ToShortDateString();
            ViewBag.NoPO = pemesananBarang.NoPemesananBarang.ToString();
            ViewBag.Supplier = pemesananBarang.contact.Perusahaan;
            ViewBag.Kontak = pemesananBarang.contact.Kontak;
            ViewBag.PemesananBarangID = id;
            ViewBag.Catatan = pemesananBarang.Catatan;
            ViewBag.MataUang = pemesananBarang.mataUang.Kode;
            var pemesananBarangRincians = db.PemesananBarangRincians.Where(x => x.PemesananBarangID == id).Include(p => p.bahanbaku).Include(p => p.pemesananBarang);

            return View(pemesananBarangRincians.ToList());

        }
        // GET: PemesananBarangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            if (pemesananBarang == null)
            {
                return HttpNotFound();
            }
            return View(pemesananBarang);
        }

        // GET: PemesananBarangs/Create
        public ActionResult Create()
        {
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x=>x.Supplier==true).OrderBy(x=>x.Perusahaan), "ContactID", "Perusahaan");
            ViewBag.MataUangID = new SelectList(db.MataUangs ,"MataUangID","Kode");

            return View();
        }

        // POST: PemesananBarangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PemesananBarangID,NoPemesananBarang,TglPesan,ContactID,TglPengiriman,User,Posting,Closed, Catatan, MataUangID, TglKurs")] PemesananBarang pemesananBarang)
        {
            pemesananBarang.NoPemesananBarang = "-";
            pemesananBarang.TglKurs = DateTime.Parse("01/01/2001");
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.PemesananBarangs.Add(pemesananBarang);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Supplier == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", pemesananBarang.ContactID);
            ViewBag.MataUangID = new SelectList(db.MataUangs, "MataUangID", "Kode",pemesananBarang.MataUangID );
            return View(pemesananBarang);
        }

        // GET: PemesananBarangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            if (pemesananBarang == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Supplier == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", pemesananBarang.ContactID);
            ViewBag.MataUangID = new SelectList(db.MataUangs, "MataUangID", "Kode", pemesananBarang.MataUangID);
            return View(pemesananBarang);
        }

        // POST: PemesananBarangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PemesananBarangID,NoPemesananBarang,TglPesan,ContactID,TglPengiriman,User,Posting,Closed, Catatan, MataUangID, TglKurs")] PemesananBarang pemesananBarang)
        {

            if (ModelState.IsValid)
            {
                db.Entry(pemesananBarang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Supplier == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", pemesananBarang.ContactID);
            ViewBag.MataUangID = new SelectList(db.MataUangs, "MataUangID", "Kode", pemesananBarang.MataUangID);
            return View(pemesananBarang);
        }

        public ActionResult NoPO(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            if (pemesananBarang == null)
            {
                return HttpNotFound();
            }
             return View(pemesananBarang);
        }

        // POST: PemesananBarangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NoPO([Bind(Include = "PemesananBarangID,NoPemesananBarang,TglPesan,ContactID,TglPengiriman,User,Posting,Closed, Catatan, MataUangID, TglKurs,Kurs")] PemesananBarang pemesananBarang)
        {

            if (ModelState.IsValid)
            {
                db.Entry(pemesananBarang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = true });
            }
             return View(pemesananBarang);
        }

        // GET: PemesananBarangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            if (pemesananBarang == null)
            {
                return HttpNotFound();
            }
            return View(pemesananBarang);
        }

        // POST: PemesananBarangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PemesananBarang pemesananBarang = db.PemesananBarangs.Find(id);
            db.PemesananBarangs.Remove(pemesananBarang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {
            PemesananBarang PesananBarang = db.PemesananBarangs.Find(id);
            if (PesananBarang == null)
            {
                return HttpNotFound();
            }

            tblDefault Default = db.tblDefaults.First();

            int NoPO = Default.NoPO;
            int thn = Default.CurrentYear;
            string FormatNoPO = string.Format("{0}.{1}", thn - 2000, NoPO.ToString("D4"));
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    PesananBarang.Posting = true;
                    PesananBarang.NoPemesananBarang = FormatNoPO;
                    PesananBarang.User = User.Identity.Name;
                    db.Entry(PesananBarang).State = EntityState.Modified;
                    
                    Default.NoPO = Default.NoPO + 1;
                    db.Entry(Default).State = EntityState.Modified;
                    db.SaveChanges();

                    transaction.Commit();

                    ViewBag.Info = "Posting Berhasil";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.Info = "Posting Tidak berhasil " + e.Message.Trim();

                }
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
