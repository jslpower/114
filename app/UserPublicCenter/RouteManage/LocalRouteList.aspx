<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalRouteList.aspx.cs"
    Inherits="UserPublicCenter.RouteManage.LocalRouteList" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="uc1" %>
<asp:Content ID="LocalRouteList" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu" runat="server" HeadMenuIndex="2" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="15" style="background: url(<%=ImageServerPath%>/images/UserPublicCenter/line2.gif) no-repeat;
                            width: 730px;">
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="border: 1px solid #DFDFDF; border-top: 0px solid #ffffff;">
                            <table width="98%" border="0" align="center" cellpadding="5" cellspacing="0">
                                <tr>
                                    <td width="19%" rowspan="2" align="center" valign="top">
                                        <img src="<%=ImageServerPath%>/images/UserPublicCenter/gouwutu1.gif" id="imgLogo"
                                            runat="server" width="150" height="90" />
                                    </td>
                                    <td width="51%" align="left" valign="top" style="border-bottom: 1px dashed #cccccc;">
                                        <h1>
                                            <asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal></h1>
                                    </td>
                                    <td valign="top" style="border-bottom: 1px dashed #cccccc;">
                                        许可证号：<asp:Literal ID="ltrLicense" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" style="height: 70px;" colspan="2">
                                        <asp:Literal ID="ltrRemark1" runat="server"></asp:Literal><asp:Literal ID="ltrRemark"
                                            runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="margin10" style="border-bottom: 2px solid #FF5500;">
                                <tr>
                                    <td width="67%">
                                        <div class="xianluon">
                                            <strong><a href="javascript:void(0);">所有线路</a></strong></div>
                                    </td>
                                    <td width="33%">
                                        <%--<a href="#" class="dianright">下一页</a>
                                        <div class="dianlefth">
                                            上一页</div>
                                        <div style="float: right; margin-right: 10px; font-size: 14px;">
                                            1/100
                                        </div>--%>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="xianluhangcx"
                                style="line-height: 10px; padding: 0px; border: 1px solid #ccc; border-bottom: 0px;"
                                height="10">
                                <tr>
                                    <td width="67%" align="left" style="padding-left: 65px;">
                                        <strong>线路基本信息</strong>
                                    </td>
                                    <td width="20%" align="center">
                                        <strong>天数</strong>
                                    </td>
                                    <td width="13%" align="center">
                                        <strong>MQ洽谈</strong>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #D8D8D8;
                                text-align: left;">
                                <asp:Repeater ID="rptRouteList" runat="server">
                                    <ItemTemplate>
                                        <tr bgcolor="#FFFFFF">
                                            <td width="489" height="35" style="border-bottom: 1px dashed #ccc; padding-top: 5px;">
                                                <strong>
                                                    <img src="<%=ImageServerPath%>/images/UserPublicCenter/ico.gif" width="11" height="11" /><a
                                                        href="<%=strDomain %>/RouteAgency/RouteManage/RoutePrint.aspx?RouteID=<%# DataBinder.Eval(Container.DataItem,"ID") %>"
                                                        target="_blank" class="lan14"><%# DataBinder.Eval(Container.DataItem,"RouteName") %></a></strong>
                                            </td>
                                            <td width="150" align="center" style="border-bottom: 1px dashed #ccc; line-height: 18px;">
                                                <%# DataBinder.Eval(Container.DataItem,"TourDays") %>天
                                            </td>
                                            <td width="94" style="border-bottom: 1px dashed #ccc; line-height: 14px;">
                                                <div style="width: 65px; text-align: center; padding-top: 3px;">
                                                    <%# EyouSoft.Common.Utils.GetMQ(DataBinder.Eval(Container.DataItem,"ContactMQID").ToString()) %></div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Panel ID="pnlNoData" runat="server" Visible="false">
                                    <tr bgcolor="#FFFFFF">
                                        <td colspan="3" align="center" height="55" style="border-bottom: 1px dashed #ccc;
                                            padding-top: 5px;">
                                            暂无线路信息!
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
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
                            <img src="<%=ImageServerPath%>/images/UserPublicCenter/lxfs2.gif" width="220" height="28" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="border-left: 1px solid #DDDDDD; border-right: 1px solid #DDDDDD;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;">
                                <tr>
                                    <td height="25" align="right" style="width:60px;" valign="top">
                                        单位名称：
                                    </td>
                                    <td align="left" valign="top"><asp:Literal ID="ltrCompanyName1" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="right" valign="top">
                                        许可证号：
                                    </td>
                                    <td align="left" valign="top"><asp:Literal ID="ltrLicense1" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="right" valign="top">
                                        品牌名称：
                                    </td>
                                    <td align="left" valign="top"><asp:Literal ID="ltrBrandName" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="right" valign="top">
                                        联系人：
                                    </td>
                                    <td align="left" valign="top"><asp:Literal ID="ltrContactName" runat="server"></asp:Literal>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="right" valign="top">
                                        手机：
                                    </td>
                                    <td align="left" valign="top"><asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="right" valign="top">
                                        电 话：
                                    </td>
                                    <td align="left" valign="top"><asp:Literal ID="ltrTel" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="right" valign="top">
                                        传 真：
                                    </td>
                                    <td align="left" valign="top"><asp:Literal ID="ltrFax" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="right" valign="top">
                                        地 址：
                                    </td>
                                    <td align="left" valign="top"><asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <img src="<%=ImageServerPath%>/images/UserPublicCenter/lxfsbb.gif" width="220" height="10" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div class="digg">
                    <uc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton" />
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#spanRemark1").hide();
            $("#spanRemark,#spanRemark1").click(function() {
                if ($("#spanRemark").is(":visible")) {
                    $("#spanRemark").hide();
                    $("#spanRemark1").show();
                } else {
                    $("#spanRemark").show();
                    $("#spanRemark1").hide();
                }
            });
        });
    </script>

</asp:Content>
