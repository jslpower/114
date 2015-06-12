<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetPayMoneys.aspx.cs" Inherits="UserBackCenter.SMSCenter.GetPayMoneys" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<cc2:CustomRepeater ID="GetPayMoneys_repPayMoneyList" runat="server">
    <ItemTemplate>
        <div>
            <%# Convert.ToDateTime(Eval("PayTime").ToString()).ToShortDateString().Replace("-","/")%>&nbsp;&nbsp;&nbsp;&nbsp;充值<%#Convert.ToDecimal( Eval("PayMoney").ToString()).ToString("F2")%>元&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("IsChecked").ToString() == "1" ? "充值成功    获得可用金额" + Eval("UseMoney","{0:f2}") + "元" : "未通过"%>
            &nbsp;&nbsp;<%#Eval("UserFullName")%></div>
    </ItemTemplate>
</cc2:CustomRepeater>
<div align="center" class="digg">
    <cc2:ExporPageInfoSelect ID="GetPayMoneys_ExporPageInfoSelect" PageStyleType="NewButton"
        runat="server" />
</div>
