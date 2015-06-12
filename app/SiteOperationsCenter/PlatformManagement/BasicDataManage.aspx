<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicDataManage.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.BasicDataManage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>基础数据维护</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script language="JavaScript">
 
  function mouseovertr(o) {
	  o.style.backgroundColor="#FFF9E7";
      //o.style.cursor="hand";
  }
  function mouseouttr(o) {
	  o.style.backgroundColor=""
  }
    var ajaxPages = {
        //基础数据
        pages: [{ 'Name': '线路主题', 'Url': 'AjaxByRouteProList.aspx', 'Container': 'divRoute', 'Page': 1 }
                        , { 'Name': '报价等级', 'Url': 'AjaxByPriceProList.aspx', 'Container': 'divPrice', 'Page': 1 }
                        , { 'Name': '客户等级', 'Url': 'AjaxByCustormerProList.aspx', 'Container': 'divCustomer', 'Page': 1}],
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
    <form id="form1" name="form1" runat="server">
    <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#7DC2E3">
                <strong>主题类型</strong>：
            </td>
            <td width="720" align="left" bgcolor="#7DC2E3">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#C3E3F2">
                <strong>线路主题</strong>：
            </td>
            <td width="720" align="left" bgcolor="#C3E3F2">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td>
                <div id="divRoute">
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#7DC2E3">
                <strong>报价等级</strong>：
            </td>
            <td width="720" align="left" bgcolor="#7DC2E3">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td>
                
                <div id="divPrice">
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#7DC2E3">
                <strong>客户等级</strong>：
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
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
