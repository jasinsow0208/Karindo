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
    public class SuratJalansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SuratJalans
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };

            var suratJalans = db.SuratJalans.Where(x => x.Posting == Posting).Include(s => s.contact);
            return View(suratJalans.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "SuratJalanID,ContactID,NoSuratJalan,tglSuratJalan,TglTransaksi,Posting,TglPosting,User")] SuratJalan suratJalan)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = suratJalan.Posting });
            }
            return RedirectToAction("Index");
        }

        // GET: SuratJalans/Details/5
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

        public ActionResult NoSJ(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NoSJ([Bind(Include = "GudangID, Catatan, Closed, Cetak, Gudang, SuratJalanID,ContactID,NoSuratJalan,tglSuratJalan,TglTransaksi,Posting,TglPosting,User, TglTerima, Penerima")] SuratJalan suratJalan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suratJalan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Posting = suratJalan.Posting });
            }

            return View(suratJalan);
        }


        // GET: SuratJalans/Details/5
        public ActionResult UndoPostingInfo(int? id)
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

        public ActionResult UndoPostingTutup(int? id)
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

        // GET: SuratJalans/Create
        public ActionResult Create()
        {
            //Closed Surat Jalan kalau sudah diposting sama bagian marketing
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true), "ContactID", "Perusahaan");
            //ViewBag.SalesOrderID = new SelectList(db.SalesOrders.Where(x=>x.Posting==true && x.Closed == false).OrderBy(x=>x.NoSO) , "SalesOrderID", "NoSO");
            return View();
        }

        // POST: SuratJalans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuratJalanID,ContactID,NoSuratJalan,tglSuratJalan,TglTransaksi,Posting,TglPosting,User")] SuratJalan suratJalan)
        {
            if (ModelState.IsValid)
            {

                //Total Saldo awal piutang per Customer.
                //Perhatian: Proses ini masih belum memeriksa piutang berdasarkan tgl jatuh tempo. 
                //Harusnya, kalau belum jatuh tempo belum termasuk piutang
                decimal TotalSAPiutang = 0;
                try
                {
                    TotalSAPiutang = db.SAPiutangs.Where(x => x.ContactID == suratJalan.ContactID).Sum(x => x.Jumlah);
                }
                catch (Exception)
                {
                    TotalSAPiutang = 0;
                }

                //Total jumlah Faktur Jual per Customer
                var fakturJual = db.FakturJuals.Where(x => x.ContactID == suratJalan.ContactID).ToList();
                decimal TotalFaktur = 0;

                if (fakturJual.Count() > 0)
                {
                    TotalFaktur = fakturJual.Sum(x => x.Total);
                }

                //Total jumlah pembayaran per Customer
                var pembayaranSOes = db.PembayaranSOes.Where(x => x.ContactID == suratJalan.ContactID && x.PostingBayar == true);
                decimal jmlBayar = 0;
                if (pembayaranSOes.Count() != 0)
                {
                    jmlBayar = pembayaranSOes.Sum(x => x.Jumlah);
                }
                else
                {
                    jmlBayar = 0;
                }

                //Periksa status piutang per customer
                Contact customer = db.Contacts.Where(x => x.ContactID == suratJalan.ContactID).FirstOrDefault();

                if (customer.StatusKredit == "COD")
                {
                    if (jmlBayar < TotalFaktur + TotalSAPiutang)
                    {
                        ViewBag.Info = "Customer COD. Ada faktur penjualan yang belum ditagihkan.";
                        return View("Informasi");
                    }
                    else
                    {
                        suratJalan.TglTransaksi = DateTime.Now;
                        suratJalan.TglTerima = DateTime.Parse("2001-01-01");
                        suratJalan.TglPosting = DateTime.Parse("2001-01-01");
                        db.SuratJalans.Add(suratJalan);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                if (customer.StatusKredit == "CBD")
                {
                    if (jmlBayar <= TotalFaktur + TotalSAPiutang)
                    {
                        ViewBag.Info = "Customer CBD. harus dilakukan pembayaran tunai terlebih dahulu.";
                        return View("Informasi");
                    }
                    else
                    {
                        suratJalan.TglTransaksi = DateTime.Now;
                        suratJalan.TglTerima = DateTime.Parse("2001-01-01");
                        suratJalan.TglPosting = DateTime.Parse("2001-01-01");
                        db.SuratJalans.Add(suratJalan);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                if (customer.StatusKredit == "Kredit")
                {

                    if (customer.KreditLimit < TotalSAPiutang + TotalFaktur)
                    {
                        ViewBag.Info = "Telah melebihi batas kredit";
                        return View("Informasi");
                    }

                    suratJalan.TglTransaksi = DateTime.Now;
                    suratJalan.TglTerima = DateTime.Parse("2001-01-01");
                    suratJalan.TglPosting = DateTime.Parse("2001-01-01");
                    db.SuratJalans.Add(suratJalan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true), "ContactID", "Perusahaan", suratJalan.ContactID);
            //ViewBag.SalesOrderID = new SelectList(db.SalesOrders, "SalesOrderID", "NoSO", suratJalan.SalesOrderID);
            return View(suratJalan);
        }

        // GET: SuratJalans/Edit/5
        public ActionResult Edit(int? id)
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

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true), "ContactID", "Perusahaan", suratJalan.ContactID);
            //ViewBag.SalesOrderID = new SelectList(db.SalesOrders, "SalesOrderID", "NoSO", suratJalan.SalesOrderID);
            return View(suratJalan);
        }

        // POST: SuratJalans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuratJalanID,ContactID,NoSuratJalan,tglSuratJalan,TglTransaksi,Posting,TglPosting,User, TglTerima, Penerima")] SuratJalan suratJalan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suratJalan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true), "ContactID", "Perusahaan", suratJalan.ContactID);
            //ViewBag.SalesOrderID = new SelectList(db.SalesOrders, "SalesOrderID", "NoSO", suratJalan.SalesOrderID);
            return View(suratJalan);
        }

        // GET: SuratJalans/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: SuratJalans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuratJalan suratJalan = db.SuratJalans.Find(id);
            db.SuratJalans.Remove(suratJalan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {
            SuratJalan suratJalan = db.SuratJalans.Find(id);
            if (suratJalan == null)
            {
                return HttpNotFound();
            }

            tblDefault Default = db.tblDefaults.First();

            int NoSuratJalan;
            int thn = Default.CurrentYear;
            if (suratJalan.contact.PPN == true)
            {
                NoSuratJalan = Default.NoSuratJalanPPN;
            }
            else
            {
                NoSuratJalan = Default.NoSuratJalan;
            }
            //string FormatNoSuratJalan = string.Format("{0}.{1}", thn - 2000, NoSuratJalan.ToString("D4"));
            string FormatNoSuratJalan = string.Format("{0}", NoSuratJalan.ToString("D4"));
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    if (suratJalan.NoSuratJalan == null)
                    {
                        suratJalan.NoSuratJalan = FormatNoSuratJalan;

                        if (suratJalan.contact.PPN == true)
                        {
                            Default.NoSuratJalanPPN = Default.NoSuratJalanPPN + 1;
                        }
                        else
                        {
                            Default.NoSuratJalan = Default.NoSuratJalan + 1;
                        }

                        db.Entry(Default).State = EntityState.Modified;
                    }

                    suratJalan.Posting = true;
                    suratJalan.User = User.Identity.Name;
                    db.Entry(suratJalan).State = EntityState.Modified;

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

        public ActionResult UndoPosting(int id)
        {
            SuratJalan suratJalan = db.SuratJalans.Find(id);
            if (suratJalan == null)
            {
                return HttpNotFound();
            }

            if (suratJalan.Closed == true)
            {
                ViewBag.Info = "Surat Jalan sudah di tutup, tidak dapat undo posting";
                return View("Informasi");
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    suratJalan.Posting = false;

                    suratJalan.User = User.Identity.Name;
                    db.Entry(suratJalan).State = EntityState.Modified;

                    db.SaveChanges();

                    transaction.Commit();

                    ViewBag.Info = "Undo Posting Berhasil";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.Info = "Undo Posting Tidak berhasil " + e.Message.Trim();

                }
            }
            return View("Informasi");
        }

        public ActionResult UndoPostingTutupProses(int id)
        {
            ViewBag.SuratJalanID = id;

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spSuratJalanUndoPosting @SuratJalanID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@SuratJalanID", id),
                                                        new SqlParameter("@User", User.Identity.Name),
                                                        ReturnParameter);

            if (ReturnParameter.Value.ToString() == "0")
            {
                ViewBag.Info = "Undo Posting Berhasil";
            }
            else
            {
                ViewBag.Info = string.Format("Undo Posting Gagal. error: {0}", ReturnParameter.Value);
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
