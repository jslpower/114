<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleInfoControl.ascx.cs"
    Inherits="UserPublicCenter.SupplierInfo.UserControl.ArticleInfoControl" %>
<%@ Import Namespace="EyouSoft.Common" %>
<table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-right: 10px;">
    <tr>
        <td valign="top" style="border: 1px solid #C4C4C4; padding: 1px;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding: 10px;
                background: url(<%= Domain.ServerComponents %>/images/UserPublicCenter/bg_new.gif) repeat-x;">
                <tr>
                    <td class="huise1" align="left">
                        &nbsp;<asp:Literal runat="server" ID="ltrTopicClass"></asp:Literal><asp:Literal runat="server"
                            ID="ltrTopIcArea"></asp:Literal>
                        > 正文
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <h1>
                            <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></h1>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="center" style="border-bottom: 1px dashed #ccc;" class="huise">
                                    <asp:Literal runat="server" ID="ltrTime"></asp:Literal>
                                    &nbsp;&nbsp;
                                    <asp:Literal runat="server" ID="ltrSource"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="heise" style="padding-top: 15px;">
                        <div style="text-align: center;display:none;">
                            <asp:Literal runat="server" ID="ltrImg"></asp:Literal>
                        </div>
                        <br />
                        <div style="margin-left:5px;margin-right:5px;"><asp:Literal runat="server" ID="ltrInfo"></asp:Literal></div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px dashed #ccc;
                padding: 3px; text-align: left;">
                <tr>
                    <td width="81%">
                        Tags：<strong><asp:Literal runat="server" ID="ltrTags"></asp:Literal></strong>
                    </td>
                    <td width="19%">
                        责任编辑：<asp:Literal runat="server" ID="ltrEditOR"></asp:Literal>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-top: 1px solid #ccc;
                border-bottom: 1px solid #ccc; padding: 3px; text-align: left; margin: 10px 0 10px 0;">
                <tr>
                    <td width="51%" align="center">
                        <asp:Literal runat="server" ID="ltrPrev"></asp:Literal>
                    </td>
                    <td width="49%" align="center">
                        <asp:Literal runat="server" ID="ltrNext"></asp:Literal>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;
                padding: 1px; margin-bottom: 15px;">
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="background: #dedede; text-align: left; padding: 3px;">
                                    <strong>最新图文资讯</strong>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <asp:Repeater runat="server" ID="rptArticle">
                                    <ItemTemplate>
                                        <td>
                                            <a href='<%# String.Format("/SupplierInfo/{0}?Id={1}",Eval("TopicClassId").ToString()=="行业资讯"?"ArticleInfo.aspx":"SchoolIntroductionInfo.aspx",Eval("ID")) %>'>
                                                <img src="<%# Domain.FileSystem + Eval("ImgThumb").ToString() %>" width="120" height="100"
                                                    border="0" style="border: 1px solid #ccc; padding: 1px;" /></a><br />
                                            <a href="/SupplierInfo/SchoolIntroductionInfo.aspx?Id=<%# Eval("ID") %>" class="heise1">
                                                <%# Utils.GetText(Eval("ArticleTitle").ToString(), 10) %></a>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;
                padding: 1px;">
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="background: #dedede; text-align: left; padding: 3px;">
                                    相关文章列表
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="50%" class="xuetang2">
                                    <ul>
                                        <asp:Repeater runat="server" ID="rptRelatedArticle">
                                            <ItemTemplate>
                                                <li>·<a href='<%# String.Format("/SupplierInfo/{0}?Id={1}",Eval("TopicClassId").ToString()=="行业资讯"?"ArticleInfo.aspx":"SchoolIntroductionInfo.aspx",Eval("ID")) %>'><%# Utils.GetText(Eval("ArticleTitle").ToString(), 20) %></a></li>
                                                <%# Container.ItemIndex != 0 && Container.ItemIndex != 7 && (Container.ItemIndex + 1) % 4 == 0 ? "</ul></td><td width=\"50%\" class=\"xuetang2\"><ul>" : ""%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
