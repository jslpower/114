<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuickTour.aspx.cs" Inherits="UserBackCenter.RouteAgency.AddQuickTour" %>

<asp:content id="AddQuickTour" contentplaceholderid="ContentPlaceHolder1" runat="server">
<%@ Register Src="~/usercontrol/RouteAgency/TourPriceStand.ascx" TagName="TourPriceStand"
    TagPrefix="cc1" %>
    <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="true"></script>
<script type="text/javascript">
commonTourModuleData.add({
    ContainerID:'<%=tblID %>',
    ReleaseType:'AddQuickTour',
    FCKID:'<%=FCKID %>'
});
</script>
<table id="<%=tblID %>" width="99%" border="0" cellpadding="0" cellspacing="0">
<input type="hidden" id="AddQuickTour_hidTourID" name="AddQuickTour_hidTourID" runat="server" />
<input type="hidden" id="AddQuickTour_TemplateTourID" name="AddQuickTour_TemplateTourID" runat="server" />
      <tr>
        <td valign="top"><table width="100%" height="33" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 15px;">&nbsp;</td>
            <td  class="tianon" id="TabAddQuickTour" runat="server">快速发布</td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
            <td class="tianup" id="TabAddStandardTour" runat="server"><a id="AddStandardTourLink" href="/routeagency/addstandardtour.aspx" onclick="return TourModule.CheckForm(this,'<%=tblID %>');">标准版发布</a></td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217);">&nbsp;</td>
          </tr>
        </table>
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:10px;">
            <tr>
              <td width="2%">&nbsp;</td>
              <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben1.gif" width="128" height="34" /></td>
            </tr>
          </table>
          <table border="0" align="center" cellpadding="4" cellspacing="1" style="border:1px solid #ECECEC; background:#FAFAFA; margin:8px 0px 10px 0px; width:99%;">
            <tr>
              <td width="11%" align="right"><span class="ff0000">*</span>线路区域：</td>

              <td colspan="3" align="left"><select name="AddQuickTour_RouteArea" id="AddQuickTour_RouteArea" runat="server" valid="required" errmsg="请选择线路区域!">
                <option value="">请选择</option>
              </select>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddQuickTour_RouteArea.ClientID %>" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right"><span class="ff0000">*</span>线路名称：</td>
              <td colspan="3" align="left"><input name="AddQuickTour_RouteName" id="AddQuickTour_RouteName" runat="server" type="text" class="bitian" size="50" valid="required|limit" max="100" errmsg="请填写或选择线路名称!|线路名称不能超过100个字!" />
                <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择线路','/RouteAgency/RouteList.aspx?ReleaseType=Quick&ContainerID=<%=tblID %>&rnd='+ Math.random(),550,400);return false;">从线路库导入</a>
              <label for="<%=AddQuickTour_AddToRoute.ClientID %>"><input type="checkbox" name="AddQuickTour_AddToRoute" id="AddQuickTour_AddToRoute" runat="server" value="checkbox" />添加到线路库</label>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddQuickTour_RouteName.ClientID %>" class="errmsg"></span>
