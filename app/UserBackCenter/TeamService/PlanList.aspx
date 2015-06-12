<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="PlanList.aspx.cs" Inherits="UserBackCenter.TeamService.PlanList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:Content ID="PlanList" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="<%=Key %>" class="right">
        <div class="tablebox">
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;"
                class="toolbj">
                <tr>
                    <td width="1%" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <b><a href="javascript:void(0);" onclick="return topTab.url(topTab.activeTabIndex,'/TeamService/LineLibraryList.aspx?lineId=<%=areaId %>&type=<%=type %>')"
                            class="ff0000">返回线路库</a></b>
                    </td>
                </tr>
            </table>
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="list_basicbg">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <span class="search">&nbsp;</span> 出团日期：
                        <input id="txt_goTimeS" runat="server" type="text" size="12" onfocus="WdatePicker();"
                            width="80px;" />
                        至
                        <input id="txt_goTimeE" runat="server" type="text" size="12" onfocus="WdatePicker();"
                            width="80px;" />
                        <button type="button" id="btn_Select" class="search-btn">
                            搜索</button>
                        <input type="checkbox" id="ShowPrice" />
                        显示结算价
                    </td>
                </tr>
            </table>
            <div id="tab_list">
                <table id="list" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                    style="width: 100%; margin-top: 1px;" class="liststylesp">
                    <tr class="list_basicbg">
                        <th class="list_basicbg" nowrap="nowrap">
                            团号
                        </th>
                        <th class="list_basicbg" nowrap="nowrap">
                            线路名称
                        </th>
                        <th class="list_basicbg" nowrap="nowrap">
                            出团日期
                        </th>
                        <th class="list_basicbg" nowrap="nowrap">
                            报名截止
                        </th>
                        <th class="list_basicbg" nowrap="nowrap">
                            人数
                        </th>
                        <th class="list_basicbg" nowrap="nowrap">
                            余位
                        </th>
                        <th class="list_basicbg" nowrap="nowrap">
                            成人价
                        </th>
                        <th class="list_basicbg" nowrap="nowrap">
                            儿童价
                        </th>
                        <th class="list_basicbg ShowJSPrice" style="display: none">
                            结算价
                        </th>
                        <th class="list_basicbg" nowrap="nowrap">
                            功能
                        </th>
                    </tr>
                    <asp:repeater runat="server" id="rpt_list">
                        <ItemTemplate>
                            <tr>
                                <td align="left" nowrap="nowrap">
                                    <%#Eval("TourNo")%>
                                </td>
                                <td align="left">
                                    <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank"><%#Eval("RouteName")%></a>
                                </td>
                                <td align="center" nowrap="nowrap">
                                    <%#((DateTime)Eval("LeaveDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("LeaveDate")).ToString("ddd")%>)
                                </td>
                                <td><span class="ff0000"><%#((DateTime)Eval("RegistrationEndDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("RegistrationEndDate")).ToString("ddd")%>)</span></td>
                                <td align="center" nowrap="nowrap">
                                    <%#Eval("TourNum")%>
                                </td>
                                <td align="center" nowrap="nowrap">
                                    <%#((int)Eval("MoreThan")) == 0 ? "" : Eval("MoreThan")%>
                                </td>
                                <td align="center" nowrap="nowrap">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                                </td>
                                <td align="center" nowrap="nowrap">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
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
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            var form = $("#<%=Key %>");
            //显示结算价
            form.find("#ShowPrice").click(function() {
                form.find(".ShowJSPrice").css("display", $(this).attr("checked") ? "" : "none")
            })
            //初始化结算价
            var showPrice = '<%=Request.QueryString["showPrice"]!=null&&Request.QueryString["showPrice"].ToString()=="true" %>' == "True";
            form.find("#ShowPrice").attr("checked", showPrice ? "checked" : "");
            form.find(".ShowJSPrice").css("display", showPrice ? "" : "none")
            //团队状态添加class
            var className = ["keman", "tings", "zhengc", "chengtuan"]
            form.find("a.scatteredFightPlan").each(function() {
                $(this).addClass(className[parseInt($(this).attr("val")) - 1])
            })
            form.find("#btn_select").click(function() {
                TeamServicePlanList.GetList();
                return false;
            })
            form.find("#tab_list #list a:not([target='_blank'])").click(function() {
                TeamServicePlanList.GoUrl(this);
                return false;
            })
            form.find("#ExportPageInfo a").click(function() {
                $(this).attr("href", $(this).attr("href") + "&showPrice=" + $("#<%=Key %> #ShowPrice").attr("checked"))
                TeamServicePlanList.GoUrl(this);
                return false;
            })
            //列表颜色效果
            form.find("#tab_list tr").hover(
            function() {
                if ($(this).attr("class") != "list_basicbg")
                    $(this).addClass("highlight");
            },
            function() {
                if ($(this).attr("class") != "list_basicbg")
                    $(this).removeClass("highlight");
            })
            .click(function() {
                $(this).parent().find("tr").removeClass("selected");
                $(this).addClass("selected");
            })
            form.find("#btn_Select").click(function() {
                TeamServicePlanList.GetList();
                return false;
            })
        })
        var TeamServicePlanList = {
            GetList: function() {
                var url = "/TeamService/PlanList.aspx";
                url += "?routeId=" + "<%=routeId %>";
                url += "&showPrice=" + $("#<%=Key %> #ShowPrice").attr("checked");
                url += "&goTimeS=" + $("#<%=txt_goTimeS.ClientID %>").val();
                url += "&goTimeE=" + $("#<%=txt_goTimeE.ClientID %>").val();
                topTab.url(topTab.activeTabIndex, url);
                return false;
            },
            GoUrl: function(obj) {
                topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
                return false;
            }
        }
    </script>

</asp:Content>
