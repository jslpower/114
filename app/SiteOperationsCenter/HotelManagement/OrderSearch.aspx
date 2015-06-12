<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSearch.aspx.cs" Inherits="SiteOperationsCenter.HotelManagement.OrderSearch" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>酒店订单查询</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript">
        //查询参数
        var Parms = { CustomerName: "", OrderId: "", CreateSDate: "", CreateEDate: "", CheckInSDate: "", CheckInEDate: "", CheckOutSDate: "", CheckOutEDate: "", HotelName: "", OrderSate: "", Page: 1 };
        var OrderSearch = {
            //获取查询数据
            GetOrderSearchList: function() {
                var self = this;
                LoadingImg.ShowLoading("DivSearchOrderList");
                if (LoadingImg.IsLoadAddDataToDiv("DivSearchOrderList")) {
                    $.ajax({
                        type: "GET",
                        dateType: "html",
                        url: "AjaxSearchOrderInfo.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#DivSearchOrderList").html(html);
                        }
                    });
                }
            },
            //得到用户详细信息
            GetCompanyDetailInfo: function(BuyerCId, event1, this1) {
                var tarThis = $(this1);
                if (tarThis.attr("data") != "") {
                    $.ajax(
	                 {
	                     url: "OrderSearch.aspx",
	                     data: "BuyerCId=" + BuyerCId + "&method=GetComapnyDetailInfo",
	                     dataType: "text",
	                     cache: false,
	                     type: "GET",
	                     async: true,
	                     success: function(result) {
	                         wsug(this1, result);
	                     },
	                     error: function() {
	                         wsug(this1, "操作失败!");
	                     }
	                 });
                }
                else {
                    wsug(this1, tarThis.attr("data"));
                }
            },
            SetHotelOrderState: function(HotelOrderID,State) {
                $.ajax({
                    url: "OrderSearch.aspx",
                    data: "HotelOrderID=" + HotelOrderID + "&State=" + State + "&method=SetHotelOrderState",
                    dataType: "text",
                    cache: false,
                    type: "GET",
                    async: true,
                    success: function(result) {
                        if (result == "True") {
                            alert("订单状态修改成功!");
                            OrderSearch.GetOrderSearchList();
                        } else {
                            alert("订单状态修改失败!");
                        }
                    }
                });
            },
            SetHotelCheckState: function(HotelOrderID, CheckState) {
                $.ajax({
                    url: "OrderSearch.aspx",
                    data: "HotelOrderID=" + HotelOrderID + "&CheckState=" +  CheckState + "&method=SetHotelCheckState",
                    dataType: "text",
                    cache: false,
                    type: "GET",
                    async: true,
                    success: function(result) {
                        if (result == "True") {
                            alert("审核状态修改成功!");
                            OrderSearch.GetOrderSearchList();
                        } else {
                            alert("审核状态修改失败!");
                        }
                    }
                });
            },
            LoadData: function(obj) {       //分页
                var Page = exporpage.getgotopage(obj);
                Parms.Page = Page;
                this.GetOrderSearchList();
            },
            OnSearch: function() {              //查询
                Parms.CustomerName = $.trim($("#txtCustomerName").val());
                Parms.OrderId = $.trim($("#txtOrderId").val());
                Parms.CreateSDate = $.trim($("#txtCreateSDate").val());
                Parms.CreateEDate = $.trim($("#txtCreateEDate").val());
                Parms.CheckInSDate = $.trim($("#txtCheckInSDate").val());
                Parms.CheckInEDate = $.trim($("#txtCheckInEDate").val());
                Parms.CheckOutSDate = $.trim($("#txtCheckOutSDate").val());
                Parms.CheckOutEDate = $.trim($("#txtCheckOutEDate").val());
                Parms.HotelName = $.trim($("#txtHotelName").val());
                Parms.OrderSate = $("#tbOrderListSearch").find("input[type='radio']:checked").val();
                Parms.Page = 1;
                OrderSearch.GetOrderSearchList();
            },
            openDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
                Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
            },
            LookHotelAoccount: function(CompanyId) {
                OrderSearch.openDialog("HotelAccountInfo.aspx", "查看酒店开户账户", "400", "150", "CompanyId=" + CompanyId);
            }
        };
        $(function() {
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            $("#tbOrderListSearch input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    OrderSearch.OnSearch();
                    return false;
                }
            });
        });
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="26">
                <table width="116" border="1" cellpadding="0" cellspacing="0" bordercolor="#FFFFFF">
                    <tr>
                        <td width="114" height="26" align="center" background="<%= ImageServerUrl %>/images/jdsymk.gif"
                            class="h14">
                            <font color="#FFFF66">订单查询</font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tbOrderListSearch" width="98%" border="0" align="center" cellpadding="0"
        cellspacing="0" style="border: 1px solid #BDD7FB; margin-bottom: 20px;">
        <tr>
            <td colspan="2" align="right" bgcolor="F3F7FF">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="17%" height="26" align="right" bgcolor="F3F7FF">
                用&nbsp;户&nbsp;名：
            </td>
            <td width="83%" bgcolor="F3F7FF">
                <input id="txtCustomerName" runat="server" name="txtCustomerName" type="text" class="textfield"
                    size="50" />
            </td>
        </tr>
        <tr>
            <td width="17%" height="26" align="right" bgcolor="F3F7FF">
                订单编号：
            </td>
            <td width="83%" bgcolor="F3F7FF">
                <input id="txtOrderId" runat="server" name="txtOrderId" type="text" class="textfield"
                    size="50" />
            </td>
        </tr>
        <tr>
            <td height="26" align="right" bgcolor="F3F7FF">
                预定日期：
            </td>
            <td bgcolor="F3F7FF">
                <input id="txtCreateSDate" runat="server" onfocus="WdatePicker()" name="txtCreateSDate"
                    type="text" class="textfield" size="20" />
                <%--<img src="<%= ImageServerUrl %>/images/time.gif" width="16" height="13" />--%>
                &nbsp;&nbsp;-&nbsp;&nbsp;
                <input id="txtCreateEDate" runat="server" onfocus="WdatePicker()" name="txtCreateEDate"
                    type="text" class="textfield" size="20" />
            </td>
        </tr>
        <tr>
            <td height="26" align="right" bgcolor="F3F7FF">
                入住日期：
            </td>
            <td bgcolor="F3F7FF">
                <input id="txtCheckInSDate" runat="server" onfocus="WdatePicker()" name="txtCheckInSDate"
                    type="text" class="textfield" size="20" />
                &nbsp;&nbsp;-&nbsp;&nbsp;
                <input id="txtCheckInEDate" runat="server" onfocus="WdatePicker()" name="txtCheckInEdate"
                    type="text" class="textfield" size="20" />
            </td>
        </tr>
        <tr>
            <td height="26" align="right" bgcolor="F3F7FF">
                离店日期：
            </td>
            <td bgcolor="F3F7FF">
                <input id="txtCheckOutSDate" runat="server" onfocus="WdatePicker()" name="txtCheckOutSDate"
                    type="text" class="textfield" size="20" />
                &nbsp;&nbsp;-&nbsp;&nbsp;
                <input id="txtCheckOutEDate" runat="server" onfocus="WdatePicker()" name="txtCheckOutEDate"
                    type="text" class="textfield" size="20" />
            </td>
        </tr>
        <tr>
            <td height="26" align="right" bgcolor="F3F7FF">
                酒店名称：
            </td>
            <td bgcolor="F3F7FF">
                <input id="txtHotelName" runat="server" name="txtHotelName" type="text" class="textfield"
                    size="50" />
            </td>
        </tr>
        <tr>
            <td height="26" align="right" bgcolor="F3F7FF">
                订单状态：
            </td>
            <td bgcolor="F3F7FF">
                <asp:RadioButtonList ID="cbOrderState" runat="server" RepeatColumns="5">
                    <asp:ListItem Value="0">&nbsp;全部</asp:ListItem>
                    <asp:ListItem Value="1">&nbsp;已确认</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;取消</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;处理中</asp:ListItem>
                    <%--<asp:ListItem Value="4">&nbsp;NOWSHOW</asp:ListItem>--%>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td height="26" align="right" bgcolor="F3F7FF">
                &nbsp;
            </td>
            <td bgcolor="F3F7FF">
                <img src="<%= ImageServerUrl %>/images/admin_orderform_ybans_03.jpg" width="79" height="25"
                    border="0" style="margin-bottom: -3px; cursor: pointer" onclick="OrderSearch.OnSearch();" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" bgcolor="F3F7FF">
                &nbsp;
            </td>
        </tr>
    </table>
    <div id="DivSearchOrderList" align="center">
    </div>

    

    </form>
</body>
</html>
