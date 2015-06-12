<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalStandardRoute.aspx.cs"
    Inherits="UserBackCenter.LocalAgency.LocalStandardRoute" %>

<asp:content id="LocalStandardRoute" contentplaceholderid="ContentPlaceHolder1" runat="server">
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
        ReleaseType:'LocalStandardRoute'
    });
    </script>
<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth" id="<%=tblID %>">
      <tr><input type="hidden" id="LocalStandardRoute_hidRouteID" name="LocalStandardRoute_hidRouteID" runat="server" />
        <td valign="top"><table width="100%" height="33" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td style="border-bottom:1px solid #ABC9D9; width:15px;">&nbsp;</td>
            <td  class="tianup" id="TabLocalQuickRoute" runat="server"><a id="LocalQuickRouteLink" href="/localagency/localquickroute.aspx" onclick="return TourModule.CheckForm(this,'<%=tblID %>');">快速发布</a></td>

            <td style="border-bottom:1px solid #ABC9D9; width:10px; ">&nbsp;</td>
            <td class="tianon" id="TabLocalStandardRoute" runat="server">标准版发布</td>
            <td style="border-bottom:1px solid #ABC9D9;">&nbsp;</td>
          </tr>
        </table>
        <table width="98%" border="0" cellspacing="0" cellpadding="3" class="lankuang" style="margin-top:5px;">
            
            <tr>
              <td width="17%" align="right" class="shenglan_lr"><span class="shenghui"> <span style="color: #ff0000">*</span>线路名称：</span></td>

              <td width="83%" align="left"  class="zhonglan"><input name="LocalStandardRoute_RouteName" id="LocalStandardRoute_RouteName" runat="server" type="text" class="bitian" size="50" valid="required|limit" max="100" errmsg="请填写线路名称!|线路名称不能超过100个字!" />&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_<%=LocalStandardRoute_RouteName.ClientID %>"></span></td>
            </tr>
            <tr>
              <td align="right" class="shenglan_lr"><span class="ff0000">*</span>天数：</td>
              <td align="left"  class="zhonglan"><input name="LocalStandardRoute_TourDays" id="LocalStandardRoute_TourDays" runat="server" type="text" class="bitian" size="5" valid="required|RegInteger|custom" custom="CheckMaxDay" /><img src="<%=ImageServerPath %>/images/tian.gif" width="32" height="22" class="shijianbiao" onclick="TourModule.addTourDays('<%=tblID %>','<%=MaxTourDays %>');" style="cursor:pointer;" />&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_<%=LocalStandardRoute_TourDays.ClientID %>"></span></td>
            </tr>
          </table>
		  <cc1:TourPriceStand ID="LocalStandardRoute_tourpricestand" runat="server" ReleaseType="LocalStandardRoute" ModuleType="route" />
          <uc1:TourStandardPlan ID="LocalStandardRoute_StandardPlan" runat="server" ModuleType="route" ReleaseType="LocalStandardRoute" />
		  <cc1:ServiceStandard ID="LocalStandardRoute_ServiceStandard" runat="server" ModuleType="route" ReleaseType="LocalStandardRoute" />
		  <table width="96%"  border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang" style="margin-top:15px;">
                <tr>
                  <td width="10%" align="right" class="shenglan_lr">其他说明：</td>
                  <td width="80%" align="left" class="zhonglan"><textarea name="LocalStandardRoute_Service" id="LocalStandardRoute_Service" runat="server" cols="100" rows="7" class="input" style="color:#ccc;">不含项目：
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

                  <td width="80%" align="left" class="zhonglan"><textarea name="LocalStandardRoute_Remark" id="LocalStandardRoute_Remark" runat="server" cols="100" rows="3" class="input"></textarea></td>
                </tr>
              </table>
              <cc1:TourContactInfo id="LocalStandardRoute_TourContactInfo" runat="server" ReleaseType="LocalStandardRoute"></cc1:TourContactInfo>
          <table align="center" border="0" cellpadding="0" cellspacing="0" height="30" width="100%">
            <tr>
              <td width="23%" align="right" class="fontcolor">&nbsp;</td>
                <td class="fontcolor"><table width="100%" height="30"  border="0" align="left" cellpadding="0" cellspacing="0">
                  <tr>                    
                    <td width="23%" align="center"><img id="LocalStandardRoute_btnSubmit" onclick="TourModule.SubmitCheck(false,'/localagency/localstandardroute.aspx','线路列表','/localagency/localrouteview.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;" style="cursor:pointer;" src="<%=ImageServerPath %>/images/fabuan.gif" width="151" height="42" /></td>
              <td width="49%" align="left"><a href="JavaScript:void(0);" class="xiayiyec" id="LocalStandardRoute_btnAdd" onclick="TourModule.SubmitCheck(true,'/localagency/localstandardroute.aspx','线路列表','/localagency/localstandardroute.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;">保存并继续发布</a>&nbsp;&nbsp;&nbsp;&nbsp;<span id="span1" style="margin-top:10px;float:left;margin-left:10px; display:none; color:Red;">正在提交数据...</span> </td>
                  </tr>
                </table></td>
            </tr>
          </table>
	    </td>
      </tr>
    </table>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("autocomplete") %>"  cache="true"></script>
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("CommonTour") %>"  cache="true"></script>
    <script type="text/javascript"> 
        $("#<%=tblID %>").find("input[type=text][id$=TourDays]").attr("errmsg","请填写天数!|天数只能是数字!|天数不能大于<%=MaxTourDays %>天!");
        CheckFormIsChange.recodeInitialDataForm($("#<%=tblID %>").closest("form").get(0)); 
        
        FV_onBlur.initValid($("#<%=tblID %>").closest("form").get(0),null,false);
        
        $("#<%=tblID %>").find("input[type=text]").eq(0).focus();
        
        if($.trim($("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").val()) != $.trim('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n'))
        {
            $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").removeAttr("style");
        }
        
         $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").focus(function(){
            var sVal = $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").val();
            if($.trim(sVal) == $.trim('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n')){
                $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").val('');
                $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").removeAttr("style");
            }
        });
        
        $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").blur(function(){
            var sVal = $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").val();
            if($.trim(sVal) == ''){
                $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").val('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n');
                 $("#<%=tblID %>").find("textarea[name$=LocalStandardRoute_Service]").attr("style","color:#ccc;");
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
