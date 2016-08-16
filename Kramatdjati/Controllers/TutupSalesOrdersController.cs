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
using System.Data.SqlClient;

namespace Kramatdjati.Controllers
{
    public class TutupSalesOrdersController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: TutupSalesOrders
        public ActionResult Index()
        {
            var salesOrders = db.SalesOrders.Where(x=>x.Closed !=true).Include(s => s.contact);
            return View(salesOrders.ToList());
        }

        // GET: TutupSalesOrders/Details/5
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

        public ActionResult Posting(int id)
        {
            ViewBag.SalesOrderID = id;

            var ReturnParameter = new SqlParameter();
            ReturnParameter.ParameterName = "@ReturnValue";
            ReturnParameter.Direction = ParameterDirection.Output;
            ReturnParameter.SqlDbType = SqlDbType.Int;

            var Data = db.Database.ExecuteSqlCommand("spTutupSalesOrder @SalesOrderID, @User, @ReturnValue OUT",
                                                        new SqlParameter("@SalesOrderID", id),
                                                        new SqlParameter("@User", User.Identity.Name),
                                                        ReturnParameter);

            if (ReturnParameter.Value.ToString() == "0")
            {
                ViewBag.Info = "Tutup Sales Order Berhasil";
            }
            else
            {
                ViewBag.Info = string.Format("Tutup Sales Order Gagal. error: {0}", ReturnParameter.Value);
            };

            return View("Informasi");
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
