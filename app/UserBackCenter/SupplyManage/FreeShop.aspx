<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreeShop.aspx.cs" Inherits="UserBackCenter.SupplyManage.FreeShop" %>
    <%@ Import Namespace="EyouSoft.Common" %>
    <%@ Register Src="/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc2" %>
<asp:content id="FreeShop" runat="server" contentplaceholderid="ContentPlaceHolder1">
       <script type="text/javascript" src="/kindeditor/kindeditor.js"  cache="true" ></script>
<script type="text/javascript">
 //初始化编辑器
 KE.init({
	id : 'txt_FreeShop_Remark',//编辑器对应文本框id
	width : '610px',
	height : '270px',
	skinsPath:'/kindeditor/skins/',
	pluginsPath:'/kindeditor/plugins/',
	scriptPath:'/kindeditor/skins/',
    resizeMode : 0,//宽高不可变
	items:keSimple //功能模式(keMore:多功能,keSimple:简易)
});
</script> 
<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <table width="99%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px solid #ABC9D9;
                                border-left: 1px solid #ABC9D9; border-right: 1px solid #ABC9D9; height: 470px;">
                                <tr>
                                    <td align="left" valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="7%">
                                                    &nbsp;
                                                </td>
                                                <td width="93%" align="left">
                                                    <img src="<%=ImageServerUrl%>/images/danweixinxi.gif" width="87" height="36" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="1" align="center" cellpadding="2" cellspacing="0" bordercolor="#D0E8F8"
                                            style="border-bottom: 2px dashed #ccc; margin-bottom: 15px;">
                                            <tr>
                                                <td width="16%" align="right" bgcolor="#F1F9FF">
                                                    公司名称：
                                                </td>
                                                <td width="84%" align="left">
                                                    <%=CompanyName %>
                                                </td>
                                            </tr>
                                            <%if (isShowShortRemark)
                                              {%>
                                            <tr runat="server" id="tr_ShortRemark">

                                                            <td width="10%" bgcolor="#F1F9FF"  align="right">
                                                                <span class="ff0000">*</span><%=ShortRmarkIntroduce%>：
                                                            </td>
                                                            <td width="90%">
                                                                <input name="ShortRemark" id="txtShortRemark" maxlength="100" value="<%=ShortRemark %>" type="text"
                                                                    class="bitian" style="width: 600px;" />
                                                            </td>
                                            </tr>
                                            <%} %>
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    公司logo上传：
                                                </td>
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <uc2:SingleFileUpload ID="SingleFileUpload1" runat="server" ImageWidth="92" ImageHeight="84" />
                                                                <input id="hidCompanyImg" name="hidCompanyImg" value="<%=CompanyImgThumb %>" type="hidden" />
                                                            </td>
                                                            <td>
                                                                <a href="<%=Domain.FileSystem+CompanyImgThumb %>" target="_blank" id="a_showCompanyImg">
                                                                    <%=CompanyImgThumb!=""?"查看图片":"" %></a> <span style="text-align: left; color: Blue;">
                                                                        (请上传宽92*高84像素大小的图片)</span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    公司介绍：
                                                </td>
                                                <td align="left">
                                                    <textarea name="Remark" id="txt_FreeShop_Remark" cols="1" rows="1" style="display:none;"><%=Remark %></textarea>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    &nbsp;
                                                </td>
                                                <td align="left" height="50">
                                                    <a href="javascript:void(0)" class="xiayiye"  id="btnSupplerManageSave">保存</a>                                                    
                                                    <div id="SaveUploadFile">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <script type="text/javascript" language="javascript">        
        var SupplerManage={            
            save:function(){
                $.newAjax({
                   type: "POST",
                   url: "/SupplyManage/FreeShop.aspx?action=SupplerManage",
                   data:$($("#txt_FreeShop_Remark").closest("form").get(0)).serialize(),
                   success: function(msg){
                     var returnMsg=eval(msg);
                     if(returnMsg){                             
                         if(returnMsg[0].isSuccess){            
                            $("#SaveUploadFile").html("保存成功...").hide(2000).html("")            
                            alert(returnMsg[0].Message);
                            $("#imgLogo_supply_default").attr("src","<%=EyouSoft.Common.Domain.FileSystem %>"+$("#ctl00_ContentPlaceHolder1_SingleFileUpload1_hidFileName").val())
                            topTab.url(topTab.activeTabIndex,"/supplymanage/freeshop.aspx");                    
                         }else{
                            alert(returnMsg[0].Message);
                         }
                     }
                   },error:function(){
                        $("#SaveUploadFile").hide() 
                        alert("对不起，保存失败！");
                   }
                });
            },
            SingleFileUploadFunction:function(fileObj){
                 if(fileObj.getStats().files_queued>0)
                 {
                    fileObj.customSettings.UploadSucessCallback = SupplerManage.save;
                    fileObj.startUpload();
                 }
                else
                 {
                     SupplerManage.save();
                 }
            }
        };
        $(document).ready(function(){
            $("#btnSupplerManageSave").click(function(){
                $("#txt_FreeShop_Remark").val(encodeURIComponent(KE.html('txt_FreeShop_Remark')));//获取编辑器内容并赋值到文本框
                var SingleFile1=<%=SingleFileUpload1.ClientID %>;                
                if(document.getElementById("txtShortRemark")!=null&& $("#txtShortRemark").val()==""){
                    alert("<%=ShortRmarkIntroduce %>不能为空！");
                    return;
                }
                SupplerManage.SingleFileUploadFunction(SingleFile1);                
                return false;
            });
            setTimeout(function(){
                  KE.create('txt_FreeShop_Remark',0);//创建编辑器                                    
                  KE.html('txt_FreeShop_Remark',htmlDecode($("#txt_FreeShop_Remark").html())) //赋值
             },50);   
        });
        
    </script>
</asp:content>