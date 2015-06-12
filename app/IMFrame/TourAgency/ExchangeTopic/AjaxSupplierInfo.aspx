<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSupplierInfo.aspx.cs"
    Inherits="IMFrame.ExchangeTopic.AjaxSupplierInfo" %>
<head runat="server"></head>
<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="liststyle"
    style="margin: 10px 0 10px 0; border-bottom: 1px solid #ccc;">
    <tr>
    <td width="80%" align="left" class="hdbartable">推荐信息</td>
    <td width="20%" align="center" class="hdbartable"> 时间</td>
  </tr>
    <asp:repeater runat="server" id="rptExchangeList" onitemdatabound="RepeatorList_ItemDataBound">
        <ItemTemplate>
            <tr>
                <td width="76%" height="24" class="lan14">
                    <div style="float: left;">
                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
                    </div>
                </td>
                <td width="4%" class="huise">
                    <%# Eval("IssueTime","{0:MM.dd}")%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr bgcolor="#EFEFEF">
                <td width="76%" height="24" class="lan14">
                    <div style="float: left;">
                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
                    </div>
                </td>
                <td width="4%" class="huise">
                    <%# Eval("IssueTime","{0:MM.dd}")%>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:repeater>
    <input type="hidden" id="hPageSize" value="<%= intPageSize %>" />
    <input type="hidden" id="hPageIndex" value="<%= CurrencyPage %>" />
    <input type="hidden" id="hRecordCount" value="<%= intRecordCount %>" />
</table>
