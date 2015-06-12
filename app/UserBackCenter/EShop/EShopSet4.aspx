<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EShopSet4.aspx.cs" Inherits="UserBackCenter.EShop.EShopSet4" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>高级网店模板4管理首页</title>
    <link href="<%=CssManage.GetCssFilePath("T4.m.shop.css")%>" rel="stylesheet" type="text/css" />
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

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("T4.m.dropdowntabs.js") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('.boxgrid.captionfull').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '100px' }, { queue: false, duration: 100 });
            });
            //
            $('.boxgrid2.infocfull').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '150px' }, { queue: false, duration: 100 });
            });
            $('.boxgrid3.infocfull3').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '36px' }, { queue: false, duration: 100 });
            });

            $('.boxgrid4.infocfull4').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '250px' }, { queue: false, duration: 100 });
            });
            $('.boxgrid5.infocfull5').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '122px' }, { queue: false, duration: 100 });
            });

        });
    </script>

    <script type="text/javascript">
        var getT = {
            //获取旅游线路
            getTours: function(tourSpreadState, areaId, obj) {
                var parms = { cid: "<%=this.CompanyId %>", tss: tourSpreadState, aid: areaId };
                var cacheName = "cache_tss" + parms.tss + "_aid" + parms.aid;
                var cache = $("#jQueryCache").data(cacheName);

                if (cache != undefined && cache != null && cache != 'undefined') {
                    $("#" + obj).html(cache);
                    return;
                }

                $.ajax({
                    url: "/EShop/T4Tours.aspx",
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

        function boxymethods(pageurl, strtitle) {
            Boxy.iframeDialog({ title: strtitle, iframeUrl: pageurl, width: 730, height: 350, draggable: true });
            return false;
        }
        function boxymethodssmall(pageurl, strtitle) {
            Boxy.iframeDialog({ title: strtitle, iframeUrl: pageurl, width: 650, height: 330, draggable: true });
            return false;
        }

        $(document).ready(function() {
            getT.getRTours(0);
            getT.getLTours(0);
            tabdropdown.init("bluemenu");
            $('#boxy1').click(function() { return boxymethodssmall("CompanyProfile.aspx", "修改公司档案"); });
            $('#boxy2').click(function() { return boxymethodssmall("SetCard.aspx", "上传名片"); });
            $('#boxy3').click(function() { return boxymethods("SetNews.aspx", "最新旅游动态管理"); });
            $('#boxy4').click(function() { return boxymethods("SetFriendLinkManage.aspx", "友情链接管理"); });
            $('#boxy5').click(function() { return boxymethods("RotationPicManage.aspx", "上传轮换图片"); });
            $('.boxy6').click(function() { return boxymethods("SetTravelGuidList.aspx?GuideType=0", "出游指南管理"); });
            $('#boxy7').click(function() { return boxymethods("SetTravelGuid.aspx?GuideType=0", "出游指南管理"); });
            $('#boxy8').click(function() { return boxymethods("SetCopyRight.aspx", "修改版权"); });
            $('#boxy9').click(function() { return boxymethods("TeamCustomizationManage.aspx", "团队定制"); });
            $('#boxy10').click(function() { return boxymethodssmall("SetLogoPic.aspx?type=t4", "设置高级网店头部图片"); });
            $('#<%=lnkGYWM1.ClientID %>').click(function() { return boxymethods("SetAboutUs.aspx", "关于我们"); });

            $(".tab_add_tours").click(function() { parent.topTab.open("/routeagency/routemanage/routeview.aspx", "我的线路库", { isRefresh: false }); return false; });
            $(".tab_m_tours").click(function() { parent.topTab.open("/routeagency/scatteredfightplan.aspx", "我的散拼计划", { isRefresh: false }); return false; });

            $("#txtStartDate").focus(function() {
                WdatePicker({
                    onpicked: function() {
                        $("#txtEndDate")[0].focus();
                    }
                });
            });
            $("#txtStartDate,#txtEndDate").focus(function() {
                WdatePicker();
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <!--header-->
    <div id="top">
        <div class="topda">
            <li class="daleft">
                <img src="<%=ImageServerUrl %>/T4/m/images/logo_114.gif" width="68" height="20" /></li>
            <li class="daright"><a href="javascript:void(0)">同业114线路 </a>| <a href="javascript:void(0)">
                机票预订</a> | <a href="javascript:void(0)">同业互动</a> | <a href="javascript:void(0)">同业MQ下载</a>
                | <a href="javascript:void(0)">我要提意见</a></li>
        </div>
    </div>
    <div class="boxgrid captionfull">
        <asp:PlaceHolder runat="server" ID="phDefaultHead" Visible="false">
            <div class="topbanner" style="<%=STYHeadBanner%>">
                <div class="logoimg">
                    <a href="index.html">
                        <img src="" width="160" height="52" border="0" runat="server" id="imgHeadLogo" alt="" /></a></div>
                <div class="name">
                    <ul>
                        <li class="name-top">
                            <asp:Literal runat="server" ID="ltrHeadLogoName"></asp:Literal>
                        </li>
                        <li class="name-id">&nbsp;</li>
                    </ul>
                </div>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phPersonalizeHead" Visible="false">
            <div class="topbanner_personalize">
                <asp:Literal runat="server" ID="ltrPersonalizeHead"></asp:Literal>
            </div>
        </asp:PlaceHolder>
        <div class="cover boxcaption">
            <a href="javascript:void(0)" id="boxy10">
                <h3 style="height: 100px; padding-top: 80px; cursor: pointer">
                    点击可上传log图片</h3>
            </a>
        </div>
    </div>
    <div id="bluemenu" class="nav">
        <ul>
            <li><a href="javascript:void(0)" class="nav-on">首 页</a></li>
            <li><a href="javascript:void(0)" rel="dropmenu1_b">
                <img src="<%=ImageServerUrl %>/T4/m/images/xxit.gif" alt="点击此图标维护本项信息！" />散拼计划</a></li>
            <li><a href="javascript:void(0)" id="boxy9">
                <img src="<%=ImageServerUrl %>/T4/m/images/xxit.gif" alt="点击此图标维护本项信息！" />团队定制</a></li>
            <li><a href="javascript:void(0)" class="boxy6" title="点击此图标维护本项信息！">
                <img src="<%=ImageServerUrl %>/T4/m/images/xxit.gif" alt="点击此图标维护本项信息！" />出游指南</a></li>
            <!--<li><a href="javascript:void(0)">机票预定</a></li>
            <li><a href="javascript:void(0)">酒店预定</a></li> -->
        </ul>
    </div>
    <!--1st drop down menu -->
    <div id="dropmenu1_b" class="dropmenudiv_b" style="width: 100px;">
        <a href="javascript:void(0)" class="tab_add_tours">我的线路库</a> <a href="javascript:void(0)"
            class="tab_m_tours">我的散拼计划</a></div>
    <!--header end-->
    <!--mainbox-->
    <div class="mainbox">
        <!--left-->
        <div class="left">
            <div class="leftn">
                <div class="neileftk">
                    <div class="neilefth">
                        公司档案</div>
                    <div class="neileftxx">
                        <div class="neilist">
                            <div class="boxgrid4 infocfull4">
                                <p>
                                    品牌名称：</p>
                                <p>
                                    <span>
                                        <asp:Literal runat="server" ID="ltrCompanyBrandName"></asp:Literal>
                                    </span>
                                </p>
                                <p>
                                    联系人：<asp:Literal runat="server" ID="ltrContactName"></asp:Literal>
                                </p>
                                <p>
                                    手机：<asp:Literal runat="server" ID="ltrContactMobile"></asp:Literal></p>
                                <p>
                                    电话：<asp:Literal runat="server" ID="ltrContactTelephone"></asp:Literal>
                                </p>
                                <p>
                                    传真：<asp:Literal runat="server" ID="ltrContactFax"></asp:Literal></p>
                                <p>
                                    地址：<asp:Literal runat="server" ID="ltrContactAddress"></asp:Literal></p>
                                <div class="cover boxcaption4">
                                    <a href="javascript:void(0)" id="boxy1">
                                        <h3 style="height: 100px; padding-top: 80px; cursor: pointer; text-align: center;">
                                            点击添加公司档案</h3>
                                    </a>
                                </div>
                            </div>
                            <p>
                                名片：</p>
                            <div class="boxgrid5 infocfull5">
                                <p>
                                    <img src="" width="237" height="132" runat="server" id="imgCompanyCard" /></p>
                                <div class="cover boxcaption5">
                                    <a href="javascript:void(0)" id="boxy2">
                                        <h3 style="height: 100px; padding-top: 80px; cursor: pointer; text-align: center;">
                                            点击上传名片</h3>
                                    </a>
                                </div>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="login">
                <div class="login-left">
                    用户名：<input name="textfield" type="text" size="20" /><br />
                    密&nbsp;&nbsp;&nbsp; 码：<input name="textfield2" type="text" size="20" /><br />
                    <%--验证码：<img src="<%=ImageServerUrl %>/T4/m/images/yanzheng.gif" width="60" height="25" />--%>
                </div>
                <div class="login-right">
                    <input type="submit" name="Submit" value="提交" /></div>
                <div style="clear: both;">
                </div>
                <div class="deng">
                    <a href="javascript:void(0)">组团社(零售商)注册</a></div>
            </div>
            <div class="leftn">
                <div class="neileftk">
                    <div class="neilefth">
                        查询接口</div>
                    <div class="neileftxx">
                        <div id="queryhead">
                            <ul id="querymenu">
                                <li class="sec2">线路查询</li>
                                <%--<li class="sec1">机票查询</li>
                                <li class="sec1">酒店查询</li>--%>
                            </ul>
                            <ul id="main">
                                <li class="block">
                                    <div class="query">
                                        <table class="lquery-left">
                                            <tr>
                                                <td height="27">
                                                    线路名称：
                                                </td>
                                                <td>
                                                    <input name="textfield" type="text" size="20">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="27">
                                                    天 数：
                                                </td>
                                                <td>
                                                    <input name="textfield22" type="text" size="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="27">
                                                    出发时间：
                                                </td>
                                                <td>
                                                    <input name="txtStartDate" id="txtStartDate" class="borderlv" type="text" size="4"
                                                        style="width: 57px" />至<input name="txtEndDate" id="txtEndDate" class="borderlv"
                                                            type="text" size="4" style="width: 57px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="27">
                                                </td>
                                                <td>
                                                    <div class="query-right">
                                                        <input type="button" name="Submit" value="查询" /></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </li>
                                <li class="unblock">第二个内容</li>
                                <li class="unblock">第三个内容</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="leftn">
                <div class="neileftk">
                    <div class="neilefth">
                        常用工具</div>
                    <div class="neileftxx">
                        <table width="240" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td height="37" align="center">
                                    <a href="javascript:void(0)" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_03.gif" width="105" height="26" border="0" /></a>
                                </td>
                                <td height="37" align="center">
                                    <a href="http://site.baidu.com/list/wannianli.htm" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_05.gif" width="105" height="26" border="0" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td height="37" align="center">
                                    <a href="javascript:void(0)" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_09.gif" width="105" height="26" border="0" /></a>
                                </td>
                                <td height="37" align="center">
                                    <a href="http://www.ip138.com/post/" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_10.gif" width="105" height="26" border="0" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td height="37" align="center">
                                    <a href="http://qq.ip138.com/train/" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_13.gif" width="105" height="26" border="0" /></a>
                                </td>
                                <td height="37" align="center">
                                    <a href="http://qq.ip138.com/idsearch/" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_14.gif" width="105" height="26" border="0" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td height="37" align="center">
                                    <a href="http://weather.tq121.com.cn/" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_17.gif" width="105" height="26" border="0" /></a>
                                </td>
                                <td height="37" align="center">
                                    <a href="http://www.ip138.com/sj/index.htm" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_18.gif" width="105" height="26" border="0" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td height="37" align="center">
                                    <a href="http://www.hao123.com/haoserver/kuaidi.htm" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_21.gif" width="105" height="26" border="0" /></a>
                                </td>
                                <td height="37" align="center">
                                    <a href="http://www.ip138.com/ips8.asp" target="_blank">
                                        <img src="<%=ImageServerUrl %>/T4/m/images/tool_22.gif" width="105" height="26" border="0" /></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="leftn">
                <div class="neileftk">
                    <div class="neilefth">
                        最新旅游动态</div>
                    <div class="neileftxx">
                        <div class="boxgrid6 infocfull6">
                            <div class="newslist">
                                <ul>
                                    <cc1:CustomRepeater ID="rptNews" runat="server" EmptyText="<li>暂无最新旅游动态</li>">
                                        <ItemTemplate>
                                            <li>·<a href="javascript:void(0)"><%#Utils.GetText(Eval("Title").ToString(),18,true)%></a></li>
                                        </ItemTemplate>
                                    </cc1:CustomRepeater>
                                </ul>
                            </div>
                            <div class="cover boxcaption6">
                                <a href="javascript:void(0)" id="boxy3">
                                    <h3 style="height: 100px; padding-top: 80px; cursor: pointer; text-align: center;">
                                        点击上传旅游动态</h3>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="width: 265px;">
                <iframe border="0" name="play" marginwidth="0" marginheight="0" src="http://www.soso.com/tb.q"
                    frameborder="0" width="260" scrolling="no" height="195"></iframe>
            </div>
            <div style="height: 15px;">
            </div>
        </div>
        <!--left end-->
        <!--right-->
        <div class="right">
            <div class="fouse">
                <div class="boxgrid2 infocfull">

                    <script type="text/javascript">
                        var pic_width = 414; //图片宽度
                        var pic_height = 175; //图片高度
                        var button_pos = 4; //按扭位置 1左 2右 3上 4下
                        var stop_time = 3000; //图片停留时间(1000为1秒钟)
                        var show_text = 1; //是否显示文字标签 1显示 0不显示
                        var txtcolor = "000000"; //文字色
                        var bgcolor = "FFFFFF"; //背景色
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
                        document.write('<param name="movie" value="<%=ImageServerUrl %>/T4/m/images/focus.swf">');
                        document.write('<param name="quality" value="high"><param name="wmode" value="transparent">');
                        document.write('<param name="FlashVars" value="pics=' + pics + '&links=' + links + '&texts=' + texts + '&pic_width=' + pic_width + '&pic_height=' + pic_height + '&show_text=' + show_text + '&txtcolor=' + txtcolor + '&bgcolor=' + bgcolor + '&button_pos=' + button_pos + '&stop_time=' + stop_time + '">');
                        document.write('<embed wmode="transparent" src="<%=ImageServerUrl %>/T4/m/images/focus.swf" FlashVars="pics=' + pics + '&links=' + links + '&texts=' + texts + '&pic_width=' + pic_width + '&pic_height=' + pic_height + '&show_text=' + show_text + '&txtcolor=' + txtcolor + '&bgcolor=' + bgcolor + '&button_pos=' + button_pos + '&stop_time=' + stop_time + '" quality="high" width="' + pic_width + '" height="' + swf_height + '" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />');
                        document.write('</object>');
                    </script>

                    <div class="cover boxcaption2">
                        <a href="javascript:void(0)" id="boxy5">
                            <h3 style="height: 100px; padding-top: 80px; cursor: pointer; text-align: center;">
                                点击可上传轮换图片息</h3>
                        </a>
                    </div>
                </div>
            </div>
            <!--cuxiao line-->
            <div class="linecx">
                <div class="linecxbar">
                    <span>特价旅游线路</span><a href="javascript:void(0)">更多>></a></div>
                <ul>
                    <cc1:CustomRepeater ID="rptPromotionTours" runat="server" EmptyText="<li>暂无特价旅游线路</li>">
                        <ItemTemplate>
                            <li>·<a href="javascript:void(0)" class="huizi"><%#Utils.GetText(Eval("RouteName").ToString(), 18, true)%></a></li>
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
                        推荐旅游线路 <a href="javascript:void(0)" class="more">查看全部旅游线路</a> <a href="javascript:void(0)"
                            class="more tab_m_tours"><strong>【我的散拼计划】</strong></a><a href="javascript:void(0)"
                                class="more tab_add_tours"><strong>【我的线路库】</strong></a></div>
                    <div class="linetjxx">
                        <div class="linemenu">
                            <ul style="text-align: left">
                                <li class="areatabactive"><a href="javascript:void(0)">全部</a></li>
                                <asp:Repeater runat="server" ID="rptAreas">
                                    <ItemTemplate>
                                        <li><a href="javascript:void(0)">
                                            <nobr><%#Eval("AreaName") %></nobr>
                                        </a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div id="RecommendTours" class="defaulttours">
                        </div>
                    </div>
                </div>
            </div>
            <!--tuijian line end-->
            <!--jinqi line-->
            <div class="linetj">
                <div class="linetjtk">
                    <div class="linetjth">
                        近期旅游线路 <a href="javascript:void(0)" class="more">查看全部旅游线路</a></div>
                    <div class="linetjxx">
                        <div class="linemenu">
                            <ul style="text-align: left">
                                <li class="areatabactive"><a href="javascript:void(0)">全部</a></li>
                                <asp:Repeater runat="server" ID="rtpAreas1">
                                    <ItemTemplate>
                                        <li><a href="javascript:void(0)">
                                            <nobr><%#Eval("AreaName") %></nobr>
                                        </a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div id="LatestTours" class="defaulttours">
                        </div>
                    </div>
                </div>
            </div>
            <!--jinqi line end-->
        </div>
        <!--right end-->
        <!--zhinan-->
        <div class="zhinan">
            <div class="zhinantk">
                <div class="zhinanth">
                    出游指南 <a href="javascript:void(0)" class="more">更多</a> <a href="javascript:void(0)"
                        title="点击此图标维护本项信息！" class="more boxy6"><strong>【管理出游指南】</strong></a><a href="javascript:void(0)"
                            title="点击此图标维护本项信息！" class="more" id="boxy7"><strong>【添加出游指南】</strong></a></div>
                <div class="zhinanxx">
                    <table width="940" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 0px 0px 10px 10px;">
                        <tr>
                            <td width="235" align="left" valign="top">
                                <table width="212" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td height="30" class="zhinantt">
                                            <table width="180" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="86" align="center" bgcolor="#FFFFFF">
                                                        <strong><font color="#356902">风土人情介绍</font></strong>
                                                    </td>
                                                    <td width="54" align="center">
                                                        &nbsp;
                                                    </td>
                                                    <td width="40" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                        <a href="javascript:void(0)">更多</a>
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
                                        <td height="30" class="zhinantt">
                                            <table width="180" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="71" align="center" bgcolor="#FFFFFF">
                                                        <strong><font color="#356902">温馨提醒</font></strong>
                                                    </td>
                                                    <td width="69" align="center">
                                                        &nbsp;
                                                    </td>
                                                    <td width="40" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                        <a href="javascript:void(0)">更多</a>
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
                                        <td height="30" class="zhinantt">
                                            <table width="180" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="71" align="center" bgcolor="#FFFFFF">
                                                        <strong><font color="#356902">综合介绍</font></strong>
                                                    </td>
                                                    <td width="69" align="center">
                                                        &nbsp;
                                                    </td>
                                                    <td width="40" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                        <a href="javascript:void(0)">更多</a>
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
                                        <td height="30" class="zhinantt">
                                            <table width="180" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="86" align="center" bgcolor="#FFFFFF">
                                                        <strong><font color="#356902">旅游资源推荐</font></strong>
                                                    </td>
                                                    <td width="54" align="center">
                                                        &nbsp;
                                                    </td>
                                                    <td width="40" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                        <a href="javascript:void(0)">更多</a>
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
        <div class="clear">
        </div>
        <div class="boxgrid3 infocfull3">
            <div class="bottom">
                <div class="bottomtk">
                    <div class="bottomth">
                        友情链接
                    </div>
                    <div class="bottomxx">
                        <div class="firendlist">
                            <ul>
                                <asp:Repeater ID="rptLinks" runat="server">
                                    <ItemTemplate>
                                        <li><a target="_blank" href="<%#Eval("LinkAddress") %>">·<%#Eval("LinkName") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="cover boxcaption3">
                <a href="javascript:void(0)" id="boxy4">
                    <h3 style="height: 30px; padding-top: 10px; cursor: pointer; text-align: center;">
                        点击添加友情链接</h3>
                </a>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <table width="960px" border="0" cellspacing="0" cellpadding="0" style="margin: 0px auto;
        margin-top: 10px;">
        <tr>
            <td align="center" bgcolor="#95C55E" height="3">
            </td>
        </tr>
        <tr>
            <td height="97" align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0)" runat="server" id="lnkSY1">首 页</a> | <a href="javascript:void(0)"
                    runat="server" id="lnkSPJH1">散拼计划</a> | <a href="javascript:void(0)" runat="server"
                        id="lnkTDDZ1">团队定制</a> | <a href="javascript:void(0)" runat="server" id="lnkCYZN1">出游指南</a>
                |
                <!--<a href="javascript:void(0)" runat="server" id="lnkJPYD1">机票预定</a> | <a href="javascript:void(0)" runat="server" id="lnkJDYD1">酒店预定</a> | -->
                <a href="javascript:void(0)" runat="server" id="lnkGYWM1">
                    <img src="<%=ImageServerUrl %>/T4/m/images/xxit.gif" alt="点击此图标维护本项信息！" />关于我们</a>
                <br />
                <br />
                <div class="boxgrid3 infocfull3">
                    <asp:Literal ID="ltrCopyRight" runat="server"></asp:Literal><br />
                    <div class="cover boxcaption3">
                        <a href="javascript:void(0)" id="boxy8">
                            <h3 style="height: 30px; padding-top: 10px; cursor: pointer; text-align: center;">
                                点击修改版权信息</h3>
                        </a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
