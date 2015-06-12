<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="FITOrders.aspx.cs" Inherits="UserBackCenter.TeamService.FITOrders" %>

<asp:Content ID="FITOrders" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="ControlLibrary" namespace="Adpost.Common.ExportPageSet" tagprefix="cc1" %>

    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'FITOrders'
        });
    </script>

    <div id="<%=Key %>" class="right">
        <!--表格盒子开始-->
        <div class="tablebox">
            <table id="tab_areaType" border="0" align="center" cellpadding="0" cellspacing="0"
                style="width: 100%;" class="toolbj1">
                <tr>
                    <td align="left" class="title">
                        订单区域：
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <a href="javascript:void(0);" val="0">国内</a> <a href="javascript:void(0);" val="1">国际</a>
                        <a href="javascript:void(0);" val="2">周边</a>
                    </td>
                </tr>
            </table>
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <span class="search">&nbsp;</span>关键字
                        <input id="txt_keyWord" class="keydownSelect" runat="server" value="" size="30" />
                        订单状态：
                        <asp:dropdownlist runat="server" id="sel_status">
                        <asp:listItem value="-1">-全部-</asp:listItem>
                        </asp:dropdownlist>
                        出团日期
                        <input id="txt_goTimeS" class="keydownSelect" onfocus="WdatePicker();" runat="server"
                            size="30" style="width: 80px;" />
                        至
                        <input id="txt_goTimeE" class="keydownSelect" onfocus="WdatePicker();" runat="server"
                            size="30" style="width: 80px;" />
                        <button type="button" class="search-btn" onclick="FITOrders.GetList()">
                            搜索</button>
                    </td>
                </tr>
            </table>
            <table id="tab_status" border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/lmnavm.gif); height: 32px;">
                    <td width="1%" align="left">
                        &nbsp;
                    </td>
                    <td width="99%" align="left">
                        <span class="guestmenu">订单状态</span>
                        <asp:repeater runat="server" id="rpt_powderOrderStatus">
                            <ItemTemplate>
                                <a href="javascript:void(0);" id="status_<%#Eval("Value") %>"   val="<%#Eval("Value") %>"><%#Eval("Text") %></a>
                            </ItemTemplate>
                        </asp:repeater>
                        <a href="javascript:void(0);" id="status_-1" style="display: none" val="-1">全部</a>
                    </td>
                </tr>
            </table>
            <div id="tab_list">
                <table width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                    class="liststylesp">
                    <tr class="list_basicbg">
                        <th class="list_basicbg">
                            团号
                        </th>
                        <th class="list_basicbg" style="width: 400px">
                            线路名称
                        </th>
                        <th class="list_basicbg">
                            出发时间
                        </th>
                        <th class="list_basicbg">
                            游客
                        </th>
                        <th class="list_basicbg">
                            预定时间
                        </th>
                        <th class="list_basicbg">
                            人数
                        </th>
                        <th class="list_basicbg">
                            订单状态
                        </th>
                        <th class="list_basicbg">
                            支付状态
                        </th>
                        <%--<th class="list_basicbg">
                                        状态修改
                                    </th>--%>
                        <th class="list_basicbg">
                            打印
                        </th>
                        <th class="list_basicbg">
                            操作
                        </th>
                    </tr>
                    <asp:repeater runat="server" id="rpt_list">
                                    <ItemTemplate>
                                        <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                                            <td height="30" align="center">
                                                <%#Eval("TourNo")%>
                                            </td>
                                            <td align="left">
                                                <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank"><%#Eval("RouteName")%></a><br />
                                                专线商：<a target="_blank" href="<%#Eval("CompanyType")!=null?EyouSoft.Common.Utils.GetCompanyDomain(Eval("Publishers").ToString(),(EyouSoft.Model.CompanyStructure.CompanyType)Eval("CompanyType")):"javascript:void(0);" %>"><%#Eval("PublishersName")%></a>
                                            </td>
                                            <td align="center">
                                                <%#((DateTime)Eval("LeaveDate")).ToString("MM-dd")%>
                                            </td>
                                            <td align="left">
                                              <%#Eval("VisitorContact")%><br />
                                              <%#Eval("VisitorTel")%>
                                            </td>
                                            <td align="center">
                                               <%#Eval("IssueTime")%>
                                            </td>
                                            <td align="center">
                                                <%#Eval("AdultNum")%><sup>+<%#Eval("ChildrenNum")%></sup>
                                            </td>
                                            <td align="center">
                                                <%#Eval("OrderStatus")%>
                                            </td>
                                            <td align="left">
                                                <%#Eval("PaymentStatus")%>
                                            </td>
                                            <%--<td align="left">
                                                <a href="#" class="basic_btn"><span>取消</span></a>
                                            </td>--%>
                                            <td align="center">
                                                <a href='/PrintPage/TeamConfirm.aspx?OrderId=<%#Eval("OrderId") %>' target="_blank">团队确认单</a>
                                            </td>
                                            <td align="center" nowrap="nowrap">
                                                <a href="javascript:void(0);" onclick="javascript:topTab.open('/order/touragency/orderupdate.aspx?orderID=<%#Eval("OrderID") %>','订单查看',{isOpen:false})">查看</a><br />
                             
                                                <a href="/RouteAgency/OrderStateLog.aspx?orderID=<%#Eval("OrderId") %>" target="_blank">日志</a>
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
            <%--<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <strong>线路团队订单管理的权限有哪些?</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>答</strong>：答：组团社有预定线路团队，收取游客定金和全款，以及取消订单的权力。而专线商有预留团队空位，确认收到组团社款项的权力。专线商无权取消订单，订单必须由组团社提出确认。业务过程中，由于订单产生的纠纷请双方线下协商，与本网站无关。
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>是不是收款后必须点击收款的按钮？</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <strong>答</strong>：不需要，收款只是您的订单记录，但是建议当汇款给专线商后，请你提醒专线商点选确认收款的按钮，以减少纠纷。
                    </td>
                </tr>
            </table>--%>
        </div>
        <!--表格盒子结束-->
    </div>

    <script type="text/javascript">
        var FITOrders = {
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
                form.find("#ExportPageInfo a").click(function() {
                    topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                    return false;
                })
            },
            //设置选中
            SetSelectStatus: function(obj) {
                var form = $("#<%=Key %>");
                form.find("#tab_status").find(".ff0000").removeClass("ff0000");
                form.find("#tab_status").find("#status_" + $(obj).val()).addClass("ff0000");
            },
            //设置选中
            SetSelected: function(obj/*选中对象*/, className/*选中状态class名称*/) {
                $(obj).parent().find("." + className).removeClass(className);
                $(obj).addClass(className);
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
                    area: "", //订单区域
                    companyID: ""//公司id
                }
                SelectData.companyID = '<%=SiteUserInfo.CompanyID %>';
                var statusid = form.find("#tab_status .ff0000").attr("val");
                form.find("#sel_status").val(statusid);
                //状态
                SelectData.status = statusid;
                //关键字
                SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
                //出团时间开始
                SelectData.goTimeS = $.trim(form.find("#<%=txt_goTimeS.ClientID %>").val());
                //出团时间结束
                SelectData.goTimeE = $.trim(form.find("#<%=txt_goTimeE.ClientID %>").val());
                //线路区域
                SelectData.area = $.trim(form.find("#tab_areaType .select").attr("val"));
                $.newAjax({
                    type: "get",
                    url: "/teamservice/ajaxfitorders.aspx",
                    data: SelectData,
                    cache: false,
                    dataType: "html",
                    success: function(html) {
                        form.find("#tab_list").html(html);
                        FITOrders.Init();
                    }, error: function() {
                        alert("获取异常")
                    }
                })
            }
        }
        $(function() {
            var form = $("#<%=Key %>");
            //回车查询效果
            form.find(".keydownSelect").keydown(function(e) {
                if (e.keyCode == 13) {
                    FITOrders.GetList();
                    return false;
                } else {
                    return true;
                }
            });
            form.find("#tab_areaType a[val='<%=area %>']").addClass("select");
            FITOrders.Init();
            form.find("#tab_status a").click(function() {
                form.find("#<%=sel_status.ClientID %>").val($(this).attr("val"))
                FITOrders.SetSelected(this, "ff0000");
                FITOrders.GetList();
                return false;
            })
            form.find("#<%=sel_status.ClientID %>").change(function() {
                if ($(this).val() == "4,1,5") {
                    FITOrders.SetSelected(form.find("#tab_status a[val='4,1,5']"), "ff0000");
                }
                else {
                    FITOrders.SetSelected(form.find("#tab_status #status_" + $(this).val()), "ff0000");
                }
                return false;
            })
            form.find("#tab_areaType a").click(function() {
                FITOrders.SetSelected(this, "select");
                FITOrders.GetList();
                return false;
            })
        })
    </script>

</asp:Content>
