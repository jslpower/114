<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Title="购物点"
    CodeBehind="ShoppingListPage.aspx.cs" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    Inherits="UserPublicCenter.ShoppingInfo.ShoppingListPage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Src="../WebControl/AdveControl.ascx" TagName="AdveControl" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc4" %>
<%@ Register Src="../WebControl/ShopAdvControl.ascx" TagName="ShopAdvControl" TagPrefix="uc5" %>
<asp:Content ContentPlaceHolderID="Main" ID="Default_ctMain" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />

    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td align="center">
                <asp:DataList ID="dal_PicAdvList" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="6"
                    RepeatDirection="Horizontal" Width="98%">
                    <ItemTemplate>
                        <div class="wupings">
                            <%# ShowPicAdvInfo(DataBinder.Eval(Container.DataItem, "RedirectURL").ToString(), DataBinder.Eval(Container.DataItem, "ImgPath").ToString())%>
                        </div>
                        <div class="wupingx">
                            <%# ShowTitleAdvInfo(DataBinder.Eval(Container.DataItem, "Title").ToString(), DataBinder.Eval(Container.DataItem, "RedirectURL").ToString())%>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <div class="goulefths">
                    <div style="float: left">
                        <strong>购物点列表</strong></div>
                    <span style="float: left; margin-left: 200px; font-size: 14px;"></span>
                </div>
                <div class="goulefthx">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="5%" class="hei14">
                                搜索
                            </td>
                            <td style="width: 69%">
                                <asp:TextBox ID="txt_CompanyName" runat="server" Style="line-height: 18px; height: 18px; width: 160px;
                                    color: #999; border: 1px solid #7E9DB0;"></asp:TextBox>
                                <uc4:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                            </td>
                            <td width="43%">
                             <img id="ImgSearch" src="<%=ImageServerPath %>/images/UserPublicCenter/gwss.gif"
                                    width="69" height="21" style="cursor: pointer;" />
                            </td>
                        </tr>
                    </table>
                </div>
                <table width="100%">
                    <div class="gouwucp">
                        <cc1:CustomRepeater ID="rpt_CarListInfo" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="left">
                                        <div class="gwcptu">
                                            <%#GetImageLink(DataBinder.Eval(Container.DataItem, "ID").ToString(), DataBinder.Eval(Container.DataItem, "CompanyImgThumb").ToString())%>
                                        </div>
                                        <div class="gouwunei">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="huise">
                                                <tr>
                                                    <td width="49%">
                                                        <span class="lan14"><strong><a title="<%#DataBinder.Eval(Container.DataItem, "CompanyName").ToString()%>"
                                                            href="<%#GoToUrl %>" target="_blank">
                                                            <%#GetContentRemark(DataBinder.Eval(Container.DataItem, "CompanyName").ToString(),10)%></a></strong></span>
                                                    </td>
                                                    <td width="51%">
                                                        地区：<strong><%#GetProCityName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CityId").ToString()))%></strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        主营产品：<%#GetContentRemark(Convert.ToString(DataBinder.Eval(Container.DataItem, "ShortRemark")),10)%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <%#GetContentInfo(DataBinder.Eval(Container.DataItem, "ContactInfo"))%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        介绍：<%#GetContentRemark(DataBinder.Eval(Container.DataItem, "Remark").ToString(),12)%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                    </div>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <div class="digg">
                                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageStyleType="NewButton" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <uc5:ShopAdvControl ID="ShopAdvControl1" runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        $(function() {
            $("#ImgSearch").click(function() {
            var params = { CompanyName: encodeURIComponent($("#<%=txt_CompanyName.ClientID %>").val()), ProvinceID: $("#ctl00_Main_ProvinceAndCityList1_ddl_ProvinceList").val(), S_CityID: $("#ctl00_Main_ProvinceAndCityList1_ddl_CityList").val(), CityId: "<%=CityId %>" };
                var str = $.param(params);
                window.location.href = "ShoppingListPage.aspx?" + str;
            });
            $("#<%=txt_CompanyName.ClientID %>").keydown(function(event) {
                if (event.keyCode == 13) {
                    $("#ImgSearch").click();
                    return false;
                }
            });
        });
    </script>
</asp:Content>
