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
    public class TutupSalesOrderRinciansController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: TutupSalesOrderRincians
        public ActionResult Index(int id)
        {
            var salesOrder = db.SalesOrders.Find(id);

            ViewBag.Customer = salesOrder.contact.Perusahaan;
            ViewBag.TglPesan = salesOrder.TglPesan.ToShortDateString();
            ViewBag.NoSO = salesOrder.NoSO;
            ViewBag.NOPO = salesOrder.NoPO;
            ViewBag.Catatan = salesOrder.Catatan;
            ViewBag.TglKirim = salesOrder.TglPengiriman.ToShortDateString() ;

            var salesOrderRincians = db.SalesOrderRincians.Where(x=>x.SalesOrderID ==id).Include(s => s.bahanBaku).Include(s => s.SalesOrder).Include(s => s.statusProduksi);
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
