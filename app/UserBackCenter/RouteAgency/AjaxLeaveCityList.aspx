<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxLeaveCityList.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AjaxLeaveCityList" %>

<asp:datalist id="LeaveCity" runat="server" repeatcolumns="15" borderwidth="0px"
    cellpadding="0" cellspacing="0" horizontalalign="Left" itemstyle-verticalalign="Top"
    repeatdirection="Horizontal">
        <ItemStyle ForeColor="red" />
        <ItemTemplate>
            <%--<input type="checkbox" id="LeaveCity" name="LeaveCity" value='<%# DataBinder.Eval(Container.DataItem,"ID") %>' />
            <%# DataBinder.Eval(Container.DataItem, "CityName")%>--%>
        </ItemTemplate>
    </asp:datalist>
<table border="0" cellpadding="0" cellspacing="0" runat="server" id="table_NoData"
    visible="false">
    <tr>
        <td valign="top" align="left" style="color: Red">
            <asp:label id="lblError" runat="server" text="请先选择线路区域!"></asp:label>
        </td>
    </tr>
</table>
