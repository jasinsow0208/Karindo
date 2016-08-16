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
    public class FakturJualsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: FakturJuals
        public ActionResult Index()
        {
            var fakturJuals = db.FakturJuals.Include(f => f.customer).Include(f => f.suratJalanCetak);
            return View(fakturJuals.ToList());
        }

        // GET: FakturJuals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturJual fakturJual = db.FakturJuals.Find(id);
            if (fakturJual == null)
            {
                return HttpNotFound();
            }
            return View(fakturJual);
        }

        public ActionResult UndoDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturJual fakturJual = db.FakturJuals.Find(id);
            if (fakturJual == null)
            {
                return HttpNotFound();
            }
            return View(fakturJual);
        }

        // GET: FakturJuals/Create
        public ActionResult Create()
        {
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x=>x.Customer==true).OrderBy(x=>x.Perusahaan) , "ContactID", "Perusahaan");
            var suratJalan = db.SuratJalanCetaks.
                                Where(x => x.StatusFaktur == false).
                                Select(x => new { Value = x.SuratJalanCetakID, Text = x.suratJalan.NoSuratJalan }).
                                OrderBy(x => x.Text);

            ViewBag.SuratJalanCetakID = new SelectList(suratJalan, "Value", "Text");
            return View();
        }

        // POST: FakturJuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FakturJualID,ContactID,TglFaktur,NoFaktur,NomorSeri,TglJatuhTempo,PPN,SuratJalanCetakID, TglPesan, Diskon")] FakturJual fakturJual)
        {
            if (ModelState.IsValid)
            {
                Contact customer = db.Contacts.Find(fakturJual.ContactID);
                fakturJual.PPN = customer.PPN;
                if (customer.PPN == true)
                {
                    fakturJual.Nama = customer.NamaFaktur1;
                    fakturJual.Alamat = customer.AlamatFaktur1;
                    fakturJual.Kota = customer.KotaFaktur1;
                    fakturJual.NPWP = customer.NPWPFaktur1;
                }
                else
                {
                    fakturJual.Nama = customer.Perusahaan;
                    fakturJual.Alamat = customer.Alamat;
                    fakturJual.Kota = customer.Kota;
                    fakturJual.NPWP = "";
                }

                db.FakturJuals.Add(fakturJual);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", fakturJual.ContactID);
            var suratJalan = db.SuratJalanCetaks.
                                Where(x => x.StatusFaktur == false && x.suratJalan.ContactID== fakturJual.ContactID).
                                Select(x => new { Value = x.SuratJalanCetakID, Text = x.suratJalan.NoSuratJalan }).
                                OrderBy(x => x.Text);

            ViewBag.SuratJalanCetakID = new SelectList(suratJalan, "Value", "Text", fakturJual.SuratJalanCetakID);
            return View(fakturJual);
        }

        // GET: FakturJuals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturJual fakturJual = db.FakturJuals.Find(id);
            if (fakturJual == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", fakturJual.ContactID);
            var suratJalan = db.SuratJalanCetaks.
                                Where(x => x.StatusFaktur == false && x.suratJalan.ContactID== fakturJual.ContactID).
                                Select(x => new { Value = x.SuratJalanCetakID, Text = x.suratJalan.NoSuratJalan }).
                                OrderBy(x => x.Text);

            ViewBag.SuratJalanCetakID = new SelectList(suratJalan, "Value", "Text", fakturJual.SuratJalanCetakID);
            return View(fakturJual);
        }

        // POST: FakturJuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FakturJualID,ContactID,TglFaktur,NoFaktur,NomorSeri,TglJatuhTempo,PPN,SuratJalanCetakID, TglPesan, Nama, Alamat, Kota, NPWP, Diskon")] FakturJual fakturJual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakturJual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", fakturJual.ContactID);
            var suratJalan = db.SuratJalanCetaks.
                                 Where(x => x.StatusFaktur == false && x.suratJalan.ContactID == fakturJual.ContactID).
                                 Select(x => new { Value = x.SuratJalanCetakID, Text = x.suratJalan.NoSuratJalan }).
                                 OrderBy(x => x.Text);

            ViewBag.SuratJalanCetakID = new SelectList(suratJalan, "Value", "Text", fakturJual.SuratJalanCetakID);
            return View(fakturJual);
        }

        // GET: FakturJuals/Edit/5
        public ActionResult EditDiskon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturJual fakturJual = db.FakturJuals.Find(id);
            if (fakturJual == null)
            {
                return HttpNotFound();
            }

            ViewBag.FakturJualID = id;

            return View(fakturJual);
        }

        // POST: FakturJuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDiskon([Bind(Include = "FakturJualID,ContactID,TglFaktur,NoFaktur,NomorSeri,TglJatuhTempo,PPN,SuratJalanCetakID, TglPesan, Nama, Alamat, Kota, NPWP, Diskon, Posting")] FakturJual fakturJual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakturJual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "FakturJualRincians", new { id = fakturJual.FakturJualID });
            }

            ViewBag.FakturJualID = fakturJual.FakturJualID;
             return View(fakturJual);
        }

        // GET: FakturJuals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturJual fakturJual = db.FakturJuals.Find(id);
            if (fakturJual == null)
            {
                return HttpNotFound();
            }
            return View(fakturJual);
        }

        // POST: FakturJuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FakturJual fakturJual = db.FakturJuals.Find(id);
            db.FakturJuals.Remove(fakturJual);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {
            FakturJual fakturJual = db.FakturJuals.Find(id);
            if (fakturJual == null)
            {
                return HttpNotFound();
           }

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spFakturJualPosting @FakturJualID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@FakturJualID", id),
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

        public ActionResult UndoPosting(int id)
        {
            FakturJual fakturJual = db.FakturJuals.Find(id);
            if (fakturJual == null)
            {
                return HttpNotFound();
           }

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spFakturJualUndoPosting @FakturJualID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@FakturJualID", id),
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
