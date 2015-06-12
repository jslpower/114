<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StartAndEndDate.ascx.cs" Inherits="SiteOperationsCenter.usercontrol.StartAndEndDate" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<asp:Label ID="lblTitle" runat="server"></asp:Label><input type="text"  style="width:80px;" onfocus="WdatePicker()" id="dpkStart" name="dpkStart" runat="server" size="10"/>至<input type="text" style="width:80px;" onfocus="WdatePicker()" id="dpkEnd" name="dpkEnd" runat="server" size="10"/>      
<script type="text/javascript" language="javascript">
var <%=this.ClientID %>;
<%=this.ClientID %>=
{
    GetStartDate:function()
    {
        return $("#<%=dpkStart.ClientID%>").val();
    },
    GetEndDate:function()
    {
        return $("#<%=dpkEnd.ClientID %>").val();
    },
    ClearText:function()
    {
        $("#<%=dpkStart.ClientID%>").val("");
        $("#<%=dpkEnd.ClientID %>").val("");
    }
}
</script>
