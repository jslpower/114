<%@ Page Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="TravelDetails.aspx.cs" Inherits="UserPublicCenter.TravelManage.TravelDetails" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/GeneralShopControl.ascx" TagName="GeneralShopControl"
    TagPrefix="uc2" %>
<%@ Register Src="TravelRightControl.ascx" TagName="TravelRightControl" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                 <uc2:GeneralShopControl ID="GeneralShopControl1" runat="server" />
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="gwright">
                            <div class="gwrhang">
                                旅游用品新货上架</div>
                            <div class="gwrnei">
                                <ul>
                                    <asp:Repeater ID="rptNewTravel" runat="server">
                                        <ItemTemplate>
                                             <li><a  target="_blank"  href='<%#Utils.GetWordAdvLinkUrl(1,Convert.ToInt32(Eval("advId")),-1,this.CityId)%>'> <%#Utils.GetText(Eval("Title").ToString(),18)%>
                                                </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                    <tr>
                        <td>
                            <uc3:TravelRightControl ID="TravelRightControl1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
