<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvinceAndCityList.ascx.cs"
    Inherits="UserBackCenter.usercontrol.ProvinceAndCityList" %>
<span class="unnamed1" id="ProvinceRequired" style="display: none">*</span>省份：<asp:DropDownList
    ID="ddl_ProvinceList"  valid="required" errmsg="省份不能为空！"  runat="server">
</asp:DropDownList>
<span class="unnamed1" id="CityRequired" style="display: none">
    *</span>城市：<asp:DropDownList ID="ddl_CityList" runat="server"  valid="required" errmsg="城市不能为空！" >
    </asp:DropDownList>
    <span id="errMsg_<%=ddl_ProvinceList.ClientID %>" class="errmsg"></span>
<span id="errMsg_<%=ddl_CityList.ClientID %>" class="errmsg"></span>
<script type="text/javascript">
  
    function SetProvince(ProvinceId) {
        $("#<%=ddl_ProvinceList.ClientID %>").attr("value", ProvinceId);
    }
    function SetCity(CityId) {
        $("#<%=ddl_CityList.ClientID %>").attr("value", CityId);
    }
    var <%=this.ClientID %> ={
        getProvince:function(){
           return "<%=ddl_ProvinceList.ClientID %>";
        },
        getCity:function(){
            return "<%=ddl_CityList.ClientID%>";
        }
    };
</script>

