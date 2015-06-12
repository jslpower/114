<%@ Page Language="C#" MasterPageFile="~/SupplierInfo/SupplierNew.Master"
    AutoEventWireup="true" CodeBehind="ExchangeInfoOld.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.ExchangeInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/ExchangeLeft.ascx" TagName="ExchangeLeft"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierBody" runat="server">


    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>

    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <uc1:ExchangeLeft runat="server" ID="ExchangeLeft1" />
            </td>
            <td valign="top">
                <table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;">
                    <tr>
                        <td valign="top" style="border: 1px solid #C4C4C4; padding: 1px;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding: 10px;
                                background: url(<%= ImageServerPath %>/images/UserPublicCenter/bg_new.gif) repeat-x;">
                                <tr>
                                    <td style="text-align: center;">
                                        <h1>
                                            <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="border-bottom: 1px solid #ccc; height: 40px;">
                                        <span class="huise">
                                            <asp:Literal runat="server" ID="ltrTime"></asp:Literal></span>&nbsp;&nbsp;<asp:Literal
                                                runat="server" ID="ltrCompanyName"></asp:Literal><asp:Literal runat="server" ID="ltrMQ"></asp:Literal>
                                        &nbsp;&nbsp;<span class="chengse"><strong><a href="#d1"><asp:Literal runat="server"
                                            ID="ltrCommentCount"></asp:Literal></a><asp:Literal runat="server" ID="ltrViewCount"></asp:Literal></strong></span>
                                        &nbsp;&nbsp;<a href="javascript:void(0);" onclick="AddFavor('<%= ExchangeId %>');return false;"><img
                                            src="<%= ImageServerPath %>/images/UserPublicCenter/20090716hf.gif" width="69"
                                            height="22" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="heise" style="padding-top: 15px;">
                                        <div style="text-align: center">
                                            <asp:Repeater runat="server" ID="rptExchangeImgList">
                                                <ItemTemplate>
                                                    <%# string.IsNullOrEmpty(Eval("ImgPath").ToString()) ? string.Empty : "<a target=\"_blank\" href=\"" + Domain.FileSystem + Eval("ImgPath").ToString() + "\"><img src=\"" + Domain.FileSystem + Eval("ImgPath").ToString() + "\" width=\"144\" height=\"89\" /></a>"%>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <br />
                                        <asp:Literal runat="server" ID="ltrInfo"></asp:Literal>
                                        <br />
                                        <span class="heise" style="padding-top: 15px;">&nbsp;&nbsp;&nbsp;</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; font-size: 12px; color: #666; background: #E3ECF5; height: 22px;
                                        padding-left: 10px;">
                                        <asp:Literal runat="server" ID="ltrDownLoad"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- 评论Start --%>
                                        <div id="divComment">
                                        </div>
                                        <div id="DivPage" class="digg">
                                        </div>
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
                                                                <input id="isLogin" type="hidden" value="<%=GetIsLogin() %>" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <%-- 评论End --%>
                                        <%-- 最新供需Start --%>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;
                                            margin: 10px 0 10px 0;">
                                            <tr>
                                                <td style="background: url(<%= ImageServerPath %>/images/UserPublicCenter/gqplhang.gif) repeat-x;
                                                    height: 29px; font-size: 14px; padding-left: 10px; text-align: left;">
                                                    <strong>最新供需</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="liststyle"
                                                        style="margin: 10px 0 10px 0;">
                                                        <asp:Repeater runat="server" ID="rptNewExchange">
                                                            <ItemTemplate>
                                                                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                                                                    <td width="74%" height="24" class="lan14">
                                                                        <%# GetTagHtml((EyouSoft.Model.CommunityStructure.Tag)Eval("ExchangeTagHtml"))%>
                                                                        <div style="float: left;">
                                                                            <a href='/SupplierInfo/ExchangeList.aspx?pid=<%# Eval("ProvinceId") %>&CityId=<%# base.CityId %>'>
                                                                                <%# GetProvinceNameById(Eval("ProvinceId").ToString())%></a><a href="<%# EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Eval("ID").ToString(), CityId) %>"><%# Utils.GetText(Eval("ExchangeTitle").ToString(), 24)%></a>
                                                                        </div>
                                                                    </td>
                                                                    <td width="4%" class="huise">
                                                                        <%# Eval("IssueTime", "{0:MM.dd}")%>
                                                                    </td>
                                                                    <td width="15%" class="huise">
                                                                        <%# Utils.GetText(Eval("CompanyName").ToString(), 8)%>
                                                                    </td>
                                                                    <td width="7%">
                                                                        <%# GetMQUrl(Eval("OperatorMQ").ToString())%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr bgcolor="#EFEFEF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                                                                    <td width="74%" height="24" class="lan14">
                                                                        <%# GetTagHtml((EyouSoft.Model.CommunityStructure.Tag)Eval("ExchangeTagHtml")) %>
                                                                        <div style="float: left;">
                                                                            <a href='/SupplierInfo/ExchangeList.aspx?pid=<%# Eval("ProvinceId") %>&CityId=<%# base.CityId %>'>
                                                                                <%# GetProvinceNameById(Eval("ProvinceId").ToString()) %></a><a href="<%# EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Eval("ID").ToString(), CityId) %>"><%# Utils.GetText(Eval("ExchangeTitle").ToString(), 24)%></a>
                                                                        </div>
                                                                    </td>
                                                                    <td width="4%" class="huise">
                                                                        <%# Eval("IssueTime","{0:MM.dd}")%>
                                                                    </td>
                                                                    <td width="15%" class="huise">
                                                                        <%# Utils.GetText(Eval("CompanyName").ToString(), 8) %>
                                                                    </td>
                                                                    <td width="7%">
                                                                        <%# GetMQUrl(Eval("OperatorMQ").ToString()) %>
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <%-- 最新供需End --%>
                                        <%-- 同类其他供需Start --%>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;
                                            margin: 10px 0 10px 0;">
                                            <tr>
                                                <td style="background: url(<%= ImageServerPath %>/images/UserPublicCenter/gqplhang.gif) repeat-x;
                                                    height: 29px; font-size: 14px; padding-left: 10px; text-align: left;">
                                                    <strong>同类其他供需</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="liststyle"
                                                        style="margin: 10px 0 10px 0;">
                                                        <asp:Repeater runat="server" ID="rptOtherExchange">
                                                            <ItemTemplate>
                                                                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                                                                    <td width="74%" height="24" class="lan14">
                                                                        <%# GetTagHtml((EyouSoft.Model.CommunityStructure.Tag)Eval("ExchangeTagHtml"))%>
                                                                        <div style="float: left;">
                                                                            <a href='/SupplierInfo/ExchangeList.aspx?pid=<%# Eval("ProvinceId") %>&CityId=<%# base.CityId %>'>
                                                                                <%# GetProvinceNameById(Eval("ProvinceId").ToString())%></a><a href="<%# EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Eval("ID").ToString(), CityId) %>"><%# Utils.GetText(Eval("ExchangeTitle").ToString(), 24)%></a>
                                                                        </div>
                                                                    </td>
                                                                    <td width="4%" class="huise">
                                                                        <%# Eval("IssueTime", "{0:MM.dd}")%>
                                                                    </td>
                                                                    <td width="15%" class="huise">
                                                                        <%# Utils.GetText(Eval("CompanyName").ToString(), 8)%>
                                                                    </td>
                                                                    <td width="7%">
                                                                        <%# GetMQUrl(Eval("OperatorMQ").ToString())%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr bgcolor="#EFEFEF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                                                                    <td width="74%" height="24" class="lan14">
                                                                        <%# GetTagHtml((EyouSoft.Model.CommunityStructure.Tag)Eval("ExchangeTagHtml"))%>
                                                                        <div style="float: left;">
                                                                            <a href='/SupplierInfo/ExchangeList.aspx?pid=<%# Eval("ProvinceId") %>&CityId=<%# base.CityId %>'>
                                                                                <%# GetProvinceNameById(Eval("ProvinceId").ToString())%></a><a href="<%# EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Eval("ID").ToString(), CityId) %>"><%# Utils.GetText(Eval("ExchangeTitle").ToString(), 24)%></a>
                                                                        </div>
                                                                    </td>
                                                                    <td width="4%" class="huise">
                                                                        <%# Eval("IssueTime", "{0:MM.dd}")%>
                                                                    </td>
                                                                    <td width="15%" class="huise">
                                                                        <%# Utils.GetText(Eval("CompanyName").ToString(), 8)%>
                                                                    </td>
                                                                    <td width="7%">
                                                                        <%# GetMQUrl(Eval("OperatorMQ").ToString())%>
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <%-- 同类其他供需End --%>
                                        <%-- 您查看过的供需信息Start --%>
                                        <asp:Panel ID="tabSeeExchange" runat="server" Visible="false">
                                            <table width="100%" visible="false" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;
                                                margin: 10px 0 10px 0;">
                                                <tr>
                                                    <td style="background: url(<%= ImageServerPath %>/images/UserPublicCenter/gqplhang.gif) repeat-x;
                                                        height: 29px; font-size: 14px; padding-left: 10px; text-align: left;">
                                                        <strong>您查看过的供需信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="liststyle"
                                                            style="margin: 10px 0 10px 0;">
                                                            <asp:Repeater runat="server" ID="rptSeeExchange">
                                                                <ItemTemplate>
                                                                    <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                                                                        <td width="74%" height="24" class="lan14">
                                                                            <%# GetTagHtml((EyouSoft.Model.CommunityStructure.Tag)Eval("ExchangeTagHtml"))%>
                                                                            <div style="float: left;">
                                                                                <a href='/SupplierInfo/ExchangeList.aspx?pid=<%# Eval("ProvinceId") %>&CityId=<%# base.CityId %>'>
                                                                                    <%# GetProvinceNameById(Eval("ProvinceId").ToString())%></a><a href="<%# EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Eval("ID").ToString(), CityId) %>"><%# Utils.GetText(Eval("ExchangeTitle").ToString(), 24)%></a>
                                                                            </div>
                                                                        </td>
                                                                        <td width="4%" class="huise">
                                                                            <%# Eval("IssueTime", "{0:MM.dd}")%>
                                                                        </td>
                                                                        <td width="15%" class="huise">
                                                                            <%# Utils.GetText(Eval("CompanyName").ToString(), 8)%>
                                                                        </td>
                                                                        <td width="7%">
                                                                            <%# GetMQUrl(Eval("OperatorMQ").ToString())%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr bgcolor="#EFEFEF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                                                                        <td width="74%" height="24" class="lan14">
                                                                            <%# GetTagHtml((EyouSoft.Model.CommunityStructure.Tag)Eval("ExchangeTagHtml"))%>
                                                                            <div style="float: left;">
                                                                                <a href='/SupplierInfo/ExchangeList.aspx?pid=<%# Eval("ProvinceId") %>&CityId=<%# base.CityId %>'>
                                                                                    <%# GetProvinceNameById(Eval("ProvinceId").ToString())%></a><a href="<%# EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Eval("ID").ToString(), CityId) %>"><%# Utils.GetText(Eval("ExchangeTitle").ToString(), 24)%></a>
                                                                            </div>
                                                                        </td>
                                                                        <td width="4%" class="huise">
                                                                            <%# Eval("IssueTime", "{0:MM.dd}")%>
                                                                        </td>
                                                                        <td width="15%" class="huise">
                                                                            <%# Utils.GetText(Eval("CompanyName").ToString(), 8)%>
                                                                        </td>
                                                                        <td width="7%">
                                                                            <%# GetMQUrl(Eval("OperatorMQ").ToString())%>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%-- 您查看过的供需信息End --%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
    <table id="CommentInfo" style="display: none;" width="100%" border="0" cellspacing="0"
        cellpadding="0">
        <tr>
            <td>
                <textarea runat="server" id="txtCommentInfo1" name="txtCommentInfo1" style="height: 150px;
                    width: 630px; border: 1px solid #666;"></textarea>
            </td>
        </tr>
        <tr>
            <td height="40" align="left" style="padding-left: 50px;">
                <img alt="提交" id="save" src="<%= Domain.ServerComponents %>/images/UserPublicCenter/20090716tijiao.gif"
                    width="69" height="22" onclick="ClientSaveComment1()" />
                <span class="huise">[评论内容不能超过500个字符]</span>
            </td>
        </tr>
    </table>
    <%--盖楼回复结束--%>

    <script language="JavaScript" type="text/javascript">

        function mouseovertr(o) {
            o.style.backgroundColor = "#E2EDFF";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
        
    </script>

    <script type="text/javascript" language="JavaScript">
    
        function show() {
            Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetReturnUrl() %>", width: "400px", height: "250px", modal: true });
        }
        
        function AddFavor(infoid) {
            var isLogin = $("#isLogin").val();
            if (isLogin=="True") {
                if (infoid != "") {
                    $.ajax({
                        type: "GET",
                        cache: false,
                        url: "/SupplierInfo/Ashx/FavorExchange.ashx",
                        data: "Id=" + infoid,
                        success: function(msg) {
                            var strErr = "";
                            if (msg == "1")
                                strErr = "已设为关注！";
                            else if (msg == "2")
                                strErr = "操作失败！";
                            else if (msg == "0")
                                strErr = "请先登录！";
                            alert(strErr);
                        }
                    });
                }
            } else {
                show();
            }
        }

        $(document).ready(function() {
         showDialog();
         function showDialog(){
           var boolIsLogin = $("#isLogin").val();
            if (boolIsLogin == "False") {
                show();
            } 
        }
            LoadCommentList("<%= ExchangeId %>", "1");
        });
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

        function ClientSaveComment() {
            var strInfo = $("#<%= txtCommentInfo.ClientID %>").val();
            var strErr = "";
            var boolIsLogin = $("#isLogin").val();
            if (boolIsLogin == "False") {
                show();
            } else {
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
            return false;
        }
        
        

        function LoadCommentList(infoid, pageIndex) {
            var cityid="<%= base.CityId %>";
            $("#divComment").html("<img id=\"img_loading\" src='\<%= ImageServerPath %>/images/loadingnew.gif\' border=\"0\" /><br />&nbsp;正在加载...&nbsp;");
            $.ajax({
                type: "GET",
                cache: false,
                async: false,
                url: "/SupplierInfo/Ashx/AjaxCommentPage.ashx",
                data: "cityid="+cityid+"&TopicType=1&TopicId=" + encodeURI(infoid) + "&pageIndex=" + pageIndex,
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

    <script type="text/javascript">
        function gotoComment(CommentId, num) {
            var IsLogin = $("#isLogin").val();
            if (IsLogin == "True") {
                if (CommentId == "")
                    return false;
                $("#hidCommentId").val(CommentId);
                var boxy = new Boxy($("#CommentInfo"), { title: "回复" + num + "楼：", width: "800", height: "180", draggable: false, modal: true,
                    afterHide: function() { $("#hidCommentId").val(""); $("#<%= txtCommentInfo1.ClientID %>").val(""); }
                });
            } else {
                show();
            }
        }
    </script>
   
</asp:Content>
