<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperatorNewsPage.aspx.cs"
    Inherits="SiteOperationsCenter.NewsCenterControl.OperatorNewsPage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新闻中心新增修改管理页</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="1" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;
        padding: 1px;">
        <tr>
            <td width="13%" style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                <span class="unnamed1">* </span>类别：
            </td>
            <td width="87%" style="background: #fff; height: 28px; text-align: left;">
                <asp:DropDownList ID="ddlType" name="ddlType" valid="required" runat="server" errmsg="请选择！">
                </asp:DropDownList>
                <span id="errMsg_ddlType" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                <span class="unnamed1">* </span>标题：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <input id="txt_TitleName" runat="server" name="txt_TitleName" type="text" size="80"
                    valid="required" maxlength="80" errmsg="请输入标题！" />&nbsp; <span id="errMsg_txt_TitleName"
                        class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td height="33" style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                图片：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <uc1:SingleFileUpload ID="SingleFileUpload1" runat="server" ImageHeight="400" ImageWidth="600"
                    SiteModule="新闻中心" />
                &nbsp;&nbsp;&nbsp;图片最佳分辨率为：600*400<%=img_Path %>
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                内容：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <FCKeditorV2:FCKeditor ID="FCK_PlanTicketContent" ToolbarSet="Default" Height="420px"
                    runat="server">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr id="tr_SendPeople" runat="server">
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                发布人：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <input id="txtSendPeople" readonly="true" runat="server" name="txtSendPeople" type="text"
                    size="20" />
            </td>
        </tr>
        <tr id="tr_SendTime" runat="server">
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                发布时间：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <input id="txtIssuTime" readonly="true" runat="server" name="txtSendPeople" type="text"
                    size="20" />
            </td>
        </tr>
    </table>
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="btn_Save" runat="server" Text="保存" OnClick="btn_Save_Click" />
                <input type="hidden" runat="server" id="hdfAgoImgPath" name="hdfAgoImgPath" />
            </td>
            <td align="center">
            </td>
            <td align="center">
                <asp:Button ID="btn_Cancel" runat="server" Text="取消" OnClick="btn_Cancel_Click" />
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
    var sfu1 =<%=SingleFileUpload1.ClientID %>;
    var isSubmit = false; //区分按钮是否提交过
    //模拟一个提交按钮事件
    function doSubmit(){
    alert("aaaa");
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
                    if(sfu1.getStats().files_queued<=0)
                    {
                        return true;
                    }
        	        sfu1.customSettings.UploadSucessCallback = doSubmit;
                    sfu1.startUpload();     
                    return false;    
            }
            return false;
        });
	    FV_onBlur.initValid($("#form1").get(0));
    });
    </script>

</body>
</html>
