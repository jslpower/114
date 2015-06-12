<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelersAdd.aspx.cs" Inherits="UserBackCenter.TravelersManagement.TravelersAdd" EnableEventValidation="false" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%--<%@ Register Src="~/usercontrol/ProvinceAndCityAndCounty.ascx" TagName="ProvinceAndCityAndCounty" TagPrefix="uc1" %>--%>
<asp:content id="ContentInfo" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'TravelersAdd'
    });
</script>
<script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("GetCityList") %>" ></script>
<div class="right">
       <table id="<%=tblID %>"  cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center" class="padd5 tablewidth" style="margin-top:5px;">
         <tbody><tr>
           <td width="199" height="28" align="right"><span class="ff0000">*</span>中文名 ：</td>
           <td width="660" height="28" align="left"><input name="txtNameCn" id="txtNameCn" valid="custom"  custom="TravelersAdd.checkName" errmsg="常旅客姓名不能为空,中文名和英文名任填其一！" size="30" type="text" runat="server"/>
           <span id="errMsg_<%=this.txtNameCn.ClientID%>" class="errmsg"></span></td>
         </tr>
         <tr>
           <td height="28" align="right">英文名：</td>
           <td height="28" align="left"><input name="txtNameEn" size="30" type="text" id="txtNameEn" runat="server"/>
           </td>
         </tr>
         <tr>
           <td height="28" align="right">旅客类型：</td>
           <td height="28" align="left">
               <asp:DropDownList runat="server" ID="ddlType"></asp:DropDownList>
           </td>
         </tr>
         <tr>
           <td height="28" align="right"><span class="ff0000">*</span>性别： </td>
           <td height="28" align="left">
            <input type="radio" name="rdSex" id="rdSex0" value="0" checked="checked"/>
             <label for="rdSex0">男</label>
             <input type="radio" name="rdSex" id="rdSex1" value="1"/>
             <label for="rdSex1">女</label>
            </td>
         </tr>
         <tr>
           <td height="28" align="right"><span class="ff0000">*</span>手机号码：</td>
           <td height="28" align="left">
           <input size="30" type="text" name="txtMobile" id="txtMobile" valid="required|isMobile" errmsg="手机号码必填！|号码格式不正确！" runat="server"/>
           <span id="errMsg_<%=this.txtMobile.ClientID%>" class="errmsg"></span>
           </td>
            
         </tr>
                  <tr>
           <td height="28" align="right">电话号码：</td>
           <td height="28" align="left"><input name="txtTel" id="txtTel" size="50" type="text" runat="server"/>
            
           </td>
         </tr>
         <tr>
           <td height="28" align="right">邮寄地址：</td>
           <td height="28" align="left"><input name="txtAddress" id="txtAddress" size="50" type="text" runat="server"/>
            
           </td>
         </tr>
         <tr>
           <td height="28" align="right">邮编：</td>
           <td height="28" align="left"><input name="txtPostCode" id="txtPostCode" type="text" runat="server" valid="isZip" errmsg="邮编格式不正确！"/>
            <span id="errMsg_<%=this.txtPostCode.ClientID%>" class="errmsg"></span>
           </td>
         </tr>
         <tr>
           <td height="28" align="right">生日：</td>
           <td height="28" align="left"><input name="txtBirth" id="txtBirth" size="30" type="text" runat="server" onfocus="WdatePicker({maxDate:'%y-%M-%d'})"/>
             格式：1980-01-01</td>
         </tr>
         <tr>
           <td height="28" align="right">身份证：</td>
           <td height="28" align="left"><input name="txtCardId" id="txtCardId" size="30" type="text" runat="server" valid="isIdCard" errmsg="身份证格式不正确！"/>
           <span id="errMsg_<%=this.txtCardId.ClientID%>" class="errmsg"></span>
           </td>
         </tr>
         <tr>
           <td height="28" align="right">护照：</td>
           <td height="28" align="left"><input name="txtPassport" id="txtPassport" size="30" type="text" runat="server"/></td>
         </tr>
         <tr>
           <td height="28" align="right">其它证件：</td>
           <td height="28" align="left">
                <asp:DropDownList runat="server" ID="ddlOtherCard"></asp:DropDownList>
             </td>
         </tr>
         <tr>
           <td height="28" align="right">证件号码：</td>
           <td height="28" align="left"><input name="txtCardNum" id="txtCardNum" size="30" type="text" runat="server"/></td>
         </tr>
         <tr>
           <td height="28" align="right">所在国家：</td>
           <td height="28" align="left">
            <asp:DropDownList runat="server" ID="ddlCountry" onchange="TravelersAdd.isSelectCn(this)"></asp:DropDownList>
             <span id="spanCnSelect" style="display:none;">
                    <%--<uc1:ProvinceAndCityAndCounty runat="server" ID="ProCityCountry"/>--%>
                   省份：<asp:DropDownList id="ProvinceList" runat="server"></asp:DropDownList>
                   城市：<asp:DropDownList id="CityList" runat="server"></asp:DropDownList>
                   县区：<asp:DropDownList runat="server" id="CountyList"></asp:DropDownList>
             </span>
             </td>
         </tr>
         <tr>
           <td height="28" align="right">备注：</td>
           <td height="28" align="left"><textarea rows="5" cols="80" name="txtRemark" id="txtRemark" runat="server"></textarea></td>
         </tr>
         <tr>
           <td height="48" align="center" colspan="2">
           <a class="baocun_btn" href="javascript:void(0);" id="btnSubmit">保 存</a> 
           <a class="baocun_btn" href="javascript:void(0);" onclick="TravelersAdd.resetForm()">重 置</a>
            <a class="baocun_btn" href="javascript:void(0);" onclick="topTab.url(topTab.activeTabIndex,'/TravelersManagement/TravelersList.aspx')">返 回</a>
           </td>
         </tr>
       </tbody></table>
     </div>
     <script type="text/javascript">
         var TravelersAdd = {
             _getData: function() {
                 return commonTourModuleData.get('<%=this.tblID %>');
             },
             checkName: function(obj) {
                 $conObj = $("#" + TravelersAdd._getData().ContainerID);
                 if ($.trim($conObj.find("#<%=this.txtNameCn.ClientID%>").val()) == "" && $.trim($conObj.find("#<%=this.txtNameEn.ClientID%>").val()) == "") {
                     return false;
                 }
                 else
                     return true;
             },
             isSelectCn: function(obj) {
                 $conObj = $("#" + TravelersAdd._getData().ContainerID);
                 if (obj.value == "224") {//选择“中国”时显示省市
                     $conObj.find("#spanCnSelect").show();
                 }
                 else {
                     $conObj.find("#spanCnSelect").hide();
                 }
             },
             resetForm: function() {
                 $conObj = $("#" + TravelersAdd._getData().ContainerID);
                 $conObj.find("#btnSubmit").closest("form").get(0).reset();
                 $conObj.find("#spanCnSelect").hide();
             },
             //设置选中性别
             setSex: function(v) {
                 $conObj.find("input[name='rdSex']").each(function() {
                     if (this.value == v)
                         this.checked = true;
                 });
             },
             setOptions: function(obj, value) {
                 $ops = $(obj).find("option");
                 for (var i = 0; i < $ops.length; i++) {
                     if ($($ops[i]).attr("value") == value) {
                         $($ops[i]).attr("selected", true);
                         break;
                     }
                 }
             },
             InitPlace:function(provinceID,cityID,countryID){
                var objProvince=document.getElementById("<%=this.ProvinceList.ClientID%>");
                var objCity=document.getElementById("<%=this.CityList.ClientID%>");
                var objCountry=document.getElementById("<%=this.CountyList.ClientID%>");
                if(provinceID!="")
                {
                    TravelersAdd.setOptions(objProvince, provinceID);
                    SetList("<%=this.CityList.ClientID%>", $(objProvince).val(), cityID);
                    if(cityID!="")
                    {
                        SetList('<%=this.CountyList.ClientID%>', 1, $(objCity).val(), countryID);
                    }
                }
              }
         };
         $(function() {
             $conObj = $("#" + TravelersAdd._getData().ContainerID);
             FV_onBlur.initValid($conObj.find("#btnSubmit").closest("form").get(0));
             $conObj.find("#btnSubmit").click(function() {
                if ($(this).attr("disabled") == "disabled") {
                     return false;
                 }
                 var validatorshow = ValiDatorForm.validator($conObj.find("#btnSubmit").closest("form").get(0), "span"); //获取提示信息
                 if (!validatorshow) {
                     return false;
                 }
                 $.newAjax({
                     url: '/TravelersManagement/TravelersAdd.aspx?type=save&method=ajax&id=<%=Request.QueryString["id"]%>',
                     data: $conObj.find("#btnSubmit").closest("form").serialize(),
                     cache: false,
                     type: "POST",
                     success: function(data) {
                         $conObj.find("#btnSubmit").removeAttr("disabled").html("保存");
                         alert(data);
                         topTab.url(topTab.activeTabIndex, '/TravelersManagement/TravelersList.aspx');
                     },
                     beforeSend: function() {
                         $conObj.find("#btnSubmit").html("保存中...").attr({ "disabled": "disabled" });
                     },
                     error: function() {
                         alert("服务器繁忙，请稍后再试！");
                         return;
                     }
                 });
             });
             TravelersAdd.isSelectCn(document.getElementById("<%=this.ddlCountry.ClientID%>"));
         });
     </script>
     </asp:content>