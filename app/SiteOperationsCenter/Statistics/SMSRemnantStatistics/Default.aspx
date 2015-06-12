<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SiteOperationsCenter.Statistics.SMSRemnantStatistics.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register src="../../usercontrol/ProvinceAndCityList.ascx" tagname="ProvinceAndCityList" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信余额统计</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif" id="tdSMS"><uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />单位名称<input name="txtCompanyName" id="txtCompanyName" type="text" class="textfield" size="12" />&nbsp;<img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom:-3px; cursor:pointer" id="imgBtnSearch" /></td>
    </tr>
  </table>
 
    <div id="divSMSRemnantList" style="margin:0px;padding:0px;" align="center"></div>
    </form>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
<script type="text/javascript">
    var SMSRemnantStatistics = {
        SmsParms: { "Page": 1, "ProvinceId": 0, "CityId": 0, "CompanyName": "" },
        GetSMSRemnantList: function() {
            LoadingImg.ShowLoading("divSMSRemnantList");
            if (LoadingImg.IsLoadAddDataToDiv("divSMSRemnantList")) {
                $.ajax({
                    type: "GET",
                    dataType: 'html',
                    url: "GetSMSRemnantList.aspx?" + $.param(SMSRemnantStatistics.SmsParms),
                    cache: false,
                    success: function(html) {
                        $("#divSMSRemnantList").html(html);
                    },
                    error:function(){
                        $("#divSMSRemnantList").html("未能成功获取响应结果");
                    }
                });
            }
        },
        OnSearch: function() {
            this.SmsParms.Page = 1;
            this.SmsParms.ProvinceId = $("#ProvinceAndCityList1_ddl_ProvinceList").val();
            this.SmsParms.CityId = $("#ProvinceAndCityList1_ddl_CityList").val();
            this.SmsParms.CompanyName = encodeURIComponent($.trim($("#txtCompanyName").val()));
            this.GetSMSRemnantList();
        },
        LoadData: function(obj) {
            var Page = exporpage.getgotopage(obj);
            this.SmsParms.Page = Page;
            this.GetSMSRemnantList();
        }
    };

    $(document).ready(function() {
        LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
        SMSRemnantStatistics.GetSMSRemnantList();
        $("#imgBtnSearch").click(function() {
            SMSRemnantStatistics.OnSearch();
        });
        $("#tdSMS input[type='text']").keypress(function(e){
            if(e.keyCode == 13)
            {
                SMSRemnantStatistics.OnSearch();
                return false;
            }
        });
    });
</script>
</body>
</html>
