<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvinceAndCityList.ascx.cs"
    Inherits="SiteOperationsCenter.usercontrol.ProvinceAndCityList" %>
<span class="unnamed1" id="ProvinceRequired" style="display: none">*</span>省份：<asp:DropDownList
    ID="ddl_ProvinceList" runat="server">
</asp:DropDownList>
<span class="unnamed1" id="CityRequired" style="display: none">*</span>城市：<asp:DropDownList
    ID="ddl_CityList" runat="server">
</asp:DropDownList>

<script type="text/javascript">
  
    function SetProvince(ProvinceId) {
        $("#<%=ddl_ProvinceList.ClientID %>").attr("value", ProvinceId);
    }
    function SetCity(CityId) {
        $("#<%=ddl_CityList.ClientID %>").attr("value", CityId);
    }

    var provinceAndCityUserControl = $.extend(provinceAndCityUserControl || {});

    provinceAndCityUserControl["<%=this.ClientID %>"] = {
        provinceId: "<%=this.ddl_ProvinceList.ClientID %>",
        cityId: "<%=this.ddl_CityList.ClientID %>"
    };
</script>

