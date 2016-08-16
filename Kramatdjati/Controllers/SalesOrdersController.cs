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
    public class SalesOrdersController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: SalesOrders
        public ActionResult Index(bool? Posting)
        {
            if (Posting == null)
            {
                Posting = false;
            };
            var salesOrders = db.SalesOrders.Where(x => x.Posting == Posting).Include(s => s.contact);
            return View(salesOrders.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "SalesOrderID,ContactID,NoPemesananBarang,TglPesan,TglPengiriman,User,Catatan,Posting,Closed, NoPO")] SalesOrder  salesOrder)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Posting = salesOrder.Posting });
            }
            return RedirectToAction("Index");
        }
      
        // GET: SalesOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        }
        public ActionResult UndoPostingInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        }

        // GET: SalesOrders/Create
        public ActionResult Create()
        {

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer  == true).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan");
            //Total piutang belum dihitung
            //kalau melebihi dari kredit limit tidak ditampilkan

             return View();
        }

   
        // POST: SalesOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesOrderID,ContactID,NoPemesananBarang,TglPesan,TglPengiriman,User,Catatan,Posting,Closed, NoPO")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                db.SalesOrders.Add(salesOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true ).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", salesOrder.ContactID);
            return View(salesOrder);
        }

        // GET: SalesOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true ).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", salesOrder.ContactID);
            return View(salesOrder);
        }

        // POST: SalesOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesOrderID,ContactID,NoPemesananBarang,TglPesan,TglPengiriman,User,Catatan,Posting,Closed, NoPO")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactID = new SelectList(db.Contacts.Where(x => x.Customer == true ).OrderBy(x => x.Perusahaan), "ContactID", "Perusahaan", salesOrder.ContactID);
            return View(salesOrder);
        }

        // GET: SalesOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        }

        // POST: SalesOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            db.SalesOrders.Remove(salesOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Posting(int id)
        {
            SalesOrder  salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            tblDefault Default = db.tblDefaults.First();

            int NoSO = Default.NoSO ;
            int thn = Default.CurrentYear;
            string FormatNoSO = string.Format("{0}.{1}", thn - 2000, NoSO.ToString("D4"));
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (salesOrder.NoSO == null)
                    {
                        salesOrder.NoSO = FormatNoSO;
                        Default.NoSO = Default.NoSO + 1;
                        db.Entry(Default).State = EntityState.Modified;
                     }

                    salesOrder.Posting = true;
                    salesOrder.User = User.Identity.Name;
                    db.Entry(salesOrder).State = EntityState.Modified;
                   
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
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
 
                    salesOrder.Posting = false;
                    salesOrder.User = User.Identity.Name;
                    db.Entry(salesOrder).State = EntityState.Modified;

                    db.SaveChanges();

                    transaction.Commit();

                    ViewBag.Info = "Batal Posting Berhasil";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.Info = "Batal Posting Tidak berhasil " + e.Message.Trim();

                }
            }
            return View("Informasi");
        }

        public ActionResult Email(int id)
        {
            var salesOrder = db.SalesOrders.Where(x => x.SalesOrderID  == id).Include(p => p.contact).FirstOrDefault();

            dynamic email = new Email("EmailSalesOrder");

            if (salesOrder.contact.Email == null)
            {
                email.to = "jasinsowandi@gmail.com";
            }
            else
            {
                email.to = salesOrder.contact.Email;
            }

            email.from = "jasinsowandi@yahoo.com";
            email.subject = "SO Nomor" + salesOrder.NoSO.ToString().Trim();
            email.TglKirim = salesOrder.TglPengiriman.ToShortDateString();
            email.TglTransaksi = salesOrder.TglPesan.ToShortDateString();
            email.NoSO = salesOrder.NoSO.ToString();
            email.Supplier = salesOrder.contact.Perusahaan;
            email.SalesOrderID = id;
            email.Catatan = salesOrder.Catatan;

            List<SalesOrderRincian > salesOrderRincians = db.SalesOrderRincians.Where(x => x.SalesOrderID == id).Include(p => p.bahanBaku).Include(p => p.SalesOrder ).ToList();
            decimal Total = salesOrderRincians.Sum(x => x.HargaJual  * x.Jumlah);
            email.Total = Total;
            email.salesOrderRincians = salesOrderRincians.ToList();

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

            var salesOrder = db.SalesOrders.Where(x => x.SalesOrderID  == id).Include(p => p.contact).FirstOrDefault();

            ViewBag.TglKirim = salesOrder.TglPengiriman.ToShortDateString();
            ViewBag.TglTransaksi = salesOrder.TglPesan.ToShortDateString();
            ViewBag.NoSO = salesOrder.NoSO.ToString();
            ViewBag.Customer = salesOrder.contact.Perusahaan;
            ViewBag.SalesOrderID = id;
            ViewBag.Catatan = salesOrder.Catatan;
            var salesOrderRincians = db.SalesOrderRincians.Where(x => x.SalesOrderID  == id).Include(p => p.bahanBaku).Include(p => p.SalesOrder );

            return View(salesOrderRincians.ToList());

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
