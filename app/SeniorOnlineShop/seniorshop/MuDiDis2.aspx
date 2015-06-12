<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MuDiDis2.aspx.cs" Inherits="SeniorOnlineShop.seniorshop.MuDiDis2"
    MasterPageFile="~/master/SeniorShop.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<asp:Content ID="chpguidList" ContentPlaceHolderID="c1" runat="server">
    <div class="rightn">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="neiringht">
                    出游指南
                </td>
            </tr>
            <tr>
                <td>
                    <div class="neiringhtk">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop5" style="border: 1px solid #EAEAEA; padding: 1px;">
                            <tr>
                                <td>
                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="muhang" style="border-bottom: 1px solid #EEEEEE;">
                                        <tr>
                                            <td width="88%" class="lvsezi14">
                                                <strong>
                                                    <%=guidType%></strong>
                                            </td>
                                            <td width="12%">
                                                <a href="#" class="lvsezi"></a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:CustomRepeater runat="server" ID="rptGuides" OnItemDataBound="repeater_list_ItemDataBound">
                                        <ItemTemplate>
                                              <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0" runat="server" id="table1">
                                                <tr>
                                                    <td class="linenew">
                                                        • <a href="javascript:void(0)" onclick="<%# Eval("ID","getUrlDetail('{0}')") %>"><%#GetNewTitle(Eval("Title").ToString(), 36)%>
                                                        </a>
                                                    </td>
                                                    <td class="linenew" width="16%">
                                                        <span class="hui">【<%#Eval("IssueTime","{0:yyyy-MM-dd}") %>】</span></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </cc1:CustomRepeater>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style=" height:60px;">
                                        <tr>
                                            <td align="center" valign="middle">
     
                                                <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server"  PageStyleType="NewButton" />   
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
    function getUrlDetail(id)
    {
        window.location.href="MuDiDi_"+id+"_<%=CompanyId%>";
    }
    </script>
</asp:Content>
