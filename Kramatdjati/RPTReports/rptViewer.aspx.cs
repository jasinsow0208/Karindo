using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kramatdjati.RPTDatasets;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Kramatdjati.RPTReports
{
    public partial class rptViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if (Request.QueryString["Counter"] != null)
                //{
                string strSuratJalanID = Request.QueryString["Counter"];
                int suratJalanID = int.Parse(strSuratJalanID);

                    SqlCommand cmdLatest = OpenConnection("spTesCetak");
                    cmdLatest.Parameters.AddWithValue("@suratJalanID", suratJalanID);

                    DataSet dt = GetDataSet(cmdLatest);

                    //ReportViewer1.Reset();
                    //ReportViewer1.LocalReport.ReportPath = "../RPTReports/rptTesCetak.rdlc";
                    //ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt.Tables[0]));

                    //string DomainURL = Request.Url.GetLeftPart(UriPartial.Authority);
                    //ReportViewer1.Reset();
                    //List<ReportParameter> reportParameter = null;
                    //reportParameter = new List<ReportParameter>(1);
                    //reportParameter.Add(new ReportParameter("SlideURL", DomainURL));
                    //ReportViewer1.LocalReport.ReportPath = "RDLC/rptReport1.rdlc";
                    //ReportViewer1.LocalReport.EnableExternalImages = true;
                    //ReportViewer1.LocalReport.SetParameters(reportParameter);
                    //ReportViewer1.LocalReport.DataSources.Clear();
                    //ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt.Tables[0]));
                //}
            }

        }
        public SqlCommand OpenConnection(string strSpName)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["IdentityDb"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSpName;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            return cmd;
        }
        public DataSet GetDataSet(SqlCommand cmd)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Microsoft.Reporting.WebForms.Warning[]  warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);

            string namaFileOutput = string.Format("~/PDF/Output{0:ddMMyyHHmmss}.pdf", DateTime.Now);
            string namaFilePrint = string.Format("~/PDF/Print{0:ddMMyyHHmmss}.pdf", DateTime.Now);
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(namaFileOutput),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            //Open existing PDF
            Document document = new Document(PageSize.LETTER);
            PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath(namaFileOutput));
            //Getting a instance of new PDF writer
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(
               HttpContext.Current.Server.MapPath(namaFilePrint), FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            int i = 0;
            int p = 0;
            int n = reader.NumberOfPages;
            Rectangle psize = reader.GetPageSize(1);

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
        }

    }
}