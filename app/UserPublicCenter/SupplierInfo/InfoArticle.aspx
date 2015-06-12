<%@ Page Language="C#" AutoEventWireup="true" Title="行业资讯_供求信息" CodeBehind="InfoArticle.aspx.cs"
    MasterPageFile="~/SupplierInfo/Supplier.Master" Inherits="UserPublicCenter.SupplierInfo.InfoArticle" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/SWFControl.ascx" TagName="Swf" TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/NewsControl.ascx" TagName="News" TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/CommonTopicControl.ascx" TagName="CommonTopic"
    TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/PopularityCompanyAdv.ascx" TagName="PopularityCompanyAdv"
    TagPrefix="uc4" %>
<asp:Content ContentPlaceHolderID="SupplierMain" ID="Test" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("gongqiu") %>" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="280">
                <%--最新行业资讯开始--%>
                <uc1:CommonTopic runat="server" ID="CommonTopic1" PartText="最新行业资讯" PartCss="hangleft"
                    TextCss="hanglk" TopNumber="7" />
                <%--最新行业资讯结束--%>
                
                <%--本周人气企业广告开始--%>
                <uc4:PopularityCompanyAdv runat="server" ID="PopularityCompanyAdv1" />
                <%--本周人气企业广告结束--%>
                
                <%--同业之星访谈开始--%>
                <uc1:CommonTopic runat="server" ID="CommonTopic2" PartText="同业之星访谈" PartCss="hangleft"
                    TextCss="hanglk" TopNumber="1" />
                <%--同业之星访谈结束--%>
                <div class="maintop10">
                </div>
                <%--同业学堂开始--%>
                <uc1:CommonTopic runat="server" ID="CommonTopic3" PartText="<a style='color:#333333;' href='/SupplierInfo/SchoolIntroduction.aspx' target='_blank'>同业学堂</a>" PartCss="hangleft"
                    TextCss="hanglk" TopNumber="12" />
                <%--同业学堂结束--%>
                
                <%--图片新闻开始--%>
                <uc1:CommonTopic runat="server" ID="CommonTopic4" PartText="图片新闻" PartCss="hangleft"
                    TextCss="hanglk" TopNumber="3" />
                <%--图片新闻结束--%>
            </td>
            <td width="10">
            </td>
            <td valign="top" width="680">
                <table width="680" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="420" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="magin5">
                                <tr>
                                    <td>
                                        <uc1:Swf runat="server" SwfHeight="170" SwfType="1" SwfWidth="420" IsShowTitle="true"
                                            TopNumber="5" ID="swf1" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10"
                                style="border: 1px solid #AAC6DB; padding: 1px;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="zxzbg">
                                            <tr>
                                                <td>
                                                    <asp:Label style="font-size: 16px;" runat="server" ID="lbNews" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 12px; color: #666; text-align: left;">
                                                    <asp:Label runat="server" ID="lbNewsContent"></asp:Label>
                                                    <asp:Literal runat="server" ID="ltnews"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0" id="tbNews">
                                                        <tr>
                                                            <td width="88%" height="10">
                                                            </td>
                                                            <td width="12%">
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater runat="server" ID="rpNews">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td width="80%" align="left" height="27">
                                                                        ·<a  title="<%# Eval("ArticleTitle") %>" style="font-size:14px;" href='<%# String.Format("/SupplierInfo/ArticleInfo.aspx?Id={0}",Eval("ID")) %>'><%# Utils.GetText(Eval("ArticleTitle").ToString(), 22)%></a>
                                                                    </td>
                                                                    <td width="20%" style="font-size:14px;" class="huise">
                                                                        <%# Eval("IssueTime","{0:MM-dd}")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="xtneikuang"
                                style="margin-top: 10px;">
                                <tr>
                                    <td width="84%" class="xtneihang">
                                        <strong>行业资讯</strong>
                                    </td>
                                    <td width="16%" class="xtneihang" style="font-size: 12px;">
                                        <a href="/SupplierInfo/ArticleList.aspx?Typeid=<%= (int)EyouSoft.Model.CommunityStructure.TopicClass.行业资讯 %>&AreaId=<%= (int)EyouSoft.Model.CommunityStructure.TopicAreas.行业动态 %>">更多&gt;&gt;</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="font-size: 16px; color: #285B92; padding-top: 10px;">
                                                    <asp:Label Font-Bold="true" runat="server" ID="lbInfo"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 12px; color: #666; text-align: left;">
                                                    <asp:Label runat="server" ID="lbinfoContent"></asp:Label>
                                                    <asp:Literal runat="server" ID="ltinfo"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="96%" border="0" align="center" id="TbInfo" cellpadding="0" cellspacing="0" style="border-top: 1px dashed #666;">
                                                        <tr>
                                                            <td width="88%" height="10">
                                                            </td>
                                                            <td width="12%">
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater runat="server" ID="rpInfo">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td width="70%" align="left" height="27">
                                                                        ·<a href="<%# String.Format("/SupplierInfo/ArticleInfo.aspx?Id={0}",Eval("ID")) %>" title="<%# Eval("ArticleTitle") %>" style="font-size:14px;"><%# Utils.GetText(Eval("ArticleTitle").ToString(),22)%></a>
                                                                    </td>
                                                                    <td width="30%" style="font-size:14px;">
                                                                        <%# Eval("IssueTime","{0:MM-dd}")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10">
                        </td>
                        <td width="250" valign="top">
                            <%--景点新闻开始--%>
                            <uc1:News runat="server" ID="Viewpoint" IsMarginTop="false" ContentCss="xtneibiao1" IsShowMore="true" />
                            <%--景点新闻结束--%>
                            
                            <%--旅行社开始--%>
                            <uc1:News runat="server" ID="Travel"  ContentCss="xtneibiao1" IsShowMore="true" />
                            <%--旅行社结束--%>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="maintop10">
                                <tr>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrAdv"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                            <%--酒店新闻开始--%>
                            <uc1:News runat="server"  ContentCss="xtneibiao1" ID="Hotel" IsShowMore="true" />
                            <%--酒店新闻结束--%>
                            
                            <%--政策解读开始--%>
                            <uc1:News runat="server"  ContentCss="xtneibiao1" ID="Policy" IsShowMore="true" />
                            <%--政策解读结束--%>
                            <%--成功故事开始--%>
                            <uc1:News runat="server"  ContentCss="xtneibiao1" ID="Success" IsShowMore="true" />
                            <%--成功故事结束--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(function(){
            if($.trim($("#<%= lbNews.ClientID %>").html())!="")
            {
                $("#tbNews").attr("style","border-top: 1px dashed #666;")
            }
            if($.trim($("#<%= lbInfo.ClientID %>").html())=="")
            {
                $("#TbInfo").removeAttr("style");
            }
        })
    </script>
</asp:Content>
