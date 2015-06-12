<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oldTourList.aspx.cs" MasterPageFile="~/MasterPage/NewPublicCenter.Master"
    Inherits="UserPublicCenter.TourManage.oldTourList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPageByBtn" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>

<%@ Register Src="../WebControl/InfomationControl/InfoFoot.ascx" TagName="FootMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/UCLineRight.ascx" TagName="UCLineRight" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/TourSearchKeys.ascx" TagName="TourSearchKeys" TagPrefix="uc3" %>
<%@ Register Src="../WebControl/RouteList.ascx" TagName="RouteList" TagPrefix="uc4" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRouteRecommend" TagPrefix="uc5" %>
<%@ Register Src="../WebControl/InfomationControl/AllCountryTourInfo.ascx" TagName="AllCountryMenu" TagPrefix="uc6" %>

<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<%@ Import Namespace="EyouSoft.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("news2011") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("gongqiu2011") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("head2011") %>" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">
    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF6C7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>
    <div class="body" style=" overflow:hidden; margin-top:10px;">
        <div id="news-list-left">
        <!--列表 start-->
        <div class="box">
              <div class="box-c box-c-r">
                <h3>
                    <a href="<%=SubStation.CityUrlRewrite(CityId) %>" class="returnHome">返回首页&gt;</a>
                    <%=strRourListName %>
                    <div id="xianluList">
                        <a href="#" id="aCategory" onmousemove="showFloatCategory('popDiv')" onmouseout="hideFloatCategory('popDiv')">切换专线</a> 
                        <div id="popDiv" onmousemove="showFloatCategory('popDiv')" onmouseout="hideFloatCategory('popDiv')">
                             <uc4:RouteList ID="RouteList1" runat="server" />
                        </div>
                    </div>
                </h3>                
            </div>
          <div class="box-main">
            <div class="box-content box-content-main-xianlu">
                <ul class="xianluul">
                 <uc3:TourSearchKeys ID="TourSearchKeys1" runat="server" />
                 <li class="xianimg">
                    <ul>
                       <%=FiveCompany %>
                    </ul>
                 </li>
                </ul>
            </div>
          </div>
           <!--tabs切换导航 开始-->
        <div class="hr-10"></div>
        <div id="listContent">
            <div id="tags">
                <ul class="tagchg">
                  <li class="selectTag"><a href="/TourManage/oldTourList.aspx?CityId=<%=CityId %>&TourAreaId=<%=TourAreaId %>">所有线路</a></li>
                </ul>
            </div>
            <div id="tagContent" class="tagContentBoder">
              <div class="tagContent selectTag" id="tagContent1">
                 <ul class="xianlulistul">
                   <li class="title">
                     <div class="title1 title2"><b>团队基本信息</b></div>
                     <div class="publish publish2"><b>成人价/儿童价</b></div>
                   </li>
                   <!--信息列表循环输出() start-->
                   
                   <cc1:CustomRepeater ID="repTourList" runat="server">
                        <ItemTemplate>
                            <li onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                               <div class="title1">
                                   <h3><a href="<%#EyouSoft.Common.URLREWRITE.Tour.GetTourPrintUrl(Eval("ID").ToString())%>" target="_blank" class="lan14">
                                            <%# Eval("RouteName")%>（<%#Eval("TourDays")%>天）</a></h3>
                                    <%# GetCompanyInfo(Eval("CompanyID").ToString(), Eval("CompanyName").ToString())%>
                                   <p>最近一班：<span><%# GetLeaveInfo(Convert.ToDateTime(Eval("LeaveDate").ToString()))%></span>/
                                        <b>计划:<%# Eval("PlanPeopleCount")%>&nbsp;&nbsp;剩余:<%# Eval("RemnantNumber")%></b>
                                        <a href="javascript:void(0);" onclick="ClickCalendar('<%#Eval("ParentTourID") %>',this, <%# Convert.ToInt32(Eval("AreaType")) %>);return false;">
                                            <span class="huise">其它<%#Eval("RecentLeaveCount")%>个发团日期>></span></a> 
                                   </p>
                               </div>
                               <div class="price">
                                   <p class="state"><%# TourSpreadState(Eval("TourState").ToString(), Eval("TourSpreadStateName").ToString())%></p>
                                   <%# 
                                        "<p>门市价：<span class='br'>￥" + Convert.ToDecimal(Eval("RetailAdultPrice").ToString()).ToString("F0") + "</span>/<span class='bb'>" + Convert.ToDecimal(Eval("RetailChildrenPrice").ToString()).ToString("F0") + "元起</span></p>"
                                    %>
                                </div>
                                <div class="publish">
                                    <a href="<%#EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(Eval("ID").ToString(),CityId )%>?ReturnUrl=<%=ReturnUrl %>"
                                            target="_blank"><img src='<%=ImageServerUrl +"/images/new2011/xianlu/png_bg_08.png" %>' alt="预定"/></a><br />
                                    <%# EyouSoft.Common.Utils.GetMQ(Eval("TourContacMQ").ToString())%>
                                </div>
                                <div style="clear:both;height:0;overflow:hidden;margin:0;padding:0;width:100%"></div>
                             </li>
                        </ItemTemplate>
                    </cc1:CustomRepeater>
                  <!--信息列表循环输出 end-->
                 </ul>
                      <!--分页 开始-->
                  <div class="digg">
                    <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton" />
                  </div>
                 <!--分页 结束-->
              </div>
            </div>
        </div>
      </div>
        <!--列表 end-->
      </div>
        <!--右边 开始-->
      <uc2:UCLineRight ID="UCLineRight" runat="server" />
        <!--右边 结束-->
       <div class="hr-10"></div>
       <uc1:FootMenu ID="footMenu" runat="server" />
      <div class="hr-10"></div>
  <!--列表 start-->
      <uc6:AllCountryMenu ID="allCountryMenu" runat="server" />
  <!--列表 end-->
