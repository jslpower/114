<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxNotStartingTeams.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AjaxNotStartingTeams" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<head id="Head1" runat="server" visible="false">
</head>
<input id="hidAjaxNotStartingTeamsPage" value="<%=intPageIndex %>" type="hidden" />
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="xianluhangcx"
    style="line-height: 10px; padding: 0px; border: 1px solid #ccc; border-bottom: 0px;"
    height="18">
    <tr>
        <td width="66%" align="left" style="padding-left: 65px;">
            <strong>团队基本信息</strong>
        </td>
        <td width="21%" align="center">
            <strong>成人价/儿童价</strong>
        </td>
        <td width="13%" align="center">
            <strong>操作明细表</strong>
        </td>
    </tr>
</table>
<table runat="server" id="NoData" visible="false" width="100%" border="0" cellspacing="0"
    cellpadding="0" class="xianluhangcx" style="line-height: 10px; padding: 0px;
    border: 1px solid #ccc;" height="30">
    <tr>
        <td colspan="3">
            暂无团队基本信息！
        </td>
    </tr>
</table>
<asp:repeater id="rptTourBasicInfo" runat="server">
        <ItemTemplate>
            <table width="100%" border="0" align="center" cellpadding="2" cellspacing="0" style="border: 1px solid #D8D8D8;
                text-align: left;">
                <tr bgcolor="#FFFFFF" >
                    <td width="489" style="border-bottom: 1px dashed #ccc; height: 50px; padding-top: 5px;">
                        <strong>
                            <img src="<%=ImageServerUrl %>/images/ico.gif" width="11" height="11" />
                            <a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("id") %>&TemplateTourID=<%#Eval("ParentTourID") %>"
                                target="_blank"><%#Eval("TourSpreadStateName")%><%#Eval("RouteName") %>
                             </a>
                        </strong><br />
                        &nbsp;&nbsp;<span class="danhui">最近一班：</span><span class="huise"><%#Eval("LeaveDate", "{0:MM-dd}")%>(<%#EyouSoft.Common.Utils.ConvertWeekDayToChinese(Convert.ToDateTime(Eval("LeaveDate"))) %>)/</span><span
                            class="chengse"><strong>剩:<%#Eval("RemnantNumber")%></strong></span>&nbsp;&nbsp;
                        <span class="huise"><a href="javascript:void(0)" class="a_otherTour" templatetourid="<%#Eval("ParentTourID") %>" TourID="<%#Eval("id") %>" AreaType="<%#(int)((EyouSoft.Model.SystemStructure.AreaType)Eval("AreaType")) %>" >
                            <img src="<%=ImageServerUrl %>/images/rili.gif" style="margin-bottom: -3px;" />其它<strong><%#Eval("RecentLeaveCount")%></strong>个发团日期>></a></span>
                    </td>
                    <td width="150" style="border-bottom: 1px dashed #ccc; line-height: 18px;">
                        门市价：<span class="chengse">￥<%#Eval("RetailAdultPrice", "{0:F2}")%></span>/<%#Eval("RetailChildrenPrice", "{0:F2}")%><br />
                        同行价：<span class="chengse">￥<%#Eval("TravelAdultPrice", "{0:F2}")%></span>/<%#Eval("TravelChildrenPrice", "{0:F2}")%>
                    </td>
                    <td width="94" align="center" style="border-bottom: 1px dashed #ccc; line-height: 14px;">
                        <a href="javascript:void(0)" templatetourid="<%#Eval("ParentTourID") %>" class="NotStartingTeamsDetail">
                            <img src="<%=ImageServerUrl %>/images/jihuab.gif" alt="点击查看详细计划" width="30" height="28"
                                border="0" /></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" bgcolor="#eeeeee" style="border-bottom: 1px dashed #ccc; padding-top: 2px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="50%" align="center">
                                    <a style="cursor: pointer;" class="a_again" templatetourid="<%#Eval("id") %>"><strong>
                                        <img src="<%=ImageServerUrl %>/images/arrowpl.gif" width="11" height="9" /><span
                                            class="ff0000">再发布</span></strong></a> <a style="cursor: pointer;" templatetourid="<%#Eval("ParentTourID") %>"
                                                releasetype="<%#Eval("ReleaseType") %>" class="a_updateTemplate">批量修改</a>
                                    <a style="cursor: pointer;" templatetourid="<%#Eval("ParentTourID") %>" class="a_deleteTemplate">
                                        删除</a>
                                </td>
                                <td width="50%">
                                    <a href="javascript:void(0)" class="state1" templatetourid="<%#Eval("ParentTourID") %>"  onclick="NotStartingTeams.setTourMarkerNote(1,this);return false;">
                                        <nobr>推荐精品</nobr>
                                    </a><a href="javascript:void(0)" templatetourid="<%#Eval("ParentTourID") %>" onclick="NotStartingTeams.setTourMarkerNote(2,this);return false;"
                                        class="state2">
                                        <nobr>促销</nobr>
                                    </a><a href="javascript:void(0)" templatetourid="<%#Eval("ParentTourID") %>" class="state3"
                                        onclick="NotStartingTeams.setTourMarkerNote(3,this);return false;">
                                        <nobr>最新</nobr>
                                    </a><a href="javascript:void(0)" templatetourid="<%#Eval("ParentTourID") %>" class="state4"
                                        onclick="NotStartingTeams.setTourMarkerNote(4,this);return false;">
                                        <nobr>品质</nobr>
                                    </a><a href="javascript:void(0)" templatetourid="<%#Eval("ParentTourID") %>" class="state5"
                                        onclick="NotStartingTeams.setTourMarkerNote(5,this);return false;">
                                        <nobr>纯玩</nobr>
                                    </a><a href="javascript:void(0)" TemplateTourID="<%#Eval("ParentTourID") %>" class="nostate"
                                        onclick="NotStartingTeams.setTourMarkerNote('0',this);return false;">
                                        <nobr>取消设置</nobr>
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:repeater>
<div id="NotStartingTeams_ExportPage" class="F2Back" style="text-align: right;" height="40">
    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"
        runat="server"></cc2:ExportPageInfo>
</div>
