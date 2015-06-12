<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourList.aspx.cs" Inherits="UserBackCenter.CustomerManage.TourList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
       <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    var TourList={
        //行背景色切换
        mouseovertr:function(o){
             o.style.backgroundColor="#FFF9E7";
        },
        mouseouttr:function(o){
             o.style.backgroundColor="";
        }
      }  
    </script>
    <style  type="text/css">
    *{
      padding:0px; margin:0px;
    }
body { margin:0px; padding:0px;width:90%;text-align:center;}
<%--#container{text-align:left;margin:0 auto;width:100%;padding:0px;position:absolute; left:50%;margin-left:-300px;}
--%>
#container{text-align:center;margin:0 auto;width:100%;padding:0px; margin-left:25px;}
td { font-size:12px;padding:2px;}
table{	border-collapse:collapse;margin:0px;padding:0px;}
img { border:none}
.hui { color:#aaaaaa}
</style>
</head>
<body>
    <div id="container">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="5" align="center">
  <tr>
    <td bgcolor="#DEF2FC"><%=companyName %> &nbsp;许可证号：<%=cer %>&nbsp;负责人：<%=admin %> </td>
  </tr>
</table>

 <asp:CustomRepeater id="tl_rpt_TourList" runat="server"  >
 <HeaderTemplate>
 <table width="100%" border="1" cellpadding="3" cellspacing="0" bordercolor="#B9D3E7" style="text-align:center; margin:auto;"  align="center" >

  <tr>
    <th width="20%" height="20" align="center" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#CEE4EF">线路区域</th>
    <th width="42%" align="center" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#CEE4EF">线路名称</th>
    <th width="20%" align="center" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#CEE4EF">出团时间</th>
    <th width="18%" align="center" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#CEE4EF">线路负责人</th>
  </tr>
  </HeaderTemplate>
  <ItemTemplate>
  <tr onmouseover="TourList.mouseovertr(this)" onmouseout="TourList.mouseouttr(this)">
    <td align="center" class="tbline"><%#GetAreaName(Convert.ToInt32(Eval("AreaId")))%></td>
    <td align="center" class="tbline"><%# Eval("RouteName") %></td>
     <td align="center" class="tbline">【<%# Convert.ToDateTime(Eval("LeaveDate")).ToString("yyyy-MM-dd") %>】</td>
    <td align="left" class="tbline"><a  style="cursor:default; text-decoration:none"><%# Eval("TourContact")%></a></td>
  </tr>
  </ItemTemplate>
  <FooterTemplate>

  </table>
  </FooterTemplate>
  </asp:CustomRepeater>
   
 <table id="rs_ExportPage"  cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
          <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
        </td>
    </tr>
</table>
<table width="100%" border="0" >
  <tr>
    <td align="center"></td>

    <td align="center"><input type="button" name="Submit" value="关闭" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"/></td>
  </tr>
  </tbody>
</table>
    </form>
    </div>
</body>
</html>
