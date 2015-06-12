<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCommunityAdvisor.aspx.cs"
    Inherits="SiteOperationsCenter.SupplierManage.AddCommunityAdvisor" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagPrefix="cc1" TagName="SingleFileUpload" %>
<%@ Register Src="~/usercontrol/GuestInterviewMenu.ascx" TagPrefix="cc1" TagName="GuestInterviewMenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
    </title>
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:GuestInterviewMenu runat="server" ID="GuestInterviewMenu1"></cc1:GuestInterviewMenu>
    <table style="border: 1px solid rgb(204, 204, 204);" width="100%" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td class="fangtanhang" align="right">
                <a href="/SupplierManage/CommunityAdvisor.aspx">返回列表</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <strong>
                    <asp:Literal runat="server" ID="ltrSmallTitle"></asp:Literal></strong>
            </td>
        </tr>
        <tr>
            <td style="font-size: 14px; text-align: left; padding: 5px 5px 5px 15px;" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            width="24%" align="right">
                            <span class="chengse">*</span> 姓名：
                        </td>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            width="76%" align="left">
                            <input id="txtName" name="txtName" runat="server" style="width: 200px; border: 1px solid rgb(204, 204, 204);
                                height: 17px; font-size: 14px; color: rgb(102, 102, 102); text-align: left; padding-top: 3px;"
                                type="text" valid="required" errmsg="请填写姓名！"><span id="errMsg_<%= txtName.ClientID %>"
                                    class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="right">
                            性别：
                        </td>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="left">
                            <asp:RadioButtonList runat="server" ID="raSex" RepeatDirection="Horizontal">
                                <asp:ListItem Text="男" Value="1"></asp:ListItem>
                                <asp:ListItem Text="女" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="right">
                            单位：
                        </td>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="left">
                            <input runat="server" id="txtCompany" name="txtCompany" style="width: 200px; border: 1px solid rgb(204, 204, 204);
                                height: 17px; font-size: 14px; color: rgb(102, 102, 102); text-align: left; padding-top: 3px;"
                                type="text">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="right">
                            <span class="chengse">*</span> 手机：
                        </td>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="left">
                            <input runat="server" id="txtMobile" name="txtMobile" style="width: 200px; border: 1px solid rgb(204, 204, 204);
                                height: 17px; font-size: 14px; color: rgb(102, 102, 102); text-align: left; padding-top: 3px;"
                                type="text" valid="required|isMobile" errmsg="请填写手机！|请输入正确的手机号码！"><span id="errMsg_<%= txtMobile.ClientID %>"
                                    class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="right">
                            Q&nbsp;&nbsp;Q：
                        </td>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="left">
                            <input runat="server" id="txtQQ" name="txtQQ" style="width: 200px; border: 1px solid rgb(204, 204, 204);
                                height: 17px; font-size: 14px; color: rgb(102, 102, 102); text-align: left; padding-top: 3px;"
                                type="text">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="right">
                            职务：
                        </td>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="left">
                            <input runat="server" id="txtJob" name="txtJob" style="width: 200px; border: 1px solid rgb(204, 204, 204);
                                height: 17px; font-size: 14px; color: rgb(102, 102, 102); text-align: left; padding-top: 3px;"
                                type="text">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="right">
                            照片上传：
                        </td>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="left">
                            <cc1:SingleFileUpload runat="server" ID="txtPhoto" ImageWidth="110" ImageHeight="85" />
                            <asp:Literal runat="server" ID="ltrOldImg"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            valign="top" align="right">
                            主要成就：
                        </td>
                        <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                            align="left">
                            <textarea runat="server" id="txtAchieve" name="txtAchieve" style="width: 400px; height: 100px;
                                border: 1px solid rgb(204, 204, 204); font-size: 14px; color: rgb(102, 102, 102);
                                text-align: left; padding-top: 3px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" height="60">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button runat="server" ID="btnSave" Text="保    存" Width="140" Height="37" BorderStyle="None"
                                Font-Size="Large" OnClick="btnSave_Click" />
                            <input type="hidden" runat="server" id="hdfOldImgPath" name="hdfAgoImgPath" />
                        </td>
                    </tr>
                </table>
                <p>
                    &nbsp;</p>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var sfu1 =<%=txtPhoto.ClientID %>;
        var isSubmit = false; //区分按钮是否提交过
        function doSubmit() {
            isSubmit = true;
            $("#<%=btnSave.ClientID%>").click();
        }
        $(function() {
        $("#<%=btnSave.ClientID %>").click(function() {
                if (isSubmit) {
                    //如果按钮已经提交过一次验证，则返回执行保存操作
                    return true;
                }
                var a = ValiDatorForm.validator($("#form1").get(0), "span");
                if (a) {
                    if (sfu1.getStats().files_queued > 0) {
                        //如果验证成功，则提交按钮保存事件
                        sfu1.customSettings.UploadSucessCallback = doSubmit;
                        sfu1.startUpload();
                    }
                    else {
                        return true;
                    }
                }
                return false;
            });
            FV_onBlur.initValid($("#form1").get(0));
        });
    </script>

    </form>
</body>
</html>
