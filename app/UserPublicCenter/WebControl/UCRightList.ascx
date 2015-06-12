<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCRightList.ascx.cs"
    Inherits="UserPublicCenter.WebControl.UCRightList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<div class="yudingleft">
    <%if (IsSearch)
      { %>
    <div class="tujianxl_sou">
        <ul>
            <li>
                <label>
                    关 键 字：</label><input type="text" size="15" id="txtKeyWord" name="txtKeyWord" /></li>
            <li>
                <label>
                    出团日期：</label><input type="text" size="15" style="width: 65px;" id="txt_SDate" onfocus="WdatePicker({onpicked:function(){$('#ctl00_ContentPlaceHolder1_txt_RDate').focus();},minDate:'%y-%M-#{%d}'})" />-<input
                        type="text" size="15" style="width: 65px;" id="txt_EDate" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txt_SDate\')}'})" /></li>
            <li class="diann">
                <img width="85" id="BtnLSearch" height="30" align="middle" style="cursor: pointer"
                    alt="搜旅游" src="<%= EyouSoft.Common.Domain.ServerComponents %>/images/UserPublicCenter/xianllb_30.jpg" /></li>
        </ul>
    </div>
    <%} %>
    <%if (IsPinpai)
      { %>
    <div class="tujianxl">
        <div class="tujiantitle">
            推荐品牌</div>
        <ul>
            <asp:Repeater ID="rptPinPaiList" runat="server">
                <ItemTemplate>
                    <li><a target="_blank" href="<%#GetCompanyShopUrl(Eval("ID").ToString()) %>">
                        <img style="width: 100px; height: 67px;" class="tujianxl_img" src="<%#GetCompanyLogoSrc(Eval("AttachInfo"))%>" /></a><span
                            class="haitian"><a title="<%#GetCompanyBrandall(Eval("ID").ToString())%>" href="<%#GetCompanyShopUrl(Eval("ID").ToString()) %>">
                                <%#GetCompanyBrand(Eval("ID").ToString())%></a></span></li>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="rptPinPai" runat="server">
                <ItemTemplate>
                    <li><a target="_blank" href="<%#GetCompanyShopUrl(Eval("ID").ToString()) %>">
                        <img style="width: 100px; height: 67px;" class="tujianxl_img" src="<%#GetCompanyLogo(Eval("CompanyLogo"))%>" /></a><span
                            class="haitian"><a title="<%#GetCompanyBrandall(Eval("ID").ToString())%>" href="<%#GetCompanyShopUrl(Eval("ID").ToString()) %>">
                                <%#GetCompanyBrand(Eval("ID").ToString())%></a></span></li>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Literal ID="lbPinPai" runat="server" Visible="false"></asp:Literal>
        </ul>
    </div>
    <%} %>
    <div class="tujianxl2">
        <div class="tujiantitle2">
            推荐线路</div>
        <ul>
            <asp:Repeater ID="rptRecommendList" runat="server">
                <ItemTemplate>
                    <li><a target="_blank" title="<%#Eval("RouteName") %>" href="<%#EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(string.IsNullOrEmpty(Eval("Id").ToString()) ? 0 : long.Parse(Eval("Id").ToString()), CityID)%>">
                        ·<%# Utils.GetText2(Convert.ToString(Eval("RouteName")),15,false)%></a></li>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Literal ID="lbRecommend" runat="server" Visible="false"></asp:Literal>
        </ul>
    </div>
    <div class="tujianxl2">
        <div class="tujiantitle2">
            旅游资讯</div>
        <ul>
            <asp:Repeater ID="rptTraveNewList" runat="server">
                <ItemTemplate>
                    <li><a target="_blank" title="<%#Eval("AfficheTitle") %>" href="<%# EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(Utils.GetInt(Eval("AfficheClass").ToString()),Utils.GetInt(Eval("Id").ToString())) %>">
                        ·<%# Utils.GetText2(Eval("AfficheTitle").ToString(),15,true)%></a></li>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Literal ID="lbtravenew" runat="server" Visible="false"></asp:Literal>
        </ul>
    </div>
    <%if (IsToolbar)
      { %>
    <div class="tujianxl4">
        <div class="tujiantitle4">
            工具栏</div>
        <ul>
            <li><a target="_blank" href="http://xhjq.tongye114.com/">火车查询</a></li>
            <li><a target="_blank" href="http://www.travelsky.com/travelsky/static/home/">机票验证</a></li>
            <li><a target="_blank" href="http://www.ip138.com/post/">区号查询</a></li>
            <li><a target="_blank" href="http://weather.tq121.com.cn/">天气查询</a> </li>
            <li><a target="_blank" href="<%=EyouSoft.Common.Domain.UserPublicCenter%>/visa/Map.aspx">
                地图查询</a></li>
            <li><a target="_blank" href="http://www.ip138.com/sj/index.htm">手机查询</a></li>
            <li><a target="_blank" href="http://www.hao123.com/haoserver/kuaidi.htm">快递查询</a></li>
            <li><a target="_blank" href="http://www.ip138.com/post/">邮编查询</a></li>
        </ul>
    </div>
    <%} %>
    <div class="tujianxl3">
        <div class="tujianxl3li">
            <div class="tujiantitle3">
                加盟旅行社</div>
            <ul>
                <asp:Repeater ID="rptJoinTraveList" runat="server">
                    <ItemTemplate>
                        <li><a target="_blank" href="<%#GetCompanyShopUrl(Eval("ID").ToString()) %>">·
                            <%#Eval("CompanyName")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="lbjoinTrave" runat="server" Visible="false"></asp:Literal>
            </ul>
            <div class="tujianbg">
                <img src="<%= EyouSoft.Common.Domain.ServerComponents %>/images/yuding_41.jpg"></div>
        </div>
    </div>
</div>

<script type="text/javascript">
  $(function() {
  $("#BtnLSearch").click(function() {
            var RouteName = $.trim($("#txtKeyWord").val());
            var StartDate = $.trim($("#txt_SDate").val());
            var EndDate = $.trim($("#txt_EDate").val());
            var Url ="/TourManage/TourList.aspx?SearchType=More&keyWord="+encodeURIComponent(RouteName)+ "&StartDate=" + StartDate + "&EndDate=" + EndDate;
            window.location.href = Url;
        });
        });
  
</script>

