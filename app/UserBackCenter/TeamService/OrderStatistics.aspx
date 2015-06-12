<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderStatistics.aspx.cs"
    Inherits="UserBackCenter.TeamService.OrderStatistics" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:content id="OrderStatistics" contentplaceholderid="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'OrderStatistics'
        });
    </script>
    <div id="<%=Key %>" class="right">
        <div class="tablebox">
            <table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;display:<%=isDetail?"":"none"%>">
               <tr style="background:url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                 <td width="1%" height="30" align="left">&nbsp;</td>
                 <td align="left"><span class="search">&nbsp;</span>专线：<asp:Label runat="server" id="lbl_title" Text=""></asp:Label></td>
               </tr>
             </table>
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;display:<%=isDetail?"none":""%>"">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        出团日期
                        <input id="txt_goTimeS" class="keydownSelect" onfocus="WdatePicker();" runat="server" size="30" style="width: 80px;" />
                        至
                        <input id="txt_goTimeE" class="keydownSelect" onfocus="WdatePicker();" runat="server" size="30" style="width: 80px;" />
                        <button type="button" class="search-btn" onclick="OrderStatistics.GetList()">
                            搜索</button>
                    </td>
                </tr>
            </table>
            <div class="hr_5">
            </div>
            <div id="tab_list">
             <table width="100%" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                    class="padd5">
                    <tr class="list_basicbg">
                        <th class="list_basicbg" style="display:<%=isDetail?"none":""%>">
                            时间
                        </th>
                        <th class="list_basicbg"  style="display:<%=isDetail?"none":""%>">
                            专线
                        </th>
                        <th class="list_basicbg" style="display:<%=isDetail?"":"none"%>">
                            线路名
                        </th>
                        <th class="list_basicbg" style="display:<%=isDetail?"":"none"%>">
                            专线商
                        </th>
                        <th class="list_basicbg">
                            订单量
                        </th>
                        <th class="list_basicbg">
                            总人数
                        </th>
                        <th class="list_basicbg">
                            成人
                        </th>
                        <th class="list_basicbg">
                            儿童
                        </th>
                        <th class="list_basicbg">
                            销售总额
                        </th>
                        <th class="list_basicbg">
                            结算总额
                        </th>
                        <th class="list_basicbg">
                            功能
                        </th>
                    </tr>
                    <asp:Repeater runat="server" id="rpt_parentList">
                        <ItemTemplate>
                            <tr val="<%#Eval("AreaId") %>" >
                                <td height="30" align="left" nowrap="nowrap" bgcolor="#FFFFFF" style="display:<%=isDetail?"none":""%>">
                                    <%#((DateTime)Eval("LeaveDateMin")).ToString("yyyy年MM月dd日")%>至<%#((DateTime)Eval("LeaveDateMax")).ToString("yyyy年MM月dd日")%>
                                </td>
                                <td align="left" bgcolor="#FFFFFF" style="display:<%=isDetail?"none":""%>">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td style="display:<%=isDetail?"":"none"%>">
                                    <%#Eval("RouteName")%>
                                </td>
                                <td style="display:<%=isDetail?"":"none"%>">
                                    <%#Eval("Publisher")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                  <%if (!isDetail)
                                    {%>
                                    <a class="a_showDetail" href="/TeamService/OrderStatistics.aspx?lineId=<%#Eval("AreaId") %>"><%#Eval("TotalOrder")%></a>
                                    <%}
                                    else
                                    { %>
                                    <%#Eval("TotalOrder")%>
                                    <%} %>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#Eval("TotalPeople")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#Eval("TotalAdult")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#Eval("TotalChild")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("TotalSale").ToString())%>元
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("TotalSettle").ToString())%>元
                                </td>
                                <td align="center" nowrap="nowrap" bgcolor="#FFFFFF">
                                   <%if (!isDetail)
                                     { %> <a class="a_showDetail" href="/TeamService/OrderStatistics.aspx?lineId=<%#Eval("AreaId") %>">明细查看</a><%}
                                     else
                                     { %><a class="a_showOrders"href="/TeamService/FITOrders.aspx?routeId=<%#Eval("RouteId") %>&keyWord=<%#Eval("RouteName") %>">订单查看</a> <%} %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel runat="server" id="pnlNodata" visible="false">
                <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                    <tr>
                        <td>暂无线路数据!</td>
                    </tr>
                 </table>
                </asp:Panel>
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
    <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="true"></script>
    <script type="text/javascript">

        var OrderStatistics = {
            Init: function() {
                var form = $("#<%=Key %>");
                form.find("#ExportPageInfo a,.a_showOrders").click(function() {
                    OrderStatistics.GoUrl(this);
                    return false;
                })
                form.find("#tab_list .a_showDetail").click(function() {
                    var url = $(this).attr("href");
                    url += "&title=";
                    var trobj = $(this).closest("tr");
                    url += $.trim(trobj.find("td:eq(1)").html());
                    url += "    ";
                    url += $.trim(trobj.find("td:eq(0)").html())
                    $(this).attr("href", url)
                    OrderStatistics.GoUrl(this);
                    return false;
                })
            },
            GetList: function() {
                var form = $("#<%=Key %>");
                form.find("#tab_list").html('<div id="div_load"><img src="<%= ImgURL%>/images/default/tree/loading.gif"/>加载中......</div>')
                //查询参数对象
                var SelectData = {
                    companyID: "", //公司id
                    goTimeS: "", //出团时间开始
                    goTimeE: "" //出团时间结束

                }
                SelectData.companyID = '<%=SiteUserInfo.CompanyID %>'
                //出团时间开始
                SelectData.goTimeS = $.trim(form.find("#<%=txt_goTimeS.ClientID %>").val());
                //出团时间结束
                SelectData.goTimeE = $.trim(form.find("#<%=txt_goTimeE.ClientID %>").val());
                $.newAjax({
                    type: "get",
                    url: "/TeamService/AjaxOrderStatistics.aspx",
                    data: SelectData,
                    cache: false,
                    dataType: "html",
                    success: function(html) {
                        form.find("#tab_list").html(html);
                        OrderStatistics.Init();
                    }, error: function() {
                        alert("获取异常")
                    }
                })
                return false;
            },
            GoUrl: function(obj) {
                topTab.url(topTab.activeTabIndex, encodeURI($(obj).attr("href")));
                return false;
            }
        }
        $(function() {
            var Key = $("#<%=Key %>");
            //回车查询效果
            Key.find(".keydownSelect").keydown(function(e) {
                if (e.keyCode == 13) {
                    OrderStatistics.GetList();
                    return false;
                } else {
                    return true;
                }
            });
            OrderStatistics.Init();
        })
    </script>
</asp:content>
