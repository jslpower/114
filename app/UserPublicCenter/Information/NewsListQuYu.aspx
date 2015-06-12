<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsListQuYu.aspx.cs" Inherits="UserPublicCenter.Information.NewsListQuYu" 
 MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"%>

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
<div class="body" style=" overflow:hidden">
  <div id="news-list-left">
    <!--列表 start-->
    <div class="box">
      <div class="box-l">
        <div class="box-r">
          <div class="box-c">

            <h3>全国旅游资讯 ><%= provinceName %></h3>
          </div>
        </div>
      </div>
      <div class="box-main">
        <div class="box-content box-content-main">
            <!--下分页 开始-->
                    <cc1:exporpageinfoselect ID="ExportPageInfoUp" runat="server" LinkType="3" PageStyleType="ClassicStyle"
                        CurrencyPageCssClass="RedFnt" />
            <!--下分页 结束-->
                        
          <!--资讯 开始-->
          <style>
       
       </style>
        <div class="box-main" style=" color:#074387">
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
          <div class="recom">
                <asp:Repeater ID="rptQuYu" runat="server">
                    <HeaderTemplate>
                        <ul class="news-list-text">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                        <h2><%= provinceName %><a href="<%# EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(int.Parse(Eval("AfficheClass").ToString())) %>" >【<%# Eval("ClassName")%>】</a> 
                            <a href="<%# String.IsNullOrEmpty(Eval("GotoUrl").ToString()) ? EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(int.Parse(Eval("AfficheClass").ToString()),int.Parse(Eval("Id").ToString())):Eval("GotoUrl") %>"  target="_blank">
                                <font color="<%# Eval("TitleColor") %>"><%# EyouSoft.Common.Utils.LoseHtml(Eval("AfficheTitle").ToString(),19) %></font>
                            </a>
                            <span><%# Convert.ToDateTime(Eval("UpdateTime")).ToString("yyyy-MM-dd") %></span>
                        </h2>
                        <p><%# EyouSoft.Common.Utils.GetText2(EyouSoft.Common.Utils.LoseHtml(Eval("AfficheContent").ToString()), 147,true) %></p>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
          </div>
          <!--资讯 结束-->
            <!--下分页 开始-->
                    <cc1:exporpageinfoselect ID="ExportPageInfoDown" runat="server" LinkType="3" PageStyleType="ClassicStyle"
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
  
  <div class="hr-10"></div>
  
  <uc4:FootMenu ID="footMenu" runat="server" />
  
  <div class="hr-10"></div>

  <!--全国旅游列表 start-->
    <uc7:AllCountryMenu ID="allCountryMenu" runat="server" />
  <!--全国旅游列表 end-->
</div>
<!--新闻列表结束-->
<!--bottom nav start-->
<div class="hr-10"></div>
<uc5:HotRoute ID="hotRoute" runat="server" />
<!--bottom nav end-->

    <div class="hr-10"></div>
    <uc6:linkMenu ID="linkMenu" runat="server" />



</asp:Content>
