<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTeamOrders.aspx.cs"
    Inherits="UserBackCenter.TeamService.AjaxTeamOrders" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
    class="liststylesp">
    <tr class="list_basicbg">
        <th class="list_basicbg">
            出发城市
        </th>
        <th class="list_basicbg">
            线路名
        </th>
        <th class="list_basicbg">
            团队联系人
        </th>
        <th class="list_basicbg">
            出发时间
        </th>
        <th class="list_basicbg">
            预订时间
        </th>
        <th class="list_basicbg">
            人数
        </th>
        <th class="list_basicbg">
            状态
        </th>
        <th class="list_basicbg">
            操作
        </th>
    </tr>
    <asp:repeater runat="server" id="rpt_list">
                                    <ItemTemplate>
                                        <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                                            <td height="30" align="left">
                                               <%#Eval("StartCityName")%>
                                            </td>
                                            <td align="left">
                                               <a href='/PrintPage/LineTourInfo.aspx?RouteId=<%#Eval("RouteId") %>' target="_blank"> <%#Eval("RouteName")%></a><br />
                                                <%if (routeSource == null)
                                                  { %>
                                                专线商：<a target="_blank" href="<%#Eval("CompanyType")!=null?EyouSoft.Common.Utils.GetCompanyDomain(Eval("Business").ToString(),(EyouSoft.Model.CompanyStructure.CompanyType)Eval("CompanyType")):"javascript:void(0);" %>"><%#Eval("BusinessName")%></a>
                                                <%}
                                                  else
                                                  { %>
                                                组团社：<a target="_blank" href="<%#Eval("CompanyType")!=null?EyouSoft.Common.Utils.GetCompanyDomain(Eval("Travel").ToString(),(EyouSoft.Model.CompanyStructure.CompanyType)Eval("CompanyType")):"javascript:void(0);" %>"><%#Eval("TravelName")%></a>
                                                <%} %>
                                            </td>
                                            <td align="left">
                                                 <%#Eval("TravelContact")%><br />
                                                 <%#Eval("TravelTel")%>
                                            </td>
                                            <td align="center">
                                                 <%#((DateTime)Eval("StartDate")).ToString("MM-dd")%>
                                            </td>
                                            <td align="center">
                                                 <%#((DateTime)Eval("IssueTime")).ToString("MM-dd HH:mm")%>
                                            </td>
                                            <td align="center">
                                                 <%#Eval("ScheduleNum")%>
                                            </td>
                                            <td align="center">
                                                 <%#Eval("OrderStatus")%>
                                            </td>
                                            <td align="center">
                                                <a class="a_show" href="/TeamService/SingleGroupPre.aspx?tourId=<%#Eval("TourId") %>">查看</a><br />
                                                <a style="display:<%#((int)Eval("OrderStatus"))==3?"none":""%>" class="a_TourOrderStatusChange" href="/TeamService/TourOrderStatusChange.ashx?tourId=<%#Eval("TourId") %>&intStatus=3">取消</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:repeater>
</table>
<asp:panel id="pnlNodata" runat="server" visible="false">
                     <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                        <tr>
                            <td>暂无线路数据!</td>
                        </tr>
                     </table>
                 </asp:panel>
<table id="ExportPageInfo" cellspacing="0" cellpadding="0" width="98%" align="right"
    border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
            <cc1:ExportPageInfo ID="ExportPageInfo1" Visible="false" LinkType="4" runat="server" />
        </td>
    </tr>
</table>
