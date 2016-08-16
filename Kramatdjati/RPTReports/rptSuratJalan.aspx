<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptSuratJalan.aspx.cs" Inherits="Kramatdjati.RPTReports.rptSuratJalan" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click"  Text="Cetak" />
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                <LocalReport ReportPath="RPTReports\rptSuratJalan.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
          </div>
        <iframe id="frmPrint" name="IframeName" width="500"  height="200" runat="server" style="display: none" ></iframe>
     </form>
</body>
</html>



