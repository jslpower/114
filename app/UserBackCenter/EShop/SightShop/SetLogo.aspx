<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetLogo.aspx.cs" Inherits="UserBackCenter.EShop.SightShop.SetLogo" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>景区网店-Logo设置</title>
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="22%" class="left1">
                LOGO：
            </td>
            <td width="78%" align="left">
                <uc1:SingleFileUpload ID="sfuLogo" runat="server" ImageWidth="714" ImageHight="74" IsUploadSwf="true" />
                （图片大小218px*88px）<br/>
                <asp:Literal runat="server" ID="ltOldLogo"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button runat="server" ID="btnSave" Text="保存" Style="height: 22px; width: 60px;"
                    OnClick="btnSave_Click" />
                &nbsp;
                <input type="button" name="btnCancel" value="取消" style="height: 22px; width: 60px;"
                    onclick="parent.Boxy.getIframeDialog('<%= Request.QueryString["iframeId"] %>').hide()" />
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hOldLogoPath" />
    </form>
</body>
</html>
<script type="text/javascript">
    var isNewSubmit = false;
    var sfuLogo =<%=sfuLogo.ClientID %>;
$(function(){
    $("#<%= btnSave.ClientID %>").click(function(){
        //$(this).attr("disabled","disabled");
        if (isNewSubmit) {
            return true;
        }
        else {
            var strErr = "";
            if ($("#<%= hOldLogoPath.ClientID %>").val() == "") {  //原来有图片不作上传图片验证
                if (sfuLogo.getStats().files_queued <= 0)
                        strErr += "请上传LOGO！\n";
            }
            if(strErr!="")
            {
                alert(strErr);
                return false;
            }
            if (sfuLogo.getStats().files_queued > 0) {
                sfuLogo.customSettings.UploadSucessCallback = doNewSubmit;
                sfuLogo.startUpload();
                return false;
            }
            else
            {
                return true;
            }
        }
    });
});
function doNewSubmit() {
    isNewSubmit = true;
    $("#<%= btnSave.ClientID %>").click();
}
</script>
