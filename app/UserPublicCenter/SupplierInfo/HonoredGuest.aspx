<%@ Page Language="C#" AutoEventWireup="true" Title="嘉宾访谈_供求信息" MasterPageFile="~/SupplierInfo/Supplier.Master"
    CodeBehind="HonoredGuest.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.HonoredGuest" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/SWFControl.ascx" TagName="Swf" TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/CommonTopicControl.ascx" TagName="CommonTopic"
    TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/HonoredGuestLeftAndRight.ascx" TagName="HonoredGuestLeft"
    TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="SupplierMain" ID="Test" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("gongqiu") %>" />
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("body") %>" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <uc1:HonoredGuestLeft runat="server" ID="HonoredGuestLeft1" />
            </td>
            <td valign="top">
                <table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;">
                    <tr>
                        <td width="470" valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;">
                                <tr>
                                    <td class="fangtanhang">
                                        &nbsp;&nbsp;<strong>本期讨论话题</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px; text-align: left;">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="magin5">
                                                        <tr>
                                                            <td>
                                                                <%--Falsh广告开始--%>
                                                                <uc1:Swf runat="server" ID="swf1" TopNumber="5" IsShowTitle="false" SwfHeight="181"
                                                                    SwfWidth="455" SwfType="2" />
                                                                <%--Falsh广告结束--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--嘉宾访谈标题与内容开始--%>
                                                <td style="font-size: 14px; line-height: 26px; background: url(<%=ImageServerPath %>/images/UserPublicCenter/line1.gif) repeat-x bottom;">
                                                    <h1 style="text-align: center; padding-top: 15px;">
                                                            <asp:Label runat="server" ID="lbTitle"></asp:Label>
                                                        <%--标题--%>
                                                    </h1>
                                                    <div style="height:120px; overflow:hidden;">
                                                        <asp:Label runat="server" ID="lbContent"></asp:Label>
                                                    </div>
                                                    <%-- 内容 --%>
                                                    <a href="<%= HonoredGuestInfoUrl %>" target="_blank" class="lanse">[详细内容]</a>
                                                </td>
                                                <%--嘉宾访谈标题与内容结束--%>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop15">
                                            <tr>
                                                <td>
                                                    <img src="<%=ImageServerPath %>/images/UserPublicCenter/jbgd.gif" width="144" height="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--观点一开始--%>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop15">
                                                        <tr>
                                                            <td style="font-size: 14px; line-height: 26px; background: url(<%=ImageServerPath %>/images/UserPublicCenter/line1.gif) repeat-x bottom;">
                                                                <div style="float: left;">
                                                                    <img src="<%=ImageServerPath %>/images/UserPublicCenter/jbgd1.gif" width="24" height="23" />
                                                                </div>
                                                                <div style="height:113px; overflow:hidden;">
                                                                    <asp:Label runat="server" ID="OpinionContent1"></asp:Label>
                                                                </div>
                                                                <a href="<%= HonoredGuestInfoUrl %>" target="_blank" class="lanse">[详细内容]</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--观点一结束--%>
                                                    <%--观点二开始--%>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop15">
                                                        <tr>
                                                            <td style="font-size: 14px; line-height: 26px; background: url(<%=ImageServerPath %>/images/UserPublicCenter/line1.gif) repeat-x bottom;">
                                                                <div style="float: left;">
                                                                    <img src="<%=ImageServerPath %>/images/UserPublicCenter/jbgd2.gif" width="24" height="23" /></div>
                                                                <div style="height:113px; overflow:hidden;">
                                                                <asp:Label runat="server" ID="OpinionContent2"></asp:Label>
                                                                </div>
                                                                <a href="<%= HonoredGuestInfoUrl %>" target="_blank" class="lanse">[详细内容]</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--观点二结束--%>
                                                    <%--观点三开始--%>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop15">
                                                        <tr>
                                                            <td style="font-size: 14px; line-height: 26px;">
                                                                <div style="float: left;">
                                                                    <img src="<%=ImageServerPath %>/images/UserPublicCenter/jbgd3.gif" width="24" height="23" /></div>
                                                                <div style="height:113px; overflow:hidden;">
                                                                <asp:Label runat="server" ID="OpinionContent3"></asp:Label>
                                                                </div>
                                                                <a href="<%= HonoredGuestInfoUrl %>" target="_blank" class="lanse">[详细内容]</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--观点三结束--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <%--小编总结开始--%>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 2px solid #ccc;">
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="border: 1px solid #B7B7B7; background: #EEEEEE; padding: 4px; line-height: 24px;
                                                                font-size: 14px;">
                                                                <strong>小编总结: </strong>
                                                                <div style="height:150px; overflow:hidden;" style="float:left;">
                                                                    <asp:Label  runat="server" ID="Summary"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <a href="<%= HonoredGuestInfoUrl %>" target="_blank"><span class="lanse">[详细]</span></a>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <%--小编总结结束--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10">
                        </td>
                        <td width="230" valign="top">
                            <%--同业交流专区开始--%>
                            <uc1:CommonTopic runat="server" ID="CommonTopic5" PartText="同业交流专区" PartCss="fangtanhang"
                                TextCss="" />
                            <%--同业交流专区结束--%>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10"
                                style="border: 1px solid #ccc;">
                                <tr>
                                    <td width="82%" class="fangtanhang">
                                        &nbsp;&nbsp;<strong>评论</strong>
                                    </td>
                                    <td width="18%" class="fangtanhang">
                                        <asp:Literal runat="server" ID="ltrMoreComment"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top" style="font-size: 14px; text-align: left; padding: 2px;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#F3F3F8">
                                            <tr>
                                                <td align="center">
                                                    <textarea name="txt_content" id="txt_content" style="border: 1px solid #ccc; height: 90px;
                                                        width: 210px; overflow-y: hidden;">请输入评论内容！</textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#F3F3F8" style="padding-top: 5px;">
                                                    <div id="UserInfo">
                                                        &nbsp;&nbsp;用户名：
                                                        <input type="text" id="u" size="10" style="width: 100px; border: 1px solid #ccc;
                                                            height: 16px; font-size: 12px; color: #666;  padding-top: 0px;" />
                                                        <span id="err_uname" style="color: Red; display: none;">*</span>
                                                        <br />
                                                        <input type="hidden" id="vc" name="vc" />
                                                        &nbsp;&nbsp;密&nbsp;&nbsp;码：
                                                        <input id="p" type="password" style="width: 100px; border: 1px solid #ccc; height: 16px;
                                                            font-size: 12px; color: #666;  padding-top: 0px;" />
                                                        <span id="err_pass" style="color: Red; display: none;">*</span>
                                                    </div>
                                                    <div style="margin-top: 10px;">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px dashed #ccc;
                                                            padding-bottom: 5px;">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                    <asp:CheckBox runat="server" ID="IsAnonymous" Text="匿名" />
                                                                </td>
                                                                <td>
                                                                    <img style="cursor:pointer;" src="<%=ImageServerPath %>/images/UserPublicCenter/fabiaoan.gif" onclick="ValidData(this)" />
                                                                    <asp:Button runat="server" ID="btnSave" Style="display: none;" OnClick="btnSave_Click" />
                                                                </td>
                                                                <td>
                                                                    <a href="/Register/CompanyUserRegister.aspx" id="register">新用户注册</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Repeater runat="server" ID="RpComment">
                                            <ItemTemplate>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                                                    <tr>
                                                        <td height="66" class="lv" style="border-bottom: 1px solid #ddd;">
                                                            网友：<%# bool.Parse(Eval("IsAnonymous").ToString()) ? "匿名" : Eval("OperatorName")%>
                                                            于
                                                            <%# Eval("IssueTime","{0:yyyy-MM-dd}") %>
                                                            评论道：<br />
                                                            <span class="huise">
                                                                <%# Eval("CommentText")%></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:HiddenField runat="server" ID="hGuestId" Value="201b0c60-bde8-4817-bf5e-f6ed65d8bc0c" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("blogin") %>"></script>

    <script type="text/javascript">
        var islogin = "<%= IsLogin %>";
        $(function() {
            if (islogin == "True") {
                $("#UserInfo").hide();
                $("#register").hide();
            }

            $("#txt_content").focus(function() {
                var content = $.trim($("#txt_content").val());
                if (content == "请输入评论内容！") {
                    $("#txt_content").val("");
                }
            });

            $("#txt_content").blur(function() {
                var content = $.trim($("#txt_content").val());
                if (content == "") {
                    $("#txt_content").val("请输入评论内容！");
                }
            });
        });
        function ValidData(obj) {
            var content = $.trim($("#txt_content").val());
            if (content == "请输入评论内容！") {
                return false;
            }
            if (islogin == "False") {
                var u = $.trim($("#u").val());
                var p = $.trim($("#p").val());
                $("#err_uname").hide();
                $("#err_pass").hide();
                if (u == "") {
                    $("#err_uname").show();
                    return false;
                }
                if (p == "") {
                    $("#err_pass").show();
                    return false;
                }
                blogin.ssologinurl = "<%=EyouSoft.Common.Domain.PassportCenter %>";
                blogin3(u, p, function(message) {
                    if (message == "1") //验证通过
                    {
                        $("#<%= btnSave.ClientID %>").click();
                    }
                    else {
                        alert(message);
                    }
                });
            }
            else {
                $("#<%= btnSave.ClientID %>").click();
            }
        }
    </script>

</asp:Content>
