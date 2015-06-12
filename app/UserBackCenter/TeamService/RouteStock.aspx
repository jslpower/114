<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteStock.aspx.cs" Inherits="UserBackCenter.TeamService.RouteStock" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content id="DirectorySet" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">



	<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td valign="top" >
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="zttoolbar">
          <tr ><td><div class="zttooltitle" style=" padding-top:5px;">
            我的产品目录 <img src="<%=ImageServerUrl %>/images/auto_lmy_033.gif"  width="14" height="15" border="0" /></div>
            <div class="zttooltitleun" style=" padding-top:5px; margin-right:30px;"><a href="<%=EyouSoft.Common.Domain.UserPublicCenter%>/RouteManage/Default.aspx" target="_blank">平台所有产品</a></div>
          <div style="float:left;padding-top:5px;">
            <select id="rs_selCity" runat="server" onchange="return RouteStock.routeFiter(this,true)" runat="server">
            </select>
            <a href="javascript:void(0);" <%=allStyle%> onclick="return RouteStock.routeFiter('3',false)">全部</a>(<%=allRoute%>) | <a href="javascript:void(0);" <%=longStyle %> onclick="return RouteStock.routeFiter('0',false)">国内长线</a>(<%=longRoute%>) | <a href="javascript:void(0);" <%=shortStyle %> onclick="return RouteStock.routeFiter('2',false)">周边游</a>(<%=shortRoute%>) | <a href="javascript:void(0);" <%=exitStyle %> onclick="return RouteStock.routeFiter('1',false)">出境线路</a>(<%=exitRoute%>) </div></td>
            
            <td width="6" align="right" style="background:url(<%=ImageServerUrl %>/images/zxtoolright.gif); width:4px;"></td>
          </tr>
          <tr style="height:18px;">
            <td colspan="2">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ztlistsearch">
              <tr>
                <td width="4%" align="center" style="height:18px;"><img src="<%=ImageServerUrl %>/images/searchico2.gif" width="23" height="24" /></td>
                <td width="96%" align="left" style="height:18px;">
                   线路区域<select  class="selzx2" id="rs_selRouteArea" runat="server" >
				           
                           </select>
				  线路名称：<input  type="text" runat="server" size="15" id="rs_txtRouteName"/>
                  专线商：<select  class="selzx" id="rs_selRouteCompany" runat="server">
                   
                  </select> 
                  出团日期：<input  runat="server" onfocus="WdatePicker()" type="text"  style="width:70px;" id="rs_txtStartTime"/> 至<input onfocus="WdatePicker()" type="text" style="width:70px;" id="rs_txtEndTime" runat="server" />
                    <input type="image" name="rs_search" onclick="return RouteStock.routeSearch();" value="提交" src="<%=ImageServerUrl %>/images/chaxun.gif" style="width:62px; height:21px; border:none; margin-bottom:-3px;" />
                  </td>
              </tr>
            </table></td>
          </tr>
        </table>
		<table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7" class="zttype">
		<asp:CustomRepeater id="rs_rpt_tourList" runat="server">
		<HeaderTemplate><tr>
            <th width="51%" align="center">线路名称</th>
            <th width="5%" align="center">天数</th>
            <th width="13%" align="center">出团日期</th>
            <th width="4%" align="center">剩余</th>
            <th width="8%" align="center">成人门市</th>
            <th width="8%" align="center">小孩门市</th>
            <th width="11%" align="center">操作</th>
          </tr>
        </HeaderTemplate>
         <ItemTemplate>
             <tr onMouseOver="RouteStock.mouseovertr(this)" onMouseOut="RouteStock.mouseouttr(this)">
            <td align="left" class="tbline"><%# Eval("TourSpreadStateName")%><a href='/PrintPage/TeamInformationPrintPage.aspx?TourID=<%# Eval("ID") %>&TemplateTourID=<%# Eval("ParentTourID") %>' target="_blank"><%# Eval("RouteName")%></a></td>
            <td align="center"><%# Eval("TourDays")%></td>
            <td align="center"><%# Convert.ToDateTime(Eval("LeaveDate")).ToString("yyyy-MM-dd")%>(<%#EyouSoft.Common.Utils.ConvertWeekDayToChinese(Convert.ToDateTime(Eval("LeaveDate"))).Substring(1, 1)%>)<br/>
              <a href="javascript:void(0)" onclick="RouteStock.ClickCalendar('<%#Eval("ParentTourID") %>',this,'<%# Convert.ToInt32(Eval("AreaType"))%>');return false">其它<%# Eval("RecentLeaveCount")%>个班次>></a></td>
            <td align="center"><%# Eval("RemnantNumber")%></td>
            <td align="center"><%#String.Format("{0:f}", Eval("RetailAdultPrice"))%></td>
            <td align="center"><%#String.Format("{0:f}", Eval("RetailChildrenPrice"))%></td>
            <td align="center"><a href="javascript:void(0);" onclick="return RouteStock.OpenDialog('线路预定','/TeamService/RouteOrder.aspx?tourid=<%# Eval("ID") %>','800px','500px');" class="goumai" title="点击预订">预 订</a>
            <%# Utils.GetMQ(Eval("TourContacMQ").ToString())%></td>
          </tr>
         </ItemTemplate>
      
		</asp:CustomRepeater>
		
       
         
        </table>
   <table id="rs_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
          <cc2:ExportPageInfo ID="rs_ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
        </td>
    </tr>
