<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsListText.aspx.cs" Inherits="UserPublicCenter.Information.NewsListText"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<%@ Register Src="../WebControl/InfomationControl/InfoRight.ascx" TagName="InfoRight"
    TagPrefix="uc2" %>
<%@ Register Src="../WebControl/InfomationControl/InfoFoot.ascx" TagName="InfoFoot"
    TagPrefix="uc3" %>
<%@ Register Src="../WebControl/InfomationControl/AllCountryTourInfo.ascx" TagName="AllCountryTourInfo"
    TagPrefix="uc4" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRouteRecommend"
    TagPrefix="uc5" %>
<%@ Register Src="../WebControl/InfomationControl/InfomationBanner.ascx" TagName="InfomationBanner"
    TagPrefix="uc6" %>
<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("InformationStyle") %>" />
    <uc1:CityAndMenu ID="CityAndMenu1" HeadMenuIndex="7" runat="server" />
    <div>
        <uc6:InfomationBanner ID="InfomationBanner1" runat="server" />
    </div>
    <!--新闻列表开始-->
    <div id="news-list-ad">
        <a href="#" title="#">
            <img src="news/images/news-list_03.gif" alt="#" /></a></div>
    <div class="body" style="overflow: hidden">
        <div id="news-list-left">
            <!--列表 start-->
            <div class="box">
                <div class="box-l">
                    <div class="box-r">
                        <div class="box-c">
                            <h3>
                                行业资讯</h3>
                        </div>
                    </div>
                </div>
                <div class="box-main">
                    <div class="box-content box-content-main">
                        <!--上分页 开始-->
                        <div class="pageNav">
                            <a href="#">首页</a> <span class="na">上一页</span> <strong>1</strong> <a href="#">2</a>
                            <a href="#">3</a> <a href="#">4</a> <a href="#">5</a><span class="mor">...</span>
                            <a href="#">80</a> <a class="f12" href="#">下一页</a> <a class="f12" href="#">尾页</a>
                            <span class="clear1">转到</span><select><option>1</option>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                            </select>
                        </div>
                        <!--上分页 结束-->
                        <!--资讯 开始-->
                        <style>
                            </style>
                        <div class="recom">
                            <asp:Repeater ID="rptNews" runat="server">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <h2>
                                            <a href="#" title="#">
                                                <%# Eval("Title") %></a><span><%# Eval("NewsTime")%></span></h2>
                                        <p>
                                            <%# Eval("Content") %></p>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <!--资讯 结束-->
                        <!--下分页 开始-->
                        <div class="pageNav">
                            <a href="#">首页</a> <span class="na">上一页</span> <strong>1</strong> <a href="#">2</a>
                            <a href="#">3</a> <a href="#">4</a> <a href="#">5</a><span class="mor">...</span>
                            <a href="#">80</a> <a class="f12" href="#">下一页</a> <a class="f12" href="#">尾页</a>
                            <span class="clear1">转到</span><select><option>1</option>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                            </select>
                        </div>
                        <!--下分页 结束-->
                    </div>
                </div>
            </div>
            <!--列表 end-->
        </div>
        <!--右边 开始-->
        <uc2:InfoRight ID="InfoRight1" runat="server" />
        <!--右边 结束-->
        <div class="hr-10">
        </div>
        <!--下面四个 开始-->
        <uc3:InfoFoot ID="InfoFoot1" runat="server" />
        <!--下面四个 结束-->
        <div class="hr-10">
        </div>
        <!--全国旅游咨询开始 start-->
        <uc4:AllCountryTourInfo ID="AllCountryTourInfo1" runat="server" />
        <!--旅游咨询结束 end-->
    </div>
    <!--热门线路开始-->
    <!--bottom nav start-->
    <uc5:HotRouteRecommend ID="HotRouteRecommend1" runat="server" />
    <!--bottom nav end-->
    <!--热门线路结束-->
    <div class="friendlink">
        友情链接</div>
    <div class="friendlinkword">
        <a href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a> | <a href="#">中国之旅</a><a
            href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a> | <a href="#">中国之旅</a><a
                href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a> | <a href="#">中国之旅</a><a
                    href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a> | <a href="#">中国之旅</a><a
                        href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a> | <a href="#">中国之旅</a><a
                            href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a> |
        <a href="#">中国之旅</a><a href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a>
        | <a href="#">中国之旅</a><a href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a>
        | <a href="#">中国之旅</a><a href="#">搜狐</a> | <a href="#">新浪</a> | <a href="#">百度</a>
        | <a href="#">中国之旅</a></div>
    <div class="bottom">
        <div class="bottomleft">
            <a href="company.html">关于我们</a> | <a href="kefu.html">客服中心</a> | <a href="fuwushuoming.html">
                服务说明</a> | <a href="dailihezuo.html">代理合作</a> | <a href="job.html">招聘英才</a>
            | <a href="help/help_index.html">帮助中心</a> | <a href="yijian.html">提建议</a><br />
            杭州易诺科技有限 公司 版权所有 <a href="http://www.miibeian.gov.cn" target="_blank">浙ICP备 08009299号</a></div>
        <div class="bottomright">
            <img src="images/pingan.gif" /></div>
    </div>
    <div id="divStayTopright" style="position: absolute; z-index: 999">
        <a href="http://www.tongye114.com/AboutUsManage/Proposal.aspx" target="_blank">
            <img src="images/topleftvoid.gif" /></a>
    </div>

    <script type="text/javascript">
        var verticalpos = "frombottom"
        function JSFX_FloatTopDiv() {
            var startX = 0,
startY = 459;
            var ns = (navigator.appName.indexOf("Netscape") != -1);
            var d = document;
            function ml(id) {
                var el = d.getElementById ? d.getElementById(id) : d.all ? d.all[id] : d.divs[id];
                if (d.divs) el.style = el;
                el.sP = function(x, y) { this.style.right = x; this.style.top = y; };
                el.x = startX;
                if (verticalpos == "fromtop")
                    el.y = startY;
                else {
                    el.y = ns ? pageYOffset + innerHeight : document.documentElement.scrollTop +
document.documentElement.clientHeight;
                    el.y -= startY;
                }
                return el;
            }
            window.stayTopright = function() {
                if (verticalpos == "fromtop") {
                    var pY = ns ? pageYOffset : document.documentElement.scrollTop;
                    ftlObj.y += (pY + startY - ftlObj.y) / 8;
                }
                else {
                    var pY = ns ? pageYOffset + innerHeight : document.documentElement.scrollTop +
document.documentElement.clientHeight;
                    ftlObj.y += (pY - startY - ftlObj.y) / 8;
                }
                ftlObj.sP(ftlObj.x, ftlObj.y);
                setTimeout("stayTopright()", 10);
            }
            ftlObj = ml("divStayTopright");
            stayTopright();
        }
        JSFX_FloatTopDiv();
    </script>

</asp:Content>
