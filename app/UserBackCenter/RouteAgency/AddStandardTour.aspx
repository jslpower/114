<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStandardTour.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AddStandardTour" %>

<asp:content id="AddStandardTourTbl" contentplaceholderid="ContentPlaceHolder1" runat="server">
<%@ Register Src="~/usercontrol/RouteAgency/TourStandardPlan.ascx" TagName="TourStandardPlan"
    TagPrefix="uc1" %>
<%@ Register Src="~/usercontrol/RouteAgency/TourServiceStandard.ascx" TagName="ServiceStandard"
    TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/RouteAgency/TourPriceStand.ascx" TagName="TourPriceStand"
    TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/RouteAgency/TourContactInfo.ascx" TagName="TourContactInfo"
    TagPrefix="cc1" %>
<script type="text/javascript">
commonTourModuleData.add({
    ContainerID:'<%=tblID %>',
    ReleaseType:'AddStandardTour'
});
</script>
<table id="<%=tblID %>" width="99%" height="500" border="0" cellpadding="0" cellspacing="0">
<input type="hidden" id="AddStandardTour_hidTourID" name="AddStandardTour_hidTourID" runat="server" />
<input type="hidden" id="AddStandardTour_TemplateTourID" name="AddStandardTour_TemplateTourID" runat="server" />
      <tr>
        <td valign="top"><table width="100%" height="33" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 15px;">&nbsp;</td>
            <td  class="tianup" id="TabAddQuickTour" runat="server"><a id="AddQuickTourLink" href="/routeagency/addquicktour.aspx" onclick="return TourModule.CheckForm(this,'<%=tblID %>');">快速发布</a></td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
            <td class="tianon" id="TabAddStandardTour" runat="server">标准版发布</td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217);">&nbsp;</td>
          </tr>
        </table>

		  <table width="96%" border="0" cellspacing="0" cellpadding="3" class="lankuang" style="margin-top:5px;" align="center">
            <tr>
              <td width="17%" align="right" class="shenglan_lr"><span class="shenghui"><span style="color: #ff0000">*</span>线路区域：</span></td>
              <td width="83%" align="left"  class="zhonglan"><select name="AddStandardTour_RouteArea" id="AddStandardTour_RouteArea" runat="server" valid="required" errmsg="请选择线路区域!">
                <option value="">请选择</option>
              </select>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddStandardTour_RouteArea.ClientID %>" class="errmsg"></span></td>
            </tr>

            <tr>
              <td align="right" class="shenglan_lr"><span class="shenghui"> <span style="color: #ff0000">*</span>线路名称：</span></td>
              <td align="left"  class="zhonglan"><input name="AddStandardTour_RouteName" id="AddStandardTour_RouteName" runat="server" type="text" class="bitian" size="50" valid="required|limit" max="100" errmsg="请填写或选择线路名称!|线路名称不能超过100个字!" />
                <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择线路','/RouteAgency/RouteList.aspx?ReleaseType=Standard&ContainerID=<%=tblID %>&rnd='+ Math.random(),550,400);return false;">从线路库导入</a>
                <label for="<%=AddStandardTour_AddToRoute.ClientID %>"><input type="checkbox" name="AddStandardTour_AddToRoute" id="AddStandardTour_AddToRoute" runat="server" value="checkbox" />
