<%@ Page Language="C#" Inherits ="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Laporan Stok</title>
    <script runat="server" >
        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack ){
                List<Kramatdjati.Models.rptStok> stokbarangs = new List<Kramatdjati.Models.rptStok>() ;
                List<Kramatdjati.Models.BahanBaku> bahanbakus= new List<Kramatdjati.Models.BahanBaku>();
                  
                using (Kramatdjati.Infrastructure.AppIdentityDbContext db = new Kramatdjati.Infrastructure.AppIdentityDbContext())
                {
                      bahanbakus = db.BahanBakus.ToList() ;
                      foreach (Kramatdjati.Models.BahanBaku rw in bahanbakus)
                      {
                          Kramatdjati.Models.rptStok stokbarang = new Kramatdjati.Models.rptStok();
                          stokbarang.Departemen = rw.Departemen.Keterangan;
                          stokbarang.Divisi = rw.Divisi.Keterangan;
                          stokbarang.HargaJual = rw.HargaJual;
                          stokbarang.HargaRata2 = rw.HargaRata2;
                          stokbarang .HargaTerakhir =rw.HargaTerakhir ;
                          stokbarang.KodeBahanBaku =rw.KodeBahanBaku ;
                          stokbarang.NamaBarang =rw.Keterangan ;
                          stokbarang.Satuan =rw.satuan .Keterangan ;

                          stokbarangs.Add(stokbarang);
                      };
                };
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("StokBarang", stokbarangs);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
                
            }
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" AsyncRendering ="false" SizeToReportContent ="true" >
            <LocalReport ReportPath="RPTReports\rptdaftarBarang.rdlc"></LocalReport>
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
