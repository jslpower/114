<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopularityCompanyAdv.ascx.cs"
    Inherits="UserPublicCenter.SupplierInfo.UserControl.PopularityCompanyAdv" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="margin10">
    <tr>
        <td class="hangleft">
            <strong>本周最具人气企业推荐</strong>
        </td>
    </tr>
    <tr>
        <td class="hanglk">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <asp:Repeater runat="server" ID="rptAdvList">
                        <ItemTemplate>
                            <td <%# Container.ItemIndex == 0 ? "width='33%'" : (Container.ItemIndex == 1 ? "width='34%'" : "") %>
                                align="center">
                                <div class="jings">
                                    <%# ShowPicAdvInfo(DataBinder.Eval(Container.DataItem, "RedirectURL").ToString(), DataBinder.Eval(Container.DataItem, "ImgPath").ToString())%>
                                </div>
                                <div class="jingx">
                                    <%# ShowTitleAdvInfo(DataBinder.Eval(Container.DataItem, "Title").ToString(), DataBinder.Eval(Container.DataItem, "RedirectURL").ToString())%>
                                </div>
                            </td>
                            <%# Container.ItemIndex != 0 && (Container.ItemIndex + 1) % 2 == 0 ? "</tr><tr>" : "" %>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </table>
        </td>
    </tr>
</table>
