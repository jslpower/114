<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AirTicket.Master" AutoEventWireup="true" CodeBehind="TicketSelect.aspx.cs" Inherits="UserPublicCenter.AirTickets.TeamBook.TicketSelect" %>
<%@ Register Src="~/AirTickets/TeamBook/TicketTopMenu.ascx" TagName="TopMenu" TagPrefix="myASP" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">


 <link href="<%=CssManage.GetCssFilePath("tipsy") %>" rel="stylesheet" type="text/css" />
 <style type="text/css">
  #ts_divTicketList a.remark{ text-decoration:none; padding-top:10px;}
</style>
<myASP:TopMenu id="ts_ucTopMenu" runat="server" TabIndex="tab2"></myASP:TopMenu>
     <div class="sidebar02_con">
    	<div class="sidebar02_con_table01">
    	  <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FAFAFA" style="border:1px #CCCCCC solid;">
            <tr>
              <td height="5" colspan="5" align="left"></td>
            </tr>
            <tr>
              <td height="30" colspan="5" align="left"><b>航班类型：</b>
                  <input name="radVoyageType" type="radio" id="radVType1"  value="1" checked="checked"  onclick="TicketSelect.isShowEndDate();"/>
                  <strong><label for="radVType1">单程</label></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <input name="radVoyageType" type="radio" id="radVType2" value="2"  onclick="TicketSelect.isShowEndDate();"/>
                  <strong><label for="radVType2">往返-联程</label></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 </td>
            </tr>
            <tr>
              <td width="9%" height="30" align="left"><b>出发地</b></td>
              <td width="22%" align="left">
               <select id="ts_selFromCity" onchange="return TicketSelect.changeSeattle(this);">
                     <option value="0">请选择城市</option>
                    </select>
         
              <td width="9%" align="right"><b>目的地</b></td>
              <td colspan="2" align="left">
               <select id="ts_selToCity" >
                       <option value="0">请选择城市</option>
                    </select>
          
            </tr>
            <tr>
              <td height="30" align="left"><b>出发时间</b></td>
              <td align="left"><input type="text"  id="ts_txtStartDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})" value="<%=startDate %>"/>
                  <img style="position:relative; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl%>/images/jipiao/time.gif" width="16" height="13" onclick="javascript:$('#ts_txtStartDate').focus();"/></td>
              <td align="right"><b style="display:none">返回时间</b></td>
              <td width="24%" align="left"><input type="text" id="ts_txtBackDate"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})" value="<%=backDate %>" style="display:none;"/>
                  <img style="position:relative; display:none; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl%>/images/jipiao/time.gif" width="16" height="13" onclick="javascript:$('#ts_txtBackDate').focus();" /></td>
              <td width="36%" align="left"><img src="<%=ImageServerUrl%>/images/jipiao/btn.jpg" onclick="return TicketSelect.search();" style=" cursor:pointer;" /></td>
            </tr>
            <tr>
              <td colspan="5">&nbsp;</td>
            </tr>
          </table>
           <div id="divHotCitys" >
                    <ul id="ulHotCitys">
                    </ul></div>
    	</div>
        <div class="sidebar02_con_table02" id="ts_divTicketList">
        </div>
      
     
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("tipsy") %>"></script>
    <script type="text/javascript">
        var searchParams = { airType: "", startCity: "", toCity: "", startDate: "", backDate: "" }

        var TicketSelect =
        {
            closeDiv: function() {
                $("#ats_CompanyInfo").css("display", "none");
                return false;
            },
            //是否显示返程日期
            isShowEndDate: function() {
                var airType = $("input[name='radVoyageType']:checked").val();
                if (airType == 1) {
                    $("#ts_txtBackDate").hide().next("img").hide().parent("td").prev("td").html("");
                } else {
                    $("#ts_txtBackDate").show().next("img").show().parent("td").prev("td").html("<b>返回时间</b>");
                }
            },
          
            //查询
            search: function() {
                var startCity = $("#ts_selFromCity").val();
                var toCity = $("#ts_selToCity").val();
                if (startCity == "0") {
                    alert("请选择始发地");
                    return false;
                }
                if (toCity == "0") {
                    alert("请选择目的地");
                    return false;
                }

                for (var i in searchParams) {
                    searchParams[i] = "";
                }


                searchParams.peopleType = "<%=peopleType %>";
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
                searchParams.startCity = startCity;
                searchParams.toCity = toCity;
                searchParams.startDate = startDate;
                searchParams.backDate = backDate;
                searchParams.airType = airType;
                var startCity1 = $("#ts_selFromCity").find("option:selected").html();
                var toCity1 = $("#ts_selToCity").find("option:selected").html();
                searchParams.startCity1 = encodeURIComponent(startCity1);
                searchParams.toCity1 = encodeURIComponent(toCity1);
                LoadingImg.ShowLoading("ts_divTicketList");
                TicketSelect.getTicketList(); //查询机票
                document.title = "查询【" + startCity1 + "-" + toCity1 + "】【组团预定/散拼：机票】_同业114旅游交易平台";
                return false;
            },

            changeSeattle: function(tar) {
                var seatid = $(tar).val();
                TicketSelect.getSeattle(seatid, "", "");
                return false;
            },
            getSeattle: function(seattleId1, isSelect1, selectId1) {
                $.ajax(
	               {
	                   url: "GetSeattles.ashx",
	                   data: { seattleId: seattleId1, isSelect: isSelect1, selectId: selectId1 },
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

            },
            //获取供应商信息
            getSupplierInfo: function(sId, tar_a) {
                $.ajax({
                    type: "get",
                    dataType: "json",
                    url: "/AirTickets/TeamBook/TicketSelect.aspx?method=getInfo&sId=" + sId,
                    cache: false,
                    success: function(result) {

                        if (result.success == "1") {
                            var divInfo = $("#ats_CompanyInfo");
                            var theResult = eval(result);
                            divInfo.find("#ats_cName").html(theResult.contactName);
                            divInfo.find("#ats_cTel").html(theResult.tel);
                            divInfo.find("#ats_wTime").html(theResult.wTime);
                            divInfo.find("#ats_url").html('http://' + theResult.webSite).parent("a").attr("href", "http://" + theResult.webSite);
                            divInfo.find("#ats_lev").html(theResult.lev);
                            divInfo.find("#ats_success").html(theResult.successRate + "%");
                            divInfo.find("#ats_tTime").html(theResult.tTime);
                            divInfo.find("#bronze").attr("src", theResult.bronze);
                            divInfo.find("#trade").attr("src", theResult.trade);
                            divInfo.find("#cerImg").attr("src", theResult.cerImg);
                            divInfo.css("display", "block").css({ top: $(tar_a).position().top - 150, left: $(tar_a).position().left + 150 });
                        }
                        else {
                            alert("你尚未登录");
                        }
                    },
                    error: function() {
                        alert("查询失败!");
                    }
                });
                return false;
            },
            //调用Ajax获取数据
            getTicketList: function() {
                if (LoadingImg.IsLoadAddDataToDiv("ts_divTicketList")) {
                    $.ajax({
                        type: "get",
                        dataType: "html",
                        url: "/AirTickets/TeamBook/AjaxTicketSelect.aspx",
                        data: searchParams,
                        cache: false,
                        success: function(result) {
                            $("#ts_divTicketList").html(result);

                            $("#ts_divTicketList").find("a.remark").tipsy({ gravity: "s", fade: true });
                        },
                        error: function() {
                            alert("查询失败!");
                        }
                    });
                    return false;
                }
            }
        }
        $(function() {
            /*机票 BEGIN */
            TicketSelect.isShowEndDate();
          
            TicketSelect.getSeattle("all","isSelect","<%=startCity%>");
            TicketSelect.getSeattle("<%=startCity%>","isSelect","<%=toCity%>");
            searchParams.airType = "<%=airType%>";
            searchParams.startCity = "<%=startCity %>";
            searchParams.toCity = "<%=toCity %>";
            searchParams.startCity1 = "<%=startCity1%>";
            searchParams.toCity1 = "<%=toCityName%>";
            searchParams.startDate = "<%=startDate %>";
            searchParams.backDate = "<%=backDate %>";
            searchParams.peopleNum = "<%=peopleNum %>";
            searchParams.peopleType = "<%=peopleType %>";
            searchParams.airCompany = "<%=airCompany %>";
            if ("<%=airType%>" == "2") {
                $("#radVType2").attr("checked", "checked");
                $("#ts_txtBackDate").show().next("img").show().parent("td").prev("td").html("<b>返回时间</b>");
            }
            document.title = "选择-【<%=startCityName%>-<%=toCityName%>】-组团预定/散拼-机票_同业114旅游交易平台";
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            LoadingImg.ShowLoading("ts_divTicketList");
            TicketSelect.getTicketList(); //查询机票
          

            /*机票 END */
        });


    </script>
      <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
    
</asp:Content>
