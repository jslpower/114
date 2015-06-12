<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SeniorOnlineShop._Default"
    MasterPageFile="~/master/SeniorShop.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<asp:Content ID="c1" runat="server" ContentPlaceHolderID="c1">
    <div class="fouse">

        <script type="text/javascript">
var pic_width=385; //图片宽度
var pic_height=170; //图片高度
var button_pos=4; //按扭位置 1左 2右 3上 4下
var stop_time=3000; //图片停留时间(1000为1秒钟)
var show_text=1; //是否显示文字标签 1显示 0不显示
var txtcolor="000000"; //文字色
var bgcolor="DDDDDD"; //背景色

var imag=new Array();
var link=new Array();
var text=new Array();
<%=initFlashJs %>
//imag[1]="<%=ImageServerUrl %>/images/seniorshop/01.jpg";
//link[1]="http://www.lanrentuku.com/";
//text[1]="标题 1";
//imag[2]="<%=ImageServerUrl %>/images/seniorshop/02.jpg";
//link[2]="http://www.lanrentuku.com/";
//text[2]="标题 21";
//imag[3]="<%=ImageServerUrl %>/images/seniorshop/03.jpg";
//link[3]="http://www.lanrentuku.com/";
//text[3]="标题 3";
//imag[4]="<%=ImageServerUrl %>/images/seniorshop/04.jpg";
//link[4]="http://www.lanrentuku.com/";
//text[4]="标题 4";
//imag[5]="<%=ImageServerUrl %>/images/seniorshop/05.jpg";
//link[2]="http://www.lanrentuku.com/";
//text[5]="标题 5";
//可编辑内容结束
var swf_height=show_text==1?pic_height+20:pic_height;
var pics="", links="", texts="";
for(var i=1; i<imag.length; i++){
	pics=pics+("|"+imag[i]);
	links=links+("|"+link[i]);
	texts=texts+("|"+text[i]);
}
pics=pics.substring(1);
links=links.substring(1);
texts=texts.substring(1);
document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cabversion=6,0,0,0" width="'+ pic_width +'" height="'+ swf_height +'">');
document.write('<param name="movie" value="<%=ImageServerUrl %>/images/seniorshop/focus.swf">');
document.write('<param name="quality" value="high"><param name="wmode" value="opaque">');
document.write('<param name="FlashVars" value="pics='+pics+'&links='+links+'&texts='+texts+'&pic_width='+pic_width+'&pic_height='+pic_height+'&show_text='+show_text+'&txtcolor='+txtcolor+'&bgcolor='+bgcolor+'&button_pos='+button_pos+'&stop_time='+stop_time+'">');
document.write('<embed src="<%=ImageServerUrl %>/images/seniorshop/focus.swf" FlashVars="pics='+pics+'&links='+links+'&texts='+texts+'&pic_width='+pic_width+'&pic_height='+pic_height+'&show_text='+show_text+'&txtcolor='+txtcolor+'&bgcolor='+bgcolor+'&button_pos='+button_pos+'&stop_time='+stop_time+'" quality="high" width="'+ pic_width +'" height="'+ swf_height +'" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />');
document.write('</object>');
        </script>

    </div>
    <div class="newsleft">
        <div class="newsleftbar">
            <span>最新旅游动态</span><a runat="server" id="linkNewsList" href="">更多&gt;&gt;</a></div>
        <ul style="height: 148px;">
            <cc1:CustomRepeater ID="rptNewsList" runat="server" EmptyText="<li>暂无最新旅游动态</li>"
                OnItemCreated="rptNewsList_ItemCreated">
                <ItemTemplate>
                    <li>·<a id="linkNew" runat="server" class="huizi"></a></li>
                </ItemTemplate>
            </cc1:CustomRepeater>
        </ul>
    </div>
    <div class="clear">
    </div>
    <div class="bar-line">
        <div class="bar-line-title">
            我的旅游线路</div>
        <div class="bar-line-mid" style="padding-left: 10px;">
            <input type="radio" name="radlinkCity" value="0" />
            <a href="<%=EyouSoft.Common.Utils.GenerateShopPageUrl2("/TourList",Master.CompanyId) %>"
                style="color: Black">全部</a>
            <asp:Repeater ID="rptCitys" runat="server" OnItemCreated="rptCitys_ItemCreated">
                <ItemTemplate>
                    <input type="radio" name="radlinkCity" id="City_<%# Eval("CityId") %>" value="<%# Eval("CityId") %>" />
                    <label for="City_<%# Eval("CityId") %>">
                        <a id="linkCity" runat="server" style="color: Black"></a>
                    </label>
                </ItemTemplate>
            </asp:Repeater>
            &nbsp
            <asp:DropDownList ID="ddlCitys" runat="server">
            </asp:DropDownList>
        </div>
        <div class="bar-line-more">
            <a href="<%=EyouSoft.Common.Utils.GenerateShopPageUrl2("/TourList",Master.CompanyId) %>">
                查看全部旅游线路</a></div>
    </div>
    <table class="liststyle" border="1" bordercolor="#e3e3e3" cellpadding="0" cellspacing="0"
        width="100%">
        <thead>
            <tr>
                <th width="10%" align="center">
                    出发
                </th>
                <th width="45%" align="center">
                    团队基本信息
                </th>
                <th width="19%" align="center">
                    班次
                </th>
                <th width="19%" align="center">
                    市场价
                </th>
                <th width="9%" align="center">
                    操作
                </th>
            </tr>
        </thead>
        <tbody>
            <cc1:CustomRepeater ID="rptTourList" runat="server" OnItemCreated="rptTourListItemCreated">
                <ItemTemplate>
                    <tr class="odd">
                        <td align="center">
                            <asp:Literal ID="ltrDanFangCha" runat="server"></asp:Literal>
                        </td>
                        <td align="left">
                            <div class="listtitle">
                                <img src="<%=ImageServerUrl %>/images/seniorshop/ico.gif" height="11" width="11"
                                    alt="" />
                                <asp:Literal ID="ltrTuiGuang" runat="server"></asp:Literal>
                                <a id="linkTour" runat="server" target="_blank">
                                    <asp:Literal ID="LitRouteName" runat="server"></asp:Literal>
                                </a>
                            </div>
                        </td>
                        <td align="center">
                            <asp:Literal ID="ltrCurrentTour" runat="server"></asp:Literal>
                        </td>
                        <td align="center">
                            <asp:Literal ID="ltrPrices" runat="server"></asp:Literal>
                        </td>
                        <td align="center" width="80">
                            <%#GetOrderByRoute(Eval("RouteId").ToString(), Eval("RouteSource"))%><br />
                            <asp:Literal ID="linkMQ" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
            </cc1:CustomRepeater>
        </tbody>
        <tfoot>
            <tr>
                <th colspan="5" align="center" style="margin-top: 75px; margin-bottom: 75px">
                    <asp:Label ID="labShowMsg" runat="server"></asp:Label>
                </th>
            </tr>
        </tfoot>
    </table>
    <div class="mudidikuang">
        <div class="mudidi">
            <div class="mudizi">
                目的地指南</div>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 158px;">
            <tbody>
                <tr>
                    <td valign="top" width="33%">
                        <table class="maintop5" style="margin-left: 5px;" border="0" cellpadding="0" cellspacing="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td class="muhang">
                                        <strong>风土人情介绍</strong><span><a id="linkMuDiDi1" runat="server">更多&gt;&gt;</a></span>
                                    </td>
                                </tr>
                                <cc1:CustomRepeater ID="rptTrip1" runat="server" OnItemCreated="rptTrip1_ItemCreated">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="ltrTrip" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </cc1:CustomRepeater>
                            </tbody>
                        </table>
                    </td>
                    <td valign="top" width="33%">
                        <table class="maintop5" style="margin-left: 5px;" border="0" cellpadding="0" cellspacing="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td class="muhang">
                                        <strong>温馨提醒</strong><span><a id="linkMuDiDi2" runat="server">更多&gt;&gt;</a></span>
                                    </td>
                                </tr>
                                <cc1:CustomRepeater ID="rptTrip2" runat="server" OnItemCreated="rptTrip1_ItemCreated">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="ltrTrip" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </cc1:CustomRepeater>
                            </tbody>
                        </table>
                    </td>
                    <td valign="top" width="34%">
                        <table class="maintop5" style="margin-left: 5px;" border="0" cellpadding="0" cellspacing="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td class="muhang">
                                        <strong>综合介绍</strong><span><a id="linkMuDiDi3" runat="server">更多&gt;&gt;</a></span>
                                    </td>
                                </tr>
                                <cc1:CustomRepeater ID="rptTrip3" runat="server" OnItemCreated="rptTrip3_ItemCreated">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                ·<a id="linkTrip" runat="server" class="huizi"></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </cc1:CustomRepeater>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="mudidikuang">
        <div class="mudidi">
            <div class="mudizi">
                旅游资源推荐</div>
            <span class="STYLE1" style="padding-left: 480px;"><a href="" id="linkZiYuans" runat="server"
                class="white">更多&gt;&gt;</a></span></div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 122px;">
            <tbody>
                <tr>
                    <cc1:CustomRepeater ID="rptziyuans" runat="server" EmptyText="<td align='center'>暂无旅游资源推荐</td>"
                        OnItemCreated="rptziyuans_ItemCreated">
                        <ItemTemplate>
                            <td width="33%">
                                <table style="margin: 10px 5px;" border="0" cellpadding="0" cellspacing="0" width="125">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <a id="linkZiYuanShow1" runat="server"></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <a id="linkZiYuanShow2" runat="server"></a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </ItemTemplate>
                    </cc1:CustomRepeater>
                </tr>
            </tbody>
        </table>
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
                        <a href="javascript:;" onclick="CloseLeft();">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/hulue.gif"></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <%--浮动咨询结束--%>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery.floating.js") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("TourCalendar") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript">
        //预定按钮调用的方法 模板团ID，点击对象
        function ClickCalendar(TourId,obj,AreaType) {
            SingleCalendar.config.isLogin = "<%=IsLogin %>"; //是否登陆
            SingleCalendar.config.stringPort ="<%= EyouSoft.Common.Domain.UserPublicCenter %>";//配置
            SingleCalendar.initCalendar({
                currentDate:<%=thisDate %>,//当时月
                firstMonthDate: <%=thisDate %>,//当时月
                srcElement: obj,
                areatype:AreaType,//当前模板团线路区域类型 
                TourId:TourId,//模板团ID
                AddOrder:AddOrder
            });
        }
        
        function AddOrder(TourId) {
            if ("<%=IsLogin %>" == "True") {
                var strParms = { TourId: TourId };
                Boxy.iframeDialog({ title: "预定", iframeUrl: "/seniorshop/RouteOrder.aspx", width: "800", height: GetAddOrderHeight(), draggable: true, data: strParms });
            } else {
                //登录
                window.location.href = '<%= EyouSoft.Common.Domain.UserPublicCenter %>/Register/Login.aspx?isShow=1&CityId=<%=CityId %>&returnurl=' + escape('<%= EyouSoft.Common.Domain.SeniorOnlineShop %><%=Request.ServerVariables["SCRIPT_NAME"]%>?<%=Request.QueryString%>');
            }
        }

        function goTourListByCity(city) {
            window.location.href = '<%=EyouSoft.Common.Utils.GenerateShopPageUrl2("/TourList_' + city + '",Master.CompanyId) %>';
        }
        $(function() {
            $(".liststyle a[rel='calendar']").click(function() {
                var o = $(this);
                ClickCalendar(o.attr("pid"), this, parseInt(o.attr("areatype")));
                return false;
            });
            $("#<%=ddlCitys.ClientID %>").change(function() {
                goTourListByCity($(this).val());
            });
            $("#divZX").easydrag();
            $("#divZX").floating({ position: "left", top: 100, left: 10, width: 400 });

            $("input[type='radio'][name='radlinkCity']").bind("click", function() {
                $("input[type='radio'][name='radlinkCity']:checked").each(function() {
                    if ($(this).val() == "0") {
                        window.location.href = '<%=EyouSoft.Common.Utils.GenerateShopPageUrl2("/TourList",Master.CompanyId) %>';
                    }
                    else {
                        window.location.href = '<%=EyouSoft.Common.Utils.GenerateShopPageUrl2("/TourList_' + $(this).val() + '",Master.CompanyId) %>';
                    }
                });
            });

            $(".goumai0").click(function() {
                if ("<%=IsLogin %>" == "False") {
                    Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true });  
          return false;
                }
              
            });
        });
    </script>

</asp:Content>
