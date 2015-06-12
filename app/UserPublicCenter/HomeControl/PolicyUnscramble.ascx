<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PolicyUnscramble.ascx.cs"
    Inherits="UserPublicCenter.HomeControl.PolicyUnscramble" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<div class="newslist">
    <ul>
        <cc1:CustomRepeater ID="repPolicyUnscramble" runat="server">
            <ItemTemplate>
                <li>
                    <img src="<%=ImageServerPath %>/Images/UserPublicCenter/newsdot.gif" />
                    <a href="/SupplierInfo/ArticleInfo.aspx?id=<%#Eval("ID") %>&CityId=<%=CityId %>" target="_blank">
                        <%# EyouSoft.Common.Utils.GetText(Eval("ArticleTitle").ToString(), 18)%></a>
                </li>
            </ItemTemplate>
        </cc1:CustomRepeater>
    </ul>
</div>
