using Kramatdjati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Kramatdjati.RPTDatasets.dsSuratJalanTableAdapters;
using Kramatdjati.RPTDatasets;
using System.Web.UI.WebControls;


namespace Kramatdjati.Controllers
{
    public class CetakLaporanController : Controller
    {
        // GET: CetakLaporan
        public ActionResult Index()
        {
            ContohCetak contohCetak = new ContohCetak() {Counter=2096,Data="Contoh1" };
            List<ContohCetak> contohCetaks = new List<ContohCetak>();
            contohCetaks.Add(contohCetak);

            return View(contohCetaks.ToList ());
        }

        public ActionResult SuratJalan(int id)
        {
            ViewBag.SuratJalanID = id;
            ContohCetak contohCetak = new ContohCetak() { Counter = id, Data = "SuratJalanID" };
           return View(contohCetak);
        }
        public ActionResult SuratJalanNF(int id)
        {
            ViewBag.SuratJalanID = id;
            ContohCetak contohCetak = new ContohCetak() { Counter = id, Data = "SuratJalanID" };
            return View(contohCetak);
        }

        public ActionResult SuratJalanPPN(int id)
        {
            ViewBag.SuratJalanID = id;
            ContohCetak contohCetak = new ContohCetak() { Counter = id, Data = "SuratJalanID" };
            return View(contohCetak);
        }

        public ActionResult SuratJalanPPNNF(int id)
        {
            ViewBag.SuratJalanID = id;
            ContohCetak contohCetak = new ContohCetak() { Counter = id, Data = "SuratJalanID" };
            return View(contohCetak);
        }

        public ActionResult FakturJualPPN(int id)
        {
            ViewBag.FakturJualID = id;
            ContohCetak contohCetak = new ContohCetak() { Counter = id, Data = "FakturJualID" };
            return View(contohCetak);
        }

        public ActionResult FakturJualNonPPN(int id)
        {
            ViewBag.FakturJualID = id;
            ContohCetak contohCetak = new ContohCetak() { Counter = id, Data = "FakturJualID" };
            return View(contohCetak);
        }

        public ActionResult SuratJalanNF1(int id)
        {
            dsSuratJalan ds = new dsSuratJalan();

            SuratJalanHTableAdapter qry = new SuratJalanHTableAdapter();
            qry.Fill(ds.SuratJalanH, id);

            SuratJalanDTableAdapter qryD = new SuratJalanDTableAdapter();
            qryD.Fill(ds.SuratJalanD, id);

            ReportViewer reportViewer = new ReportViewer()
            {
                SizeToReportContent = true,
                Width =  Unit.Percentage(100),
                Height = Unit.Percentage(100),
            }; 
            reportViewer.ProcessingMode = ProcessingMode.Local;

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"RPTReports\rptSuratJalanNF.rdlc";
            reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("SuratJalanH", ds.SuratJalanH.ToList()));
            reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("SuratJalanD", ds.SuratJalanD.ToList()));

            ViewBag.ReportViewer = reportViewer;

            return View();
        }
    }
}