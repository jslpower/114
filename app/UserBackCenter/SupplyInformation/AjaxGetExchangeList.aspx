<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxGetExchangeList.aspx.cs"
    Inherits="UserBackCenter.SupplyInformation.AjaxGetExchangeList" %>

<asp:datalist id="dlExchangeList" runat="server" repeatcolumns="2" horizontalalign="left"
    repeatdirection="Horizontal">
        <ItemTemplate>
            <li><span class="gqbiaoqian<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ExchangeTag"))%>"><strong>[<%# DataBinder.Eval(Container.DataItem, "ExchangeTag")%>]</strong></span>&nbsp;<a href='<%=strDomain %>/SupplierInfo/ExchangeInfo.aspx?Id=<%# DataBinder.Eval(Container.DataItem,"ID") %>' target="_blank"><%# EyouSoft.Common.Utils.GetText(DataBinder.Eval(Container.DataItem, "ExchangeTitle").ToString(), 16, true)%></a></li>
        </ItemTemplate>
    </asp:datalist>
