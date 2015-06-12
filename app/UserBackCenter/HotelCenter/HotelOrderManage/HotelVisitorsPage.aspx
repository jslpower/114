<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelVisitorsPage.aspx.cs" Inherits="UserBackCenter.HotelCenter.HotelOrderManage.HotelVisitorsPage" %>

<asp:Content id="HotelVisitorsPage" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">   	
   	<table id="tb_hvp_HotelVisitorsPage" width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td align="left" valign="top" >		
		<table width="815" border="0" cellpadding="0" cellspacing="0">
		<tr>
            <td height="35" align="left"  class="pand" style="font-size:14px;"><strong><font color="#003C61"><%=FlageName%></font></strong><strong><font color="#0000FF"><a href="frequent-flyer_add.html"></a></font></strong></td>
            </tr>
        </table>		
		<table width="815" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#eef7ff" style="border:1px solid #AACFE6;">
         <tr>
            <td width="233" align="right">&nbsp;</td>
            <td width="580">&nbsp;<input id="hvp_hfEditId" runat="server" type="hidden" /></td>
          </tr>
          <tr>
            <td width="233" height="28" align="right">中文名 ：</td>
            <td height="28" align="left">
            <input name="hvp_txtChinaName" runat="server" id="hvp_txtChinaName" size="30" />
            <span id="errMsg_ctl00_ContentPlaceHolder1_hvp_txtChinaName" class="errmsg">*常旅客姓名不能为空,中文名和英文名任填其一！</span>
            </td>
          </tr>
          <tr>
            <td height="28" align="right">英文名：</td>
            <td height="28" align="left">
            <input name="hvp_txtEnglishName" runat="server" id="hvp_txtEnglishName" size="30" />
            <span id="errMsg_ctl00_ContentPlaceHolder1_hvp_txtEnglishName" class="errmsg"></span>
            </td>
          </tr>
          <tr>
            <td height="28" align="right">旅客类型：</td>
            <td height="28" align="left">
           <%-- valid="required" errmsg="请选择旅客类型"
            <span id="errMsg_ctl00_ContentPlaceHolder1_hvp_ddlVisitorType" class="errmsg"></span>--%>
            <asp:DropDownList ID="hvp_ddlVisitorType" runat="server" ></asp:DropDownList>            
            </td>
          </tr>
          <tr>
            <td height="28" align="right"><span  class="errmsg">*</span>性别： </td>
            <td height="28" align="left">
            <input id="hvp_rdoMale"  type="radio"  name="hvp_rdoGender" runat="server" checked="true" value="0" 
            valid="requiredRadioed" errmsg="*请选择性别" errmsgend="hvp_rdoGender" />男
            <input id="hvp_rdoFemale" type="radio" name="hvp_rdoGender" runat="server" value="1"/>女   
            <span class="errmsg" id="errMsg_hvp_rdoGender"></span>
            </td>
          </tr>
		  <tr>
            <td height="28" align="right">证件类型： </td>
            <td height="28" align="left">
            <asp:DropDownList ID="hvp_ddlCardType" runat="server"></asp:DropDownList>
            <span id="errMsg_ctl00_ContentPlaceHolder1_hvp_ddlCardType" class="errmsg"></span>
             </td>
          </tr>		  
		  <tr>
            <td height="28" align="right">证件号码：</td>
            <td height="28"align="left">
            <input name="hvp_txtCardNo" runat="server" id="hvp_txtCardNo" size="50" 
            valid="custom" custom="isValidCardNo" errmsg="*请填写有效的身份证号码"/>
            <span id="errMsg_ctl00_ContentPlaceHolder1_hvp_txtCardNo" class="errmsg"></span>
            </td>
          </tr>
          <tr>
            <td height="28" align="right">所在国家：</td>
            <td height="28" align="left">
            <asp:DropDownList ID="hvp_ddlCountry" runat="server"></asp:DropDownList>
            </td>
          </tr>		  
          <tr>
            <td height="28" align="right"><span  class="errmsg">*</span>手机号码：</td>
            <td height="28"align="left">
            <input name="hvp_txtMobilePhone" runat="server" id="hvp_txtMobilePhone" size="30"
            valid="required|isMobile" runat="server" errMsg="*请输入手机号码|*请输入正确的手机号码" />
            <span id="errMsg_ctl00_ContentPlaceHolder1_hvp_txtMobilePhone" class="errmsg"></span>
            </td>
          </tr>
		  <tr>
            <td height="28" align="right">备注：</td>
            <td height="28"align="left">
            <textarea name="hvp_txtReamrk" runat="server" id="hvp_txtReamrk" rows="5" cols="80"></textarea></td>
          </tr>
          <tr>
            <td height="48" align="right">&nbsp;</td>
            <td height="48"><a href='javascript:void(0)' id="hvp_btnSave" >
            <img src="<%=ImageServerUrl %>/images/hotel/userBackCenter/baocun_btn.jpg" alt="保存" width="79" height="25" border="0" /></a> 
            <a href='javascript:void(0)'  >
            <img id="hvp_reset" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/admin_orderform_ybans_05.jpg" alt="重置" width="79" height="25" />
           </a>
            </td>
          </tr>
        </table>		
		</td>
      </tr></table>
