<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetResources.aspx.cs" Inherits="UserBackCenter.EShop.SetResources" ValidateRequest="false" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx"TagName="SingleFileUpload" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加旅游资源推荐</title>
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />
    
<style type="text/css">
.errmsg{
color:#FF0000;
}
    .style1
    {
        width: 500px;
    }
</style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin:3px 0 3px 0;">
          <tr>
            <td width="80" style="border-bottom:1px solid #8BA2BE;">&nbsp;</td>
             <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqonc.gif" align="center"><a href="SetResources.aspx">旅游资源推添加</a></td>
            <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqupc.gif"  align="center"><a href="SetResourcesList.aspx">旅游资源推荐列表</a></td>
            <td style="border-bottom:1px solid #8BA2BE;">&nbsp;</td>
          </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="14%" class="left1"><span class="errmsg">*</span>标题：</td>
            <td width="86%" class="right1">
                <input name="textfield4" id="setreso_txttitle" runat="server" type="text" 
                    size="50" valid="required|limit" max="30" errmsg="请填写标题|标题不能大于30个字符" 
                    class="style1" />
                 <span id="errMsg_setreso_txttitle" class="errmsg"></span>
                </td>
          </tr> 
          <tr>
            <td class="left1">图片：</td>
            <td class="right1"><uc1:SingleFileUpload ID="sfuResoImg" runat="server" ImageWidth="123" ImageHight="82" />
                     (图片大小123*82像素)<%=img_Path %><a runat="server" id="delete_a_img"></a>
            </td>
          </tr>
          <tr>
            <td class="left1"><span class="errmsg">*</span>内容：</td>
            <td class="right1"><textarea id="editreso" name="editreso" style="width:500px;height:200px;" runat="server" valid="required" errmsg="请输入内容" ></textarea>
            <span id="errMsg_editreso" class="errmsg"></span>        
            </td>
          </tr>
          <tr>
            <td class="left1">发布时间：</td>
            <td class="right1"><input name="textfield3" id="setreso_txtDate" type="text" 
                    value="" readonly="readonly" runat="server" size="15" /></td>
          </tr>
        </table>
        <table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td align="center">  <asp:Button ID="btnSave" Text="提交" style="width:60px;" 
                    runat="server" onclick="btnSave_Click"  OnClientClick="return SetResources.Save()"/>  
            <input type="reset" name="Submit2" value="重置" style="height:22px; width:60px;"/>
                    <input type="hidden" runat="server" id="hdfAgoImgPath" name="hdfAgoImgPath"/>
                    </td>
          </tr>
        </table>
    </div>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="false" ></script>
    <script type="text/javascript">
     //初始化编辑器
     KE.init({
	    id : 'editreso',//编辑器对应文本框id
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
    <script type="text/javascript"> 
         var sfu1,isSubmit=false;   
//        function doSubmit()
//        {
//            isSubmit = true;
//            $("#<%=btnSave.ClientID %>").click();
//        }
        $(document).ready(function()
        {
            setTimeout(
                function(){
                  KE.create('editreso',0);//创建编辑器
             },100);
            FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
//            $("#<%=btnSave.ClientID %>").click(function()
//            {
//                   if(isSubmit)
//                   {
//                        return true;
//                  }
//                  sfu1 = "<%=sfuResoImg.ClientID %>";
//                  var b= ValiDatorForm.validator($("#form1").get(0),"span");
//                  if(!b)
//                  {
//                    return false;
//                  }
//                  var oEditor = $("#editreso").val();
//                  var dataContent= oEditor.replace(/<(?!img).*?>|&nbsp;/g,"").replace(/\s/gi,'');
//                  if(dataContent.length<1)              
//	               {
//	                    $("#errMsg_editreso").html("内容不能为空");
//	                    return false;
//	               }
//	               else
//                   {        
//                        $("#errMsg_editreso").html("");	
//                     
//                        if(sfu1.getStats().files_queued<=0)
//                        {
//                            doSubmit();
//                        }
//        	            sfu1.customSettings.UploadSucessCallback = doSubmit;
//                        sfu1.startUpload();     
//                        return false;          
//                   }   
//                        return false;       
//             });   
             	                             
        });
        SetResources=
        {
            Save:function()
            {
                if(isSubmit)
                   {
                        return true;
                  }
                  sfu1 = <%=sfuResoImg.ClientID %>;
                  $("#editreso").val(KE.html('editreso'));//2010-08-25
                  var b= ValiDatorForm.validator($("#form1").get(0),"span");
                  if(!b)
                  {
                    return false;
                  }

                  var oEditor = $("#editreso").val();
                  var dataContent= oEditor.replace(/<(?!img).*?>|&nbsp;/g,"").replace(/\s/gi,'');
                  if(dataContent.length<1)              
	               {
	                    $("#errMsg_editreso").html("内容不能为空");
	                    return false;
	               }
	               else
                   {        
                        $("#errMsg_editreso").html("");	
                 
                        if(sfu1.getStats().files_queued<=0)
                        {
                            return SetResources.doSubmit();
                        }
        	            sfu1.customSettings.UploadSucessCallback = SetResources.doSubmit;
                        sfu1.startUpload();                        
                   }   
                        return false;    
            },
            doSubmit:function()
            {
                isSubmit = true;
                $("#<%=btnSave.ClientID %>").click();
            }
        }
    </script>
    </form>
</body>
</html>
