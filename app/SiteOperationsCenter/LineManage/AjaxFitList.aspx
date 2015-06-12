<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxFitList.aspx.cs" Inherits="SiteOperationsCenter.LineManage.AjaxFitList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<cc1:CustomRepeater ID="repList" runat="server">
    <HeaderTemplate>
        <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C7DEEB"
            class="table_basic">
            <tr>
                <th>
                    订单号
                </th>
                <th>
                    出发时间
                </th>
                <th>
                    线路名
                </th>
                <th>
                    状态
                </th>
                <th>
                    支付
                </th>
                <th>
                    专线商
                </th>
                <th>
                    预订单位
                </th>
                <th>
                    游客
                </th>
                <th>
                    电话
                </th>
                <th>
                    人数
                </th>
                <th>
                    预定时间
                </th>
                <th>
                    操作
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="left">
                <%# Eval("OrderNo")%>
            </td>
            <td align="center">
                <%# Convert.ToDateTime(Eval("LeaveDate").ToString()).ToShortDateString()%>
            </td>
            <td align="left">
                <a href="AddLine.aspx?LineId=<%#Eval("RouteId") %>">
                    <%# Eval("RouteName")%></a>
            </td>
            <td align="center">
                <%# Eval("OrderStatus")%>
            </td>
            <td align="center">
                <%# Eval("PaymentStatus")%>
            </td>
            <td align="center">
                <%# Eval("Publishers")%>
            </td>
            <td align="left">
                <%# Eval("TravelName")%>
            </td>
            <td align="left">
                <%# Eval("VisitorContact")%>
            </td>
            <td align="center">
                <%# Eval("VisitorTel")%>
            </td>
            <td align="center">
                <%# Utils.GetInt(Eval("AdultNum").ToString()) +Utils.GetInt(Eval("ChildrenNum").ToString())%>
            </td>
            <td align="center">
                <%# Eval("IssueTime")%>
            </td>
            <td align="center">
                <a href="FitOrderDetail.aspx?OrderId=<%# Eval("OrderId")%>">查看</a> <a href="/Statistics/LogManagement.aspx?OrderNo=<%# Eval("OrderNo")%>">
                    日志</a>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</cc1:CustomRepeater>
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td height="30" align="right">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
    

</script>

