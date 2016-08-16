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
    [ClaimsAccess(Issuer = "LocalClaims", ClaimType = "Form", Value = "SIUDJurnalUmumPosting")]
    public class tblGLBatchDetailsReadOnlyController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: tblGLBatchDetailsReadOnly
        public ActionResult Index(int id)
        {
            var tblGLBatchDetails = db.tblGLBatchDetails.Where(m => m.tblGLBatchId == id).Include(t => t.tblGLAccount);
            ViewBag.tblGLBatchId = id;
            return View(tblGLBatchDetails.ToList());
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
