<%@ Page Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="MQNewsDetail.aspx.cs" Inherits="UserPublicCenter.Information.MQNewsDetail"
    Title="" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/InfomationControl/InfomationBanner.ascx" TagName="HeadMenu"
    TagPrefix="uc2" %>
<%@ Register Src="../WebControl/InfomationControl/AllCountryTourInfo.ascx" TagName="AllCountryMenu"
    TagPrefix="uc7" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRoute"
    TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("InformationStyle") %>" />
    <uc1:CityAndMenu ID="CityAndMenu1" HeadMenuIndex="7" runat="server" />
    <uc2:HeadMenu ID="headMenu" runat="server" />
    <div style="border: solid 1px #C6D8E0; width: 968px; margin: 0 auto; margin-bottom: 7px;
        margin-top: 10px;">
        <div style="text-align: left; margin-top: 5px; margin-left: 5px;">
            <p style="margin: 0;">
                <span>同业中心</span> <span>&gt;</span> <span>&nbsp;</span> <%=Class %>
            </p>
        </div>
        <div style="text-align: center; border-bottom: 1px dashed #999999;">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
            <p>
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></p>
        </div>
        <div id="content" runat="server" style="font-size: 14px; line-height: 25px; text-indent: 25px;
            text-align: left; padding: 20px 10px;">
        </div>
    </div>
    <uc5:HotRoute ID="hotRoute" runat="server" />
</asp:Content>
