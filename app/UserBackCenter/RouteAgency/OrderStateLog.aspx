<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderStateLog.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.OrderStateLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单操作日志</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="800" border="0" align="center">
            <tbody>
                <tr>
                    <td width="695" height="25">
                        <strong>订单操作记录</strong>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="800" cellspacing="1" cellpadding="0" border="0" bgcolor="#000000" align="center"
            style="margin-top: 5px;" class="padd5">
            <tbody>
                <tr>
                    <th height="25" bgcolor="#FFFFFF" align="center">
                        序号
                    </th>
                    <th bgcolor="#FFFFFF" align="center">
                        订单号
                    </th>
                    <th bgcolor="#FFFFFF" align="center">
                        管理公司
                    </th>
                    <th bgcolor="#FFFFFF" align="center">
                        操作员
                    </th>
                    <th bgcolor="#FFFFFF" align="center">
                        类型
                    </th>
                    <th bgcolor="#FFFFFF" align="center">
                        操作日期
                    </th>
                    <th bgcolor="#FFFFFF" align="center">
                        操作描述
                    </th>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="25" bgcolor="#FFFFFF" align="left">
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td bgcolor="#FFFFFF" align="left">
                                <%#Eval("OrderNo")%>
                            </td>
                            <td bgcolor="#FFFFFF" align="left">
                                <%#Eval("CompanyName")%>
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <%#Eval("OperatorName")%>
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <%#Eval("OrderType").ToString()%>
                            </td>
                            <td bgcolor="#FFFFFF" align="left">
                                <%#Convert.ToDateTime( Eval("IssueTime")).ToString("yyyy-MM-dd")%> <%#Convert.ToDateTime(Eval("IssueTime")).ToLongTimeString()%>
                            </td>
                            <td bgcolor="#FFFFFF" align="left">
                                <%#Eval("Remark")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
