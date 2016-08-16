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
    public class HargaJualPerCustomersController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: HargaJualPerCustomers
        public ActionResult Index(int id)
        {
            var Customer = db.Contacts.Find(id);
            ViewBag.Nama = Customer.Perusahaan ;
            ViewBag.id = id;
            return View(db.HargaJualPerCustomers.Where(x=>x.ContactID ==id).ToList());
        }

           // GET: HargaJualPerCustomers/Create
        public ActionResult Create(int id)
        {
            var Customer = db.Contacts.Find(id);
            ViewBag.Nama = Customer.Perusahaan;
            ViewBag.id = id;

            var BarangJadi = db.BahanBakus.Where(x => x.BarangJadi == true).Select(x => new { x.KodeBarangJadi }).Distinct().OrderBy(x => x.KodeBarangJadi);
            ViewBag.JenisSpon = new SelectList(BarangJadi.ToList(), "KodeBarangJadi", "KodeBarangJadi");

            return View();
        }

        // POST: HargaJualPerCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HargaJualPerCustomerID,ContactID,TglBerlaku,JenisSpon,Harga,mm")] HargaJualPerCustomer hargaJualPerCustomer)
        {
            if (ModelState.IsValid)
            {
                db.HargaJualPerCustomers.Add(hargaJualPerCustomer);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = hargaJualPerCustomer.ContactID });
            }

            var Customer = db.Contacts.Find(hargaJualPerCustomer.ContactID );
            ViewBag.Nama = Customer.Perusahaan;
            ViewBag.id = hargaJualPerCustomer.ContactID ;

            var BarangJadi = db.BahanBakus.Where(x => x.BarangJadi == true).Select(x => new { x.KodeBarangJadi }).Distinct().OrderBy(x => x.KodeBarangJadi);
            ViewBag.JenisSpon = new SelectList(BarangJadi.ToList(), "KodeBarangJadi", "KodeBarangJadi", hargaJualPerCustomer.JenisSpon );

            return View(hargaJualPerCustomer);
        }

        // GET: HargaJualPerCustomers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HargaJualPerCustomer hargaJualPerCustomer = db.HargaJualPerCustomers.Find(id);
            if (hargaJualPerCustomer == null)
            {
                return HttpNotFound();
            }

            var Customer = db.Contacts.Find(hargaJualPerCustomer.ContactID);
            ViewBag.Nama = Customer.Perusahaan;
            ViewBag.id = hargaJualPerCustomer.ContactID; 
   
            var BarangJadi = db.BahanBakus.Where(x => x.BarangJadi == true).Select(x => new { x.KodeBarangJadi }).Distinct().OrderBy(x => x.KodeBarangJadi);
            ViewBag.JenisSpon = new SelectList(BarangJadi.ToList(), "KodeBarangJadi", "KodeBarangJadi", hargaJualPerCustomer.JenisSpon);

            return View(hargaJualPerCustomer);
        }

        // POST: HargaJualPerCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HargaJualPerCustomerID,ContactID,TglBerlaku,JenisSpon,Harga,mm")] HargaJualPerCustomer hargaJualPerCustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hargaJualPerCustomer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = hargaJualPerCustomer.ContactID });
            }

            var Customer = db.Contacts.Find(hargaJualPerCustomer.ContactID);
            ViewBag.Nama = Customer.Perusahaan;

            var BarangJadi = db.BahanBakus.Where(x => x.BarangJadi == true).Select(x => new { x.KodeBarangJadi }).Distinct().OrderBy(x => x.KodeBarangJadi);
            ViewBag.JenisSpon = new SelectList(BarangJadi.ToList(), "KodeBarangJadi", "KodeBarangJadi", hargaJualPerCustomer.JenisSpon);

            return View(hargaJualPerCustomer);
        }

        // GET: HargaJualPerCustomers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HargaJualPerCustomer hargaJualPerCustomer = db.HargaJualPerCustomers.Find(id);
            if (hargaJualPerCustomer == null)
            {
                return HttpNotFound();
            }
            var Customer = db.Contacts.Find(hargaJualPerCustomer.ContactID);
            ViewBag.Nama = Customer.Perusahaan;
            return View(hargaJualPerCustomer);
        }

        // POST: HargaJualPerCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HargaJualPerCustomer hargaJualPerCustomer = db.HargaJualPerCustomers.Find(id);
            db.HargaJualPerCustomers.Remove(hargaJualPerCustomer);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = hargaJualPerCustomer.ContactID });
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
