<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchHotel.aspx.cs" Inherits="UserBackCenter.HotelCenter.HotelOrderManage.SearchHotel" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:content id="HotelOrderList" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'SearchHotel'
    });
</script>
<style type="text/css">
    .liStyle{width:20%;float:left;}
</style>
<link href="<%=CssManage.GetCssFilePath("tipsy") %>" rel="stylesheet" type="text/css" />
<div class="tablebox">
         <!--添加信息表格-->
         <table id='<%=this.tblID%>' cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center" class="margintop5" style="width:100%;">
           
           <tbody><tr>
             <td width="16%" bgcolor="#f2f9fe" align="right"><font class="ff0000">*</font>目的城市：</td>
             <td align="left"><input name="txtCity" id="txtCity" type="text" autocomplete="off"/>
               &nbsp;&nbsp; 
                   <asp:DropDownList runat="server" ID="ddlCity" valid="custom" custom="SearchHotel.checkCity" errmsg="目的城市必须选择！"></asp:DropDownList>
                    <span id="errMsg_<%=this.ddlCity.ClientID%>" class="errmsg"></span>
              </td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">常用城市：</td>
             <td align="left">
             <table width="65%" cellspacing="0" cellpadding="0" border="0" class="hotelCityTable">
               <tbody>
                 <tr>
                    <td style="padding:0px">
                        <ul style="margin:0px;width:100%;">
                                <li style="width:20%;float:left;"><input name="radio" value="PEK" type="radio" /><span>北京</span></li>
								<li style="width:20%;float:left;"><input type="radio" name="radio" value="SHA" /><span>上海</span></li>
								<li style="width:20%;float:left;"><input type="radio" name="radio" value="CAN" /><span>广州</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="SZX" /><span>深圳</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="HGH" /><span>杭州</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="NKG" /><span>南京</span></li>
								<li style="width:20%;float:left;"><input type="radio" name="radio" value="CTU" /><span>成都</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="WUH" /><span>武汉</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="TAO" /><span>青岛</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="DLC" /><span>大连</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="CKG" /><span>重庆</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="TSN" /><span>天津</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="SZV" /><span>苏州</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="NGB" /><span>宁波</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="SIA" /><span>西安</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="HRB" /><span>哈尔滨</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="SYX" /><span>三亚</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="KMG" /><span>昆明</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="SHE" /><span>沈阳</span></li>
                                <li style="width:20%;float:left;"><input type="radio" name="radio" value="HKG" /><span>香港</span></li>
                        </ul>
                    </td>
                 </tr>
               </tbody>
             </table>
             </td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">行政区：</td>
             <td align="left"><input name="txtRegion" id="txtRegion" type="text">
               &nbsp; 
               <asp:DropDownList runat="server" id="ddlRegion"></asp:DropDownList>
               </td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">地理位置：</td>
             <td align="left"><input name="txtPlace" id="txtPlace" type="text">
               &nbsp; 
               <asp:DropDownList runat="server" id="ddlPlace">
                <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
               </asp:DropDownList>
               </td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right"><font class="ff0000">*</font>入店日期：</td>
             <td align="left"><input name="txtInTime" valid="required" errmsg="入店日期不能为空！" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtOutTime\')}',minDate:'%y-%M-%d',onpicked:function(){if(this.value!='')$('#errMsg_txtInTime').html('');}})" id="txtInTime" type="text"/>
             <img width="16" height="13" align="middle" onclick="javascript:$(this).prev('#txtInTime').focus()" src="<%=ImageServerUrl %>/images/time.gif">
             <span id="errMsg_txtInTime" class="errmsg"></span>
             </td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right"><font class="ff0000">*</font>离店日期：</td>
             <td align="left"><input name="txtOutTime"  valid="required" errmsg="离店日期不能为空！" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtInTime\')||\'%y-%M-%d\'}',onpicked:function(){if(this.value!='')$('#errMsg_txtOutTime').html('');}})" id="txtOutTime" type="text"/>
               <img width="16" height="13" align="middle" onclick="javascript:$(this).prev('#txtOutTime').focus()" src="<%=ImageServerUrl %>/images/time.gif">
               <span id="errMsg_txtOutTime" class="errmsg"></span>
               </td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="left" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp; <strong>更多搜索条件</strong></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">酒店中英文名称：</td>
             <td align="left"><input  name="txtHotelName" value="中英文/拼音首字母" onblur="if($.trim(this.value)=='')this.value='中英文/拼音首字母'" onfocus="if($.trim(this.value)=='中英文/拼音首字母')this.value=''" id="txtHotelName" type="text"/></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">价格范围：</td>
             <td align="left"><input name="txtPriceStart" size="8" id="txtPriceStart" type="text" valid="isMoney" errmsg="起始价格的格式不正确！"/>
