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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "BatasKredit")]
    public class StatusKreditController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: StatusKredit
        public ActionResult Index()
        {
            var contacts = db.Contacts.Where(x=>x.Customer==true).Include(c => c.NoRekHutang).Include(c => c.NoRekPiutang);
            return View(contacts.ToList());
        }

 
        // GET: StatusKredit/Edit/5
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

            List<StatusKreditDropDown> statusKredits= new List<StatusKreditDropDown>();
            statusKredits.Add(new StatusKreditDropDown() { StatusKredit = "COD" });
            statusKredits.Add(new StatusKreditDropDown() { StatusKredit = "CBD" });
            statusKredits.Add(new StatusKreditDropDown() { StatusKredit = "Kredit" });

            List<ViewNoRek> ViewNoReks = tblViewNoReks();

            ViewBag.StatusKredit = new SelectList(statusKredits.ToList(), "StatusKredit", "StatusKredit", contact.StatusKredit);
            ViewBag.NoRekPiutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc", contact.NoRekPiutangID);

            return View(contact);
        }

        // POST: StatusKredit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactID,Perusahaan,Kontak,Email,Alamat,Kota,Propinsi,Negara,Telepon,Fax,NoRekPiutangID,NoRekHutangID,KreditLimit,PPN,Customer,Supplier,NamaFaktur1,AlamatFaktur1,KotaFaktur1,NPWPFaktur1,NamaFaktur2,AlamatFaktur2,KotaFaktur2,NPWPFaktur2,Kredit, StatusKredit")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<StatusKreditDropDown> statusKredits = new List<StatusKreditDropDown>();
            statusKredits.Add(new StatusKreditDropDown() { StatusKredit = "COD" });
            statusKredits.Add(new StatusKreditDropDown() { StatusKredit = "CBD" });
            statusKredits.Add(new StatusKreditDropDown() { StatusKredit = "Kredit" });

            List<ViewNoRek> ViewNoReks = tblViewNoReks();

            ViewBag.StatusKredit = new SelectList(statusKredits.ToList(), "StatusKredit", "StatusKredit", contact.StatusKredit);
            ViewBag.NoRekPiutangID = new SelectList(ViewNoReks, "tblGLAccountId", "AccCodeDesc", contact.NoRekPiutangID);
            return View(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
    }

    public class StatusKreditDropDown
    {
        public string StatusKredit { get; set; }
    }
}
