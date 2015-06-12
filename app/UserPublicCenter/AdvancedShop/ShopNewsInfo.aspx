<%@ Page  Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ShopNewsInfo.aspx.cs" Inherits="UserPublicCenter.AdvancedShop.ShopNewsInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="AdvancedControl1.ascx" TagName="AdvancedControl1" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop10">
        <tr>
            <td align="center">
                <span class="boxbanner">
                    <asp:Image ID="ImgHead" runat="server" Width="970px" Height="167px" />
                </span>
            </td>
        </tr>
    </table>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="15" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/line2.gif) no-repeat;
                            width: 730px;">
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" >
                    <tr>
                        <td style="border: 1px solid #DFDFDF; border-top: 0px solid #ffffff;">
                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="table-layout: fixed">
                                <tr>
                                    <td height="35px" align="center" style="border-bottom: 1px solid #F1C37B; font-size: 24px;
                                        line-height: 120%; word-wrap: break-word; width: 730px;">
                                        <strong>
                                            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding: 3px;">
                                        <table width="100%" border="0" cellpadding="3" cellspacing="0" bgcolor="#FFFDF2"
                                            style="border: 1px dashed #ECECEC; table-layout: fixed">
                                            <tr>
                                                <td style="word-wrap: break-word;">
                                                    <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding: 10px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px;" align="right">
                                        <asp:Literal ID="litGoBack" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                            <a href="ShopNewsInfo.aspx"></a>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <img src="<%=ImageServerPath %>/images/UserPublicCenter/lxfs.gif" width="220" height="28" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="border-left: 1px solid #DDDDDD; border-right: 1px solid #DDDDDD;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;">
                                <tr>
                                    <td width="25%" height="25" align="left">
                                        联系人：
                                    </td>
                                    <td width="75%" align="left">
                                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                                        &nbsp; <%=Utils.GetBigImgMQ(MQ) %>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="left">
                                        电 话：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblTelPhone" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="left">
                                        传 真：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="left">
                                        地 址：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                  <tr>
                                    <td height="25" align="left" valign="middle">推广网址：
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:Label ID="lblUrl" runat="server" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <img src="<%=ImageServerPath %>/images/UserPublicCenter/lxfsbb.gif" width="220" height="10" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="margin10">
                    <tr>
                        <td align="left" class="gwright">
                            <div class="gwrhang">
                                企业新闻</div>
                            <div class="gwrnei">
                                <ul>
                                    <uc2:AdvancedControl1 ID="AdvancedControl11" runat="server" />
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
                <iframe src="http://m.weather.com.cn/m/pn12/weather.htm " width="220" height="110"
                    marginwidth="0" marginheight="0" hspace="0" vspace="0" frameborder="0" scrolling="no">
                </iframe>
            </td>
        </tr>
    </table>
</asp:Content>
