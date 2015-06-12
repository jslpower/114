<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="UserPublicCenter.Information.NewsDetail"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/InfomationControl/InfomationBanner.ascx" TagName="HeadMenu"
    TagPrefix="uc2" %>
<%@ Register Src="../WebControl/InfomationControl/InfoRight.ascx" TagName="RightMenu"
    TagPrefix="uc3" %>
<%@ Register Src="../WebControl/InfomationControl/InfoFoot.ascx" TagName="FootMenu"
    TagPrefix="uc4" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRoute"
    TagPrefix="uc5" %>
<%@ Register Src="../HomeControl/FrindLinkList.ascx" TagName="linkMenu" TagPrefix="uc6" %>
<%@ Register Src="../WebControl/InfomationControl/AllCountryTourInfo.ascx" TagName="AllCountryMenu"
    TagPrefix="uc7" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">

    <script type="text/javascript">
            function countDown(secs, surl) {
                //alert(surl);    
                var jumpTo = document.getElementById('jumpTo');
                jumpTo.innerHTML = secs;
                if (--secs > 0) {
                    setTimeout("countDown(" + secs + ",'" + surl + "')", 1000);
                }
                else {
                    window.location.href = surl;
                }
            }    
    </script>

    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("InformationStyle") %>" />
    <uc1:CityAndMenu ID="CityAndMenu1" HeadMenuIndex="7" runat="server" />
    <uc2:HeadMenu ID="headMenu" runat="server" />
    <div class="hr-10">
    </div>
    <div class="body" style="overflow: hidden">
        <div id="news-list-left" class="addBg">
            <!--列表 start-->
            <div class="box">
                <div class="box-main">
                    <div class="box-content box-content-main box-content-read">
                        <!--咨询详细 开始-->
                        <div class="news-read">
                            <%
                                if (model != null)
                                { 
                            %>
                            <div class="bread-nav">
                                <p>
                                    <span><a href="<%= EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(-(int)model.AfficheCate) %>">
                                        <%= cateName %></a></span> <span>&gt;</span> <a href="<%= EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(int.Parse(Request["TypeId"].ToString())) %>">
                                            <%= model.ClassName%></a> <span>&gt;</span> 正文
                                </p>
                            </div>
                            <div class="title">
                                <h1>
                                    <font color="<%= model.TitleColor %>">
                                        <%= EyouSoft.Common.Utils.LoseHtml(model.AfficheTitle,30) %></font></h1>
                                <p>
                                    <span>
                                        <%= Convert.ToDateTime(model.IssueTime).ToString("yyyy年MM月dd日 HH:mm")%></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;来源：<span><%= model.AfficheSource%></span></p>
                            </div>
                            <div class="content">
                                <%= model.ContentInfo == null ? "":model.ContentInfo.Content %>
                                <!-- c -->
								<p><span>
<span><img src="http://files.tongye114.com/File/SiteOperation/NewsCenter/2011/8/201108031125233036242_3c7df0b3-ba79-4afd-9ad1-0c91bbb673e9.jpg" style="width: 113px; height: 60px;" /></span>
<span style="font-size: 20px;color: rgb(51, 102, 255);">&nbsp;接单揽生意，就得安装MQ</span>
<span style="font-size: 22px;"><a href="http://im.tongye114.com">旅游同行的MQ免费下载地址</a></span>
<br />
  <span style="font-size: 18px;color: rgb(255, 0, 0)">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;烦死了，MQ上面天天有人找地接，生意好到爆！再不装个MQ生意全被抢走了！</span>
 </p>
								<!-- c -->
                            </div>
                            <div style="text-align: center; padding-top: 20px; padding-bottom: 20px; font-weight: 700;
                                font-size: 14px;">
                                <cc1:ExporPageInfoSelect ID="ExportPageInfo" runat="server" LinkType="3" PageStyleType="ContentStyle"
                                    CurrencyPageCssClass="RedFnt" />
                            </div>
                            <div style="text-align: left; padding-top: 20px; padding-bottom: 20px; font-weight: 700;
                                font-size: 14px;">
                                <!-- JiaThis Button BEGIN -->
                                <div id="jiathis_style_32x32" style="clear: both; width: 600px; text-align: center;
                                    margin-bottom: 10px;">
                                    <a class="jiathis_button_tsina"></a><a class="jiathis_button_qzone"></a><a class="jiathis_button_xiaoyou">
                                    </a><a class="jiathis_button_tqq"></a><a class="jiathis_button_kaixin001"></a><a
                                        class="jiathis_button_renren"></a><a class="jiathis_button_baidu"></a><a class="jiathis_button_taobao">
                                        </a><a href="http://www.jiathis.com/share/" class="jiathis jiathis_txt jtico jtico_jiathis"
                                            target="_blank"></a>
                                </div>

                                <script type="text/javascript" src="http://v1.jiathis.com/code/jia.js" charset="utf-8"></script>

                                <!-- JiaThis Button END -->
                            </div>
                            <div class="news-read-footter">
                                <p>
                                    <span class="left"><b><a href="<%= EyouSoft.Common.URLREWRITE.Infomation.GetTagListUrl() %>">
                                        Tags：</a></b>
                                        <%
                                            for (int i = 0; i < model.NewsTagItem.Count; i++)
                                            {
                                                int tagId = model.NewsTagItem[i].ItemId;
                                        %>
                                        <a href="<%= EyouSoft.Common.URLREWRITE.Infomation.GetOtherListUrl(tagId) %>">
                                            <%= model.NewsTagItem[i].ItemName%></a>
                                        <%
                                            } 
                                        %>
                                    </span><span class="right"><b>责任编辑：</b><span><%= model.AfficheAuthor%></span></span>
                                    <p>
                                        <span class="left">
                                            <%
                                                if (preModel != null)
                                                { 
                                            %>
                                            <b>上一篇：</b> <a href="<%= String.IsNullOrEmpty(preModel.GotoUrl) ? EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(int.Parse(Request["TypeId"].ToString()),preModel.Id): preModel.GotoUrl %>">
                                                <%= preModel == null ? "" : EyouSoft.Common.Utils.LoseHtml(preModel.AfficheTitle.ToString(), 17)%>
                                            </a>
                                            <%
                                                } 
                                            %>
                                        </span><span class="right">
                                            <%
                                                if (nextModel != null)
                                                {
                                            %>
                                            <b>下一篇：</b> <span><a href="<%= String.IsNullOrEmpty(nextModel.GotoUrl) ? EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(int.Parse(Request["TypeId"].ToString()),nextModel.Id):nextModel.GotoUrl %>">
                                                <%= nextModel == null ? "" : EyouSoft.Common.Utils.LoseHtml(nextModel.AfficheTitle.ToString(), 17)%></a>
                                            </span>
                                            <%
                                                } 
                                            %>
                                        </span>
                                    </p>
                            </div>
                            <%
                                }
            else
            { 
                            %>
                            <div class="bread-nav" style="color: #074387">
                                <br />
                                <span>没有此资讯的消息...</span> <span id="jumpTo">5</span>秒后自动跳转到<%= Infomation.InfoDefaultUrlWrite() %>

                                <script type="text/javascript">countDown(5, '<%= Infomation.InfoDefaultUrlWrite() %>');</script>

                            </div>
                            <%
                                } 
                            %>
                            <div class="hr-10">
                            </div>
                            <!--相关咨询 开始-->
                            <div class="news-list news-list-relative">
                                <h3>
                                    相关资讯：</h3>
                                <asp:Label ID="NoRelateNews" runat="server" Visible="false" ForeColor="Red" Font-Size="Larger"
                                    Text=""></asp:Label>
                                <asp:Repeater ID="rptRelatedNews" runat="server">
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li><a href="<%# String.IsNullOrEmpty(Eval("GotoUrl").ToString()) ? EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(int.Parse(Request["TypeId"].ToString()),int.Parse(Eval("Id").ToString())):Eval("GotoUrl") %>">
                                            <font color="<%# Eval("TitleColor") %>">
                                                <%# EyouSoft.Common.Utils.LoseHtml(Eval("AfficheTitle").ToString(),18) %></font>
                                        </a><span>
                                            <%# Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd") %></span> </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <!--相关咨询 结束-->
                            <!--推荐资讯： 开始-->
                            <div class="news-list news-list-relative news-list-relative-img">
                                <div class="box-c box-c-clear">
                                    <h3 class="add">
                                        <span><a href="<%= EyouSoft.Common.URLREWRITE.Infomation.GetNewsListZhuanTiUrl(0) %>">
                                            更多></a></span>推荐资讯：</h3>
                                </div>
                                <asp:Label ID="NoRecommendNews" runat="server" Visible="false" ForeColor="Red" Font-Size="Larger"
                                    Text=""></asp:Label>
                                <asp:Repeater ID="rptRecommendNews" runat="server">
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <%--ImageServerPath--%>
                                            <a href="<%# String.IsNullOrEmpty(Eval("GotoUrl").ToString()) ? EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(int.Parse(Request["TypeId"].ToString()),int.Parse(Eval("Id").ToString())) :Eval("GotoUrl") %>">
                                                <img style="width: 120px; height: 100px;" src="<%# EyouSoft.Common.Domain.FileSystem+Eval("PicPath") %>"
                                                    alt="#" />
                                                <p>
                                                    <font color="<%# Eval("TitleColor") %>">
                                                        <%# EyouSoft.Common.Utils.LoseHtml(Eval("AfficheTitle").ToString(),10) %></font></p>
                                            </a></li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <!--推荐资讯： 结束-->
                        </div>
                        <!--咨询列表 结束-->
                    </div>
                </div>
            </div>
            <!--列表 end-->
        </div>
        <!--右边 开始-->
        <uc3:RightMenu ID="rightMenu" runat="server" />
        <!--右边 结束-->
        <div class="hr-10">
        </div>
        <!--列表 start-->
        <uc7:AllCountryMenu ID="allCountry" runat="server" />
        <!--列表 end-->
    </div>
    <!--新闻详情结束-->
    <!--bottom nav start-->
    <div class="hr-10">
    </div>
    <uc5:HotRoute ID="hotRoute" runat="server" />
    <!--bottom nav end-->
</asp:Content>
