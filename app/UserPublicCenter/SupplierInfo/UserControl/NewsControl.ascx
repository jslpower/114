<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsControl.ascx.cs" Inherits="UserPublicCenter.SupplierInfo.UserControl.NewsControl" %>
<div class="<%= _contentcss %>">
    <table class="xtneikuang" width="100%" border="0" cellpadding="0" cellspacing="0" runat="server" id="MainTB">
          <tbody>
          <tr>
            <td class="xtneihang" width="74%"><strong><%= ToppicTag %></strong></td>
            <td class="xtneihang" style="font-size: 12px;" width="26%"><asp:Label runat="server" ID="lbMore"></asp:Label></td>
          </tr>
          <tr>
            <td colspan="2" class="xutanglist">
            <asp:Repeater runat="server" ID="rpMain">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>·<a title="<%# Eval("ArticleTitle") %>" href='<%# String.Format("/SupplierInfo/{0}?Id={1}",Eval("TopicClassId").ToString()=="行业资讯"?"ArticleInfo.aspx":"SchoolIntroductionInfo.aspx",Eval("ID")) %>'><%# EyouSoft.Common.Utils.GetText(Eval("ArticleTitle").ToString(),16)%></a></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </td>
          </tr>
        </tbody>
    </table>
</div>