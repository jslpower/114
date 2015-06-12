<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SeniorOnlineShop.template4.Default"
    MasterPageFile="~/master/T4.Master" Title="首页--专线网店" %>

<%@ MasterType VirtualPath="~/master/T4.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Model.TourStructure" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder" ID="PageHead">
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .areatabactive
        {
            color: #000;
            font-weight: bold;
        }
        .areatabactive a
        {
            color: #000;
        }
        .areatabactive a:hover
        {
            color: #000;
        }
    </style>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("TourCalendar") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("MouseFollow") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery.floating.js") %>"></script>
    <script type="text/javascript">
        //预定按钮调用的方法 模板团ID，点击对象
        function ClickCalendar(TourId, obj, AreaType) {
            SingleCalendar.config.isLogin = "<%=IsLogin %>"; //是否登陆
            SingleCalendar.config.stringPort = "<%= EyouSoft.Common.Domain.UserPublicCenter %>"; //配置
            SingleCalendar.initCalendar({
                currentDate: new Date(Date.parse('<%=string.Format("{0:yyyy\\/MM\\/dd}", DateTime.Today) %>')), //当时月
                firstMonthDate: new Date(Date.parse('<%=string.Format("{0:yyyy\\/MM\\/dd}", DateTime.Today) %>')), //当时月
                srcElement: obj,
                areatype: AreaType, //当前模板团线路区域类型 
                TourId: TourId, //模板团ID
                AddOrder: AddOrder
            });
        }
        
        function AddOrder(TourId) {
            if ("<%=IsLogin %>" == "True") {
                var strParms = { TourId: TourId };
                Boxy.iframeDialog({ title: "预定", iframeUrl: "/seniorshop/RouteOrder.aspx", width: "800", height: GetAddOrderHeight(), draggable: true, data: strParms });
            } else {
                window.location.href = '<%= EyouSoft.Common.Domain.UserPublicCenter %>/Register/Login.aspx?isShow=1&CityId=<%=CityId %>&returnurl=' + escape('<%= EyouSoft.Common.Domain.SeniorOnlineShop %><%=Request.ServerVariables["SCRIPT_NAME"]%>?<%=Request.QueryString%>');
            }
        }

        var getT = {
            //获取旅游线路
            getTours: function(tourSpreadState, areaId, obj) {
                var parms = { cid: "<%=this.Master.CompanyId %>", tss: tourSpreadState, aid: areaId };
                var cacheName = "cache_tss" + parms.tss + "_aid" + parms.aid;
                var cache = $("#jQueryCache").data(cacheName);
                if (cache != undefined && cache != null && cache != 'undefined') {
                    $("#" + obj).html(cache);
                    return;
                }
                $.ajax({
                    url: "/template4/defaulttours.aspx",
                    data: parms,
                    cache: true,
                    success: function(response) {
                        $("#" + obj).html(response);
                        $("div").data(cacheName, response);
                    }
                });
            },
            //获取最新旅游线路
            getLTours: function(areaId, obj) {
                this.getTours(0, areaId, "LatestTours");
                if (obj != null && obj != undefined) {
                    $("#LAreas li").removeClass();
                    $(obj).parent().addClass("areatabactive");
                }
            },
            //获取推荐旅游线路
            getRTours: function(areaId, obj) {
                this.getTours(1, areaId, "RecommendTours");
                if (obj != null && obj != undefined) {
                    $("#RAreas li").removeClass();
                    $(obj).parent().addClass("areatabactive");
                }
            }
        };

        $(document).ready(function() {
            getT.getRTours(0);
            getT.getLTours(0);
            $("#divZX").easydrag();
            $("#divZX").floating({ position: "left", top: 100, left: 10, width: 400 });
        });
        
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainPlaceHolder" ID="PageMain">
    <div id="jQueryCache" style="display: none">
    </div>
    <div class="fouse">

        <script type="text/javascript">
            var pic_width = 414;  //图片宽度
            var pic_height = 175;  //图片高度
            var button_pos = 4;  //按扭位置 1左 2右 3上 4下
            var stop_time = 3000;  //图片停留时间(1000为1秒钟)
            var show_text = 1;  //是否显示文字标签 1显示 0不显示
            var txtcolor = "000000";  //文字色
            var bgcolor = "FFFFFF";  //背景色
            var imag = new Array();
            var link = new Array();
            var text = new Array();
            <%=RollJs %>
            var swf_height = show_text == 1 ? pic_height + 20 : pic_height;
            var pics = "", links = "", texts = "";
            for (var i = 1; i < imag.length; i++) {
                pics = pics + ("|" + imag[i]);
                links = links + ("|" + link[i]);
                texts = texts + ("|" + text[i]);
            }
            pics = pics.substring(1);
            links = links.substring(1);
            texts = texts.substring(1);
            document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cabversion=6,0,0,0" width="' + pic_width + '" height="' + swf_height + '">');
            document.write('<param name="movie" value="<%=ImageServerUrl %>/T4/images/focus.swf">');
            document.write('<param name="quality" value="high"><param name="wmode" value="opaque">');
            document.write('<param name="FlashVars" value="pics=' + pics + '&links=' + links + '&texts=' + texts + '&pic_width=' + pic_width + '&pic_height=' + pic_height + '&show_text=' + show_text + '&txtcolor=' + txtcolor + '&bgcolor=' + bgcolor + '&button_pos=' + button_pos + '&stop_time=' + stop_time + '">');
            document.write('<embed src="<%=ImageServerUrl %>/T4/images/focus.swf" FlashVars="pics=' + pics + '&links=' + links + '&texts=' + texts + '&pic_width=' + pic_width + '&pic_height=' + pic_height + '&show_text=' + show_text + '&txtcolor=' + txtcolor + '&bgcolor=' + bgcolor + '&button_pos=' + button_pos + '&stop_time=' + stop_time + '" quality="high" width="' + pic_width + '" height="' + swf_height + '" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />');
            document.write('</object>');
        </script>
    </div>
    <!--cuxiao line-->
    <div class="linecx">
        <div class="linecxbar">
            <span>特价旅游线路</span></div>
        <ul>
            <cc1:CustomRepeater ID="rptPromotionTours" runat="server" EmptyText="<li>暂无特价旅游线路</li>">
                <ItemTemplate>
                    <li>·<a href="<%# GetRouteDetailUrl(Eval("RouteId").ToString()) %>"
                        class="huizi"><%#Utils.GetText(Eval("RouteName").ToString(), 18, true)%></a></li>
                </ItemTemplate>
            </cc1:CustomRepeater>
        </ul>
    </div>
    <div class="clear">
    </div>
    <!--cuxiao line end-->
    <!--tuijian line-->
    <div class="linetj">
        <div class="linetjtk">
            <div class="linetjth">
                推荐旅游线路 
                <a href="<%=Utils.GenerateShopPageUrl2("/TourList3",this.Master.CompanyId) + "_" + Convert.ToString((int)EyouSoft.Model.NewTourStructure.RecommendType.推荐) %>"
                    class="more">查看全部旅游线路</a></div>
            <div class="linetjxx">
                <div class="linemenu">
                    <ul style="text-align: left" id="RAreas">
                        <li class="areatabactive"><a href="javascript:void(0)" onclick="javascript:getT.getRTours(0,this)">
                            全部</a></li>
                        <asp:Repeater runat="server" ID="rptAreas">
                            <ItemTemplate>
                                <li><a href="javascript:void(0)" onclick="javascript:getT.getRTours(<%#Eval("AreaId") %>,this)">
                                    <nobr><%#Eval("AreaName") %></nobr>
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div id="RecommendTours" class="defaulttours">
                    <!--推荐旅游线路-->
                </div>
            </div>
        </div>
    </div>
    <!--tuijian line end-->
    <!--jinqi line-->
    <div class="linetj">
        <div class="linetjtk">
            <div class="linetjth">
                近期旅游线路 <a href="<%=Utils.GenerateShopPageUrl2("/TourList3",this.Master.CompanyId) + "_" + Convert.ToString((int)EyouSoft.Model.NewTourStructure.RecommendType.无) %>"
                    class="more">查看全部旅游线路</a></div>
            <div class="linetjxx">
                <div class="linemenu">
                    <ul style="text-align: left" id="LAreas">
                        <li class="areatabactive"><a href="javascript:void(0)" onclick="javascript:getT.getLTours(0,this)">
                            全部</a></li>
                        <asp:Repeater runat="server" ID="rtpAreas1">
                            <ItemTemplate>
                                <li><a href="javascript:void(0)" onclick="javascript:getT.getLTours(<%#Eval("AreaId") %>,this)">
                                    <nobr><%#Eval("AreaName") %></nobr>
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div id="LatestTours" class="defaulttours">
                    <!--近期旅游线路-->
                </div>
            </div>
        </div>
    </div>
    <!--jinqi line end-->
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="GuidebookPlaceHolder" ID="PageGuidebook">
    <!--zhinan-->
    <div class="zhinan">
        <div class="zhinantk">
            <div class="zhinanth">
                出游指南 <a href="<%=Utils.GenerateShopPageUrl2("/GuideBooks",this.Master.CompanyId) %>"
                    class="more">更多</a></div>
            <div class="zhinanxx">
                <table width="940" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 0px 0px 10px 10px;">
                    <tr>
                        <td width="235" align="left" valign="top">
                            <table width="212" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="30" align="center" class="zhinantt">
                                        <table width="180" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="86" align="center" bgcolor="#FFFFFF">
                                                    <strong><font color="#356902">风土人情介绍</font></strong>
                                                </td>
                                                <td width="54" align="center">
                                                    &nbsp;
                                                </td>
                                                <td width="40" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                    <a href="<%=Utils.GenerateShopPageUrl2(string.Format("/GuideBookList_{0}",(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.风土人情),this.Master.CompanyId) %>">
                                                        更多</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <asp:Literal runat="server" ID="ltrFTRQ">
                                    <tr>
                                        <td>暂无相关内容！</td>
                                    </tr>
                                </asp:Literal>
                            </table>
                        </td>
                        <td width="235" valign="top">
                            <table width="212" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="30" align="center" class="zhinantt">
                                        <table width="180" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="71" align="center" bgcolor="#FFFFFF">
                                                    <strong><font color="#356902">温馨提醒</font></strong>
                                                </td>
                                                <td width="69" align="center">
                                                    &nbsp;
                                                </td>
                                                <td width="40" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                    <a href="<%=Utils.GenerateShopPageUrl2(string.Format("/GuideBookList_{0}",(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.温馨提醒),this.Master.CompanyId) %>">
                                                        更多</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <asp:Literal runat="server" ID="ltrWXTX">
                                    <tr>
                                        <td style="text-align:center">暂无相关内容！</td>
                                    </tr>
                                </asp:Literal>
                            </table>
                        </td>
                        <td width="235" valign="top">
                            <table width="212" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="30" align="center" class="zhinantt">
                                        <table width="180" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="71" align="center" bgcolor="#FFFFFF">
                                                    <strong><font color="#356902">综合介绍</font></strong>
                                                </td>
                                                <td width="69" align="center">
                                                    &nbsp;
                                                </td>
                                                <td width="40" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                    <a href="<%=Utils.GenerateShopPageUrl2(string.Format("/GuideBookList_{0}",(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍),this.Master.CompanyId) %>">
                                                        更多</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="90" valign="top">
                                        <table width="205" border="0" align="right" cellpadding="0" cellspacing="0">
                                            <asp:Literal runat="server" ID="ltrZHJS">
                                            <tr>
                                                <td height="19" style="text-align:center">
                                                    暂无相关内容
                                                </td>
                                            </tr>
                                            </asp:Literal>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="235" valign="top">
                            <table width="212" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="30" align="center" class="zhinantt">
                                        <table width="180" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="86" align="center" bgcolor="#FFFFFF">
                                                    <strong><font color="#356902">旅游资源推荐</font></strong>
                                                </td>
                                                <td width="54" align="center">
                                                    &nbsp;
                                                </td>
                                                <td width="40" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                    <a href="<%=Utils.GenerateShopPageUrl2(string.Format("/GuideBookList_{0}",(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.旅游资源推荐),this.Master.CompanyId) %>">
                                                        更多</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <asp:Literal runat="server" ID="ltrZYTJ">
                                    <tr>
                                        <td style="text-align:center">暂无相关内容！</td>
                                    </tr>
                                </asp:Literal>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
     <%--浮动咨询开始--%>
    <div id="divZX" style="display:none;z-index:99999;">
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
                        <a href="javascript:;" onclick="CloseLeft();">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/hulue.gif"></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <%--浮动咨询结束--%>
    <script type="text/javascript">
        $(document).ready(function() {
            $(".linecx ul").css("height","148px");
        });
    </script>
</asp:Content>