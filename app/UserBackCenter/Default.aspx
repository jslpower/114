<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserBackCenter.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/WebHeader.ascx" TagName="webHeader" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/WebFooter.ascx" TagName="webFooter" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/UserBackDefault.ascx" TagName="userbackdefault" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>旅行社后台_同业114</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <style type="text/css">
        .topbar, .divtopfull, .divmainfull, .margin10
        {
            position: absolute;
            top: -10000px;
            left: -10000px;
        }
        #top
        {
            background-image: url(<%=ImageServerPath %>/images/UserPublicCenter/sitebarbj.gif);
        }
        #top
        {
            background-repeat: repeat-x;
            height: 26px;
            text-align: left;
        }
        .topda
        {
            width: 970px;
            margin: 0 auto;
            padding: 0;
            padding-top: 5px;
        }
        .topda .daleft
        {
            width: 460px;
            float: left;
        }
        .topda .daright
        {
            width: 510px;
            float: left;
            text-align: right;
        }
        .topda .daright a
        {
            color: #333;
        }
        html
        {
            overflow-y: scroll;
            margin-right: 0;
            padding: 0;
        }
    </style>
</head>
<link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
<link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
<link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
<link href="<%=CssManage.GetCssFilePath("autocomplete") %>" rel="Stylesheet" type="text/css" />
<link href="<%=CssManage.GetCssFilePath("rightnew") %>" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("tipsy") %>"></script>

<script type="text/javascript">
    var commonTourModuleData = {
        _data: [],
        add: function(obj) {
            this._data[obj.ContainerID] = obj;
        },
        get: function(id) {
            return this._data[id];
        }
    };
</script>

