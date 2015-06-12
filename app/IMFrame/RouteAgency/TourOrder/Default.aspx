<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IMFrame.RouteAgency.TourOrder.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>

<script language="javascript" type="text/javascript">
    function ajaxOrderListLoadData(obj, GetInformationType, OrderSate) {
        var intpage = exporpage.getgotopage(obj);
        GetOrderInfo(intpage, GetInformationType, OrderSate);
    }
    
    function GetOrderInfo(intpage, GetInformationType, OrderSate) {
        $("#div_" + GetInformationType).html("正在加载....");
        $.ajax({
            cache: false,
            url: "/RouteAgency/TourOrder/AjaxOrderList.aspx?Page=" + intpage + "&OrderType=" + GetInformationType + "&OrderSate=" + OrderSate,
            success: function(html) {
                $("#div_" + GetInformationType).html(html);
            }
        });
    }

    function getOrdersReceived(o) {
        $("#div_OrdersReceived").show();
        $("#div_OrderProcessed").hide();
        var InformationType = $(o).attr("id")
        GetOrderInfo(1, InformationType, '');
    }

    function getOrderProcessed(o) {
        $("#div_OrderProcessed").show();
        $("#div_OrdersReceived").hide();
        var GetInformationType = $(o).attr("id")
        GetOrderInfo(1, GetInformationType, '')
    }
</script>

<table id="OrderTable" width="210" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td align="left">
            <div style="border: 1px solid #A1C1EC; background: #C8DCF7; margin-top: 5px; height: 17px;
                padding-top: 3px;">
                <img src="<%=ImageServerUrl %>/im/images/ico1.gif" width="13" height="13" style="margin-bottom: -3px;" />
                <a href="javascript:void(0)" class="bbl" id="OrdersReceived" onclick="getOrdersReceived(this);return false;">
                    待处理订单（<%= OrderOrdersReceivedNum%>）</a>

                <script type="text/javascript">
                    getOrdersReceived(document.getElementById("OrdersReceived"));
                </script>

            </div>
            <div id="div_OrdersReceived">
            </div>
        </td>
    </tr>
    <tr>
        <td align="left">
            <div style="border: 1px solid #A1C1EC; background: #C8DCF7; margin-top: 5px; height: 17px;
                padding-top: 3px;">
                <img src="<%=ImageServerUrl %>/im/images/ico1.gif" width="13" height="13" style="margin-bottom: -3px;" />
                <a href="javascript:void(0)" class="bbl" id="OrderProcessed" onclick="getOrderProcessed(this);return false;">处理中订单（<%=OrderProcessedNum%>）</a>
            </div>
            <div>
                <div id="div_OrderProcessed">
                </div>
            </div>
        </td>
    </tr>
    <tr id="HistoryTourTr" runat="server">
        <td align="left">
            <div style="border: 1px solid #A1C1EC; background: #C8DCF7; margin-top: 5px; height: 17px;
                padding-top: 3px;">
                <img src="<%=ImageServerUrl %>/IM/images/ico1.gif" width="13" height="13" style="margin-bottom: -3px;" />
                <a href="<%=GetDesPlatformUrl(EyouSoft.Common.Domain.UserBackCenter+"/routeagency/allfitorders.aspx") %>"
                    class="bbl" target="_blank" id="OrderHistory">历史订单（<%=OrderHistoryNum%>）</a>
            </div>
        </td>
    </tr>
</table>
