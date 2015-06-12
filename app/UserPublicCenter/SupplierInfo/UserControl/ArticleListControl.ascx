<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleListControl.ascx.cs"
    Inherits="UserPublicCenter.SupplierInfo.ArticleListControl" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;">
    <tr>
        <td valign="top">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="gqline">
                        &nbsp;<asp:Literal runat="server" ID="ltrTopicClass"></asp:Literal><asp:Literal runat="server"
                            ID="ltrTopIcArea"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="gqline">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin: 15px 0 15px 0">
                            <asp:Repeater runat="server" ID="rptArticleList">
                                <ItemTemplate>
                                    <tr>
                                        <td width="88%" class="gqlist">
                                            <a href='<%# String.Format("/SupplierInfo/{0}?Id={1}",Eval("TopicClassId").ToString()=="行业资讯"?"ArticleInfo.aspx":"SchoolIntroductionInfo.aspx",Eval("ID")) %>'>
                                                <%# Utils.GetText(Eval("ArticleTitle").ToString(), 34, true)%></a>
                                        </td>
                                        <td width="12%">
                                            <%# Eval("IssueTime","{0:MM/dd hh:mm}")%>
                                        </td>
                                    </tr>
                                    <%# Container.ItemIndex != 0 && (Container.ItemIndex + 1) != intPageSize && (Container.ItemIndex + 1) % 6 == 0 ? "</table></td></tr><tr><td class=\"gqline\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin: 15px 0 15px 0\">" : ""%>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="gqline">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <div class="digg" align="right">
                                        <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
