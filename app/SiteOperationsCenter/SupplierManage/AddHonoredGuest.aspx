<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddHonoredGuest.aspx.cs"
    Inherits="SiteOperationsCenter.SupplierManage.AddHonoredGuest" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/GuestInterviewMenu.ascx" TagPrefix="cc1" TagName="GuestInterviewMenu" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagPrefix="cc1" TagName="SingleFileUpload" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:GuestInterviewMenu runat="server" ID="GuestInterviewMenu1"></cc1:GuestInterviewMenu>
    <table width="100%" border="1" bordercolor="#bde1f4" cellpadding="5" cellspacing="0">
        <tr>
            <td align="right" bgcolor="#d9eef9">
                <span class="unnamed1">* </span>嘉宾访谈标题：
            </td>
            <td align="left">
                <input runat="server" id="txtTitle" name="txtTitle" type="text" size="100" valid="required"
                    errmsg="请输入标题！"><span id="errMsg_txtTitle" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td width="10%" align="right" bgcolor="#d9eef9">
                嘉宾banner：
            </td>
            <td width="90%" align="left">
                <cc1:SingleFileUpload runat="server" ID="txtBanner" ImageWidth="455" ImageHeight="181" />
                <asp:Literal runat="server" ID="ltrOldBanner"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#d9eef9">
                嘉宾小头像：
            </td>
            <td align="left">
                <cc1:SingleFileUpload runat="server" ID="txtSmallImg" ImageWidth="50" ImageHeight="50" />
                <asp:Literal runat="server" ID="ltrOldSmallImg"></asp:Literal>
            </td>
        </tr>
    </table>
    <table width="99%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background: none repeat scroll 0% 0% rgb(217, 238, 249); padding: 5px;
                font-weight: bold; color: rgb(204, 0, 51);" height="15">
                &nbsp;&nbsp;&gt;&gt; 嘉宾介绍
            </td>
        </tr>
        <tr>
            <td height="15">
                <span style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <FCKeditorV2:FCKeditor runat="server" ID="txtHonoredGuest" Height="300">
                    </FCKeditorV2:FCKeditor>
                </span>
            </td>
        </tr>
        <tr>
            <td style="background: none repeat scroll 0% 0% rgb(217, 238, 249); padding: 5px;
                font-weight: bold; color: rgb(204, 0, 51);" height="15">
                &nbsp;&nbsp;&gt;&gt; 观点1
            </td>
        </tr>
        <tr>
            <td style="padding: 5px; font-weight: bold; color: rgb(204, 0, 51);" height="15">
                <span style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <FCKeditorV2:FCKeditor runat="server" ID="txtView1" Height="300">
                    </FCKeditorV2:FCKeditor>
                </span>
            </td>
        </tr>
        <tr>
            <td style="background: none repeat scroll 0% 0% rgb(217, 238, 249); padding: 5px;
                font-weight: bold; color: rgb(204, 0, 51);" height="15">
                &nbsp;&nbsp;&gt;&gt; 观点2
            </td>
        </tr>
        <tr>
            <td style="padding: 5px; font-weight: bold; color: rgb(204, 0, 51);" height="15">
                <span style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <FCKeditorV2:FCKeditor runat="server" ID="txtView2" Height="300">
                    </FCKeditorV2:FCKeditor>
                </span>
            </td>
        </tr>
        <tr>
            <td style="background: none repeat scroll 0% 0% rgb(217, 238, 249); padding: 5px;
                font-weight: bold; color: rgb(204, 0, 51);" height="15">
                &nbsp;&nbsp;&gt;&gt; 观点3
            </td>
        </tr>
        <tr>
            <td style="padding: 5px; font-weight: bold; color: rgb(204, 0, 51);" height="15">
                <span style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <FCKeditorV2:FCKeditor runat="server" ID="txtView3" Height="300">
                    </FCKeditorV2:FCKeditor>
                </span>
            </td>
        </tr>
        <tr>
            <td style="background: none repeat scroll 0% 0% rgb(217, 238, 249); padding: 5px;
                font-weight: bold; color: rgb(204, 0, 51);" height="15">
                &nbsp;&nbsp;&gt;&gt; 小编总结
            </td>
        </tr>
        <tr>
            <td height="15">
                <span style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <FCKeditorV2:FCKeditor runat="server" ID="txtSummary" Height="300">
                    </FCKeditorV2:FCKeditor>
                </span>
            </td>
        </tr>
        <tr>
            <td height="30">
                <table width="25%" align="center" border="0" cellpadding="0" cellspacing="0" height="30">
                    <tr>
                        <td align="center">
                            <asp:Button runat="server" ID="btnSave" Text="保 存" CssClass="baocun_an" OnClick="btnSave_Click" />
                        </td>
                        <td align="center">
                        </td>
                        <td align="center">
                            <input name="btnCancel" class="baocun_an" value="取消" type="button" onclick="javascript:location.href='/SupplierManage/HonoredGuest.aspx'">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" runat="server" id="hdfOldOldBanner" name="hdfOldOldBanner" />
    <input type="hidden" runat="server" id="hdfOldSmallImg" name="hdfOldSmallImg" />

    <script type="text/javascript" type="text/javascript">
    var sfu1 = <%= txtBanner.ClientID %>;
    var sfu2 = <%= txtSmallImg.ClientID %>;
    var isSubmit = false; //区分按钮是否提交过
    //模拟一个提交按钮事件
    function doSubmit(){
        isSubmit = true;
         $("#<%=btnSave.ClientID%>").click();
    }
    function UpLoadSmallImg()
    {
        if(sfu2.getStats().files_queued > 0)
        {
            sfu2.startUpload();
            sfu2.customSettings.UploadSucessCallback = doSubmit;
        }
        else
        {
            doSubmit();
        }
    }
    $(function(){
        $("#<%=btnSave.ClientID%>").click(function(){
            if(isSubmit){
            //如果按钮已经提交过一次验证，则返回执行保存操作
                return true;
            }
	        var a= ValiDatorForm.validator($("#form1").get(0),"span");
	        if(a){
	        //如果验证成功，则提交按钮保存事件
                    if(sfu1.getStats().files_queued > 0)
                    {
                        sfu1.startUpload();
                        sfu1.customSettings.UploadSucessCallback = UpLoadSmallImg;
                    }
                    else
                    {
                        UpLoadSmallImg();
                    }
                    return false;    
            }
            return false;
        });
	    FV_onBlur.initValid($("#form1").get(0));
    });
    </script>

    </form>
</body>
</html>