</table>
   </td>
      </tr></table>
   <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("MouseFollow") %>" cache="true"></script>
        <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("TourCalendar") %>" cache="true"></script>
        
        <script type="text/javascript">
 $(document).ready(function()
 {
     $("#rs_ExportPage").find("a").click(function(){
          topTab.url(topTab.activeTabIndex,$(this).attr("href"));
          return false;
      });
 });
 
  //切换行背景色
    var RouteStock=
    {  
        
        mouseovertr:function(o){
            o.style.backgroundColor="#FFF9E7";
          },
        mouseouttr:function(o){
            o.style.backgroundColor="";
        },
    //预定按钮调用的方法 模板团ID，点击对象
        ClickCalendar:function(TourId,obj,areaType) {
                SingleCalendar.config.isLogin = "True"; //是否登陆
                SingleCalendar.config.stringPort ="<%=EyouSoft.Common.Domain.UserPublicCenter %>";//配置
                SingleCalendar.initCalendar({
                    currentDate:<%=thisDate %>,//当时月
                    firstMonthDate: <%=thisDate %>,//当时月
                    srcElement: obj,
                    areatype:areaType,//当前模板团线路区域类型 
                    TourId:TourId,//模板团ID
                    AddOrder:function(tourid){
                        RouteStock.OpenDialog('线路预定','/TeamService/RouteOrder.aspx?tourid='+tourid,'800px','500');
                        return false;
                    }
                });
            } ,
      refresh:function(){
           topTab.url(topTab.activeTabIndex,"/TeamService/RouteStock.aspx");
      },
    //查询
    routeSearch:function(){
         var routeArea=$("#<%=rs_selRouteArea.ClientID %>").val();
         var routeName=$("#<%=rs_txtRouteName.ClientID %>").val();
         var companyId=$("#<%=rs_selRouteCompany.ClientID %>").val();
         var startTime=$("#<%=rs_txtStartTime.ClientID %>").val();
         var endTime=$("#<%=rs_txtEndTime.ClientID %>").val(); 
         topTab.url(topTab.activeTabIndex,"/TeamService/RouteStock.aspx?method=search&routearea="+encodeURIComponent(routeArea)+"&routename="+encodeURIComponent(routeName)+"&companyid="+encodeURIComponent(companyId)+"&starttime="+encodeURIComponent(startTime)+"&endtime="+encodeURIComponent(endTime));
         return false;
    }, 
    //筛选
    routeFiter:function(filter,isCity){
           if(isCity)
               topTab.url(topTab.activeTabIndex,"/TeamService/RouteStock.aspx?method=cityfilt&cityid="+filter.value);
           else
               topTab.url(topTab.activeTabIndex,"/TeamService/RouteStock.aspx?method=areatype&areatype="+filter);
           return false;
    },
     //弹出窗体
    OpenDialog:function(title,url,width,height){
    var height1=GetAddOrderHeight();
        Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height1,draggable:true,data:null});
          return false;
    },
    toDirectSet:function(){
        $("#divDirectoryset").find("a").click();
        return false;
    }
    
  }
</script>
        
</asp:Content>

