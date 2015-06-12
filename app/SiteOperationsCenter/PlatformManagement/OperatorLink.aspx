<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperatorLink.aspx.cs" Inherits="SiteOperationsCenter.PlatformManagement.OperatorLink" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>友情链接管理页面</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>


    <style>
        h2
        {
            margin: 0 auto;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" name="form1" runat="server">
    <table width="100%" border="1" cellpadding="5" cellspacing="0" bordercolor="#B9D3F2">
        <tr>
            <td width="17%" align="right" bgcolor="#D7E9FF">
                <span class="unnamed1">*</span>链接文字：
            </td>
            <td width="68%" align="left" bgcolor="#F7FBFF">
                <input ID="txtLinkWords" runat="server" name="txtLinkWords" valid="required" errmsg="请输入链接文字！" MaxLength="30" />
              <span id="errMsg_txtLinkWords" class="errmsg"></span>
           </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#D7E9FF">
                <span class="unnamed1">*</span>链接地址：
            </td>
            <td align="left" bgcolor="#F7FBFF">
                <input ID="txtLinkAddress" runat="server" size="50" valid="required|isUrl" errmsg="请输入链接地址|请输入有效的链接地址,例如：http://www.地址" value="http://www." />
              <span id="errMsg_txtLinkAddress" class="errmsg"></span>
           </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#D7E9FF">
            </td>
            <td align="left" bgcolor="#F7FBFF" colspan="3">
                <asp:Button ID="btn_Save" runat="server" Text="提交" OnClick="btn_Save_Click" />
            </td>
        </tr>
    </table>
    </form>
    
    <script type="text/javascript">
     $(function(){
	$("#<%=btn_Save.ClientID %>").click(function(){
		return ValiDatorForm.validator($("#form1").get(0),"span");
	});
	FV_onBlur.initValid($("#form1").get(0));
	
});
    </script>
</body>
</html>
