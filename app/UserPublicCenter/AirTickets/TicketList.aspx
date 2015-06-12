<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" AutoEventWireup="true" CodeBehind="TicketList.aspx.cs" Inherits="UserPublicCenter.AirTickets.TicketList" %>
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
    <uc1:CityAndMenu ID="CityAndMenu1" HeadMenuIndex="3" runat="server" />
    <!--新闻列表开始-->
    <div class="hr-10"></div>
    <div class="body" style="overflow: hidden">
        <div id="news-list-left">
            <!--列表 start-->
            <div class="box">
                <div class="box-l">
                    <div class="box-r">
                        <div class="box-c">
                            <h3>首页　>　特价机票</h3>
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
                        </div>
                        <!--咨询头条 结束-->
                        <!--咨询列表5条一分割 开始-->
                        <div class="news-list">
                            
                            <%if (sflist != null && sflist.Count > 0)
                              {
                                  int temp = 0;
                                  foreach (EyouSoft.Model.TicketStructure.SpecialFares sf in sflist)
                                  { %>
                                  <%if (temp % 5 == 0)
                                    { %>
                                    <ul>
                                  <%} %>
                                    <li>
                                        <span><% =sf.AddTime.ToString("yyyy-MM-dd")%></span>
                                        <a href="<%= sf.IsJump?( IsLogin?"/PlaneInfo/PlaneListPage.aspx":"/AirTickets/Login.aspx?return=PlaneListPage" ):EyouSoft.Common.URLREWRITE.Plane.SpecialFaresUrl(sf.ID, CityId) %>"  target="_blank">
                                        <font >[<%=sf.SpecialFaresType.ToString()%>]<%= EyouSoft.Common.Utils.LoseHtml(sf.Title, 30)%></font></a>
                                    </li>
                                  <%if ((temp+1) % 5 == 0)
                                    { %>
                                    </ul>
                                  <%} temp++; %>
                                  
                            <%}
                                  if ((temp + 1) % 5 == 0)
                                  {%>
                                  </ul>
                                  <% }
                              } %>
                        
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

