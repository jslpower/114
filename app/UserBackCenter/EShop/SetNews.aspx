<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetNews.aspx.cs" Inherits="UserBackCenter.EShop.SetNews"  ValidateRequest="false"%>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>最新动态</title>    
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .errmsg{
    color:#FF0000;
    }
        .style1
        {
            width: 497px;
        }
    </style>
    <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="false" ></script>
    <script type="text/javascript">
     //初始化编辑器
     KE.init({
	    id : 'editnews',//编辑器对应文本框id
	    width : '550px',
	    height : '350px',
	    skinsPath:'/kindeditor/skins/',
	    pluginsPath:'/kindeditor/plugins/',
	    scriptPath:'/kindeditor/skins/',
        resizeMode : 0,//宽高不可变
        imageUploadJson: '<%=EyouSoft.Common.Domain.FileSystem%>/UserBackCenter/upload_json.ashx',
        items: keMoreImg, //功能模式(keMore:多功能,keSimple:简易)
        blankPageUrl: '<%=EyouSoft.Common.Domain.UserBackCenter%>/kindeditor/plugins/image/blankpage.html'
    });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin:3px 0 3px 0;">
  <tr>
    <td width="80" style="border-bottom:1px solid #8BA2BE;">&nbsp;</td>
     <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqonc.gif" align="center"><a href="SetNews.aspx">最新动态添加</a></td>
    <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqupc.gif"  align="center"><a href="SetNewsList.aspx">最新动态列表</a></td>
    <td style="border-bottom:1px solid #8BA2BE;">&nbsp;</td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="14%" class="left1"><span class="errmsg">*</span>标题：</td>
    <td width="86%" class="right1">
       <input type="text" id="setnews_txtTitle" valid="required|limit" runat="server" max="30" 
            errmsg="请填写标题|标题不能大于30个字符" class="style1" />
             <span id="errMsg_setnews_txtTitle" class="errmsg"></span>
    </td>
  </tr>
  <tr>
    <td class="left1"><span class="errmsg">*</span>内容：</td>
    <td class="right1">             
    <textarea id="editnews" name="editnews" style="width:500px;height:200px;" runat="server" valid="required" errmsg="请输入内容"></textarea><span id="errMsg_editnews" class="errmsg"></span>
    </td>
  </tr>
  <tr>
    <td class="left1">发布时间：</td>
    <td class="right1" ><input type="text" id="setnews_txtDate" runat="server" readonly="readonly"/></td>
  </tr>
</table>
<table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center">
        <asp:Button ID="btnsubmit" Height="22" Width="60" runat="server" Text="提交" 
            onclick="btnSubmit_Click" />
        <input type="reset" name="Submit2" id="setnews_btnReplace" value="重置" style="height:22px; width:60px;"/>
        <asp:HiddenField ID="hdfOpeaType" runat="server" />
    </td>
  </tr>
</table>
    </div>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            FV_onBlur.initValid($("#<%=btnsubmit.ClientID %>").closest("form").get(0));
            setTimeout(
                function() {
                    KE.create('editnews', 0); //创建编辑器
                }, 100);
            $("#<%=btnsubmit.ClientID %>").click(function() {
                var b = false;
                $("#editnews").val(KE.html('editnews'));//2010-08-25xuty
                var dataContent= $("#editnews").val().replace(/<(?!img).*?>|&nbsp;/g,"").replace(/\s/gi,'');
                if (dataContent.length < 1) {
                    $("#errMsg_editnews").html("请输入内容");
                    return false;
                }
                if (dataContent.length > 0) {
                    $("#errMsg_editnews").html("");
                    b = true;
                }
                if (ValiDatorForm.validator($("#form1").get(0), "span") == true && b == true) {
                    return true;
                } else {
                    return false;
                }
            });
            //            $("#setnews_btnReplace").click(function()
            //            {
            //	            var oEditor = FCKeditorAPI.GetInstance('fckContent');
            //  	            oEditor.SetHTML('');
            //  	            $("#setnews_txtTitle").val("");
            //  	            return false;
            //            });
        });
    </script>
    </form>
</body>
</html>
