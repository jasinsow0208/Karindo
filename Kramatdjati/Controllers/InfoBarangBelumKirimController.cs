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
    public class InfoBarangBelumKirimController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: InfoBarangBelumKirim
        public ActionResult Index()
        {
            var salesOrderRincians = db.SalesOrderRincians.Where(x=>x.statusLengkap != true && x.Jumlah - x.JmlYangSudahDiKirim > 0).Include(s => s.bahanBaku).Include(s => s.SalesOrder).Include(s => s.statusProduksi);
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
