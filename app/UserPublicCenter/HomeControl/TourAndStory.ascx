<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourAndStory.ascx.cs"
    Inherits="UserPublicCenter.HomeControl.TourAndStory" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<div class="newslist">
    <ul>
        <cc1:CustomRepeater ID="repTourAndStory" runat="server">
            <ItemTemplate>
                <li>
                    <img src="<%=ImageServerPath %>/Images/UserPublicCenter/newsdot.gif"  />
                    <a href="/SupplierInfo/ArticleInfo.aspx?id=<%#Eval("ID") %>&CityId=<%=CityId %>" target="_blank">
                        <%# EyouSoft.Common.Utils.GetText(Eval("ArticleTitle").ToString(), IsGetStory ? 18 : 22)%></a></li>
            </ItemTemplate>
        </cc1:CustomRepeater>
    </ul>
</div>
