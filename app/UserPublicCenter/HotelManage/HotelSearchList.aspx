<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelSearchList.aspx.cs"
    Inherits="UserPublicCenter.HotelManage.HotelSearchList" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="~/WebControl/HotelControl/HotelSearchControl.ascx" TagName="HotelSearchControl"
    TagPrefix="uc2" %>
<%@ Register Src="../WebControl/HotelControl/ImgFristControl.ascx" TagName="ImgFristControl"
    TagPrefix="uc3" %>
<%@ Register Src="../WebControl/HotelControl/CommonUserControl.ascx" TagName="CommonUserControl"
    TagPrefix="uc4" %>
<%@ Register Src="../WebControl/HotelControl/SpecialHotelControl.ascx" TagName="SpecialHotelControl"
    TagPrefix="uc5" %>
<%@ Register Src="../WebControl/HotelControl/ImgSecondControl.ascx" TagName="ImgSecondControl"
    TagPrefix="uc6" %>
<%@ Register Src="../WebControl/HotelControl/HotHotelControl.ascx" TagName="HotHotelControl"
    TagPrefix="uc7" %>
<%@ Register Src="../WebControl/HotelControl/ImgBannerControl.ascx" TagName="ImgBannerControl"
    TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="main">
        <uc8:ImgBannerControl ID="ImgBannerControl1" runat="server" />
        <!--content start-->
        <div class="content">
            <!--sidebar start-->
            <div class="sidebar sidebarSearch">
                <!--sidebar_1-->
                <uc2:HotelSearchControl ID="HotelSearchControl1" runat="server" />
                <!--sidebar_1 end-->
                <uc3:ImgFristControl ID="ImgFristControl1" runat="server" />
                <!-- sidebar_2 end-->
                <uc4:CommonUserControl ID="CommonUserControl1" runat="server" />
                <uc5:SpecialHotelControl ID="SpecialHotelControl1" runat="server" />
                <uc6:ImgSecondControl ID="ImgSecondControl1" runat="server" />
                <uc7:HotHotelControl ID="HotHotelControl1" runat="server" />
            </div>
            <!--sidebar02 start-->
            <div class="sidebar02 sidebar02Search">
                <div class="sidebar02_1">
                    <p class="xuanzhe">
                        <span>选择(<asp:Literal ID="litCity" runat="server"></asp:Literal><asp:Literal ID="litMoney"
                            runat="server"></asp:Literal>)酒店</span>
                        <img src="<%=ImageServerPath %>/images/hotel/liucheng.gif" /></p>
                    <!--sidebar02SearchC start-->
                    <div class="sidebar02SearchC">
                        <div class="xuanzhe_subtitle">
                            <ul>
                                <li><a name="t1">列表方式查看</a></li>
                                <li style="display: none"><a href="#">地图方式查看</a></li>
                            </ul>
                            <div class="clear">
                            </div>
                            <span><font class="C_Grb">入住日期：</font><font class="frb">
                                <asp:Label ID="lblInTime" runat="server" Text="" Width="85px"></asp:Label></font>
                                <font class="C_Grb">离店日期：</font><font class="frb"><asp:Label ID="lblLeaveTime" runat="server"
                                    Text="" Width="85px"></asp:Label></font> <font class="C_Grb">共</font><font class="frb">
                                        <asp:Label ID="lblDayCount" runat="server" Text="1" Width="10px"></asp:Label></font><font
                                            class="C_Grb">晚</font> </span>
                        </div>
                        <div class="xuanzhe_subtitle02">
                            <ul>
                                <li class="paixu">排序方式：</li>
                                <li style="width: 150px; height: 25px; *padding-top: 2px;">
                                    <asp:Localize ID="lclPriceSort" runat="server"></asp:Localize>
                                    &nbsp;
                                    <asp:Localize ID="lclStarSort" runat="server"></asp:Localize>
                                </li>
                            </ul>
                            <div class="clear">
                            </div>
                            <%--<span>（共<font class="frb">210</font>家酒店） <a href="#" class="page01">上一页</a> 1/14页 <a
                                href="#" class="page02">下一页</a></span>--%></div>
                        <div class="clear">
                        </div>
                        <div id="AjaxHotelList" style="width: 730px">
                            <div style='width: 100%; text-align: center;'>
                                <img src='<%=EyouSoft.Common.Domain.ServerComponents %>/images/loadingnew.gif' border='0'
                                    align='absmiddle' />&nbsp;正在查询酒店,请等待....&nbsp;</div>
                        </div>
                    </div>
                    <!--sidebar02SearchC end-->
                </div>
            </div>
            <!--sidebar02 end-->
        </div>
        <!--content end-->
        <div class="clear">
        </div>
    </div>
    <input type="hidden" value="" runat="server" id="hideUrl" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>

    <script type="text/javascript">
        function ShowHideRoom(num) {
            if ($("#div_list_" + num).css("display") == "none") {
                $("#div_list_" + num).slideDown("fast");
                $("#img_connn_" + num).attr("src", "<%=ImageServerPath %>/images/hotel/iconnn2.gif");
                $("#a_" + num).text("隐藏房型");
            } else {
                $("#div_list_" + num).slideUp("fast");
                $("#img_connn_" + num).attr("src", "<%=ImageServerPath %>/images/hotel/iconnn.gif");
                $("#a_" + num).text("全部房型");
            }
        }

        function setTab() {

        }

        function OpenYuDing(hCode, rCode, vCode, pCode) {
            var para = { hotelCode: hCode, roomCode: rCode, vendorCode: vCode, ratePlanCode: pCode, comeDate: "", leaveDate: "", CityId: "" };
            para.comeDate = $("#<%=lblInTime.ClientID%>").html();
            para.leaveDate = $("#<%=lblLeaveTime.ClientID%>").html();
            para.CityId = "<%=this.CityId %>"
            window.open("HotelBook.aspx?" + $.param(para));
        }
        function OpenMap(title, lat, lng) {
            if (window.XMLHttpRequest) {
                Boxy.iframeDialog({ title: "地图", iframeUrl: "/HotelManage/HotelMap.aspx?title=" + encodeURIComponent(title) + "&lat=" + lat + "&lng=" + lng, width: "600px", height: "400px", draggable: true, data: null, hideFade: true, modal: true });
            } else {
                //IE63
                Boxy.iframeDialog({ title: "地图", iframeUrl: "/HotelManage/HotelMap.aspx?title=" + encodeURIComponent(title) + "&lat=" + lat + "&lng=" + lng, width: "600px", height: "400px", draggable: true, data: null, hideFade: true, modal: true });

                // window.open("/HotelManage/HotelMap.aspx?title=" + title + "&lat=" + lat + "&lng=" + lng);
            }
        }

        function AjaxGetHotelList(pageIndex) {
            //AJAX 加载数据
            $("#AjaxHotelList").html("<div style='width:100%; text-align:center;'><img src='<%=EyouSoft.Common.Domain.ServerComponents %>/images/loadingnew.gif' border='0' align='absmiddle'/>&nbsp;正在查询酒店,请等待....&nbsp;</div>");

            var url = $("#<%=this.hideUrl.ClientID %>").val();
            $.ajax({
                type: "Get",
                url: "/HotelManage/AJAXHotelSearchList.aspx" + url + "&Page=" + pageIndex + "&returnUrl=" + encodeURIComponent("<%=Request.Url.ToString() %>"),
                cache: false,
                success: function(result) {
                    $("#AjaxHotelList").html(result);

                    $("#div_AjaxPage a").click(function() {
                        window.location.href = "#t1";
                        var str = $(this).attr("href").match(/&[^&]+$/);
                        pageIndex = str.toString().replace("&Page=", "");

                        AjaxGetHotelList(pageIndex);
                        return false;
                    });
                }
            });

        }

        $(function() {
            $(".font14b a").click(function() {
                var comeDate = $("#<%=lblInTime.ClientID%>").html();
                var leaveDate = $("#<%=lblLeaveTime.ClientID%>").html();
                var url = $(this).attr("href") + "&comeDate=" + comeDate + "&leaveDate=" + leaveDate + "&CityId=<%=this.CityId %>";
                window.location.href = url;
                return false;
            });
            $(".C_red a").click(function() {
                var comeDate = $("#<%=lblInTime.ClientID%>").html();
                var leaveDate = $("#<%=lblLeaveTime.ClientID%>").html();
                var url = $(this).attr("href") + "&comeDate=" + comeDate + "&leaveDate=" + leaveDate + "&CityId=<%=this.CityId %>";
                window.location.href = url;
                return false;
            });

            //加载酒店列表
            AjaxGetHotelList();
        })
        
    </script>

</asp:Content>
