<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IMOrderList.aspx.cs" Inherits="IMFrame.RouteAgency.TourOrder.IMOrderList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>订单列表</title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        var OrderList = {
            isGrant: "<%=isGrant %>",
            dialog: function(title, obj, width, height, orderId) {
                if (OrderList.isGrant == "False") {
                    alert("对不起，你目前的帐号没有修改权限！");
                    return;
                } else {
                    var url = $(obj).attr("dialogUrl");
                    Boxy.iframeDialog({ title: title, iframeUrl: url, width: 800, height: GetAddOrderHeight(), draggable: true});
                }
            },
            ReloadPrent: function() {
                window.location.reload();
            }
        }

        function mouseovertr(o) { o.style.backgroundColor = "#FFF6C7"; }
        function mouseouttr(o) { o.style.backgroundColor = ""; }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="tbl_OrdersReceived" width="98%" border="0" align="center" cellpadding="0"
        cellspacing="0" style="margin-top: 10px; margin-bottom: 3px;">
        <tr>
            <td height="48" colspan="2" align="left" <%-- background="/images/newhang5bg.gif"--%>
                bgcolor="#E9F2FB" style="border: 1px solid #C7D9EB">
                &nbsp;<img src="<%=ImageServerUrl %>/images/ttt.gif" width="15" height="16">
                团号：<asp:Label runat="server" ID="labTourNo"></asp:Label>
                &nbsp;<a href="<%=EyouSoft.Common.Domain.UserBackCenter %>/PrintPage/TeamInformationPrintPage.aspx?TourID=<%= TourId %>"
                    target="_blank"><asp:Label runat="server" ID="labRouteName"></asp:Label><asp:Label
                        runat="server" ID="labLeaveDate"></asp:Label>
                </a>
                <br />
                &nbsp;当前空位<strong class="font"><%#Eval("RealRemnantNumber")%></strong>个；现有 <span
                    class="chengse"><strong>
                        <asp:Label runat="server" ID="labBuyCompanyNumber"></asp:Label>
                        家</strong></span> 零售商共采购 <strong class="chengse">
                            <asp:Label runat="server" ID="labBuySumNumber"></asp:Label>
                        </strong>个位置
            </td>
        </tr>
        <tr>
            <td width="5%" align="right" valign="top" bgcolor="#E9F2FB">
                <img src="<%=ImageServerUrl %>/images/zhexian.gif" alt="查看该线路被预定信息" width="20" height="22">
            </td>
            <td width="95%" bgcolor="#E9F2FB" style="padding: 5px;">
                <table width="100%" border="1" cellpadding="1" cellspacing="0">
                    <asp:Repeater runat="server" ID="repOrderList">
                        <HeaderTemplate>
                            <tr class="white" style="font-size: 13px;">
                                <th height="20" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                    <strong>零售商</strong>
                                </th>
                                <th width="9%" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                    <strong>联系人</strong>
                                </th>
                                <th width="14%" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                    <strong>电话</strong>
                                </th>
                                <th width="10%" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                    <strong>预定时间</strong>
                                </th>
                                <th width="6%" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                    <strong>人数</strong>
                                </th>
                                <th width="9%" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                    <strong>金额</strong>
                                </th>
                                <th width="16%" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                    <strong>操作</strong>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="text-align: center" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                                <td height="30">
                                    <!--零售商-->
                                    <a href="<%=EyouSoft.Common.Domain.UserBackCenter %>/PrintPage/TourConfirmation.aspx?type=1&OrderID=<%#Eval("ID") %>&TourID=<%#Eval("TourID") %>"
                                        target="_blank">
                                        <%#Eval("BuyCompanyName")%></a>
                                </td>
                                <td>
                                    <!--联系人-->
                                    <%#Eval("ContactName")%>
                                </td>
                                <td>
                                    <!--电话-->
                                    <%#Eval("ContactTel")%>
                                </td>
                                <td>
                                    <!--预定时间-->
                                    <%#Eval("IssueTime","{0:MM-dd hh:mm}")%>
                                </td>
                                <td>
                                    <!--人数-->
                                    <a href="<%=EyouSoft.Common.Domain.UserBackCenter %>/PrintPage/BookingList.aspx?TourID=<%#Eval("TourID") %>&OrderID=<%#Eval("ID") %>"
                                        target="_blank">
                                        <%#Eval("AdultNumber")%><sup>+<%#Eval("ChildNumber")%></sup></a>
                                </td>
                                <td>
                                    <!--金额-->
                                    <strong>
                                        <%#Eval("SumPrice","{0:F2}")%></strong>
                                </td>
                                <td>
                                    <!--操作-->
                                    <input type="button" name="Submit" value="<%#GetSateName((int)Eval("OrderState"),Eval("SaveSeatDate")) %>"
                                        dialogurl='<%#  Utils.GetDesPlatformUrlForMQMsg (EyouSoft.Common.Domain.UserBackCenter+"/UserOrder/EditOrder.aspx?action=OrdersReceived&OrderCallBack=OrderList.ReloadPrent&OrderID="+Eval("ID"),SiteUserInfo.ContactInfo.MQ,SiteUserInfo.PassWordInfo.MD5Password) %>'
                                        onclick="OrderList.dialog('处理订单',this,930,500,'<%#Eval("ID") %>')" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
