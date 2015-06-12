<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourList.aspx.cs" Title="散拼计划"
    Inherits="SeniorOnlineShop.seniorshop.TourList" MasterPageFile="~/master/SeniorShop.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:Content ContentPlaceHolderID="c1" ID="c1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="neiringhtn">
                <div class="bar-line-title1">
                    我的旅游线路</div>
                <div class="bar-line-mid1">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                &nbsp;<asp:DropDownList ID="dropLeaveCity" runat="server">
                                </asp:DropDownList>
                                线路名称<input name="txtRouteName" id="txtRouteName" type="text" size="11" runat="server" />
                                天数<input name="txtDay" id="txtDay" type="text" size="3" style="width: 20px;" runat="server" />
                                出团日期<input name="txtStartDate" id="txtStartDate" type="text" size="7" runat="server" />至<input
                                    name="txtEndDate" id="txtEndDate" runat="server" type="text" size="7" />
                                <input type="button" id="btnSearch" value="搜索" style="width: 35px;" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:CustomRepeater ID="repTourList" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="1" id="tbTouList" cellpadding="0" cellspacing="0" bordercolor="#E3E3E3"
                            class="liststyle">
                            <thead>
                                <tr>
                                    <th width="10%" align="center">
                                        出发
                                    </th>
                                    <th width="43%" align="center">
                                        团队基本信息
                                    </th>
                                    <th width="17%" align="center">
                                        班次
                                    </th>
                                    <th width="15%" id="thShowTravel" align="center">
                                        市场价
                                    </th>
                                    <th width="7%" align="center">
                                        操作
                                    </th>
                                    <th width="7%" align="center">
                                        查看
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td align="center">
                                    <%# Eval("StartCityName")%>
                                </td>
                                <td align="left">
                                    <div class="listtitle">
                                        <img src="<%=ImageServerPath %>/Images/seniorshop/ico.gif" width="11" height="11" />
                                        <%# GetRouteName(((int)Eval("RecommendType")).ToString())%>
                                        <a href='javascript:' class="RouteDetail" ref="<%# Eval("RouteId") %>">
                                            <%#Eval("RouteName")%></a>
                                    </div>
                                </td>
                                <%# TeamPlanDesAndPrices(Eval("RouteId").ToString(),Eval("RouteSource").ToString(),Eval("TeamPlanDes").ToString(),Convert.ToDecimal(Eval("RetailAdultPrice").ToString()).ToString("F0")) %>
                                <td align="center" width="80">
                                    <%#GetOrderByRoute(Eval("RouteId").ToString(), Eval("RouteSource"))%><br />
                                    <%= MqURl%>
                                </td>
                                <td align="center" width="80">
                                    <%# Eval("ClickNum")%>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </cc1:CustomRepeater>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="right">
                            <div style="margin: 10px 3px 3px; padding: 3px;" class="digg">
                                <cc3:ExporPageInfoSelect ID="ExportPageInfo1" runat="server" PageStyleType="NewButton" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet"
        type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("TourCalendar") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript">       
        $(function() {        
            $("#<%=txtStartDate.ClientID %>").focus(function() {
                WdatePicker({
                    onpicked: function() {
                        $("#<%= txtEndDate.ClientID%>")[0].focus();
                    }
                });
            });
            $("#<%=txtStartDate.ClientID %>,#<%=txtEndDate.ClientID%>").focus(function() {
                WdatePicker();
            });
            $("#btnSearch").click(function() {
                var LeaveCity = $("#<%=dropLeaveCity.ClientID %>").val();
                var RouteName = $.trim($("#<%=txtRouteName.ClientID %>").val());
                var Days = $.trim($("#<%=txtDay.ClientID %>").val());
                var StartDate = $.trim($("#<%=txtStartDate.ClientID %>").val());
                var EndDate = $.trim($("#<%=txtEndDate.ClientID %>").val());
                window.location.href = "/seniorshop/TourList.aspx" + "?CityId=" + LeaveCity + "&cid=<%=Request.QueryString["cid"] %>&RouteName=" + encodeURIComponent(RouteName) + "&Days=" + Days + "&StartDate=" + StartDate + "&EndDate=" + EndDate;
            });
            
            $(".RouteDetail").click(function(){
                var routeId=$(this).attr("ref");       
                var cid='<%=Request.QueryString["cid"] %>';         
                window.location.href = '<%=EyouSoft.Common.Utils.GenerateShopPageUrl2("/TourDetail_'+routeId+'", "'+cid+'") %>';
                return false;
            });                       
            
             $(".goumai0").click(function() {
                if ("<%=IsLogin %>" == "False") {
                    Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true });return false;
                }
                
            });
        });
    </script>

</asp:Content>