<script type="text/javascript">
 //////////////////////////表单验证///////////////////////////
$(function(){	
	$("#hvp_btnSave").click(function(){
	    var validName;   
		var form = $(this).closest("form").get(0);
		var isValid=true;		
		var chinaNameObj=$("#ctl00_ContentPlaceHolder1_hvp_txtChinaName");
		var englishNameObj=$("#ctl00_ContentPlaceHolder1_hvp_txtEnglishName");	
		isValid=ValiDatorForm.validator(form,"span",null,false);
		validName=checkVisitorName(chinaNameObj,englishNameObj);
		if(isValid==false || validName==false ){           
             return false;
		}else{
		    HotelVisitorsPage.SaveHotelVisitor();		
		}
	});	
	//初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
	FV_onBlur.initValid($("#hvp_btnSave").closest("form").get(0));	
});
////////////////////验证身份证号码是否合法////////////////////
function isValidCardNo(e,formElements){
    var carNo=$("#ctl00_ContentPlaceHolder1_hvp_txtCardNo").val();
    carNo = $.trim(carNo);
    var reg1=/(^\d{15}$)|(^\d{17}[0-9Xx]$)/;//身份证正则表达式
    var cardType=$("#<%=hvp_ddlCardType.ClientID %>").val();
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
///////////////////////////
function checkVisitorName(cName,eName)
{
   var Cname=$("#ctl00_ContentPlaceHolder1_hvp_txtChinaName").val().trim();
   var Ename=$("#ctl00_ContentPlaceHolder1_hvp_txtEnglishName").val().trim();
   var objMsg=$("#errMsg_ctl00_ContentPlaceHolder1_hvp_txtChinaName");
   var checkChinaName=true;
   if(Cname==""&&Ename=="")
   {
      objMsg.html("*常旅客姓名不能为空,中文名和英文名任填其一！");
      checkChinaName=false;	
   }
   if((Cname==""&&Ename!="")||(Cname!=""&&Ename==""))
   {
      objMsg.html("");
      checkChinaName=true;	
   }
   return checkChinaName;
}
////////////////////////////中文名和英文名失去焦点事件///////////////
$("#ctl00_ContentPlaceHolder1_hvp_txtChinaName").blur(function(){
   var Cname=$("#ctl00_ContentPlaceHolder1_hvp_txtChinaName").val().trim();
   var Ename=$("#ctl00_ContentPlaceHolder1_hvp_txtEnglishName").val().trim();
   return checkVisitorName(Cname,Ename);
});
$("#ctl00_ContentPlaceHolder1_hvp_txtEnglishName").blur(function(){
   var Cname=$("#ctl00_ContentPlaceHolder1_hvp_txtChinaName").val().trim();
   var Ename=$("#ctl00_ContentPlaceHolder1_hvp_txtEnglishName").val().trim();
   return checkVisitorName(Cname,Ename);
});
///////////////////////////////////////保存常旅客////////////////////////////////
var HotelVisitorsPage={
    SaveHotelVisitor:function(){
            var form = $("#tb_hvp_HotelVisitorsPage").closest("form").get(0);   
         $.newAjax({
           url:"/HotelCenter/HotelOrderManage/HotelVisitorsPage.aspx",
           data:$(form).serialize().replace(/&Input=/,'')+"&method=save",
           cache:false,
           type:"post",
           success:function(result){
               if(result=="2")
               {      
                  alert("新增成功！");             
                  topTab.url(topTab.activeTabIndex,"/HotelCenter/HotelOrderManage/HotelVisitorManage.aspx");
               }
               else if(result=="-2")
               {
                  alert("新增失败！");             
                  topTab.url(topTab.activeTabIndex,"/HotelCenter/HotelOrderManage/HotelVisitorsPage.aspx");
               }
                else if(result=="-1")
               {      
                  alert("该常旅客已存在！");             
                  topTab.url(topTab.activeTabIndex,"/HotelCenter/HotelOrderManage/HotelVisitorsPage.aspx");
               }
               else if(result=="1")
               {      
                  alert("修改成功！");             
                  topTab.url(topTab.activeTabIndex,"/HotelCenter/HotelOrderManage/HotelVisitorManage.aspx");
               }else
               {
                  alert("修改失败！");             
                  topTab.url(topTab.activeTabIndex,"/HotelCenter/HotelOrderManage/HotelVisitorsPage.aspx");
               }
           },
           error:function(){
                 alert("操作失败！");
                 return ;   
             }
         });
    }
}
//////////////////////////////////////////////重置///////////////////////////
$(document).ready(function(){
        $("#hvp_reset").click(function(){
               var myForm = $(this).closest("form").get(0);
                myForm.reset();
//                return false;
//               $("#tb_hvp_HotelVisitorsPage input[type=text]").each(function(){
//                 $(this).val("");             
//             });
//               $("#ctl00_ContentPlaceHolder1_hvp_txtReamrk").val("");
//               $("#<%=hvp_ddlCardType.ClientID %>").val("-请选择-");
//               $("#<%=hvp_ddlVisitorType.ClientID %>").val("-请选择-");
//               $("#<%=hvp_ddlCountry.ClientID %>").val("-请选择-");
           });
}); 
</script>
</asp:Content>