</td>
            </tr>
            <tr>
              <td align="right"><span class="ff0000">*</span>天数：</td>
              <td width="25%" align="left"><input name="AddQuickTour_TourDays" id="AddQuickTour_TourDays" runat="server" type="text" class="bitian" size="5" valid="required|RegInteger|custom" custom="CheckMaxDay" /><img src="<%=ImageServerPath %>/images/tian.gif" width="32" height="22" class="shijianbiao" onclick="TourModule.addTourDays('<%=tblID %>','<%=MaxTourDays %>');" style="cursor:pointer;"/>&nbsp;<span class="errmsg" id="errMsg_<%=AddQuickTour_TourDays.ClientID %>"></span></td>
              <td width="8%" align="right"><span class="ff0000">*</span>人数：</td>
              <td align="left"><input name="AddQuickTour_PeopleNumber" id="AddQuickTour_PeopleNumber" runat="server" type="text" class="bitian" size="8" valid="required|RegInteger" errmsg="请填写团队人数!|团队人数只能是数字!" />&nbsp;&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_<%=AddQuickTour_PeopleNumber.ClientID %>"></span></td>
            </tr>            
            <tr>
              <td align="right" bgcolor="#FBFFFB" style="border-top:2px dashed #DBEFDB;"><span class="ff0000">*</span>线路主题：</td>
              <td colspan="3" align="left" bgcolor="#FBFFFB"  style="border-top:2px dashed #DBEFDB;"><%=strRouteTheme%>&nbsp;&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_AddQuickTour_chkRouteTopic"></span></td>
            </tr>

            <tr>
              <td align="right" bgcolor="#FBFFFB"><span class="ff0000">*</span>出港城市：</td>
              <td colspan="3" align="left" bgcolor="#FBFFFB"><span id="divLeaveCity"><%=strLeaveCity%></span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_AddQuickTour_radPortCity"></span></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#FBFFFB"><span class="ff0000">*</span>销售区域：</td>

              <td colspan="3" align="left" bgcolor="#FBFFFB"><%=strSaleCity %>&nbsp;&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_AddQuickTour_chkSaleCity"></span></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#FBFFFB"><span class="ff0000">*</span>出团日期：</td>
              <td colspan="3" align="left" bgcolor="#FBFFFB"><span id="spanSelectDate" runat="server"><a href="javascript:void(0)" onclick="TourModule.OpenDateDialog('<%=tblID %>');return false;"><img src="<%=ImageServerPath %>/images/index_22.gif" width="21" height="14" /><span style="font-size:14px; font-weight:bold">请选择时间</span></a></span>  <span id="spanAlreadyDate" runat="server">（已生成<span style="font-size:16px; color:#ff6600;" id="spanChildCount"><strong><asp:Literal id="AddQuickTour_ChildTourCount" text="0" runat="server"></asp:Literal></strong></span>个团队）</span><asp:Label id="lblEditText" runat="server"></asp:Label> <span id="spanSetTourNo" runat="server"><img src="<%=ImageServerPath %>/images/jiben5.gif" width="8" height="12" /><a href="javascript:void(0);" onclick="TourModule.OpenDialog('设置团号规则','/RouteAgency/SetTourNo.aspx?rnd='+ Math.random(),500,400);return false;"><span class="huise">设置团号规则</span></a></span><input type="hidden" id="hidTourLeaveDate" name="hidTourLeaveDate" runat="server" valid="required" errmsg="请选择出团日期!" />&nbsp;&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_hidTourLeaveDate"></span><input type="hidden" id="AddQuickTour_hidChildLeaveDateList" name="AddQuickTour_hidChildLeaveDateList" runat="server" /><input type="hidden" id="AddQuickTour_hidChildTourCodeList" name="AddQuickTour_hidChildTourCodeList" runat="server" /></td>
          
            </tr>
          </table>
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:10px;">
            <tr>
              <td width="2%">&nbsp;</td>
              <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben2.gif" width="131" height="37" /></td>
            </tr>
          </table>
         <cc1:TourPriceStand ID="AddQuickTour_tourpricestand" runat="server" ReleaseType="AddQuickTour" ModuleType="tour" />
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:18px;">
            <tr>
              <td width="2%">&nbsp;</td>
              <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben3.gif" width="149" height="34" /></td>
            </tr>
          </table>          
          <table width="100%" border="0" cellspacing="1" cellpadding="1">
            <tr><td width="10">&nbsp;</td>
              <td align="left">
              <textarea id="<%=FCKID %>" name="AddQuickTour_divFCK" style="height:350px; width:750px; border:1px solid #ECECEC; background-color:White;">点击添加行程信息</textarea>
              </td>
            </tr>
          </table>
          <table width="100%" height="60" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td width="24%">自动停收设置：
                <input name="AddQuickTour_AutoOffDays" id="AddQuickTour_AutoOffDays" runat="server" type="text" class="shurukuang" size="4" value="1" /> 天</td>
              <td width="27%" align="center"><img id="AddQuickTour_btnSubmit" onclick="TourModule.SubmitCheck(false,'/routeagency/addquicktour.aspx','未出发团队','<%=templateTourId.Trim()!=""?"/routeagency/NotStartingTeamsDetail.aspx?TemplateTourID="+templateTourId:"/routeagency/notstartingteams.aspx" %>','<%=tblID %>','<%=MaxTourDays %>');return false;" src="<%=ImageServerPath %>/images/fabuan.gif" width="151" height="42" style="cursor:pointer;" /></td>
              <td width="49%" align="left"><a href="JavaScript:void(0);"class="xiayiyec" id="AddQuickTour_btnAdd" onclick="TourModule.SubmitCheck(true,'/routeAgency/addquicktour.aspx','','/routeAgency/addquicktour.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;">保存并继续发布</a>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanSubmit1" style="margin-top:10px;float:left;margin-left:10px; display:none; color:Red;">正在提交数据...</span> </td>
            </tr>
          </table><textarea id="hidFCKVal" style="display:none;"><%=QuickPlan %></textarea></td>
      </tr>
    </table>
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("CommonTour") %>"  cache="true"></script>
    <script type="text/javascript">
        $("#<%=tblID %>").find("input[type=text][id$=TourDays]").attr("errmsg","请填写天数!|天数只能是数字!|天数不能大于<%=MaxTourDays %>天!");
        CheckFormIsChange.recodeInitialDataForm($("#<%=tblID %>").closest("form").get(0)); 
        
        FV_onBlur.initValid($("#<%=tblID %>").closest("form").get(0),null,false);
        
        $("#<%=tblID %>").find("input[type=text]").eq(0).focus();
        
	    function CheckMaxDay(e,formElements){
		    if(parseInt(e.value) <= parseInt('<%=MaxTourDays %>'))
		    {
		        return true;
		    }else{
		        return false;
		    }
	    }
        
        function KEInit(){
            KE.init({
                id : '<%=FCKID %>',//编辑器对应文本框id
                width : '750px',
                height : '350px',
                skinsPath:'/kindeditor/skins/',
                pluginsPath:'/kindeditor/plugins/',
                scriptPath:'/kindeditor/skins/',
                
                resizeMode : 0,//宽高不可变
                items:keMore //功能模式(keMore:多功能,keSimple:简易)
              
            });
            
        }  
          

        
        $("#<%=tblID %>").find("input[type=text][id$=TourDays]").blur(function(){TourModule.TourDaysFocus('<%=tblID %>','<%=MaxTourDays %>');});

        if($.trim($("#<%=tblID %>").find("textarea[id=hidFCKVal]").html()) == '' || $.trim($("#<%=tblID %>").find("textarea[id=hidFCKVal]").html()) == null || $.trim($("#<%=tblID %>").find("textarea[id=hidFCKVal]").html()) == undefined)
        {
            $("#<%=tblID %>").find("textarea[id=<%=FCKID %>]").bind("click",function(){
                KEInit();
                setTimeout(
                function(){
                    KE.create('<%=FCKID %>',0);
                    KE.html('<%=FCKID %>','');
                },200);
            });
        }else{
            KEInit();
            setTimeout(
                function(){
                  KE.create('<%=FCKID %>',0);//创建编辑器
                  KE.html('<%=FCKID %>',htmlDecode($("#<%=tblID %>").find("textarea[id=hidFCKVal]").html())) //赋值
                },200);
        }        
        
         if(($("#<%=tblID %>").find("input[type=hidden][id$=hidTourID]").val() != '' && $("#<%=tblID %>").find("input[type=hidden][id$=hidTourID]").val() != null && $("#<%=tblID %>").find("input[type=hidden][id$=hidTourID]").val() != undefined) || ($("#<%=tblID %>").find("input[type=hidden][id$=TemplateTourID]").val() != '' && $("#<%=tblID %>").find("input[type=hidden][id$=TemplateTourID]").val() != null && $("#<%=tblID %>").find("input[type=hidden][id$=TemplateTourID]").val() != undefined))
        {
            $("#<%=tblID %>").find("img[id$=btnSubmit]").attr("src","<%=ImageServerPath %>/images/save(1).gif");
        }
    </script>
</asp:content>
