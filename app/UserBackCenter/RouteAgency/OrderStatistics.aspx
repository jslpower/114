<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderStatistics.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.OrderStatistics" %>

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
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                         专线：
                       <asp:DropDownList runat="server" id="ddl_ZX">
                           <asp:listItem value="-1">-全部-</asp:listItem>
                       </asp:DropDownList>
                       出团日期
                        <input id="txt_goTimeS" class="keydownSelect" onfocus="WdatePicker();" runat="server" size="30" style="width: 80px;" />
                        至
                        <input id="txt_goTimeE" class="keydownSelect" onfocus="WdatePicker();" runat="server" size="30" style="width: 80px;" />
                        <button type="button" class="search-btn"  onclick="OrderStatistics.GetList()">
                            搜索</button>
                    </td>
                </tr>
            </table>
            <!--列表-->
            <div id="tab_list">
                <table width="100%" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                    class="padd5">
                    <tr class="list_basicbg">
                        <th class="list_basicbg">
                            时间
                        </th>
                        <th class="list_basicbg">
                            专线
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
                            <tr val="<%#Eval("AreaId") %>" <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                                <td height="30" align="left" nowrap="nowrap" bgcolor="#FFFFFF">
                                   <%#((DateTime)Eval("LeaveDateMin")).ToString("yyyy年MM月dd日")%>至<%#((DateTime)Eval("LeaveDateMax")).ToString("yyyy年MM月dd日")%>
                                </td>
                                <td align="left" bgcolor="#FFFFFF">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <a class="a_showOrders" href="/routeagency/allfitorders.aspx?goTimeS=<%#((DateTime)Eval("LeaveDateMin")).ToString("yyyy-MM-dd")%>&goTimeE=<%#((DateTime)Eval("LeaveDateMax")).ToString("yyyy-MM-dd")%>&lineId=<%#Eval("AreaId")%>"><%#Eval("TotalOrder")%></a>
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
                                    <a href="javascript:void(0);" onclick="OrderStatistics.ShowDetailed(this)">明细查看</a>
                                </td>
                            </tr>
                            <%#GetList((int)Eval("AreaId"))%>
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
            <%--<table width="98%" border="0" align="center">
                <tr>
                    <td align="left">
                        点击详细，按照线路统计
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
            </table>--%>
        </div>
    </div>

    <script type="text/javascript">

        var OrderStatistics = {
            Init: function() {
                var Key = $("#<%=Key %>");
                Key.find("#ExportPageInfo a,.a_showOrders").click(function() {
                    OrderStatistics.GoUrl(this);
                    return false;
                })
            },
            ShowDetailed: function(obj) {
                var form = $("#<%=Key %>");
                var id = $(obj).closest("tr").attr("val")
                form.find("[id^='div_Detailed_']:not([id='div_Detailed_" + id + "'])").css("display", "none");
                if (form.find("#div_Detailed_" + id).css("display") == "none") {
                    form.find("#div_Detailed_" + id).css("display", "")
                    return false;
                }
                else {
                    form.find("#div_Detailed_" + id).css("display", "none")
                    return false;
                }
                return false;
            },
            GetList: function() {
                var form = $("#<%=Key %>");
                form.find("#tab_list").html('<div id="div_load"><img src="<%= ImgURL%>/images/default/tree/loading.gif"/>加载中......</div>')

                //查询参数对象
                var SelectData = {
                    companyId: "",
                    goTimeS: "", //出团时间开始
                    goTimeE: "", //出团时间结束
                    lineId: "" //专线id

                }
                SelectData.companyId = '<%=SiteUserInfo.CompanyID %>';
                SelectData.lineId = form.find("#<%=ddl_ZX.ClientID %>").val();
                SelectData.goTimeS = form.find("#<%=txt_goTimeS.ClientID %>").val();
                SelectData.goTimeE = form.find("#<%=txt_goTimeE.ClientID %>").val();
                $.newAjax({
                    type: "get",
                    url: "/RouteAgency/AjaxOrderStatistics.aspx",
                    data: SelectData,
                    cache: false,
                    success: function(html) {
                        form.find("#tab_list").html(html);
                        OrderStatistics.Init();
                    }, error: function() {
                        alert("获取异常")
                    }
                })
            },
            GoUrl: function(obj) {
                topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
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
