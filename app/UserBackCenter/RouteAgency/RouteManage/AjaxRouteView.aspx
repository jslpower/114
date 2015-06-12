<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxRouteView.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.RouteManage.AjaxRouteView" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table class="liststylesp" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
    style="width: 100%; margin-top: 1px;" class="liststyle" border="1">
    <tr class="list_basicbg">
        <th class="list_basicbg">
            <input type="checkbox" id="chk_All" />
            全
        </th>
        <th class="list_basicbg" style="width: 450px">
            线路名称
        </th>
        <th class="list_basicbg">
            出发
        </th>
        <th class="list_basicbg">
            状态
        </th>
        <th class="list_basicbg">
            天数
        </th>
        <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
          {%>
        <th class="list_basicbg">
            计划班次
        </th>
        <th class="list_basicbg">
            成人价
        </th>
        <th class="list_basicbg">
            儿童价
        </th>
        <%}
          else
          { %>
        <th class="list_basicbg">
            团队参考价
        </th>
        <%} %>
        <th class="list_basicbg">
            阅览
        </th>
        <th class="list_basicbg">
            打印
        </th>
        <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
          {%>
        <th class="list_basicbg">
            计划管理
        </th>
        <%} %>
        <th class="list_basicbg">
            操作
        </th>
    </tr>
    <asp:repeater id="rpt_List" runat="server">
                   <ItemTemplate>
                       <tr routeid="<%#Eval("RouteId") %>" routetype="<%#(int)Eval("RouteType") %>" <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                             <td align="center"><input  type="checkbox" class="list_id" value="<%#Eval("RouteId") %>" /></td>
                             <td align="left"><a href='/PrintPage/RouteDetail.aspx?RouteId=<%#Eval("RouteId") %>' target="_blank"><%#Eval("RouteName")%></a></td>
                             <td align="center"><%#EyouSoft.Common.Utils.GetText2( Eval("StartCityName").ToString(),18,false)%></td>
                             <td align="center"><span class="state<%#(int)Eval("RecommendType")-1%>"><%#Eval("RecommendType").ToString() == "无" ? "" : Eval("RecommendType")%></span></td>
                             <td align="center"><%#Eval("Day")%></td>
                             <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                               {%>
                             <td align="center"><a title="<%#Eval("TeamPlanDes")!=null&&Eval("TeamPlanDes").ToString().Length>0?"计划列表":"历史团队"%>" href="/RouteAgency/<%#Eval("TeamPlanDes")!=null&&Eval("TeamPlanDes").ToString().Length>0?"PlanList.aspx":"HistoryTeam.aspx" %>?routeId=<%#Eval("RouteId") %>" class="a_goPlanList"><%#Eval("TeamPlanDes") != null && Eval("TeamPlanDes").ToString().Length>0?Eval("TeamPlanDes"):""%></a></td>
                             <td align="center"><%#Eval("TeamPlanDes") != null && Eval("TeamPlanDes").ToString().Length > 0 ? EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) : Eval("IndependentGroupPrice") != null && EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("IndependentGroupPrice").ToString()) != "0" ? EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("IndependentGroupPrice").ToString()) : ""%></td>
                             <td align="center"><%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%></td>
                             <%}
                               else
                               { %>
                               <td align="center"><%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("IndependentGroupPrice").ToString())%></td>
                               <%} %>
                             <td align="center"><a href="javascript:void(0);"><%#Eval("ClickNum") %>次</a></td>
                             <td align="center" nowrap="nowrap"><a href='/PrintPage/LineTourInfo.aspx?RouteId=<%#Eval("RouteId") %>' target="_blank">线路行程单</a></td>
                             <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                               {%>
                             <td align="center" nowrap="nowrap"><a href="/RouteAgency/AddScatteredFightPlan.aspx?routeid=<%#Eval("RouteId") %>&routename=<%#Eval("RouteName")%>" class="AddorUpdate">批量添加修改计划</a></td>
                             <%} %>
                             <td align="center" nowrap="NOWRAP" class="list-caozuo">
                                 <a href="/RouteAgency/RouteManage/AddTourism.aspx" class="Update">修改</a> 
                                 <a href="/RouteAgency/RouteManage/AddTourism.aspx" class="Copy">复制</a><br />
                                 <a href="javascript:void(0);" class="Del">删除</a> 
                                 <a href="javascript:void(0);" class="RouteStatus" val="<%#(int)Eval("RouteStatus") == 1 ? "2" : "1"%>"><span class="<%#(int)Eval("RouteStatus") == 1 ? "greencolor" : "ff0000"%>"><%#(int)Eval("RouteStatus") == 1 ? "下架" : "上架"%></span></a> 
                             </td>
                       </tr>
                   </ItemTemplate>
               </asp:repeater>
</table>
<asp:panel id="pnlNodata" runat="server" visible="false">
     <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
        <tr>
            <td>暂无线路数据!&nbsp;&nbsp;<a href="/routeagency/routemanage/rmdefault.aspx" rel="toptab" tabrefresh="false" onclick='topTab.open($(this).attr("href"),"新增线路",{isRefresh:false});return false;' style="color:Red;">点此添加</a></td>
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
