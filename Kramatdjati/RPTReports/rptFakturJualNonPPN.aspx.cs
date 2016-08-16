using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kramatdjati.RPTDatasets.dsFakturJualTableAdapters;
using Kramatdjati.RPTDatasets;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Kramatdjati.RPTReports
{
    public partial class rptFakturJualNonPPN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string strFakturJualID = Request.QueryString["FakturJualID"];

                int intFakturJualID = 0;
                if (int.TryParse(strFakturJualID, out intFakturJualID) == false)
                {
                    intFakturJualID = 0;
                }

                dsFakturJual ds = new dsFakturJual();

                FakturJualHTableAdapter qry = new FakturJualHTableAdapter();
                qry.Fill(ds.FakturJualH, intFakturJualID);

                FakturJualDTableAdapter qryD = new FakturJualDTableAdapter();
                qryD.Fill(ds.FakturJualD, intFakturJualID);

                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FakturJualH", ds.FakturJualH.ToList()));
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FakturJualD", ds.FakturJualD.ToList()));

            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            string namaFileOutput = string.Format("~/PDF/Output{0:ddMMyyHHmmss}.pdf", DateTime.Now);
            string namaFilePrint = string.Format("~/PDF/Print{0:ddMMyyHHmmss}.pdf", DateTime.Now);

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            //FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(namaFileOutput),FileMode.Create);
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();

            //PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath(namaFileOutput));
            PdfReader reader = new PdfReader(bytes);
            Rectangle psize = reader.GetPageSize(1);

            //Open existing PDF
            Document document = new Document(psize, 0, 0, 0, 0);

            //Getting a instance of new PDF writer
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath(namaFilePrint), FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            int i = 0;
            int p = 0;
            int n = reader.NumberOfPages;

            float width = psize.Width;
            float height = psize.Height;

            //Add Page to new document
            while (i < n)
            {
                document.NewPage();
                p++;
                i++;

                PdfImportedPage page1 = writer.GetImportedPage(reader, i);
                cb.AddTemplate(page1, 0, 0);
            }

            //Attach javascript to the document
            PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
            writer.AddJavaScript(jAction);
            document.Close();

            //Attach pdf to the iframe
            frmPrint.Attributes["src"] = namaFilePrint;
            //frmPrint.Attributes["src"] = namaFileOutput;

        }

    }
}