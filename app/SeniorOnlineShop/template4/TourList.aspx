<%@ Page Language="C#" MasterPageFile="~/master/T4.Master" AutoEventWireup="true"
    CodeBehind="TourList.aspx.cs" Inherits="SeniorOnlineShop.TourList" Title="散拼计划" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/master/T4.Master" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="linetj">
        <div class="linetjtk">
            <div class="linetjth">
                我的旅游线路
                <table width="560" border="0" align="right" cellpadding="0" cellspacing="0" style="margin-top: -7px;
                    *margin-top: -24px; font-weight: normal; color: #FFFFFF;">
                    <tr>
                        <td width="500" align="left">
                            <font color="#FFFFFF">筛选：
                                <asp:DropDownList ID="dropLeaveCity" runat="server">
                                </asp:DropDownList>
                                &nbsp;&nbsp; 线路名称
                                <input name="txtRouteName" id="txtRouteName" type="text" size="3" runat="server" />
                                天数
                                <input name="txtDay" id="txtDay" type="text" size="1" class="borderlv" runat="server"
                                    style="width: 20px" />
                                出团日期
                                <input name="txtStartDate" id="txtStartDate" class="borderlv" type="text" size="8"
                                    runat="server" />至<input name="txtEndDate" id="txtEndDate" class="borderlv" runat="server"
                                        type="text" size="8" />
                            </font>
                        </td>
                        <td width="60" align="center">
                            <a href="javascript:void(0);" onclick="javascript:SeachTours('-1');return false;">
                                <img src="<%= ImageServerPath %>/T4/images/search2.gif" width="40" height="18" border="0" /></a>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="linetjxx">
                <div class="linemenu">
                    <ul>
                        <ul style="text-align: left">
                            <li><a href="javascript:void(0)" onclick="javascript:SeachTours('0');return false;">
                                全部</a></li>
                            <asp:Repeater runat="server" ID="rptAreas">
                                <ItemTemplate>
                                    <li><a href="javascript:void(0)" onclick="javascript:SeachTours(<%#Eval("AreaId") %>);return false;">
                                        <nobr><%#Eval("AreaName") %></nobr>
                                    </a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </ul>
                </div>
                <table width="665" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#E3E3E3"
                    style="margin: 10px;">
                    <tr>
                        <th width="62" height="26" align="center" bgcolor="#F4F4F4">
                            出发
                        </th>
                        <th width="242" height="26" align="center" bgcolor="#F4F4F4">
                            团队基本信息
                        </th>
                        <th width="77" height="26" align="center" bgcolor="#F4F4F4">
                            出团时间
                        </th>
                        <th width="32" height="26" align="center" bgcolor="#F4F4F4">
                            成人价
                        </th>
                        <th width="81" height="26" align="center" bgcolor="#F4F4F4">
                            儿童价
                        </th>
                        <th width="71" align="center" bgcolor="#F4F4F4">
                            交易操作
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptTourList">
                        <ItemTemplate>
                            <tr>
                                <td align="center" width="62">
                                    <%# Eval("StartCityName")%>
                                </td>
                                <td align="left" class="jiange">
                                    <span style="float: left" title="<%# Eval("RouteName") %>">
                                     <a target="_blank" href="<%#Utils.GenerateShopPageUrl2("/TourDetail2_"+Eval("RouteId"),Utils.GetQueryStringValue("cid")) %>">
                                        <%#Utils.GetText(Eval("RouteName").ToString(), 15, false)%></a> </span>
                                    <%#GetRecommendType(((int)Eval("RecommendType")).ToString())%>
                                </td>
                                <td align="center">
                                    <%# GetLeaveInfo(DateTime.Parse(Eval("LeaveDate").ToString()))%>
                                </td>
                                <td align="center">
                                    ￥<%# Eval("RetailAdultPrice", "{0:F0}")%>/<%# Eval("SettlementAudltPrice", "{0:F0}")%>
                                </td>
                                <td align="center" width="81">
                                    ￥<%# Eval("RetailChildrenPrice", "{0:F0}")%>/<%# Eval("SettlementChildrenPrice", "{0:F0}")%>
                                </td>
                                <td align="center" width="69">
                                    <%#GetOrderByRoute(Eval("TourId").ToString())%><br />
                                    <%=mqUrl%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr runat="server" id="trNoData" visible="false">
                        <td height="100px" colspan="6" align="center">
                            对不起，此线路区域下没有计划信息！
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="right">
                            <div class="digg" align="center">
                                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageStyleType="NewButton" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet"
        type="text/css" />

    <script type="text/javascript">
        $(function() {
            $("#<%=txtStartDate.ClientID %>").focus(function() {
                WdatePicker({
                    onpicked: function() {
                        $("#<%= txtEndDate.ClientID%>")[0].focus();
                    }
                });
            });
            $("#<%=txtStartDate.ClientID %>,#<%= txtEndDate.ClientID%>").focus(function() {
                WdatePicker();
            });

            $(".goumai0").click(function() {
                if ("<%=IsLogin %>" == "False") {
                    Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true });
                    return false;
                }

            });
        });

        function SeachTours(AreaId) {
            var url = "/template4/TourList.aspx" + '?cid=<%=Request.QueryString["cid"] %>' + '&State=<%=EyouSoft.Common.Utils.GetQueryStringValue("State") %>';
            if (parseInt(AreaId) >= 0) {
                url += "&AreaId=" + AreaId;
            }
            else {
                var LeaveCity = $("#<%=dropLeaveCity.ClientID %>").val();
                var RouteName = $.trim($("#<%=txtRouteName.ClientID %>").val());
                var Days = $.trim($("#<%=txtDay.ClientID %>").val());
                var StartDate = $.trim($("#<%=txtStartDate.ClientID %>").val());
                var EndDate = $.trim($("#<%=txtEndDate.ClientID %>").val());
                url += "&CityId=" + LeaveCity + "&RouteName=" + encodeURIComponent(RouteName) + "&Days=" + Days + "&StartDate=" + StartDate + "&EndDate=" + EndDate;
            }
            window.location.href = url;
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="GuidebookPlaceHolder" runat="server">
</asp:Content>
