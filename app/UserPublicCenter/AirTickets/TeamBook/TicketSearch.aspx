<%@ Page  Language="C#" MasterPageFile="~/MasterPage/AirTicket.Master" AutoEventWireup="true" CodeBehind="TicketSearch.aspx.cs" Inherits="UserPublicCenter.AirTickets.TeamBook.TicketSearch" %>
<%@ Register Src="~/AirTickets/TeamBook/TicketTopMenu.ascx" TagName="TopMenu" TagPrefix="myASP" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">

<myASP:TopMenu id="ts_ucTopMenu" runat="server" TabIndex="tab1"></myASP:TopMenu>
<div class="sidebar02_con">
<style type="text/css">
	.sidebar02_con_left table td{
	padding-left:50px;
	}
</style>
        <!--sidebar02_con_left-->
        <div class="sidebar02_con_left">
        	<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8" bgcolor="#FAFAFA">
                  <tr>
                    <td height="30" colspan="2" align="left"><table width="70%" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <th align="left">航班类型：</th>
                        <th align="left"><input name="radVoyageType" id="radVoyageType_1" type="radio" value="1" checked="checked" onclick="TicketSearch.isShowEndDate();"/>
<label for="radVoyageType_1">单程</label></th>
                        <th align="left"><input name="radVoyageType" id="radVoyageType_2" type="radio" value="2" onclick="TicketSearch.isShowEndDate();" />
