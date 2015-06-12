<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvinceAndCityAndAreaList.ascx.cs"
    Inherits="SiteOperationsCenter.usercontrol.ProvinceAndCityAndAreaList" %>
省份：<asp:DropDownList ID="drpProvinceID" runat="server">
    <asp:ListItem Value="0">请选择</asp:ListItem>
</asp:DropDownList>城市：<asp:DropDownList ID="drpCity" runat="server">
    <asp:ListItem Value="0">请选择</asp:ListItem>
</asp:DropDownList>线路区域：<select id="drpAreaID"><option value="0">请选择线路区域</option>
</select>

<script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

<script type="text/javascript">
  
    function SetProvince(ProvinceId) {
        $("#<%=drpProvinceID.ClientID %>").val(ProvinceId);
    }
    function SetCity(CityId, ProvinceId) {
        $("#<%=drpCity.ClientID %>").val(CityId);
        GetAreaList(CityId, ProvinceId);
    }
    function SetArea(AreaId) {
        $("#drpAreaID").val(AreaId);
    }

    function GetAreaList(cityid, ProvinceId)
    {
        var url = "/usercontrol/AjaxGetAreaList.aspx?ProvinceId=" + ProvinceId + "&CityID=" + cityid + "&rnd=" + Math.random();
        $.ajax
        ({
            url:url,
            cache:false,
            success:function(html)
            {
                 $("#drpAreaID").html(html); 
            }
        });
    }

    $(document).ready(function() {
        //GetAreaList($("#<%=drpCity.ClientID %>").val());

        $("#<%=drpCity.ClientID %>").change(function() {
            GetAreaList($(this).val(), $("#<%=drpProvinceID.ClientID %>").val());
        });
    });
    var provinceAndCityAndAreaUserControl = $.extend(provinceAndCityAndAreaUserControl || {});

    provinceAndCityAndAreaUserControl["<%=this.ClientID %>"] = {
    provinceId: "<%=this.drpProvinceID.ClientID %>",
    cityId: "<%=this.drpCity.ClientID %>",
    areaId: "drpAreaID"
    };
</script>

