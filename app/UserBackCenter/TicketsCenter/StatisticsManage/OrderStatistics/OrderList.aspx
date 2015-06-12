<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="UserBackCenter.TicketsCenter.StatisticsManage.OrderStatistics.OrderList"
    MasterPageFile="~/MasterPage/Site1.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="OrderListStatistics" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="835" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#7dabd8"
        id="tb_OrderList" name="tb_OrderList">
        <tr>
            <th height="35" colspan="10">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="14%" align="center">
                            订单号：
                        </td>
                        <td width="25%" align="left">
                            <input name="txtOrderNumber" type="text" id="txtOrderNumber" size="20" runat="server" />
                        </td>
                        <td width="31%" align="left">
                            订单状态：
                            <select name="sltOrderState" id="Or_sltOrderState" runat="server" onchange="OrderList.OrdChangTypeControl()">
                            </select>
                            <select name="sltOrdChangetype" id="Or_sltOrdChangetype" runat="server">
                            </select>
                            <input type="hidden" id="Or_hidchangeType" runat="server" />
                            <input type="hidden" id="hide_Or_sltOrderState" runat="server" value="" />
                            <input type="hidden" id="hide_Or_sltOrdChangetype" runat="server" value="" />
                        </td>
                        <td width="30%" align="left">
                            <a href="javascript:void(0);" onclick="OrderList.OnSearch();return false;">
                                <img src="<%=ImageServerUrl %>/images/jipiao/admin_orderform_ybans_03.jpg" width="79"
                                    height="25" alt="查询" /></a>
                        </td>
                    </tr>
                </table>
            </th>
        </tr>
        <tr>
            <th width="80" height="30" align="center" bgcolor="#EEF7FF">
                订单号
            </th>
            <th width="80" align="center" bgcolor="#EEF7FF">
                采购商
            </th>
            <th width="80" align="center" bgcolor="#EEF7FF">
                旅客数
            </th>
            <th width="80" align="center" bgcolor="#EEF7FF">
                航程
            </th>
            <th width="126" align="center" bgcolor="#EEF7FF">
                航程起始日
            </th>
            <th width="80" align="center" bgcolor="#EEF7FF">
                运价类型
            </th>
            <th width="80" align="center" bgcolor="#EEF7FF">
                支付时间
            </th>
            <th width="80" align="center" bgcolor="#EEF7FF">
                支付金额(元)
            </th>
            <th width="60" align="center" bgcolor="#EEF7FF">
                出票耗时（分钟）
            </th>
        </tr>
        <asp:repeater id="rptList" runat="server">
         <ItemTemplate>
          <tr>
            <td height="30" align="center"><%#Eval("OrderNo")%></td>
            <td align="center"><%#Eval("BuyerCName") %></td>
            <td align="center"><%#Eval("PCount")%></td>
            <td align="center"><%#Eval("HomeCityName")%> - <%#Eval("DestCityName")%></td>
            <td align="center"><%#Eval("FreightType").ToString() == "1" ? Convert.ToDateTime(Eval("LeaveTime")).ToString("yyyy-MM-dd") : Convert.ToDateTime(Eval("LeaveTime")).ToString("yyyy-MM-dd") + "/" + Convert.ToDateTime(Eval("ReturnTime")).ToString("yyyy-MM-dd")%></td>
            <td align="center"><%#Eval("FreightType").ToString()%> </td>
            <td align="center"><%#Convert.ToDateTime(Eval("PayTime")).ToString("yyyy-MM-dd") == "0001-01-01" ? "未支付" : Convert.ToDateTime(Eval("PayTime")).ToString("yyyy-MM-dd")%></td>
            <td align="center"><%#Utils.GetMoney(Convert.ToDecimal(Eval("TotalAmount")))%></td>
            <td align="center"><%#Eval("ElapsedTime")%></td>
          </tr>
         </ItemTemplate>
       </asp:repeater>
        <tr>
            <td height="30" colspan="9" align="center">
                <asp:label runat="server" text="" id="lblMsg"></asp:label>
                <asp:panel runat="server" id="panIsShow">
            <a href="javascript:void(0);" onclick="OrderList.ExportExcel();return false;">点击下载EXCEL</a></asp:panel>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var OrderList = {
            OnSearch: function() {
                var Searchparms = { "OrderNumber": "", "OrderType": "", "TwoOrderType": "" };

                Searchparms.OrderNumber = $.trim($("#tb_OrderList").find("input[id=<%=txtOrderNumber.ClientID%>]").val());
                Searchparms.OrderType = $.trim($("#tb_OrderList").find("select[id=<%=Or_sltOrderState.ClientID%>] option:selected").val());
                Searchparms.TwoOrderType = $.trim($("#tb_OrderList").find("select[id=<%=Or_sltOrdChangetype.ClientID%>] option:selected").val());

                var goToUrl = " /TicketsCenter/StatisticsManage/OrderStatistics/OrderList.aspx?" + $.param(Searchparms);
                topTab.url(topTab.activeTabIndex, goToUrl);
            },
            ExportExcel: function() {
                var goToUrl = " /TicketsCenter/StatisticsManage/OrderStatistics/OrderList.aspx?isExport=1&<%=Request.QueryString %>";
                window.open(goToUrl, "下载EXCEL");

            },
            OrdChangTypeControl: function() {
                var stateval = $("#<%=Or_sltOrderState.ClientID %>").val();
                var arrtype;
                var changtype = $("#<%=Or_hidchangeType.ClientID%>").val();
                if (changtype.indexOf(",") > 0) {
                    arrtype = changtype.split(',');
                    for (var i = 0; i < arrtype.length - 1; i++) {
                        if (stateval == arrtype[i]) {
                            $('select[id=<%=Or_sltOrdChangetype.ClientID%>] option').each(function() {
                                if ($(this).val() != "0") {
                                    $(this).text(stateval + $(this).val());
                                }
                            });
                            $("#<%=Or_sltOrdChangetype.ClientID %>").show();
                            break;
                        } else {
                            $("#<%=Or_sltOrdChangetype.ClientID %>").hide();
                            $("#<%=Or_sltOrdChangetype.ClientID %>").val("0");
                        }
                    }
                }
            },
            OrdChangTypeSetVal: function() {
                var ordertype = $("#tb_OrderList").find("input[id=<%=hide_Or_sltOrderState.ClientID%>]").val();
                var ordChangetype = $("#tb_OrderList").find("input[id=<%=hide_Or_sltOrdChangetype.ClientID%>]").val();

                if (ordertype != "") {
                    $("#tb_OrderList").find("select[id=<%=Or_sltOrderState.ClientID%>] option").each(function() {

                        if ($(this).val() == ordertype) {
                            $(this).attr("selected", "selected");
                        }
                    });
                }
                if (ordChangetype != "0" && ordChangetype != "") {
                    $("#tb_OrderList").find("select[id=<%=Or_sltOrdChangetype.ClientID%>] option").each(function() {
                        $("#tb_OrderList").find("select[id=<%=Or_sltOrdChangetype.ClientID%>]").show();
                        if ($(this).val() == ordChangetype) {
                            $(this).attr("selected", "selected");
                        }
                    });
                }
            }
        };
        $("#tb_OrderList input").bind("keypress", function(e) {
            if (e.keyCode == 13) {
                OrderList.OnSearch();
                return false;
            }
        });
        $(function() {
            //初始化下拉框
            OrderList.OrdChangTypeControl();
            //查询时候显示查询条件
            OrderList.OrdChangTypeSetVal();
        })
    </script>

</asp:Content>
