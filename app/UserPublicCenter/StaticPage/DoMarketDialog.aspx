<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoMarketDialog.aspx.cs" Inherits="UserPublicCenter.StaticPage.DoMarketDialog"  EnableEventValidation="false" %>
<%@ Register Src="/WebControl/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>营销申请</title>
</head>
<body>
    <style type="text/css">   
	body{ margin:0; padding:0;}
	.reg-box{ width:611px;  margin:0 auto; font-family:"宋体"; font-size:14px; color:#000000; padding:0;}
	.reg-box ul{list-style:none; margin:0; padding:0; text-align:left;}
	.reg-box ul li{ padding:8px 0;}
	.reg-box ul li label{ text-align:right; vertical-align:top; display:inline-block; width:115px; line-height:20px; padding-right:15px;}
	.reg-box ul li select{ width:80px;}
	.btn-box{ position:relative;  background:url(<%=ImageServerPath %>/images/new2011/index/yxsq_11.gif) no-repeat center bottom; height:127px;}
	.btn-box .manicon{ background:url(<%=ImageServerPath %>/images/new2011/index/yxsq_07.png) no-repeat right bottom; display:inline-block; height:157px; position:absolute; top:-30px; right:0; width:108px;}
	.btn{ background:url(<%=ImageServerPath %>/images/new2011/index/yxregbg.gif) no-repeat 0 -144px; margin:25px 0 0 130px; border:0 none; width:97px; height:31px; font-weight:bold; font-size:14px; color:#FFFFFF; cursor:pointer;}
    </style>
    <!--[if lte IE 6]>
        <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("Png") %>"></script>
        <script type="text/javascript">
            DD_belatedPNG.fix('div,ul,li,a,p,img,s,span');
        </script>  
    <![endif]-->
    <form id="form1" runat="server">
        <div class="reg-box">
    	    <ul>
        	    <li><label><font color="#FF0000">*</font> 贵公司名称：</label><input  id="txtCompanyName" name="txtCompanyName" type="text" size="25" /></li>
                <li><span style="display:inline-block; line-height:20px; padding-right:15px; text-align:right; width:115px;"><font color="#FF0000">*</font> 所在地：</span><uc2:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" /></li>
                <li><label><font color="#FF0000">*</font> 您贵姓：</label><input  id="txtName" name="txtName" type="text" size="10" /></li>
                <li><label><font color="#FF0000">*</font> 您的联系方式：</label><input id="txtTel" name="txtTel" type="text" size="25" /> (手机或座机)</li>
                <li><label>目前您企业碰到的问题：</label><textarea  id="txtContent" name="txtContent" cols="45" rows="6"></textarea></li>
            </ul>
            <div class="btn-box">
                <input name="btnSave" id="btnSave" value="提 交" type="button" class="btn" />
                <s class="manicon"></s>
                <div style="clear:both;"></div>
            </div>
        </div>
        
        <input id="hidSave" name="hidSave" type="hidden" />
        <input id="hidType" name="hidType" type="hidden" value="<%=type %>" />
    </form>
    
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery1.4") %>"></script>
    
    <script type="text/javascript">
        $("#btnSave").click(function() {
            var errMsg = "";
            if ($.trim($("#txtCompanyName").val()) == "") {
                errMsg += "请填写公司名称\n";
            }
            if ($.trim($('#<%=ProvinceAndCityList1.FindControl("ddl_ProvinceList").ClientID%>').val()) == "0") {
                errMsg += "请选择省份\n";
            }
            if ($.trim($('#<%=ProvinceAndCityList1.FindControl("ddl_CityList").ClientID%>').val()) == "0") {
                errMsg += "请选择城市\n";
            }
            if ($.trim($("#txtName").val()) == "") {
                errMsg += "请填写姓名\n";
            }
            if ($.trim($("#txtTel").val()) == "") {
                errMsg += "请填写联系方式";
            }
            if (errMsg != "") {
                alert(errMsg);
                return false;
            } else {
                $("#hidSave").val("save");
                $("#<%= form1.ClientID %>").get(0).submit();
                return true;
            }
            return false;
        });
    </script>
</body>
</html>
