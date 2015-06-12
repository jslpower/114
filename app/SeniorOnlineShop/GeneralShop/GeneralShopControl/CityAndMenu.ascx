<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CityAndMenu.ascx.cs"
    Inherits="SeniorOnlineShop.GeneralShop.GeneralShopControl.CityAndMenu" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<div class="souch_1" >
    <div class="logo_1">
        <a href="<%=SubStation.CityUrlRewrite(CityId) %>">
            <img src="<%=UnionLogo %>" width="130px" height="55px" alt="同业114" /></a></div>
    <div class="chengshi_1">
        <span>
            <asp:Label ID="labCityName" runat="server"></asp:Label></span><br />
        <a href="<%=SubStation.CityUrlRewrite(19) %>">北京</a>&nbsp;<a href="<%=SubStation.CityUrlRewrite(48) %>">广州</a>&nbsp;<a
            href="<%=SubStation.CityUrlRewrite(362) %>">杭州</a><br />
        <a href="<%=SubStation.CityUrlRewrite(292) %>">上海</a><a href="<%=Domain.UserPublicCenter %>/ToCutCity.aspx?Index=<%=HeadMenuIndex %>">[切换城市]</a>
    </div>
    <div class="sousuobox_1">
        <div class="sousuo_1">
            <label for="textfield">
            </label>
            <input name="txtKeyWord" runat="server" type="text" class="ssk" id="txtKeyWord" value="请输入关键词" />
            <div class="ssg" id="SearchRoute" runat="server">
                <a href="javascript:void(0);">搜线路</a></div>
            <div class="ssg ssg2" id="SearchRival" runat="server">
                <a href="javascript:void(0);">搜商家</a></div>
        </div>
        <div class="remen"><%=sbThemelist.ToString() %>
        </div>
    </div>
    <div class="dianhua">
    </div>
</div>
<div class="nav">
    <ul>
        <li id="liMenu1" runat="server"><a runat="server" id="menu1">首页</a></li><li class="lixian">
        </li>
        <li id="liMenu2" runat="server"><a runat="server" id="menu2">线路</a></li><li class="lixian">
        </li>
        <li id="liMenu3" runat="server"><a runat="server" id="menu3" >机票</a></li><li class="lixian"></li>
        <li id="liMenu4" runat="server"><a runat="server" id="menu4">酒店</a></li><li class="lixian">
        </li>
        <li id="liMenu5" runat="server"><a runat="server" id="menu5">景区</a></li><li class="lixian">
        </li>
        <li id="liMenu6" runat="server"><a runat="server" id="menu6">供求</a></li><li class="lixian">
        </li>
        <li id="liMenu7" runat="server"><a runat="server" id="menu7">资讯</a></li>
        <li class="xiazai">
            <img src="<%=ImageServerPath %>/images/new2011/index/home_27.jpg" alt=" " width="234"
                height="33" border="0" usemap="#Map" />
            <map name="Map" id="Map">
                <area shape="rect" coords="-1,1,154,48" href="http://im.tongye114.com/" />
                <area shape="rect" coords="156,1,249,52" href="http://im.tongye114.com/IM/DownLoad/download.aspx" />
            </map>
        </li>
    </ul>
</div>

<script type="text/javascript">
    $(function() {
        if (<%=HeadMenuIndex %> > 0) {
            if (<%=HeadMenuIndex %> == 10)
                $("#eare").css("marginTop", "16px");
        }
        if ($.browser.msie) {
            if (parseFloat($.browser.version) <= 6) {
                try {
                    document.execCommand('BackgroundImageCache', false, true);
                } catch (e) { }
            }
        }

        $("#<%=txtKeyWord.ClientID %>").focus(function() {
            if ($(this).val() == "请输入关键词") {
                $(this).val('');
            }
        }).blur(function() {
            if ($(this).val() == "") {
                $(this).val("请输入关键词");
            }
        });

        $("#<%=txtKeyWord.ClientID %>").bind("keypress", function(e) {
            if (e.keyCode == 13) {
                $(".sousuo_1 a:eq(0)").click();
                return false;
            }
        });

        $(".sousuo_1 a:eq(0)").click(function() {
            var keyWord = $.trim($("#<%=txtKeyWord.ClientID %>").val()) == "请输入关键词" ? "" : $.trim($("#<%=txtKeyWord.ClientID %>").val());
            var url = "<%= Domain.UserPublicCenter %>/TourManage/TourList.aspx?SearchType=More&keyWord=" + escape(keyWord);
            window.location.href = url;
            return false;
        });

        $(".sousuo_1 a:eq(1)").click(function() {
            var CityId = "<%=CityId %>";
            var PrivoinceId = "<%=PrivoinceId %>";
            var keyWord = $.trim($("#<%=txtKeyWord.ClientID %>").val()) == "请输入关键词" ? "" : $.trim($("#<%=txtKeyWord.ClientID %>").val());
            var url = "<%= Domain.UserPublicCenter %>/FindRival/rival.aspx?cId=" + CityId + "&provinceID=" + PrivoinceId + "&companyname=" + escape(keyWord);
            window.location.href = url;
            return false;
        });


    });
       
</script>
