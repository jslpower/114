<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdManageHome.aspx.cs" Inherits="SiteOperationsCenter.AdManagement.AdManageHome" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

<style type="text/css">

a{TEXT-DECORATION:none}
</style>
</head>

<body>   
 <form id="form1" name="form1" method="post" action="">
 <cc1:CustomRepeater ID="crptLocationParent" runat="server" 
     onitemdatabound="crptLocationParent_ItemDataBound">
    <ItemTemplate>
      <table width="100%" border="0" cellspacing="0" cellpadding="5">
        <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
          <td width="16%"><h2 >
          <a id="acatelogo" runat="server"><%# Container.DataItem %></a></h2>
          
          <td width="68%">
             <asp:DataList ID="dltLocationChild" runat="server" RepeatColumns="3">             
                <ItemTemplate>
                <table width="100%" border="0" cellpadding="2" cellspacing="0" >
                  <tr >
                    <td style=" width:250px;"align="left" nowrap="nowrap" style=" font-size:small"><%# GetPostion(Eval("DisplayType").ToString(),Eval("Position").ToString())%></td>      
                  </tr>        
                   </table>          
                  </ItemTemplate>
                </asp:DataList>
        
          </td>
          <td width="16%"><a runat="server" id="hrefImg" style="cursor:pointer;" target="_blank"><img alt="点击查看大图" id="imgshow" src="<%#ImageServerUrl %>/images/yunying/siteimg/xiaotu.gif" width="80" height="64" border="0" /></a>
          </td>
        </tr>
      </table>
          
    </ItemTemplate>
 </cc1:CustomRepeater>
  <table width="99%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="10" align="right" bgcolor="#CCCCCC">&nbsp;</td>
  </tr>
</table>
<script type="text/javascript">
 
  function mouseovertr(o) {
	  o.style.backgroundColor="#FFF6D5";
      //o.style.cursor="hand";
  }
  function mouseouttr(o) {
	  o.style.backgroundColor=""
  }

</script>
</form>
</body>
</html>
