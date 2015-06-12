<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxLineLibraryList.aspx.cs"
    Inherits="UserBackCenter.TeamService.AjaxLineLibraryList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table id="table_list" border="1" align="center" cellpadding="1" cellspacing="0"
    bordercolor="#9dc4dc" style="width: 100%; margin-top: 1px;" class="liststylesp">
    <tr class="list_basicbg">
        <th>
            出发
        </th>
        <th style="width: 400px">
            线路名称
        </th>
        <th>
            专线品牌
        </th>
        <th>
            状态
        </th>
        <th>
            天数
        </th>
        <th>
            出团班次
        </th>
        <th>
            打印
        </th>
        <th>
            成人价
        </th>
        <th>
            功能
        </th>
    </tr>
    <asp:repeater runat="server" id="rpt_List">
        <ItemTemplate>
            <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                <td align="center" nowrap="nowrap">
                    <%#Eval("StartCityName")%>
                </td>
                <td align="left">
                    <a target="_blank" href='/PrintPage/RouteDetail.aspx?RouteId=<%#Eval("RouteId") %>'><%#Eval("RouteName")%></a><a href="javascript:void(0);"><img src="<%=ImgURL %>/images/MQWORD.gif"
                        align="middle" /></a>
                </td>
                <td align="center" nowrap="nowrap">
                    <a target="_blank" href="<%#Eval("Publishers")!=null?EyouSoft.Common.Utils.GetCompanyDomain(Eval("Publishers").ToString(),(EyouSoft.Model.CompanyStructure.CompanyType)Eval("CompanyType")):"javascript:void(0);" %>"><%#Eval("CompanyBrand")%></a><%#EyouSoft.Common.Utils.GetCompanyLevImg((EyouSoft.Model.CompanyStructure.CompanyLev)Eval("CompanyLev"))%>
                </td>
                <td align="center" nowrap="nowrap">
                   <span class="state<%#(int)Eval("RecommendType")-1%>"><%#Eval("RecommendType").ToString() == "无" ? "" : Eval("RecommendType")%></span>
                </td>
                <td align="center" nowrap="nowrap">
                    <%#Eval("Day") %>
                </td>
                <td align="center">
                    <a href="/TeamService/PlanList.aspx?routeId=<%#Eval("RouteId") %>"><%#Eval("TeamPlanDes")%></a><br />
                </td>
                <td align="center" nowrap="nowrap">
                    <a target="_blank" href='/PrintPage/LineTourInfo.aspx?RouteId=<%#Eval("RouteId") %>'>线路行程单</a>
                </td>
                <td align="center" nowrap="nowrap">
                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                </td>
                <td align="center" nowrap="nowrap" class="list-caozuo">
                    <span style="display:<%#((string)Eval("TeamPlanDes")).Length<=0?"none":""%>"><a href="/TeamService/PlanList.aspx?routeId=<%#Eval("RouteId") %>&areaId=<%=areaId %>&type=<%=type %>">计划列表</a><br /></span>
                    <a class="a_SingleGroupPre" href="/TeamService/SingleGroupPre.aspx?routeId=<%#Eval("RouteId") %>&isZT=true">单团预订</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:repeater>
</table>
<asp:panel id="pnlNodata" runat="server" visible="false">
     <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
        <tr>
            <td>暂无线路数据!&nbsp;&nbsp;</td>
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
