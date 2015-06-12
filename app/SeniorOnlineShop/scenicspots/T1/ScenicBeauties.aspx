<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicBeauties.aspx.cs"
    Inherits="SeniorOnlineShop.scenicspots.T1.ScenicBbeauties" MasterPageFile="/master/ScenicSpotsT1.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/scenicspots/usercontrol/ChildMenu.ascx" TagName="ChildMenu" TagPrefix="uc1" %>
<asp:Content runat="server" ID="HeadPlaceHolder" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content runat="server" ID="MainPlaceHolder" ContentPlaceHolderID="MainPlaceHolder">
    <div class="sidebar02 sidebar02Scenic">
        <uc1:ChildMenu ID="ChildMenu1" runat="server" />
        <div class="content">
            <div runat="server" id="NoData" visible="false" style="text-align: center;">
                该景区还未添加图片！
            </div>
            <ul class="ScenicSpotPic">
                <asp:Repeater runat="server" ID="rptData">
                    <ItemTemplate>
                        <li>
                            <div style="height: 150px;">
                                <a title="<%#Eval("Description")%>" href="/scenicspots_<%#Eval("ImgId") %>_<%# Eval("CompanyId") %>">
                                    <img style="width: 201px; height: 146px;" src="<%#Eval("ThumbAddress") %>" /></a>
                            </div>
                            <div style="line-height: 20px;">
                                <%--<a title="<%#Eval("ScenicName")%>" href="/ScenicTickets_<%# Eval("ScenicId") %>_<%# Eval("CompanyId") %>">
                                    <%#Utils.GetText2(Eval("ScenicName") == null ? null : Eval("ScenicName").ToString(), 8, true)%></a> &nbsp; --%>
                                <a title="<%#Eval("Description")%>" href="/scenicspots_<%#Eval("ImgId") %>_<%# Eval("CompanyId") %>">
                                        <%#Utils.GetText2(Eval("Description") == null ? null : Eval("Description").ToString(), 8, true)%></a></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clearboth">
                </div>
            </ul>
        </div>
        <div class="digg" id="page" style="width: 97%;">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageStyleType="NewButton" runat="server" />
        </div>
        <div>
            &nbsp;
        </div>
    </div>
</asp:Content>