</div>
    <input id="hidRourName" type="hidden" runat="server" />
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("groupdate") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("TourCalendar") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("autocomplete") %>"></script>

    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("517autocomplete") %>" />

    <script type="text/javascript">
        var iFloat=null;
        function showFloatCategory(str) {
	        document.getElementById(str).style.display="block";
	        if(iFloat!=null)
		        clearInterval(iFloat);
        }
        function hideFloatCategory(str)
        {
	        obj = document.getElementById(str);
	        if(obj!=null)
		        iFloat = setInterval('document.getElementById("'+str+'").style.display="none"',300);
        }
 
        //预定按钮调用的方法 模板团ID，点击对象
        function ClickCalendar(TourId,obj,AreaType) {
            SingleCalendar.config.isLogin = "<%=IsLogin %>"; //是否登陆
            SingleCalendar.config.stringPort ="";//配置
            SingleCalendar.initCalendar({
                currentDate:<%=thisDate %>,//当时月
                firstMonthDate: <%=thisDate %>,//当时月
                srcElement: obj,
                areatype:AreaType,//当前模板团线路区域类型 
                TourId:TourId,//模板团ID
                AddOrder:AddOrder
            });
        }
         function AddOrder(TourId) {
            if ("<%=IsLogin %>" == "True") {
                var strParms = { TourId: TourId };
                Boxy.iframeDialog({ title: "预定", iframeUrl: "<%=EyouSoft.Common.Domain.UserBackCenter %>/TeamService/RouteOrder.aspx", width: "800", height:GetAddOrderHeight(), draggable: true, data: strParms });
            } else {
                var returnUrl=escape("<%=Request.ServerVariables["SCRIPT_NAME"]%>?TourId=<%=Request.QueryString["TourId"] %>&<%=Request.QueryString%>");
                //登录
                window.location.href = "/Register/Login.aspx?isShow=1&CityId=<%=CityId %>&returnurl="+returnUrl;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
    
    <uc5:HotRouteRecommend ID="HotRouteRecommend1" runat="server" />
    
</asp:Content>




