<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IMFrame.Card.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControls/SingleFileUpload.ascx" TagName="SingleFileUpload" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业名片</title>
    <style type="text/css">
            BODY
            {
                color: #333;
                font-size: 12px;
                font-family: "宋体" ,Arial, Helvetica, sans-serif;
                text-align: center;
                background: #DEE7FF;
                margin: 0px;
            }
            img
            {
                border: thin none;
            }
            table
            {
                border-collapse: collapse;
                margin: 0px auto;
                padding: 0px auto;
                border: 0px;
            }
            tr
            {
                border: 0px;
                margin: 0px;
            }
            TD
            {
                font-size: 12px;
                color: #0E3F70;
                line-height: 20px;
                border: 0px;
                font-family: "宋体" ,Arial, Helvetica, sans-serif;
            }
            div
            {
                margin: 0px auto;
                text-align: left;
                padding: 0px auto;
                border: 0px;
            }
            textarea
            {
                font-size: 12px;
                font-family: "宋体" ,Arial, Helvetica, sans-serif;
                color: #333;
            }
            select
            {
                font-size: 12px;
                font-family: "宋体" ,Arial, Helvetica, sans-serif;
                color: #333;
            }
            .ff0000
            {
                color: #f00;
            }
            a
            {
                color: #0E3F70;
                text-decoration: none;
            }
            a:hover
            {
                color: #f00;
                text-decoration: underline;
            }
            a:active
            {
                color: #f00;
                text-decoration: none;
            }
            a.red
            {
                color: #cc0000;
            }
            a.red:visited
            {
                color: #cc0000;
            }
            a.red:hover
            {
                color: #ff0000;
            }
            a.white
            {
                color: #ffffff;
            }
            a.white:visited
            {
                color: #ffffff; 
            }
            a.white:hover
            {
                color: #ffffff;
                text-decoration: underline;
            }
            a.cs
            {
                color: #723B00;
            }
            a.cs:visited
            {
                color: #723B00;
            }
            a.cs:hover
            {
                color: #ff0000;
            }
            .pn-w
            {
                width: 140px;
                border: 0px;
                color: #0E3F70;
                font-weight: bold;
                background: #DEE7FF;
            }
            .input
            {
                border: 1px solid #7F9DB9;width:100px;
            }
            .alginCenter
            {
                text-align: center;
            }
            .cardbj
            {
                background-image: url(<%=ImageServerUrl %>/IM/images/cardbar.gif);
                background-repeat: repeat-x;
                height: 22px;
                width: 100%;
                background-position: 0px -22px;
            }
            .cardbj a
            {
                color: #fff;
                font-weight: bold;
            }
            .cardon
            {
                width: 65px;
                text-align: center;
                background: url(<%=ImageServerUrl %>/IM/images/cardbar.gif);
            }
            .cardon a
            {
                color: #f00;
            }
    .cardshop a{ display:block; width:144px; height:32px;  background-image:url(<%=ImageServerUrl %>/IM/images/cardshop.gif); text-indent:-9999px; margin-top:2px;}
    .cardshop a:hover {background-image:url(<%=ImageServerUrl %>/IM/images/cardshop.gif); background-position:0 -33px;}
    .cardshopgj a{ display:block; width:144px; height:32px;  background-image:url(<%=ImageServerUrl %>/IM/images/cardshop.gif); background-position:0 -66px; text-indent:-9999px; margin-top:2px;}
    .cardshopgj a:hover {background-image:url(<%=ImageServerUrl %>/IM/images/cardshop.gif); background-position:0 -99px;}
        </style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript">
        function changeCard(v) {
            if (v == 1) {
                if ($("#trOtherSide").attr("className") == "cardon") return;

                $("#trSelfSide").attr("className", "");
                $("#trOtherSide").attr("className", "cardon");

                $("#divOtherSide").show();
                $("#divSelfSide").hide();
            } else {
                if ($("#trSelfSide").attr("className") == "cardon") return;

                $("#trOtherSide").attr("className", "");
                $("#trSelfSide").attr("className", "cardon");

                $("#divSelfSide").show();
                $("#divOtherSide").hide();
            }
        }

        function showMsg(objId) {
            $("#" + objId).show();
        }

        function hideMsg(objId) {
            $("#" + objId).hide();
        }
  
    </script>

    <script type="text/javascript">
        function fileQueueError(file, errorCode, message) {
            try {
                var object = this.getStats();
                switch (errorCode) {
                    case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
                        var fileCount = this.getSetting("file_upload_limit");
                        errorName = "当前只能上传" + fileCount + "个文件.";
                        break;
                    case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                        errorName = "您选择的文件是空的"
                        break;
                    case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                        errorName = "您选择的文件超过了指定的大小" + this.getSetting("file_size_limit");
                        break;
                    case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                        errorName = "错误的文件类型,只能上传" + this.getSetting("file_types");
                        break;
                    default:
                        errorName = message;
                        break;
                }
                alert(errorName);

            } catch (ex) {
            //this.debug(ex);
                alert("eee")
            }
        }

        function fileQueued(file) {
            try {
                var fType = this.customSettings.ErrMsgId;
                var self = this;
                var hidFileName = document.getElementById(this.customSettings.HidFileNameId);
                hidFileName.value = "";
                var newName;
                var str = file.name.split('.');
                if (str[0].length > 5) {
                    newName = str[0].substring(0, 5);
                }
                else {
                    newName = str[0];
                }
                newName += "." + str[1];

                if (fType == 'Logo') {
                    $("#fileInfoLogo").show();
                    $("#fileInfoLogo").html("<label title=\"" + file.name + "\">" + newName + "</label>&nbsp;<a id=\"delfileLogo\" href=\"javascript:;\">删除</a>");
                    $("#delfileLogo").click(function() {
                        $("#fileInfoLogo").hide();
                        $("#fileLogo").css({ left: '0px' });
                        $("#fileLogo").css({ position: 'relative' });
                        resetSwfupload(self, file);
                        return false;
                    });
                    $("#fileLogo").css({ position: 'absolute', left: '-200px' });
                } else {
                    $("#fileInfoAd").show();
                    $("#fileInfoAd").html("<label title=\"" + file.name + "\">" + newName + "</label>&nbsp;<a id=\"delfileAd\" href=\"javascript:;\">删除</a>");
                    $("#delfileAd").click(function() {
                        $("#fileInfoAd").hide();
                        $("#fileAd").css({ left: '0px' });
                        $("#fileAd").css({ position: 'relative' });
                        resetSwfupload(self, file);
                        return false;
                    });
                    $("#fileAd").css({ position: 'absolute', left: '-200px' });
                }

                var progress = new FileProgress(file, this.customSettings.upload_target, this);

            } catch (e) {
            }
        }
    </script>
    
    

