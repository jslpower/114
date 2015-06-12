<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetTravelGuid.aspx.cs"
    Inherits="UserBackCenter.EShop.SetTravelGuid" ValidateRequest="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>旅游资讯添加</title>
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="false"></script>

    <style type="text/css">
        .errmsg
        {
            color: #FF0000;
        }
        .style1
        {
            width: 497px;
        }
    </style>

    <script type="text/javascript">
        //初始化编辑器
        KE.init({
            id: 'editGuid', //编辑器对应文本框id
            width: '550px',
            height: '350px',
            pluginsPath: '/kindeditor/plugins/',
            scriptPath: '/kindeditor/skins/',
            resizeMode: 0, //宽高不可变
            imageUploadJson: '<%=EyouSoft.Common.Domain.FileSystem%>/UserBackCenter/upload_json.ashx',
            items: keMoreImg, //功能模式(keMore:多功能,keSimple:简易)
            blankPageUrl: '<%=EyouSoft.Common.Domain.UserBackCenter%>/kindeditor/plugins/image/blankpage.html'
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin: 3px 0 3px 0;">
            <tr>
                <td width="80" style="border-bottom: 1px solid #8BA2BE;">
                    &nbsp;
                </td>
                <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqonc.gif"
                    align="center">
                    <a href="SetTravelGuid.aspx?GuideType=<%= GuideType %>&TypeId=<%= intTypeId %>">
                        <asp:Literal runat="server" ID="ltrEditTitle">目的地指南添加</asp:Literal></a>
                </td>
                <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqupc.gif"
                    align="center">
                    <a href="SetTravelGuidList.aspx?GuideType=<%= GuideType %>&TypeId=<%= intTypeId %>">
                        <asp:Literal runat="server" ID="ltrListTitle">目的地指南列表</asp:Literal></a>
                </td>
                <td style="border-bottom: 1px solid #8BA2BE;">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="14%" class="left1">
                    <span class="errmsg">*</span>类别：
                </td>
                <td width="86%" class="right1" align="left">
                    <asp:DropDownList runat="server" ID="ddlGuidType" valid="isPIntegers" errmsg="请选择类别">
                    </asp:DropDownList>
                    <span id="errMsg_ddlGuidType" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td class="left1">
                    <span>*</span>标题：
                </td>
                <td class="right1" align="left">
                    <input name="settravelguid_txtTitle" type="text" valid="required|limit" max="30"
                        runat="server" id="txtGuidTitle" errmsg="标题不能为空|标题不大于30个字" class="style1" />
                    <span id="errMsg_txtGuidTitle" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td class="left1">
                    图片：
                </td>
                <td class="right1" align="left">
                    <uc1:SingleFileUpload ID="sfuguidpic" runat="server" ImageWidth="679" ImageHeight="454" />
                    (图片大小679*454像素)<%=img_Path %><a runat="server" id="delete_a_img"> </a>
                </td>
            </tr>
            <tr>
                <td class="left1">
                    <span class="errmsg">*</span>内容：
                </td>
                <td class="right1" align="left">
                    <textarea id="editGuid" name="editGuid" style="width: 500px; height: 200px;" runat="server"
                        valid="required" errmsg="请输入内容"></textarea>
                    <span class="errmsg" id="errMsg_editGuid"></span>
                </td>
            </tr>
            <tr>
                <td class="left1">
                    发布时间：
                </td>
                <td class="right1" align="left">
                    <input name="txtGuidTime" type="text" readonly="readonly" valid="required" size="15"
                        runat="server" id="txtGuidTime" errmsg="发布时间不能为空" />
                </td>
            </tr>
        </table>
        <table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" Text="提交" Style="height: 22px; width: 60px;" runat="server"
                        OnClick="Submit1_Click" OnClientClick="return SetTravelGuid.Save()" />
                    <input type="reset" name="Reset1" value="重置" style="height: 22px; width: 60px;" id="btnclear" />
                    <input type="hidden" runat="server" id="hdfAgoImgPath" name="hdfAgoImgPath" />
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript">     
     var sfu1,isSubmit=false;   

        $(document).ready(function()
        {
             setTimeout(
                function(){
                  KE.create('editGuid',0);//创建编辑器
             },100);
            FV_onBlur.initValid($("#btnSave").closest("form").get(0));
	    });	         
    var SetTravelGuid=
    {
       Save:function()
        {
               if(isSubmit)
                {
                    return true;
                }
                sfu1 = <%=sfuguidpic.ClientID %>;
                
              $("#editGuid").val(KE.html('editGuid'));//2010-08-25
              var b= ValiDatorForm.validator($("#form1").get(0),"span");
              if(!b)
              {
                return false;
              }
              
            
              var oEditor = $("#editGuid").val();
              var dataContent= oEditor.replace(/<(?!img).*?>|&nbsp;/g,"").replace(/\s/gi,'');
              if(dataContent.length<1)              
	           {
	                $("#errMsg_editGuid").html("内容不能为空");
	                return false;
	           }
	           else
               {        
                 $("#errMsg_editGuid").html(""); 
                    if(sfu1.getStats().files_queued<=0)
                    {
                        return SetTravelGuid.doSubmit();
                    }
        	        sfu1.customSettings.UploadSucessCallback = SetTravelGuid.doSubmit;
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
