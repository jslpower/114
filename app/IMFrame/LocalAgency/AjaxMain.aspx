<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMain.aspx.cs" Inherits="IMFrame.LocalAgency.AjaxMain" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>
<form id="form1" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <asp:repeater id="rptRoutes" runat="server">

 <ItemTemplate>
            <tr>
                <td height="30" style="border-bottom: 1px solid #dddddd;">
                  <input type="checkbox" name="checkbox" class="RouteMain"  value='<%#Eval("RouteId")%>' routeType='<%#Eval("RouteType") %>' />
                    <a href="javascript:void(0)" onclick="AjaxMain.ShowPrint('<%#Eval("RouteId")%>',this)" target="_blank" >
                       <%#Eval("RouteName")%></a>
                </td>
            </tr>
        </ItemTemplate>

</asp:repeater>
</table>
<table width="100%" runat="server" id="NoData" visible="false" border="0" cellspacing="0"
    cellpadding="0">
    <tr>
        <td height="30" style="border-bottom: 1px solid #dddddd; text-align: center;">
            暂无你的线路数据！
        </td>
    </tr>
</table>
<div class="digg" style="text-align: center">
    <table>
        <tr align="center">
            <td>
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" PageStyleType="MostEasyNewButtonStyle"
                    HrefType="JsHref"/>
            </td>
        </tr>
    </table>
</div>
</form>

<script type="text/javascript">
    AjaxMain = {
        ShowPrint: function(id, obj) {
            //$(obj).attr("href",GetDesPlatformUrlForMQMsg("<%=Domain.UserBackCenter %>/RouteAgency/RouteManage/RoutePrint.aspx?RouteID="+id,"<%= MQLoginId%>","<%= Password%>"));
            //跳转到线路详细里边 2012-02-22 修改 方琪
            $(obj).attr("href", GetDesPlatformUrlForMQMsg("<%=Domain.UserBackCenter %>/PrintPage/RouteDetail.aspx?RouteId=" + id, "<%= MQLoginId%>", "<%= Password%>"));
        }
    }
</script>

