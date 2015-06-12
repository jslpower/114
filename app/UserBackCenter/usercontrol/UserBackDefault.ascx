<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserBackDefault.ascx.cs"
    Inherits="UserBackCenter.usercontrol.UserBackDefault" %>
<%@ Register Src="~/usercontrol/MemoCalendar.ascx" TagName="memocalendar" TagPrefix="uc1" %>
<%@ Register Src="~/SupplyInformation/UserControl/ExchangeListControl.ascx" TagName="exchangelist"
    TagPrefix="uc1" %>
<div class="rigtop">
    <div class="rigtopl">
        <div style="margin-top: 0px; margin-bottom: 5px;">
            <asp:Label ID="lblLoginUser" runat="server"></asp:Label><br />
            目前平台已有<strong class="chengse"><asp:Label ID="lblRouteAgencyCount" runat="server"></asp:Label></strong>家旅行社，<strong
                class="chengse"><asp:Label ID="lblHotelCount" runat="server"></asp:Label></strong>家酒店，<strong
                    class="chengse"><asp:Label ID="lblSightCount" runat="server"></asp:Label></strong>家景区，<strong
                        class="chengse"><asp:Label ID="lblCarCount" runat="server"></asp:Label></strong>家车队<span
                            style="display: none;">，<strong class="chengse"><asp:Label ID="lblShoppingCount"
                                runat="server"></asp:Label></strong>家购物点</span>加盟</div>
    </div>
