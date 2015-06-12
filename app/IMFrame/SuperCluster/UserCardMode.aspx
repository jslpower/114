<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCardMode.aspx.cs" Inherits="IMFrame.SuperCluster.UserCardMode" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Src="MQCardUploadControl.ascx" TagName="MQCardUploadControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("swfupload") %>"></script>

</head>
<style>
    body
    {
        font-size: 9pt;
    }
    a
    {
        color: #828282;
        text-decoration: none;
    }
    a:hover
    {
        color: #F30;
    }
    a img
    {
        border: 0;
    }
    ul, li
    {
        margin: 0;
        padding: 0;
        list-style: none;
    }
    #tags ul
    {
        width: 100%;
        clear: both;
        width: 100%;
        height: 26px;
        border-bottom: 1px solid #d3d3d3;
        margin-bottom: 10px;
    }
    .clear
    {
        clear: both;
        width: 100%;
        margin: 0;
        padding: 0;
        overflow: hidden;
        height: 0px;
    }
    #tags li
    {
        float: left;
        width: 96px;
        height: 26px;
        text-align: center;
        line-height: 26px;
        position: relative;
        top: 1px;
    }
    #tags li a
    {
        display: block;
    }
    #tags li.selectTag
    {
        background: url(<%=ImageServerUrl%>/IM/images/bgg.gif) no-repeat left top;
        position: relative;
        top: 1px;
    }
