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
using System.Text;

namespace Kramatdjati.Controllers
{
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDContact")]
    public class ContactsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Contacts
        public ActionResult Index()
        {
            var contacts = db.Contacts.Include(c => c.NoRekHutang).Include(c => c.NoRekPiutang);
            return View(contacts.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            List<ViewNoRek> ViewNoReks = tblViewNoReks();

            ViewBag.NoRekHutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc");
            ViewBag.NoRekPiutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc");
            return View();
        }


        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactID,Perusahaan,Kontak,Email,Alamat,Kota,Propinsi,Negara,Telepon,Fax,NoRekPiutangID,NoRekHutangID,PPN, KreditLimit, Customer, Supplier, NamaFaktur1, AlamatFaktur1, KotaFaktur1, NPWPFaktur1,NamaFaktur2, AlamatFaktur2, KotaFaktur2, NPWPFaktur2,Kredit ")] Contact contact)
        {
            contact.KreditLimit = 0;
            if (ModelState.IsValid)
            {
                contact.NoRekPiutangID = 13;
                contact.StatusKredit = "CBD";
                contact.KreditLimit = 0;
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<ViewNoRek> ViewNoReks = tblViewNoReks();
            ViewBag.NoRekHutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc", contact.NoRekHutangID);
            ViewBag.NoRekPiutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc", contact.NoRekPiutangID);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            List<ViewNoRek> ViewNoReks = tblViewNoReks();
            ViewBag.NoRekHutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc", contact.NoRekHutangID);
            ViewBag.NoRekPiutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc", contact.NoRekPiutangID);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactID,Perusahaan,Kontak,Email,Alamat,Kota,Propinsi,Negara,Telepon,Fax,NoRekPiutangID,NoRekHutangID,PPN, KreditLimit, Customer,Supplier,NamaFaktur1, AlamatFaktur1, KotaFaktur1, NPWPFaktur1,NamaFaktur2, AlamatFaktur2, KotaFaktur2, NPWPFaktur2,Kredit")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<ViewNoRek> ViewNoReks = tblViewNoReks();
            ViewBag.NoRekHutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc", contact.NoRekHutangID);
            ViewBag.NoRekPiutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc", contact.NoRekPiutangID);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<ViewNoRek> tblViewNoReks()
        {
            List<ViewNoRek> ViewNoReks = new List<ViewNoRek>();

            foreach (tblGLAccount rw in db.tblGLAccounts.OrderBy(x => x.AccCode))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(rw.AccCode.Trim().PadRight(10, ' '));
                if (rw.AccDescription != null)
                {
                    sb.Append(string.Format("{0} {1}", rw.AccName.Trim(), rw.AccDescription.Trim()));
                }
                else
                {
                    sb.Append(string.Format("{0}", rw.AccName.Trim()));
                };

                ViewNoRek ViewNoRek = new ViewNoRek
                {
                    tblGLAccountID = rw.tblGLAccountId,
                    AccCodeDesc = sb.ToString()
                };
                ViewNoReks.Add(ViewNoRek);
            }
            return ViewNoReks;
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
