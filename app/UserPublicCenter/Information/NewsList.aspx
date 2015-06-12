<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="UserPublicCenter.Information.NewsList" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/InfomationControl/InfomationBanner.ascx" TagName="HeadMenu" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/InfomationControl/InfoRight.ascx" TagName="RightMenu" TagPrefix="uc3" %>
<%@ Register Src="../WebControl/InfomationControl/InfoFoot.ascx" TagName="FootMenu" TagPrefix="uc4" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRoute" TagPrefix="uc5" %>
<%@ Register Src="../HomeControl/FrindLinkList.ascx" TagName="linkMenu" TagPrefix="uc6" %>
<%@ Register Src="../WebControl/InfomationControl/AllCountryTourInfo.ascx" TagName="AllCountryMenu" TagPrefix="uc7" %>
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
    <!--新闻列表开始-->

    <uc2:HeadMenu ID="headMenu" runat="server" />
    
    <div class="hr-10"></div>
    <div class="body" style="overflow: hidden">
        <div id="news-list-left">
            <!--列表 start-->
            <div class="box">
                <div class="box-l">
                    <div class="box-r">
                        <div class="box-c">
                            <h3>
                                 <%= cateName %> <%= Request["TagId"] == null ? (className=="" ? "": ">"):"" %> <%= className %></h3>
                        </div>
                    </div>
                </div>
                <div class="box-main">
                    <div class="box-content box-content-main">
                        <!--上分页 开始-->
                                <cc1:exporpageinfoselect ID="ExportPageInfoUp" runat="server" PageStyleType="ClassicStyle" PageLinkCount="8" LinkType="3"
                                    CurrencyPageCssClass="RedFnt" />
                        <!--上分页 结束-->
                        <!--咨询头条 开始-->
                        <div class="recom">
                            <asp:Repeater ID="rptTopNews" runat="server">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <a href="<%# String.IsNullOrEmpty(Eval("GotoUrl").ToString()) ? EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(Request["TypeId"] == null ? int.Parse(Request["TagId"].ToString()):int.Parse(Request["TypeId"].ToString()),int.Parse(Eval("Id").ToString())):Eval("GotoUrl") %>"  class="img" target="_blank">
                                              <%# String.IsNullOrEmpty(Eval("PicPath").ToString()) ? "" : string.Format("<img src='{0}' alt='' />", EyouSoft.Common.Domain.FileSystem + Eval("PicPath").ToString())%>
                                              <%--<img src="<%# Eval("PicPath") %>" alt="" />--%>
                                        </a>
                                        <h2>
                                            <a href="<%# String.IsNullOrEmpty(Eval("GotoUrl").ToString()) ? EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(int.Parse(Eval("AfficheClass").ToString()),int.Parse(Eval("Id").ToString())):Eval("GotoUrl") %>"  target="_blank">
                                                <font color="<%# Eval("TitleColor") %>"><%# EyouSoft.Common.Utils.LoseHtml(Eval("AfficheTitle").ToString(), 20)%></font>
                                            </a>
                                            <span><%# Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd")%></span>
                                        </h2>
                                        <p><%# EyouSoft.Common.Utils.GetText2(EyouSoft.Common.Utils.LoseHtml(Eval("AfficheContent").ToString()), 72,true) %></p>
                                        <div style="clear:both;"></div>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <!--咨询头条 结束-->
                        <!--咨询列表5条一分割 开始-->
                        <div class="news-list">
                            <asp:Repeater ID="rptNewsList" runat="server">
                                <ItemTemplate>
                                    <%# (Container.ItemIndex+1)%5 == 1 ? "<ul>":"" %>
                                    <li>
                                        <input type="hidden" value="<%# Eval("Id") %>" /><span><%# Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd")%></span><a
                                            href="<%# String.IsNullOrEmpty(Eval("GotoUrl").ToString()) ? EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(int.Parse(Eval("AfficheClass").ToString()),int.Parse(Eval("Id").ToString())):Eval("GotoUrl") %>"  target="_blank">
                                            <font color="<%# Eval("TitleColor") %>"><%# EyouSoft.Common.Utils.LoseHtml(Eval("AfficheTitle").ToString(), 30)%></font></a>
                                    </li>
                                    <%# (Container.ItemIndex+1)%5 == 0 ? "</ul>":"" %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        
                    <div class="recom" style=" color:#074387">
                        <% 
                           if (lsNews.Count <= 0)
                           { 
                        %>
                            <span>没有相关的资讯消息...</span>
                            <span id="jumpTo">5</span>秒后自动跳转到<%= Infomation.InfoDefaultUrlWrite()%>
                            <script type="text/javascript">countDown(5, '<%= Infomation.InfoDefaultUrlWrite() %>');</script>
                        <%
                           } 
                        %>
                    </div>

                        <!--下分页 开始-->
                                <cc1:exporpageinfoselect ID="ExportPageInfo" runat="server" LinkType="3" PageStyleType="ClassicStyle" PageLinkCount="8"
                                    CurrencyPageCssClass="RedFnt" />
                        <!--下分页 结束-->
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
        
        <uc4:FootMenu ID="footMenu" runat="server" />
        
        <div class="hr-10">
        </div>
        <!--全国旅游列表 start-->
        <uc7:AllCountryMenu ID="allCountryMenu" runat="server" />
        <!--全国旅游列表 end-->
    </div>
    <!--新闻列表结束-->
    
    <!--bottom nav start-->
        <div class="hr-10"></div>
        <uc5:HotRoute ID="hotRoute" runat="server" />
    <!--bottom nav end-->

</asp:Content>
