<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultNew.aspx.cs" Inherits="UserBackCenter.DefaultNew" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/WebHeader.ascx" TagName="webHeader" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/WebFooter.ascx" TagName="webFooter" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/UserBackDefault.ascx" TagName="userbackdefault" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>旅行社后台_同业114</title>
    <link href="<%=CssManage.GetCssFilePath("rightnew") %>" rel="stylesheet" type="text/css" />
 </head>
<link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
<link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
<link href="<%=CssManage.GetCssFilePath("autocomplete") %>" rel="Stylesheet" type="text/css" />

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
            JavaScript，然后 <a href="/DefaultNew.aspx">重试</a>。</p>
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
                    <span><a href="#">首页</a></span></div>
            </div>
        </div>
    </div>
    <!--主体-->
    <div class="divmainfull">
        <div class="leftmenu">
            <div class="bigclassbar2">
                <span>专线商功能</span><a href="#"><img src="<%=ImageServerPath %>/images/wenhao.gif" /></a>
            </div>
            <div class="gongnengxx2">
            </div>
            <div class="gongneng2">
                <a href="#" class="xianluku">线路管理</a>
                <div style="clear: both;">
                </div>
                <ul class="left-submenu">
                    <li><a href="#" rel="toptab">添加线路</a></li>
                    <li><a href="#" rel="toptab">我的线路库</a></li>
                    <li><a href="#" rel="toptab">我的散拼计划</a></li>
                    <li><a href="#" rel="toptab">我的历史团队</a></li>
                </ul>
            </div>
            <div class="gongneng2">
                <a href="#" class="xianluku">我的订单</a>
                <div style="clear: both;">
                </div>
                <ul class="left-submenu">
                    <li><a href="#" rel="toptab">最新散客订单</a></li>
                    <li><a href="#" rel="toptab">所有旅游订单</a></li>
                    <li><a href="#" rel="toptab">独立团队订单</a></li>
                    <li><a href="#" rel="toptab">专线订单统计</a></li>
                </ul>
            </div>
            <div class="bigclassbar2">
                <span>我的网店</span><a href="#"><img src="<%=ImageServerPath %>/images/wenhao.gif" /></a></div>
            <div class="gongnengxx2">
            </div>
            <div class="gongneng2">
                <a class="xianluku wangdian" href="#" rel="toptab">查看网店</a> <a class="xianluku wangdianmb" href="#">
                    网店模版 </a><a class="xianluku wangdianmbgj" href="#" rel="toptab">高级网店模版</a> <a class="xianluku shqwangdiangj"
                        href="#" rel="toptab">申请高级网店</a> <a class="xianluku tongyegg" href="#" rel="toptab">同业通告</a>
                <div style="clear: both;">
                </div>
            </div>
            <div class="bigclassbar2">
                <span>景区门票</span><a href="#"><img src="../images/wenhao.gif" /></a></div>
            <div class="gongnengxx2">
            </div>
            <div class="gongneng2">
                <a class="xianluku wdjingqu" href="ScenicManage/MyScenice.aspx" rel="toptab">我的景区</a>
                <!--<a class="xianluku" href="../zx/wangdian.html">我的门票订单 </a>-->
                <div style="clear: both;">
                </div>
            </div>
            <!--组团社功能-->
            <div class="bigclassbar2">
                <span>组团社功能</span><a href="#"><img src="<%=ImageServerPath %>/images/wenhao.gif" /></a></div>
            <div class="gongnengxx2">
            </div>
            <div class="gongneng2">
                <a href="#" class="xianluku">线路团队</a>
                <div style="clear: both;">
                </div>
                <ul class="left-submenu">
                    <li><a href="#" rel="toptab">国内散拼计划</a></li>
                    <li><a href="#" rel="toptab">国际散拼计划</a></li>
                    <li><a href="#" rel="toptab">周边散拼计划</a></li>
                    <li><a href="#" rel="toptab">旅游线路库</a></li>
                    <li><a href="#" rel="toptab">我的收藏</a></li>
                </ul>
            </div>
            <div class="gongneng2">
                <a href="#" class="xianluku">我的订单</a>
                <div style="clear: both;">
                </div>
                <ul class="left-submenu">
                    <li><a href="#" rel="toptab">我的散客订单</a></li>
                    <li><a href="#" rel="toptab">独立成团订单</a></li>
                    <li><a href="#" rel="toptab">旅游订单统计</a></li>
                </ul>
            </div>
            <!--宾馆酒店功能-->
            <div class="bigclassbar2">
                <span>宾馆酒店功能</span><a href="#"><img src="<%=ImageServerPath %>/images/wenhao.gif" /></a></div>
            <div class="gongnengxx2">
            </div>
            <div class="gongneng2">
                <a href="#" class="xianluku jiudiangl">酒店管理</a>
                <div style="clear: both;">
                </div>
                <ul class="left-submenu">
                    <li><a href="#" rel="toptab">我的酒店</a></li>
                    <li><a href="#" rel="toptab">我的酒店订单</a></li>
                    <li><a href="#" rel="toptab">住店审核管理</a></li>
                </ul>
            </div>
            <div class="gongneng3">
                <a href="#" class="bigclassbar">旅游资源</a><a title="点击展开" class="barmove" href="javascript:void(0)">展开</a></div>
            <div class="gongneng">
                <a href="#" rel="toptab" class="basic_Smenubg">预定酒店</a> <a href="#" rel="toptab" class="basic_Smenubg">酒店订单</a>
                <a href="#" rel="toptab" class="basic_Smenubg">酒店团队</a> <a href="#" rel="toptab" class="basic_Smenubg">易诺订房</a>
                <a href="#" rel="toptab" class="basic_Smenubg">预订机票</a> <a href="http://jipiao.tongye114.com/order/default.asp"
                    class="basic_Smenubg">机票后台</a> <a href="#" class="basic_Smenubg">景区门票</a>
                <div style="clear: both;">
                </div>
            </div>
            <div class="gongneng3">
                <a href="#" class="bigclassbar">营销工具</a><a title="点击展开" class="barmove" href="javascript:void(0)">展开</a></div>
            <div class="gongneng">
                <a href="#" rel="toptab" class="basic_Smenubg">供求信息</a> <a href="#" rel="toptab" class="basic_Smenubg">同业资讯</a>
                <a href="#" rel="toptab" class="basic_Smenubg">备忘录</a> <a href="#" rel="toptab" class="basic_Smenubg">常旅客管理</a>
                <a href="#" rel="toptab" class="basic_Smenubg">同业名录</a> <a href="#" rel="toptab" class="basic_Smenubg">短信中心</a>
                <a href="#" rel="toptab" class="basic_Smenubg">财务管理</a>
                <div style="clear: both;">
                </div>
            </div>
            <!--通用功能-->
            <div class="gongneng3">
                <a href="#" class="bigclassbar">系统管理</a><a title="点击展开" class="barmove" href="javascript:void(0)">展开</a></div>
            <div class="gongneng">
                <a href="#" rel="toptab" class="basic_Smenubg">个人设置</a> <a href="#" rel="toptab" class="basic_Smenubg">修改密码</a>
                <a href="#" rel="toptab" class="basic_Smenubg">单位信息 </a><a href="#" rel="toptab" class="basic_Smenubg">部门设置</a>
                <a href="#" rel="toptab" class="basic_Smenubg">子帐户管理</a> <a href="#" rel="toptab" class="basic_Smenubg">权限管理</a>
                <a href="#" rel="toptab" class="basic_Smenubg">管理首页</a> <a href="#" rel="toptab" class="basic_Smenubg">安全退出</a>
                <div style="clear: both;">
                </div>
            </div>
        </div>
        <div class="right" id="optionPanel">
            <div class="option-panel-on">
                <%--<uc1:userbackdefault ID="userbackdefault" runat="server" />--%>
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

    <script type="text/javascript">
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



        $(document).ready(function() {
            $("div.gongneng2 a.xianluku").click(function() {
                $(this).parent().find("ul.left-submenu").slideToggle("slow")
			        .siblings("ul.left-submenu:visible").slideUp("slow");
            })
        })

        $(document).ready(function() {
            $(".gongneng3").click(function() {
                $(this).next("div").slideToggle("slow")
			        .siblings("div.gongneng:visible").slideUp("slow");
                $(this).find("a.barmove").toggleClass("barmove02");
                $(this).find("a.barmove").siblings(".barmove02").removeClass("barmove02");
            })
        })

        //        function Hide_Show_Div(obj) {
        //            if ($(obj).next().next().css("display") == "none") {
        //                $(obj).next().css("display", "");
        //                $(obj).next().next().css("display", "");
        //            } else {
        //                $(obj).next().css("display", "none");
        //                $(obj).next().next().css("display", "none");
        //            }
        //        }

        //        $(".bigclassbar2").eq(0).next().next().css("display", "");
        //        $(".bigclassbar2").eq(0).next().css("display", "");
        //        $(".bigclassbar2,.bigclassbar,.gongnengzu").click(function() {
        //            Hide_Show_Div(this);
        //        });

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
                    url: "/defaultNew.aspx?flag=change&type=" + type + "&rnd=" + Math.random(),
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
                //                $(obj).parent("div").bind("click", function() {
                //                    Hide_Show_Div(this);
                //                });
                //                $("#divFirstMenu").find($(".bigclassbar2")).bind("click", function() {
                //                    Hide_Show_Div(this);
                //                });
            }
        };
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
