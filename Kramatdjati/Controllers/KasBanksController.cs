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
    public class KasBanksController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: KasBanks
        public ActionResult Index()
        {
            var kasBanks = db.KasBanks.Include(k => k.tblGlAccount);
            return View(kasBanks.ToList());
        }

        // GET: KasBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KasBank kasBank = db.KasBanks.Find(id);
            if (kasBank == null)
            {
                return HttpNotFound();
            }
            return View(kasBank);
        }

        // GET: KasBanks/Create
        public ActionResult Create()
        {
            ViewBag.tblGLAccountID = new SelectList(tblViewNoReks()  , "tblGLAccountId", "AccCodeDesc");
            return View();
        }

        // POST: KasBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KasBankID,NamaKasBank,NamaAccount,NoAccount,Saldo,tblGLAccountID")] KasBank kasBank)
        {
            if (ModelState.IsValid)
            {
                db.KasBanks.Add(kasBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tblGLAccountID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", kasBank.tblGLAccountID);
            return View(kasBank);
        }

        // GET: KasBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KasBank kasBank = db.KasBanks.Find(id);
            if (kasBank == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblGLAccountID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", kasBank.tblGLAccountID);
            return View(kasBank);
        }

        // POST: KasBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KasBankID,NamaKasBank,NamaAccount,NoAccount,Saldo,tblGLAccountID")] KasBank kasBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kasBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tblGLAccountID = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", kasBank.tblGLAccountID);
            return View(kasBank);
        }

        // GET: KasBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KasBank kasBank = db.KasBanks.Find(id);
            if (kasBank == null)
            {
                return HttpNotFound();
            }
            return View(kasBank);
        }

        // POST: KasBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KasBank kasBank = db.KasBanks.Find(id);
            db.KasBanks.Remove(kasBank);
            db.SaveChanges();
            return RedirectToAction("Index");
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
}
