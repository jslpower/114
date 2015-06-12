<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HonoredGuestLeftAndRight.ascx.cs"
    Inherits="UserPublicCenter.SupplierInfo.UserControl.HonoredGuestLeftAndRight" %>
<%@ Register Src="~/SupplierInfo/UserControl/CommonTopicControl.ascx" TagName="CommonTopic"
    TagPrefix="uc1" %>
<%--访谈介绍开始--%>
<uc1:CommonTopic runat="server" ID="CommonTopic1" PartText="访谈介绍" PartCss="fangtanhang"
    TextCss="" />
<%--访谈介绍结束--%>
<%--顾问团队开始--%>
<uc1:CommonTopic runat="server" ID="CommonTopic2" PartText="&nbsp;&nbsp;顾问团队" PartCss="fangtanhang"
    TextCss="" TopNumber="3" />
<%--顾问团队结束--%>
<%--近期访谈回顾开始--%>
<uc1:CommonTopic runat="server" ID="CommonTopic3" PartText="&nbsp;&nbsp;近期访谈回顾" PartCss="fangtanhang"
    TextCss="" TopNumber="3" />
<%--近期访谈回顾结束--%>
<%--最新行业资讯开始--%>
<uc1:CommonTopic runat="server" ID="CommonTopic4" PartText="最新行业资讯" PartCss="fangtanhang"
    TextCss="xuetang" TopNumber="7" />
<%--最新行业资讯结束--%>