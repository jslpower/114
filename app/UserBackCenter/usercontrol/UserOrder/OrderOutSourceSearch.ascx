<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderOutSourceSearch.ascx.cs"
    Inherits="UserBackCenter.usercontrol.UserOrder.OrderOutSourceSearch" %>
    <table align="center" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td height="30" align="center" bgcolor="#E2F3FC">
                <strong>订单搜索</strong><img src="<%=ImageServerUrl %>/images/ddsearch.gif" width="14"
                    height="14" />线路名称：
                <input size="18" name="textfield" id="txtRouteName" class="ddinput" />
                专线供应商：<asp:DropDownList ID="dplRouteCompany" runat="server">
                    <asp:ListItem>请选择</asp:ListItem>
                </asp:DropDownList>
                出团日期：<input size="5" name="textfield2" id="txtBeginDate" onfocus="WdatePicker()"
                    class="ddinput" />-<input size="5" id="txtEndDate" onfocus="WdatePicker()" class="ddinput" />
                下单日期：<input size="5" name="textfield2" id="txtOrderBeginDate" onfocus="WdatePicker()"
                    class="ddinput" />-<input size="5" id="txtOrderEndDate" onfocus="WdatePicker()" class="ddinput" />
                <input type="button" id="btnSearch"
                    value="搜索订单" />
            </td>
        </tr>
    </table>