添加到线路库</label>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddStandardTour_RouteName.ClientID %>" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>天数：</td>
              <td align="left" class="zhonglan"><input name="AddStandardTour_txtTourDays" id="AddStandardTour_txtTourDays" runat="server" type="text" class="bitian" size="5" valid="required|RegInteger|custom" custom="CheckMaxDay" /><img src="<%=ImageServerPath %>/images/tian.gif" width="32" height="22" class="shijianbiao" onclick="TourModule.addTourDays('<%=tblID %>','<%=MaxTourDays %>');" style="cursor:pointer;" />&nbsp;<span id="errMsg_<%=AddStandardTour_txtTourDays.ClientID %>" class="errmsg"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="ff0000">*</span>人数：
              <input name="AddStandardTour_PeopleNumber" id="AddStandardTour_PeopleNumber" runat="server" type="text" class="bitian" size="8" valid="required|RegInteger" errmsg="请填写团队人数!|团队人数只能是数字!" />&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddStandardTour_PeopleNumber.ClientID %>" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr">线路主题：</td>

              <td align="left"  class="zhonglan"><%=strRouteTheme%></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>出团日期：</td>
              <td align="left"  class="zhonglan"><span id="spanSelectDate" runat="server"><a href="javascript:void(0)" onclick="TourModule.OpenDateDialog('<%=tblID %>');return false;"><img src="<%=ImageServerPath %>/images/index_22.gif" width="21" height="14" /><span style="font-size:14px; font-weight:bold">请选择时间</span></a> </span>   <span id="spanAlreadyDate" runat="server">（已生成<span style="font-size:16px; color:#ff6600;" id="spanChildCount"><strong><asp:Literal id="AddStandardTour_ChildTourCout" text="0" runat="server"></asp:Literal></strong></span>个团队）</span><asp:Label id="lblEditText" runat="server"></asp:Label> <span id="spanSetTourNo" runat="server"><img src="<%=ImageServerPath %>/images/jiben5.gif" width="8" height="12" /><a href="javascript:void(0)" onclick="TourModule.OpenDialog('设置团号规则','/RouteAgency/SetTourNo.aspx?ReleaseType=AddStandardTour&rnd='+ Math.random(),500,400);return false;"><span class="huise">设置团号规则</span></a></span><input type="hidden" id="hidTourLeaveDate" name="hidTourLeaveDate" runat="server" valid="required" errmsg="请选择出团日期!" />&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_hidTourLeaveDate" class="errmsg"></span><input type="hidden" id="AddStandardTour_hidChildLeaveDateList" name="AddStandardTour_hidChildLeaveDateList" runat="server" /><input type="hidden" id="AddStandardTour_hidChildTourCodeList" name="AddStandardTour_hidChildTourCodeList" runat="server" /></td>

            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>出港城市：</td>
              <td align="left"  class="zhonglan"><span id="divLeaveCity"><%=strLeaveCity%></span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_AddStandardTour_radPortCity" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>销售区域：</td>

              <td align="left"  class="zhonglan"><%=strSaleCity %>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_AddStandardTour_chkSaleCity" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr">交通安排：</td>
              <td align="left"  class="zhonglan">
              <textarea id="AddStandardTour_LeaveTraffic" name="AddStandardTour_LeaveTraffic" runat="server" cols="85" rows="3" class="input"></textarea></td>
            </tr>

            <tr>
              <td align="right" class="shenglan_lr"><a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择集合方式','/RouteAgency/ServiceStandardList.aspx?Type=8&ReleaseType=AddStandardTour&ContainerID=<%=tblID %>',550,400);return false;">
                集合方式<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11"
                    height="12" border="0" /></a>：</td>
              <td align="left"  class="zhonglan">
              <textarea name="AddStandardTour_CollectionContect" id="AddStandardTour_CollectionContect" cols="85" rows="3" class="input"><%=CollectionContect%></textarea></td>
              </tr>
              <tr>
              <td align="right" class="shenglan_lr">
             <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择接团方式','/RouteAgency/ServiceStandardList.aspx?Type=9&ReleaseType=AddStandardTour&ContainerID=<%=tblID %>',550,400);return false;">
                接团方式<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11"
                    height="12" border="0" /></a>：</td><td align="left"  class="zhonglan">
              <textarea name="AddStandardTour_MeetTourContect" id="AddStandardTour_MeetTourContect" cols="85" rows="3" class="input"><%=MeetTourContect%></textarea></td>
            </tr>
          </table>		 
          <cc1:TourPriceStand ID="AddStandardTour_tourpricestand" runat="server" ReleaseType="AddStandardTour" ModuleType="tour" />
          <uc1:TourStandardPlan ID="AddStandardTour_StandardPlan" runat="server" ModuleType="tour" ReleaseType="AddStandardTour" />
		  <cc1:ServiceStandard ID="AddStandardTour_ServiceStandard" runat="server" ModuleType="tour" ReleaseType="AddStandardTour" />
		  <table width="96%"  border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang" style="margin-top:15px;">
                <tr>
                  <td width="10%" align="right" class="shenglan_lr">其他说明：</td>
                  <td width="80%" align="left" class="zhonglan"><textarea name="AddStandardTour_Service" id="AddStandardTour_Service" runat="server" cols="100" rows="7" class="input" style="color:#ccc;">不含项目：
