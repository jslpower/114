<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetAdvInfo.aspx.cs" Inherits="UserBackCenter.EShop.SightShop.SetAdvInfo" %>

<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>景区网店广告设置</title>
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
                广告图片：
            </td>
            <td width="78%" align="left">
                <uc1:SingleFileUpload ID="sfuAdv" runat="server" ImageWidth="714" ImageHight="74" IsUploadSwf="true" />
                （图片大小714px*74px）<br/>
                <asp:Literal runat="server" ID="ltOldAdv"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="left1">
                广告链接：
            </td>
            <td align="left">
                <input type="text" id="txtSite" name="txtSite" runat="server" />
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
    <asp:HiddenField runat="server" ID="hOldAdvPath" />
    <asp:HiddenField runat="server" ID="hOldAdvId" />
    </form>
</body>
</html>

<script type="text/javascript">
var isNewSubmit=false;
var RegExps = function() { };
RegExps.isUrl = /[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
$(function(){
    $("#<%= btnSave.ClientID %>").click(function(){
        //$("#<%= btnSave.ClientID %>").attr("disabled","disabled");
        if (isNewSubmit) {
           return true;
        }
        else {
            var strErr = "";
            if ($("#<%= hOldAdvPath.ClientID %>").val() == "") {  //原来有图片不作上传图片验证
                if (sfuAdv.getStats().files_queued <= 0)
                    strErr += "请上传广告图片！\n";
            }
            else if($.trim($("#<%= txtSite.ClientID %>").val())=="")
                strErr+="请输入广告链接！";
            else if(!RegExps.isUrl.test($.trim($("#txtSite").val())))
                strErr+="请输入合法的广告链接！";
            if(strErr!="")
            {
                alert(strErr);
                return false;
            }
            if (sfuAdv.getStats().files_queued > 0) {
                sfuAdv.customSettings.UploadSucessCallback = doNewSubmit;
                sfuAdv.startUpload();
                return false;
            }
            else
            {
                alert("请选择上传文件!");
                return false;
            }            
        }
    });
});
function doNewSubmit() {
    isNewSubmit = true;
    $("#<%= btnSave.ClientID %>").click();
}
</script>

