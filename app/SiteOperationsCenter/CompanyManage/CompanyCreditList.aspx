<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyCreditList.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.CompanyCreditList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看公司证书</title>
      <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <%=AllCompanyCertif %>
    </form>
</body>
</html>