<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" align="center" cellpadding="0" runat="server" id="tbIsCheck"
        visible="false" cellspacing="0" style="border: 2px solid #EF9739; background: #FFF8E2">
        <tr>
            <td style="padding: 5px;" align="center">
                <img src="<%=ImageServerPath %>/images/woring.gif" width="15" height="15" />&nbsp;
                当前您的帐号未审核，您暂时还不能发布线路、团队和预订！ 我们会在24小时内通知您审核结果！或请致电0571-56892810，QQ：1397604721。
            </td>
        </tr>
    </table>
    <div id="loadingItem" style="position: absolute; top: 8em; left: 2em">
        <br />
        <font size="+2">正在加载...</font><br />
    </div>
    <noscript>
        <style type="text/css">
            div
            {
                display: none;
            }
            table
            {
                display: none;
            }
            #noscript
            {
                padding: 3em;
                font-size: 130%;
            }
        </style>
        <p id="noscript">
            要使用当前网站平台，必须启用 JavaScript。不过，JavaScript 似乎已被禁用或者您的浏览器不支持 JavaScript。要使用网站平台，请更改您的浏览器选项，启用
            JavaScript，然后 <a href="/Default.aspx">重试</a>。</p>
    </noscript>
    <div id="topmessage" class="topmessage">
        <span>正在载入...</span></div>
    <cc1:webHeader ID="header" runat="server" />
    <div class="divtopfull">
        <div class="logo">
            <img id="imgLogo" runat="server" src="<%=ImageServerPath %>/images/logo.gif" width="130"
                height="50" /></div>
        <div class="headright">
            <span>
                <asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal></span>
            <div class="option" id="optionTab">
                <div class="option-tab-on-index">
                    <span><a href="javascript:;">首页</a></span></div>
            </div>
        </div>
    </div>
    <!--主体-->
    <div class="divmainfull">
        <div class="leftmenu">
            <div class="topopeara">
                <b>操作台</b>
            </div>
            <div id="divFirstMenu">
            </div>
            <div id="div_TourAgency" runat="server" visible="false">
                <div class="bigclassbar2" style="cursor: pointer;">
                    <span>组团服务</span><a href="javascript:void(0)" class="barmove" title="点击移动" style="cursor: pointer;"
                        onclick="DefaultFunction.ChangeFirstMenu(this,1);return false;">移动</a></div>
                <div class="gongnengxx2" style="display: none;">
                </div>
                <div class="gongneng2" style="display: none;">
                    <div id="divDirectoryset" runat="server">
                        <a href="/teamservice/scatterplanc.aspx" class="gongnengej" rel="toptab">国内散拼计划</a>
                        <a href="/teamservice/scatterplani.aspx" class="gongnengej" rel="toptab">国际散拼计划</a>
                        <a href="/teamservice/scatterplanp.aspx" class="gongnengej" rel="toptab">周边散拼计划</a>
                        <a href="/teamservice/linelibrarylist.aspx" class="gongnengej" rel="toptab">旅游线路库</a>
                        <a href="/teamservice/collect.aspx" class="gongnengej" rel="toptab">我的收藏</a>
                        <%--<a href="/teamservice/directoryset.aspx" class="gongnengej" rel="toptab">我的收藏</a>这个先不要删--%>
                        <a href="/teamservice/fitorders.aspx" class="gongnengej" rel="toptab">我的散客订单</a>
                        <a href="/teamservice/teamorders.aspx?ref=0" class="gongnengej" rel="toptab">团队订单管理</a>
                        <a href="/teamservice/orderstatistics.aspx" class="gongnengej" rel="toptab">散拼订单统计</a>
                    </div>
                    <div style="height: 7px; clear: both">
                    </div>
                </div>
            </div>
            <div id="div_RouteAgency" runat="server" visible="false">
                <div class="bigclassbar2" style="cursor: pointer;">
                    <span>专线服务</span><a href="javascript:void(0)" class="barmove" title="点击移动" style="cursor: pointer;"
                        onclick="DefaultFunction.ChangeFirstMenu(this,0);return false;">移动</a></div>
                <div class="gongnengxx2" style="display: none;">
                </div>
                <div class="gongneng2" style="display: none;">
                    <a href="/routeagency/routemanage/rmdefault.aspx" class="gongnengej" rel="toptab">添加线路</a>
                    <a href="/routeagency/routemanage/routeview.aspx" class="gongnengej" rel="toptab">我的线路库</a>
                    <a href="/routeagency/scatteredfightplan.aspx" class="gongnengej" rel="toptab">我的散拼计划</a>
                    <a href="/routeagency/historyteam.aspx" rel="toptab" class="gongnengej">我的历史团队</a>
                    <a href="/order/routeagency/neworders.aspx" rel="toptab" class="gongnengej">最新散客订单</a>
                    <a href="/routeagency/allfitorders.aspx" class="gongnengej" rel="toptab">所有散客订单</a>
                    <a href="/teamservice/teamorders.aspx?routeSource=1" class="gongnengej" rel="toptab">
                        团队订单管理</a> <a href="/routeagency/OrderStatistics.aspx" class="gongnengej" rel="toptab">
                            散拼订单统计</a>
                    <div style="height: 7px; clear: both">
                    </div>
                </div>
            </div>
            <!-----景区----->
            <div id="div_SightAgency" runat="server" visible="false">
                <div class="bigclassbar2">
                    <span>景区门票</span><a href="javascript:void(0)" class="barmove" title="点击移动" style="cursor: pointer;"
                        onclick="DefaultFunction.ChangeFirstMenu(this,3);return false;">移动</a></div>
                <div class="gongnengxx2" style="display: none;">
                </div>
                <div class="gongneng2" style="display: none;">
                    <a href="/ScenicManage/MyScenice.aspx" class="gongnengxz" rel="toptab">我的景区</a>
                    <a runat="server" id="hrefSightShop" class="commbar" rel="toptab">我的网店</a>
                    <div style="height: 7px; clear: both">
                    </div>
                </div>
            </div>
            <!-----宾馆酒店----->
            <div id="div_HotelAgency" runat="server" visible="false">
                <div class="bigclassbar2">
                    <span>宾馆酒店</span><a href="javascript:void(0)" class="barmove" title="拖拽移动">移动</a></div>
                <div class="gongnengxx2">
                </div>
                <div class="gongneng2">
                    <a href="#" class="gongnengxz">我的酒店</a> <a href="#" class="gongnengwc">酒店订单</a>
                    <a href="#" class="gongnengyc">住店审核</a>
                    <div style="height: 7px; clear: both">
                    </div>
                </div>
            </div>
            <div id="div_LocalAgency" runat="server" visible="false">
                <div class="bigclassbar2" style="cursor: pointer;">
                    <span>地接服务</span><a href="javascript:void(0)" class="barmove" title="点击移动" style="cursor: pointer;"
                        onclick="DefaultFunction.ChangeFirstMenu(this,2);return false;">移动</a></div>
                <div class="gongnengxx2" style="display: none;">
                </div>
                <div class="gongneng2" style="display: none;">
                    <div id="divAddLocalRoute" runat="server">
                        <a href="/routeagency/routemanage/rmdefault.aspx?RouteSource=2" class="gongnengej"
                            rel="toptab">添加线路</a> <a href="/routeagency/routemanage/routeview.aspx?routeSource=2"
                                class="gongnengej" rel="toptab">我的线路库</a>
                    </div>
                    <a href="/teamservice/teamorders.aspx?routeSource=2" class="gongnengej" rel="toptab">
                        团队订单管理</a>
                    <div style="height: 7px; clear: both">
                    </div>
                </div>
            </div>
            <div style="clear: both;">
            </div>
            <div id="divHotelCenter" runat="server" visible="false">
                <div class="bigclassbar" id="div1">
                    <a href="javascript:void(0);">酒店订单管理</a>
                </div>
                <div class="gongnengxx" style="display: none;">
                </div>
                <div class="gongneng" style="display: none;">
                    <a href="/hotelcenter/hotelordermanage/HotelOrderList.aspx?IsFirstKey=1" id="url_orderSearch"
                        class="gongnengej" rel="toptab">订单查询</a> <a href="/hotelcenter/hotelordermanage/hotelvisitormanage.aspx"
                            class="gongnengej" rel="toptab">常旅客管理</a> <a href="/hotelcenter/hotelordermanage/SettlementAccount.aspx"
                                class="gongnengej" rel="toptab">结算账户设置</a> <a href="/hotelcenter/hotelordermanage/teamonlinesubmit.aspx"
                                    class="gongnengej" rel="toptab">港澳/团队申请 </a>
                </div>
            </div>
            <!--xcl 2011-12-14  开始-->
            <!--我的网店开始-->
            <div id="divMyShop" runat="server">
                <div style="clear: both;">
                </div>
                <div class="gongnengzu">
                    <a href="javascript:void(0);" class="bigclassbar">我的网店</a></div>
                <div class="gongnengxx" style="display: none">
                </div>
                <div class="gongneng" style="display: none">
                    <a href="/generalshop/geneshopmainpage.aspx" class="gongnengej" rel="toptab" id="simpleShop"
                        runat="server">普通网店模板</a> <a href="/eshop/eshoppage.aspx" class="gongnengej" rel="toptab"
                            id="heightShop" runat="server">高级网店模板</a> <a href="/GeneralShop/ApplicationEShop.aspx"
                                class="gongnengej" rel="toptab" id="applyShop" runat="server">申请高级网店</a>
                    <a href="/GeneralShop/applyEyou.aspx" class="gongnengej" rel="toptab" id="applyEyou"
                        runat="server">易游通申请</a> <a href="http://www.gocn.cn" target="_blank" class="gongnengej"
                            id="eyouManager" runat="server">易游通管理</a> <a href="/generalshop/tongyenews/newslist.aspx"
                                class="gongnengej" rel="toptab" id="tongYeInfo" runat="server">同业资讯管理</a>
                </div>
            </div>
            <div style="clear: both;">
            </div>
            <div class="gongnengzu">
                <a href="javascript:void(0);" class="bigclassbar">旅游资源</a></div>
            <div class="gongnengxx" style="display: none">
            </div>
            <div class="gongneng" style="display: none">
                <a href="http://vipjp.tongye114.com" class="gongnengej" target="_blank">航空票务系统</a>
                <div id="divSceniceList" runat="server" visible="false">
                    <a href="/ScenicManage/SceniceList.aspx?sortType=1" class="gongnengej" rel="toptab">
                        景区门票</a></div>
                <a href="/HotelCenter/HotelOrderManage/SearchHotel.aspx" class="gongnengej" rel="toptab">
                    预定酒店</a> <a href="/HotelCenter/HotelOrderManage/HotelOrderList.aspx" class="gongnengej"
                        rel="toptab">酒店订单</a> <a href="/HotelCenter/HotelOrderManage/GroupOrderList.aspx"
                            class="gongnengej" rel="toptab">酒店团单</a>
                <%--<a href="/HotelCenter/HotelOrderManage/TeamOnlinesubmit.aspx" class="gongnengej"  rel="toptab">易诺订房</a> --%>
            </div>
            <!--旅游资源结束-->
            <!--营销工具开始-->
            <div style="clear: both;">
            </div>
            <div class="gongnengzu">
                <a href="javascript:void(0);" class="bigclassbar">营销工具</a></div>
            <div class="gongnengxx" style="display: none">
            </div>
            <div class="gongneng" style="display: none">
                <a id="a_GQXX" href="/supplyinformation/addsupplyinfo.aspx" class="gongnengej" rel="toptab"
                    runat="server">供求信息</a> <a href="/tongyeinfo/infolist.aspx" class="gongnengej" rel="toptab">
                        同业资讯</a> <a href="/Memorandum/MemorandumCalendar.aspx" class="gongnengej" rel="toptab">
                            备忘录</a> <a href="/TravelersManagement/TravelersList.aspx" class="gongnengej" rel="toptab">
                                常旅客管理</a> <a href="/customermanage/allcustomers.aspx" class="gongnengej" rel="toptab">
                                    同业名录</a> <a href="/smscenter/sendsms.aspx" class="gongnengej" rel="toptab">短信中心</a>
                <a href="/financemanage/accountsreceivable.aspx" class="gongnengej" rel="toptab">财务管理</a>
            </div>
            <!--营销工具结束-->
            <!--xcl 2011-12-14  结束-->
            <div class="gongnengzu">
                <a href="javascript:void(0);" class="bigclassbar" href="/systemset/systemindex.aspx"
                    onclick="javascript:return false;">系统设置</a></div>
            <!--xcl 2011-12-14  开始-->
            <div class="gongnengxx" style="display: none">
            </div>
            <div class="gongneng" style="display: none">
                <a href="/systemset/personinfoset.aspx" class="gongnengej" rel="toptab">个人设置</a>
                <a href="/systemset/passwordchange.aspx" class="gongnengej" rel="toptab">修改密码</a>
                <a href="/systemset/companyinfoset.aspx" class="gongnengej" rel="toptab">单位信息</a>
                <a href="/systemset/departmanage.aspx" class="gongnengej" rel="toptab">部门设置</a>
                <a href="/systemset/sonusermanage.aspx" class="gongnengej" rel="toptab">子帐户管理</a>
                <a href="/systemset/permitmanage.aspx" class="gongnengej" rel="toptab">权限管理</a>
            </div>
            <!--xcl 2011-12-14  结束-->
        </div>
        <div class="right" id="optionPanel">
            <div class="option-panel-on">
                <uc1:userbackdefault ID="userbackdefault" runat="server" />
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <uc1:webFooter ID="footer" runat="server" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dhtmlHistory") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dcommon") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("autocomplete") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("GetCityList") %>"
        cache="true"></script>

    <script type="text/javascript">
        if ('<%=FirstMenu %>' == '1') {
            $("#divFirstMenu").html($("div[id$=div_TourAgency]").html());
            $("div[id$=div_TourAgency]").css("display", "none");
        }
        if ('<%=FirstMenu %>' == '0') {
            $("#divFirstMenu").html($("div[id$=div_RouteAgency]").html());
            $("div[id$=div_RouteAgency]").css("display", "none");
        }
        if ('<%=FirstMenu %>' == '2') {
            $("#divFirstMenu").html($("div[id$=div_LocalAgency]").html());
            $("div[id$=div_LocalAgency]").css("display", "none");
        }
        if ('<%=FirstMenu %>' == '3') {
            $("#divFirstMenu").html($("div[id$=div_SightAgency]").html());
            $("div[id$=div_SightAgency]").css("display", "none");
        }

        function historyChangeHandler(hash, historyData) {
            var url = decodeURIComponent(hash).replace("page:", "");
            var index = topTab.getIndexByUrl(url);
            if (index != undefined) {
                topTab.select(index);
            } else {
                if (historyData == null || historyData == undefined) {
                    var currentLink = $(".leftmenu").find("a[href*='" + url + "']");
                    if (currentLink.length > 0) {
                        historyData = currentLink.eq(0).text();
                    }
                }
                if (historyData != null && historyData != undefined) {


                    topTab.add(url, historyData);
                }
            }
        }
        var topTab, currentSwfuploadInstances = [], LoginUrl = "<%=EyouSoft.Security.Membership.UserProvider.Url_Login %>";

        var setTimeRefresh = false;
        function renderBody() {
            topTab = new TopTab({
                onSelect: function(tabObj) {
                    if (tabObj.index == 0) {
                        dhtmlHistory.add("", tabObj.title);
                        if (setTimeRefresh) {
                            topTab.url(tabObj.index, "/UserBackDefault.aspx");
                            setTimeRefresh = false;
                            return false;
                        }
                    } else {
                        dhtmlHistory.add(encodeURIComponent("page:" + tabObj.url), tabObj.title);
                    }
                },
                onAdd: function(tabObj) {
                    //alert(tabObj.index+tabObj.url+tabObj.title);return;
                    if (tabObj.index == 0) {

                        dhtmlHistory.add("", tabObj.title);
                    } else {

                        dhtmlHistory.add(encodeURIComponent("page:" + tabObj.url), tabObj.title);
                    }
                }
            });

            dhtmlHistory.initialize();

            // add ourselves as a DHTML History listener
            dhtmlHistory.addListener(historyChangeHandler);

            //currentHash
            var currentHash = decodeURIComponent(dhtmlHistory.getCurrentLocation()).replace("page:", "");
            var currentLink = $(".leftmenu").find("a[href*='" + currentHash + "']");
            if (currentHash != "" && currentLink.length > 0) {
                var b = topTab.open(currentHash, currentLink.eq(0).text());
            } else if (currentHash != "") {
                //find in child tab.
                var parentUrl = ChildTab.getParentUrl(currentHash);
                var parentUrlLink = $(".leftmenu").find("a[href*='" + parentUrl + "']");
                if (parentUrl != "") {
                    topTab.open(parentUrl, parentUrlLink.eq(0).text(), { desUrl: currentHash });
                } else {
                    var isForm = FormTab.isFormUrl(currentHash);
                    if (isForm) {
                        topTab.open(currentHash, "线路修改");
                    }
                }
                //MQ进入订单查询
                if (/ordermqyesss/.test(window.location)) {
                    var thehref = "/hotelcenter/hotelordermanage/HotelOrderList.aspx?orderId=" + window.location.href.slice(-6);
                    topTab.open("/hotelcenter/hotelordermanage/HotelOrderList.aspx?IsFirstKey=1", "订单查询", { desUrl: thehref });
                }
            }

            $("#loadingItem").html("").css({ top: "-1000px", left: "-1000px" });
            $(".topbar,.divtopfull,.divmainfull,.margin10").css({
                position: "static", top: "0px", left: "0px"
            });

            //for css file encoding bug.
            if ($.browser.mozilla || $.browser.safari) {
                createStyleRule("body", 'color:#333;font-size:12px;font-family:"宋体",Arial, Helvetica, sans-serif; text-align: center; background:#fff; margin:0px;');
                createStyleRule("input,textarea,select", 'font-size:12px;font-family:"宋体",Arial, Helvetica, sans-serif;COLOR: #333;');
            }
        };
        renderBody();

        $("#setDirectory").click(function() {
            topTab.open($(this).attr("href"), "挑选专线", { isRefresh: false });
            return false;
        });
        $("#RouteStock").click(function() {
            topTab.open($(this).attr("href"), "进入采购区", { isRefresh: false });
            return false;
        });
        $("#AddTourPlan").click(function() {
            topTab.open($(this).attr("href"), "新增计划", { isRefresh: false });
            return false;
        });

        function Hide_Show_Div(obj) {
            if ($(obj).next().next().css("display") == "none") {
                $(obj).next().css("display", "");
                $(obj).next().next().css("display", "");
            } else {
                $(obj).next().css("display", "none");
                $(obj).next().next().css("display", "none");
            }
        }

        $(".bigclassbar2").eq(0).next().next().css("display", "");
        $(".bigclassbar2").eq(0).next().css("display", "");
        $(".bigclassbar2,.bigclassbar,.gongnengzu").click(function() {
            Hide_Show_Div(this);
        });

        var DefaultFunction = {
            ClickGoToURL: function() {
                var tab = topTab;
                $(".leftmenu a[href][rel='toptab']").click(function() {
                    var a = $(this);
                    var tabrefresh = a.attr("tabrefresh") == "false" ? false : true;
                    var href = a.attr("href");
                    var hash = href.replace(/^.*#/, '');
                    if (href.indexOf("#") == -1) {
                        var b = tab.open(href, a.text(), { isRefresh: tabrefresh });
                    }
                    return false;
                });
            },
            SetLogoURL: function(logo) {
                $("img[id$=imgLogo]").attr("src", logo);
            },
            ChangeFirstMenu: function(obj, type) {
                $(obj).parent("div").unbind("click");
                $.ajax({
                    url: "/default.aspx?flag=change&type=" + type + "&rnd=" + Math.random(),
                    success: function(state) {
                        if (state == 'True') {
                            $("#divFirstMenu").html($(obj).parent("div").parent("div").html());
                            $(obj).parent("div").parent("div").css("display", "none");
                            $(obj).parent("div").parent("div").siblings("div").css("display", "");

                            $(".bigclassbar2").eq(0).next().next().css("display", "");
                            $(".bigclassbar2").eq(0).next().css("display", "");
                            DefaultFunction.ClickGoToURL();
                        }
                    }
                });
                $(obj).parent("div").bind("click", function() {
                    Hide_Show_Div(this);
                });
                $("#divFirstMenu").find($(".bigclassbar2")).bind("click", function() {
                    Hide_Show_Div(this);
                });
            }
        };
        if ('<%=IsRouteAgency %>' == 'True' && '<%=IsTourAgency %>' == 'False') {
            $("#divOrder").click(function() {
                $("#divOrder").next().next().css("display", "none");
                $(this).next("div").css("display", "none");
                topTab.open("/userorder/ordersreceived.aspx", "收到的订单", { isRefresh: false });
                return false;
            });
        }
        else if ('<%=IsRouteAgency %>' == 'False' && '<%=IsTourAgency %>' == 'True') {
            $("#divOrder").click(function() {
                $("#divOrder").next().next().css("display", "none");
                $(this).next("div").css("display", "none");
                topTab.open("/userorder/ordersoutsource.aspx", "购买的订单", { isRefresh: false });
                return false;
            });
        }

        DefaultFunction.ClickGoToURL();

        setTimeout(function() { setTimeRefresh = true; }, 300000);

        window.onbeforeunload = function(e) {
            var b = topTab.isHaveFormChanged();
            if (b) {
                return "----------------------------------------\n提示:尚未保存的信息会丢失\n----------------------------------------";
            }
        };

 
      
    </script>

    </form>
</body>
</html>
