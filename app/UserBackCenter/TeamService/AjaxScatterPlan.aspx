<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxScatterPlan.aspx.cs"
    Inherits="UserBackCenter.TeamService.AjaxScatterPlan" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
    style="width: 100%; margin-top: 1px;" class="liststylesp">
    <tr class="list_basicbg">
        <th nowrap="nowrap" class="list_basicbg">
            出发
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            团号
        </th>
        <th nowrap="nowrap" class="list_basicbg" style="width: 400px">
            线路名称
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            状态
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            天数
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            出团日期
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            报名截止
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            人数
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            余位
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            成人价
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            儿童价
        </th>
        <th nowrap="nowrap" class="list_basicbg ShowJSPrice" style="display: none">
            结算价(成人/儿童)
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            功能
        </th>
    </tr>
    <asp:repeater runat="server" id="rpt_list">
    <ItemTemplate>
         <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
            <td align="center" nowrap="NOWRAP">
                <%#Eval("StartCityName")%>
            </td>
            <td align="left" nowrap="nowrap">
                <%#EyouSoft.Common.Utils.GetCompanyLevImg((EyouSoft.Model.CompanyStructure.CompanyLev)Eval("CompanyLev"))%><%#Eval("TourNo")%>
            </td>
            <td align="left">
                <a target="_blank" href='/PrintPage/TeamRouteDetails.aspx?TeamId=<%#Eval("TourId") %>'><%#Eval("RouteName")%></a><%=EyouSoft.Common.Utils.GetMQ(EyouSoft.Common.Utils.GetQueryStringValue("MQ"))%>
            </td>
            <td align="center" nowrap="NOWRAP">
               <span class="state<%#(int)Eval("RecommendType")-1 %>"><%#Eval("RecommendType").ToString() == "0" || Eval("RecommendType").ToString() =="无"? "" : Eval("RecommendType")%></span> 
            </td>
            <td align="center" nowrap="nowrap">
                <%#Eval("Day")%>
            </td>
            <td align="center">
               <%#((DateTime)Eval("LeaveDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("LeaveDate")).ToString("ddd")%>)
            </td>
            <td align="center">
                <span class="ff0000"><%#((DateTime)Eval("RegistrationEndDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("RegistrationEndDate")).ToString("ddd")%>)</span>
            </td>
            <td align="center" nowrap="nowrap">
                <%#Eval("TourNum")%>
            </td>
            <td align="center" nowrap="nowrap">
                <%# Eval("IsLimit").ToString().ToLower()=="true"?"∞": Eval("MoreThan")%>
            </td>
            <td align="center" nowrap="nowrap">
                <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
            </td>
            <td align="center" nowrap="nowrap">

                <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
            </td>
            <td align="center" nowrap="NOWRAP" class="ShowJSPrice"  style="display:none">
               <span class="ff0000"> <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString())%></span>
                /
                <span class="ff0000"><%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString())%></span>
            </td>
            <td align="center" nowrap="NOWRAP">
                <%#((int)Eval("PowderTourStatus") == 1 || (int)Eval("PowderTourStatus") == 2 ? Eval("PowderTourStatus") : GetButt(Eval("TourId").ToString()))%>
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
