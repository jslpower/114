<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetLogoPic.aspx.cs" Inherits="UserBackCenter.EShop.SetLogoPic" %>

<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>上传logo图片</title>
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="divOldTemplate">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr style="height: 150px;">
                <td width="22%" class="left1">
                    上传网店招牌：
                </td>
                <td width="78%" class="right1" align="left">
                    <uc1:SingleFileUpload ID="sfulogo" runat="server" ImageWidth="960" ImageHight="147"
                        IsUploadSwf="true" />
                    (图片大小960*147像素)&nbsp;&nbsp;
                    <asp:Literal ID="ltr_logo" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <asp:Button ID="btnsubmitlogo" runat="server" Text="提交" Style="height: 22px; width: 60px;"
                        OnClick="btnsubmitlogo_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdfAgoImgPath" runat="server" />

        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

        <script type="text/javascript">
        var sfu1,isSubmit=false;
    $(function(){
        sfu1 = <%=sfulogo.ClientID %>;
        $("#<%=btnsubmitlogo.ClientID %>").click(function(){
            if(isSubmit){
                return true;
            }
            if(sfu1.getStats().files_queued<=0)
            {
                alert("请选择文件上传！");
                return false;
            }
            sfu1.customSettings.UploadSucessCallback = doSubmit;
            sfu1.startUpload();                
            return false;
        });
    });
    function doSubmit()
    {
        isSubmit = true;
        $("#<%=btnsubmitlogo.ClientID %>").click();
    }
    
        </script>

    </div>
    <div runat="server" id="divNewTemplate">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="45%" height="30" align="center">
                    <input id="ckbDefault" name="ckbModel" runat="server" onclick="ShowFileUp('d')" type="radio" /><label
                        for="ckbDefault">普通模式</label>
                    <input id="ckbCustom" type="radio" runat="server" name="ckbModel" onclick="ShowFileUp('c')" /><label
                        for="ckbCustom">定制模式</label>
                </td>
                <td width="55%">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5" id="tblCompanyLog">
            <tr>
                <td width="22%" class="left1">
                    上传LOGO：
                </td>
                <td width="78%" class="right1">
                    <uc1:SingleFileUpload ID="sfuCompanyLog" runat="server" ImageWidth="150" ImageHight="60"
                        IsUploadSwf="false" />
                    (图片大小150px*60px)&nbsp;&nbsp;
                    <asp:Literal ID="ltrCompanyLog" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="left1">
                    上传背景图片：
                </td>
                <td class="right1">
                    <uc1:SingleFileUpload ID="sfuBackImg" runat="server" ImageWidth="960" ImageHight="120"
                        IsUploadSwf="false" />
                    (图片大小960px*120px)&nbsp;&nbsp;
                    <asp:Literal ID="ltrBackImg" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5" id="tblCustomImg">
            <tr>
                <td width="22%" class="left1">
                    上传网店招牌：
                </td>
                <td width="78%" class="right1">
                    <uc1:SingleFileUpload ID="sfuCustomImg" runat="server" ImageWidth="960" ImageHight="120"
                        IsUploadSwf="true" />
                    （图片大小960px*120px）<br />
                    （定制模式表现形式丰富，banner区域不拘于现有格式，可自定义放置任何文字、图片内容！支持.gif.jpg.swf格式）<br />
                    <asp:Literal ID="ltrCustomImg" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <asp:Button runat="server" ID="btnSaveNew" Text="提交" Style="height: 22px; width: 60px;"
                        OnClick="btnSaveNew_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidCompanyLog" runat="server" />
        <asp:HiddenField ID="hidBackImg" runat="server" />
        <asp:HiddenField ID="hidCustomImg" runat="server" />

        <script type="text/javascript">

            var sfuLog;
            var sfuImg;
            var sfuCustomImg;
            var isNewSubmit = false;

            $(function() {
                sfuLog = <%= sfuCompanyLog.ClientID %>;
                sfuImg = <%= sfuBackImg.ClientID %>;
                sfuCustomImg = <%= sfuCustomImg.ClientID %>;
                $("#<%= btnSaveNew.ClientID %>").click(function() {
                    $("#<%= btnSaveNew.ClientID %>").attr("disabled","disabled");
                    if (isNewSubmit) {
                        <%= this.Page.GetPostBackEventReference(this.btnSaveNew) %>
                        //return true;
                    }
                    else {
                        var strErr = "";
                        if ($("#<%= ckbDefault.ClientID %>").attr("checked")) {
                            if ($("#<%= hidCompanyLog.ClientID %>").val() == "") {  //原来有图片不作上传图片验证
                                if (sfuLog.getStats().files_queued <= 0)
                                    strErr += "请上传Logo！\n";
                            }
                            if ($("#<%= hidBackImg.ClientID %>").val() == "") {
                                if (sfuImg.getStats().files_queued <= 0)
                                    strErr += "请上传背景图片！\n";
                            }
                        }
                        else if ($("#<%= ckbCustom.ClientID %>").attr("checked")) {
                            if ($("#<%= hidCustomImg.ClientID %>").val() == "") {
                                if (sfuCustomImg.getStats().files_queued <= 0)
                                    strErr += "请上传网店招牌！\n";
                            }
                        }
                        else
                            strErr += "请选择模式！\n";

                        if (strErr != "") {
                            alert(strErr);
                            return false;
                        }
                        if ($("#<%= ckbDefault.ClientID %>").attr("checked"))
                            UpdateCompanyLog();
                        else if ($("#<%= ckbCustom.ClientID %>").attr("checked"))
                            UpdateCustomImg();

                        return false;
                    }
                });
            });

            //上传公司Log
            function UpdateCompanyLog() {
                if (sfuLog.getStats().files_queued > 0) {
                    sfuLog.customSettings.UploadSucessCallback = UpdateCompanyImg;
                    sfuLog.startUpload();
                }
                else {
                    UpdateCompanyImg();
                }
            }

            //上传公司背景图片
            function UpdateCompanyImg() {
                if (sfuImg.getStats().files_queued > 0) {
                    sfuImg.customSettings.UploadSucessCallback = doNewSubmit;
                    sfuImg.startUpload();
                }
                else {
                    doNewSubmit();
                }
            }

            //上传定制图片
            function UpdateCustomImg() {
                if (sfuCustomImg.getStats().files_queued > 0) {
                    sfuCustomImg.customSettings.UploadSucessCallback = doNewSubmit;
                    sfuCustomImg.startUpload();
                }
                else {
                    doNewSubmit();
                }
            }

            function doNewSubmit() {
                isNewSubmit = true;
                $("#<%= btnSaveNew.ClientID %>").click();
            }

            function ShowFileUp(type) {
                isNewSubmit = false;
                if (type == "d") {
                    $("#tblCompanyLog").show();
                    $("#tblCustomImg").hide();
                }
                else if (type == "c") {
                    $("#tblCompanyLog").hide();
                    $("#tblCustomImg").show();
                }
            }
        </script>

    </div>
    </form>
</body>
</html>
