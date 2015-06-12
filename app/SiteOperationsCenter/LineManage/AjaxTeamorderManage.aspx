<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTeamorderManage.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.AjaxTeamorderManage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<cc1:CustomRepeater ID="repList" runat="server">
    <HeaderTemplate>
    
        <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#9dc4dc"
            class="table_basic">
            <tr class="list_basicbg">
                <th align="middle" nowrap="nowrap">
                    出发城市
                </th>
                <th align="middle" nowrap="nowrap">
                    线路名称
                </th>
                <th align="middle" nowrap="nowrap">
                    出发时间
                </th>
                <th align="middle" nowrap="nowrap">
                    专线商
                </th>
                <th align="middle" nowrap="nowrap">
                     预定单位
                </th>
                <th align="middle" nowrap="nowrap">
                    <strong>预定时间</strong>
                </th>
                <th align="middle" nowrap="nowrap">
                    <strong>人数</strong>
                </th>
                <th align="middle" nowrap="nowrap">
                    状态
                </th>
                <th align="middle" nowrap="nowrap">
                    操作
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td align="center">
                <%# Eval("StartCityName")%>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick="UrlUpdateLine('<%#Eval("RouteId") %>','LineName');return false;">
                    <%# Eval("RouteName")%></a>
            </td>
            <td align="center">
                <%# Convert.ToDateTime(Utils.GetDateTimeNullable(Eval("StartDate").ToString())).ToShortDateString()%>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick="UrlUpdateLine('<%#Eval("Business") %>','Company');return false;">
                    <%# Eval("BusinessName")%></a>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick="UrlUpdateLine('<%#Eval("Business") %>','Company');return false;">
                    <%# Eval("TravelName")%></a>
            </td>
            <td align="center">
                <%# Utils.GetDateTimeNullable(Eval("IssueTime").ToString())%>
            </td>
            <td align="center">
                <%# Eval("ScheduleNum")%>
            </td>
            <td align="center">
                <%# Eval("OrderStatus")%>
            </td>
            <td align="center">
                <a href="TeamorderDetail.aspx?TourId=<%# Eval("TourId") %>">查看</a> <a href="/Statistics/LogManagement.aspx?OrderNo=<%# Eval("OrderNo")%>">
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


    
    function UrlUpdateLine(id,type) {
         if(type=="LineName")//跳转线路
         {
            window.location="AddLine.aspx?LineId="+id;
         }
         else if(type=="Company")
         {
            window.location="/CompanyManage/AddBusinessMemeber.aspx?EditId="+id;
         }
    } 
</script>

