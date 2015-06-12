<%@ Page Title="顾问团队申请" Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master"
    AutoEventWireup="true" CodeBehind="ApplicationTeam.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.ApplicationTeam" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/CommonTopicControl.ascx" TagName="CommonTopic"
    TagPrefix="uc1" %>
<%@ Register Src="~/WebControl/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("gongqiu") %>" />
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("body") %>" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <%--访谈介绍开始--%>
                <uc1:CommonTopic runat="server" ID="CommonTopic1" PartText="访谈介绍" PartCss="fangtanhang"
                    TextCss="" />
                <%--访谈介绍结束--%>
                <%--顾问团队开始--%>
                <uc1:CommonTopic runat="server" ID="CommonTopic2" PartText="&nbsp;&nbsp;顾问团队" PartCss="fangtanhang"
                    TextCss="" TopNumber="3" />
                <%--顾问团队结束--%>
                <%--近期访谈回顾开始--%>
                <uc1:CommonTopic runat="server" ID="CommonTopic3" PartText="&nbsp;&nbsp;近期访谈回顾" PartCss="fangtanhang"
                    TextCss="" TopNumber="3" />
                <%--近期访谈回顾结束--%>
                <%--最新行业资讯开始--%>
                <uc1:CommonTopic runat="server" ID="CommonTopic4" PartText="&nbsp;&nbsp;最新行业资讯" PartCss="fangtanhang"
                    TextCss="xuetang" TopNumber="7" />
                <%--最新行业资讯结束--%>
            </td>
            <td valign="top">
                <table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;">
                    <tr>
                        <td width="710" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc;">
                                <tr>
                                    <td class="fangtanhang">
                                        &nbsp;&nbsp;<strong>申请顾问</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="font-size: 14px; text-align: left; padding: 5px 5px 5px 15px;">
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
                                                        <asp:ListItem Text="男" Value="1" Selected="True"></asp:ListItem>
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
                                            </tr>
                                            <tr>
                                                <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                                                    align="right">
                                                    照片上传：
                                                </td>
                                                <td style="font-size: 14px; border-bottom: 1px dashed rgb(221, 221, 221); height: 30px;"
                                                    align="left">
                                                    <uc2:SingleFileUpload runat="server" ID="txtPhoto" ImageWidth="500" ImageHeight="500" />
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
                                                </td>
                                            </tr>
                                        </table>
                                        <p>
                                            &nbsp;</p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
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
                var a = ValiDatorForm.validator($(document.forms[0]).get(0), "span");
                if (a) {
                    if (<%=txtPhoto.ClientID %>.getStats().files_queued > 0) {
                        //如果验证成功，则提交按钮保存事件
                        <%=txtPhoto.ClientID %>.customSettings.UploadSucessCallback = doSubmit;
                        <%=txtPhoto.ClientID %>.startUpload();
                    }
                    else {
                        return true;
                    }
                }
                return false;
            });
            FV_onBlur.initValid($(document.forms[0]).get(0));
        });
    </script>

</asp:Content>
