<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStandardRoute.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.RouteManage.AddStandardRoute" %>

<asp:content id="AddStandardRoute" contentplaceholderid="ContentPlaceHolder1" runat="server">
<%@ Register Src="~/usercontrol/RouteAgency/TourStandardPlan.ascx" TagName="StandardPlan"
    TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/RouteAgency/TourServiceStandard.ascx" TagName="ServiceStandard"
    TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/RouteAgency/TourPriceStand.ascx" TagName="TourPriceStand"
    TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/RouteAgency/TourContactInfo.ascx" TagName="TourContactInfo"
    TagPrefix="cc1" %>
<script type="text/javascript">
commonTourModuleData.add({
    ContainerID:'<%=tblID %>',
    ReleaseType:'AddStandardRoute'
});
</script>
<table id="<%=tblID %>" width="99%" height="500" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td valign="top"><table width="100%" height="33" border="0" cellpadding="0" cellspacing="0">

          <tr>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 15px;">&nbsp;</td>
            <td  class="tianup" id="TabAddQuickRoute" runat="server"><a id="AddQuickRouteLink" href="/routeagency/routemanage/addquickroute.aspx" onclick="return TourModule.CheckForm(this,'<%=tblID %>');">快速发布</a></td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
            <td class="tianon" id="TabAddStandardRoute" runat="server">标准版发布</td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217);">&nbsp;</td>
          </tr>
        </table>
<input type="hidden" id="AddStandardRoute_hidRouteID" name="AddStandardRoute_hidRouteID" runat="server" />
		  <table width="96%" border="0" cellspacing="0" cellpadding="3" class="lankuang" style="margin-top:5px;" align="center">
            <tr>
              <td width="17%" align="right" class="shenglan_lr"><span class="shenghui"><span style="color: #ff0000">*</span>线路区域：</span></td>
              <td width="83%" align="left"  class="zhonglan"><select name="AddStandardRoute_RouteArea" id="AddStandardRoute_RouteArea" runat="server" valid="required" errmsg="请选择线路区域!">
                <option value="">请选择</option>
              </select>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddStandardRoute_RouteArea.ClientID %>" class="errmsg"></span></td>
            </tr>

            <tr>
              <td align="right" class="shenglan_lr"><span class="shenghui"> <span style="color: #ff0000">*</span>线路名称：</span></td>
              <td align="left"  class="zhonglan"><input name="AddStandardRoute_RouteName" id="AddStandardRoute_RouteName" runat="server" type="text" class="bitian" size="50" valid="required|limit" max="100" errmsg="请填写线路名称!|线路名称不能超过100个字!" />&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddStandardRoute_RouteName.ClientID %>" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>天数：</td>
              <td align="left"  class="zhonglan"><input name="AddStandardRoute_txtTourDays" id="AddStandardRoute_txtTourDays" runat="server" type="text" class="bitian" size="5" valid="required|RegInteger|custom" custom="CheckMaxDay" /><img src="<%=ImageServerPath %>/images/tian.gif" width="32" height="22" class="shijianbiao" onclick="TourModule.addTourDays('<%=tblID %>','<%=MaxTourDays %>');" style="cursor:pointer;" />&nbsp;<span id="errMsg_<%=AddStandardRoute_txtTourDays.ClientID %>" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>线路主题：</td>
              <td align="left"  class="zhonglan"><%=strRouteTheme%>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_AddStandardRoute_chkRouteTopic" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>出港城市：</td>
              <td align="left"  class="zhonglan"><span id="divLeaveCity"><%=strLeaveCity%></span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_AddStandardRoute_radPortCity" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>销售区域：</td>
              <td align="left"  class="zhonglan"><%=strSaleCity %>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_AddStandardRoute_chkSaleCity" class="errmsg"></span></td>
            </tr>
          </table>
		 <div id="divPriceDetail"></div>		 
          <cc1:TourPriceStand ID="AddStandardRoute_tourpricestand" runat="server" ReleaseType="AddStandardRoute" ModuleType="route" />
		  <cc1:StandardPlan ID="AddStandardRoute_StandardPlan" runat="server" ModuleType="route" ReleaseType="AddStandardRoute" />
		  <cc1:ServiceStandard ID="AddStandardRoute_ServiceStandard" runat="server" ModuleType="route" ReleaseType="AddStandardRoute" />
		  <table width="96%"  border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang" style="margin-top:15px;">
                <tr>
                  <td width="10%" align="right" class="shenglan_lr">其他说明：</td>
                  <td width="80%" align="left" class="zhonglan"><textarea name="AddStandardRoute_Service" id="AddStandardRoute_Service" runat="server" cols="100" rows="7" class="input" style="color:#ccc;">不含项目：
