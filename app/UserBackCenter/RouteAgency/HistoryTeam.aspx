<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoryTeam.aspx.cs" Inherits="UserBackCenter.RouteAgency.HistoryTeam" %>

<asp:content id="HistoryTeam" contentplaceholderid="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=Key %>',
        ReleaseType: 'HistoryTeam'
    });
</script>
<%@ Register Src="~/usercontrol/ILine.ascx" TagName="ILine" TagPrefix="uc1" %>
    <div id="<%=Key %>" class="right">
        <div class="tablebox">
            <uc1:ILine ID="ILine1" runat="server" />
            <table  border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td height="30" align="left">
                        <span class="search">&nbsp;</span>关键字及团号
                        <input id="txt_keyWord" class="keydownSelect" runat="server" type="text" size="20" style="width: 100px;" />
                        专线：
                        <asp:DropDownList runat="server" id="ddl_ZX">
                           <asp:listItem value="-1">-全部-</asp:listItem>
                        </asp:DropDownList>
                        出团日期
                        <input id="txt_goTimeS" class="keydownSelect" runat="server" onfocus="WdatePicker();" type="text" size="12" width="80px;" />
                        至
                        <input id="txt_goTimeE" class="keydownSelect" runat="server" onfocus="WdatePicker();" type="text" size="12" width="80px;" />
                        <button type="button" id="btn_select" class="search-btn" onclick="HistoryTeam.GetList()">
                            搜索</button>
                    </td>
                    
                </tr>
            </table>
            <div id="tab_list">
            <table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                style="width: 100%; margin-top: 1px;" class="liststylesp">
                <tr class="list_basicbg">
                    <th class="list_basicbg">
                        团号
                    </th>
                    <th class="list_basicbg" style="width: 450px">
                        线路名称
                    </th>
                    <th class="list_basicbg">
                        出团日期
                    </th>
                    <th class="list_basicbg">
                        返回日期
                    </th>
                    <th class="list_basicbg">
                        团队人数
                    </th>
                    <th class="list_basicbg">
                        订单量
                    </th>
                    <th class="list_basicbg">
                        实收人数
                    </th>
                    <th class="list_basicbg">
                        成人价
                    </th>
                    <th class="list_basicbg">
                        儿童价
                    </th>
                    <th class="list_basicbg">
                        打印
                    </th>
                    <th class="list_basicbg">
                        功能
                    </th>
                </tr>
                <asp:Repeater runat="server" id="rpt_list">
                    <ItemTemplate>
                        <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                            <td align="center">
                                <%#Eval("TourNo")%>
                            </td>
                            <td align="left">
                                <a href='/PrintPage/LineTourInfo.aspx?RouteId=<%#Eval("RouteId") %>' target="_blank"><%#Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <%#((DateTime)Eval("LeaveDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("LeaveDate")).ToString("ddd")%>)
                            </td>
                            <td align="center">
                                <%#((DateTime)Eval("ComeBackDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("ComeBackDate")).ToString("ddd")%>)
                            </td>
                            <td align="center">
                                 <%#Eval("TourNum")%>
                            </td>
                            <td align="center">
                                 <%#Eval("OrderNum")%>
                            </td>
                            <td align="center">
                                 <%#Eval("OrderPeopleNum")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
                            </td>
                            <td align="center" nowrap="nowrap">
                                <a href='/PrintPage/TouristInfo.aspx?TeamId=<%#Eval("TourId") %>&OrderStatus=lishi' target="_blank">名单</a>
                            </td>
                            <td align="center" nowrap="nowrap" class="list-caozuo">
                                <a class="a_orders" href="/RouteAgency/AllFITOrders.aspx?tourId=<%#Eval("TourId") %>">订单</a>
                                <a class="a_Del" ordernum="<%#Eval("OrderNum") %>" href="javascript:void(0);" tourid="<%#Eval("TourId") %>">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel id="pnlNodata" runat="server" visible="false">
                 <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                    <tr>
                        <td>暂无线路数据!</td>
                    </tr>
                 </table>
             </asp:Panel>
             <table id="ExportPageInfo" cellspacing="0" cellpadding="0" width="98%" align="right" border="0">
                <tr>
                    <td class="F2Back" align="right" height="40">
                        <cc1:ExportPageInfo ID="ExportPageInfo1" Visible=false LinkType="4" runat="server" />
                    </td>
                </tr>
            </table>
            </div>
            <%--<table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="25" align="left">
                        <strong>我的历史团队主要是哪些团队？</strong>
                    </td>
                </tr>
                <tr>
                    <td height="25" align="left">
                        <strong>答</strong>：当前时间所有过了回团时间的团队，都算是历史团队。
                    </td>
                </tr>
                <tr>
                    <td height="25" align="left">
                        <strong>历史团队页面主要有那些功能？</strong>
                    </td>
                </tr>
                <tr>
                    <td height="25" align="left">
                        <strong>答</strong>：您可以查看打印团队一些基本情况，以及通过功能链接，查询在线报名的人员名单以及该团队的过往订单。
                    </td>
                </tr>
            </table>--%>
        </div>
    </div>
