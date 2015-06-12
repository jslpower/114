<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxOrderListOfCompany.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.AjaxOrderListOfCompany" %>

<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>
<table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" class="tab_luand">
    <tr>
        <td colspan="5" align="center" height="15">
            共预订<span id="Span1"><asp:literal runat="server" id="ltrCount"></asp:literal></span>
            次，当前页共<span id="Span2"><asp:literal runat="server" id="ltrRouteCompanyNum"></asp:literal></span>
            家批发商产品
        </td>
    </tr>
    <tr class="tab_luan">
        <th class="font12_writh"  style="width:36px;">
            序号
        </th>
        <th class="font12_writh"  style="width:70px;" >
            <a href="javascript:void(0)" <%=GetAboutOrderInfo(0) %>>
                时间</a>
        </th>
        <th class="font12_writh"   style="width:260px;">
            <a href="javascript:void(0)" <%=GetAboutOrderInfo(1) %>>
                团号/线路名称</a>
        </th>
        <th class="font12_writh" style="width:80px;">
            <a href="javascript:void(0)" <%=GetAboutOrderInfo(2) %>>
                预订人数</a>
        </th>
        <th class="font12_writh"  style="width:80px;">
            <a href="javascript:void(0)" <%=GetAboutOrderInfo(3) %>>
                订单状态</a>
        </th>
        <th class="font12_writh"   style="width:160px;">
            <a href="javascript:void(0)" <%=GetAboutOrderInfo(4) %>>
                所属批发商</a>
        </th>
    </tr>
    <tr runat="server" id="NoData">
        <td colspan="5" align="center" height="30">
            暂无符合你查找条件的预订信息！
        </td>
    </tr>
    <asp:repeater runat="server" id="rpt_CompanyOrderList" onitemdatabound="rpt_CompanyOrderList_ItemDataBound">
                <ItemTemplate>
                    <tr class="lr_hangbg">
                        <td align="center">
                            <asp:Literal ID="ltrXH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td align="left">
                            【<%#Eval("TourNo")%>】<a href="<%#EyouSoft.Common.Utils.GetTeamInformationPagePath(Eval("TourID").ToString()) %>" target="_blank">
                                <%#Eval("RouteName")%></a>
                        </td>
                        <td align="center">
                            <%#Eval("PeopleNumber")%>人
                        </td>
                        <td align="center">
                            <span class="font12_writh"><%#Eval("OrderState")%></span>
                        </td>
                        <td align="left">
                            <a href='/Statistics/CompanyInfo.aspx?companyId=<%#Eval("TourCompanyId") %>'  class="TourCompanyName"><%#Eval("TourCompanyName")%></a>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="lr_hangbg2">
                         <td align="center">
                            <asp:Literal ID="ltrXH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td align="left">
                            【<%#Eval("TourNo")%>】<a href="<%#EyouSoft.Common.Utils.GetTeamInformationPagePath(Eval("TourID").ToString()) %>" target="_blank">
                                <%#Eval("RouteName")%></a>
                        </td>
                        <td align="center">
                            <%#Eval("PeopleNumber")%>人
                        </td>
                        <td align="center">
                            <span class="font12_writh"><%#Eval("OrderState")%></span>
                        </td>
                        <td align="left">
                            <a href='/Statistics/CompanyInfo.aspx?companyId=<%#Eval("TourCompanyId") %>' class="TourCompanyName"><%#Eval("TourCompanyName")%></a>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:repeater>
</table>
<div style="text-align: right;" id="div_AjaxOrderListOfCompany">
    <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
</div>
<input id="hidAjaxOrderListOfCompany_pageindex" value="<%=PageIndex %>" type="hidden" />

