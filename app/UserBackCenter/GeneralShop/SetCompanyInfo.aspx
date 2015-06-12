<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetCompanyInfo.aspx.cs" Inherits="UserBackCenter.GeneralShop.SetCompanyInfo" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx"TagName="SingleFileUpload" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title></title>
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />    
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <style type="text/css">
        .errmsg{
        color:#FF0000;
        }
    </style>
</head>

<body>
 <form id="form1" runat="server">
<table width="100%" id="" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="22%" class="left1">
        <span class="errmsg">*</span>品牌名称：
    </td>
    <td width="78%" class="right1" >
        <input name="txtCompanyBrand" runat="server" id="txtCompanyBrand" type="text" size="35" valid="required" errmsg="请填写品牌名称" runat="server"/>
        <span id="errMsg_txtCompanyBrand" class="errmsg"></span>
    </td>
    </tr>
  <%--<tr>
    <td width="22%" class="left1" >
        出港城市：</td>
    <td width="78%" class="right1" >
        <asp:DropDownList ID="ddlDepartureCity" runat="server"  valid="required"  errmsg="请选择出港城市">
            <asp:ListItem>请选择</asp:ListItem>
        </asp:DropDownList>
        <span id="errMsg_ddlDepartureCity" class="errmsg"></span>
            </td>
    </tr>--%>
    <tr>
        <td class="left1">
            <span class="errmsg">*</span>联系人：
        </td>
        <td class="right1" >
            <input name="txtContactName" runat="server" id="txtContactName" type="text" size="35" valid="required" errmsg="请填写联系人"/><span id="errMsg_txtContactName" class="errmsg"></span>
        </td>
    </tr>
    <tr>
        <td class="left1">
            MQ号：</td>
        <td class="right1" >
            <input runat="server" name="txtMQ" runat="server" id="txtMQ" type="text" size="35" disabled /></td>
    </tr>
    <tr>
        <td class="left1">
            手机：
        </td>
        <td class="right1" >
         <input name="txtContactMobile" runat="server" id="txtContactMobile" type="text" size="35" valid="isMobile"   errmsg="手机号码填写错误"/><span id="errMsg_txtContactMobile" class="errmsg"></span>
        </td>
    </tr>
    <tr>
        <td class="left1">
            电话：
        </td>
        <td class="right1">
            <input name="txtContactTel" runat="server" id="txtContactTel" type="text" size="45" /><span
                id="errMsg_txtContactTel" class="errmsg"></span>
        </td>
    </tr>
    <tr>
        <td class="left1">
            传真：
        </td>
        <td class="right1" >
            <input name="txtContactFax" runat="server" id="txtContactFax" type="text" size="35" />
        </td>
    </tr>
    <tr>
        <td class="left1">
            地址：
        </td>
        <td class="right1" >
            <input name="txtOfficeAddress" runat="server" id="txtOfficeAddress" type="text" size="55" /><%--valid="required" errmsg="请填写地址!"<span id="errMsg_txtOfficeAddress" class="errmsg"></span>--%>
        </td>
    </tr>
  </table>
<table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center"><asp:Button ID="btnSubmInfo" runat="server" Text="提交" Width="60" 
            Height="22" onclick="btnSave_Click" />
    <input type="reset" value="重置" id="btnReplInfo" style="height:22px; width:60px;"/></td>
  </tr>
</table>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    <script type="text/javascript">
    $(document).ready(function(){    	
       	FV_onBlur.initValid($("#<%=btnSubmInfo.ClientID %>").closest("form").get(0));
        $("#<%=btnSubmInfo.ClientID %>").click(function(){
            if("<%=IsCompanyCheck%>"=="False")
            {
                alert("对不起，您还未开通审核，不能进行此操作！");
                return false;
            }
	        return ValiDatorForm.validator($("#form1").get(0),"span");
        });
        $("#btnReplInfo").click(function()
        {        
       
              $("#btnReplInfo").closest("form").get(0).reset();
            return false;
        });
    });
    </script>
    </form>
</body>
</html>