<script type="text/javascript">

    var HistoryTeam = {
        Init: function() {
            var form = $("#<%=Key %>");
            form.find("#ExportPageInfo a").click(function() {
                HistoryTeam.GoUrl(this);
                return false;
            })
            form.find(".a_orders").click(function() {
                topTab.open($(this).attr("href"), "所有散客订单", {})
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
            form.find(".a_Del").each(function() {
                if (parseInt($(this).attr("ordernum")) > 0) {
                    $(this).remove();
                }
                else {
                    $(this).parent().find(".a_orders").remove();
                }
            })
            form.find(".a_Del").click(function() {
                if (confirm("确定删除该团队？")) {
                    HistoryTeam.Del($(this).attr("tourid"));
                }
                return false;

            })
        },
        Del: function(ids) {
            var url = "/RouteAgency/HistoryTeam.aspx?isDel=true";
            var DelData = {
                ids: ids //id
            }
            $.newAjax({
                type: "get",
                url: url,
                data: DelData,
                cache: false,
                dataType: "html",
                success: function(html) {
                    if (html) {
                        alert("删除成功！")
                        HistoryTeam.GetList();
                    }
                    else {
                        alert("删除失败！")
                    }
                }, error: function() {
                    alert("设置异常")
                    topTab.url(topTab.activeTabIndex, "/RouteAgency/HistoryTeam.aspx");
                }
            })
        },
        //设置选中专线
        SetSelectLine: function(obj) {
            var iLine1 = $("#<%=Key %>" + "_tab_ILine");
            iLine1.find(".select").removeClass("select");
            iLine1.find("[lineid='" + $(obj).val() + "']").addClass("select");
        },
        GetList: function() {
            var form = $("#<%=Key %>");
            form.find("#tab_list").html('<div id="div_load"><img src="<%= ImgURL%>/images/default/tree/loading.gif" alt="加载中......" />加载中......</div>')
            //查询参数对象
            var SelectData = {
                keyWord: "", //关键字
                goTimeS: "", //出团时间开始
                goTimeE: "", //出团时间结束
                lineId: "", //专线id
                lineType: "", //专线类别
                companyID: ""
            }
            SelectData.companyID = '<%=SiteUserInfo.CompanyID %>'
            //关键字
            SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
            //出团时间开始
            SelectData.goTimeS = $.trim(form.find("#<%=txt_goTimeS.ClientID %>").val());
            //出团时间结束
            SelectData.goTimeE = $.trim(form.find("#<%=txt_goTimeE.ClientID %>").val());
            //选中的专线对象
            var tab_ILine_select = form.find("#<%=Key %>" + "_tab_ILine .select");
            form.find("#<%=ddl_ZX.ClientID %>").val(tab_ILine_select.attr("lineid"));
            //专线id
            SelectData.lineId = tab_ILine_select.attr("lineid");
            //专线类型
            SelectData.lineType = tab_ILine_select.attr("linetype");

            $.newAjax({
                type: "get",
                url: "/RouteAgency/AjaxHistoryTeam.aspx",
                data: SelectData,
                cache: false,
                dataType: "html",
                success: function(html) {
                    form.find("#tab_list").html(html);
                    HistoryTeam.Init();
                }, error: function() {
                    alert("获取异常")
                    topTab.url(topTab.activeTabIndex, "/RouteAgency/HistoryTeam.aspx");
                }
            })
        },
        GoUrl: function(obj) {
            topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
            return false;
        }
    }
    $(function() {
        var form = $("#<%=Key %>");
        //专线下拉效果
        form.find("#<%=ddl_ZX.ClientID %>").change(function() {
            HistoryTeam.SetSelectLine(this);
        })
        //回车查询效果
        form.find(".keydownSelect").keydown(function(e) {
            if (e.keyCode == 13) {
                HistoryTeam.GetList();
                return false;
            } else {
                return true;
            }
        });
        HistoryTeam.Init();
    })
</script>
</asp:content>
