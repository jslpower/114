<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteList.aspx.cs" Inherits="UserBackCenter.TeamService.RouteList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    var RouteList={
        //行背景色切换
        mouseovertr:function(o){
             o.style.backgroundColor="#FFF9E7";
        },
        mouseouttr:function(o){
             o.style.backgroundColor="";
        }
      }  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="width:500px; text-align:center; margin:auto;">
<table width="96%" border="0" cellspacing="0" cellpadding="5">
  <tr>
    <td bgcolor="#DEF2FC"><%=companyName %>&nbsp; <span style="color:#0048A3">许可证号：</span><%=cer %>&nbsp;<span style="color:#0048A3">负责人：</span><%=admin %> </td>
  </tr>
</table>

 <asp:CustomRepeater id="rl_rpt_TourList" runat="server"  >
 <HeaderTemplate>
 <table width="96%" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7" style="text-align:center;">
  <tr>
   <%-- <th width="8%" height="20" align="center" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#CEE4EF">选择</th>--%>
    <th width="92%" align="center" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#CEE4EF">线路名称</th>
  </tr>
  </HeaderTemplate>
  <ItemTemplate>
  <tr onmouseover="RouteList.mouseovertr(this)" onmouseout="RouteList.mouseouttr(this)">
   <%-- <td align="center" class="tbline"></td>--%>
   
    <td align="left" class="tbline"><img src="<%=ImageServerUrl%>/images/ico.gif" width="11" height="11" />【<%# Convert.ToDateTime(Eval("LeaveDate")).ToString("yyyy-MM-dd")%>】<%# Eval("RouteName") %></td>
  </tr>
  </ItemTemplate>
  <FooterTemplate>

  </table>
  </FooterTemplate>
  </asp:CustomRepeater>

   <table id="rs_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
          <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
        </td>
    </tr>
</table>
</div>
<table width="96%" border="0" >
  <tr>
    <td align="center"><input type="button" name="Submit" value="关闭" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"/></td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
