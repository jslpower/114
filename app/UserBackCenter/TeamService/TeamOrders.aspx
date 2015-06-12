<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="TeamOrders.aspx.cs" Inherits="UserBackCenter.TeamService.TeamOrders" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:Content ID="TeamOrders" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="<%=Key %>" class="right">
        <div class="tablebox">
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <span class="search">&nbsp;</span>关键字
                        <input id="txt_keyWord" class="keydownSelect" runat="server" value="" size="30" />
                        <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                          {%>
                        状态：
                        <asp:dropdownlist runat="server" id="sel_status">
                        <asp:listItem value="-1">-全部-</asp:listItem>
                        </asp:dropdownlist>
                        <%} %>
                        出团日期
                        <input id="txt_goTimeS" class="keydownSelect" onfocus="WdatePicker();" runat="server"
                            size="30" style="width: 80px;" />
                        至
                        <input id="txt_goTimeE" class="keydownSelect" onfocus="WdatePicker();" runat="server"
                            size="30" style="width: 80px;" />
                        <button type="button" class="search-btn" onclick="TeamOrders.GetList()">
                            搜索</button>
                    </td>
                </tr>
            </table>
            <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
              {%>
            <table id="tab_status" border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/lmnavm.gif); height: 32px;">
                    <td width="1%" align="left">
                        &nbsp;
                    </td>
                    <td width="99%" align="left">
                        <span class="guestmenu guestmenu02">状 态</span>
                        <asp:repeater runat="server" id="rpt_powderOrderStatus">
                            <ItemTemplate>
                                <a href="javascript:void(0);" id="status_<%#Eval("Value") %>" val="<%#Eval("Value") %>"><%#Eval("Text") %></a>
                            </ItemTemplate>
                        </asp:repeater>
                    </td>
                </tr>
            </table>
            <%} %>
            <div id="tab_list">
                <table width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                    class="liststylesp">
                    <tr class="list_basicbg">
                        <th class="list_basicbg">
                            出发城市
                        </th>
                        <th class="list_basicbg" style="width: 450px">
                            线路名
                        </th>
                        <th class="list_basicbg">
                            团队联系人
                        </th>
                        <th class="list_basicbg">
                            出发时间
                        </th>
                        <th class="list_basicbg">
                            预订时间
                        </th>
                        <th class="list_basicbg">
                            人数
                        </th>
                        <th class="list_basicbg">
                            状态
                        </th>
                        <th class="list_basicbg">
                            操作
                        </th>
                    </tr>
                    <asp:repeater runat="server" id="rpt_list">
                                    <ItemTemplate>
                                        <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                                            <td height="30" align="center">
                                               <%#Eval("StartCityName")%>
                                            </td>
                                            <td align="left">
                                                <a href='/PrintPage/LineTourInfo.aspx?RouteId=<%#Eval("RouteId") %>' target="_blank"> <%#Eval("RouteName")%></a><br />
                                                <%if (routeSource == null)
                                                  { %>
                                                专线商：<a target="_blank" href="<%#Eval("CompanyType")!=null?EyouSoft.Common.Utils.GetShopUrl(Eval("Business").ToString()):"javascript:void(0);" %>"><%#Eval("BusinessName")%></a>
                                                <%}
                                                  else
                                                  { %>
                                                组团社：<a target="_blank" href="<%#Eval("CompanyType")!=null?EyouSoft.Common.Utils.GetShopUrl(Eval("Travel").ToString()):"javascript:void(0);" %>"><%#Eval("TravelName")%></a>
                                                <%} %>
                                            </td>
                                            <td align="left">
                                                 <%#Eval("TravelContact")%><br />
                                                 <%#Eval("TravelTel")%>
                                            </td>
                                            <td align="center">
                                                 <%#((DateTime)Eval("StartDate")).ToString("MM-dd")%>
                                            </td>
                                            <td align="center">
                                                 <%#((DateTime)Eval("IssueTime")).ToString("MM-dd HH:mm")%>
                                            </td>
                                            <td align="center">
                                                 <%#Eval("ScheduleNum")%>
                                            </td>
                                            <td align="center">
                                                 <%#Eval("OrderStatus")%>
                                            </td>
                                            <td align="center">
                                                <a class="a_show" href="/TeamService/SingleGroupPre.aspx?tourId=<%#Eval("TourId") %>&intRouteSource=<%=intRouteSource %>">查看</a><br />
                                                <a orderstatus="<%#(int)Eval("OrderStatus") %>" class="a_TourOrderStatusChange" href="/TeamService/TourOrderStatusChange.ashx?tourId=<%#Eval("TourId") %>&intStatus=3">取消</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:repeater>
                </table>
                <asp:panel id="pnlNodata" runat="server" visible="false">
                     <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                        <tr>
                            <td>暂无数据!</td>
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
            <%--<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <strong>团队订单和散客订单有什么差别?</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>答</strong>：答：由于团队订单比散客订单复杂，本网站的团队订单仅作为双方协商记录所用，订单的取消权限俱在组团社方。
                    </td>
                </tr>
            </table>--%>
        </div>
        <!--表格盒子结束-->
    </div>

    <script type="text/javascript">


        var TeamOrders = {
            Init: function() {
                var form = $("#<%=Key %>");
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
                form.find("#tab_list .a_show").click(function() {
                    if ('<%=routeSource==null %>' == "True") {
                        topTab.open($(this).attr("href") + "&isZT=true", "组团社-订单查看", {})
                    }
                    else {
                        topTab.open($(this).attr("href"), "专线商-订单查看", {})
                    }

                    return false;
                })
                form.find("#ExportPageInfo a").click(function() {
                    topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                    return false;
                })
                form.find(".a_TourOrderStatusChange").each(function() {
                    if ($(this).attr("orderstatus") != "0"/*未确定状态*/) {
                        $(this).remove();
                    }
                })

            },
            //设置选中状态
            SetSelectStatus: function(obj) {
                var form = $("#<%=Key %>");
                form.find("#tab_status .ff0000").removeClass("ff0000");
                form.find("#tab_status #status_" + $.trim($(obj).attr("val") || $(obj).val())).addClass("ff0000");
            },
            GetList: function() {
                var form = $("#<%=Key %>");
                form.find("#tab_list").html('<div id="div_load"><img src="<%= ImgURL%>/images/default/tree/loading.gif" alt="加载中......" />加载中......</div>')
                //查询参数对象
                var SelectData = {
                    keyWord: "", //关键字
                    goTimeS: "", //出团时间开始
                    goTimeE: "", //出团时间结束
                    status: "", //状态
                    companyID: "", //公司id
                    routeSource: ""
                }
                var statusid = form.find("#tab_status .ff0000").attr("val");
                form.find("#<%=sel_status.ClientID %>").val(statusid);

                SelectData.routeSource = '<%=intRouteSource %>';
                SelectData.companyID = '<%=SiteUserInfo.CompanyID %>'
                //状态
                SelectData.status = $.trim(statusid); ;
                //关键字
                SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
                //出团时间开始
                SelectData.goTimeS = $.trim(form.find("#<%=txt_goTimeS.ClientID %>").val());
                //出团时间结束
                SelectData.goTimeE = $.trim(form.find("#<%=txt_goTimeE.ClientID %>").val());
                $.newAjax({
                    type: "get",
                    url: "/teamservice/AjaxTeamOrders.aspx",
                    data: SelectData,
                    cache: false,
                    dataType: "html",
                    success: function(html) {
                        form.find("#tab_list").html(html);
                        TeamOrders.Init();
                    }, error: function() {
                        alert("获取异常")
                        topTab.url(topTab.activeTabIndex, "/TeamService/TeamOrders.aspx?routeSource=<%=intRouteSource %>");
                    }
                })
            },
            TourOrderStatusChange: function(title, url) {
                if (confirm("确定" + title + "?")) {
                    $.newAjax(
	               {
	                   url: url,
	                   dataType: "html",
	                   cache: false,
	                   type: "get",
	                   success: function(result) {
	                       if (result.toLowerCase() == "true") {
	                           alert(title + "成功!")
	                           topTab.url(topTab.activeTabIndex, "/TeamService/TeamOrders.aspx?routeSource=<%=intRouteSource %>");
	                       }
	                       else {
	                           alert("设置失败！")
	                       }
	                   },
	                   error: function() {
	                       alert("操作失败!");
	                   }
	               });
                }
            }
        }
        $(function() {
            var form = $("#<%=Key %>")
            //回车查询效果
            form.find(".keydownSelect").keydown(function(e) {
                if (e.keyCode == 13) {
                    TeamOrders.GetList();
                    return false;
                } else {
                    return true;
                }
            });
            TeamOrders.Init();
            form.find("#<%=sel_status.ClientID %>").change(function() {
                TeamOrders.SetSelectStatus(this)
            })
            form.find("#tab_status a").click(function() {
                TeamOrders.SetSelectStatus(this)
                TeamOrders.GetList();
            })
            form.find(".a_TourOrderStatusChange").click(function() {
                TeamOrders.TourOrderStatusChange($(this).text(), $(this).attr("href"))
                return false;
            })

        })
    </script>

</asp:Content>
