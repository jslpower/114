<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RotationPicManage.aspx.cs"
    Inherits="UserBackCenter.EShop.RotationPicManage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC">
            <tr>
                <td style="width: 10%;" class="hang">
                    序号
                </td>
                <td style="width: 45%;" class="hang">
                    上传图片（图片大小385*170像素）
                </td>
                <td style="width: 45%;" class="hang">
                    链接
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span class="right1">
                        <asp:TextBox ID="txtSort1" runat="server" valid="required|isInt|limit" errmsg="图片1排序值为空，请填写整数！|图片1排序值请填写整数！|图片1排序值为1-5的整数字"
                            min="1" max="5" Width="50" Text="1"></asp:TextBox>
                    </span>
                </td>
                <td align="center">
                    <uc1:SingleFileUpload ID="sfupic1" runat="server" ImageWidth="385" ImageHeight="170" />
                    <asp:Literal ID="ltrUpImagePath1" runat="server"></asp:Literal>
                    <input type="hidden" id="hdfAgoImgPath1" name="hdfAgoImgPath1" value="" runat="server" />
                </td>
                <td align="center">
                    <asp:TextBox ID="txtLinkAddress1" Width="95%" runat="server" valid="notHttpUrl|custom"
                        custom="custom1" errmsg="图片1的链接地址输入错误|请上传图片1"></asp:TextBox>
                    <span id="errMsg_txtLinkAddress1" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span class="right1">
                        <asp:TextBox ID="txtSort2" valid="required|limit|isInt" min="1" max="5" errmsg="图片2排序值为空，请填写整数！|图片2排序值为1-5的整数字|图片2排序值请填写整数！"
                            runat="server" Width="50" Text="2"></asp:TextBox>
                        <span id="errMsg_txtSort2" class="errmsg"></span></span>
                </td>
                <td align="center">
                    <uc1:SingleFileUpload ID="sfupic2" runat="server" ImageWidth="385" ImageHeight="170" />
                    <asp:Literal ID="ltrUpImagePath2" runat="server"></asp:Literal>
                    <input type="hidden" id="hdfAgoImgPath2" name="hdfAgoImgPath2" value="" runat="server" />
                </td>
                <td align="center">
                    <asp:TextBox ID="txtLinkAddress2" Width="95%" runat="server" valid="notHttpUrl|custom"
                        custom="custom2" errmsg="图片2的链接地址输入错误|请上传图片2"></asp:TextBox>
                    <span id="errMsg_txtLinkAddress2" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span class="right1">
                        <asp:TextBox ID="txtSort3" valid="required|isInt|limit" min="1" max="5" errmsg="图片3排序值为空，请填写整数！|图片3排序值请填写整数！|图片3排序值为1-5的整数字"
                            runat="server" Width="50" Text="3"></asp:TextBox>
                    </span>
                </td>
                <td align="center">
                    <uc1:SingleFileUpload ID="sfupic3" runat="server" ImageWidth="385" ImageHeight="170" />
                    <asp:Literal ID="ltrUpImagePath3" runat="server"></asp:Literal>
                    <input type="hidden" id="hdfAgoImgPath3" name="hdfAgoImgPath3" value="" runat="server" />
                </td>
                <td align="center">
                    <asp:TextBox ID="txtLinkAddress3" Width="95%" runat="server" valid="notHttpUrl|custom"
                        custom="custom3" errmsg="图片3的链接地址输入错误|请上传图片3"></asp:TextBox>
                    <span id="errMsg_txtLinkAddress3" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span class="right1">
                        <asp:TextBox ID="txtSort4" valid="required|isInt|limit" errmsg="图片4排序值为空，请填写整数！|图片4排序值请填写整数！|图片4排序值为1-5的整数字"
                            min="1" max="5" runat="server" Width="50" Text="4"></asp:TextBox>
                    </span>
                </td>
                <td align="center">
                    <uc1:SingleFileUpload ID="sfupic4" runat="server" ImageWidth="385" ImageHeight="170" />
                    <asp:Literal ID="ltrUpImagePath4" runat="server"></asp:Literal>
                    <input type="hidden" id="hdfAgoImgPath4" name="hdfAgoImgPath4" value="" runat="server" />
                </td>
                <td align="center">
                    <asp:TextBox ID="txtLinkAddress4" Width="95%" runat="server" valid="notHttpUrl|custom"
                        custom="custom4" errmsg="图片4的链接地址输入错误|请上传图片4"></asp:TextBox>
                    <span id="errMsg_txtLinkAddress4" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span class="right1">
                        <asp:TextBox ID="txtSort5" valid="required|isInt|limit" errmsg="图片5排序值为空，请填写整数！|图片5排序值请填写整数！|图片5排序值为1-5的整数字"
                            min="1" max="5" runat="server" Width="50" Text="5"></asp:TextBox>
                    </span>
                </td>
                <td align="center">
                    <uc1:SingleFileUpload ID="sfupic5" runat="server" ImageWidth="385" ImageHeight="170" />
                    <asp:Literal ID="ltrUpImagePath5" runat="server"></asp:Literal>
                    <input type="hidden" id="hdfAgoImgPath5" name="hdfAgoImgPath5" value="" runat="server" />
                </td>
                <td align="center">
                    <asp:TextBox ID="txtLinkAddress5" Width="95%" runat="server" valid="notHttpUrl|custom"
                        custom="custom5" errmsg="图片5的链接地址输入错误|请上传图片5"></asp:TextBox>
                    <span id="errMsg_txtLinkAddress5" class="errmsg"></span>
                </td>
            </tr>
        </table>
        <table width="98%" height="30" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" Text="提交" Width="60px" Height="22px" 
                        OnClick="btnSave_Click" />
                    <asp:HiddenField ID="hdfids" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript">    
     function custom2(e,formelements){
        var v = $("#<%=txtLinkAddress2.ClientID %>").val();
        var l=$("#ago2").attr("href"); 
       if((v==undefined || v=="") && (l==undefined || l=="")){
            return true;
        }else{
            if(sfu2.getStats().files_queued>0){
                return true;
            }else {        
                if(l!=null && l!="")
                {
                 return true;
                }            
                return false;
            }
        }
    }        
    function custom1(e,formelements){        
        var v = $("#<%=txtLinkAddress1.ClientID %>").val();
        var l=$("#ago1").attr("href");
       if((v==undefined || v=="") && (l==undefined || l=="")){
            return true;
        }else{
            if(sfu1.getStats().files_queued>0){
                return true;
            }else
            {   
                if(l!=null && l!="")
                {
                 return true;
                }
                return false;
            }
        }
    }
    function custom3(e,formelements){
        var v = $("#<%=txtLinkAddress3.ClientID %>").val();        
        var l=$("#ago3").attr("href");
       if((v==undefined || v=="") && (l==undefined || l=="")){
            return true;
        }else{
            if(sfu3.getStats().files_queued>0){
                return true;
            }else{ 
             if(l!=null && l!="")
                {
                 return true;
                }                   
                return false;
            }
        }
    }        
    function custom4(e,formelements){
        var v = $("#<%=txtLinkAddress4.ClientID %>").val();
        var l=$("#ago4").attr("href");
       if((v==undefined || v=="") && (l==undefined || l=="")){
            return true;
        }else{
            if(sfu4.getStats().files_queued>0){
                return true;
            }else{
                if(l!=null && l!="")
                {
                 return true;
                }
                return false;
            }
        }
    }
    function custom5(e,formelements){
        var v = $("#<%=txtLinkAddress5.ClientID %>").val();
        var l=$("#ago5").attr("href");
       if((v==undefined || v=="") && (l==undefined || l=="")){
            return true;
        }else{
            if(sfu5.getStats().files_queued>0){
                return true;
            }else{
                if(l!=null && l!="")
                {
                 return true;
                }
                return false;
            }
        }
    }    
        var sfu1,sfu2,sfu3,sfu4,sfu5;
        var isSubmit = false;
        $(document).ready(function()
        {   
             sfu1=<%=sfupic1.ClientID %>;
             sfu2=<%=sfupic2.ClientID %>;
             sfu3=<%=sfupic3.ClientID %>;
             sfu4=<%=sfupic4.ClientID %>;
             sfu5=<%=sfupic5.ClientID %>;
               $("#<%=btnSave.ClientID %>").click(function()
               {
                    if(isSubmit)
                    {     
                        return isSubmit;
                    }
                    
                   if(!ValiDatorForm.validator($("#<%=btnSave.ClientID %>").closest("form").get(0),"alert") )
                   {
                    return isSubmit;
                   }
                   
                   var resu=true;
                   for(var i=1;i<6;i++){
                        var k=parseInt($.trim($("#txtSort"+i).val()));
                       if(k>5 || k<1){
                            resu=false;
                            break;
                       }
                   }
                   if(!resu){
                    alert("序号应为1-5的整数字");
                    return false;
                   }
                    if(sfu1.getStats().files_queued>0)
	                {
                        sfu1.customSettings.UploadSucessCallback = RotationPicManage.Pic2;
                        sfu1.startUpload();        
                    }else
                    {
                        RotationPicManage.Pic2();
                    }
                    return isSubmit;
               });
        });
       
        var RotationPicManage=
        {
             Pic2:function(){      
              if(sfu2.getStats().files_queued>0)
	            {        
                sfu2.customSettings.UploadSucessCallback = RotationPicManage.Pic3;
                sfu2.startUpload();
                 }else
                {
                RotationPicManage.Pic3();
                }
             },
             Pic3:function()
             {
              if(sfu3.getStats().files_queued>0)
	            {
                sfu3.customSettings.UploadSucessCallback =RotationPicManage.Pic4;
                sfu3.startUpload();
                 }else
                {
                RotationPicManage.Pic4();
                }
             },
             Pic4:function()
             {
                 if(sfu4.getStats().files_queued>0)
	            {
                sfu4.customSettings.UploadSucessCallback =RotationPicManage.Pic5;
                sfu4.startUpload();
                 }else
                {
                RotationPicManage.Pic5();
                }
             },
             Pic5:function()
             {
              if(sfu5.getStats().files_queued>0)
	            {
                sfu5.customSettings.UploadSucessCallback = RotationPicManage.save;
                sfu5.startUpload();
                 }else
                {
                    RotationPicManage.save();
                }
             },            
             save:function()
             {
                isSubmit=true;
              // __doPostBack('<%=btnSave.UniqueID %>','');
                $("#<%=btnSave.ClientID %>").click();
             }
        }
    </script>

</body>
</html>
