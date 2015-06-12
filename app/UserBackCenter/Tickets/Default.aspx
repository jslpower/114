<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserBackCenter.Tickets.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="Tickets" runat="server" contentplaceholderid="ContentPlaceHolder1">

   <script type="text/javascript" src="<%=JsManage.GetJsFilePath("517ticketcore") %>" cache="true"></script>
 

    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("autocomplete") %>" />
<style>
    .option-tab-un span, .option-tab-on span
    {
        width: 70px;
    }
    .inputk
    {
        border: 1px #4592BF solid;
        background: url(<%=ImageServerUrl %>/images/jpinput2.gif) repeat-x;
        width: 100px;
        height: 17px;
        padding: 2px 0 0 2px;
    }
    .inputtxt
    {
        border: 0px #ffffff solid;
        background: url(<%=ImageServerUrl %>/images/jpinput.gif);
        width: 130px;
        height: 19px;
        padding: 2px 0 0 2px;
    }
</style>
<table height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td valign="top" >
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="68%" align="left"><img src="<%=ImageServerUrl %>/images/skytitle.gif" width="490" height="55" /></td>
    <td width="32%">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" background="<%=ImageServerUrl %>/images/skybar2.gif"><img src="<%=ImageServerUrl %>/images/skybar1.gif" width="222" height="27" /></td>
    <td align="right" background="<%=ImageServerUrl %>/images/skybar2.gif"><img src="<%=ImageServerUrl %>/images/skybar3.gif" width="9" height="27" /></td>
  </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="526" align="right" valign="top" style="background:url(<%=ImageServerUrl %>images/skybj.gif) repeat-x top;"><table border="0" cellpadding="2" cellspacing="0" style="margin-top:50px; width:80%;">
     <tr>
        <td height="30" align="right" valign="bottom">航班类型</td>
        <td align="left" valign="bottom"><input type="radio" value="1" id="radVoyageType_1" name="radVoyageType" checked="checked" onclick="isShowEndDate();" ><label for="radVoyageType_1" style="cursor:pointer">单程</label> <input type="radio" value="2" id="radVoyageType_2" name="radVoyageType" onclick="isShowEndDate();"><label for="radVoyageType_2" style="cursor:pointer">返程</label> </div>
            </td>
        </tr>
      <tr>
        <td width="26%" height="30" align="right" valign="bottom">出发城市</td>
        <td align="left" valign="bottom"><table border="0" align="left" cellpadding="0" cellspacing="0">
          <tr>
            <td width="30%" align="left" style="padding-left:0px;"><input name="txtFromCity" id="txtFromCity"  type="text" size="10" class="inputtxt" /><input
                      type="hidden" id="txtFromCityLKE" name="txtFromCityLKE" /></td>
            <td width="70%" align="left"><img  style="cursor:pointer" id="fromCity" src="<%=ImageServerUrl %>/images/jpinputb.gif" width="23" height="21" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="30" align="right" valign="bottom">目的城市</td>
        <td align="left" valign="bottom"><table border="0" align="left" cellpadding="0" cellspacing="0">
          <tr>
            <td width="30%" align="left" style="padding-left:0px;"><input  name="txtToCity" id="txtToCity"  type="text" size="10" class="inputtxt" /><input type="hidden" id="txtToCityLKE" name="txtToCityLKE" /></td>
            <td width="70%" align="left"><img id="toCity" src="<%=ImageServerUrl %>/images/jpinputb.gif" style="cursor:pointer" width="23" height="21" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="30" align="right" valign="bottom">出发时间</td>
        <td align="left" valign="bottom"><input name="startDateTime" id="startDateTime" type="text" class="inputk"  onfocus="WdatePicker({minDate:'%y-%M-#{%d}'});" />
         
            </td>
        </tr>
        <tr id="trEndDate" style="display:none">
        <td height="30" align="right" valign="bottom">返程时间</td>
        <td align="left" valign="bottom"><input name="endDateTime" id="endDateTime" type="text" class="inputk"  onfocus="WdatePicker({minDate:'%y-%M-#{%d}'});" />
         
            </td>
        </tr>
    </table>
  
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="42%" height="47" align="center">&nbsp;</td>
          <td width="58%" align="left"><img style="cursor:pointer" onclick="onClickTitck();" src="<%=ImageServerUrl %>/images/jpbutton.gif" width="117" height="24" /></td>
        </tr>
    </table></td>
    <td width="464" height="280" style="background:url(<%=ImageServerUrl %>/images/skybj.gif) repeat-x top; padding-top:20px;"><img src="<%=ImageServerUrl %>/images/jpword.gif" width="210" height="180" /></td>
  </tr>
</table>
		</td>
      </tr>   <div id="divHotCitys">
            <ul id="ulHotCitys">
            </ul>
      </div>
      </table>
      

    <script type="text/javascript">

        var TicketDefault = { 
            clearIn: null,
            isLoad:false
        };
        
        function isShowEndDate() {
            var radVoyageType = $("input[name='radVoyageType']:checked").val();
            if (radVoyageType == 2) {
                $("#trEndDate").show();
            } else {
                $("#trEndDate").hide();
            }
        }

        function onClickTitck() {
            if ("<%=IsLogin %>" == "True") {
                ticketLKE.ajaxPost();
            } else {
                window.location.href = "<%=LoginUrl %>";
                return false;
            }
        }
        $(function() {
            TicketDefault.clearIn = setInterval(function() {
                if (ticketLKE && TicketDefault.clearIn != null) {
                    ticketLKE.CityInputConfig.FromCityId = "txtFromCity";
                    ticketLKE.CityInputConfig.ToCityId = "txtToCity";
                    ticketLKE.TimeDateConfig.dateTimeControlId = "startDateTime";
                    ticketLKE.TimeDateConfig.EnddateTimeControlId = "endDateTime";

                    ticketLKE.stringPort = "<% = EyouSoft.Common.Domain.UserPublicCenter %>";
                    ticketLKE.initAutoComplete();

                    clearInterval(TicketDefault.clearIn);
                    TicketDefault.clearIn = null;
                }
            }, 200);

            $("#fromCity").click(function() {
                if (!TicketDefault.isLoad) {
                    ticketLKE.initHotCitys();
                    TicketDefault.isLoad = true;
                }
                ticketLKE.showHotCitys(1);
                ticketLKE.hiddenDiv();
            });

            $("#toCity").click(function() {
                if (!TicketDefault.isLoad) {
                    ticketLKE.initHotCitys();
                    TicketDefault.isLoad = true;
                }
                ticketLKE.showHotCitys(2);
                ticketLKE.hiddenDiv();
            });
        });
    </script>
</asp:content>
