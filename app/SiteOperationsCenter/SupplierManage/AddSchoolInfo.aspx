<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSchoolInfo.aspx.cs"
    Inherits="SiteOperationsCenter.AddSchoolInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Register Src="~/usercontrol/SchoolMenu.ascx" TagPrefix="cc1" TagName="SchoolMenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:SchoolMenu runat="server" ID="SchoolMenu1" MenuIndex="2"></cc1:SchoolMenu>
    <table width="100%" border="1" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;
        padding: 1px;">
        <tr>
            <td width="13%" style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                <span class="unnamed1">* </span>类别：
            </td>
            <td width="87%" style="background: #fff; height: 28px; text-align: left;">
                <asp:DropDownList ID="ddlBigType" name="ddlBigType" valid="PositiveIntegers" runat="server"
                    errmsg="请选择！">
                </asp:DropDownList>
                <span id="errMsg_ddlBigType" class="errmsg"></span>
                <asp:DropDownList runat="server" ID="ddlSmallType" name="ddlSmallType" valid="custom"
                    custom="ValidSmall" errmsg="请选择！">
                </asp:DropDownList>
                <span id="errMsg_ddlSmallType" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                <span class="unnamed1">* </span>标题：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <input id="txt_TitleName" runat="server" name="txt_TitleName" type="text" size="80"
                    valid="required" maxlength="80" errmsg="请输入标题！" />&nbsp; <span id="errMsg_txt_TitleName"
                        class="errmsg"></span>
                <asp:CheckBox runat="server" ID="ckbIsTop" Text="设为头条" />
                <asp:CheckBox runat="server" ID="ckbIsFrontPage" Text="首页显示" />
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                标题颜色：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <asp:DropDownList runat="server" ID="ddlColor">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem style="background: #ff0000" Text="红色" Value="#ff0000"></asp:ListItem>
                    <asp:ListItem style="background: #CC0000" Text="中国红" Value="#CC0000"></asp:ListItem>
                    <asp:ListItem style="background: #00CC00" Text="绿色" Value="#00CC00"></asp:ListItem>
                    <asp:ListItem style="background: #0033FF" Text="蓝色" Value="#0033FF"></asp:ListItem>
                    <asp:ListItem style="background: #FF9900" Text="黄色" Value="#FF9900"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                标签：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <input id="txtTag" runat="server" name="txtTag" type="text" size="80" maxlength="80" />
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                来源：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <input id="txtSource" runat="server" name="txtSource" type="text" size="80" maxlength="80" value="网络" />
            </td>
        </tr>
        <tr>
            <td height="33" style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;" rowspan="2">
                图片：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <div style="float:left;">
                    <uc1:SingleFileUpload ID="SingleFileUpload1" IsGenerateThumbnail="false" ButtonText="请选择焦点图片" runat="server" ImageWidth="600" ImageHeight="400" />焦点图片最佳分辨率为：[120*90]
                </div>
                <div style="float:left;">
                    <uc1:SingleFileUpload ID="SingleFileUpload2" runat="server" IsGenerateThumbnail="false" ButtonText="请选择推荐图片" ImageWidth="600" ImageHeight="400" />&nbsp;推荐图片最佳分辨率为：[464*170]
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <asp:Literal runat="server" ID="ltrOldImgThumb"></asp:Literal>
                    <asp:Literal runat="server" ID="ltrOldImgPath"></asp:Literal>
                </div>
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                内容：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <FCKeditorV2:FCKeditor ID="FCK_PlanTicketContent" Height="400" runat="server">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                发布人：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <input id="txtSendPeople" readonly="true" runat="server" name="txtSendPeople" type="text"
                    size="20" />
            </td>
        </tr>
        <tr>
            <td style="background: #C0DEF3; height: 28px; text-align: right; font-weight: bold;">
                发布时间：
            </td>
            <td style="background: #fff; height: 28px; text-align: left;">
                <input id="txtIssuTime" readonly="true" runat="server" name="txtSendPeople" type="text"
                    size="20" />
            </td>
        </tr>
    </table>
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="btn_Save" runat="server" Text="保存" OnClick="btn_Save_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="hidden" runat="server" id="hdfImgPath" name="hdfImgPath" />
                <input type="hidden" runat="server" id="hdfImgThumb" name="hdfImgThume" />
                <input type="button" id="btnCancel" value="返回列表" onclick="javascript:location.href='/SupplierManage/SchoolList.aspx'" />
            </td>
        </tr>
    </table>
    <div id="SmallTypes" style="display: none;">
    </div>

    <script type="text/javascript">
    var sfu1 =<%=SingleFileUpload1.ClientID %>;
    var sfu2 =<%=SingleFileUpload2.ClientID %>;
    var isSubmit = false; //区分按钮是否提交过
    var SaveCount=0; //提交次数
    //模拟一个提交按钮事件
    function doSubmit(){
    if(sfu2.getStats().files_queued<=0 || SaveCount>0)
    {
        isSubmit = true;
        $("#<%=btn_Save.ClientID%>").click();
    }
    SaveCount+=1;
    sfu2.customSettings.UploadSucessCallback =doSubmit;
    sfu2.startUpload();     
    return false;      
    }
    $(function(){
            var bigType=parseInt($("#<%= ddlBigType.ClientID %>").val());
            $("#<%= ddlSmallType.ClientID %>").children().each(function(){
                if($(this).val()!="0" && $(this).attr("selected")==false &&bigType!=parseInt($(this).val().split('_')[1]))
                {
                    $(this).remove();
                }
                $("#SmallTypes").append($(this).clone());
            });
            $("#<%= ddlBigType.ClientID %>").change(function(){
               $("#errMsg_ddlSmallType").html("");
               var bigType=parseInt($(this).val());
               if(bigType==0)
               {
                   var obj;
                   $("#<%= ddlSmallType.ClientID %>").children().each(function(){
                        if($(this).val()=="0")
                        {
                            obj=$(this);
                        }
                         $(this).remove();
                    }); 
                    $("#<%= ddlSmallType.ClientID %>").append(obj);
               }
               else
               {
                    var obj;
                    $("#<%= ddlSmallType.ClientID %>").children().each(function(){
                       if($(this).val()=="0")
                        {
                            obj=$(this);
                        } 
                        $(this).remove();
                    });
                    $("#<%= ddlSmallType.ClientID %>").append(obj);
                    $("#SmallTypes").children().each(function(){
                        if($(this).val()!="0")
                        {
                            if(parseInt($(this).val().split('_')[1])==bigType)
                            {
                                $("#<%= ddlSmallType.ClientID %>").append($(this).clone());
                            }
                        }
                    });
                    $("#<%= ddlSmallType.ClientID %>").children().find("[value]='0'").attr("selected",true);
               }
            });
            
        $("#<%=btn_Save.ClientID%>").click(function(){
            if(isSubmit){
            //如果按钮已经提交过一次验证，则返回执行保存操作
                return true;
            }
           
	        var a= ValiDatorForm.validator($("#form1").get(0),"span");
	        if(a){
	        //如果验证成功，则提交按钮保存事件
                    if(sfu1.getStats().files_queued<=0)
                    {
                        doSubmit();
                    }
        	        sfu1.customSettings.UploadSucessCallback = doSubmit;
                    sfu1.startUpload();     
                    return isSubmit;    
            }
            return isSubmit;
        });
	    FV_onBlur.initValid($("#form1").get(0));
    });
    function ValidSmall(obj)
    {
        $("#errMsg_"+$(obj).attr("id")).html("");
        if($("#<%= ddlBigType.ClientID %>").val()!="5")
        {
            if($(obj).val()=="0")
            {
                $("#errMsg_"+$(obj).attr("id")).html($(obj).attr("errmsg"));
                return false;
            }
        }
        return true;
    }
    </script>

    </form>
</body>
</html>