自费项目：
儿童安排：
购物安排：
赠送项目：
温馨提醒：</textarea></td>
                </tr>
          </table>
          <table width="96%"  border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang" style="margin-top:15px;">
                <tr>
                  <td width="10%" align="right" class="shenglan_lr">备注：</td>

                  <td width="80%" align="left" class="zhonglan"><textarea name="AddStandardRoute_Remark" id="AddStandardRoute_Remark" runat="server" cols="100" rows="3" class="input"></textarea></td>
                </tr>
              </table>
              <cc1:TourContactInfo id="AddStandardRoute_TourContactInfo" runat="server" ReleaseType="AddStandardRoute"></cc1:TourContactInfo>

              <table width="100%" height="60" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  
                  <td width="27%" align="center"><img id="AddStandardRoute_btnSubmit" onclick="TourModule.SubmitCheck(false,'/routeagency/routemanage/addstandardroute.aspx','线路维护','/routeagency/routemanage/routeview.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;" style="cursor:pointer;" src="<%=ImageServerPath %>/images/fabuan.gif" width="151" height="42" /><span id="spanSubmit" style="display:none; color:Red;">正在提交...</span></td>
                  <td width="49%" align="left"><a href="JavaScript:void(0);" class="xiayiyec" id="AddStandardRoute_btnAdd" onclick="TourModule.SubmitCheck(true,'/routeagency/routemanage/addstandardroute.aspx','','/routeagency/routemanage/addstandardroute.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;">保存并继续发布</a>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanSubmit1" style="margin-top:10px;float:left;margin-left:10px; display:none; color:Red;">正在提交数据...</span>  </td>
                </tr>

              </table></td>
      </tr>
    </table>
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("CommonTour") %>"  cache="true"></script>
    <script type="text/javascript">
        $("#<%=tblID %>").find("input[type=text][id$=TourDays]").attr("errmsg","请填写天数!|天数只能是数字!|天数不能大于<%=MaxTourDays %>天!");
        CheckFormIsChange.recodeInitialDataForm($("#<%=tblID %>").closest("form").get(0)); 
        
        FV_onBlur.initValid($("#<%=tblID %>").closest("form").get(0),null,false);
        
        $("#<%=tblID %>").find("input[type=text]").eq(0).focus();
        
        if($.trim($("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").val()) != $.trim('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n'))
        {
            $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").removeAttr("style");
        }
        
        $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").focus(function(){
            var sVal = $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").val();
            if($.trim(sVal) == $.trim('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n')){
                $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").val('');
                $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").removeAttr("style");
            }
        });
        
        $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").blur(function(){
            var sVal = $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").val();
            if($.trim(sVal) == ''){
                $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").val('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n');
                $("#<%=tblID %>").find("textarea[name$=AddStandardRoute_Service]").attr("style","color:#ccc;");
            }
        });
        
        function CheckMaxDay(e,formElements){
		    if(parseInt(e.value) <= parseInt('<%=MaxTourDays %>'))
		    {
		        return true;
		    }else{
		        return false;
		    }
	    }
        
        $("#<%=tblID %>").find("input[type=text][name$=TourDays]").blur(function(){TourModule.TourDaysFocus('<%=tblID %>','<%=MaxTourDays %>');});
                   
//        $("#<%=tblID %>").find("input[type=text][name$=TourDays]").change(function(){TourStandardPlan.AddDateInfo(null,'<%=tblID %>')});      
        
         if($("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != '' && $("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != null && $("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != undefined)
    {
        $("#<%=tblID %>").find("img[id$=btnSubmit]").attr("src","<%=ImageServerPath %>/images/save(1).gif");
    }
    </script>
</asp:content>
