<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanySaleCity.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.CompanySaleCity" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看公司销售城市</title>
      <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="1" class="kuang">
        <tr>
            <td align="left">
                <asp:Label ID="labSaleCityList" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
