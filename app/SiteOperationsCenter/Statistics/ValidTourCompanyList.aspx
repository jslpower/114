<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidTourCompanyList.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.ValidTourCompanyList" %>
    
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/ProvinceAndCityAndAreaList.ascx" TagName="ProvinceAndCityAndAreaList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>有有效产品批发商</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet"
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="70%" background="<%=ImageServerUrl %>/images/gongneng_bg.gif" align="left">
                <uc1:ProvinceAndCityAndAreaList ID="ProvinceAndCityAndAreaList1" runat="server" />
                &nbsp; 单位名称：
                <input name="txtCompanyName" id="txtCompanyName" type="text" class="textfield" runat="server" size="15" />
                <img src="<%=ImageServerUrl %>/images/chaxun.gif" width="62" height="21" id="imgSearch" style="margin-bottom: -3px;cursor:pointer;" />
            </td>
        </tr>
    </table>
    <div id="divValidTourCompanys" align="center">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript">
    var Parms = {ProvinceID: 0,CityID: 0,AreaID: 0,CompanyName: "",Page: 1,Invalid: false};
    var ValidTourCompanys = {         
         OpenDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
                Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
         },
         GetValidTourCompanys: function()
         {
            if('<%=Invalid %>' == 'True')
            {
                Parms.Invalid = true;
            }
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";

            LoadingImg.ShowLoading("divValidTourCompanys");
            if (LoadingImg.IsLoadAddDataToDiv("divValidTourCompanys")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxGetValidTourCompanys.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divValidTourCompanys").html(html);
                        },
                        error:function(){
                            $("#divValidTourCompanys").html("未能成功获取相应结果!");
                        }
                    });
                }
         },
         LoadData: function(obj) {//分页
            var Page = exporpage.getgotopage(obj);
            Parms.Page = Page;
            this.GetValidTourCompanys();
        },
        OnSearch: function() {//查询
            Parms.ProvinceID = $("#ProvinceAndCityAndAreaList1_drpProvinceID").val();
            Parms.CityID = $("#ProvinceAndCityAndAreaList1_drpCity").val();
            Parms.AreaID = $("#drpAreaID").val();
            Parms.CompanyName = $.trim($("#<%=txtCompanyName.ClientID %>").val());
            Parms.Page=1;
            this.GetValidTourCompanys();
        }
    };
    $(document).ready(function(){
        ValidTourCompanys.GetValidTourCompanys();
        
        $("#imgSearch").click(function(){
            ValidTourCompanys.OnSearch();
            return false;
        });        
        $("input[type=text]").bind("keypress",function(e){
            if(e.keyCode == 13)
            {
                ValidTourCompanys.OnSearch();
                return false;
            }
        });
    });
    
    function mouseovertr(o) {
	  o.style.backgroundColor="#FFF9E7";
      }
      function mouseouttr(o) {
	      o.style.backgroundColor=""
      }
    </script>

</body>
</html>
