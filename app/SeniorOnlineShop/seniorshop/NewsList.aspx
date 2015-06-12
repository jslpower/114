<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Title="最新动态"
    Inherits="SeniorOnlineShop.seniorshop.NewsList" MasterPageFile="~/master/SeniorShop.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<asp:Content ContentPlaceHolderID="c1" ID="c1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="neiringht">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="19%" class="shenglan">
                            最新动态
                        </td>
                        <td width="29%" class="huise">
                            关键字：
                            <input name="txtKeyWord" id="txtKeyWord" type="text" size="18" value="<%=Request.QueryString["k"] %>" />
                        </td>
                        <td width="52%">
                            <a id="linkSearch" href="#">
                                <img src="<%=ImageServerUrl %>/images/seniorshop/search.gif" border="0" /></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div class="neiringhtk">
                    <cc1:CustomRepeater ID="rptNewsList" runat="server" EmptyText="暂无最新动态" OnItemCreated="rptNewsList_ItemCreated">
                        <HeaderTemplate>
                            <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="linenew">
                                    · <a href="#" runat="server" id="linkNew"></a><span class="huise0">【<%#Eval("IssueTime","{0:yyyy-MM-dd}") %>
                                        】</span>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </cc1:CustomRepeater>
                    <div align="right" class="digg">
                        <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server"  PageStyleType="NewButton"/>
                    </div>
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
    var newsList = {
        search:function(){
            var k = $("#txtKeyWord").val();
            window.location.href="/seniorshop/NewsList.aspx?cid=<%=CompanyID %>&"+$.param({k:k});
            return false;
        }
    };
    $(function(){
        $("#linkSearch").click(function(){
            return newsList.search();
        });
        $("#txtKeyWord").keydown(function(event) {
            if (event.keyCode == 13) {
                $("#linkSearch").click();
                return false;
            }
        });
    });
    </script>

</asp:Content>