</div>
<div class="rigbleft">
    <div class="impxx" id="divNoLocalAgency" runat="server">
        <div class="impxxline1">
        </div>
        <div class="impxxheader">
            同业114提醒</div>
        <div class="impxxline2">
        </div>
        <dl>
            <dt><a href="javascript:void(0);" id="span_TourAgency" runat="server" visible="false">
                <strong>组 团</strong></a> <a id="span_RouteAgency" runat="server" href="javascript:void(0);"
                    visible="false"><strong>专 线</strong></a> <a id="span_ErAgency" runat="server" href="javascript:void(0);"
                        visible="false"><strong>地 接</strong></a> <a id="span_Scenic" runat="server" href="javascript:void(0);"
                            visible="false"><strong>景 区</strong></a> </dt>
            <dd>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="impbox">
                    <tr>
                        <td class="impboxtop">
                            <ul id="ul_TourAgency" runat="server" visible="false">
                                <table width="100%" border="0" cellspacing="0" cellpadding="6">
                                    <tr>
                                        <td align="left">
                                            针对<asp:Label ID="lblCity" runat="server"></asp:Label>地区，平台有<asp:Label ID="lblTemplateTourCount"
                                                runat="server"></asp:Label>条线路（<a href="/teamservice/routestock.aspx" rel="toptab"
                                                    tabrefresh="false" onclick="topTab.open($(this).attr('href'),'采购区',{isRefresh:false});return false;"
                                                    class="lan14">共<asp:Label ID="lblChildTourCount" runat="server"></asp:Label>个团队</a>）可供报名，欢迎您点击查阅、预订！
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" cellspacing="0" cellpadding="0" border="0" style="margin-top: 5px;">
                                    <tbody>
                                        <tr>
                                            <td width="43%">
                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="27%" valign="middle" align="center" rowspan="2">
                                                                <img width="75" height="61" src="<%=ImageServerPath %>/images/impixo2.gif">
                                                            </td>
                                                            <td width="73%" height="25" align="left">
                                                                <a class="link14" href="javascript:void(0);"><b>快速采购通道</b></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="50" align="left" class="huise">
                                                                <a class="lan14" href="/teamservice/ScatterPlanC.aspx" onclick="topTab.open($(this).attr('href'),'国内散拼计划');return false;">
                                                                    国内长线</a> <a class="lan14" href="/teamservice/ScatterPlanI.aspx" onclick="topTab.open($(this).attr('href'),'国际散拼计划');return false;">
                                                                        国际线</a>&nbsp;<a class="lan14" href="/teamservice/ScatterPlanP.aspx" onclick="topTab.open($(this).attr('href'),'周边散拼计划');return false;">周边游</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <table cellspacing="0" cellpadding="0" border="0" class="cgbj">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <a href="/teamservice/linelibrarylist.aspx" onclick="topTab.open($(this).attr('href'),'旅游线路库');return false;">
                                                                                    旅游线路库</a>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td width="57%">
                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="15%" valign="middle" align="center" rowspan="3">
                                                                <img width="75" height="61" align="bottom" src="<%=ImageServerPath %>/images/impixo3.gif">
                                                            </td>
                                                            <td width="85%" height="25" align="left">
                                                                <a class="link14"><b>我的订单</b></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="25" align="left" class="huise">
                                                                <asp:Label ID="lblTourOrderFrist" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="25" align="left" class="huise">
                                                                <asp:Label ID="lblTourOrderSecond" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="15%" align="center">
                                                                &nbsp;
                                                            </td>
                                                            <td width="85%" align="left">
                                                                <table cellspacing="0" cellpadding="0" border="0" class="cgbj">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <a href="/teamservice/fitorders.aspx" onclick="topTab.open($(this).attr('href'),'我的散拼订单')">
                                                                                    最 新 订 单</a>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="display: none;">
                                    <tr>
                                        <td align="right">
                                            <a href="#" class="general">邀请您合作的专线加盟同业114，赢取积分！</a>
                                        </td>
                                    </tr>
                                </table>
                            </ul>
                            <ul id="ul_RouteAgency" runat="server" visible="false">
                                <table width="100%" border="0" cellspacing="0" cellpadding="6">
                                    <tr>
                                        <td align="left">
                                            <span class="shenghong2">
                                                <asp:Label ID="lblComingExpireToursCount" runat="server"></asp:Label></span>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="43%" height="86" align="left">
                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="27%" valign="middle" align="center" rowspan="2">
                                                                <img width="75" height="62" src="<%=ImageServerPath %>/images/impixo1.gif">
                                                            </td>
                                                            <td width="73%" align="left">
                                                                <a class="link14"><b>我的产品</b></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="left" style="line-height: 120%;" class="huise">
                                                                <asp:Label ID="lblRoutePlanCount" runat="server" Text="当前您还没有发布散拼计划，同业114平台每天都有上万家组团社关注，还等什么呢？"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <a class="xiazai" href="/routeagency/routemanage/rmdefault.aspx" onclick="topTab.open($(this).attr('href'),'添加线路');return false;">
                                                                    立即免费发布</a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td width="57%" align="left">
                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="15%" valign="middle" align="center" rowspan="4">
                                                                <img width="75" height="61" src="<%=ImageServerPath %>/images/impixo3.gif">
                                                            </td>
                                                            <td width="85%" align="left">
                                                                <a class="link14"><b>我的订单</b></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="23" align="left" class="huise">
                                                                <asp:Label ID="lblRouteOrderFrist" runat="server" Text="暂无散拼订单!"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10" align="left" class="huise">
                                                                <asp:Label ID="lblRouteOrderSecond" runat="server" Text="暂无团队订单!"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10" align="left" class="huise">
                                                                <asp:Label ID="lblRouteOrderThird" runat="server" Text="暂无历史订单!"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="bottom" height="21" align="right" colspan="2">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="display: none;">
                                    <tr>
                                        <td width="59%" align="right">
                                        </td>
                                        <td width="41%" align="right">
                                            <span class="huise">当前共有<asp:Label ID="lblBrowseUserCount" runat="server"></asp:Label>个用户访问过您的产品,</span><a
                                                href="<%=HighShopURL %>" target="_blank">查看我的网店</a>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ul>
                            <ul id="ul_ErAgency" runat="server" visible="false">
                                <table width="100%" cellspacing="0" cellpadding="6" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="43%" align="left" height="86">
                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="27%" valign="middle" align="center" rowspan="2">
                                                                <img width="75" height="62" src="<%=ImageServerPath %>/images/impixo1.gif">
                                                            </td>
                                                            <td width="73%" align="left">
                                                                <a class="link14" href="#"><b>我的产品</b></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="left" style="line-height: 120%;" class="huise">
                                                                <asp:Label ID="lblLocalRoute" runat="server" Text="您目前还没有发布线路，赶紧动手吧，还等什么呢？"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <a class="xiazai" href="/routeagency/routemanage/routeview.aspx?routeSource=2" onclick="topTab.open($(this).attr('href'),'我的线路库');return false;">
                                                                    立即免费发布</a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td width="57%" align="left">
                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="15%" valign="middle" align="center" rowspan="4">
                                                                <img width="75" height="61" src="<%=ImageServerPath %>/images/impixo3.gif">
                                                            </td>
                                                            <td width="85%" align="left">
                                                                <a href="/teamservice/teamorders.aspx?routeSource=2" class="link14" rel="toptab"
                                                                    onclick="topTab.open($(this).attr('href'),'团队订单管理');return false;">我的订单</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" height="23" class="huise">
                                                                <asp:Label ID="lblLocalOrderCount" runat="server" Text="暂无订单。"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" height="10" class="huise">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" height="10" class="huise">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="bottom" align="right" height="21" colspan="2">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="59%" align="right">
                                            </td>
                                            <td width="41%" align="right">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ul>
                            <ul id="ul_Scenic" runat="server" visible="false">
                                <table width="100%" cellspacing="0" cellpadding="6" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <span class="shenghong2">
                                                    <img src="<%=ImageServerPath %>/images/gantanhao.gif">
                                                    <asp:Label ID="lblScenicCountOver" runat="server" Text="Label"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" height="86">
                                                <table width="60%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="27%" valign="middle" align="center" rowspan="2">
                                                                <img width="75" height="62" src="<%=ImageServerPath %>/images/impixo1.gif">
                                                            </td>
                                                            <td width="73%" align="left">
                                                                <a class="link14" href="/ScenicManage/MyScenice.aspx" onclick="topTab.open('/ScenicManage/MyScenice.aspx','我的景区');return false;">
                                                                    <b>我的景区</b></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="left" style="line-height: 120%;" class="huise">
                                                                <asp:Label ID="lblScenicCount" runat="server" Text="当前您还没有发布景区!"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <a class="xiazai" href="/ScenicManage/MyScenice.aspx" onclick="topTab.open('/ScenicManage/MyScenice.aspx','我的景区');return false;">
                                                                    立即免费发布</a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="59%" align="right">
                                            </td>
                                            <td width="41%" align="right">
                                                <a class="general" href="#"></a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ul>
                        </td>
                    </tr>
                </table>
            </dd>
        </dl>
    </div>
    <div class="tixing" style="<%= strStyle%>">
        <uc1:exchangelist ID="exchangelist" runat="server" />
        <div style="clear: both">
        </div>
    </div>
    <div style="width: 49%; float: left; height: 150px; margin-top: 10px; position: relative"
        class="company_gg">
        <dl>
            <dt><a class="tixingActive"><strong>网站公告</strong></a><span class="mor"><a target="_blank"
                href="<%= EyouSoft.Common.Domain.UserPublicCenter + "/newsList_-" + Convert.ToString((int)EyouSoft.Model.NewsStructure.NewsCategory.用户后台公告)%>">更多&gt;&gt;</a></span></dt>
            <dd>
                <ul>
                    <asp:Repeater ID="rptGG" runat="server">
                        <ItemTemplate>
                            <li><a target="_blank" href="<%# EyouSoft.Common.Domain.UserPublicCenter + "/newsDetail_" + Eval("AfficheClass") + "_" + Eval("Id") %>">
                                <%#Eval("AfficheTitle") == null ? string.Empty : EyouSoft.Common.Utils.GetText2(Eval("AfficheTitle").ToString(), 20, true)%></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </dd>
        </dl>
    </div>
    <div style="width: 49%; float: right; height: 150px; margin-top: 10px;" class="company_gg">
        <dl>
            <dt><a class="tixingActive" href="javascript:void(0);"><strong>同业资讯</strong></a><span
                class="gqadd" visible="false" id="spanOration" runat="server"><a href="javascript:void(0);"
                    onclick="return topTab.open('/generalshop/tongyenews/newslist.aspx','我的同业资讯');">发布同业资讯</a></span></dt>
            <dd>
                <ul>
                    <%=GetOrationByUser()%>
                </ul>
            </dd>
        </dl>
    </div>
    <div class="rili_box">
        <uc1:memocalendar ID="memocalendar1" runat="server" IsBackDefault="true" />
    </div>
