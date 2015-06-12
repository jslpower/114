<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UserPublicCenter.Index" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/WebControl/PageHead.ascx" TagName="PageHead" TagPrefix="uc1" %>
<%@ Register Src="/WebControl/CityAndMenu.ascx" TagName="PageMenu" TagPrefix="uc2" %>
<%@ Register Src="/HomeControl/FrindLinkList.ascx" TagName="FrindLink" TagPrefix="uc3" %>
<%@ Register Src="/WebControl/PageFoot.ascx" TagName="PageFoot" TagPrefix="uc4" %>
<%@ Register Src="/HomeControl/GoldCompany.ascx" TagName="GoldC" TagPrefix="uc5" %>
<%@ Register Src="/HomeControl/RecommendProduct.ascx" TagName="RecomP" TagPrefix="uc6" %>
<%@ Register Src="/HomeControl/News.ascx" TagName="New" TagPrefix="uc7" %>
<%@ Register Src="/HomeControl/GuestsVisit.ascx" TagName="Guest" TagPrefix="uc8" %>
<%@ Register Src="/HomeControl/RecommendCompany.ascx" TagName="RecomC" TagPrefix="uc9" %>
<%@ Register Src="/HomeControl/Companys.ascx" TagName="Comp" TagPrefix="uc10" %>
<%@ Register Src="/HomeControl/Tickets.ascx" TagName="Ticket" TagPrefix="uc11" %>
<%@ Register Src="/HomeControl/Hotels.ascx" TagName="Hotel" TagPrefix="uc12" %>
<%@ Register Src="/HomeControl/RouteAreas.ascx" TagName="RouteArea" TagPrefix="uc13" %>
<%@ Register Src="/HomeControl/IndexRemind.ascx" TagName="IndexRemind" TagPrefix="uc14" %>
<%@ Register Src="HomeControl/Tickets.ascx" TagName="Tickets" TagPrefix="uc15" %>
<%@ Register Src="HomeControl/HotScenic.ascx" TagName="HotScenic" TagPrefix="uc16" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="aspnetForm" name="aspnetForm" runat="server">
    <div id="divHotCitys" style="display: none; z-index: 10000; float: left;">
        <ul id="ulHotCitys">
        </ul>
    </div>
    <uc1:PageHead ID="PageHead1" runat="server" />
    <uc2:PageMenu ID="PageMenu1" runat="server" />
    <div class="px102">
    </div>
    <!--头部信息-->
    <div class="leftnav">
        <div class="leftnav_l">
            <div class="leftnav_lt">
            </div>
            <div class="leftnav_lc">
                <ul>
                    <li><a href="/xianlu_6_0">国内专线</a></li>
                    <li><a href="/xianlu_6_1">国际专线</a></li>
                    <li><a href="/xianlu_6_2">周边短线</a></li>
                    <li><a href="<%=EyouSoft.Common.URLREWRITE.Plane.PlaneDefaultUrl(CityId) %>">预定机票</a></li>
                    <li><a href="http://hotel.tongye114.com/">国际酒店</a></li>
                    <li><a href="/info">供求信息</a></li>
                    <li><a href="<%=EyouSoft.Common.URLREWRITE.tonghang.GetTongHangUrl(CityId.ToString()) %>">
                        商家名录</a></li>
                    <li id="liBox"><a href="javascript:void(0)">旅游百宝箱</a></li>
                </ul>
            </div>
        </div>
        <div class="zizinav" style="display: none; z-index: 3;">
            <div class="zizinavl">
                &nbsp;</div>
            <div class="zizinavli">
                <ul>
                    <li class="bufua">旅游百宝箱</li>
                    <li><a target="_blank" href="http://www.hao123.com/haoserver/kuaidi.htm" rel="nofollow">常用快递</a></li>
                    <li><a target="_blank" href="http://jipiao.kuxun.cn/?fromid=Kgocn-S1328241-T1076481" rel="nofollow">机票查询</a></li>
                    <li><a target="_blank" href="http://www.tongye114.com/visa/Map.aspx">电子地图</a></li>
                    <li><a target="_blank" href="http://www.tongye114.com/visa/Weather.aspx">天气查询</a></li>
                    <li><a target="_blank" href="http://www.tongye114.com/visa/Train.aspx" rel="nofollow">铁路查询</a></li>
                    <li><a target="_blank" href="http://www.tongye114.com/visaList">旅游签证</a></li>
                    <li><a target="_blank" href="http://qq.ip138.com/idsearch/" rel="nofollow">身份证</a></li>
                    <li><a target="_blank" href="http://www.ip138.com/sj/" rel="nofollow">手机</a></li>
                    <li><a target="_blank" href="http://www.enowinfo.com">旅行社软件</a></li>
                    <li><a target="_blank" href="http://hotel.kuxun.cn/?fromid=Kgocn-S1328241-T1216391" rel="nofollow">特价酒店</a></li>
                </ul>
            </div>
        </div>
        <div class="leftnav_c">
            <div class="leftnav_ct" id="newsSlider">
                <div class="num" style="z-index: 2;">
                    <ul id="ulTab">
                        <asp:Repeater ID="rptHotAdvForLi" runat="server">
                            <ItemTemplate>
                                <li>
                                    <%#Container.ItemIndex+1 %></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="container" style="position: relative; width: 564px; overflow: hidden;
                    height: 250px; z-index: 0;" id="hotDiv">
                    <div style="position: absolute; width: 2820px;" id="hotAdv">
                        <asp:Repeater ID="rptHotAdv" runat="server">
                            <ItemTemplate>
                                <div style="width: 564px; float: left;">
                                    <a href="<%# Eval("RedirectURL") %>">
                                        <img alt="<%# Eval("Title") %>" width="564px" height="249" src="<%# EyouSoft.Common.Domain.FileSystem+Eval("ImgPath") %>" /></a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="leftnav_cb">
                <div class="shishigq">
                    实时供求</div>
                <div class="shifalist" id="nav_today">
                    <ul style="padding: 0;" class="supplys">
                        <%=supplysHtml%>
                    </ul>
                </div>
                <div class="sxanniu">
                    <a href="javascript:" class="shangan" id="btn1">&nbsp;</a> <a href="javascript:"
                        class="xiaan" id="btn2">&nbsp;</a>
                </div>
                <div class="fabugq">
                    <a href="/info" style="color: #FF6600; text-decoration: none;" target="_blank">发布供求</a></div>
            </div>
        </div>
        <div class="leftnav_r">
            <div class="dengluk">
                <ul class="login-text" runat="server" id="divYesLogin" visible="false">
                    <li>
                        <%=strLoginMessage%>
                    </li>
                </ul>
                <div class="dengluk_t" runat="server" id="divLogin" visible="true">
                    <div class="yonghuhu">
                        &nbsp;</div>
                    <input type="text" class="yonghu" id="u" name="u">
                    <span id="errU" style="color: Red; display: none;">*</span>
                    <div class="px10">
                    </div>
                    <div class="yonghuhu2">
                        &nbsp;</div>
                    <input type="hidden" id="vc" name="vc" />
                    <input type="password" class="yonghu mima" type="password" id="p" name="p" />
                    <span id="errP" style="color: Red; display: none;">*</span>
                    <div class="xcdl">
                        <span class="spanqian">
                            <input type="checkbox" checked="checked" value="" id="re" name="re">
                            下次自动登录 </span><span><a target="_blank" href="/Register/FindPassWord.aspx">忘记密码</a></span></div>
                    <div class="ljdl">
                        <a id="btnLogin" class="login-btn" onclick="loginF();" style="cursor: pointer">立即登录</a><span
                            id="hidOn" style="color: Red; display: none;">正在登录…</span></div>
                    <div class="zcsj">
                        <a href="/Register/CompanyUserRegister.aspx">注册商家</a></div>
                </div>
                <!--实时信息提醒 -->
                <uc14:IndexRemind ID="IndexRemind1" runat="server" />
            </div>
        </div>
        <div class="faq">
            <div class="faq-title">
                <ul class="clearfix">
                    <li><a onmousemove="setTab(1,2)" id="two1" class="hover" href="javascript:void(0)">
                        本站公告</a></li>
                    <li><a onmousemove="setTab(2,2)" id="two2" href="javascript:void(0)">数据统计</a></li>
                </ul>
            </div>
            <div class="faq-main">
                <div id="con_two_1">
                    <ul>
                        <%=noticeHtml%>
                    </ul>
                </div>
                <div style="display: none;" id="con_two_2">
                    <ul>
                        <li><a href="javascript:void(0)">网站注册公司<strong style="color: #0080C8"><asp:Literal
                            ID="lbregCount" runat="server"></asp:Literal></strong>
                            家</a></li>
                        <li><a href="javascript:void(0)">网站注册会员<strong style="color: #0080C8"><asp:Literal
                            ID="lbrouteCount" runat="server"></asp:Literal></strong>人</a></li>
                        <li><a href="javascript:void(0)">网站发布线路<strong style="color: #0080C8"><asp:Literal
                            ID="lbMqCount" runat="server"></asp:Literal></strong>条</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="px10">
    </div>
    <div class="glcx">
        <!-- 散拼中心-->
        <%if (!isExtendSite)
          { %>
        <uc13:RouteArea ID="routeArea1" runat="server" />
        <!--金牌企业,推荐产品-->
        <div class="glcx_r">
            <uc5:GoldC ID="goldc1" runat="server" />
            <uc6:RecomP ID="RecomP1" runat="server" />
        </div>
        <div class="px10">
        </div>
        <%}
          else
          { %><!--普通分站显示旅行社-->
        <uc10:Comp ID="Comp1" runat="server" /><%} %>
        <div style="width: 970px; height: 70px; overflow: hidden;">
            <%=bannerAdv %></div>
        <div class="px10">
        </div>
    </div>
    <!--机票景区-->
    <div class="changle">
        <uc16:HotScenic ID="HotScenic1" runat="server" />
        <uc15:Tickets ID="Tickets1" runat="server" />
        <div class="ggban">
            <div>
                <a title="云南专线" href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/TourList_134_<%=CityId %>">
                    <img width="238px" height="135px" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/ynzx.jpg"></a></div>
            <div class="px10">
            </div>
            <div>
                <a href="http://www.enowinfo.com">
                    <img  title="易游通" height="75px;" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/index/{BFF352F6-F720-4E83-97E8-E921AA5C9D35}.jpg"></a></div>
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div class="px10">
    </div>
    <!--底部资讯-->
    <div class="mainbox04 fixed">
        <uc7:New ID="new1" runat="server" />
        <uc8:Guest ID="guest1" runat="server" />
        <uc9:RecomC ID="recomc1" runat="server" />
    </div>
    <!--有情链接-->
    <%=partnerlinks%>
    <!--底部版权 -->
    <uc4:PageFoot ID="PageFoot1" runat="server" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("blogin") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Switchable") %>"></script>

    <script type="text/javascript">
        //搜索tab切换
        function setTab(cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById("two" + i);
                var con = document.getElementById("con_two_"+ i);
                menu.className = i == cursel ? "tabtwo-on" : "";
                con.style.display = i == cursel ? "block" : "none";
            }
        }
        //登录
        function loginF() {
            var UserName = $.trim($("#u").val()), PassWord = $.trim($("#p").val());
            if (UserName == "") { $("#errU").show(); }
            if (PassWord == "") { $("#errP").show(); }
            if (UserName != "" && PassWord != "") {
                $("#btnLogin").closest("div").removeClass().addClass("islogin");
                $("#btnLogin").hide();
                $("#hidOn").show();
                var url = '<%=Request.ServerVariables["SCRIPT_NAMT"] %>?CityId=<%=CityId %>';
                blogin.ssologinurl = "<%=EyouSoft.Common.Domain.PassportCenter %>";
                blogin(document.getElementById("aspnetForm"), "", url, function(message) {
                    alert(message); $("#btnLogin").show(); $("#hidOn").hide();
                    $("#p").val("").focus();
                    $("#btnLogin").closest("div").removeClass().addClass("ljdl");
                    $("#btnLogin").attr("disabled", "").css("cursor", "pointer");
                });
            } return false;
        }
        var t;
        $(function() {
            $("#liBox").mouseover(function() {
                $(".zizinav").show();
            });
            $("#liBox").mouseout(function() {
                t = setTimeout(hidediv, 200);
            });
            function hidediv() {
                $(".zizinav").hide();
            }
            $(".zizinav").mouseover(function() {
                $(this).show();
                clearTimeout(t);
            });
            $(".zizinav").mouseout(function() {
                $(".zizinav").hide();
            });
            //焦点图片轮换
            var hotTab = $('#ulTab').switchable('#hotAdv>div', { currentCls: 'on', circular: true, steps: 1, visible: 1, triggers: "li", triggerType: "click", effect: "scroll" }).autoplay({ interval: 5, api: true });
            $("#ulTab li").click(function() {
                hotTab.pause();
            });

            $("#newsSlider").hover(function() { }, function() { hotTab.play(); });

            //绑定登录回车
            $("#u,#p").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    loginF();
                    return false;
                }
            });
            //用户名密码焦点
            $("#u").focus(function() { $("#errU").hide(); }).blur(function() { if ($.trim($(this).val()) == "") { $("#errU").show(); } });
            $("#p").focus(function() { $("#errP").hide(); }).blur(function() { if ($.trim($(this).val()) == "") { $("#errP").show(); } });


            //多行应用@Mr.Think
            var _wrap = $('ul.supplys'); //定义滚动区域
            var _interval = 3000; //定义滚动间隙时间
            var _moving; //需要清除的动画
            _wrap.hover(function() {
                clearInterval(_moving); //当鼠标在滚动区域中时,停止滚动
            }, function() {
                _moving = setInterval(function() {
                    var _field = _wrap.find('li:first'); //此变量不可放置于函数起始处,li:first取值是变化的
                    var _h = _field.height(); //取得每次滚动高度
                    _field.animate({ marginTop: -_h + 'px' }, 600, function() {//通过取负margin值,隐藏第一行
                        _field.css('marginTop', 0).appendTo(_wrap); //隐藏后,将该行的margin值置零,并插入到最后,实现无缝滚动
                    })
                }, _interval)//滚动间隔时间取决于_interval
            }).trigger('mouseleave'); //函数载入时,模拟执行mouseleave,即自动滚动
        });

        (function($) {
            $.fn.extend({
                Scroll: function(opt, callback) {
                    //参数初始化
                    if (!opt) var opt = {};
                    var _btnUp = $("#" + opt.up); //Shawphy:向上按钮
                    var _btnDown = $("#" + opt.down); //Shawphy:向下按钮
                    var timerID;
                    var _this = this.eq(0).find("ul:first");
                    var lineH = _this.find("li:first").height(), //获取行高
                        line = opt.line ? parseInt(opt.line, 10) : parseInt(this.height() / lineH, 10), //每次滚动的行数，默认为一屏，即父容器高度
                        speed = opt.speed ? parseInt(opt.speed, 10) : 900; //卷动速度，数值越大，速度越慢（毫秒）
                    timer = opt.timer //?parseInt(opt.timer,10):3000; //滚动的时间间隔（毫秒）
                    if (line == 0) line = 1;
                    var upHeight = 0 - line * lineH;
                    //滚动函数
                    var scrollUp = function() {
                        _btnUp.unbind("click", scrollUp); //Shawphy:取消向上按钮的函数绑定
                        _this.animate({
                            marginTop: upHeight
                        }, speed, function() {
                            for (i = 1; i <= line; i++) {
                                _this.find("li:first").appendTo(_this);
                            }
                            _this.css({ marginTop: 0 });
                            _btnUp.bind("click", scrollUp); //Shawphy:绑定向上按钮的点击事件
                        });
                    }
                    //Shawphy:向下翻页函数
                    var scrollDown = function() {
                        _btnDown.unbind("click", scrollDown);
                        for (i = 1; i <= line; i++) {
                            _this.find("li:last").show().prependTo(_this);
                        }
                        _this.css({ marginTop: upHeight });
                        _this.animate({
                            marginTop: 0
                        }, speed, function() {
                            _btnDown.bind("click", scrollDown);
                        });
                    }
                    _btnUp.css("cursor", "pointer").click(scrollUp)
                    _btnDown.css("cursor", "pointer").click(scrollDown)
                }
            })
        })(jQuery);
        $(document).ready(function() {
            $("#nav_today").Scroll({ line: 2, speed: 900, timer: 3000, up: "btn1", down: "btn2" });
        });
    </script>
    </form>
</body>
</html>