</head>
<body scroll="no" oncontextmenu="return false;">
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="0" class="cardbj" style="width: 140px">
        <tr>
            <td width="5">
            </td>
            <td class="cardon" id="trOtherSide">
                <a style="cursor: pointer" onclick="changeCard(1)">对方名片</a>
            </td>
            <td align="center" id="trSelfSide">
                <a style="cursor: pointer" onclick="changeCard(0)">我的名片</a>
            </td>
        </tr>
    </table>
    
    <div id="divOtherSide" style="width: 140px; margin: 0px auto">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <asp:Literal runat="server" ID="ltrOtherSideEShop"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <asp:PlaceHolder runat="server" ID="phOtherSideLogo" Visible="false">
                                    <tr>
                                        <td align="center">
                                            <asp:Literal ID="ltrOtherSideLogo" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    </asp:PlaceHolder>
                                    <tr>
                                        <td align="left" style="color: #333333;">
                                            <asp:Label ID="lblOtherSideContactName" runat="server"></asp:Label>
                                            <asp:Label ID="lblOtherSideContactGender" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>
                                                <input runat="server" id="txtOtherSideCompanyName" type="text" class="pn-w" readonly="readonly" />
                                            </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="color: #333333; text-align: left;">
                                            <div style="width: 140px; text-align: left; overflow-x: hidden; word-wrap: break-word; word-break: break-all">
                                                <nobr>电话：</nobr>
                                                <asp:Literal ID="ltrOtherSideTelephone" runat="server"></asp:Literal><br />
                                                <nobr>手机：</nobr>
                                                <asp:Literal ID="ltrOtherSideMobile" runat="server"></asp:Literal><br />
                                            </div>
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="phOtherSideAreas" Visible="false">
                                    <tr>
                                        <td align="left">
                                            <div style="word-break: normal; overflow-x: hidden; word-wrap: break-word; width: 133px; padding: 3px; background: #FFF8F4; border: 1px solid #D17233;">
                                                主营：<asp:Literal ID="ltrOtherSideArea" runat="server"></asp:Literal>
                                            </div>
                                        </td>
                                    </tr>
                                    </asp:PlaceHolder>
                                    <asp:Literal ID="ltrOtherSideAd" runat="server"></asp:Literal>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <div id="divSelfSide" style=" display:none;width: 140px; margin: 0px auto;">
        <asp:Literal ID="ltrSelfEshop" runat="server"></asp:Literal>
        <asp:literal ID="ltrSelfLogo" runat="server"></asp:literal>
        <asp:PlaceHolder ID="phNotPayMQSelfLogo" runat="server" Visible="false">
            <table width="140" height="60" border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #D8D8D8; margin-top: 3px; margin-bottom: 3px;">
                <tr>
                    <td align="center" bgcolor="#F6F6F6" style="color: #B1B1B1">
                        <div style="position: relative; display: none" id="divLogoMsg">
                            <div style="position: absolute; top: -40px; left: -2px;">
                                <table width="140" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="3"><img src="<%=ImageServerUrl %>/IM/images/tsboxt.gif" width="140" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="border-left: 1px solid #E0B583; border-right: 1px solid #E0B583; background: #FFF3C5; color: #A81E1E; padding: 2px; line-height: 120%; font-weight: normal">
                                            对不起，您还不是企业MQ会员，暂不能上传企业宣传LOGO，请先申请开通企业MQ会员
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10" align="center" style="border-left: 1px solid #E0B583; background: #FFF3C5; color: #A81E1E">
                                            &nbsp;
                                        </td>
                                        <td width="158" align="center" style="background: #FFF3C5; color: #A81E1E">
                                            <a href="<%=IntroductionURL %>" target="_blank">立即开通</a> <a href="<%=IntroductionURL %>" target="_blank">查看介绍</a>
                                        </td>
                                        <td width="32" align="center" style="border-right: 1px solid #E0B583; background: #FFF3C5; color: #A81E1E">
                                            <a href="javascript:hideMsg('divLogoMsg')" style="color: #999999">关闭</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"><img src="<%=ImageServerUrl %>/IM/images/tsboxb.gif" width="140" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <a href="javascript:showMsg('divLogoMsg');">点击上传我的企业LOGO<br />
                            (大小:140*60像素)</a>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phPayMQSelfLogo" runat="server" Visible="false">
            <div id="fileInfoLogo" style="line-height:20px"></div>
            <div id="fileLogo" style="width: 140px; height: <%=FLogoHeight%>px; position: relative; display: block; float: left">
                <span style="float: left; position: absolute; left: 0px; top: 0px">
                    <uc1:SingleFileUpload ID="FSelfLogo" runat="server" />
                </span>
            </div>
        </asp:PlaceHolder>
        <table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
                <td align="left">
                    <input runat="server" id="txtSelfCompanyName" type="text" class="pn-w" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    姓名：<asp:TextBox ID="txtSelfContactName" runat="server" CssClass="input" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    性别：<asp:RadioButtonList ID="rblSelfGender" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="0">男</asp:ListItem>
                        <asp:ListItem Value="1">女</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    电话：<asp:TextBox ID="txtSelfContactTel" runat="server" CssClass="input" size="12" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    手机：<asp:TextBox ID="txtSelfContactMobile" runat="server" CssClass="input" size="12" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Literal ID="ltrSelfAd" runat="server"></asp:Literal>
        <asp:PlaceHolder runat="server" ID="phNotPayMQSelfAd" Visible="false">
            <table width="100%" border="0" cellspacing="1" cellpadding="0">
                <tr>
                    <td align="left">
                        <table width="140" height="125" border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #D8D8D8; margin-top: 3px; margin-bottom: 3px;">
                            <tr>
                                <td align="center" bgcolor="#F6F6F6" style="color: #B1B1B1">
                                    <div style="position: relative; display: none;" id="divAdMsg">
                                        <div style="position: absolute; top: -41px; left: -2px;">
                                             <table width="140" border="0" cellspacing="0" cellpadding="0">
                                    			<tr>
                                        			<td colspan="3"><img src="<%=ImageServerUrl %>/IM/images/tsboxt.gif" width="140" /></td>
                                                </tr>
                                                <tr>
                                        			<td colspan="3" style="border-left: 1px solid #E0B583; border-right: 1px solid #E0B583; background: #FFF3C5; color: #A81E1E; padding: 2px; line-height: 120%; font-weight: normal">
                                                        对不起，您还不是企业MQ会员，暂不能上传您的MQ广告，请先申请开通企业MQ会员
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10" align="center" style="border-left: 1px solid #E0B583; background: #FFF3C5; color: #A81E1E">
                                                        &nbsp;
                                                    </td>
                                                    <td width="158" align="center" style="background: #FFF3C5; color: #A81E1E">
                                                        <a href="<%=IntroductionURL %>" target="_blank">立即开通</a> <a href="<%=IntroductionURL %>" target="_blank">查看介绍</a>
                                                    </td>
                                                    <td width="32" align="center" style="border-right: 1px solid #E0B583; background: #FFF3C5; color: #A81E1E">
                                                        <a href="javascript:hideMsg('divAdMsg')" style="color: #999999">关闭</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3"><img src="<%=ImageServerUrl %>/IM/images/tsboxb.gif" width="140" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <a href="javascript:showMsg('divAdMsg');">点击上传我的广告<br />
                                        (大小140*140像素)</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phPayMQSelfAd" runat="server" Visible="false">
            <table width="100%" border="0" cellspacing="1" cellpadding="0">
                <tr>
                    <td align="left">
                        <div id="fileInfoAd" style="line-height:20px"></div>
                        <div id="fileAd" style="position: relative; display: block; float: left; width: 140px; height: <%=FAdHeight%>px">
                            <span style="float: left; position: absolute; left: 0px; top: 0px">
                                <uc1:SingleFileUpload ID="FSelfAd" runat="server" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="txtSelfCurrentAdImgPath" runat="server" />
                        链接地址:<asp:TextBox ID="txtPayMQSelfAdUrl" runat="server" CssClass="input" size="12" Width="80"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>        
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
    </div>
    <asp:PlaceHolder ID="phFScript" runat="server" Visible="false">

        <script type="text/javascript">
            var sfu1 = null;
            var sfu2 = null;
            var isSubmit = false;
            function doSubmit(){
                isSubmit = true;
                $("#<%=btnSave.ClientID%>").click();
            }
            function USelfAd() {
                if(sfu2.getStats().files_queued > 0)
                {
                    sfu2.customSettings.UploadSucessCallback = doSubmit;
                    sfu2.startUpload();
                }
                else
                {
                    doSubmit();
                }
            }
            
            $(function() {
                 sfu1 = <%= FSelfLogo.ClientID %>;
                 sfu2 = <%= FSelfAd.ClientID %>;
            })
            
            $("#<%=btnSave.ClientID %>").bind("click",function() {
                if (isSubmit) {
                    return true;
                }else {
                    if(sfu1.getStats().files_queued > 0){
                        sfu1.customSettings.UploadSucessCallback = USelfAd;
                        sfu1.startUpload();
                    }
                    else{
                        USelfAd();                        
                    }                  
                    return isSubmit;                    
                }
            });
        </script>
    </asp:PlaceHolder>
    
    <asp:PlaceHolder ID="phGYSFScript" runat="server" Visible="false">

        <script type="text/javascript">
            var sfu1 = null;
            var isSubmit = false;
            function doSubmit(){
                isSubmit = true;
                $("#<%=btnSave.ClientID%>").click();
            }
            
            $(function() {
                 sfu1 = <%= FSelfLogo.ClientID %>;
            })
            
            $("#<%=btnSave.ClientID %>").bind("click",function() {
                if (isSubmit) {
                    return true;
                }else {
                     if (sfu1.getStats().files_queued <= 0) {
                     alert(1)
                        return true;
                    }
                    sfu1.customSettings.UploadSucessCallback = doSubmit;
                    sfu1.startUpload();     
                    return false;                    
                }
            });
        </script>

    </asp:PlaceHolder>
    </form>
</body>
</html>