自费项目：
儿童安排：
购物安排：
赠送项目：
温馨提醒：
</textarea></td>
                </tr>
          </table>
          <table width="96%"  border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang" style="margin-top:15px;">
                <tr>
                  <td width="10%" align="right" class="shenglan_lr">备注：</td>

                  <td width="80%" align="left" class="zhonglan"><textarea name="AddStandardTour_Remark" id="AddStandardTour_Remark" runat="server" cols="100" rows="3" class="input"></textarea></td>
                </tr>
              </table>
              <cc1:TourContactInfo id="AddStandardTour_TourContactInfo" runat="server" ReleaseType="AddStandardTour"></cc1:TourContactInfo>
              <table width="96%" border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang"
    style="margin-top: 10px;">
                <%=strLocalInfo.ToString()%>
                <tr>
                  <td width="12%" align="right" class="shenglan">地接社信息： </td>
                  <td align="left" class="zhonglan"><input name="AddStandardTour_LocalAgency" type="text" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;许可证号：&nbsp;&nbsp;<input name="AddStandardTour_License" type="text" />&nbsp;&nbsp;&nbsp;&nbsp;电话：&nbsp;&nbsp;<input name="AddStandardTour_LocalAgencyTel" type="text" />&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0);return false;" onclick="TourModule.AddOnline(this);return false;">增加一行</a> <a href="javascript:void(0);return false;" onclick="TourModule.Delline(this,'agency');return false;">删除</a></td>
                </tr>
              </table>

              <table width="100%" height="60" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="24%">自动停收设置：
                    <input name="AddStandardTour_AutoOffDays" id="AddStandardTour_AutoOffDays" runat="server" type="text" class="shurukuang" size="4" value="1" />天</td>
                  <td width="27%" align="center"><img id="AddStandardTour_btnSubmit" onclick="TourModule.SubmitCheck(false,'/routeagency/addstandardtour.aspx','未出发团队','<%=templateTourId.Trim()!=""?"/routeagency/NotStartingTeamsDetail.aspx?TemplateTourID="+templateTourId:"/routeagency/notstartingteams.aspx" %>','<%=tblID %>','<%=MaxTourDays %>');return false;" style="cursor:pointer;" src="<%=ImageServerPath %>/images/fabuan.gif" width="151" height="42" /><span id="spanSubmit" style="display:none; color:Red;">正在提交...</span></td>
                  <td width="49%" align="left"><a href="JavaScript:void(0);" class="xiayiyec" id="AddStandardTour_btnAdd" onclick="TourModule.SubmitCheck(true,'/routeagency/addstandardtour.aspx','','/routeagency/addstandardtour.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;">保存并继续发布</a>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanSubmit1" style="margin-top:10px;float:left;margin-left:10px; display:none; color:Red;">正在提交数据...</span> </td>
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
        
        if($.trim($("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").val()) != $.trim('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n'))
        {
            $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").removeAttr("style");
        }
        
         $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").focus(function(){
            var sVal = $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").val();
            if($.trim(sVal) == $.trim('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n')){
                $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").val('')
                $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").removeAttr("style");
            }
        });
        
        $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").blur(function(){
            var sVal = $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").val();
            if($.trim(sVal) == ''){
                $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").val('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n');
                $("#<%=tblID %>").find("textarea[name$=AddStandardTour_Service]").attr("style","color:#ccc;");
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
//        $("#<%=tblID %>").find("input[type=text][name$=TourDays]").change(function(){TourStandardPlan.AddDateInfo(null,'<%=tblID %>');}); 
        
        if(($("#<%=tblID %>").find("input[type=hidden][id$=hidTourID]").val() != '' && $("#<%=tblID %>").find("input[type=hidden][id$=hidTourID]").val() != null && $("#<%=tblID %>").find("input[type=hidden][id$=hidTourID]").val() != undefined) || ($("#<%=tblID %>").find("input[type=hidden][id$=TemplateTourID]").val() != '' && $("#<%=tblID %>").find("input[type=hidden][id$=TemplateTourID]").val() != null && $("#<%=tblID %>").find("input[type=hidden][id$=TemplateTourID]").val() != undefined))
    {
        $("#<%=tblID %>").find("img[id$=btnSubmit]").attr("src","<%=ImageServerPath %>/images/save(1).gif");
    }
    </script>
</asp:content>
