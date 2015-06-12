<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataCount.aspx.cs" Inherits="SiteOperationsCenter.PlatformManagement.DataCount" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据统计维护</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="95%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td width="15%" height="22" align="right">
                旅行社数量：
            </td>
            <td width="85%" align="left">
                实际：<strong><asp:Label ID="lblJourneyCount" runat="server"></asp:Label>
                    &nbsp;加
                    <asp:TextBox ID="txtFactJourCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                </strong>
            </td>
        </tr>
        <tr>
            <td height="22" align="right">
                酒店数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblHotelCount" runat="server"></asp:Label>
                    &nbsp;加
                    <asp:TextBox ID="txtFactHotelCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                景区管理公司数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblSightCount" runat="server"></asp:Label>
                    &nbsp;加
                    <asp:TextBox ID="txtFactSightCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                车队数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblCarCount" runat="server"></asp:Label>
                    &nbsp;加
                    <asp:TextBox ID="txtFactCarCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                购物店数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblShopCount" runat="server"></asp:Label>
                    &nbsp;加
                    <asp:TextBox ID="txtFactShopCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                用户数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblUserCount" runat="server"></asp:Label>
                    &nbsp;加
                    <asp:TextBox ID="txtUserCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                供求数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblIntemediCount" runat="server"></asp:Label>
                    &nbsp;加
                    <asp:TextBox ID="txtIntemediCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                MQ数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblMQ_Count" runat="server"></asp:Label>
                    &nbsp;加
                    <asp:TextBox ID="txtMQCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                平台有效线路数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblRouteCount" runat="server"></asp:Label>
                    加 城市
                    <asp:TextBox ID="txtCityCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;全国
                    <asp:TextBox ID="txtWorldCount" runat="server" MaxLength="6" Width="64px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
		<tr>
            <td height="18" align="right">
                平台运价数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblFeightCount" runat="server"></asp:Label>
                    加 
                    <asp:TextBox ID="txtFeightCount" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                采购商数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblBuyers" runat="server"></asp:Label>
                    加 
                    <asp:TextBox ID="txtBuyersVirtual" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                供应商数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblSuppliers" runat="server"></asp:Label>
                    加 
                    <asp:TextBox ID="txtSuppliersVirtual" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                近30天供求数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblSupplyInfos" runat="server"></asp:Label>
                    加 
                    <asp:TextBox ID="txtSupplyInfosVirtual" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                景区数量：
            </td>
            <td align="left">
                实际：<strong><asp:Label ID="lblScenic" runat="server"></asp:Label>
                    加 
                    <asp:TextBox ID="txtScenicVirtual" runat="server" MaxLength="6" Width="65px"></asp:TextBox>
                    &nbsp;</strong>
            </td>
        </tr>
        <tr>
            <td height="18" align="right">
                &nbsp;
            </td>
            <td align="left">
                <asp:Button ID="btn_Save" runat="server" Text="提交" OnClick="btn_Save_Click" />
                <input type="button" id="btnCancel" name="btnCancel"  value="取消" />
            </td>
        </tr>
    </table>
    </form>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if ("<%=isManageGrant %>" == "False") {
                $("#btnCancel").hide();
            }
            $("#btnCancel").click(function() {
                $("#<%=txtFactJourCount.ClientID %>").val("");
                $("#<%=txtFactHotelCount.ClientID %>").val("");
                $("#<%=txtFactSightCount.ClientID %>").val("");
                $("#<%=txtFactCarCount.ClientID %>").val("");
                $("#<%=txtFactShopCount.ClientID %>").val("");
                $("#<%=txtCityCount.ClientID %>").val("");
                $("#<%=txtWorldCount.ClientID %>").val("");
                $("#<%=txtUserCount.ClientID %>").val("");
                $("#<%=txtIntemediCount.ClientID %>").val("");
            })
        })
    </script>
    
</body>
</html>
