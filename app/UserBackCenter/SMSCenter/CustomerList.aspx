<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="UserBackCenter.SMSCenter.CustomerList" %>

<%@ Register Src="/usercontrol/SMSCenter/SmsHeaderMenu.ascx" TagName="shm" TagPrefix="uc" %>
<%@ Register Src="/usercontrol/SMSCenter/ProvinceAndCityList.ascx" TagName="pac"
    TagPrefix="uc" %>
<asp:content id="CustomerList" runat="server" contentplaceholderid="ContentPlaceHolder1">
<uc:shm id="cl_shm1" runat="server" TabIndex="tab4"></uc:shm>
      <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mobilebox" style="margin-bottom:10px; margin-top:10px; width:99%;">
        <tr>
          <td align="left">
          <span style="display:none;">单位名称：
            <input id="cl_txtCompanyName" type="text" size="20" onkeypress="CustomerList.isEnter(event);" />
            姓名：<input id="cl_txtUserName" type="text" size="10" onkeypress="CustomerList.isEnter(event);" /></span> 
            手机：<input id="cl_txtMoible" type="text" size="12" onkeypress="CustomerList.isEnter(event);"  />
            <uc:pac id="PAC_Select" runat="server"></uc:pac>
            客户类型<select id="cl_selUserClass" runat="server">                     
                    </select>            
          <input type="button" id="cl_btnSearch"value="搜 索" onclick="CustomerList.search();" /></td>
          <td align="center">&nbsp;</td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom:2px;" class="tablewidth">
        <tr>
          <td width="17%" background="<%=ImageServerUrl%>/images/gongneng_bg.gif"><img src="<%=ImageServerUrl%>/images/ge_da.gif" width="3" height="20" /><a href="javascript:void(0);" onclick="return CustomerList.addCustomer();"><img src="<%=ImageServerUrl%>/images/xinzeng.gif" width="50" height="25" border="0" /></a><a href="javascript:void(0);" onclick="return CustomerList.updateCustomer();"><img src="<%=ImageServerUrl%>/images/xiugai.gif" width="50" height="25" /></a><a href="javascript:void(0);" onclick="return CustomerList.delCustomer();"><img src="<%=ImageServerUrl%>/images/shanchu.gif" width="51" height="25" /></a></td>
          <td width="73%" align="left" background="<%=ImageServerUrl%>/images/gongneng_bg.gif"><a href="javascript:;" onclick="return CustomerList.openDialog('从文件导入号码','/SMSCenter/ImportNumFromFile.aspx?typeID=1','700px','500px')"><img src="<%=ImageServerUrl%>/images/open.gif" width="14" height="14" />从文件导入号码</a> <%--<a href="#" onclick="return CustomerList.openDialog('从我的客户导入号码','/SMSCenter/ImportNumFromSystem.aspx','700px','500px')"><img src="<%=ImageServerUrl%>/images/open.gif" width="14" height="14" />从我的客户导入号码</a>--%></td>
        </tr>
      </table>
      <div id="cl_rpt_customerdiv"></div>
      
      <script type="text/javascript">

          //查询参数
          var searchParam = { companyname: "", username: "", moible: "", userclass: "", Page: "", method: "", provinceId: "", cityid: "" };
          var CustomerList = {
              openDialog: function(title, url, width, height) {
                  var height1 = GetAddOrderHeight();
                  height1 = "400px";
                  Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height1, draggable: true, data: null });
                  return false;
              },
              //点击查询操作
              search: function() {
                  searchParam.companyname = encodeURIComponent($("#cl_txtCompanyName").val());
                  searchParam.username = encodeURIComponent($("#cl_txtUserName").val());
                  searchParam.moible = encodeURIComponent($("#cl_txtMoible").val());
                  searchParam.userclass = $("#<%=cl_selUserClass.ClientID %>").val();
                  searchParam.provinceId = $("#ctl00_ContentPlaceHolder1_PAC_Select_ddl_ProvinceList").val();
                  searchParam.cityid = $("#ctl00_ContentPlaceHolder1_PAC_Select_ddl_CityList").val();
                  searchParam.method = "";
                  CustomerList.getCustomerList();
              },
              //调用Ajax获取数据
              getCustomerList: function() {
              $("#cl_rpt_customerdiv").html("加载数据中，请稍候...");
                  $.newAjax({
                      type: "post",
                      dataType: "html",
                      url: "/SMSCenter/AjaxCustomerList.aspx",
                      data: searchParam,
                      cache: false,
                      success: function(result) {
                          if (result == "{success:'0',message:''}") {
                              alert("对不起,你尚未审核通过!")
                              return false;
                          }
                          $("#cl_rpt_customerdiv").html(result);
                          $("#acl_ExportPage a").click(function() {
                         
                              return CustomerList.loadData($(this));
                          });
                          if (searchParam.method == "delete") {
                              alert("删除成功!");
                          }
                      },
                      error: function() {
                          alert("操作失败!");
                      }
                  });
                  return false;
              },
              //点击分页时获取数据
              loadData: function(tar_a) {
                  searchParam.Page = tar_a.attr("href").match(/Page=\d+/)[0].match(/\d+/)[0];
                  searchParam.method = "";
                  CustomerList.getCustomerList();
                  return false;
              },
              //删除客户
              delCustomer: function() {
                  var selCheckBox = $("#cl_rpt_customerdiv").find(":checkbox:checked");
                  if (selCheckBox.length == 0) {
                      alert("请选择要删除的客户!");
                      return false;
                  }
                  if (!confirm("你确定要删除所选的客户吗?")) {
                      return false;
                  }
                  searchParam.method = "delete";
                  var custIds = "";
                  selCheckBox.each(function() {
                      custIds += $(this).val() + ",";
                  });
                  searchParam.custids = custIds;
                  CustomerList.getCustomerList();

              },
              //判断是否按回车
              isEnter: function(event) {
                  event = event ? event : window.event;
                  if (event.keyCode == 13) {
                      CustomerList.search();
                  }
              },
              addCustomer: function() {
                  topTab.url(topTab.activeTabIndex, "/SMSCenter/CustomerEdit.aspx");
                  return false;
              },
              chkAll: function(tar_chk) {
                  $("#cl_rpt_customerdiv").find("input[name='cl_chk']").attr("checked", $(tar_chk).attr("checked"));
              },
              updateCustomer: function() {
                  var selCheckBox = $("#cl_rpt_customerdiv").find(":checkbox:checked");
                  if (selCheckBox.length != 1) {
                      alert("请选择要修改的1条客户!");
                      return false;
                  }
                  topTab.url(topTab.activeTabIndex, "/SMSCenter/CustomerEdit.aspx?custid=" + selCheckBox.val());
                  return false;
              }
          }
          $(document).ready(function() {
              CustomerList.getCustomerList();
          });
      </script>
</asp:content>
