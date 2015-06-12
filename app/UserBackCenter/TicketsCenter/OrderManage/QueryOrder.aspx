<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryOrder.aspx.cs" Inherits="UserBackCenter.TicketsCenter.OrderManage.QueryOrder" %>
<%@ Import Namespace="EyouSoft.Common" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<asp:content id="QueryOrder" contentplaceholderid="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("517autocomplete") %>" />
  	    <ul class="sub_leftmenu">
    	    <li><a href="/ticketscenter/ordermanage/queryorder.aspx" class="book_default" id="two1" onclick="QueryOrder.SetTab('two',1);return false;">搜索查询</a></li>
            <li><a href="/ticketscenter/ordermanage/queryorder.aspx" id="two2" onclick="QueryOrder.SetTab('two',2);return false;">精确查询</a></li>
        </ul>
        
        <script type="text/javascript">
            ticketLKE.CityInputConfig.FromCityId = 'QueryOrder_txtHomeCityId';
            ticketLKE.CityInputConfig.ToCityId = 'QueryOrder_txtDestCityId';
        </script>
        <div class="clearboth"></div>
        <div id="con_two_1">
        <table width="835" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#eef7ff" class="admin_tablebox">
	      <tr>
            <td colspan="3" height="10"></td>
          </tr>
          <tr>
            <td width="175" height="30" align="right">航空公司名：</td>
            <td colspan="2" align="left"><asp:DropDownList id="QueryOrder_drpFlightName" runat="server"></asp:DropDownList></td>
          </tr>
          <tr>
            <td height="30" align="right">始发地：</td>
            <td colspan="2" align="left"><input type="text" id="QueryOrder_txtHomeCityId" name="QueryOrder_txtHomeCityId" size="18" /><input type="hidden" id="QueryOrder_txtHomeCityIdLKE" name="QueryOrder_txtHomeCityIdLKE" /></td>
          </tr>
          <tr>
            <td height="30" align="right">目的地：</td>
            <td colspan="2" align="left"><input type="text" id="QueryOrder_txtDestCityId" name="QueryOrder_txtDestCityId" size="18" /><input type="hidden" id="QueryOrder_txtDestCityIdLKE" name="QueryOrder_txtDestCityIdLKE" /></td>
          </tr>
          <tr>
            <td height="30" align="right">机票类型：</td>
            <td colspan="2" align="left"><select name="QueryOrder_selRateType" id="QueryOrder_selRateType">
              <option value="3">团队散拼 </option>
            </select></td>
          </tr>
          <tr>
            <td height="30" align="right">订单生成时段：</td>
            <td colspan="2" align="left"><input type="text" size="10" id="QueryOrder_StartTime" name="QueryOrder_StartTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />&nbsp;至&nbsp;<input type="text" size="10" id="QueryOrder_EndTime" name="QueryOrder_EndTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" /></td>
          </tr>
          <tr>
            <td height="30" align="right">订单状态：</td>
            <td colspan="2" align="left"><select name="QueryOrder_selOrderState" id="QueryOrder_selOrderState" onchange="QueryOrder.ChangeState();">
              <option value="0">等待审核 </option>
              <option value="1">拒绝审核</option>
              <option value="2">审核通过</option>
              <option value="3">支付成功</option>
              <option value="4">拒绝出票</option>
              <option value="5">出票完成</option>
              <option value="7">所有退票</option>
              <option value="8">所有作废</option>
              <option value="9">所有改期</option>
              <option value="10">所有改签</option>
              <option value="11">所有变动</option>
              <option value="6">已取消订单</option>
            </select>
            <select name="QueryOrder_selOrderChangeState" id="QueryOrder_selOrderChangeState" style="display:none;">
            </select>
            </td>
          </tr>
          <tr>
            <td colspan="2" height="40" align="center"><img src="<%=ImageServerPath %>/images/jipiao/admin_orderform_ybans_03.jpg" width="79" height="25" onclick="QueryOrder.QueryData();return false;" style="cursor:pointer;" alt="点击查询" /></td>
            <td width="475" align="left">&nbsp;</td>
          </tr>
        </table>
        </div>
        <div class="clearboth"></div>
        <div id="con_two_2" style="display:none;">
        <table width="835" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#eef7ff" class="admin_tablebox">
	      <tr>
            <td colspan="3" height="10"></td>
          </tr>
          <tr>
            <td width="175" height="30" align="right">订单号：</td>
            <td colspan="2" align="left"><input type="text" name="QueryOrder_OrderNo" id="QueryOrder_OrderNo" /></td>
          </tr>
          <tr>
            <td height="30" align="right">票号：</td>
            <td colspan="2" align="left"><input type="text" name="QueryOrder_TicketNo" id="QueryOrder_TicketNo" /></td>
          </tr>
          <tr>
            <td height="30" align="right">游客名字：</td>
            <td colspan="2" align="left"><input type="text" name="QueryOrder_TravellerName" id="QueryOrder_TravellerName" /></td>
          </tr>
          <tr>
            <td height="30" align="right">PNR码：</td>
            <td colspan="2" align="left"><input type="text" name="QueryOrder_PNR" id="QueryOrder_PNR" /></td>
          </tr>
          
          <tr>
            <td height="40" align="center" colspan="2"><img src="<%=ImageServerPath %>/images/jipiao/admin_orderform_ybans_03.jpg" width="79" height="25" onclick="QueryOrder.QueryData();return false;" style="cursor:pointer;" alt="点击查询" /></td>
            <td width="475" align="left">&nbsp;</td>
          </tr>
        </table>
        </div>
        <script type="text/javascript">
        var QueryOrder = {
            type:1,    // 1：搜索查询，2：精确查找
            baseUrl:"/TicketsCenter/OrderManage/QueryOrderList.aspx",
            SetTab: function(name,num){
                $("#"+name+num).attr("class","book_default").parent("li").siblings("li").find("a").removeAttr("class");
                $("#con_"+name+"_"+num).css("display","").siblings("div[id^=con_"+ name +"]").css("display","none");
                this.type = num;
            },
            QueryData:function(){
                if(this.type == 1){
                    var FlightId = $("#<%=QueryOrder_drpFlightName.ClientID %>").val();
                    var HomeCityId = $("#QueryOrder_txtHomeCityIdLKE").val();
                    if(HomeCityId != null && HomeCityId != '' && HomeCityId != undefined)
                        HomeCityId = HomeCityId.split('|')[1];
                    var DestCityId = $("#QueryOrder_txtDestCityIdLKE").val();
                    if(DestCityId != null && DestCityId != '' && DestCityId != undefined)
                        DestCityId = DestCityId.split('|')[1];
                    var RateType = $("#QueryOrder_selRateType").val();
                    var StartTime = $("#QueryOrder_StartTime").val();
                    var EndTime = $("#QueryOrder_EndTime").val();
                    var OrderState = $("#QueryOrder_selOrderState").val();
                    var OrderChangeState = $("#QueryOrder_selOrderChangeState").val();
                    this.baseUrl += "?type="+ this.type +"&flightid="+ FlightId +"&homecityid="+ HomeCityId +"&destcityid="+ DestCityId +"&ratetype="+ RateType +"&starttime="+ StartTime +"&endtime="+ EndTime +"&orderstate="+ OrderState +"&orderchangestate=" + OrderChangeState;
                }else{
                    var OrderNo = $.trim($("#QueryOrder_OrderNo").val());
                    var TicketNo = $.trim($("#QueryOrder_TicketNo").val());
                    var TravellerName = $.trim($("#QueryOrder_TravellerName").val());
                    var PNR = $.trim($("#QueryOrder_PNR").val());
                    
                    if(OrderNo==""&&TicketNo==""&&TravellerName==""&&PNR==""){
                        alert("精确查询时，至少要填写一个查询条件");
                        return false;
                    }
                    
                    this.baseUrl += "?type="+ this.type +"&orderno="+ encodeURIComponent(OrderNo) +"&ticketno="+ encodeURIComponent(TicketNo) +"&travellername="+ encodeURIComponent(TravellerName) +"&PNR="+ PNR;
                }
                topTab.url(topTab.activeTabIndex,this.baseUrl);
            },
            ChangeState: function(){
                var OrderState = $("#QueryOrder_selOrderState").val();
                var str = '';
                switch(OrderState){
                    case '7':
                        str = '<option value=0>退票审核</option><option value=1>退票拒绝</option><option value=2>退票完成</option>';
                        $("#QueryOrder_selOrderChangeState").css("display","");
                        $("#QueryOrder_selOrderChangeState").html(str);
                        break;
                    case '8':
                        str = '<option value=0>作废审核</option><option value=1>作废拒绝</option><option value=2>作废完成</option>';
                        $("#QueryOrder_selOrderChangeState").css("display","");
                        $("#QueryOrder_selOrderChangeState").html(str);
                        break;
                    case '9':
                        str = '<option value=0>改期审核</option><option value=1>改期拒绝</option><option value=2>改期完成</option>';
                        $("#QueryOrder_selOrderChangeState").css("display","");
                        $("#QueryOrder_selOrderChangeState").html(str);
                        break;
                    case '10':
                        str = '<option value=0>改签审核</option><option value=1>改签拒绝</option><option value=2>改签完成</option>';
                        $("#QueryOrder_selOrderChangeState").css("display","");
                        $("#QueryOrder_selOrderChangeState").html(str);
                        break;
                    case '11':
                        str = '<option value=0>变动审核</option><option value=1>变动拒绝</option><option value=2>变动完成</option>';
                        $("#QueryOrder_selOrderChangeState").css("display","");
                        $("#QueryOrder_selOrderChangeState").html(str);
                        break;
                    default:
                        $("#QueryOrder_selOrderChangeState").css("display","none");
                        break;
                }
            }
        };
        
        $(function(){
            ticketLKE.initAutoComplete();
        });
        </script>
</asp:content>
