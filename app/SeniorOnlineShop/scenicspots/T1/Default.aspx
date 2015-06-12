<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SeniorOnlineShop.scenicspots.T1.Default"
    MasterPageFile="~/master/ScenicSpotsT1.Master" Title="高级网店" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/master/ScenicSpotsT1.Master" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ID="HeadPlaceHolder" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content runat="server" ID="MainPlaceHolder" ContentPlaceHolderID="MainPlaceHolder">

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ScenicSpots.T1.defaultslider.js") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery.floating.js") %>"></script>

    <script type="text/javascript">
        var books = {
            cType: 0,
            getBook: function(bookType, obj) {
                if (this.cType == bookType) return;
                this.cType = bookType;
                var parms = { cid: "<%=this.Master.CompanyId %>", bookType: bookType };
                var cacheName = "cache_books_" + parms.bookType;
                var cache = $("div").data(cacheName);

                if (obj != undefined && obj != 'undefined' && obj != null) {
                    $(obj).parent().parent().find("a").removeClass("tab-five-on");
                    $(obj).addClass("tab-five-on");
                }

                if (cache != undefined && cache != null && cache != 'undefined') {
                    $("#books").html(cache);
                    return;
                }
                $.ajax({
                    url: "/scenicspots/t1/DefaultBooks.ashx",
                    data: parms,
                    cache: true,
                    success: function(response) {
                        $("#books").html(response);
                        $("div").data(cacheName, response);


                    }
                });
            }
        };

        $(document).ready(function() {
            books.getBook("<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略 %>", null);
            slider.init(sliderData, "Slide");
            $("#divZX").easydrag();
            $("#divZX").floating({ position: "left", top: 100, left: 10, width: 400 });
        });
    </script>

    <div class="sidebar02">
        <!--sidebar02_1-->
        <div class="sidebar02_1">
            <div class="sidebar02_1_L">
                <div class="boxpadding">
                    <div id="Slide">
                    </div>
                    <div class="clearboth">
                    </div>
                </div>
            </div>
            <div class="sidebar02_1_R">
                <asp:Repeater runat="server" ID="rptScenic">
                    <ItemTemplate>
                        <h1>
                            <font class="C_orange">
                                <%# Utils.GetText2(Eval("ScenicName") == null ? string.Empty : Eval("ScenicName").ToString(), 12, true)%></font></h1>
                        <p class="Scenic_spot">
                            <%# Utils.GetText2(Eval("Description") == null ? string.Empty : Utils.LoseHtml(Eval("Description").ToString()), 70, true)%><a
                                href="<%# Utils.GenerateShopPageUrl2("/ScenicTickets_" + Eval("Id"),Master.CompanyId) %>"><font
                                    class="C_orange">详情>></font></a></p>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="clearboth">
            </div>
        </div>
        <!--sidebar02_2-->
        <div class="sidebar02_2">
            <ul class="subnav">
                <li><a href="javascript:void(0)" class="tab-five-on" onmouseover="books.getBook(<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略 %>,this)">
                    攻略</a></li>
                <li><a href="javascript:void(0)" onmouseover="books.getBook(<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区美食 %>,this)">
                    美食</a></li>
                <li><a href="javascript:void(0)" onmouseover="books.getBook(<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿 %>,this)">
                    住宿</a></li>
                <li><a href="javascript:void(0)" onmouseover="books.getBook(<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区交通 %>,this)">
                    交通</a></li>
                <li><a href="javascript:void(0)" onmouseover="books.getBook(<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区购物 %>,this)">
                    购物</a></li>
                <div class="clearboth">
                </div>
            </ul>
            <div class="clearboth">
            </div>
            <div class="sidebar02_2Content" id="books">
                <!--攻略区域-->
            </div>
        </div>
        <asp:Literal runat="server" ID="ltrBannerAd"></asp:Literal>
        <div class="sidebar02_2">
            <p class="more more02">
                <span style="display: none;">景区</span><a href="" runat="server" id="lnkJQMT"> >> 更多...</a></p>
            <ul class="ScenicPhoto">
                <cc1:CustomRepeater ID="rptJQMT" runat="server" EmptyText="<li class='empty c999'>暂无景区美图</li>">
                    <ItemTemplate>
                        <li>
                            <div class="pic">
                                <a href="<%#Utils.GenerateShopPageUrl2("/scenicspots_"+Eval("ImgId"), this.Master.CompanyId) %>"
                                    title="<%# Eval("Description") %>">
                                    <img src="<%#Domain.FileSystem+Eval("ThumbAddress") %>" alt="<%#Eval("Description") %>" /></a>
                            </div>
                            <div class="title" title="<%# Eval("Description") %>">
                                <a href="<%#Utils.GenerateShopPageUrl2("/scenicspots_"+Eval("ImgId"), this.Master.CompanyId) %>"
                                    title="<%# Eval("Description") %>">
                                    <%#Utils.GetText2(Eval("Description").ToString(), 5, true)%></a></div>
                        </li>
                    </ItemTemplate>
                </cc1:CustomRepeater>
                <div class="clearboth">
                </div>
            </ul>
        </div>
    </div>
    <%--浮动咨询开始--%>
    <div id="divZX" style="display: none; z-index: 99999;">
        <table height="140" cellspacing="0" cellpadding="0" border="0" background="<%= Domain.ServerComponents %>/images/seniorshop/zixunbg.gif"
            width="400">
            <tbody>
                <tr>
                    <td height="5" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left" valign="top" colspan="2">
                        &nbsp;&nbsp;您好，<asp:Label runat="server" ID="lbCompanyName"></asp:Label>竭诚为您服务
                    </td>
                </tr>
                <tr>
                    <td valign="middle" colspan="2">
                        <asp:Label runat="server" Text="欢迎您,有什么可以帮助您的吗？" ID="lbGuestInfo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <a href="/OlServer/Default.aspx?cid=<%= this.Master.CompanyId %>" target="blank">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/jieshou.gif"></a>
                    </td>
                    <td align="left">
                        <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/hulue.gif"
                            onclick="CloseLeft();" style="cursor: pointer;">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <%--浮动咨询结束--%>
</asp:Content>
