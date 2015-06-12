<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserBackCenter.TicketsCenter.StatisticsManage.OrderStatistics.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="OrderStatistics" runat="server" contentplaceholderid="ContentPlaceHolder1">
<link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("517autocomplete") %>" />
 <table width="835" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#eef7ff" class="admin_tablebox" id="tb_OrderStatistics">
    	  <tr>
            <td colspan="3" height="10"></td>
          </tr>
          <tr>
            <td height="30" align="right">订单状态：</td>
            <td align="left">&nbsp;</td>
            <td align="left">
              <select name="sltOrderState" id="sltOrderState" runat="server" onchange="OrderStatistics.OrdChangTypeControl()">
                  </select>
                  <select name="sltOrdChangetype" id="sltOrdChangetype" runat="server" >
                  </select>
                  <input type="hidden" id="hidchangeType"  runat="server"/>
                  </td>
          </tr>
          <tr>
            <td width="175" height="30" align="right">始发地：</td>
            <td align="left">&nbsp;</td>
            <td align="left"><input type="text" name="txtStartDestination" id="txtStartDestination" /></td>
          </tr>
          <tr>
            <td width="175" height="30" align="right">目的地：</td>
            <td align="left">&nbsp;</td>
            <td align="left"><input type="text" name="txtEndDestination" id="txtEndDestination" /></td>
          </tr>
          <tr>
            <td height="30" align="right">起始交易日期：</td>
            <td align="left">&nbsp;</td>
            <td align="left"><input name="startDateTime" id="startDateTime" type="text"  onfocus="WdatePicker();" />
                <img style="position:relative; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl %>/images/jipiao/time.gif" width="16" height="13" onclick="$('#tb_OrderStatistics').find('input[id=startDateTime]').focus();" /></td>
          </tr>
          <tr>
            <td height="30" align="right">结束交易日期：</td>
            <td align="left">&nbsp;</td>
            <td align="left"><input name="txtEndDateTime" id="txtEndDateTime" type="text"  onfocus="WdatePicker();" />
                <img style="position:relative; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl %>/images/jipiao/time.gif" width="16" height="13" onclick="$('#tb_OrderStatistics').find('input[id=txtEndDateTime]').focus();"  /></td>
          </tr>
          
          <tr>
            <td height="40" align="right"></td>
            <td width="53" align="center">&nbsp;</td>
            <td width="605" align="left"><a href="javascript:void(0);" onclick="OrderStatistics.OnSearch();return false;"><img src="<%=ImageServerUrl %>/images/jipiao/admin_orderform_ybans_03.jpg" width="79" height="25"  alt="查询"/></a></td>
          </tr>
        </table>
        <script type="text/javascript">
            var OrderStatistics = {
                OnSearch: function() {
                    var Searchparms = { "StartDestination": "", "EndDestination": "", "SartDateTime": "", "EndDateTime": "", "OrderType": "", "TwoOrderType": "" };
                    Searchparms.StartDestination = $.trim($("#tb_OrderStatistics").find("input[id=txtStartDestination]").val());
                    Searchparms.EndDestination = $.trim($("#tb_OrderStatistics").find("input[id=txtEndDestination]").val());
                    Searchparms.SartDateTime = $.trim($("#tb_OrderStatistics").find("input[id=startDateTime]").val());
                    Searchparms.EndDateTime = $.trim($("#tb_OrderStatistics").find("input[id=txtEndDateTime]").val());
                    Searchparms.OrderType = $.trim($("#tb_OrderStatistics").find("select[id=<%=sltOrderState.ClientID%>] option:selected").val());
                    Searchparms.TwoOrderType = $.trim($("#tb_OrderStatistics").find("select[id=<%=sltOrdChangetype.ClientID%>] option:selected").val());
                    var goToUrl = " /TicketsCenter/StatisticsManage/OrderStatistics/OrderList.aspx?" + $.param(Searchparms);
                    topTab.url(topTab.activeTabIndex, goToUrl);
                },
                OrdChangTypeControl: function() {
                    var stateval = $("#<%=sltOrderState.ClientID %>").val();
                    var arrtype;
                    var changtype = $("#<%=hidchangeType.ClientID%>").val();
                    if (changtype.indexOf(",") > 0) {
                        arrtype = changtype.split(',');
                        for (var i = 0; i < arrtype.length - 1; i++) {
                            if (stateval == arrtype[i]) {
                                $('select[id=<%=sltOrdChangetype.ClientID%>] option').each(function() {
                                    if ($(this).val() != "0") {
                                        $(this).text(stateval + $(this).val());
                                    }
                                });
                                $("#<%=sltOrdChangetype.ClientID %>").show();
                                break;
                            } else {
                                $("#<%=sltOrdChangetype.ClientID %>").hide();
                                $("#<%=sltOrdChangetype.ClientID %>").val("0");
                            }
                        }
                    }
                }
            };
            $("#tb_OrderStatistics input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    OrderStatistics.OnSearch();
                    return false;
                }
            });
            $(document).ready(function() {
                ticketLKE.CityInputConfig.FromCityId = "txtStartDestination";
                ticketLKE.CityInputConfig.ToCityId = "txtEndDestination";

                ticketLKE.stringPort = "<%= EyouSoft.Common.Domain.UserPublicCenter %>";
                ticketLKE.initAutoComplete();
                OrderStatistics.OrdChangTypeControl();
            });
        </script>
</asp:content>
