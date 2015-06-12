<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetCard.aspx.cs" Inherits="UserBackCenter.EShop.SetCard" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>名片上传</title>
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />   
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />    
            <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr style=" height:150px;">
            <td width="22%" class="left1">名片上传：</td>
            <td width="78%" class="right1"><uc1:SingleFileUpload ID="sfucard" runat="server" ImageWidth="261" ImageHight="142"/>(图片大小261*142像素)
                <asp:Label ID="lblCardPath" runat="server" Text=""></asp:Label></td>
          </tr>
        </table>
        <table width="100%" height="50" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td align="center"><asp:Button ID="btnSubmcard" runat="server" Text="提交" Width="60" 
                    Height="22" onclick="btnSubmcard_Click" /></td>
          </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdfAgoImgPath" runat="server" />
    <script type="text/javascript">
    var sfu1,isSubmit=false;
    $(function(){
        sfu1 = <%=sfucard.ClientID %>;
        $("#<%=btnSubmcard.ClientID %>").click(function(){
            if(isSubmit){
                return true;
            }
            if(sfu1.getStats().files_queued<=0)
            {
                alert("请选择文件上传！");
                return false;
            }
            sfu1.customSettings.UploadSucessCallback = doSubmit;
            sfu1.startUpload();                
            return false;
        });
    });
    function doSubmit()
    {
        isSubmit = true;
        $("#<%=btnSubmcard.ClientID %>").click();
    }
    
    </script>
    </form>
</body>
</html>
