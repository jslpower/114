<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HasTradedTeam.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.HasTradedTeam" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=TradedState%>团队列表</title>    
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="950" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2" style="line-height: 30px;">
            <div style="text-align:left; height:30px;">
                <asp:Literal ID="ltrOrderSateText" runat="server" Text="预订列表"></asp:Literal>：</div>                
                <table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#E0E0E0">
                    <tr bgcolor="#DBF7FD">
                        <td width="6%" align="center">
                            <strong>编号 </strong>
                        </td>
                        <td width="21%" align="center">
                            <strong>预订时间</strong>
                        </td>
                        <td width="11%" align="center">
                            <strong>预订人</strong>
                        </td>
                        <td width="12%" align="center">
                            <strong>预订人数</strong>
                        </td>
                        <td width="50%" align="center">
                            <strong>团号及线路信息</strong>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptHasTradedTeam" OnItemDataBound="rptHasTradedTeam_ItemDataBound">
                        <ItemTemplate>
                            <tr class="baidi">
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Literal runat="server" ID="ltrXH"></asp:Literal>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#Eval("OperatorName")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <a href="/PrintPage/BookingList.aspx?TourID=<%#Eval("TourID") %>&OrderID=<%#Eval("ID") %>"
                                        target="_blank">
                                        <%#Eval("AdultNumber")%><sup>+<%#Eval("ChildNumber")%></sup>人</a>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("TourID") %>" target="_blank">【<%#Eval("TourNo")%>】<%#Eval("RouteName")%></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr runat="server" id="NoData" visible="false">
                        <td colspan="5"  bgcolor="#FFFFFF">
                            没有找到与该公司的交易记录！
                        </td>
                    </tr>
                </table>
                <div id="TradedCustomers_ExportPage" class="F2Back" style="text-align: right;" height="40">
                    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"
                        runat="server"></cc2:ExportPageInfo>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