</div>
<div class="rigbright">
    <iframe id="iframe1" frameborder="0" scrolling="no" marginwidth="0" style="height: 500px;
        width: 210px" marginheight="0" src="MQContent.aspx?MQID=<%= MQID %>"></iframe>
</div>

<script type="text/javascript">
    // Tab 切换
    function TabShow(tabCssName) {
        $("." + tabCssName + " dl dt>a:first").addClass("" + tabCssName + "Active");
        $("." + tabCssName + " dl dd ul").not(":first").hide();
        $("." + tabCssName + " dl dt>a").unbind("mouseover").bind("mouseover", function() {
            $(this).siblings("a").removeClass("" + tabCssName + "Active").end().addClass("" + tabCssName + "Active");
            var index = $("." + tabCssName + " dl dt>a").index($(this));
            $("." + tabCssName + " dl dd ul").eq(index).siblings("." + tabCssName + " dl dd ul").hide().end().show();
        });
        
        $("." + tabCssName + " dl dt>a").removeClass("" + tabCssName + "Active");
        $("." + tabCssName + " dl dt>a").eq(0).addClass("" + tabCssName + "Active");
        $("." + tabCssName + " dl dd ul:first").show();
    }
    $(function() {
        TabShow("impxx");
    })
    
	
</script>

