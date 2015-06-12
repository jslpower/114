<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperatorPartners.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.OperatorPartners" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>战略合作伙伴的新增和修改</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>


    <script language="javascript">
    </script>

    <style>
        h2
        {
            margin: 0 auto;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hid" name="hid" valid="custom" custom="custom1" errmsg="请选择要上传的图片！" />
    <table width="100%" border="1" cellpadding="5" cellspacing="0" bordercolor="#B9D3F2">
        <tr>
            <td width="17%" align="right" bgcolor="#D7E9FF">
                文字说明：
            </td>
            <td width="83%" align="left" bgcolor="#F7FBFF">
                <input ID="txtWordRemark" runat="server" MaxLength="30" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#D7E9FF">
                <span class="unnamed1">*</span>图片上传：
            </td>
            <td align="left" bgcolor="#F7FBFF">
                <uc1:SingleFileUpload ID="SingleFileUpload1" runat="server" ImageWidth="100" ImageHeight="30" /><%=img_Path %>
                <span id="errMsg_hid" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#D7E9FF">
                <span class="unnamed1">*</span>地址：
            </td>
            <td align="left" bgcolor="#F7FBFF">
                <input ID="txtAddress" runat="server" valid="required|isUrl" errmsg="请输入地址！|请输入有效的地址请输入有效的链接地址,例如：http://www.地址！"
                    value="http://www."  size="50" />
                <span id="errMsg_txtAddress" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#D7E9FF">
                &nbsp;
            </td>
            <td align="left" bgcolor="#F7FBFF">
                <asp:Button ID="btn_Save" runat="server" Text="提交" OnClick="btn_Save_Click" /> <input type="hidden" runat="server" id="hdfAgoImgPath" name="hdfAgoImgPath" />
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
    var sfu1 =<%=SingleFileUpload1.ClientID %>;
    function custom1(){
        var flag=true;
        //判断等待上传的文件是不是为空
        if(sfu1.getStats().files_queued<=0&&$("#SingleFileUpload1_hidFileName").val()==""&&$("#hdfAgoImgPath").val()=="")
        {   
              flag=false;
              return false;
        }
        return flag;
    };
    var isSubmit = false; //区分按钮是否提交过
    //模拟一个提交按钮事件
    function doSubmit(){
    isSubmit = true;
     $("#<%=btn_Save.ClientID%>").click();
    }
    $(function(){
        $("#<%=btn_Save.ClientID%>").click(function(){
            if(isSubmit){
            //如果按钮已经提交过一次验证，则返回执行保存操作
                return true;
            }
           
	        var a= ValiDatorForm.validator($("#form1").get(0),"span");
	        if(a){
	        //如果验证成功，则提交按钮保存事件
	         if(sfu1.getStats().files_queued>0){
                     sfu1.customSettings.UploadSucessCallback = doSubmit;
                     sfu1.startUpload();
                }else
                {
                     return true;
                }
            }
            return false;
        });
	    FV_onBlur.initValid($("#form1").get(0));
    });
    </script>

</body>
</html>
