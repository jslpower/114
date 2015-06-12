<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadImageFile.aspx.cs"
    Inherits="UserBackCenter.SupplyManage.UploadImageFile" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/usercontrol/SingleFileUpload.ascx" TagName="sznb2" TagPrefix="uc2" %>
<asp:content id="UploadImageFile" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table id="tbl_UploadImageFile" width="99%" height="500" border="0" cellpadding="0"
        cellspacing="0" class="tablewidth">
        <tr>
            <td valign="top">
       
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <table width="100%" height="33" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="border-bottom: 1px solid #ABC9D9; width: 15px;">
                                        &nbsp;
                                    </td>
                                    <td class="tianup">
                                        <a href="/supplymanage/myowenershop.aspx" rel="UploadImageFile" shoptabshow="0">单位信息 </a>
                                    </td>
                                    <td style="border-bottom: 1px solid #ABC9D9; width: 10px;">
                                        &nbsp;
                                    </td>
                                    <td class="tianon">
                                        <a href="/supplymanage/uploadimagefile.aspx" rel="UploadImageFile" shoptabshow="1">图片上传 </a>
                                    </td>
                                    <td style="border-bottom: 1px solid #ABC9D9; width: 10px;">
                                        &nbsp;
                                    </td>
                                    <td style="border-bottom: 1px solid #ABC9D9;">
                                        <a href="<%=WebSiteUrl %>" target="_blank">预览我的网店</a>
                                    </td>
                                </tr>
                            </table>                                                     
                            <table width="100%" id="tbl_MyOwenerShop_Pic" border="0" cellspacing="0" cellpadding="0"
                                style="border-bottom: 1px solid #ABC9D9; border-left: 1px solid #ABC9D9;
                                border-right: 1px solid #ABC9D9; height: 470px;">
                                <tr>
                                    <td align="left" valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="28" background="../images/managertopbjmm.gif">
                                                    &nbsp;<strong>图片维护</strong>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#D0E8F8"
                                            style="border-bottom: 2px dashed #ccc; margin-bottom: 15px;">
                                            <tr>
                                                <td width="16%" align="right" bgcolor="#F1F9FF">
                                                    网店展示图片：                                                                                     
                                                </td>
                                                <td width="84%" align="left">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:192px;">
                                                               <uc2:sznb2 id="Upload_CompanyImg" runat="server" ImageWidth="970" ImageHeight="200" />
                                                               
                                                             <input id="File5" name="hidUpload_CompanyImg" value="<%=CompanyImg %>" type="hidden" />
                                                            </td>
                                                            <td colspan="2">      
                                                            <a href="<%=Domain.FileSystem+CompanyImg %>" target="_blank"><%=CompanyImg == "" ? "" : "查看图片"%></a>                                                         
                                                                     <span style="text-align:left; color:Blue">(宽970像素，高小于200像素)</span>
                                                            </td>                                                                                                                 
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    风景图片：<br /> 
                                                    <span style="text-align:left;color:Blue">(宽150*高100像素)</span>                                                                                                                                                         
                                                </td>
                                                <td align="left"  colspan="2">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="10" >
                                                                1
                                                            </td>
                                                            <td  style="width:180px;">
                                                                <uc2:sznb2 id="Upload_ProductInfo1" runat="server"  ImageWidth="150" ImageHeight="100" />
                                                                <input id="File1" name="hid_MyOwenerShop_Upload_ProductInfo1" value="<%=ProductInfoImg1 %>" type="hidden" />                                                                
                                                                <input id="Hidden1" name="hid_MyOwenerShop_ProductInfoID1" value="<%=ProductInfoID1 %>" type="hidden" />                                                                
                                                                
                                                            </td>
                                                             <td  style="width:60px;">
                                                            <a href="<%=Domain.FileSystem+ProductInfoImg1 %>"  target="_blank"><%=ProductInfoImg1 == "" ? "" : "查看图片"%></a>
                                                            </td>
                                                            <td align="left">
                                                                链接
                                                                <input type="text" name="Shop_ProductInfoLink1"  style="width:220px;"  class="shurukuang"  valid="notHttpUrl" errmsg="你输入的风景图片一链接网址格式不正确！" value="<%=ProductInfoLink1 %>" id="txt_Shop_ProductInfoLink1" />
                                                            </td>
                                                                                                              
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                2
                                                            </td>
                                                            <td  style="width:180px;">
                                                              <uc2:sznb2 id="Upload_ProductInfo2" runat="server"  ImageWidth="150" ImageHeight="100" />
                                                              <input id="File2" name="hid_MyOwenerShop_Upload_ProductInfo2" value="<%=ProductInfoImg2 %>" type="hidden" />
                                                              <input id="Hidden2" name="hid_MyOwenerShop_ProductInfoID2" value="<%=ProductInfoID2 %>" type="hidden" />                                                                
                                                            </td>
                                                            <td style="width:60px;">
                                                            <a href="<%=Domain.FileSystem+ProductInfoImg2 %>"  target="_blank"><%=ProductInfoImg2 == "" ? "" : "查看图片"%></a>
                                                            </td>
                                                            <td  align="left">
                                                                链接
                                                                <input type="text" name="Shop_ProductInfoLink2"  style="width:220px;"  class="shurukuang"  valid="notHttpUrl" errmsg="你输入的风景图片二链接网址格式不正确！"   value="<%=ProductInfoLink2 %>"  id="txt_Shop_ProductInfoLink2" />
                                                            </td>
                                                                    
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                3
                                                            </td>
                                                            <td  style="width:180px;">
                                                                <uc2:sznb2 id="Upload_ProductInfo3" runat="server"  ImageWidth="150" ImageHeight="100" />
                                                                 <input  name="hid_MyOwenerShop_Upload_ProductInfo3"  value="<%=ProductInfoImg3 %>" type="hidden" />
                                                                <input id="Hidden3" name="hid_MyOwenerShop_ProductInfoID3" value="<%=ProductInfoID3 %>" type="hidden" />                                                                
                                                            </td>
                                                             <td style="width:60px;">
                                                            <a href="<%=Domain.FileSystem+ProductInfoImg3 %>"  target="_blank"><%=ProductInfoImg3 == "" ? "" : "查看图片"%></a>
                                                            </td>
                                                            <td  align="left">
                                                                链接
                                                                <input type="text" id="txtShop_ProductInfoLink3"  style="width:220px;"  class="shurukuang" name="Shop_ProductInfoLink3"  valid="notHttpUrl" errmsg="你输入的风景图片三链接网址格式不正确！" value="<%=ProductInfoLink3 %>"  id="txt_Shop_ProductInfoLink3" />
                                                            </td>                                                                                                           
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                4
                                                            </td>
                                                            <td style="width:180px;">
                                                             <uc2:sznb2 id="Upload_ProductInfo4" runat="server" ImageWidth="150" ImageHeight="100" />
                                                             <input id="File4" name="hid_MyOwenerShop_Upload_ProductInfo4" value="<%=ProductInfoImg4 %>" type="hidden" />                                                             
                                                                <input id="Hidden4" name="hid_MyOwenerShop_ProductInfoID4" value="<%=ProductInfoID4 %>" type="hidden" />                                                                
                                                            </td>
                                                               <td style="width:60px;">
                                                            <a href="<%=Domain.FileSystem+ProductInfoImg4 %>"  target="_blank"><%=ProductInfoImg4 == "" ? "" : "查看图片"%></a>
                                                            </td>
                                                            <td  align="left">
                                                                链接
                                                                <input type="text" name="Shop_ProductInfoLink4" style="width:220px;"  class="shurukuang" value="<%=ProductInfoLink4 %>" valid="notHttpUrl" errmsg="你输入的风景图片四链接网址格式不正确！"   id="txt_Shop_ProductInfoLink4" />
                                                            </td>                                                                                                                 
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    &nbsp;
                                                </td>
                                                <td align="left" height="50">
                                                    <a href="javascript:void(0)" class="xiayiye" id="btn_Shop_ProductImageSave">保存</a>
                                                    <div id="UploadImageFile_SaveUploadFile" style="display:inline; color:Red;"></div>                                                    
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
 
 <script language="javascript" type="text/javascript">
                var MyOwenerShop={
                    Upload_CompanyImg:<%=Upload_CompanyImg.ClientID %>,
                    Upload_ProductInfo1:<%=Upload_ProductInfo1.ClientID %>,
                    Upload_ProductInfo2:<%=Upload_ProductInfo2.ClientID %>,
                    Upload_ProductInfo3:<%=Upload_ProductInfo3.ClientID %>,
                    Upload_ProductInfo4:<%=Upload_ProductInfo4.ClientID %>,                                       
                    upFile_CompanyImg:function(){
                        MyOwenerShop.upFile_function(MyOwenerShop.Upload_CompanyImg,MyOwenerShop.upFile_ProductInfo1);
                    },
                    upFile_ProductInfo1:function(){
                        MyOwenerShop.upFile_function(MyOwenerShop.Upload_ProductInfo1,MyOwenerShop.upFile_ProductInfo2);
                    },
                    upFile_ProductInfo2:function(){
                        MyOwenerShop.upFile_function(MyOwenerShop.Upload_ProductInfo2,MyOwenerShop.upFile_ProductInfo3);
                    },
                    upFile_ProductInfo3:function(){
                        MyOwenerShop.upFile_function(MyOwenerShop.Upload_ProductInfo3,MyOwenerShop.upFile_ProductInfo4);
                    },
                    upFile_ProductInfo4:function(){
                        MyOwenerShop.upFile_function(MyOwenerShop.Upload_ProductInfo4,MyOwenerShop.imgInfoSave);
                    },
                    upFile_function:function(fileObj,callBack){                         
                         if(fileObj.getStats().files_queued>0)
	                     {
                            fileObj.customSettings.UploadSucessCallback = callBack;
                            fileObj.startUpload();
                         }
                        else
                         {
                             callBack();
                         }
                    },
                    imgInfoSave:function(){
                        $("#UploadImageFile_SaveUploadFile").html("正在保存，请稍后...")                        
                        $.newAjax({
                           type: "POST",
                           url: "/SupplyManage/UploadImageFile.aspx?action=SupplyImageSave",
                           data:$($("#txtShop_ProductInfoLink3").closest("form").get(0)).serialize(),
                           success: function(msg){
                             var returnMsg=eval(msg);
                             if(returnMsg)
                             {
                                $("#UploadImageFile_SaveUploadFile").html("保存成功...").hide(2000).html("")   
                                topTab.url(topTab.activeTabIndex,"/supplymanage/uploadimagefile.aspx"); 
                                alert(returnMsg[0].ErrorMessage)
                             }else{
                                $("#UploadImageFile_SaveUploadFile").html("保存失败...").hide()
                                alert('对不起，保存失败！')
                             }                             
                           },error:function(){
                                $("#UploadImageFile_SaveUploadFile").hide();
                                alert("对不起，保存失败！");
                           }
                        });
                    },
                    cancel:function(){
                        topTab.open("/supplymanage/default.aspx","首页",{isRefresh:false});
                    }                 
                };
                $(document).ready(function(){
                    $("#tbl_UploadImageFile a[rel='UploadImageFile']").click(function(){
                        topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                        return false;
                    });
                    $("#btn_Shop_ProductImageSave").click(function(){
                        if(ValiDatorForm.validator($("#txt_Shop_ProductInfoLink4").closest("form").get(0),"alert")){                        
                            MyOwenerShop.upFile_CompanyImg();
                        }
                    });
                });
            </script>
</asp:content>
