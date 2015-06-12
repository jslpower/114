<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInformation.aspx.cs"
    Inherits="SiteOperationsCenter.NewsCenterControl.EditInformation" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Register Src="~/usercontrol/DocFileUpload.ascx" TagName="DocFileUpload" TagPrefix="uc2" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <style>
        .unnamed1
        {
            color: Red;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <table width="100%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_bg table_basic">
        <tr>
            <td width="19%" align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>标&nbsp;&nbsp; 题：
            </td>
            <td width="81%" bgcolor="#FFFFFF">
                <input name="Title" id="Title" size="45" runat="server" valid="required|limit" max="100"
                    errmsg="标题内容不能为空|标题内容不能超过100字符" />
                <span id="errMsg_Title" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                <asp:Label runat="server" ID="ltname"></asp:Label>
            </td>
            <td bgcolor="#FFFFFF">
                <asp:Label ID="RelevantLine" runat="server">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                发布公司名：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:Label runat="server" ID="CompanyName">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                发布个人账户：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:Label ID="PersonAcount" runat="server">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                分&nbsp;&nbsp; 类：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:DropDownList runat="server" ID="DdlType">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span> 内&nbsp;&nbsp; 容：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:TextBox ID="txtDescription" runat="server" valid="required" errmsg="请填写内容"></asp:TextBox>
                <span class="unnamed1" id="errMsg_txtDescription"></span>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                图&nbsp;&nbsp; 片：
            </td>
            <td bgcolor="#FFFFFF">
                <table>
                    <tr>
                        <td>
                            <uc1:SingleFileUpload runat="server" ID="ImgupLoad" IsGenerateThumbnail="false" IsUploadSwf="false" />
                        </td>
                        <td>
                            （jpg,gif,png图片上传）<asp:Label runat="server" ID="LabImgupLoad"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                附&nbsp;&nbsp; 件：
            </td>
            <td bgcolor="#FFFFFF">
                <table>
                    <tr>
                        <td>
                            <uc2:DocFileUpload runat="server" ID="DocUpload" IsGenerateThumbnail="false" IsUploadSwf="false" />
                        </td>
                        <td>
                            （.rar,.zip,.doc.xls.txt文档上传下载）<asp:Label runat="server" ID="LabDocUpload"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                B2B显示控制：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:DropDownList ID="DdlB2B" runat="server" errmsg="请选择" valid="required">
                </asp:DropDownList>
                <input id="txt_B2BOrder" valid="required|RegInteger" errmsg="请输入正向排序|请填写数字" name="txt_B2BOrder"
                    type="text" runat="server" value="50" />（1~99）正向排序，默认50（易诺管理员控制） <span id="errMsg_txt_B2BOrder"
                        class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                点击量：
            </td>
            <td>
                <asp:Label ID="ClickMount" runat="server">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                发布时间IP：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:Label ID="Time" runat="server">
                </asp:Label>
                &nbsp;&nbsp; IP:<asp:Label ID="IP" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="btnSave" Text="修 改" class="baocun_an" OnClick="btnSave_Click" />
            </td>
            <td align="center">
            </td>
            <td align="center">
                <asp:Button runat="server" ID="btnClose" class="baocun_an" Text="关 闭" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <input type="hidden" id="ImgHidden" runat="server" />
    <input type="hidden" id="DocHidden" runat="server" />

    <script type="text/javascript" src="/kindeditor/kindeditor-min.js"></script>

    <script type="text/javascript">
        KE.init({
            id: '<%=txtDescription.ClientID %>', //编辑器对应文本框id
            width: '700px',
            height: '150px',
            skinsPath: '/kindeditor/skins/',
            pluginsPath: '/kindeditor/plugins/',
            scriptPath: '/kindeditor/skins/',
            resizeMode: 0, //宽高不可变
            items: keSimple //功能模式(keMore:多功能,keSimple:简易)
        });
        var ImageUploadControl,DocUpLoadControl;
        var isSubmit=false;
        $(function() {
            ImageUploadControl=<%=ImgupLoad.ClientID %>;
            DocUpLoadControl=<%=DocUpload.ClientID %>;
            //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
	        FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
	        
            $("#<%=btnSave.ClientID %>").click(function() {
                if($.trim($("#<%=txtDescription.ClientID %>").val())=="")
                    {
                        $("#errMsg_txtDescription").html("请输入内容！");
                        return false;
                    }
                if(isSubmit){
                    return true;
                }
                var txt_B2BOrder = parseInt($("#<%=txt_B2BOrder.ClientID%>").val());
                if(txt_B2BOrder<1||txt_B2BOrder>99){
                    $("#errMsg_txt_B2BOrder").html("请填写1到99之间的数字");
                    return false;
                }
                var form = $(this).closest("form").get(0);
                if(ValiDatorForm.validator(form, "span")){
                    if (ImageUploadControl.getStats().files_queued == 0 &&
                        DocUpLoadControl.getStats().files_queued == 0) {
                        return true;
                    }
                    ImageUpLoad();
                    return false;
                    
                }
                else{
                    return false;
                }
            });
           
        })

        function ImageUpLoad() {
            if (ImageUploadControl.getStats().files_queued > 0) {
                ImageUploadControl.customSettings.UploadSucessCallback = DocUpLoad;
                ImageUploadControl.startUpload();
            }
            else {
                DocUpLoad();
            }
        }

        function DocUpLoad() {
            if (DocUpLoadControl.getStats().files_queued > 0) {
                DocUpLoadControl.customSettings.UploadSucessCallback = UpLoadSave;
                DocUpLoadControl.startUpload();
            }
            else {
                UpLoadSave();
            }
        }

        function UpLoadSave() {
            isSubmit = true;
            $("#<%=btnSave.ClientID %>").click();
            return false;
        }
        
        $(function() {
            KE.create('<%=txtDescription.ClientID %>', 0);
        })
        
    </script>

    </form>
</body>
</html>
