<%@ Page Title="嘉宾访谈评论" Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master"
    AutoEventWireup="true" CodeBehind="HonoredGuestComment.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.HonoredGuestComment" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/HonoredGuestLeftAndRight.ascx" TagName="HonoredGuestLeft"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("gongqiu") %>" />
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("body") %>" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>

    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <uc1:HonoredGuestLeft runat="server" ID="HonoredGuestLeft1" />
            </td>
            <td width="10">
            </td>
            <td valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc;">
                                <tr>
                                    <td class="fangtanhang">
                                        &nbsp;&nbsp;<strong>评论区域</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="font-size: 14px; text-align: left; padding: 5px;">
                                        <div id="divComment">
                                        </div>
                                        <div id="DivPage" class="digg">
                                        </div>
                                        <asp:Panel runat="server" ID="panSaveComment" Visible="false">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;">
                                                <tr>
                                                    <td style="background: url(<%= ImageServerPath %>/images/UserPublicCenter/gqplhang.gif) repeat-x;
                                                        height: 29px; font-size: 14px; padding-left: 10px; text-align: left;">
                                                        <strong>发表评论</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="background: #E7EFF8; text-align: left;">
                                                                    &nbsp;用户名：<asp:Literal runat="server" ID="ltrUserName"></asp:Literal>
                                                                    <asp:CheckBox runat="server" ID="ckbIsAnonymous" Text="匿名" />
                                                                    <span class="huise">理智评论文明上网，拒绝恶意谩骂</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <textarea runat="server" id="txtCommentInfo" name="txtCommentInfo" style="height: 150px;
                                                                        width: 630px; border: 1px solid #666;"></textarea>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="40" align="left" style="padding-left: 50px;">
                                                                    <input type="hidden" id="hidCommentId" name="hidCommentId" value="" />
                                                                    <asp:ImageButton runat="server" ID="ibtnSave" Width="69" Height="22" OnClick="ibtnSave_Click"
                                                                        OnClientClick="return ClientSaveComment();" />
                                                                    <span class="huise">[评论内容不能超过500个字符]</span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--盖楼回复开始--%>
    <table id="CommentInfo" style="display:none;" width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td>
            <textarea runat="server" id="txtCommentInfo1" name="txtCommentInfo1" style="height: 150px;
                width: 630px; border: 1px solid #666;"></textarea>
        </td>
    </tr>
    <tr>
        <td height="40" align="left" style="padding-left: 50px;">
            <img alt="提交" id="save" src="<%= Domain.ServerComponents %>/images/UserPublicCenter/20090716tijiao.gif" width="69" height="22" onclick="ClientSaveComment1()" />
            <span class="huise">[评论内容不能超过500个字符]</span>
        </td>
    </tr>
    </table>
    <%--盖楼回复结束--%>
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            LoadCommentList("<%= HonoredGuestId %>", "1");
        });
        //-----------------回复盖楼开始---------------------//
        function ClientSaveComment1() {
            var strInfo = $("#<%= txtCommentInfo1.ClientID %>").val();
            var strErr = "";
            if (strInfo == "" || strInfo.length > 500)
                strErr += "回复内容不能为空且应在500字符以内！\n";
            if (strErr != "") {
                alert(strErr);
                return false;
            }
            else {
                $("#<%= txtCommentInfo.ClientID %>").val(strInfo);
                $("#save").hide();
                $("#<%= ibtnSave.ClientID %>").click();
                return false;
            }
        }
        //-----------------回复盖楼结束---------------------//
        function ClientSaveComment() {
            var strInfo = $("#<%= txtCommentInfo.ClientID %>").val();
            var strCommentId = $("#hidCommentId").val();
            var strErr = "";
            if (strInfo == "" || strInfo.length > 500)
                strErr += "回复内容不能为空且应在500字符以内！\n";
            if (strErr != "") {
                alert(strErr);
                return false;
            }
            else {
                $("#<%= ibtnSave.ClientID %>").hide();
                return true;
            }
        }

        function LoadCommentList(infoid, pageIndex) {
            $("#divComment").html("<img id=\"img_loading\" src='\<%= ImageServerPath %>/images/loadingnew.gif\' border=\"0\" /><br />&nbsp;正在加载...&nbsp;");
            $.ajax({
                type: "GET",
                cache: false,
                async: false,
                url: "/SupplierInfo/Ashx/AjaxCommentPage.ashx",
                data: "TopicType=2&TopicId=" + encodeURI(infoid) + "&pageIndex=" + pageIndex + "&CityId=<%= CityId %>",
                success: function(CommentHTML) {
                    $("#divComment").html(CommentHTML);
                }
            });
            var config = {
                pageSize: parseInt($("#hSize").val()),
                pageIndex: parseInt($("#hIndex").val()),
                recordCount: parseInt($("#hRecordCount").val()),
                pageCount: 0,
                gotoPageFunctionName: 'AjaxPageControls.gotoPage',
                showPrev: true,
                showNext: true
            };
            AjaxPageControls.replace("DivPage", config);
            AjaxPageControls.gotoPage = function(pIndex) {
                LoadCommentList(infoid, pIndex);
            }
        }
    </script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    <script type="text/javascript">
        function gotoComment(CommentId,num) {
            if (CommentId == "")
                return false;
            $("#hidCommentId").val(CommentId);
            var boxy= new Boxy($("#CommentInfo"),{ title: "回复"+num+"楼：",width: "800", height:"180", draggable: false,modal:true,
            afterHide:function(){$("#hidCommentId").val(""); $("#<%= txtCommentInfo1.ClientID %>").val("");}});
        }
    </script>
</asp:Content>
