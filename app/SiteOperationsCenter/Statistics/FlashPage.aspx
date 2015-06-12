<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlashPage.aspx.cs" Inherits="SiteOperationsCenter.Statistics.FlashPage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="OpenFlashChart" Namespace="OpenFlashChart" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:OpenFlashChartControl ID="OpenFlashChartControl1" runat="server" EnableCache="false"
        LoadingMsg="正在加载..." WMode="transparent">
    </cc1:OpenFlashChartControl>
    <asp:HiddenField ID="hideType" runat="server" />
    
    </form>
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript">
        var strStartDate = "<%=strStartDate %>";
        var strEndDate = "<%=strEndDate %>";


        var dateStartArr = strStartDate.split(',');
        var dateEndArr = strEndDate.split(',');

        //打开订单购买的列表页面
        function LineClickBuy(index) {
            if (dateStartArr != null && index >= 0 && index < dateStartArr.length) {
                if (parent != null && $("#hideType").val()=="0") {
                    parent.OpenOrderPageByFlash('1', dateStartArr[index], dateEndArr[index]);
                } else {
                    OpenOrderPageByFlash('1', dateStartArr[index], dateEndArr[index]);
                }
            }
        }
        //打开订单留位的列表页面
        function LineClickReserveTimeIn(index) {
            if (dateStartArr != null && index >= 0 && index < dateStartArr.length) {
                if (parent != null && $("#hideType").val() == "0") {
                    parent.OpenOrderPageByFlash("4", dateStartArr[index], dateEndArr[index]);
                } else {
                    OpenOrderPageByFlash('4', dateStartArr[index], dateEndArr[index]);
                }
            }
        }
        //打开留位过期的列表页面
        function LineClickReserveTimeOut(index) {
            if (dateStartArr != null && index >= 0 && index < dateStartArr.length) {
                if (parent != null && $("#hideType").val() == "0") {
                    parent.OpenOrderPageByFlash("2", dateStartArr[index], dateEndArr[index]);
                } else {
                    OpenOrderPageByFlash('2', dateStartArr[index], dateEndArr[index]);
                }
            }
        }
        //打开不受理订单的列表页面
        function LineClickNoDeal(index) {
            if (dateStartArr != null && index >= 0 && index < dateStartArr.length) {
                if (parent != null && $("#hideType").val() == "0") {
                    parent.OpenOrderPageByFlash("3", dateStartArr[index], dateEndArr[index]);
                } else {
                    OpenOrderPageByFlash('3', dateStartArr[index], dateEndArr[index]);
                }
            }
        }
        function OpenOrderPageByFlash(orderType, startTime, endTime) {
            var strdate = "orderIndex=2&type=1&orderType=" + orderType + "&startTime=" + startTime + "&endTime=" + endTime;
            switch (orderType) {
                case "1": OpenDialog("LineOrderDetailsList.aspx", "成交订单", 780, 400, strdate); break;
                case "2": OpenDialog("LineOrderDetailsList.aspx", "留位过期订单", 780, 400, strdate); break;
                case "3": OpenDialog("LineOrderDetailsList.aspx", "无效订单", 780, 400, strdate); break;
                case "4": OpenDialog("LineOrderDetailsList.aspx", "已留位订单", 780, 400, strdate); break;
            }
        }

        function OpenDialog(strurl, strtitle, strwidth, strheight, strdate) {
            Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate, fixed: false });
        }
    </script>

</body>
</html>
