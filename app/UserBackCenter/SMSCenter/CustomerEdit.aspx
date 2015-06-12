<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="UserBackCenter.SMSCenter.CustomerEdit" %>

<%@ Register Src="/usercontrol/SMSCenter/SmsHeaderMenu.ascx" TagName="shm" TagPrefix="uc" %>
<%@ Register Src="/usercontrol/SMSCenter/ProvinceAndCityList.ascx" TagName="pac" TagPrefix="uc" %>
<asp:content id="CustomerEdit" runat="server" contentplaceholderid="ContentPlaceHolder1">
<uc:shm id="cl_shm1" runat="server" TabIndex="tab4"></uc:shm>
<table width="100%" border="0" cellspacing="0" cellpadding="4" class="mobilebox" style="width:99%; margin-top:10px;">
        <tr>
          <td width="114" align="right">客户类型：</td>
          <td  align="left">
          <select id="ce_selUserClass" runat="server" valid="required"  errmsg="请选择类别">
           
          </select>
          <span style="display:none;">
          <span id="errMsg_<%=ce_selUserClass.ClientID %>" class="errmsg"></span>
            <img src="<%=ImageServerUrl%>/images/add2.gif" width="13" height="12" /><a href="javascript:;" onclick="return CustomerEdit.onClass()">新增</a>
            <input id="ce_txtUserClass" type="text" size="12" style="display:none" />
           <input type="button" id="ce_btnClassAdd" value="增加"  onclick="CustomerEdit.addClass();return false;" style="display:none"/>
           <a href="javascript:;" onclick="return CustomerEdit.deleteClass()">删除</a></span>
           </td>
        </tr>
        <tr>
          <td align="right">所在地：</td>
          <td align="left">
               <uc:pac id="PAC_Select" runat="server"></uc:pac>           
          </td>
        </tr>
        <tr>
          <td align="right"><span style="color:#f00">*</span>手机号码：</td>
          <td align="left"><input id="ce_txtMoible" type="text" size="30" runat="server" valid="required|isMobile"  errmsg="请填写手机号码|请填写有效的手机格式"  />
          <span id="errMsg_<%=ce_txtMoible.ClientID %>" class="errmsg"></span>
          </td>
        </tr>
        <tr style="display:none;">
          <td align="right">单位名称：</td>
          <td align="left"><input id="ce_txtCompanyName" runat="server" type="text" size="30" maxlength="50" errmsg="请填写单位名称" />
          <span id="errMsg_<%=ce_txtCompanyName.ClientID %>" class="errmsg"></span>
          </td>
        </tr>
        <tr style="display:none;">
          <td align="right" >姓名：</td>
          <td align="left"><input id="ce_txtUserName"  runat="server" type="text" size="30" maxlength="20" errmsg="请填写姓名"/>
          <span id="errMsg_<%=ce_txtUserName.ClientID %>" class="errmsg"></span>
          </td>
        </tr>
        <tr>
          <td align="right">备注：</td>
          <td align="left"><input id="ce_txtRemark" runat="server" type="text" size="30" /></td>
        </tr>
        <tr>
          <td align="right">&nbsp;</td>
          <td align="left"><input type="button" id="ce_btnSave" value="保存" onclick="CustomerEdit.saveCustomer();"style="width:60px; height:30px;" method="<%=isAdd%>" custid="<%=customerId%>"/></td>
        </tr>
      </table>
      <script type="text/javascript">
          $(document).ready(function() {
              FV_onBlur.initValid($("#ce_btnSave").closest("form").get(0));
          });
          var param = { method: "", classname: "", cateid: "", username: "", companyname: "", moible: "", remark: "", custid: "",provinceId:"",cityid:"" };
          var CustomerEdit = {
              //添加类别
              addClass: function() {
                  var className = $("#ce_txtUserClass").val().replace(/\s+/g, '');
                  if (className != "") {
                      if (className.length > 10) {
                          alert("类别不超过10字!");
                          $("#ce_txtUserClass").focus();
                          return false;
                      }
                      param.classname = encodeURIComponent(className);
                      param.method = "addClass";
                      $.newAjax({
                          type: "GET",
                          dataType: "json",
                          url: "/SMSCenter/CustomerEdit.aspx",
                          data: param,
                          cache: false,
                          success: function(result) {
                              if (result.success == "1") {
                                  $("#<%=ce_selUserClass.ClientID %>").append("<option value='" + result.message + "'>" + className + "</option");
                                  $("#ce_txtUserClass").val('');
                                  alert("添加成功!");
                              }
                              else {
                                  alert(result.message);
                              }
                          },
                          error: function() {
                              alert("操作失败!");
                          }
                      });
                  }
              },
              //显隐添加类别
              onClass: function() {
                  $("#ce_txtUserClass,#ce_btnClassAdd").toggle();
              },
              deleteClass: function() {
                  var selClass = $("#<%=ce_selUserClass.ClientID %>").val();
                  if (selClass == "") {
                      alert("请选择要删除的类别!");
                      return false;
                  }
                  $.newAjax({
                      type: "GET",
                      dataType: "json",
                      url: "/SMSCenter/CustomerEdit.aspx",
                      data: { method: "delClass", classid: selClass },
                      cache: false,
                      success: function(result) {
                          if (result.success == "1") {
                              $("#<%=ce_selUserClass.ClientID %> option:selected").remove();
                              alert("删除成功!");
                          }
                          else {
                              alert("删除失败!")
                          }
                      },
                      error: function() {
                          alert("操作失败!");
                      }
                  });
                  return false;
              },
              saveCustomer: function() {
                  var form = $("#ce_btnSave").closest("form").get(0);
                  if (!ValiDatorForm.validator(form, "span")) {
                      return;
                  }
                  param.cateid = $("#<%=ce_selUserClass.ClientID %>").val();
                  param.catename = encodeURIComponent($("#<%=ce_selUserClass.ClientID %> option:selected").text());
                  param.companyname = encodeURIComponent($("#<%=ce_txtCompanyName.ClientID %>").val());
                  param.moible = encodeURIComponent($("#<%=ce_txtMoible.ClientID %>").val());
                  param.remark = encodeURIComponent($("#<%=ce_txtRemark.ClientID %>").val());
                  param.username = encodeURIComponent($("#<%=ce_txtUserName.ClientID %>").val());
                  param.method = $("#ce_btnSave").attr("method");
                  param.custid = $("#ce_btnSave").attr("custid");
                  //省份城市
                  param.provinceId = $("#ctl00_ContentPlaceHolder1_PAC_Select_ddl_ProvinceList").val();
                  param.cityid = $("#ctl00_ContentPlaceHolder1_PAC_Select_ddl_CityList").val();
                  $.newAjax({
                      type: "GET",
                      dataType: "json",
                      url: "/SMSCenter/CustomerEdit.aspx",
                      data: param,
                      cache: false,
                      success: function(result) {
                          alert(result.message);
                          if (result.success == "1") {
                              topTab.url(topTab.activeTabIndex, "/SMSCenter/CustomerList.aspx");
                              return false;
                          }
                      },
                      error: function() {
                          alert("操作失败!");
                      }
                  });
              }


          }
      </script>
</asp:content>
