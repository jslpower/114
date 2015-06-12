<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherAllSaleCity.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.OtherAllSaleCity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%@ import namespace="EyouSoft.Common" %>
<head runat="server">
    <title>设置销售城市</title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <style>
        body
        {
            margin: auto 0;
            padding: 0;
        }
        td
        {
            font-size: 12px;
            line-height: 120%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2" align="center" style="line-height: 25px;">
                <table width="100%" border="0" id="tb_ParentSaleCity" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="6" align="center">
                            <strong id="oasc_title">已选择的销售城市</strong>
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td id="td_ParentSaleCity" align="left" bgcolor="#FFFFFF">
                        </td>
                    </tr>
  
                </table>
                <table width="100%" border="0" id="tbAllSaleCity" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0"
                    style="margin-top: 2px;">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="2" align="center">
                           <input  type="checkbox" id="ckAllCity" name="ckAll" onclick="OtherAllSaleCity.ckAllCity(this);"/><label for="ckAllCity" style="cursor:pointer"><strong>全国</strong></label>
                        </td>
                    </tr>
                    <%=strAllCityList%>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="6">
                    <tr>
                        <td align="center">
                            <input name="btnSave" type="button" id="btnSave" value=" 确 定" onclick="OtherAllSaleCity.CkSaleCity();"
                                style="height: 28px; font-weight: bold; color: #990000" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var ckAllCityId = new Array();
        var OtherAllSaleCity = {
            SetSaleCity: function() {  //初始化己选销售城市
                var thisCompanyCity = "";
                var parentCity = parent.$("#spanSellCity").html();
                if (parentCity != null && parentCity != "" && parentCity != undefined) {
                    thisCompanyCity = parentCity
                }
                if (thisCompanyCity != "") {
                    $("#td_ParentSaleCity").html(thisCompanyCity);
                } else {
                    $("#tb_ParentSaleCity").hide();
                }
            },
            CkSaleCity: function() {
                var OldCkCity = "";
                $("#td_ParentSaleCity input[type=checkbox]:checked").each(function() {
                    var ckCityId = $(this).attr("id");
                    var ckCityValue = $(this).attr("value");
                    var CityName = $("#td_ParentSaleCity label[for='" + ckCityId + "']").html() != null ? $("#td_ParentSaleCity label[for='" + ckCityId + "']").html() : $(this).attr("cityname");
                    var strHTML = "<input type='checkbox' id='ckSale_" + ckCityValue + "' value='" + ckCityValue + "' checked='checked' name='ckSellCity'/><label for='ckSale_" + ckCityValue + "'>" + CityName + "</label>";
                    ckAllCityId.push(ckCityValue);
                    OldCkCity += strHTML;

                });
                var thisCkCity = "";
                $("#tbAllSaleCity input[type=checkbox][name!=ckProvince][name!=ckAll]:checked").each(function() {
                    var ckCityId = $(this).attr("id");
                    var ckCityValue = $(this).attr("value");
                    var CityName = $("#tbAllSaleCity label[for='" + ckCityId + "']").html();
                    var strHTML = "<input type='checkbox' id='ckSale_" + ckCityValue + "' value='" + ckCityValue + "' checked='checked' name='ckSellCity'/><label for='ckSale_" + ckCityValue + "'>" + CityName + "</label>";
                    if (ckAllCityId.length > 0 && $.inArray(ckCityValue, ckAllCityId) == -1) {
                        thisCkCity += strHTML;
                    } else if (ckAllCityId.length == 0) {
                        thisCkCity += strHTML;
                    }
                });
                parent.$("#spanSellCity").html(OldCkCity + thisCkCity);
                var frameid = window.parent.Boxy.queryString("iframeId");
                window.parent.Boxy.getIframeDialog(frameid).hide();

            },
            ckAllCity: function(obj) {
                $("#tbAllSaleCity").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            },
            ckAllProvinceCity: function(obj) {
                $(obj).parent("td").next().find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            }
        };
        $(function() {
            OtherAllSaleCity.SetSaleCity();
            var strTitle='<%= Server.HtmlDecode(Request.QueryString["title"]) %>';
            if(strTitle)
            {  
               $("#oasc_title").html(strTitle);
            }
        });
    </script>

    </form>
</body>
</html>
