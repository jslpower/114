<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Title="关于我们" Inherits="SeniorOnlineShop.seniorshop.AboutUs" MasterPageFile="~/master/SeniorShop.Master" %>
<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<asp:Content ContentPlaceHolderID="c1" ID="c1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="neiringht">
                关于我们
            </td>
        </tr>
        <tr>
            <td>
                <div class="neiringhtk">
                    <table width="100%" border="0" cellspacing="1" cellpadding="0">
                        <tr>
                            <td width="24%" class="lianxi">
                                <strong>单位名称：</strong>
                            </td>
                            <td width="76%" class="lianxix">
                                <asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal> (许可证号：<asp:Literal ID="ltrXuKeZheng" runat="server"></asp:Literal>)
                            </td>
                        </tr>
                        <tr>
                            <td class="lianxi">
                                <strong>品牌名称：</strong>
                            </td>
                            <td class="lianxix">
                                <asp:Literal ID="ltrBrandName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="lianxiz">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="ltrAboutUs" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    
                </div>
            </td>
        </tr>
    </table>
</asp:Content>