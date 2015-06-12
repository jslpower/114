<%@ Page Language="C#" MasterPageFile="~/MasterPage/AirTicket.Master" AutoEventWireup="true" CodeBehind="VisitorsPage.aspx.cs" Inherits="UserPublicCenter.AirTickets.VisitorManage.VisitorsPage" Title="常旅客添加_机票" %>
<%@ Import Namespace="EyouSoft.Common" %>  
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="c1">
<link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
<style type="text/css">
.errmsg{color:red; font-size: 12px;}
</style>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script> 
    <div class="sidebar02_con_table01">
    	<table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8" bgcolor="#FAFAFA">
         <tr>
            <td  width="14%" height="30" align="left">中文名：</td>
            <td width="20%" align="left"><input name="txtCname" runat="server"  type="text" id="txtCname" size="20" />
            <span id="errMsg_ctl00_c1_txtCname" class="errmsg"></span></td>
            <td width="15%" align="left">英文名：</td>
            <td width="51%" align="left"><input name="txtEname" runat="server"  type="text" id="txtEname" size="20" />
            <span id="errMsg_ctl00_c1_txtEname" class="errmsg"></span></td>
          </tr>
          <tr>
            <td width="14%" height="30" align="left">旅客类型：</td>
            <td width="20%" align="left">
                <asp:DropDownList ID="ddlVisitoryType" name="ddlVisitoryType" runat="server"  valid="required" errmsg="请选择旅客类型">
                </asp:DropDownList>
                 <span id="errMsg_ctl00_c1_ddlVisitoryType" class="errmsg">
                 </td>
            <td width="15%" align="left">性别：</td>
            <td width="51%" align="left">                             
            <input name="rdoSex"  type="radio" id="gender_male" runat="server" checked="true" value="0"
            valid="requiredRadioed" errmsg="请选择性别" errmsgend="rdoSex" 
            />男
            <input type="radio" name="rdoSex" runat="server"  value="1" id="gender_female" />女
            <span class="errmsg" id="errMsg_rdoSex"></span>
            </td>
          </tr>
          <tr>
            <td height="30" align="left">证件类型：</td>
            <td align="left"><asp:DropDownList 
                    ID="ddlCardType" runat="server" name="ddlCardType" valid="required" errmsg="请选择证件类型">
                </asp:DropDownList>
              &nbsp;<span id="errMsg_ctl00_c1_ddlCardType" class="errmsg"></span></td>
            <td align="left">证件号码：</td>
            <td align="left"><input name="txtCardNo" runat="server"  type="text" id="txtCardNo" size="20" valid="required|custom" custom="isValidCardNo" errmsg="证件号码不能为空|请填写有效的身份证号码" />
            <span id="errMsg_ctl00_c1_txtCardNo" class="errmsg"></span></td>
          </tr>
          <tr>
            <td height="30" align="left">联系电话：</td>
            <td align="left">
            <input name="txtPhone" type="text" id="txtPhone" valid="required|isPhone" runat="server" errMsg="请输入联系电话|请输入正确的联系电话" size="20" />
            <span id="errMsg_ctl00_c1_txtPhone" class="errmsg"></span>
            </td>
            <td align="left">所有国家：</td>
            <td align="left">
                <asp:DropDownList ID="ddlCountry" name="ddlCountry" runat="server" valid="required" errmsg="请选择国家">
                </asp:DropDownList>
                <span id="errMsg_ctl00_c1_ddlCountry" class="errmsg">
                    </td>
          </tr>
          <tr>
            <td height="109" align="left">备注：</td>
            <td colspan="3" align="left">   
               <textarea name="txtRemark" id="txtRemark" runat="server" valid="required" errMsg="请输入备注" cols="65" rows="7">
              </textarea>    
               <span id="errMsg_ctl00_c1_txtRemark" class="errmsg"></span>  
            </td>
          </tr>
          <tr>
            <td height="40" colspan="4" align="center">        
                &nbsp; <asp:ImageButton ID="ImgSave"                      
                    width="88px" height="26px" runat="server" 
                    onclick="ImgSave_Click" />
              
              </td>
          </tr>
          <tr>
            <td height="45" colspan="4" align="center"><table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td width="24%" height="35" align="center">请选择本地Excel文件路径：</td>
                <td width="31%" align="center">
               <%-- 上传控件开始--%>
                    <div style="margin: 0px 10px;">
                        <div>
                            <input type="hidden" id="hidFileName" runat="server" />
                            <span runat="server" id="spanButtonPlaceholder"></span><span id="errMsg_<%=hidFileName.ClientID %>"
                                class="errmsg"></span>
                        </div>
                        <div id="divFileProgressContainer" runat="server">
                        </div>
                        <div id="thumbnails">
                        </div>
                    </div>
                    <%--上传控件结束--%>
                </td>
                <td width="24%" align="center">
                
                 <input type="button" value="批量添加客户" id="btnAddExcel" />
                 
                </td>
                <td width="21%" align="left">
                <a href="<%=Domain.ServerComponents %>/TouristModel/常旅客模板.xls">下载Excel模板</a>                            
                </td>
              </tr>
              <tr>
              <td colspan="4">
               <ul id="ulVistorList" style="margin:auto;">
               </ul>
              </td>
              </tr>
            </table></td>
          </tr>
        </table>
    </div>
 <script type="text/javascript">
 $(document).ready(function(){
    $("#<%=txtRemark.ClientID%>").focus(function()
    {
       $(this).val($.trim($(this).val()));
    });
    
 });
 //////////////////////////表单验证///////////////////////////