</style>
<body style="margin: 0; padding: 0; border: 0;">
    <form id="form1" runat="server">
    <input type="hidden" id="companyId_hidden" runat="server" />
    <input type="hidden" id="logoPath" runat="server" />
    <div class="hr-5">
    </div>
    <div id="listContent">
        <div id="tags">
            <ul>
                <li class="selectTag"><a href="<%=ModeUrl%>">名片模式</a> </li>
                <li><a href="<%=ListUrl%>">列表模式</a> </li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <%-- width="600px"--%>
        <asp:PlaceHolder ID="place_holder" runat="server">
            <ul style="margin: 0; padding: 0; list-style: none; color: #828282; font-size: 12px;">
                <li style="width: 261px; height: 154px; padding: 0; margin: 0; border-top: 3px solid #1cade0;
                    border-bottom: 2px solid #ccd0d3; overflow: hidden; float: left; margin-right: 13px;
                    margin-bottom: 13px; position: relative">
                    <div style="border: 1px solid #a8c5d9; width: 243px; height: 144px; padding: 8px;
                        padding-top: 5px;">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; line-height: 22px;">
                            <tr>
                                <td>
                                    姓名：<span style="color: #0c5282"><%=EyouSoft.Common.Utils.GetText(SelfModel.UserName, 14)%></span>
                                    <span>
                                        <%=EyouSoft.Common.Utils.GetMQ(SelfModel.MQ.ToString())%></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    公司：<%=EyouSoft.Common.Utils.GetText(SelfModel.CompanyName,17)%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    主营：<%=EyouSoft.Common.Utils.GetText(SelfModel.Subject,17)%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    联系我们：<%=SelfModel.Contact%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 100%; border-top: 1px dashed #ccc; border-bottom: 1px dashed #ccc;
                                        padding-top: 5px; overflow: hidden; height: 42px; margin-top: 4px;">
                                        <img style="border: 1px solid #dce3eb" src="<%=GetCompanyLogoSrc(SelfModel.CompanyLogo)%>"
                                            width="126px" height="35px">
                                        <div style="position: absolute; left: 145px; bottom: 8px;">
                                            <uc1:MQCardUploadControl ID="MQCardUploadControl1" runat="server" JsMethodfileQueued="fileQueued1" />
                                            <br />
                                            <%=IsShowWebStore(SelfModel.EshopUrl)%>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </li>
            </ul>
        </asp:PlaceHolder>
        <ul style="margin: 0; padding: 0; list-style: none; color: #828282; font-size: 12px;">
            <cc2:CustomRepeater ID="Repeater1" runat="server" EmptyText="<tr ><td colspan=5>无数据显示!</td></tr>">
                <ItemTemplate>
                    <li style="width: 261px; height: 154px; padding: 0; margin: 0; border-top: 3px solid #1cade0;
                        border-bottom: 2px solid #ccd0d3; overflow: hidden; float: left; margin-right: 13px;
                        margin-bottom: 13px; position: relative">
                        <div style="border: 1px solid #a8c5d9; width: 243px; height: 144px; padding: 8px;
                            padding-top: 5px;">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; line-height: 22px;">
                                <tr>
                                    <td>
                                        姓名：<span style="color: #0c5282"><%#EyouSoft.Common.Utils.GetText(Eval("UserName").ToString(), 13)%></span>
                                        <span>
                                            <%#EyouSoft.Common.Utils.GetMQ(Eval("MQ").ToString())%></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        公司：<%#EyouSoft.Common.Utils.GetText(Eval("CompanyName").ToString(),17)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        主营：<%#EyouSoft.Common.Utils.GetText(Eval("Subject").ToString(),17)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        联系我们：<%#Eval("Contact")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="width: 100%; border-top: 1px dashed #ccc; border-bottom: 1px dashed #ccc;
                                            padding-top: 5px; overflow: hidden; height: 42px; margin-top: 4px;">
                                            <img style="border: 1px solid #dce3eb; width: 126px; height: 35px" src='<%#GetCompanyLogoSrc(Eval("CompanyLogo"))%>' />
                                            <%#IsShowWebStore(Eval("EshopUrl").ToString())%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </li>
                </ItemTemplate>
            </cc2:CustomRepeater>
        </ul>
        <div style="clear: both; width: 100%; height: 0; margin: 0; padding: 0; overflow: hidden">
        </div>
        <style>
            .page a:link, .page a:active, .page a:visited
            {
                color: #074387;
                text-decoration: none;
            }
            .page a:hover
            {
                color: #F30;
            }
        </style>
        <!--分页 开始-->
        <div style=" text-align:center">
            <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
        </div>
        <!--分页 结束-->
    </div>
    <!--tabs切换导航 结束-->

    <script type="text/javascript">

        function doAjaxSubmit() {
            $.ajax(
                   {
                       url: 'UserCardMode.aspx',
                       data: "opType=Add&companyId=" + $("#companyId_hidden").attr("value") + "&path=" + $("#MQCardUploadControl1_hidFileName").attr("value"),
                       cache: false,
                       type: "post",
                       async: false,
                       dataType: 'json',
                       success: function(result) {
                           if (result.res) {
                               alert("上传成功"); //显示预定结果
                               window.location = window.location;
                           }
                       },
                       error: function() {
                           window.location = window.location;
                       }
                   })
        }

        var newName;
        function fileQueued1(file) {
            try {
                var self = this;
                var hidFileName = document.getElementById(this.customSettings.HidFileNameId);
                hidFileName.value = "";
                var str = file.name.split('.');
                if (str[0].length > 5) {
                    newName = str[0].substring(0, 5);
                }
                else {
                    newName = str[0];
                }
                newName += "." + str[1];

                $("#fileInfo").show();
                $("#fileInfo").html("<label title=\"" + file.name + "\">" + newName + "</label><a id=\"delfile\" href=\"javascript:;\">&nbsp;删除</a>");
                $("#delfile").click(function() {
                    $("#fileInfo").hide();
                    if ($.browser.mozilla) {
                        $("#SWFUpload_0").attr("height", 20);
                        $("#SWFUpload_0").attr("width", 95);
                    }
                    else {
                        $("#sp1").css({ left: '0px' });
                        $("#sp1").css({ position: 'relative' });
                    }
                    resetSwfupload(self, file);
                    return false;
                });
                if ($.browser.mozilla) {
                    $("#SWFUpload_0").attr("height", 0);
                    $("#SWFUpload_0").attr("width", 0);
                }
                else {
                    $("#sp1").css({ position: 'absolute', left: '-100px' });
                }

                var sfu1 = MQCardUploadControl1;
                sfu1.customSettings.UploadSucessCallback = doAjaxSubmit;
                sfu1.startUpload();
            } catch (e) {
            }
        }
    </script>

    </form>
</body>
</html>
