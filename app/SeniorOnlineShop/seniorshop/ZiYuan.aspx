<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZiYuan.aspx.cs" Inherits="SeniorOnlineShop.seniorshop.ZiYuan" MasterPageFile="~/master/SeniorShop.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<asp:Content ContentPlaceHolderID="c1" ID="c1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="neiringht">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="19%" class="shenglan">
                            旅游资源推荐
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
                    <asp:DataList ID="rptZiYuanList" runat="server" OnItemCreated="rptZiYuanList_ItemCreated" RepeatColumns="5">
                    <HeaderTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px dashed #ccc">
                        <tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <td width="33%">
                            <table width="125" border="0" cellspacing="0" cellpadding="0" style="margin: 8px 4px 8px 4px;" id="table1">
                                <tr>
                                    <td>
                                        <a href="#" id="linkZiYuan" runat="server">
                                            </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <a href="#" id="linkZiYuan1" runat="server"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </ItemTemplate>
                    <FooterTemplate>                    
                    </table>
                    </FooterTemplate>
          </asp:DataList>
                    <div id="resoNoMessage" runat="server" style="text-align:center;margin-top:75px;margin-bottom:75px;">暂无旅游资源推荐信息！</div>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" style="height:60px;">
                        <tr>
                            <td align="center" valign="middle">
                                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server"  PageStyleType="NewButton" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
    var newsList = {
        search:function(){
            var k = $("#txtKeyWord").val();
            var cid="<%=CompanyId %>";
            window.location.href="/seniorshop/ZiYuan.aspx?"+$.param({k:k,cid:cid});
            return false;
        }
    };
    $(function(){
        $("#linkSearch").click(function(){
            return newsList.search();
        });
            $("#txtKeyWord").bind("keypress", function(e) {
            if (e.keyCode == 13) {
            $("#linkSearch").click(); 
            return false;
            }
        });
    });
    </script>
</asp:Content>