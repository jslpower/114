<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropSiteAndCityList.ascx.cs"
    Inherits="TourUnion.WEB.IM.TourAgency.TourManger.DropSiteAndCityList" %>
<asp:DropDownList ID="dropProvince" runat="server" Width="50px">
</asp:DropDownList>
<asp:DropDownList ID="dropCity" runat="server" Width="80px">
</asp:DropDownList>
<script type="text/javascript">
function SelectedDrop(IsProvince,Id)
{
    var obj=null;
    if(IsProvince=="1")
        obj = document.getElementById("DropSiteAndCityList1_dropProvince");
     else
        obj = document.getElementById("DropSiteAndCityList1_dropCity");
    if(obj!=null)
    {
        for(var index=0; index<obj.length; index++)
        {
            if(obj.options[index].value == Id)
            {
                obj.options[index].selected = true;
                break;
            }
        }
     }
}
  </script>