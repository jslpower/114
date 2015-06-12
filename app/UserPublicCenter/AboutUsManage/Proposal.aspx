<%@ Page Title="用户反馈" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Proposal.aspx.cs" Inherits="UserPublicCenter.AboutUsManage.Proposal" ValidateRequest="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="AboutUsHeadControl.ascx" TagName="AboutUsHeadControl" TagPrefix="uc1" %>
<%@ Register Src="AboutUsLeftControl.ascx" TagName="AboutUsLeftControl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <div id="header">
        <uc1:AboutUsHeadControl ID="AboutUsHeadControl1" runat="server" />
    </div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="142" valign="top" style="background: #FFF7D7;">
                <uc2:AboutUsLeftControl ID="AboutUsLeftControl1" runat="server" />
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="818" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="background: #F6F6F6; padding: 5px;">
                            <img src="<%=ImageServerPath %>/images/UserPublicCenter/jy.gif" width="811"
                                height="130px" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                    <tr>
                        <td width="10%" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/companyleft.gif) no-repeat left top;
                            height: 10px;">
                        </td>
                        <td width="90%" style="background: #F6F6F6; border: 1px solid #E5E5E5; border-bottom: 0px;border-left: 0px; text-align:center ">
                            
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background: #F6F6F6;">
                    <tr>
                        <td valign="top" style="padding: 10px 18px 18px 18px; text-align: left; line-height: 24px;
                            height: 250px;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/yijingxiang.gif) no-repeat right bottom;">
                                <tr height="30px">
                                    <td width="22%" align="right">
                                        <font color="red">*</font> 公司名称：
                                    </td>
                                    <td width="78%">
                                        <input type="text" style="border: 1px solid #ccc; width: 400px; height: 17px; color: #009900"
                                            id="txtCompany" runat="server" errmsg="公司名不能为空" valid="required" />
                                        <span id="errMsg_<%=txtCompany.ClientID %>" class="errmsg"></span>
                                    </td>
                                </tr>
                                <tr height="30px">
                                    <td align="right">
                                        <font color="red">*</font>联&nbsp;系&nbsp;人：
                                    </td>
                                    <td>
                                        <input type="text" style="border: 1px solid #ccc; width: 400px; height: 17px; color: #009900"
                                            id="txtComtact" runat="server" errmsg="联系人不能为空" valid="required" />
                                        <span id="errMsg_<%=txtComtact.ClientID %>" class="errmsg"></span>
                                    </td>
                                </tr>
                                <tr height="30px">
                                    <td align="right">
                                        <font color="red">*</font>联系电话：
                                    </td>
                                    <td>
                                        <input type="text" style="border: 1px solid #ccc; width: 400px; height: 17px; color: #009900"
                                            id="txtTelPhone" runat="server" errmsg="电话不能为空|电话号码格式不正确" valid="required|isPhone" />
                                        <span id="errMsg_<%=txtTelPhone.ClientID %>" class="errmsg"></span>
                                    </td>
                                </tr>
                                <tr height="100px">
                                    <td align="right" valign="top">
                                        <font color="red">*</font>填写建议：
                                    </td>
                                    <td>
                                        <textarea style="border: 1px solid #ccc; width: 400px; height: 90px; color: #009900"
                                            id="txtContentText" errmsg="留言内容不能为空！|最多只能输入1000个字!" valid="required|limit" max="1000" min="1" runat="server"></textarea>
                                        <span id="errMsg_<%=txtContentText.ClientID %>" class="errmsg"></span>
                                        <asp:HiddenField ID="hideMobile" runat="server" Value="" />
                                        <asp:HiddenField ID="hideMQ" runat="server" Value="" />
                                        <asp:HiddenField ID="hideQQ" runat="server" Value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        &nbsp;
                                    </td>
                                    <td>
                                            <asp:ImageButton ID="ImgBtn" runat="server" ImageAlign="AbsMiddle" Width="137" Height="41"
                                                OnClientClick="return CheckInput()" OnClick="ImgBtn_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
 
    <input type="hidden" value="" runat="server" id="hidetype" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            FV_onBlur.initValid($("#aspnetForm").get(0));
        });

        function CheckInput() {
            var form = $("#aspnetForm").get(0);
            var b = ValiDatorForm.validator(form, "span");
            return b;
        }
    </script>

</asp:Content>