-
            <input name="txtPriceEnd" size="8" id="txtPriceEnd" type="text" valid="isMoney" errmsg="结束价格的格式不正确！"/>
            元 
            <span id="errMsg_txtPriceStart" class="errmsg"></span><span id="errMsg_txtPriceEnd" class="errmsg"></span>
            </td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">酒店星级：</td>
             <td align="left">
             <asp:DropDownList runat="server" ID="ddlHotelStar"></asp:DropDownList>
             </td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">查询方式：</td>
             <td align="left">
             <asp:DropDownList runat="server" ID="ddlSearchWay">
                     <asp:ListItem Value="T" Text="前台现付"></asp:ListItem>
                    <asp:ListItem Value="S" Text="代收代付"></asp:ListItem>
                    <asp:ListItem Value="V" Text="预付"></asp:ListItem>
             </asp:DropDownList>
             </td>
           </tr>
           <tr>
             <td height="35" align="center" colspan="2"><a class="baocun_btn" href="javascript:void(0);" id="btnSubmit">搜 索</a></td>
           </tr>
         </tbody></table>
         <input type="hidden" id="hideRegion" runat="server" />
       </div>
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("HotelJSData") %>"></script>
      <script type="text/javascript">
          var SearchHotel = {
              SearchTimeOut: null, CityList: [],
              TxtKeyUp: function() {
                  $conObj = $("#" + SearchHotel._getData().ContainerID);
                  var val = $conObj.find("#txtCity").val();
                  if ($.trim(val) != "") {
                      SearchHotel.AddItemToCity($conObj.find("#txtCity").val());
                  }
              },
              _getData: function() {
                  return commonTourModuleData.get('<%=this.tblID %>');
              },
              checkCity: function(obj) {
                  $conObj = $("#" + SearchHotel._getData().ContainerID);
                  if ((obj.value == "" || parseInt(obj.value) <= 0) && $conObj.find(":radio:checked").length==0)
                      return false;
                  else return true;
              },
              AddItemToCity: function(val) {
                  $conObj = $("#" + SearchHotel._getData().ContainerID);
                  if ($.trim(val) == undefined || $.trim(val) == "") return;
                  if (this.CityList.length < 1) return;

                  for (var i = 0; i < this.CityList.length; i++) {
                      var cityPy = this.CityList[i].Spelling;
                      var cityCode = this.CityList[i].CityCode;
                      var cityName = this.CityList[i].CityName;

                      var indexVal = (cityPy + cityName + cityCode).toUpperCase().indexOf(val.toUpperCase())

                      if (indexVal < 0) continue;
                      if (indexVal == 0) {
                          $conObj.find("#<%=ddlCity.ClientID %>").val(cityCode);
                          break;
                      }
                      if (indexVal > 0) {
                          $conObj.find("#<%=ddlCity.ClientID %>").val(cityCode);
                      }
                  }
              },
             //添加行政区域
            AddItemToRegion: function(cityCode) {
                $("#<%=ddlRegion.ClientID %>").empty();
                $("<option value=''>--请选择--</option>").appendTo($("#<%=ddlRegion.ClientID %>"));
                if (RegionList.length > 0) {
                    for (var i = 0; i < RegionList.length; i++) {
                        var id = RegionList[i].ID;
                        var Code = RegionList[i].C;
                        var areaName = RegionList[i].AN;
                        if (cityCode.toUpperCase() == Code.toUpperCase()) {
                            $("<option value='" + id + "'>" + areaName + "</option>").appendTo($("#<%=ddlRegion.ClientID %>"));
                        }
                    }
                }
            },
            //添加地理位置
            AddItemToGeography: function(cityCode) {
            $("#<%=ddlPlace.ClientID %>").empty();
                $("<option value=''>--请选择--</option>").appendTo($("#<%=ddlPlace.ClientID %>"));
                if (GeographyList.length > 0) {
                    for (var i = 0; i < GeographyList.length; i++) {
                        var id = GeographyList[i].ID;
                        var Code = GeographyList[i].C;
                        var por = GeographyList[i].P;
                        if (cityCode.toUpperCase() == Code.toUpperCase()) {
                            $("<option value='" + id + "'>" + por + "</option>").appendTo($("#<%=ddlPlace.ClientID %>"));
                        }
                    }
                }
            }
          };
          $(function() {
              var conObj = $("#" + SearchHotel._getData().ContainerID);
              FV_onBlur.initValid(conObj.find("#btnSubmit").closest("form").get(0));
              conObj.find("#btnSubmit").click(function() {
                  var validatorshow = ValiDatorForm.validator(conObj.find("#btnSubmit").closest("form").get(0), "span"); //获取提示信息
                  if (!validatorshow) {
                      return false;
                  }
                  //拼接搜索的url
                  var cityCode = conObj.find("#<%=ddlCity.ClientID %>").val();
                  var district = conObj.find("#<%=ddlRegion.ClientID %>").val();
                  var districtTxt = conObj.find("#<%=ddlRegion.ClientID %>  option:selected").text();
                  if (district == "") { districtTxt = ""; }
                  var landMark = conObj.find("#<%=ddlPlace.ClientID %>").val();
                  var landMarkTxt = conObj.find("#<%=ddlPlace.ClientID %>  option:selected").text();
                  if (landMark == "") { landMarkTxt = ""; }
                  var inTime = $("#txtInTime").val();
                  var leaveTime = conObj.find("#txtOutTime").val();
                  var hotelName = conObj.find("#txtHotelName").val();
                  var priceBegin = conObj.find("#txtPriceStart").val();
                  var priceEnd = conObj.find("#txtPriceEnd").val();
                  var hotelLevel = conObj.find("#<%=ddlHotelStar.ClientID %>").val();
                  var searchWay = conObj.find("#<%=ddlSearchWay.ClientID %>").val();
                  if ($.trim(hotelName) == "中英文/拼音首字母") {
                      hotelName = "";
                  }
                  //验证
                  var val = $("#txtCity").val();
                  if (cityCode == "") {
                      alert("请输入或选择一个城市!");
                      return;
                  }

                  if (inTime == "") {
                      alert("请选择入店日期!");
                      return;
                  }
                  if (leaveTime == "") {
                      alert("请选择离店日期");
                      return;
                  }

                  var para = { cityCode: "", district: "", geography: "", inTime: "", leaveTime: "", hotelName: "", priceBegin: "", priceEnd: "", hotelLevel: "", searchWay: "", districtTxt: "", landMarkTxt: "" };
                  para.cityCode = cityCode;
                  para.district = district;
                  para.landMark = landMark;
                  para.inTime = inTime;
                  para.leaveTime = leaveTime;
                  para.hotelName = hotelName;
                  para.priceBegin = priceBegin;
                  para.priceEnd = priceEnd;
                  para.hotelLevel = hotelLevel;
                  para.searchWay = searchWay;
                  para.districtTxt = districtTxt;
                  para.landMarkTxt = landMarkTxt;
                  searchUrl = "<%=Domain.UserPublicCenter%>/HotelManage/HotelSearchList.aspx?" + $.param(para);
                  window.open(searchUrl, "_blank"); //直接链接到外面的酒店搜索
              });
              conObj.find("#txtCity").tipsy({ fade: true, content: '城市中文、拼音、三字码筛选', gravity: "s" });
              //城市
              conObj.find("#txtCity").keyup(function() {
                  if (SearchHotel.SearchTimeOut != null) {
                      clearTimeout(SearchHotel.SearchTimeOut);
                  }
                  SearchHotel.SearchTimeOut = setTimeout(SearchHotel.TxtKeyUp, 200);
              });
              setTimeout(function() {
                  if (conObj.find("#txtCity").val() != "") {
                      SearchHotel.AddItemToCity(conObj.find("#txtCity").val());
                  }
              }, 1000);
              conObj.find("#txtCity").blur(function() {
                  conObj.find("#<%=ddlCity.ClientID %>").change();
                  var v = conObj.find("#<%=ddlCity.ClientID %>").val();
                  if (v != "0") {
                      $(this).val(v);
                  }
              });
              //城市下拉框change事件
              conObj.find("#<%=ddlCity.ClientID %>").change(function() {
                  var code = $(this).val();
                  $("#txtCity").val("");
                  conObj.find(".hotelCityTable input").each(function() {
                      if ($(this).val() == code) {
                          this.checked = true;
                      }
                      else {
                          this.checked = false;
                      }
                  });
                  //添加行政区到下拉框
                  SearchHotel.AddItemToRegion(code);
                  //添加地理位置到下拉框
                  SearchHotel.AddItemToGeography(code);
              });
              conObj.find(".hotelCityTable input").click(function() {
                  var v = $(this).val();
                  $ops = conObj.find("#<%=ddlCity.ClientID %> option");
                  for (var i = 0; i < $ops.length; i++) {
                      if ($($ops[i]).val() == v) {
                          $($ops[i]).attr("selected", true);
                          //添加行政区到下拉框
                          SearchHotel.AddItemToRegion(v);
                          //添加地理位置到下拉框
                          SearchHotel.AddItemToGeography(v);
                          break;
                      }
                  }
              });
              //给常用城市添加label for
              conObj.find(".hotelCityTable input").each(function() {
                  $(this).attr({ "id": "rd" + this.value });
                  $(this).next("span").wrap("<label for='" + $(this).attr("id") + "'></label>");
              });
          })
    </script>
</asp:content>