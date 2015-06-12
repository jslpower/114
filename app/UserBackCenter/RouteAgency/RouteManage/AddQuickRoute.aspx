<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuickRoute.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.RouteManage.AddQuickRoute" %>

<asp:content id="AddQuickRoute" contentplaceholderid="ContentPlaceHolder1" runat="server">
<%@ Register Src="~/usercontrol/RouteAgency/TourPriceStand.ascx" TagName="TourPriceStand"
    TagPrefix="cc1" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
    <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="true"></script>
<script type="text/javascript">
commonTourModuleData.add({
    ContainerID:'<%=tblID %>',
    ReleaseType:'AddQuickRoute',
    FCKID:'<%=FCKID %>'
});
</script>
<table id="<%=tblID %>" width="99%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td valign="top"><table width="100%" height="33" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 15px;">&nbsp;</td>
            <td  class="tianon" id="TabAddQuickRoute" runat="server">快速发布</td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
            <td class="tianup" id="TabAddStandardRoute" runat="server"><a id="AddStandardRouteLink" href="/routeagency/routemanage/addstandardroute.aspx" onclick="return TourModule.CheckForm(this,'<%=tblID %>');">标准版发布</a></td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217);">&nbsp;</td>
          </tr>
        </table>
<input type="hidden" id="AddQuickRoute_hidRouteID" name="AddQuickRoute_hidRouteID" runat="server" />
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:10px;">
            <tr>
              <td width="2%">&nbsp;</td>
              <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben11.gif" width="128" height="34" /></td>
            </tr>
          </table>
          <table border="0" align="center" cellpadding="4" cellspacing="1" style="border:1px solid #ECECEC; background:#FAFAFA; margin:8px 0px 10px 0px; width:99%;">
            <tr align="left">
              <td width="11%" align="right"><span class="ff0000">*</span>线路区域：</td>

              <td width="80%" align="left"><select name="AddQuickRoute_RouteArea" id="AddQuickRoute_RouteArea" runat="server" valid="required" errmsg="请选择线路区域!" style="height:20px;">
                <option value="">请选择</option>
              </select>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddQuickRoute_RouteArea.ClientID %>" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right"><span class="ff0000">*</span>线路名称：</td>
              <td colspan="3" align="left"><input name="AddQuickRoute_txtRouteName" id="AddQuickRoute_RouteName" runat="server" type="text" class="bitian" size="50" valid="required|limit" max="100" errmsg="请填写线路名称!|线路名称不能超过100个字!" />&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=AddQuickRoute_RouteName.ClientID %>" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right"><span class="ff0000">*</span>天数：</td>
              <td width="12%" align="left"><input name="AddQuickRoute_TourDays" id="AddQuickRoute_TourDays" runat="server" type="text" class="bitian" size="5" valid="required|RegInteger|custom" custom="CheckMaxDay" /><img src="<%=ImageServerPath %>/images/tian.gif" width="32" height="22" class="shijianbiao" onclick="TourModule.addTourDays('<%=tblID %>','<%=MaxTourDays %>');" style="cursor:pointer;"/>&nbsp;<span id="errMsg_<%=AddQuickRoute_TourDays.ClientID %>" class="errmsg"></span></td>
            </tr>
            
            <tr>
              <td align="right" bgcolor="#FBFFFB" style="border-top:2px dashed #DBEFDB;"><span class="ff0000">*</span>线路主题：</td>
              <td align="left" bgcolor="#FBFFFB"  style="border-top:2px dashed #DBEFDB;"><%=strRouteTheme%>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_AddQuickRoute_chkRouteTopic" class="errmsg"></span></td>
            </tr>

            <tr>
              <td align="right" bgcolor="#FBFFFB"><span class="ff0000">*</span>出港城市：</td>
              <td align="left" bgcolor="#FBFFFB"><span id="divLeaveCity"><%=strLeaveCity%></span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_AddQuickRoute_radPortCity" class="errmsg"></span></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#FBFFFB"><span class="ff0000">*</span>销售区域：</td>

              <td align="left" bgcolor="#FBFFFB"><%=strSaleCity %>&nbsp;&nbsp;&nbsp;&nbsp;<span id="errMsg_AddQuickRoute_chkSaleCity" class="errmsg"></span></td>
            </tr>
          </table>
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:10px;">
            <tr>
              <td width="2%">&nbsp;</td>
              <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben21.gif" width="131" height="37" /></td>
            </tr>
          </table>
         <cc1:TourPriceStand ID="AddQuickRoute_tourpricestand" runat="server" ReleaseType="AddQuickRoute" ModuleType="route" />
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:18px;">
            <tr>
              <td width="2%">&nbsp;</td>
              <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben3.gif" width="149" height="34" /></td>
            </tr>
          </table>          
          <table width="100%" border="0" cellspacing="1" cellpadding="1">
            <tr><td width="10">&nbsp;</td>
              <td align="left" id="td_AddFCK" runat="server">
               <textarea id="<%=FCKID %>" name="AddQuickRoute_divFCK" style="height:350px; width:750px; border:1px solid #ECECEC; background-color:White;">点击添加行程信息</textarea>
              </td>
            </tr>
          </table>
          <table width="100%" height="60" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td width="27%" align="center"><img id="AddQuickRoute_btnSubmit" onclick="TourModule.SubmitCheck(false,'/routeagency/routemanage/addquickroute.aspx','线路维护','/routeagency/routemanage/routeview.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;" style="cursor:pointer;" src="<%=ImageServerPath %>/images/fabuan.gif" width="151" height="42" /></td>
              <td width="49%" align="left"><a href="JavaScript:void(0);" class="xiayiyec" id="AddQuickRoute_btnAdd" onclick="TourModule.SubmitCheck(true,'/routeagency/routemanage/addquickroute.aspx','','/routeagency/routemanage/addquickroute.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;">保存并继续发布</a>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanSubmit1" style="margin-top:10px;float:left;margin-left:10px; display:none; color:Red;">正在提交数据...</span> </td>
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
    if($("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != '' && $("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != null && $("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != undefined)
    {
        $("#<%=tblID %>").find("img[id$=btnSubmit]").attr("src","<%=ImageServerPath %>/images/save(1).gif");
    }
    </script>
</asp:content>
