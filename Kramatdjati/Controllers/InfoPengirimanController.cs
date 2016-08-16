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
    public class InfoPengirimanController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: InfoPengiriman
        public ActionResult Index(int id)
        {
            var salesOrder=db.SalesOrderRincians.Find(id) ;
            ViewBag.SalesOrderID = salesOrder.SalesOrderID ;
            ViewBag.NoSO = salesOrder.SalesOrder.NoSO;
            ViewBag.KodeBarang = salesOrder.bahanBaku.KodeBahanBaku;
 
            var suratJalanRincians = db.SuratJalanRincians.Where(x=>x.SalesOrderRincianID ==id && x.Kirim == true).Include(s => s.salesOrderRincian).Include(s => s.suratJalan);
            return View(suratJalanRincians.ToList());
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
