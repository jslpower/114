<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllFITOrders.aspx.cs" Inherits="UserBackCenter.RouteAgency.AllFITOrders" %>

<asp:content id="AllFITOrders" contentplaceholderid="ContentPlaceHolder1" runat="server">
<%@ register assembly="ControlLibrary" namespace="Adpost.Common.ExportPageSet" tagprefix="cc1" %>
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=Key %>',
        ReleaseType: 'AllFITOrders'
    });
    </script>
    <%@ register src="~/usercontrol/ILine.ascx" tagname="ILine" tagprefix="uc1" %>
    <div id="<%=Key %>" class="right">
        <!--表格盒子开始-->
        <div class="tablebox">
            <uc1:ILine ID="ILine1" runat="server" />
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <span class="search">&nbsp;</span>关键字： <em>
                            <input id="txt_keyWord" class="keydownSelect" runat="server" type="text" size="15" style="width: 100px;" />
                        </em>
                         专线：
                       <asp:DropDownList runat="server" id="ddl_ZX">
                           <asp:listItem value="-1">-全部-</asp:listItem>
                       </asp:DropDownList>
                        订单状态：
                        <asp:dropdownlist runat="server" id="sel_status">
                        <asp:listItem value="-1">-全部-</asp:listItem>
                        </asp:dropdownlist>
                       出团日期
                        <input id="txt_goTimeS" class="keydownSelect" onfocus="WdatePicker();" runat="server" size="30" style="width: 80px;" />
                        至
                        <input id="txt_goTimeE" class="keydownSelect" onfocus="WdatePicker();" runat="server" size="30" style="width: 80px;" />
                        <button type="button" class="search-btn" onclick="AllFITOrders.GetList()">
                            搜索</button>
                    </td>
                </tr>
            </table>
            <table id="tab_status" class="liststylesp" border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/lmnavm.gif); height: 32px;">
                    <td width="1%" align="left">
                        &nbsp;
                    </td>
                    <td id="td_powderOrderStatus" width="25%" align="left">
                        <span class="guestmenu">订单状态</span>
                        <asp:repeater runat="server" id="rpt_powderOrderStatus">
                            <ItemTemplate>
                                <a href="javascript:void(0);" id="status_<%#Eval("Value") %>"   val="<%#Eval("Value") %>"><%#Eval("Text") %></a>
                            </ItemTemplate>
                        </asp:repeater>
                        <a href="javascript:void(0);" id="status_-1" style="display:none" val="-1">全部</a>
                    </td>
                    <td width="1%" align="left">
                        &nbsp;
                    </td>
                    <td id="td_paymentStatus" width="40%" align="left">
                        <span class="guestmenu">支付状态</span>
                        <asp:repeater runat="server" id="rpt_paymentStatus">
                            <ItemTemplate>
                                <a href="javascript:void(0);"  val="<%#Eval("Value") %>"><%#Eval("Text") %></a>
                            </ItemTemplate>
                        </asp:repeater>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
                <tr>
                    <td height="26" style="width:90px;" align="center" bgcolor="#E9F2FB">
                        <a href="javascript:void(0);" id="a_sort1" sort="desc" onclick="AllFITOrders.ListSort(this)">出发时间&nbsp;<img
                            src="<%=ImgURL%>/images/xiajt.gif" /><img style="display: none" src="<%=ImgURL%>/images/shangjt.gif" /></a>
                            
                    </td>
                    <td height="26" align="left" bgcolor="#E9F2FB">
                        <a href="javascript:void(0);" id="a_sort2" sort="desc" onclick="AllFITOrders.ListSort(this)">下单时间&nbsp;<img
                            src="<%=ImgURL%>/images/xiajt.gif" /><img style="display: none" src="<%=ImgURL%>/images/shangjt.gif" /></a>
                            
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hd_sort" sort="desc" objid="a_sort1"/>
            <div id="tab_list">
                <table width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#B9D3E7"
                    class="liststyle" style="margin-top: 1px;">
                    <tr class="list_basicbg">
                        <th nowrap="nowrap" class="list_basicbg">
                            订单号
                        </th>
                        <th nowrap="nowrap" class="list_basicbg" style="width: 450px">
                            线路
                        </th>
                        <th nowrap="nowrap" class="list_basicbg">
                            出发时间
                        </th>
                        <th nowrap="nowrap" class="list_basicbg">
                            组团社
                        </th>
                        <th nowrap="nowrap" class="list_basicbg">
                            联系人
                        </th>
                        <th nowrap="nowrap" class="list_basicbg">
                            电话
                        </th>
                        <th nowrap="nowrap" class="list_basicbg">
                            预订时间
                        </th>
                        <th nowrap="nowrap" class="list_basicbg">
                            人数
                        </th>
                        <th nowrap="nowrap" class="list_basicbg">
                            订单状态
                        </th>
                        <th nowrap="nowrap" class="list_basicbg">
                            支付状态
                        </th>
                        <th class="list_basicbg">
                            结算金额
                        </th>
                        <th class="list_basicbg">
                            打印
                        </th>
                        <th class="list_basicbg">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater runat="server" id="rpt_list">
                        <ItemTemplate>
                            <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                                <td height="30" align="left">
                                   <%#Eval("OrderNo")%>
                                </td>
                                <td align="left" style=" padding-left:10px; width:150px">
                                    <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank"><%#Eval("RouteName")%></a>
                                </td>
                                <td align="center">
                                    <%#((DateTime)Eval("LeaveDate")).ToString("MM-dd")%>
                                </td>
                                <td align="left">
                                  <%#Eval("TravelName")%>
                                </td>
                                <td align="center">
                                   <%#Eval("TravelContact")%>
                                </td>
                                <td align="center">
                                   <%#Eval("TravelTel")%>
                                </td>
                                <td align="center">
                                <%#((DateTime)Eval("IssueTime")).ToString("MM-dd HH:mm")%>
                                </td>
                                <td align="center">
                                   <%#Eval("AdultNum")%><sup>+<%#Eval("ChildrenNum")%></sup>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderStatus")%>
                                </td>
                                <td align="center">
                                    <%#Eval("PaymentStatus")%>
                                </td>
                                <td align="left">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("TotalSettlementPrice").ToString())%>
                                </td>
                                <td align="center">
                                    <a href='PrintPage/TeamConfirm.aspx?OrderId=<%#Eval("OrderId") %>' target="_blank">团队确认单</a>
                                </td>
                                <td align="center" nowrap="nowrap">
                                    <a class="a_show" href="/Order/RouteAgency/OrderStateUpdate.aspx?orderID=<%#Eval("OrderId") %>">查看</a><br /> 
                                    <a target="_blank" href="/RouteAgency/OrderStateLog.aspx?orderID=<%#Eval("OrderId") %>">日志</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel runat="server" id="pnlNodata">
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
            <%--<table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="25" align="left">
                        <strong>我的所有订单和最新订单功能有什么差别吗？</strong>
                    </td>
                </tr>
                <tr>
                    <td height="25" align="left">
                        <strong>答</strong>：我的所有订单按照订单预定时间排序，而最新订单按照出团团队线路分别列表，除了表现形式不同，一般功能都一样。唯一不同的是，过期的历史订单，在所有订单中也可是查看。
                    </td>
                </tr>
            </table>--%>
        </div>
        <!--表格盒子结束-->
    </div>

    <script type="text/javascript">

        var AllFITOrders = {
            Init: function() {
                var form = $("#<%=Key %>")
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
                form.find("#tab_list a.a_show").click(function() {
                    topTab.open($(this).attr("href"), "专线商-订单查看", {})
                    return false;
                })
                //分页
                form.find("#ExportPageInfo a").click(function() {
                    topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                    return false;
                })
            },
            //列表排序
            ListSort: function(obj) {
                var tableobj = $(obj).closest("table");
                //单击任意排序，先进行图片初始化,箭头都往下
                tableobj.find("td img:odd").hide();
                tableobj.find("td img:even").show();
                //设置除本身以外的排序方式为降序
                tableobj.find("a:not([id='" + $(obj).attr("id") + "'])").attr("sort", "desc")
                //单独处理本身排序图标
                var tdobj = $(obj).parent("td");
                tdobj.find("img").hide();
                if ($(obj).attr("sort") == "desc") {
                    tdobj.find("img:eq(1)").show();
                    $(obj).attr("sort", "");
                }
                else {
                    tdobj.find("img:eq(0)").show();
                    $(obj).attr("sort", "desc");
                }
                $("#<%=Key %>").find("#hd_sort")
                .attr("sort", $(obj).attr("sort"))
                .attr("objid", $(obj).attr("id"));
                AllFITOrders.GetList();
            },
            //设置选中专线
            SetSelectLine: function(obj) {
                var iLine1 = $("#<%=Key %>" + "_tab_ILine");
                iLine1.find(".select").removeClass("select");
                iLine1.find("[lineid='" + $(obj).val() + "']").addClass("select");
            },
            //设置选中状态
            SetSelectStatus: function(obj) {
                var form = $("#<%=Key %>");
                form.find("#tab_status").find(".ff0000").removeClass("ff0000");
                form.find("#tab_status").find("#status_" + $(obj).val()).addClass("ff0000");
            },
            //设置选择
            SetSelected: function(obj/*选中对象*/, className/*选中状态class名称*/) {
                $(obj).parent().find("." + className).removeClass(className);
                $(obj).addClass(className);
                //AllFITOrders.GetList();
            },

            //获取列表
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
                    status: "", //状态
                    sort: "", //排序
                    sortType: "", //排序类型
                    companyID: "", //公司ID
                    paymentStatus: ""//支付状态
                }

                var tab_ILine_select = form.find("#<%=Key %>_tab_ILine .select");
                form.find("#sel_ZX").val(tab_ILine_select.attr("lineid"));
                var statusid = form.find("#td_powderOrderStatus .ff0000").attr("val");
                form.find("#sel_status").val(statusid);
                SelectData.companyID = '<%=SiteUserInfo.CompanyID %>'
                //订单状态
                SelectData.status = statusid;
                //支付状态
                SelectData.paymentStatus = form.find("#td_paymentStatus .ff0000").attr("val");
                var sort = $("#hd_sort");
                //排序方式
                SelectData.sort = sort.attr("sort") == "desc" ? "desc" : "asc";
                //排序类型
                SelectData.sortType = sort.attr("objid").substr(sort.attr("objid").length - 1, 1); //a_sort1=出发时间,a_sort2=下单时间
                //关键字
                SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
                //出团时间开始
                SelectData.goTimeS = $.trim(form.find("#<%=txt_goTimeS.ClientID %>").val());
                //出团时间结束
                SelectData.goTimeE = $.trim(form.find("#<%=txt_goTimeE.ClientID %>").val());
                //选中的专线对象
                form.find("#<%=ddl_ZX.ClientID %>").val(tab_ILine_select.attr("lineid"));
                //专线id
                SelectData.lineId = tab_ILine_select.attr("lineid");
                //专线类型
                SelectData.lineType = tab_ILine_select.attr("linetype");
                $.newAjax({
                    type: "get",
                    url: "/RouteAgency/AjaxAllFITOrdersList.aspx",
                    data: SelectData,
                    cache: false,
                    dataType: "html",
                    success: function(html) {
                        form.find("#tab_list").html(html);
                        AllFITOrders.Init();
                    }, error: function() {
                        alert("获取异常")
                    }
                })
            }
        }
        $(function() {
            var form = $("#<%=Key %>");
            //专线下拉效果
            form.find("#<%=ddl_ZX.ClientID %>").change(function() {
                AllFITOrders.SetSelectLine(this);
            })
            //订单状态
            form.find("#td_powderOrderStatus a").click(function() {
                form.find("#<%=sel_status.ClientID %>").val($(this).attr("val"))
                AllFITOrders.SetSelected(this, "ff0000");
                AllFITOrders.GetList();
                return false;
            })
            //支付状态
            form.find("#td_paymentStatus a").click(function() {
                AllFITOrders.SetSelected(this, "ff0000");
                AllFITOrders.GetList();
            })
            form.find("#<%=sel_status.ClientID %>").change(function() {
                AllFITOrders.SetSelected(form.find("#tab_status #status_" + $(this).val()), "ff0000");
                return false;
            })
            //回车查询效果
            form.find(".keydownSelect").keydown(function(e) {
                if (e.keyCode == 13) {
                    AllFITOrders.GetList();
                    return false;
                } else {
                    return true;
                }
            });
            AllFITOrders.Init();

        })
    </script>

</asp:content>
