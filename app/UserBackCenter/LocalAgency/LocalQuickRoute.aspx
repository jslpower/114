<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalQuickRoute.aspx.cs"
    Inherits="UserBackCenter.LocalAgency.LocalQuickRoute" %>

<asp:content id="LocalQuickRoute" contentplaceholderid="ContentPlaceHolder1" runat="server">
<%@ Register Src="~/usercontrol/RouteAgency/TourPriceStand.ascx" TagName="TourPriceStand"
    TagPrefix="cc1" %>

    <script type="text/javascript" src="/kindeditor/kindeditor.js" cache="true"></script>
<script type="text/javascript">
commonTourModuleData.add({
    ContainerID:'<%=tblID %>',
    ReleaseType:'LocalQuickRoute',
    FCKID:'<%=FCKID %>'
});
</script>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablewidth" id="<%=tblID %>">
      <tr><input type="hidden" id="LocalQuickRoute_hidRouteID" name="LocalQuickRoute_hidRouteID" runat="server" />
        <td valign="top">
        <table width="100%" height="33" border="0" cellpadding="0" cellspacing="0">

          <tr>
            <td style="border-bottom:1px solid #ABC9D9; width:15px;">&nbsp;</td>
            <td  class="tianon" id="TabLocalQuickRoute" runat="server">快速发布</td>
            <td style="border-bottom:1px solid #ABC9D9; width:10px; ">&nbsp;</td>
            <td class="tianup" id="TabLocalStandardRoute" runat="server"><a id="LocalStandardRouteLink" href="/localagency/localstandardroute.aspx" onclick="return TourModule.CheckForm(this,'<%=tblID %>');">标准发布</a></td>
            <td style="border-bottom:1px solid #ABC9D9;">&nbsp;</td>
          </tr>
        </table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:10px;">
          <tr>
            <td width="2%">&nbsp;</td>
            <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben11.gif" width="128" height="34" /></td>
          </tr>
        </table>
          <table width="100%" border="0" cellspacing="2" cellpadding="2" style="border:1px solid #ECECEC; background:#FAFAFA; margin:8px 0px 10px 0px; ">
            
            <tr>
              <td width="13%" align="right"><span class="ff0000">*</span>线路名称：</td>
              <td align="left"><input name="LocalQuickRoute_RouteName" id="LocalQuickRoute_RouteName" runat="server" type="text" class="bitian" size="50" valid="required|limit" max="100" errmsg="请填写线路名称!|线路名称不能超过100个字!" />&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_<%=LocalQuickRoute_RouteName.ClientID %>"></span></td>
            </tr>
            <tr>
              <td align="right"><span class="ff0000">*</span>天数：</td>

              <td align="left"><input name="LocalQuickRoute_TourDays" id="LocalQuickRoute_TourDays" runat="server" type="text" class="bitian" size="5" valid="required|RegInteger|custom" custom="CheckMaxDay" /><img src="<%=ImageServerPath %>/images/tian.gif" width="32" height="22" class="shijianbiao" onclick="TourModule.addTourDays('<%=tblID %>','<%=MaxTourDays %>');" style="cursor:pointer;" />&nbsp;&nbsp;&nbsp;<span id="errMsg_<%=LocalQuickRoute_TourDays.ClientID %>" class="errmsg"></span></td>
            </tr>
          </table>
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:10px;">
            <tr>
              <td width="2%">&nbsp;</td>
              <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben21.gif" width="131" height="37" /></td>
            </tr>
          </table>
            <cc1:TourPriceStand ID="LocalQuickRoute_tourpricestand" runat="server" ReleaseType="LocalQuickRoute" ModuleType="route" />
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:18px;">
            <tr>
              <td width="2%">&nbsp;</td>
              <td width="98%" align="left"><img src="<%=ImageServerPath %>/images/jiben3.gif" width="149" height="34" /></td>
            </tr>
          </table>
          <table width="100%" border="0" cellspacing="1" cellpadding="1">
            <tr><td width="10">&nbsp;</td>

              <td align="left">
              <textarea id="<%=FCKID %>" name="LocalQuickRoute_divFCK" style="height:350px; width:750px; border:1px solid #ECECEC; background-color:White;">点击添加行程信息</textarea></td>
            </tr>
          </table>
          <table width="50%" height="30"  border="0" align="center" cellpadding="0" cellspacing="0" style="margin-left:150px;">
            <tr>
              <td width="23%" align="center"><img id="LocalQuickRoute_btnSubmit" onclick="TourModule.SubmitCheck(false,'/localagency/localquickroute.aspx','线路列表','/localagency/localrouteview.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;" style="cursor:pointer;" src="<%=ImageServerPath %>/images/fabuan.gif" width="151" height="42" /></td>
              <td width="49%" align="left"><a href="JavaScript:void(0);" class="xiayiyec" id="LocalQuickRoute_btnAdd" onclick="TourModule.SubmitCheck(true,'/localagency/localquickroute.aspx','线路列表','/localagency/localquickroute.aspx','<%=tblID %>','<%=MaxTourDays %>');return false;">保存并继续发布</a>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanSubmit1" style="margin-top:10px;float:left;margin-left:10px; display:none; color:Red;">正在提交数据...</span> </td>
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

        $("#<%=tblID %>").find("input[type=text][name$=TourDays]").blur(function(){TourModule.TourDaysFocus('<%=tblID %>','<%=MaxTourDays %>');});
                   
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
        function FCKeditor_OnComplete(editorInstance){
            editorInstance.SetHTML($.trim($("#<%=tblID %>").find("div[id=divFCKHTML]").html()));
        }
        
        if($("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != '' && $("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != null && $("#<%=tblID %>").find("input[type=hidden][id$=hidRouteID]").val() != undefined)
    {
        $("#<%=tblID %>").find("img[id$=btnSubmit]").attr("src","<%=ImageServerPath %>/images/save(1).gif");
    }
    </script>
</asp:content>
