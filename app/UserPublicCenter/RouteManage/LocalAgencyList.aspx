<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalAgencyList.aspx.cs"
    Inherits="UserPublicCenter.RouteManage.LocalAgencyList" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="uc1" %>
<%@ Register Src="~/WebControl/RouteRightControl.ascx" TagName="RouteRightControl"
    TagPrefix="uc1" %>
<asp:Content ID="LocalAgencyList" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu" runat="server" HeadMenuIndex="2" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="zxlistk">
                    <tr>
                        <td class="zxlisth">
                            <strong>目的地地接社</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="dijieshecheng">
                                <ul>
                                    <%=strCityList %>
                                </ul>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td align="left" style="padding: 7px 0px 0px 10px;">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="3%" align="right">
                                                    <span class="lv" style="padding-top: 10px;">
                                                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/chaxunbiao4.gif" /></span>
                                                </td>
                                                <td width="57%" align="left">
                                                    单位名称：
                                                    <input type="text" name="txtCompanyName" id="txtCompanyName" runat="server" style="width: 160px; height: 18px; border: 1px solid #ccc; vertical-align:middle; line-height:18px;" />
                                                    联系人：
                                                    <input type="text" name="txtContactName" id="txtContactName" runat="server" style="width: 100px; height: 18px; border: 1px solid #ccc; vertical-align:middle; line-height:18px;" />
                                                </td>
                                                <td width="40%" align="left">
                                                    <a href="#">
                                                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/chaxunannui.gif" onclick="SearchData();" style="cursor:pointer;" width="60"
                                                            height="21" border="0" /></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Repeater ID="rptLocalAgencyList" runat="server" OnItemDataBound="rptLocalAgencyList_ItemDataBound">
                                <ItemTemplate>
                                    <div class="gouwucp">
                                        <div class="gwcptu">
                                            <a href="<%# EyouSoft.Common.URLREWRITE.Tour.GetLocalAgencyUrl(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>" target="_blank">
                                                <img width="92" height="84" border="0" id="imgLogo" runat="server" /></a></div>
                                        <div class="gouwunei">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="huise">
                                                <tr>
                                                    <td width="49%" colspan="2">
                                                        <span class="lan14"><strong><a href="<%# EyouSoft.Common.URLREWRITE.Tour.GetLocalAgencyUrl(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>" target="_blank">
                                                            <%# DataBinder.Eval(Container.DataItem,"CompanyName") %></a></strong></span>
                                                    </td>
                                                    <td width="51%">
                                                        地区：<strong><asp:Label ID="lblCityName" runat="server"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        企业介绍：<%# DataBinder.Eval(Container.DataItem, "ShortRemark")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <asp:Literal ID="ltrContactInfo" runat="server"></asp:Literal>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="gouwucp"></div>
                            <asp:Panel ID="pnlNoData" runat="server" Visible="false">
                                <div align="center" height="50px">
                                    暂时没有您要查找的地接社！</div>
                            </asp:Panel>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <div class="digg">
                                            <uc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <uc1:RouteRightControl ID="RouteRightControl" runat="server" />
            </td>
        </tr>
    </table>
     <%--<link href="<%=CssManage.GetCssFilePath("GreenButton") %>" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
    
    function SearchData()
    {
        var CompanyName = $("#<%=txtCompanyName.ClientID %>").val();
        var ContactName = $("#<%=txtContactName.ClientID %>").val();
        var url = '<%= strURL %>';
        window.location.href = url + "&CompanyName="+ encodeURIComponent(CompanyName) +"&ContactName="+ encodeURIComponent(ContactName) +"&rnd="+ Math.random();
    }
    $("input[type=text]").bind("keypress",function(e){
        if(document.all)e=event;
        if(e.keyCode == 13)
        {
            SearchData();
            return false;
        }
    });
    </script>
</asp:Content>
