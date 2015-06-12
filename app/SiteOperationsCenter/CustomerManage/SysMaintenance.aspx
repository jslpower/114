<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMaintenance.aspx.cs" Inherits="SiteOperationsCenter.CustomerManage.SysMaintenance" %>

<%@ Import Namespace="EyouSoft.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统维护</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    
    <script language="JavaScript">

        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
        var ajaxPages = {
            //基础数据
        pages: [{ 'Name': '客户类型', 'Url': 'AjaxByCustomerType.aspx', 'Container': 'divCustomer', 'Page': 1 }, 
                { 'Name': '适用产品', 'Url': 'AjaxBySuitProduct.aspx', 'Container': 'divSuitProduct', 'Page': 1}],
            //页面加载事件
            init: function(pageType) {
                $.ajax({
                    type: "POST",
                    url: ajaxPages.pages[pageType].Url,
                    cache: false,
                    async: false,
                    success: function(documents) {
                        $("#" + ajaxPages.pages[pageType].Container).html(documents);
                    }
                });
            }
        };
    </script>
</head>
<body>
    <form id="sys_form" runat="server">
     <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#7DC2E3">
                <strong>客户类型</strong>：
            </td>
            <td width="720" align="left" bgcolor="#7DC2E3">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td>
                
                <div id="divCustomer">
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#7DC2E3">
                <strong>适用产品</strong>：
            </td>
            <td width="720" align="left" bgcolor="#7DC2E3">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td>
                
                <div id="divSuitProduct">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
