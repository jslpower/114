﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetCopyRight.aspx.cs" Inherits="UserBackCenter.EShop.SetCopyRight" validateRequest="false"%>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>版权设置</title>
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/kindeditor/kindeditor.js"  cache="false" ></script>
    <script type="text/javascript">
     //初始化编辑器
     KE.init({
	    id : 'editcopy',//编辑器对应文本框id
	    width : '550px',
	    height : '400px',
	    skinsPath:'/kindeditor/skins/',
	    pluginsPath:'/kindeditor/plugins/',
	    scriptPath:'/kindeditor/skins/',
        resizeMode : 0,//宽高不可变
        items: keMore, //功能模式(keMore:多功能,keSimple:简易)
	     imageUploadJson: '<%=EyouSoft.Common.Domain.FileSystem%>/UserBackCenter/upload_json.ashx',
            items: keMoreImg, //功能模式(keMore:多功能,keSimple:简易)
            blankPageUrl: '<%=EyouSoft.Common.Domain.UserBackCenter%>/kindeditor/plugins/image/blankpage.html'
    });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="13%" class="left1">版权内容：</td>
            <td width="87%" class="right1">
            <textarea id="editcopy" name="editabout" style="width:500px;height:200px;" runat="server" ></textarea>
            </td>
          </tr>
        </table>
        <table width="100%" height="50" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td align="center">
            <asp:Button ID="btnSave" Text="提交" Height="22" Width="60" runat="server" 
                    onclick="btnSave_Click" />
            <input type="button" name="btnReplace" value="重置" style="height:22px; width:60px;"/>
            </td>
          </tr>
        </table>
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
        <script type="text/javascript">
             setTimeout(
                function(){
                  KE.create('editcopy',0);//创建编辑器
                },100);
                
                
                 $(document).ready(function(){ 
                    $("#<%=btnSave.ClientID %>").click(function(){
                       $("#editcopy").val(KE.html('editcopy')); 
                    });
                });
        </script>
    </div>
    </form>    
</body>
</html>
