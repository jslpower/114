<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MyOwenerShop.aspx.cs"
    Inherits="UserBackCenter.SupplyManage.MyOwenerShop" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/usercontrol/SingleFileUpload.ascx" TagName="sznb2" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<asp:content id="MyOwenerShop" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript" src="/kindeditor/kindeditor.js"  cache="true" ></script>
<script type="text/javascript">
 //初始化编辑器
 KE.init({
	id : 'txt_MyOwenerShop_Remark',//编辑器对应文本框id
	width : '550px',
	height : '270px',
	skinsPath:'/kindeditor/skins/',
	pluginsPath:'/kindeditor/plugins/',
	scriptPath:'/kindeditor/skins/',
    resizeMode : 0,//宽高不可变
	items:keSimple //功能模式(keMore:多功能,keSimple:简易)
});
</script>
<table id="tbl_MyOwenerShop" width="99%" height="500" border="0" cellpadding="0"
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
                                    <td class="tianon">
                                        <a href="/supplymanage/myowenershop.aspx" rel="MyOwenerShop" shoptabshow="0">单位信息 </a>
                                    </td>
                                    <td style="border-bottom: 1px solid #ABC9D9;">
                                        &nbsp;
                                    </td>
                                    <td style="border-bottom: 1px solid #ABC9D9;">
                                        <%--<a href="/supplymanage/uploadimagefile.aspx"  rel="MyOwenerShop" shoptabshow="1">图片上传 </a>--%>
                                    </td>
                                    <td style="border-bottom: 1px solid #ABC9D9;">
                                        &nbsp;
                                    </td>
                                    <td style="border-bottom: 1px solid #ABC9D9;">
                                        <%--<a href="<%=WebSiteUrl %>" target="_blank">预览我的网店</a>--%>
                                    </td>
                                </tr>
                            </table>                         
                            <table width="100%" id="tbl_MyOwenerShop_CompanyInfo" border="0" cellspacing="0"
                                cellpadding="0" style="border-bottom: 1px solid #ABC9D9; border-left: 1px solid #ABC9D9;
                                border-right: 1px solid #ABC9D9; height: 470px;">
                                <tr>
                                    <td align="left" valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="7%">
                                                    &nbsp;
                                                </td>
                                                <td width="93%" align="left">
                                                    <img src="<%=ImageServerUrl %>/images/danweixinxi.gif" width="87" height="36" />
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
                                                    <%--<input id="txt_MyOwenerShop_CompanyName" name="MyOwenerShop_CompanyName" style="width:350px;" readonly="readonly"
                                                        value="<%=CompanyName %>" type="text" />--%>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td width="16%" align="right" bgcolor="#F1F9FF">
                                                    推广网址：
                                                </td>
                                                <td width="84%" align="left">
                                                    <input id="txt_MyOwenerShop_WebSite"  class="shurukuang" name="MyOwenerShop_WebSite" valid="notHttpUrl" errmsg="你输入的推广网址格式不正确！" style="width:350px;"
                                                        value="<%=WebSite %>" type="text" />
                                                        <span id="errMsg_txt_MyOwenerShop_WebSite" class="errmsg"></span>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_ShortRemark">
                                                <td width="16%" align="right" bgcolor="#F1F9FF">
                                                    <span class="ff0000">*</span><%=ShortRmarkIntroduce %>：
                                                </td>
                                                <td width="84%" align="left">
                                                    <input id="txt_MyOwenerShop_ShortRemark" name="MyOwenerShop_ShortRemark" maxlength="100"  valid="required" errmsg="<%=ShortRmarkIntroduce %>不能为空！" class="bitian" style="width:600px;"
                                                        value="<%=ShortRemark %>" type="text" />
                                                        <span id="errMsg_txt_MyOwenerShop_ShortRemark" class="errmsg"></span>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_CompanyTag" visible="false">
                                                <td width="16%" align="right" bgcolor="#F1F9FF">
                                                    周边环境：
                                                </td>
                                                <td width="84%" align="left">                                                                                                     
                                                   <%=cbl_CompanyTagHtml%>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_CompanyLevel" visible="false">
                                                <td width="16%" align="right" bgcolor="#F1F9FF">
                                                    酒店星级：
                                                </td>
                                                <td width="84%" align="left">
                                                   <asp:RadioButtonList runat="server" id="rbl_HotelLevel"  RepeatDirection="Horizontal"></asp:RadioButtonList>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="F1F9FF">
                                                    公司文字介绍：
                                                </td>
                                                <td align="left">
                                                    <textarea name="MyOwenerShop_Remark" id="txt_MyOwenerShop_Remark" cols="1" rows="1" style="display:none;"><%=Remark %></textarea>
                                                    
                                                </td>
                                            </tr>
                                             <tr>
                                                <td height="22" align="right" bgcolor="#F1F9FF">
                                                    公司logo上传：
                                                </td>
                                                <td align="left" style="line-height:30px;">       
                                                <table><tr><td>
                                                 <uc2:sznb2 id="Upload_LogoImg" runat="server" ImageWidth="92" ImageHeight="84" />
                                                </td>
                                                <td>
                                                  &nbsp;&nbsp;&nbsp;<a href="<%=(LogImg==""?"":Domain.FileSystem+LogImg) %>" target="_blank" id="a_MyOwenerShop_logo"><%=(LogImg == "" ? "" : "查看图片")%></a>                                                                      
                                                   <input id="hidLogoImg" name="hidLogoImg" value="<%=LogImg %>" type="hidden" /> 
                                                   <span style="text-align:left;color:Blue">(请上传92*84像素大小的图片或同等比例图片)</span>
                                                </td></tr></table>                                            
                                                  
                                                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="22" align="right" bgcolor="#F1F9FF">
                                                    <span class="ff0000">*</span>联系人：
                                                </td>
                                                <td align="left">
                                                    <input name="MyOwenerShop_ConnectName" id="txt_MyOwenerShop_ConnectName"  valid="required" errmsg="联系人不能为空！" value="<%=ConnectName %>"
                                                        type="text" class="bitian" size="35" />
                                                        <span id="errMsg_txt_MyOwenerShop_ConnectName" class="errmsg"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="22" align="right" bgcolor="#F1F9FF">
                                                    <span class="ff0000">*</span>手机：
                                                </td>
                                                <td align="left">
                                                    <input name="MyOwenerShop_Mobile" id="txt_MyOwenerShop_ConnectMobile"  valid="required|isMobile" errmsg="手机号码不能为空！|你输入的手机号码格式不正确！" value="<%=Mobile %>"
                                                        type="text" class="bitian" size="35" />
                                                        <span id="errMsg_txt_MyOwenerShop_ConnectMobile" class="errmsg"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="22" align="right" bgcolor="#F1F9FF">
                                                    <span class="ff0000">*</span>电话：
                                                </td>
                                                <td align="left">
                                                    <input name="MyOwenerShop_ConnectTel"  valid="required|isPhone" errmsg="电话号码不能为空！|你输入的电话号码格式不正确！" id="txt_MyOwenerShop_ConnectTel" value="<%=Tel %>"
                                                        type="text" class="bitian" size="35" />
                                                        <span id="errMsg_txt_MyOwenerShop_ConnectTel" class="errmsg"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="22" align="right" bgcolor="#F1F9FF">
                                                    传真：
                                                </td>
                                                <td align="left">
                                                    <input name="MyOwenerShop_ConnectFax" id="txt_MyOwenerShop_ConnectFax" value="<%=Fax %>"
                                                        type="text" class="shurukuang" size="35" />                                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="22" align="right" bgcolor="#F1F9FF">
                                                    Email：
                                                </td>
                                                <td align="left">
                                                    <input name="MyOwenerShop_ConnectEmail" id="txt_MyOwenerShop_ConnectEmail"  valid="isEmail" errmsg="你输入的Email格式不正确！"  value="<%=Email %>"
                                                        type="text" class="shurukuang" size="35" />
                                                        <span id="errMsg_txt_MyOwenerShop_ConnectEmail" class="errmsg"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    <span class="ff0000">*</span>地区：<br />
                                                </td>
                                                <td align="left">
                                                    <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    <span class="ff0000">*</span>地址：
                                                </td>
                                                <td align="left">
                                                    <input name="MyOwenerShop_ConnectAddress"   valid="required" errmsg="联系地址不能为空！"  id="txt_MyOwenerShop_ConnectAddress" type="text"
                                                        class="bitian" value="<%=Address %>" size="60" />
                                                        <span id="errMsg_txt_MyOwenerShop_ConnectAddress" class="errmsg"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    &nbsp;
                                                </td>
                                                <td align="left" height="50">
                                                    <a href="javascript:void(0)" class="xiayiye" id="btn_MyOwenerShop_Save">保存</a><%--<a
                                                        href="javascript:void(0)" class="xiayiye" id="btn_MyOwenerShop_Cancel">取消</a>--%>
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
                    Upload_LogoImg:<%=Upload_LogoImg.ClientID %>,                             
                    save:function(){                     
                        $.newAjax({
                           type: "POST",
                           url: "/SupplyManage/MyOwenerShop.aspx?action=CompanyInfo",
                           data:$($("#tbl_MyOwenerShop_CompanyInfo").closest("form").get(0)).serialize(),
                           success: function(msg){                             
                             if(msg=="1")
                             {  
                                $("#imgLogo_supply_default").attr("src","<%=EyouSoft.Common.Domain.FileSystem %>"+$("#ctl00_ContentPlaceHolder1_Upload_LogoImg_hidFileName").val())
                                topTab.url(topTab.activeTabIndex,"/SupplyManage/MyOwenerShop.aspx");
                                alert("保存成功！");
                                
                             }else{
                                alert("对不起，保存失败！");
                             }
                           }
                        });
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
                    }              
                };
                $(document).ready(function(){                                      
                    FV_onBlur.initValid($("#tbl_MyOwenerShop").closest("form").get(0));
                    $("#tbl_MyOwenerShop a[rel]").click(function(){                        
                        topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                        return false;
                    });
                    $("#tbl_MyOwenerShop #btn_MyOwenerShop_Save").click(function(){                       
                        $("#txt_MyOwenerShop_Remark").val(encodeURIComponent(KE.html('txt_MyOwenerShop_Remark')));//获取编辑器内容并赋值到文本框
                        if(ValiDatorForm.validator($("#txt_MyOwenerShop_ConnectAddress").closest("form").get(0),"alertspan")){
                            MyOwenerShop.upFile_function(MyOwenerShop.Upload_LogoImg,MyOwenerShop.save);
                        }
                    });
                    setTimeout(function(){
                          KE.create('txt_MyOwenerShop_Remark',0);//创建编辑器
                          KE.html('txt_MyOwenerShop_Remark',htmlDecode($("#txt_MyOwenerShop_Remark").html())) //赋值
                     },50);                
                });
            </script>
</asp:content>
