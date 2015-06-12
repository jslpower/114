<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="PlanList.aspx.cs" Inherits="UserBackCenter.RouteAgency.PlanList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:Content ID="PlanList" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="<%=Key %>" class="right">
        <div class="tablebox">
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td align="left">
                        出团日期
                        <input id="txt_goTimeS" runat="server" type="text" size="12" onfocus="WdatePicker();"
                            width="80px;" />
                        至
                        <input id="txt_goTimeE" runat="server" type="text" size="12" onfocus="WdatePicker();"
                            width="80px;" />
                        <button type="button" class="search-btn" id="btn_select">
                            搜索</button>
                        <input type="checkbox" id="ShowPrice" />
                        显示结算价
                    </td>
                    <td width="15%" align="center">
                        <a id="AddPlan" href="/routeagency/addscatteredfightplan.aspx?routename=<%=routename %>&routeid=<%=routeId %>"
                            toptitle="批量添加删除计划">
                            <img src="<%=ImgURL%>/images/arrowpl.gif" />发布计划</a>
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/lmnavm.gif); height: 32px;">
                    <td width="1%" align="left">
                        &nbsp;
                    </td>
                    <td width="99%" align="left">
                        <div id="div_powderTourStatus" style="float: left">
                            <span class="guestmenu">收客状态</span>
                            <asp:repeater runat="server" id="rpt_powderTourStatus">
                            <ItemTemplate>
                                <a href="javascript:void(0);" class="scatteredFightPlan"  val="<%#Eval("Value") %>"><%#Eval("Text") %></a> 
                            </ItemTemplate>
                        </asp:repeater>
                        </div>
                        <div id="div_type" style="float: left; margin-left: 5px;">
                            <span class="guestmenu guestmenu02">类 型</span>
                            <asp:repeater runat="server" id="rpt_type">
                            <ItemTemplate>
                                <a class="state<%#EyouSoft.Common.Utils.GetInt(Eval("Value").ToString())-1%>" href="javascript:void(0);" val="<%#Eval("Value") %>"><%#Eval("Text") %></a> 
                            </ItemTemplate>
                        </asp:repeater>
                            <a class="nostate" href="javascript:void(0)" val="1">取消设置</a>
                        </div>
                        <a href="/SMSCenter/SendSMS.aspx?type=0" id="a_note" class="state1" toptitle="短信推广">
                            短信推广</a>
                    </td>
                </tr>
            </table>
            <table id="tab_list" border="1" class="liststylesp" align="center" cellpadding="1"
                cellspacing="0" bordercolor="#9dc4dc" style="width: 100%; margin-top: 1px;" class="liststyle">
                <tr class="list_basicbg">
                    <td class="list_basicbg" colspan="15" align="left" nowrap="nowrap">
                        <b><a href="/RouteAgency/ScatteredFightPlan.aspx" id="a_retuenRVList" class="ff0000">
                            返回我的散拼计划</a></b>
                    </td>
                </tr>
                <tr class="list_basicbg">
                    <th class="list_basicbg" align="middle" nowrap="nowrap">
                        <input type="checkbox" class="chk_All" />
                        全
                    </th>
                    <th class="list_basicbg" nowrap="nowrap">
                        团号
                    </th>
                    <th class="list_basicbg" nowrap="nowrap">
                        线路名称
                    </th>
                    <th class="list_basicbg" nowrap="nowrap">
                        类型
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
                        状态
                    </th>
                    <th class="list_basicbg" nowrap="nowrap">
                        成人价
                    </th>
                    <th class="list_basicbg" nowrap="nowrap">
                        儿童价
                    </th>
                    <th class="list_basicbg" nowrap="nowrap">
                        单房差
                    </th>
                    <th class="list_basicbg ShowJSPrice" style="display: none">
                        结算价
                    </th>
                    <th class="list_basicbg" nowrap="nowrap">
                        打印
                    </th>
                    <th class="list_basicbg" nowrap="nowrap">
                        功能
                    </th>
                </tr>
                <asp:repeater runat="server" id="rpt_list">
                    <ItemTemplate>
                        <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                            <td align="center">
                                <input type="checkbox" value="<%#Eval("TourId")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("TourNo")%>
                            </td>
                            <td>
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <span class="state<%#(int)Eval("RecommendType")-1%>"><%#Eval("RecommendType").ToString() == "无" ? "" : Eval("RecommendType")%></span>
                            </td>
                            <td align="center">
                                <%#((DateTime)Eval("LeaveDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("LeaveDate")).ToString("ddd")%>)
                            </td>
                            <td align="center">
                                <%#((DateTime)Eval("RegistrationEndDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("RegistrationEndDate")).ToString("ddd")%>)
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("TourNum").ToString())%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("MoreThan").ToString())%>
                            </td>
                            <td align="center">
                              <a class="scatteredFightPlan" val="<%#(int)Eval("PowderTourStatus") %>"><%#Eval("PowderTourStatus")%></a>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("MarketPrice").ToString())%>
                            </td>
                            <td align="center" nowrap="NOWRAP" class="ShowJSPrice"  style="display:none">
                               <span class="ff0000"> <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString())%></span>
                                /
                                <span class="ff0000"><%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString())%></span>
                            </td>
                            <td align="center" nowrap="nowrap">
                                <a href='/PrintPage/TouristInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank">名单</a><br />
                                <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank">行程单</a><br />
                                <a href='/PrintPage/OutGroupNotices.aspx?TeamId=<%#Eval("TourId") %>' target="_blank">出团通知书</a>
                            </td>
                            <td class="td_aGo" align="center" nowrap="nowrap">
                                <a href="/Order/RouteAgency/AddOrderByRoute.aspx?tourID=<%#Eval("TourId") %>" class="boto">代定</a><br />
                                <a href="/RouteAgency/UpdateScatteredFightPlan.aspx?tourId=<%#Eval("TourId") %>">修改</a><br />
                                <a class="Order"order="<%#Eval("OrderPeopleNum") %>" href="/Order/RouteAgency/NewOrders.aspx?tourId=<%#Eval("TourId") %>">订单</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
                <tr>
                    <td>
                        <input type="checkbox" class="chk_All" />全
                    </td>
                    <td colspan="14" align="left">
                        <a href="javascript:void(0);" id="Update" class="basic_btn"><span>统一修改团队行程</span></a>
                    </td>
                </tr>
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
            <%--<table width="100%" border="0">
                <tr>
                    <td align="left">
                        <strong>发现好几条团队行程内容有错误，如何批量修改？</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>答</strong>：由于团队行程和线路行程是独立分开的，单独修改线路内容不会改变团队行程。当您发布了团队行程，发现行程错误，又不想删除重新发布，您可以进入该线路的团队计划列表，点选进入需要修改的团队前面的选择框。然后点击下方的《统一修改团队行程》按钮。然后系统会进入一个统一的修改界面，在该界面修改后保存即可。注意！统一修改行程会覆盖除了出团时间和价格等信息的所有信息，修改的时候请注意。
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>为什么有点团队能删除，有些没有？</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>答</strong>：如果团队已经有订单出现，为保证系统的信息完全，所以不能删除有订单的团队。
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>如果我要停收某个团队，或者客满了停止收客如何设置？</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>答</strong>：点选团队前的选择框，然后点击列表上方的收客状态 【客满 停收 正常】 即可。
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>如何单独对某团设置特价？这个特价在哪里显示？</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>答</strong>：点选团队前的选择框，然后点击列表上方的团队类型 【推荐 特价 豪华 热门 新品 纯玩 经典 取消设置】即可。这些标志会在组团社的散客计划列表中，团队标题后显示对应图片标签。
                    </td>
                </tr>
            </table>--%>
        </div>
        <!--表格盒子结束-->
    </div>

    <script type="text/javascript">

        var RouteAgencyPlanList = {
            GetList: function() {
                var url = "/RouteAgency/PlanList.aspx";
                url += "?routeId=" + "<%=routeId %>"
                url += "&showPrice=" + $("#<%=Key %> #ShowPrice").attr("checked");
                url += "&goTimeS=" + $("#<%=Key %> #<%=txt_goTimeS.ClientID %>").val();
                url += "&goTimeE=" + $("#<%=Key %> #<%=txt_goTimeE.ClientID %>").val();
                topTab.url(topTab.activeTabIndex, url);
                return false;
            },
            GoUrl: function(obj) {
                topTab.url(topTab.activeTabIndex, encodeURI($(obj).attr("href")));
                return false;
            },
            AllChk: function(obj) {
                var form = $("#<%=Key %>");
                form.find("#tab_list :checkbox:not(.chk_All)").attr("checked", $(obj).attr("checked"))
            },
            Update: function() {
                var form = $("#<%=Key %>");
                if (form.find("#tab_list :checkbox:not(.chk_All):checked").length > 0) {
                    var url = "/RouteAgency/UpdateScatteredFightPlan.aspx?isAllUpdata=true";
                    var tourIds = "";
                    form.find("#tab_list :checkbox:not(.chk_All):checked").each(function() {
                        tourIds += $(this).val() + ",";
                    })
                    url += "&tourIds=" + tourIds.substring(0, tourIds.length - 1);
                    topTab.url(topTab.activeTabIndex, url);
                } else {
                    alert("请选择记录")
                }

            },
            Set: function(settype, setvalue, ids) {
                var form = $($("#<%=Key %>"));
                var url = "/RouteAgency/ScatteredFightPlan.aspx"
                var SetData = {
                    ids: ids, //id列表
                    SetType: settype, //修改类型（线路状态or推荐状态）
                    SetValue: setvalue//修改值
                }
                $.newAjax({
                    type: "get",
                    url: url,
                    data: SetData,
                    cache: false,
                    dataType: "html",
                    success: function(html) {
                        if (html.toLowerCase() == "true") {
                            alert("设置成功!")
                        }
                        else {
                            alert("设置失败！")
                        }
                        RouteAgencyPlanList.GetList();
                    }, error: function() {
                        alert("设置异常")
                    }
                })
            }
        }
        $(function() {
            var form = $("#<%=Key %>");
            form.find("#AddPlan,#a_note").click(function() {
                topTab.remove(topTab.activeTabIndex)
                topTab.open(encodeURI($(this).attr("href")), $(this).attr("toptitle"), {});

                return false;

            })
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
            //团队状态添加class
            var className = ["keman", "tings", "zhengc", "chengtuan"]
            form.find("a.scatteredFightPlan").each(function() {
                $(this).addClass(className[parseInt($(this).attr("val")) - 1])
            })
            form.find("#btn_select").click(function() {
                RouteAgencyPlanList.GetList();
                return false;
            })
            form.find("#tab_list .td_aGo a").click(function() {
                RouteAgencyPlanList.GoUrl(this);
                return false;
            })
            //全选
            form.find(".chk_All").click(function() {
                RouteAgencyPlanList.AllChk(this);
            })
            form.find("#Update").click(function() {
                RouteAgencyPlanList.Update();
                return false;
            })
            //推荐状态
            form.find("#div_type a").click(function() {
                if (form.find("#tab_list :checkbox:not(.chk_All):checked").length > 0) {
                    var str = "";
                    form.find("#tab_list :checkbox:not(.chk_All):checked").each(function() {
                        str += $(this).attr("value") + ",";
                    })
                    RouteAgencyPlanList.Set("RecommendType", $(this).attr("val"), str.substring(0, str.length - 1));
                } else {
                    alert("请选择记录")
                }
            })
            //收客状态
            form.find("#div_powderTourStatus a").click(function() {
                if (form.find("#tab_list :checkbox:not(.chk_All):checked").length > 0) {
                    var str = "";
                    form.find("#tab_list :checkbox:not(.chk_All):checked").each(function() {
                        str += $(this).attr("value") + ",";
                    })
                    RouteAgencyPlanList.Set("PowderTourStatus", $(this).attr("val"), str.substring(0, str.length - 1));
                } else {
                    alert("请选择记录")
                }
            })
            //分页
            form.find("#ExportPageInfo a").click(function() {
                RouteAgencyPlanList.GoUrl(this);
                return false;
            })
            //显示结算价
            form.find("#ShowPrice").click(function() {
                form.find(".ShowJSPrice").css("display", $(this).attr("checked") ? "" : "none")
            })
            //初始化结算价
            var showPrice = '<%=Request.QueryString["showPrice"]!=null&&Request.QueryString["showPrice"].ToString()=="true" %>' == "True";
            form.find("#ShowPrice").attr("checked", showPrice ? "checked" : "");
            form.find(".ShowJSPrice").css("display", showPrice ? "" : "none")
            //没有订单 不显示订单按钮
            form.find(".Order").each(function() {
                if (parseInt($(this).attr("order")) <= 0) {
                    $(this).remove();
                }
            })
            form.find("#a_retuenRVList").click(function() {
                topTab.open($(this).attr("href").toLowerCase(), "我的散拼计划");
                return false;
            })

        })
    </script>

</asp:Content>
