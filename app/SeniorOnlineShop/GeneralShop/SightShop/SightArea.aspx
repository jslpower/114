<%@ Page Title="" Language="C#" MasterPageFile="~/master/GeneralShop.Master" AutoEventWireup="true"
    CodeBehind="SightArea.aspx.cs" Inherits="SeniorOnlineShop.GeneralShop.SightShop.SightArea" %>

<%@ Register Src="~/GeneralShop/GeneralShopControl/SecondMenu.ascx" TagName="SecondMenu"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/master/GeneralShop.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:SecondMenu runat="server" ID="SecondMenu1" CurrMenuIndex="3" />
    <div class="main_center2">
        <div class="gscp3">
            <ul>
                <asp:Repeater runat="server" ID="rptSightArea">
                    <ItemTemplate>
                        <li>
                            <%# GetSightArea(Container.DataItem)%>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="digg" id="page" style="width: 97%;">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageStyleType="NewButton" runat="server" />
        </div>
    </div>
</asp:Content>
