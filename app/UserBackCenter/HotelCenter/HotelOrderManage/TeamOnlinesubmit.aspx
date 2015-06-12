<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamOnlinesubmit.aspx.cs"
    Inherits="UserBackCenter.HotelCenter.HotelOrderManage.TeamOnlinesubmit" EnableEventValidation="false"%>
 <%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="TeamOnlinesubmit" contentplaceholderid="ContentPlaceHolder1" runat="server"> 
<link href="<%=CssManage.GetCssFilePath("tipsy") %>" rel="stylesheet" type="text/css" />
    <div class="tablebox">
         <table width="100%" cellspacing="0" cellpadding="0" bordercolor="#9dc4dc" border="1" class="padd5 margintop5">
           <tbody><tr>
             <td width="25%" height="28" align="right"><span class="ff0000">*</span>入住时间： </td>
             <td width="75%" height="28" align="left">
                <input id="startDateTime" name="startDateTime"  type="text" onfocus="WdatePicker({minDate:'%y-%M-#{%d}',onpicked:function(){$('#errMsg_startDateTime').html('');}})" valid="required" errmsg="请填写入住时间"/>
                 <span id="errMsg_startDateTime" class="errmsg"></span>——
               <input id="endDateTime" name="endDateTime"  type="text" onfocus="WdatePicker({minDate:'%y-%M-#{%d}',onpicked:function(){$('#errMsg_endDateTime').html('');}})" valid="required" errmsg="请填写离店时间"/>
               <span id="errMsg_endDateTime" class="errmsg"></span>  
            </td>
           </tr>
           <tr>
             <td height="28" align="right">城市： </td>
             <td height="28" align="left">
                  <input name="txtCity" type="text" id="txtCity" size="7" autocomplete="off" style="width: 130px;"/>                              
                  <asp:DropDownList ID="tos_destination" runat="server"></asp:DropDownList>
             </td>
           </tr>
           <tr>
             <td height="28" align="right">地址位置要求： </td>
             <td height="28" align="left"><input name="tos_statusrequirement" type="text" id="tos_statusrequirement" size="60" /></td>
           </tr>
           <tr>
             <td height="28" align="right">是否有指定酒店： </td>
             <td height="28" align="left">
                    <span class="pandl">
                    <input type="radio" id="tos_result1"  name="tos_result" value="1"  onclick="TeamOnlinesubmit.isShowEnd();" checked="checked"/>
                    <label for="tos_result1" style="cursor: pointer">是</label>
                    <input type="radio" id="tos_result2" name="tos_result" value="2"  onclick="TeamOnlinesubmit.isShowEnd();"/></span>
                    <label for="tos_result2" style="cursor: pointer">否</label>
             </td>
           </tr>
           <tr>
             <td height="28" align="right"><span class="ff0000">*</span>指定酒店名称： </td>
             <td height="28" align="left">
                   <input type="text" id="tos_HotelName" name="tos_HotelName" size="40" style="color:Gray;" valid="required" errmsg="请填写酒店官方名称"/>
                   <span id="errMsg_tos_HotelName" class="errmsg"></span>
             </td>
           </tr>
           <tr>
             <td height="28" align="right">星级要求： </td>
             <td height="28" align="left"><asp:DropDownList ID="tos_starrequirement" runat="server"></asp:DropDownList></td>
           </tr>
           <tr>
             <td height="28" align="right">房型要求： </td>
             <td height="28" align="left"><input  name="tos_roomtyperequirements" id="tos_roomtyperequirement" type="text"   size="25"/></td>
           </tr>
           <tr>
             <td height="28" align="right"><span class="ff0000">*</span>房间数量： </td>
             <td height="28" align="left">
                 <input id="tos_roomnumber" name="tos_roomnumber" type="text" valid="required" errmsg="请填写房间数量"/>
                <span class="pandl">间</span><span id="result"></span>
                <span id="errMsg_tos_roomnumber" class="errmsg"></span>
             </td>
           </tr>
           <tr>
             <td height="28" align="right"><span class="ff0000">*</span>人数： </td>
             <td height="28" align="left">
                 <input id="tos_Number" name="tos_Number" type="text" valid="required" errmsg="请填写预订人数"/>
                 <span id="errMsg_tos_Number" class="errmsg"></span>
             </td>
           </tr>
           <tr>
             <td height="28" align="right"><span class="ff0000">*</span>团房预算： </td>
             <td height="28" align="left">
                     <input id="tos_budget1" type="text" name="tos_budget1"  size="15"  valid="required" errmsg="请填写房间预算最小值"/>
                    <span id="errMsg_tos_budget1" class="errmsg"></span>-
                     <input  type="text" name="tos_budget2" id="tos_budget2" size="15"  valid="required" errmsg="请填写房间预算最大值"/>
                     <span id="errMsg_tos_budget2" class="errmsg"></span>
             </td>
           </tr>
           <tr>
             <td height="28" align="right">宾客类型： </td>
             <td height="28" align="left"><asp:DropDownList ID="tos_visitorstype" runat="server"></asp:DropDownList></td>
           </tr>
           <tr>
             <td height="28" align="right">团队类型： </td>
             <td height="28" align="left"><asp:DropDownList ID="tos_teamtype" runat="server"></asp:DropDownList></td>
           </tr>
           <tr>
             <td height="28" align="right">其他要求： </td>
             <td height="28" align="left">
               <textarea id="textarea" name="textarea" cols="80" rows="10"></textarea>
             </td>
           </tr>
           <tr>
             <td height="28" align="center" colspan="2">
                <a class="baocun_btn" href="javascript:void(0);" onclick="TeamOnlinesubmit.Save();" id="btnSubmit">提交申请</a> 
                <a class="baocun_btn" href="javascript:void(0);" id="tos_btnresult">重 置</a>
             </td>
           </tr>
         </tbody></table>
         <!--列表-->
         <!--翻页-->
       </div>
             <script type="text/javascript">
                 var TeamOnlinesubmit = {
                     init: function() {
                         //验证初始化
                         FV_onBlur.initValid($("#btnSubmit").closest("form").get(0));

//                         $("#tos_HotelName").blur(function() { //指定酒店失去焦点的时候 清空文本框的值
//                             if (this.value == "") {
//                                 this.value = "请填写酒店官方名称";
//                                 $(this).css("color", "gray");
//                             }
//                         });
//                         $("#tos_HotelName").focus(function() {
//                             if (this.value == "请填写酒店官方名称") {
//                                 this.value = "";
//                                 $(this).css("color", "black");
//                             }
//                         });
                     },
                     isShowEnd: function() {  //是否有指定酒店 是的话 显示填写指定酒店官方名称 否 就隐藏 填写指定酒店官方名称
                         var radVoyageType = $("input[name='tos_result']:checked").val();
                         if (radVoyageType == 2) {
                             $("#tos_isshow").hide();
                             $("#tos_HotelName").val("请填写酒店官方名称");
                             $("#tos_HotelName").css("color", "gray");
                         }
                         else {
                             $("#tos_isshow").show();
                             $("#tos_HotelName").val("请填写酒店官方名称");
                             $("#tos_HotelName").css("color", "gray");
                         }
                     },
                     Save: function() { //提交事件
                         var validatorshow = ValiDatorForm.validator($("#btnSubmit").closest("form").get(0), "span"); //获取提示信息

                         if (validatorshow == false) {
                             return;
                         }

                         $.newAjax({
                             type: "POST",
                             url: "/HotelCenter/HotelOrderManage/TeamOnlinesubmit.aspx?issave=1",
                             cache: false,
                             data: $("#btnSubmit").closest("form").serialize(),
                             dataType: 'json',
                             success: function(msg) {
                                 if (msg.isSuccess) {
                                     var box = new Boxy("<p>正在进行提交中...</p>", { title: "操作信息", modal: true });
                                     alert("提交成功!我们会在20分钟回复您!");
                                     box.hide(); //关闭提示框
                                     topTab.url(topTab.activeTabIndex, "/hotelcenter/hotelordermanage/TeamOnlinesubmit.aspx"); //刷新页面
                                 }
                                 else {
                                     alert(msg.errMsg);
                                     box.hide();
                                     return false;
                                 }
                             },
                             error: function() {
                                 alert("服务器繁忙!请稍候再进行此操作!");
                                 return false;
                             }
                         });
                     }
                 }

                 var TeamOnlinesubmitAC = {
                     SearchTimeOut: null, CityList: [],

                     TxtKeyUp: function() {
                         var val = $("#txtCity").val();
                         if ($.trim(val) != "") {
                             TeamOnlinesubmitAC.AddItemToCity($("#txtCity").val());
                         }
                     },
                     AddItemToCity: function(val) {
                         if ($.trim(val) == undefined || $.trim(val) == "") return;
                         if (this.CityList.length < 1) return;

                         for (var i = 0; i < this.CityList.length; i++) {
                             var cityPy = this.CityList[i].Spelling;
                             var cityCode = this.CityList[i].CityCode;
                             var cityName = this.CityList[i].CityName;

                             var indexVal = (cityPy + cityName + cityCode).toUpperCase().indexOf(val.toUpperCase())

                             if (indexVal < 0) continue;
                             if (indexVal == 0) {
                                 $("#<%=tos_destination.ClientID %>").val(cityCode);
                                 break;
                             }
                             if (indexVal > 0) {
                                 $("#<%=tos_destination.ClientID %>").val(cityCode);
                             }
                         }
                     }
                 };


                 $(function() {
                     TeamOnlinesubmit.init();

                     setTimeout(function() {
                         $("#txtCity").tipsy({ fade: true, content: '城市中文、拼音、三字码筛选', gravity: "s" });
                     }, 1000);

                     $("#txtCity").keyup(function() {
                         if (TeamOnlinesubmitAC.SearchTimeOut != null) {
                             clearTimeout(TeamOnlinesubmitAC.SearchTimeOut);
                         }
                         TeamOnlinesubmitAC.SearchTimeOut = setTimeout(TeamOnlinesubmitAC.TxtKeyUp, 200);
                     });

                     setTimeout(function() {
                         if ($("#txtCity").val() != "") {
                             TeamOnlinesubmitAC.AddItemToCity($("#txtCity").val());
                         }
                     }, 1000);


                     //表单重置
                     $("#tos_btnresult").click(function() {
                         var myForm = $(this).closest("form").get(0);
                         myForm.reset();
                         return false;
                     });
                 });                             
        </script>
</asp:content>