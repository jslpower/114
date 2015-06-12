<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxLineOrder.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.AjaxLineOrder" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="OpenFlashChart" Namespace="OpenFlashChart" TagPrefix="cc1" %>
<form id="form1" runat="server">
<table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#BDD1E4">
    <tr>
        <td width="29%" height="22" align="right" bgcolor="#D8E6F2">
            确认成交订单：
        </td>
        <td width="71%" bgcolor="#FFFFFF">
            <a href="javascript:void(0)" onclick="OpenOrderPage('1','1','')">
                <asp:label runat="server" text="" id="lblDateTime"></asp:label>
                确认成交订单 <span class="blue_bb"><strong>
                    <asp:label runat="server" text="" id="lblCertainCount"></asp:label>
                </strong></span>笔 点击查看详细 </a>
        </td>
    </tr>
    <tr>
        <td height="22" align="right" bgcolor="#D8E6F2">
            留位过期：
        </td>
        <td bgcolor="#FFFFFF">
            <a onclick="OpenOrderPage('2','1','')" href="javascript:void(0)">
                <asp:label runat="server" text="" id="lblExpiredTime"></asp:label>
                留位过期<span class="blue_bb"><strong><asp:label runat="server" text="" id="lblExpiredCount"></asp:label>
                </strong></span>笔 点击查看详细</a>
        </td>
    </tr>
    <tr>
        <td height="22" align="right" bgcolor="#D8E6F2">
            无效订单量：
        </td>
        <td bgcolor="#FFFFFF">
            <a onclick="OpenOrderPage('3','1','')" href="javascript:void(0)">
                <asp:label runat="server" text="" id="lblInvalidTime"></asp:label>
                无效订单<span class="blue_bb"><strong><asp:label runat="server" text="" id="lblInvalidCount"></asp:label>
                </strong></span>笔 点击查看详细</a>
        </td>
    </tr>
</table>
</form>
