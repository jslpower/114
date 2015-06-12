<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxByRouteAgencyList.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.AjaxByRouteAgencyList" %>

<asp:repeater id="CompanyList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#D5EDFB">
                <tr align="left">
        </HeaderTemplate>
        <ItemTemplate>
            <%# ShowCompanyName(Container.ItemIndex + 1, DataBinder.Eval(Container.DataItem, "Id").ToString(), DataBinder.Eval(Container.DataItem, "CompanyName").ToString())%>
        </ItemTemplate>
        <FooterTemplate>
            </tr> </table>
        </FooterTemplate>
 </asp:repeater>

<script type="text/javascript">
 var inputVal="<%=RecordCount %>";
</script>

