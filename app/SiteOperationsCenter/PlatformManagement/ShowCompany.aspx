<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCompany.aspx.cs" Inherits="SiteOperationsCenter.PlatformManagement.ShowCompany" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="<%=CssManage.GetCssFilePath("acss") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td bgcolor="E1E6F1" height="3">
            </td>
        </tr>
        <tr>
            <td bgcolor="E1E6F1" height="3">
            </td>
        </tr>
        <tr>
            <td class="font12_bk">
                <asp:Repeater ID="CompanyList" runat="server">
                    <HeaderTemplate>
                        <table width="96%" border="0" align="center" cellpadding="2" cellspacing="1" class="tab_luand">
                            <tr class="tab_luan">
                                <td colspan="5">
                                    成员
                                </td>
                            </tr>
                            <tr class="lr_hangbg">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# ShowCompanyName(Container.ItemIndex+1,  DataBinder.Eval(Container.DataItem, "CompanyName").ToString())%>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tr> </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="NoData" runat="server" Visible="False">
                    <table width="96%" border="0" align="center" cellpadding="2" cellspacing="1" class="tab_luand">
                        <tr class="lr_hangbg">
                            <td align="center" style="height: 100px">
                                暂无公司列表
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td bgcolor="E1E6F1" class="font12_bk">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td bgcolor="E1E6F1" class="font12_bk">
                <table width="30%" height="20" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr align="center">
                        <td>
                            <input name="Submit22" onclick="window.close();" type="submit" class="renyuan_an" value="关　闭" />
                        </td>
                    </tr>
                </table>
            </td>
    </table>
    </form>
</body>
</html>