<label for="radVoyageType_2">往返-联程</label></th>
  
                      </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td width="35%" height="30" align="left"><font color="#FF0000">*</font>出发地</td>
                    <td align="left">
                    <select id="ts_selFromCity" onchange="return TicketSearch.changeSeattle(this);">
                       <option value="0">请选择城市</option>
                    </select>
                    
                    </td>
                  </tr>
                  <tr>
                    <td width="35%" height="30" align="left"><font color="#FF0000">*</font>目的地</td>
                    <td align="left">
                    <select id="ts_selToCity" >
                      <option value="0">请选择城市</option>
                    </select>
                  
                    </td>
                       
                  </tr>
                  <tr>
                    <td width="35%" height="30" align="left"><font color="#FF0000">*</font>乘机人数</td>
                    <td align="left"><input name="textfield9" type="text" id="ts_txtPeopleNum" size="10" /></td>
                  </tr>
                  <tr>
                    <td width="35%" height="30" align="left"><font color="#FF0000">*</font>出发时间</td>
                    <td align="left"><input type="text" name="textfield10" id="ts_txtStartDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})"/><img style="position:relative; left:-18px; top:3px; *top:1px;" src="<%=ImageServerUrl%>/images/jipiao/time1.gif" width="16" height="13" onclick="javascript:$('#ts_txtStartDate').focus();" /></td>
                  </tr>
                  <tr style="display:none">
                    <td width="35%" height="30" align="left"><font color="#FF0000">*</font>返回时间</td>
                    <td align="left"><input type="text"  name="textfield10" id="ts_txtBackDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})"/><img style="position:relative; left:-18px; top:3px; *top:1px;" src="<%=ImageServerUrl%>/images/jipiao/time1.gif" width="16" height="13" onclick="javascript:$('#ts_txtBackDate').focus();"/></td>
                  </tr>
                  <tr>
                    <td width="35%" height="30" align="left">航空公司</td>
                    <td align="left"><input type="text" name="textfield11" id="ts_txtCompany" /></td>
                  </tr>
                  <tr>
                    <td width="35%" height="30" align="left">旅客类型</td>
                    <td align="left"><input name="ts_rdiPeopleType" type="radio" value="0" checked="checked" />
                内宾&nbsp;&nbsp;&nbsp;&nbsp;
                <input name="ts_rdiPeopleType" type="radio"  value="1" />
                外宾</td>
                  </tr>
                  <tr>
                    <td height="35" colspan="2" align="left"><a href="javascript:void(0);" onclick="return TicketSearch.search();"><img src="<%=ImageServerUrl%>/images/jipiao/btn.jpg"  alt="查询"/></a></td>
                  </tr>
                  
          </table>
           <div id="divHotCitys" >
                    <ul id="ulHotCitys">
                    </ul>
      </div>
       
        </div>
        <!--sidebar02_con_right-->
        <div class="sidebar02_con_right">
        	<div class="sidebar02_con_tableContain">
            	<table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <th>综合客服</th>
              </tr>
              <tr>
                <td height="97"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td height="30" align="left"><%= Utils.GetBigImgMQ2("27440") %></td>
                     <td align="left"><%= Utils.GetBigImgMQ2("27440") %></td>
                  </tr>
                  <tr>
                    <td height="30" align="left"><%= Utils.GetBigImgMQ2("27440") %></a></td>
                    <td align="left"><%= Utils.GetBigImgMQ2("27440") %></td>
                  </tr>
                  <tr>
                    <td height="30" align="left"><%= Utils.GetBigImgMQ2("27440") %></td>
                    <td align="left"><%= Utils.GetBigImgMQ2("27440") %></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td style="border-bottom:1px #999999 dashed;"></td>
              </tr>
              <tr>
                <th>综合客服电话</th>
              </tr>
              <tr>
                <td height="46"><span>机票客服:0571-56893761<br />
                机票客服:0571-56893761</span></td>
              </tr>
              <tr>
                <td style="border-bottom:1px #999999 dashed;"></td>
              </tr>
              <tr>
                <th>工作时间</th>
              </tr>
              <tr>
                <td><span>(9:00-17:30)周一至周五</span></td>
              </tr>
            </table>
            </div>
        </div> 
    </div>

      <script type="text/javascript">

          var TicketSearch =
        {
            selectItem: function(tr, input1) {
                if (tr.cells.length > 0) {

                    input1.attr("airId", $(tr.cells[1]).html());

                }
            },
            //查询机票
            search: function() {


                var startCity = $("#ts_selFromCity").val(); //始发城市ID
                var toCity = $("#ts_selToCity").val(); //目的地城市ID
                if (startCity == "0") {
                    alert("请选择始发地");
                    return false;
                }
                if (toCity == "0") {
                    alert("请选择目的地");
                    return false;
                }
                if (/^\d+$/.test($("#ts_txtPeopleNum").val()) != true) {
                    alert("请输入正确的乘机人数");
                    return false;
                }
                var airType = $("input[name='radVoyageType']:checked").val();
                var startDate = $("#ts_txtStartDate").val();
                var backDate = $("#ts_txtBackDate").val();
                if (startDate == "") {
                    alert("请选择出发时间");
                    return false;
                }
                if (airType == "2" && backDate == "") {
                    alert("请选择返回时间");
                    return false;
                }
                if (airType == "2") {
                    if (startDate > backDate) {
                        alert("出发时间不能大于返回时间");
                        return false;
                    }

                }
                var startCity1 = encodeURIComponent($("#ts_selFromCity").find("option:selected").html());
                var toCity1 = encodeURIComponent($("#ts_selToCity").find("option:selected").html());
                var peopleNum = $("#ts_txtPeopleNum").val();

                var airComapny = encodeURIComponent($("#ts_txtCompany").val());

                var peopleType = $("input[name='ts_rdiPeopleType']:checked").val();
                window.location = "TicketSelect.aspx?airType=" + airType + "&startCity=" + startCity + "&toCity=" + toCity + "&startCity1=" + startCity1 + "&toCity1=" + toCity1 + "&peopleNum=" +
                peopleNum + "&startDate=" + startDate + "&backDate=" + backDate + "&airCompany=" + airComapny + "&peopleType=" + peopleType;
                return false;
            },
            //是否显示返程日期
            isShowEndDate: function() {
                var airType = $("input[name='radVoyageType']:checked").val();
                if (airType == 2) {
                    $("#ts_txtBackDate").closest("tr").show();
                }
                else {
                    $("#ts_txtBackDate").closest("tr").hide();
                }
            },
            changeSeattle: function(tar) {
                var seatid = $(tar).val();
                TicketSearch.getSeattle(seatid);
                return false;
            },
            getSeattle: function(seattleId1) {
                $.ajax(
	               {
	                   url: "GetSeattles.ashx",
	                   data: { seattleId: seattleId1 },
	                   dataType: "text",
	                   cache: false,
	                   type: "get",
	                   success: function(result) {
	                       if (seattleId1 == "all") {
	                           $("#ts_selFromCity").html(result);
	                       }
	                       else if (seattleId1 == "0") {
	                           return false;
	                       }
	                       else
	                           $("#ts_selToCity").html(result);
	                   },
	                   error: function() {
	                       alert("获取城市时出错!");
	                   }
	               });
                return false;

            }


        }

        $(function() {


            TicketSearch.getSeattle("all");
          

        });
    </script>
     <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
    
</asp:Content>
