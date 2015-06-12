<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookList.aspx.cs" Inherits="SiteOperationsCenter.Statistics.BookList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
      <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
        
            <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                时间<input type="text" name="bl_txtStartDate"  style="width:85px"/>—<input type="text" name="bl_txtEndDate" style="width:85px;" />
                组团社名称<input type="text" name="bl_txtTourCompanyName" style="width:85px;" />
                团号<input type="text" name="bl_txtTourNo" style="width:85px;" />
                线路名称
                <input  type="text" class="textfield" size="12" id="pl_txtRouteName"  style="width:85px;"/>
                <a href="javascript:void(0);"><img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom: -3px;" /></a>
            </td>
        </tr>
    </table>
    <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>预定人数</strong></td>
            <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>下单时间</strong></td>
            <td width="20%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>组团社名称</strong></td>
            <td width="20%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>专线商名称</strong></td>
            <td  width="38%"align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>【团号】线路名称</strong></td>

            </tr>
            
              <tr class="baidi" onMouseOver="UserListManage.mouseovertr(this)" onMouseOut="UserListManage.mouseouttr(this)">
                <td  align="center">1+0</td>
                <td  align="center">2010-09-15 15:12:36</td>
                <td align="center"><a href="#">杭州旅行社</a></td>
                <td align="center"><a href="#>上海旅行社人</a></td>
                <td align="center">【09123456】<a href="#">上海一日游</a></td>
               
            
            </tr>
             <tr bgcolor="#F3F7FF" onMouseOver="UserListManage.mouseovertr(this)" onMouseOut="UserListManage.mouseouttr(this)">
                <td  align="center">2+1</td>
                <td  align="center">2010-09-23 15:45:36</td>
                <td align="center"><a href="#">上海旅行社</a></td>
                <td align="center"><a href="#">杭州旅行社</a></td>
                <td  align="center">【09123464】<a href="#">杭州一日游</a></td>
             
            
            </tr>
            </table>
     <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    var UserListManage = {
        mouseovertr: function(o) {
            o.style.backgroundColor = "#FFF9E7";
        },
        mouseouttr: function(o) {
            o.style.backgroundColor = "";
        }
    }
</script>
</html>
