<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShopAdvControl.ascx.cs"
    Inherits="UserPublicCenter.WebControl.ShopAdvControl" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <%@ register assembly="ControlLibrary" namespace="ControlLibrary" tagprefix="cc1" %>
    <tr>
        <td align="left" class="gwright">
            <div class="gwrhang">
               新货上架</div>
            <div class="gwrnei">
                <ul>
                    <cc1:CustomRepeater ID="rpt_NeedNewInfo" runat="server">
                        <ItemTemplate>
                            <%# GetTitleLink(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AdvId")), Convert.ToString(DataBinder.Eval(Container.DataItem, "Title")))%>
                        </ItemTemplate>
                    </cc1:CustomRepeater>
                </ul>
            </div>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
    <tr>
        <td>
            <%=ImgUrl1%>
        </td>
    </tr>
    <tr>
        <td height="10">
        </td>
    </tr>
    <tr>
        <td>
            <%=ImgUrl2%>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="maintop10">
    <tr>
        <td align="left" class="gwright">
            <div class="gwrhang">
                最新加入</div>
            <div class="gwrnei">
                <cc1:CustomRepeater ID="rpt_NewsEnjoy" runat="server">
                    <ItemTemplate>
                        <ul>
                            <%#GetShopLink(Convert.ToString(DataBinder.Eval(Container.DataItem, "Title")), Convert.ToString(DataBinder.Eval(Container.DataItem, "RedirectURL")))%>
                        </ul>
                    </ItemTemplate>
                </cc1:CustomRepeater>
            </div>
        </td>
    </tr>
</table>