$(function(){	
	$("#<%=ImgSave.ClientID %>").click(function(){	
	    var validName;   
		var form = $(this).closest("form").get(0);
		var isValid=true;		
		var chinaNameObj=$("#ctl00_c1_txtCname");
		var englishNameObj=$("#ctl00_c1_txtEname");
		isValid=ValiDatorForm.validator(form,"span",null,false);
		validName=checkChinaName(chinaNameObj)&&checkEnglishName(englishNameObj)
		if(isValid==false || validName==false){
             return false;
		  }
		//点击按纽触发执行的验证函数
		//return ValiDatorForm.validator(form,"span",null,isValid);
	});	
	//初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
	FV_onBlur.initValid($("#<%=ImgSave.ClientID %>").closest("form").get(0));
});
////////////////////验证身份证号码是否合法////////////////////
function isValidCardNo(e,formElements){
    var carNo=$("#ctl00_c1_txtCardNo").val();
    carNo = $.trim(carNo);
    var reg1=/(^\d{15}$)|(^\d{17}[0-9Xx]$)/;//身份证正则表达式
    var cardType=$("#<%=ddlCardType.ClientID %>").val();
    var isValid = true;
    if(cardType=="0")//选择的类型是身份证,就验证
    {  
       if(carNo.match(reg1)){   
               isValid= true;
        }else{                               
            isValid= false;
        }   
    }
    return isValid;
}
/////////////////验证中文名////////////////
function checkChinaName(e){
    var Cname=$("#ctl00_c1_txtCname").val()    Cname=Cname.trim();	var Ename=$("#ctl00_c1_txtEname").val().trim();	var objMsg=$("#errMsg_ctl00_c1_txtCname");
	var checkChinaName=true;
	if(Ename!=""&&Cname!=""){	    objMsg.html("常旅客中文名和英文名只能填其中一个！");	    $("#errMsg_ctl00_c1_txtEname").html("");	    checkChinaName=false;	}else if(Cname==""&&Ename==""){	    objMsg.html("中文名不能为空！");	    checkChinaName=false;	}else if(Ename!=""&&Cname==""){	    $("#errMsg_ctl00_c1_txtEname").html("");	}else{	     $("#errMsg_ctl00_c1_txtEname").html("");	     objMsg.html("");	     checkChinaName=true;	     if(Cname!=""&&Ename=="") 		 {		       var reg3=/^[\u0391-\uFFE5]+$/;//中文名正则，形如：张三		       if(Cname.match(reg3)){		         if(Cname.length<=13)		         {		              objMsg.html("");		              checkChinaName=true;		         }		         else		         {		              objMsg.html("中文名的长度不能超过13位!");		              checkChinaName=false;		         }		       }		       else		       {			    objMsg.html("请输入正确的中文名！");			    checkChinaName=false;		       }		 }	}
	return checkChinaName;	
}
////////////////////////////验证英文名////////////////////////////
function checkEnglishName(e){
        var Cname=$("#ctl00_c1_txtCname").val().trim();		var Ename=$("#ctl00_c1_txtEname").val().trim();		var objMsg=$("#errMsg_ctl00_c1_txtEname");		var checkEnglishName=true;		if(Ename!=""&&Cname!=""){		   objMsg.html("常旅客中文名和英文名只能填其中一个！！");		   $("#errMsg_ctl00_c1_txtCname").html("");		   checkEnglishName=false;		}else if(Ename==""&&Cname==""){		   objMsg.html("英文名不能为空！");		   checkEnglishName=false;		}else if(Ename==""&&Cname!=""){		    $("#errMsg_ctl00_c1_txtCname").html("");		}else{		  $("#errMsg_ctl00_c1_txtCname").html("");		  objMsg.html("");		  checkEnglishName=true;          if(Ename!=""&&Cname=="")	      {		      		       var reg4=/^[a-zA-Z]+\/[a-zA-Z]+$/;//英文名正则,形如：zhang/san		       if(Ename.match(reg4))		       {		         if(Ename.length<=27)			         {	         		            objMsg.html("");			         	        		        		            checkEnglishName=true;		         }		         else		         {		            objMsg.html("英文名的长度不能超过27位!");			         	        		        		            checkEnglishName=false;		         }		       }		       else		       {		         objMsg.html("请输入正确的英文名！");		         checkEnglishName=false;		       }		   }		}
		return checkEnglishName;
}
////////////////////////////中文名和英文名失去焦点事件///////////////
$("#ctl00_c1_txtCname").blur(function(){return checkChinaName(this);});
$("#ctl00_c1_txtEname").blur(function(){return checkEnglishName(this);});
</script>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
 <script type="text/javascript">
        function uploadSuccess1(file, serverData) {          
            try {               
                var obj = eval("["+serverData+"]");              
                if(serverData=="001")
                {
                    alert("成功导入所有数据！")
                    window.location.href="VisitorList.aspx";
                }
               else if(serverData!="001" && serverData!="0" && serverData!="-1")
                {
                   alert("成功导入"+serverData+"条数据,部分数据已存在!")
                   window.location.href="VisitorList.aspx";
                }                   
                else if(serverData=="0")
                {
                   alert("数据已存在，导入失败！");
                   window.location.href="VisitorsPage.aspx";
                }
                else//异常数据
                {
                   alert("文档格式不正确，未能导入数据!请下载Excel模板！");
                   window.location.href="VisitorsPage.aspx";
                }
                if (obj.error) {
                    return;
                }
                else {

                    var progress = new FileProgress(file, this.customSettings.upload_target);
                    progress.setStatus("上传成功.");
                    //                        
                    resetSwfupload(fileupload1, file);
                }
            }
            catch (ex) {
                 alert("上传失败！");
             }
        }
	var fileupload1;
    fileupload1 = new SWFUpload({
	    // Backend Settings
	    upload_url: "<%=Domain.UserPublicCenter %>/AirTickets/VisitorManage/GetVisitorList.ashx?type=saveexcel&CompanyId=<%=CompanyId %>",
	    file_post_name:"Filedata",
        post_params : {
            "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO %>": "<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO].Value %>",
            "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName].Value %>",
            "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_Password %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password]!=null?Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password].Value:string.Empty %>"
        },

	    // File Upload Settings
	    file_size_limit : "1 MB",
	    file_types : "*.xls;*.xlsx",
	    file_types_description : "Excel文件",
	    file_upload_limit : "1",    // Zero means unlimited

	    // Event Handler Settings - these functions as defined in Handlers.js
	    //  The handlers are not part of SWFUpload but are part of my website and control how
	    //  my website reacts to the SWFUpload events.
	    swfupload_loaded_handler :swfUploadLoaded,
	    file_dialog_start_handler : fileDialogStart,
	    file_queued_handler : fileQueued,
	    file_queue_error_handler : fileQueueError,
	    file_dialog_complete_handler : fileDialogComplete,
	    upload_progress_handler : uploadProgress,
	    upload_error_handler : uploadError,
	    upload_success_handler : uploadSuccess1,
	    upload_complete_handler : uploadComplete,

	    // Button settings
	    button_image_url : "<%=ImageServerUrl%>/images/swfupload/XPButtonNoText_160x22.png",
	    button_placeholder_id : "<%=spanButtonPlaceholder.ClientID %>",
	    button_width: 160,
	    button_height: 22,
	    button_text : '<span class="button">选择Excel文件<span class="buttonSmall">(最大1 MB)</span></span>',
	    button_text_style : '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
	    button_text_top_padding: 1,
	    button_text_left_padding: 5,
	    button_cursor: SWFUpload.CURSOR.HAND, 

	    // Flash Settings
	    flash_url : "<%=ImageServerUrl%>/js/swfupload/swfupload.swf",	// Relative to this file

	    custom_settings : {
		    upload_target : "<%=divFileProgressContainer.ClientID %>",
	        HidFileNameId:"<%=hidFileName.ClientID %>",
	        ErrMsgId:"errMsg_<%=hidFileName.ClientID %>",
	        UploadSucessCallback:null
	    },

	    // Debug Settings
	    debug: false,
		
	    // SWFObject settings
        minimum_flash_version : "9.0.28",
        swfupload_pre_load_handler : swfUploadPreLoad,
        swfupload_load_failed_handler : swfUploadLoadFailed
    });	    
		   // } );
 
    $("#btnAddExcel").click(function(){
        if(fileupload1.getStats().files_queued>0){  ;
            fileupload1.startUpload();            
        }else{
            alert("请选择Excel文件！");
            return false;
        }
    });
</script>
</asp:Content>
