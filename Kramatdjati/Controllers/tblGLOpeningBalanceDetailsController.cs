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
    public class tblGLOpeningBalanceDetailsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblGLOpeningBalanceDetails
        public ActionResult Index(int id)
        {
            var tblGLOpeningBalanceDetails = db.tblGLOpeningBalanceDetails.Where (x=>x.tblGLOpeningBalanceId ==id).Include(t => t.tblGLAccount).Include(t => t.tblGLOpeningBalance);
            ViewBag.tblGLOpeningBalanceId = id;
            return View(tblGLOpeningBalanceDetails.ToList());
        }

        // GET: tblGLOpeningBalanceDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLOpeningBalanceDetail tblGLOpeningBalanceDetail = db.tblGLOpeningBalanceDetails.Find(id);
            if (tblGLOpeningBalanceDetail == null)
            {
                return HttpNotFound();
            }
             return View(tblGLOpeningBalanceDetail);
        }

        // GET: tblGLOpeningBalanceDetails/Create
        public ActionResult Create(int id)
        {
            ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc");
            ViewBag.tblGLOpeningBalanceId = id;
            return View();
        }

        // POST: tblGLOpeningBalanceDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblGLOpeningBalanceDetailId,tblGLOpeningBalanceId,tblGLAccountId,Debet,Kredit,Keterangan")] tblGLOpeningBalanceDetail tblGLOpeningBalanceDetail)
        {
            if (ModelState.IsValid)
            {
                db.tblGLOpeningBalanceDetails.Add(tblGLOpeningBalanceDetail);
                db.SaveChanges();
                return RedirectToAction("Index", new {id=tblGLOpeningBalanceDetail.tblGLOpeningBalanceId   });
            }

            ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblGLOpeningBalanceDetail.tblGLAccountId);
            return View(tblGLOpeningBalanceDetail);
        }

        // GET: tblGLOpeningBalanceDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLOpeningBalanceDetail tblGLOpeningBalanceDetail = db.tblGLOpeningBalanceDetails.Find(id);
            if (tblGLOpeningBalanceDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblGLAccountId = new SelectList(tblViewNoReks (), "tblGLAccountId", "AccCodeDesc", tblGLOpeningBalanceDetail.tblGLAccountId);
            ViewBag.tblGLOpeningBalanceId = tblGLOpeningBalanceDetail.tblGLOpeningBalanceId ;
            return View(tblGLOpeningBalanceDetail);
        }

        // POST: tblGLOpeningBalanceDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblGLOpeningBalanceDetailId,tblGLOpeningBalanceId,tblGLAccountId,Debet,Kredit,Keterangan")] tblGLOpeningBalanceDetail tblGLOpeningBalanceDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGLOpeningBalanceDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {id=tblGLOpeningBalanceDetail.tblGLOpeningBalanceId  });
            }
            ViewBag.tblGLAccountId = new SelectList(tblViewNoReks(), "tblGLAccountId", "AccCodeDesc", tblGLOpeningBalanceDetail.tblGLAccountId);
            ViewBag.tblGLOpeningBalanceId = tblGLOpeningBalanceDetail.tblGLOpeningBalanceId ;
            return View(tblGLOpeningBalanceDetail);
        }

        // GET: tblGLOpeningBalanceDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGLOpeningBalanceDetail tblGLOpeningBalanceDetail = db.tblGLOpeningBalanceDetails.Find(id);
            if (tblGLOpeningBalanceDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblGLOpeningBalanceDetail);
        }

        // POST: tblGLOpeningBalanceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGLOpeningBalanceDetail tblGLOpeningBalanceDetail = db.tblGLOpeningBalanceDetails.Find(id);
            db.tblGLOpeningBalanceDetails.Remove(tblGLOpeningBalanceDetail);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = tblGLOpeningBalanceDetail.tblGLOpeningBalanceId });
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

            foreach (tblGLAccount rw in db.tblGLAccounts.OrderBy (x=>x.AccCode ))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(rw.AccCode.Trim().PadRight(15, ' '));
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
