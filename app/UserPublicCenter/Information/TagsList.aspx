<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TagsList.aspx.cs" Inherits="UserPublicCenter.Information.TagsList"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/InfomationControl/InfomationBanner.ascx" TagName="HeadMenu"
    TagPrefix="uc2" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRoute"
    TagPrefix="uc5" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRouteRecommend"
    TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("InformationStyle") %>" />
    <uc1:CityAndMenu ID="CityAndMenu1" HeadMenuIndex="7" runat="server" />
    <uc2:HeadMenu ID="headMenu" runat="server" />
    <div class="hr-10">
    </div>
    <div class="body">
        <div class="tags_list">
            <dl class="tbox">
                <dt><strong>最新标签</strong></dt>
                <dd>
                    <%=html %>
                </dd>
            </dl>
        </div>
        <uc3:HotRouteRecommend ID="HotRouteRecommend" runat="server" />
        <div class="fenzhan">
            <h2>
                同业114分站</h2>
            <div class="list">
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(19) %>">北京同业网</a>&nbsp;&nbsp;
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(48) %>">广州同业网</a>&nbsp;&nbsp;
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(362) %>">杭州同业网</a>&nbsp;&nbsp;
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(292) %>">上海同业网</a>&nbsp;&nbsp;
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(192) %>">南京同业网</a>&nbsp;&nbsp;
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(257) %>">济南同业网</a>&nbsp;&nbsp;
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(367) %>">宁波同业网</a>&nbsp;&nbsp;
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(352) %>">昆明同业网</a>&nbsp;&nbsp;
                <a href="<%=EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(225) %>">沈阳同业网</a>
            </div>
        </div>
    </div>
</asp:Content>
