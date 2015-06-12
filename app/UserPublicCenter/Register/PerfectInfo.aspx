<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfectInfo.aspx.cs" Inherits="UserPublicCenter.Register.PerfectInfo" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/SingleFileUpload.ascx"TagName="SingleFileUpload" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/RegisterHead.ascx" TagName="RegisterHead" TagPrefix="uc1" %>
<asp:Content ID="cntPerfect" ContentPlaceHolderID="Main" runat="server">
 <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="false" ></script>
<uc1:RegisterHead ID="RegisterHead1" runat="server" />

      <script type="text/javascript">
     //初始化编辑器
     KE.init({
	    id : 'txaCompanyInfo',//编辑器对应文本框id
	    width : '550px',
	    height : '350px',
	    skinsPath:'/kindeditor/skins/',
	    pluginsPath:'/kindeditor/plugins/',
	    scriptPath:'/kindeditor/skins/',
        resizeMode : 0,//宽高不可变
	    items:keMore //功能模式(keMore:多功能,keSimple:简易)
    });   
    </script>
<style type="text/css">
.fulill
{
	background:url(<%=ImageServerPath %>/images/UserPublicCenter/subb.gif); width:270px; height:37px; border:none; font-size:14px; font-weight:bold; color:#ffffff;
}
</style>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
<link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

<div class="body">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td align="center"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/join-3.gif" width="956" height="30" /></td>
    </tr>
  </table>
  
  <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="border:4px #ff6600 solid;">
    <tr>
      <td width="15%" height="70" align="right"> 形象logo：</td>
      <td width="1%" align="left">&nbsp;</td>
      <td align="left"><uc1:SingleFileUpload ID="sfuLogo" runat="server" /></td>
    </tr>
    <tr>
      <td height="40" align="right">公司介绍：</td>
      <td align="left">&nbsp;</td>
      <td align="left"><textarea name="txaCompanyDetail" id="txaCompanyInfo" cols="120" rows="15" ></textarea>
      </td>
    </tr>
    <tr>
      <td height="40" align="right">营业执照：</td>
      <td align="left">&nbsp;</td>
      <td align="left"><uc1:SingleFileUpload ID="sfuYyzzImg" runat="server" /></td>
    </tr>
    <tr runat="server" id="trCuperous" visible="false">
      <td height="40" align="right">民航代理人许可证(铜牌)：</td>
      <td align="left">&nbsp;</td>
      <td align="left"><uc1:SingleFileUpload ID="sfuCupreousImg" runat="server" /></td>
    </tr>
    <tr>
      <th height="55" align="right">&nbsp;</th>
      <td>&nbsp;</td>
      <td align="left">
        
          <asp:Button ID="btnFulInBack" runat="server" Text="完成进入后台" CssClass="fulill" 
              onclick="btnFulInBack_Click" />
          <asp:HiddenField ID="hfdBackUrl" runat="server" />
      </td>
   <%--   <input type="button" name="btnFulill" value="完成进入后台" style="background:url(images/subb.gif); width:270px; height:37px; border:none; font-size:14px; font-weight:bold; color:#ffffff;" />--%>
    </tr>
  </table>
</div>
      <script type="text/javascript">
     //初始化编辑器
     KE.init({
	    id : 'txaCompanyInfo',//编辑器对应文本框id
	    width : '550px',
	    height : '350px',
	    skinsPath:'/kindeditor/skins/',
	    pluginsPath:'/kindeditor/plugins/',
	    scriptPath:'/kindeditor/skins/',
        resizeMode : 0,//宽高不可变
	    items:keMore //功能模式(keMore:多功能,keSimple:简易)
    });   
    var sfu1,sfu2,sfu3;
    var submit=false;
    $(document).ready(function(){
     // $("#txaCompanyInfo").val(KE.html('txaCompanyInfo'));//获取编辑器内容并赋值到文本框
        setTimeout(
            function() {
                KE.create('txaCompanyInfo', 0); //创建编辑器
                //KE.html('txaCompanyInfo',htmlDecode($("#txaCompanyInfo2").html())) //赋值
            }, 100);
        $("#<%=btnFulInBack.ClientID%>").click(function(){  
            if(submit){           
            $("#txaCompanyInfo").val(KE.html('txaCompanyInfo'));//获取编辑器内容并赋值到文本框
               // $(this).attr("disabled", "disabled");
                $("#<%=btnFulInBack.ClientID%>").val("正在跳转，请稍候");
                return true;
            }
            sfu1=<%=sfuLogo.ClientID %>;
            sfu2=<%=sfuYyzzImg.ClientID %>;
            if(sfu1.getStats().files_queued>0){
                sfu1.customSettings.UploadSucessCallback = PerfectInfo.Pic2;
                sfu1.startUpload();        
            }else{
                PerfectInfo.Pic2();
            }
            return submit;
        });
     });
     var PerfectInfo={
        Pic2:function(){ 
          if(sfu2.getStats().files_queued>0){        
                sfu2.customSettings.UploadSucessCallback = PerfectInfo.Pic3;
                sfu2.startUpload();
          }else{
            PerfectInfo.Pic3();
          }
        },
        Pic3:function(){
            if($("#<%=trCuperous.ClientID %>").length>0){            
                sfu3=<%=sfuCupreousImg.ClientID %>;
                if(sfu3.getStats().files_queued>0){        
                sfu3.customSettings.UploadSucessCallback =PerfectInfo.save;
                sfu3.startUpload();
              }else{
               this.save();
              }
            }else{
                PerfectInfo.save();               
            }
        },
        save:function()
        {
             submit=true;
             $("#<%=btnFulInBack.ClientID %>").click();
        }
     }
   </script>
</asp:Content>
