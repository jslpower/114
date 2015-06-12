<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstPageDataAdd.aspx.cs" Inherits="SiteOperationsCenter.HotelManagement.FirstPageDataAdd" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>首页板块数据添加</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>  
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetHotelCity") %>"></script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="26" >
                <table width="461" border="1" cellpadding="0" cellspacing="0" bordercolor="#FFFFFF" id="tbHotelShowType">
                  <tr>
                    <%
                        var htmlstr="";
                        if (showtype != null && showtype.Count > 0)
                        {
                            var defaultimg=ImageServerUrl+"/images/yunying/jdsymk.gif";
                            for (int i = 0; i < showtype.Count; i++)
                            {
                                if(i!=0)
                                {
                                    defaultimg=ImageServerUrl+"/images/yunying/jdsymk2.gif";
                                }
                                htmlstr += string.Format("<td id=\"td{3}\" width=\"114\" height=\"26\" align=\"center\" background=\"{0}\" class=\"h14\" tdShowType=\"{1}\"><a href=\"javascript:void(0)\" ><font color=\"#3880C5\">{2}</font></a></td>", defaultimg, showtype[i].Value, showtype[i].Text, showtype[i].Value);
                            }
                        }
                    %>
                    <%=htmlstr %>            
                    
                  </tr>
                </table>
              </td>
           </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif" id="tdSelect"><span style="color:Red">*</span>输入城市:
              <input name="txtCity" type="text" id="txtCity" size="7" autocomplete="off" style="width: 120px;" />
              
                <select name="sltCity" id="sltCity" runat="server" style=" width:180px;" >
                </select>
                <span style="color:Red">*</span>入住日期
                <input name="txtInDate"  onfocus="WdatePicker()" type="text" class="textfield" size="10"  id="txtInDate"/>
                <%--<img src="<%=ImageServerUrl %>/images/yunying/time.gif" width="16" height="13" />--%> 

                <span style="color:Red">*</span>离店日期
                <input name="txtOutDate" onfocus="WdatePicker()" type="text" class="textfield" size="10" id="txtOutDate" />
                <%--<img src="<%=ImageServerUrl %>/images/yunying/time.gif" width="16" height="13" />--%>
                价格范围
                <input name="txtMinPrice" type="text" class="textfield" size="5" id="txtMinPrice" />
                -
                <input name="txtMaxPrice" type="text" class="textfield" size="5" id="txtMaxPrice" />
                 <br />
                星级
                <select name="sltStartNum" id="sltStartNum" runat="server"></select>
               
                查询方式
                <select name="sltSelectType" id="sltSelectType"><option value="1">前台先付</option></select>
                酒店名称：<input name="txtHotelName" type="text" class="textfield" size="23" id="txtHotelName"/>
                    <a href="javascript:void(0)" id="hfSelect"><img src="<%= ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" border="0" style="margin-bottom:-3px;" alt="查询" /></a>
              </td>
            </tr>
        </table>
        <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td width="23%" height="25" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif" >
                <table width="40%"  border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="4%" align="right"><img src="<%=ImageServerUrl %>/images/yunying/ge_da.gif" width="3" height="20" /></td>
                    <td width="23%"><a href="javascript:void(0)" onclick="FirstPageDataAdd.DeleteLocalHotel('')" id="hfDelete"><img src="<%=ImageServerUrl %>/images/yunying/shanchu.gif" width="51" height="25" /></a>
                        <a href="javascript:void(0)" onclick="FirstPageDataAdd.AddLocalDateByItems('')" id="hfAdd" style="display:none" ><img src="<%=ImageServerUrl %>/images/yunying/xinzeng.gif" width="51" height="25" /></a>
                    </td>
                  </tr>
                </table>
            </td>
             <td width="15%"  background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif"><%--<img src="<%=ImageServerUrl %>/images/yunying/ge_d.gif" width="11" height="25" />--%><span id="spwait" style=" display:none; color:Red">正在处理请稍候...</span></td>
            <td width="77%" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif" align="center"><span style=" display:none;color:Red" id="sptjHotel"></span></td><!--提示：一个城市下只能置顶两个酒店-->
          </tr>
        </table>
        <div id="divDataList" style=" text-align:center;">
        
        </div>
    </div>
    </form>
    <script type="text/javascript">
      function mouseovertr(o) {
	      o.style.backgroundColor="#FFF9E7";
          //o.style.cursor="hand";
      }
      function mouseouttr(o) {
	      o.style.backgroundColor=""
      }
      var parms={"City":0,"CheckInDate":"","CheckOutDate":"","MinPrice":0,"MaxPrice":0,"StarNum":0,"SelectType":0,"HotelName":"","Page":1,"HotelShowType":0};  //查询参数
      var addPrams={"CheckInDate":"","CheckOutDate":"","HotelCodes":null,"HotelTopCodes":null,"method":"add","HotelShowType":"","InterHotelList":""}; //添加参数
      var SearchTimeOut = null;
      var FirstPageDataAdd = {
          GetHotelList: function(url) {    //获取列表数据
              LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
              LoadingImg.ShowLoading("divDataList");
              if (LoadingImg.IsLoadAddDataToDiv("divDataList")) {
                  $.ajax({
                      type: "GET",
                      dataType: 'html',
                      url: url,
                      data: parms,
                      cache: false,
                      success: function(html) {
                          $("#divDataList").html(html);
                          if (parms.HotelShowType == 2) { //特约酒店
                              $("#sptjHotel").show();
                              $("#tbHotelList tr:gt(0)").each(function() {
                                  $(this).children().eq(3).show();
                              });
                          } else {
                              $("#sptjHotel").hide();
                          }
                      },
                      error: function() {
                          $("#divDataList").html("查询超时，请再次进行查询！");
                      }
                  });
              }
          },
          GetLocalHotel: function(HotelType) {  //根据酒店类型显示页面
              $("#hfAdd").hide();
              $("#hfDelete").show();
              $("#tbHotelShowType td[tdShowType]").each(function() {
                  if ($(this).attr("id") == "td" + HotelType) {
                      $(this).attr("background", "<%=ImageServerUrl%>/images/yunying/jdsymk.gif")
                  } else {
                      $(this).attr("background", "<%=ImageServerUrl%>/images/yunying/jdsymk2.gif")
                  }
              });
              parms.HotelShowType = HotelType;
              this.ClearParms();
              this.ClearSelectControl();
              this.GetHotelList("AjaxLocalData.aspx"); //根据酒店类型获取数据
          },
          ClearSelectControl: function() {  //恢复默认查询控件的值
              if (parms.HotelShowType == "2") { //特约酒店
                  $("#sptjHotel").show();
              } else {
                  $("#sptjHotel").hide();
              }
              $("#tdSelect").find("input[type='text']").each(function() {
                  $(this).val("");
              });
              $("#<%=sltCity.ClientID %>").val("-1");
              $("#<%=sltStartNum.ClientID %>").val("0");
          },
          ClearParms: function() {  //查询参数恢复默认
              parms.City = 0;
              parms.CheckInDate = "";
              parms.CheckOutDate = "";
              parms.PriceScope = "";
              parms.StarNum = "";
              parms.SelectType = "";
              parms.HotelName = "";
              parms.Page = 1;
          },
          LoadData: function(obj, url) {//分页
              var Page = exporpage.getgotopage(obj);
              parms.Page = Page;
              this.GetHotelList(url); //获取接口数据列表
          },
          DeleteLocalHotel: function(strValues) {  //删除本地数据库数据
              var parmsdel = "method=delete&HotelCodes=";
              if (strValues == "" || strValues == null) { //多条删除
                  var hotelArry = new Array();
                  $("#divDataList").find("input[type='checkbox'][name='chkHotel']:checked").each(function() {
                      hotelArry.push($.trim($(this).val())); //获取所选中的乘客id
                  });
                  if (hotelArry.length < 1) {
                      alert("至少选择一项进行删除！");
                      return;
                  }
                  parmsdel += hotelArry;
              } else {   //单条删除
                  parmsdel += strValues;
              }
              $.ajax({
                  url: "FirstPageDataAdd.aspx?",
                  cache: false,
                  async: false,
                  dataType: "json",
                  type: "post",
                  data: parmsdel,
                  success: function(msg) {
                      if (msg.success == "1") {
                          alert(msg.message);
                          FirstPageDataAdd.GetHotelList("AjaxLocalData.aspx"); //根据酒店类型获取数据
                      } else {
                          alert(msg.message);
                      }
                  },
                  error: function() {
                      alert("操作失败");
                  }
              });
          },
          AddLocalDateByItems: function(strCodes) {
              var hotelArry = new Array();
              if (strCodes != "" & strCodes != null) {  //单个添加
                  hotelArry.push(strCodes);
              } else {              //多项添加
                  $("#divDataList").find("input[type='checkbox'][name='chkHotel']:checked").each(function() {
                      hotelArry.push($(this).val()); //获取所选中的乘客id
                  });

                  if (hotelArry.length < 1) {
                      alert("至少选择一项进行添加！");
                      return;
                  }
              }
              var list = "";
              if ($("#hidInterHotel").length > 0) {
                  list = $("#hidInterHotel").val();
                  if ($.trim(list) == "") {
                      return;
                  }
              } else {
                  return;
              }
              $("#spwait").show();
              //          addPrams.CheckInDate=parms.CheckInDate;
              //          addPrams.CheckOutDate=parms.CheckOutDate;
              //          addPrams.HotelCodes=hotelArry;
              //          addPrams.HotelShowType=parms.HotelShowType;          
              //          addPrams.InterHotelList=list;
              var datas = "method=add&HotelCodes=" + hotelArry + "&HotelShowType=" + parms.HotelShowType + "&InterList=" + escape(list);
              if (parms.HotelShowType == 2) {
                  var topArry = new Array();
                  for (var i = 0; i <= hotelArry.length; i++) {
                      if ($("#chkSetTop" + hotelArry[i]).attr("checked")) {
                          topArry.push(hotelArry[i]);
                      }
                  }
                  //            if(topArry.length>2){
                  //                alert("一个城市下只能置顶两个酒店");
                  //                return;
                  //            }
                  datas += "&HotelTopCodes=" + topArry;
              } else {
                  datas += "&HotelTopCodes=";
              }
              $.ajax({
                  url: "FirstPageDataAdd.aspx?",
                  cache: false,
                  async: false,
                  dataType: "json",
                  type: "post",
                  data: datas,
                  success: function(msg) {
                      if (msg.success == "1") {
                          alert(msg.message);
                      } else {
                          alert(msg.message);
                      }
                      $("#spwait").hide();
                  },
                  error: function() {
                      $("#spwait").hide();
                      alert("操作失败");
                  }
              });
          },
          AllCheckControl: function(obj) {   //全选和反选、
              $("#divDataList").find("input[type='checkbox'][name='chkHotel']").each(function() {
                  $(this).attr("checked", $(obj).attr("checked"));
              });
          },
          AddItemToCity: function(val) {
              var isThere = false;
              if (val == "Load") {
                  $("#<%=sltCity.ClientID %>").empty();
                  $("<option value=''>--请选择--</option>").appendTo($("#<%=sltCity.ClientID %>"));
                  if (CityList.length > 0) {
                      for (var i = 0; i < CityList.length; i++) {
                          var Ping = CityList[i].P;
                          var Code = CityList[i].C;
                          var cityName = CityList[i].CN;
                          $("<option value='" + Code + "'>" + Ping + cityName + Code + "</option>").appendTo($("#<%=sltCity.ClientID %>"));
                      }
                  }
              } else if ($.trim(val) != "") {
                  if (CityList.length > 0) {
                      for (var i = 0; i < CityList.length; i++) {
                          var Ping = CityList[i].P;
                          var Code = CityList[i].C;
                          var cityName = CityList[i].CN;
                          var indexVal = (Ping + cityName + Code).toUpperCase().indexOf(val.toUpperCase())
                          if (indexVal == 0) {
                              $("#<%=sltCity.ClientID %>").val(Code);
                              return true;
                          }
                          if (indexVal > 0) {
                              $("#<%=sltCity.ClientID %>").val(Code);
                              isThere = true;
                          }
                      }
                  }
              }
              return isThere;
          },
          TxtKeyUp: function() {
              var val = $("#txtCity").val();
              if ($.trim(val) != "") {
                  if (FirstPageDataAdd.AddItemToCity(val)) {
                      var code = $("#<%=sltCity.ClientID %>").val();
                  }
              }
          }
      }
      $(function() {
          FirstPageDataAdd.AddItemToCity("Load"); //加载城市
          FirstPageDataAdd.GetHotelList("AjaxLocalData.aspx"); //获取本地数据库数据列表  
          $("#tbHotelShowType td[tdShowType]").each(function() {
              var typeVal = "";
              $(this).click(function() {
                  typeVal = $(this).attr("tdShowType");
                  FirstPageDataAdd.GetLocalHotel(typeVal);
              })
          })
          $("#txtCity").keyup(function() {
              if (SearchTimeOut != null) {
                  clearTimeout(SearchTimeOut);
              }
              SearchTimeOut = setTimeout(FirstPageDataAdd.TxtKeyUp, 200);
          });
          setTimeout(function() {
              if ($("#txtCity").val() != "") {
                  if (FirstPageDataAdd.AddItemToCity(val)) {
                      var code = $("#<%=sltCity.ClientID %>").val();
                  }
              }
          }, 1000)

          $("#hfSelect").click(function() { // 查询页面
              var city = $("#<%=sltCity.ClientID %>").val();
              var checkInDate = $("#txtInDate").val();
              var checkOutDate = $("#txtOutDate").val();
              if (city == "") {
                  alert("请选择城市");
                  return;
              }
              if (checkInDate == "" || checkInDate == null) {
                  alert("请填写入住日期");
                  return;
              }
              if (checkOutDate == "" || checkOutDate == null) {
                  alert("请填写离店日期");
                  return;
              }
              var maxPrice = $("#txtMaxPrice").val();
              var minPrice = $("#txtMinPrice").val();
              if (!(/^\d+(\.\d+)?$/).test(maxPrice) && maxPrice != "" && maxPrice != null) {
                  alert("价格范围格式填写错误");
                  return;
              }
              if (!(/^\d+(\.\d+)?$/).test(minPrice) && minPrice != "" && minPrice != null) {
                  alert("价格范围格式填写错误");
                  return;
              }
              if (parseFloat(maxPrice) < parseFloat(minPrice)) {
                  alert("价格范围格式填写错误");
                  return;
              }
              FirstPageDataAdd.ClearParms();
              parms.City = city;
              parms.CheckInDate = checkInDate;
              parms.CheckOutDate = checkOutDate;
              parms.HotelName = $("#txtHotelName").val();
              parms.MaxPrice = maxPrice;
              parms.MinPrice = minPrice;
              parms.SelectType = $("#sltSelectType").val();
              parms.StarNum = $("#<%= sltStartNum.ClientID%>").val();
              FirstPageDataAdd.GetHotelList("AjaxInterfaceData.aspx"); //获取接口数据列表                
              $("#hfAdd").show();
              $("#hfDelete").hide();
          });
      })
</script>
</body>
</html>
