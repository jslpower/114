<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetVotePolicy.aspx.cs" Inherits="UserBackCenter.EShop.SetVotePolicy" validateRequest="false" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>门票政策</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="false"></script>
    <script type="text/javascript">
        //初始化编辑器
        KE.init({
            id: 'editGuid', //编辑器对应文本框id
            width: '550px',
            height: '350px',
            skinsPath: '/kindeditor/skins/',
            pluginsPath: '/kindeditor/plugins/',
            scriptPath: '/kindeditor/skins/',
            resizeMode: 0, //宽高不可变
            items: keMore, //功能模式(keMore:多功能,keSimple:简易)
            imageUploadJson: '<%=EyouSoft.Common.Domain.FileSystem%>/UserBackCenter/upload_json.ashx',
            items: keMoreImg,//功能模式(keMore:多功能,keSimple:简易)
            blankPageUrl:'<%=EyouSoft.Common.Domain.UserBackCenter%>/kindeditor/plugins/image/blankpage.html'
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="right">
                门票政策：
            </td>
            <td class="right1" align="left">
                <textarea id="editGuid" name="editGuid" style="width: 500px; height: 200px;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" Text="提交" Style="height: 22px; width: 60px;" runat="server"
                        OnClick="Submit1_Click" OnClientClick="$('#editGuid').val(KE.html('editGuid'));" />
                    <input type="button"  value="取消" style="height: 22px; width: 60px;" onclick="parent.Boxy.getIframeDialog('<%= Request.QueryString["iframeId"] %>').hide()" />
            </td>
        </tr>
     </table>
     <asp:HiddenField runat="server" ID="hId" />
     <asp:HiddenField runat="server" ID="hidDefaultHTML" Value="<div style='margin-top:15px; line-height:24px;'><font style='color:#194c07; font-weight:bold;'>地点：</font>&nbsp;这里请填写景区所在的地址&nbsp;&nbsp;&nbsp;<br><font style='color:#194c07; font-weight:bold;'>门票价：</font><font style='font-weight:bold; font-size:14px;color:#fc4d00;'>&nbsp;￥0.0</font><br><font style='color:#194c07; font-weight:bold;'>优惠说明：</font>&nbsp;这里请填写需说明的优惠政策&nbsp;&nbsp;&nbsp;<br><font style='color:#194c07; font-weight:bold;'>团队预订说明：</font>&nbsp;这里请填写具体的团队预订说明&nbsp;&nbsp;&nbsp;</div>" />
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(document).ready(function() {
        setTimeout(
        function() {
            KE.create('editGuid', 0); //创建编辑器
        }, 100);
    });	         
</script>
