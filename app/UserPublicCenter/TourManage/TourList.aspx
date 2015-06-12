<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master"
    AutoEventWireup="true" CodeBehind="TourList.aspx.cs" Inherits="UserPublicCenter.TourManage.TourList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPageByBtn" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<%@ Register Src="../WebControl/UCLineRight.ascx" TagName="UCLineRight" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/TourSearchKeys.ascx" TagName="TourSearchKeys" TagPrefix="uc3" %>
<%@ Register Src="../WebControl/RouteList.ascx" TagName="RouteList" TagPrefix="uc4" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/UCRightList.ascx" TagName="UCRightList" TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("news2011") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("gongqiu2011") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("head2011") %>" rel="Stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">
    <form id="form1" runat="server">

    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#F1EDF4";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>

    <div id="news-list-ad">
        <a title="点击下载" href="http://im.tongye114.com/IM/DownLoad/download.aspx">
            <img alt="MQ下载" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/news/news-list_03.gif" /></a></div>
    <div class="body" style="overflow: hidden; margin-top: 10px;">
        <div id="news-list-left">
            <div class="box">
                <div class="box-c box-c-r">
                    <h3>
                        <a href="<%=SubStation.CityUrlRewrite(CityId) %>" class="returnHome">返回首页&gt;</a>
                        <%=strRourListName %>
                        <div id="xianluList">
                            <div id="tabRoute">
                                <a href="javascript:void(0)" id="aCategory">切换专线</a>
                            </div>
                            <div id="popDiv" onmousemove="showFloatCategory('popDiv')" onmouseout="hideFloatCategory('popDiv')">
                                <uc4:RouteList ID="RouteList1" runat="server" />
                            </div>
                        </div>
                    </h3>
                </div>
                <div class="box-main">
                    <div class="box-content box-content-main-xianlu">
                        <uc3:TourSearchKeys ID="TourSearchKeys1" runat="server" />
                    </div>
                </div>
                <div class="hr-10">
                </div>
                <div class="newxianlu">
                    <div class="newxianlut">
                        <div class="newxl1">
                            <img width="23" height="29" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/xianlu/xianllb_33.jpg"></div>
                        <div class="newxl2">
                            最新<%=RouteTypeName %>旅游线路</div>
                        <div class="newxl3">
                            <img width="9" height="29" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/xianlu/xianllb_36.jpg"></div>
                    </div>
                    <div class="fangye">
                        <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" PageStyleType="NewButton" />
                    </div>
                    <div class="gscp">
                        <ul>
                            <cc1:CustomRepeater ID="repTourList" runat="server">
                                <HeaderTemplate>
                                    <li class="gs_title"><span class="gs_xianlu">出发</span> <span class="gs_banci" style="text-align: left">
                                        线路</span> <span class="gs_tianshu">天数</span> <span class="gs_chengren">团队班次</span>
                                        <span class="gs_ertong">市场价</span> <span class="gs_caozuo">操作</span> </li>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li class="" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)"><span
                                        class="gs_xianlu2">
                                        <%#Eval("StartCityName")%></span> <span class="gs_banci2">
                                            <p class="xl_wenzi">
                                                <%# GetRecommendTypeImgSrc(Eval("RecommendType")) %><a href="<%#EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(string.IsNullOrEmpty(Eval("Id").ToString()) ? 0 : long.Parse(Eval("Id").ToString()), CityId )%>"><%# Eval("RouteName")%><%#Utils.GetCompanyLevImg((EyouSoft.Model.CompanyStructure.CompanyLev)(Eval("CompanyLev"))) %></a></p>
                                            <p class="xl_zxs">
                                                <strong>品牌名：</strong><a href="<%#GetCompanyShopUrl(Eval("Publishers").ToString()) %>"
                                                    target="_blank"><%# Convert.ToString(Convert.ToString(Eval("CompanyBrand")) == "" ? Eval("Introduction") : Eval("CompanyBrand")) == "" ? Utils.GetText2(Convert.ToString(Eval("PublishersName")), 6, false) : (Convert.ToString(Eval("CompanyBrand")) == "" ? Eval("Introduction") : Eval("CompanyBrand"))%></a><%# EyouSoft.Common.Utils.GetMQ(GetMq(Eval("Publishers").ToString()))%></p>
                                        </span><span class="gs_tianshu2">
                                            <%#Eval("Day")%></span> <span class="gs_chengren2"><a href="javascript:void(0)" onclick="ClickCalendar('<%#Eval("RouteId") %>',this, <%# Convert.ToInt32(Eval("RouteType")) %>,'<%#GetTourId(Eval("RouteId")) %>');return false;">
                                                <%#(string.IsNullOrEmpty(Eval("TeamPlanDes").ToString()) ? "暂无班次" : Eval("TeamPlanDes"))%></a></span>
                                        <span class="gs_ertong2"><span class="fd7b19">
                                            <%#SetTourPrice(Eval("TeamPlanDes") ,Eval("RetailAdultPrice"))%></span></span>
                                        <span class="gs_caozuo2"><a href="<%#EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(string.IsNullOrEmpty(Eval("Id").ToString()) ? 0 : long.Parse(Eval("Id").ToString()),CityId )%>">
                                            <img width="69" height="23" border="0" alt="" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/xianlu/gscp_14.jpg"></a></span>
                                    </li>
                                </ItemTemplate>
                            </cc1:CustomRepeater>
                        </ul>
                    </div>
                    <div class="fangye">
                        <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton" />
                    </div>
                </div>
            </div>
        </div>
        <uc7:UCRightList ID="UCRightList1" runat="server" />
        <div class="hr-10">
        </div>
    </div>
    <input id="hidRourName" type="hidden" runat="server" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("groupdate") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("TourCalendar") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("autocomplete") %>"></script>

    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("517autocomplete") %>" />

    <script type="text/javascript">
     $(function(){
        $("#tabRoute").mouseover(function(){
            showFloatCategory('popDiv');
        })
         $("#tabRoute").mouseout(function(){
            hideFloatCategory('popDiv');
        })
    })
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
    
        
        //预定按钮调用的方法 线路编号，点击对象
        function ClickCalendar(RouteId,obj,AreaType,Tourid) {
            SingleCalendar.config.isLogin = "<%=IsLogin %>"; //是否登陆
            SingleCalendar.config.stringPort ="";//配置
            SingleCalendar.initCalendar({
                currentDate:<%=thisDate %>,//当时月
                firstMonthDate: <%=thisDate %>,//当时月
                srcElement: obj,
                areatype:AreaType,//当前模板团线路区域类型
                TourId:RouteId,//线路编号
                AddOrder:AddOrder
            });
        }
         function AddOrder(TourId) {
         var Url="";
            if ("<%=IsLogin %>" == "True") {
//               Url="<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/RouteAgency/AddOrderByRoute.aspx?tourID="+TourId;
//                   window.location.href=Url;
                if("<%=IsTour %>"=="True")
                {
                  //组团身份
                    Url="<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/OrderByTour.aspx?tourID="+TourId;
                    window.location.href=Url;
                 }
                 else if("<%=IsRoute %>"=="True")
                 {
                   //专线身份
                   Url="<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/RouteAgency/AddOrderByRoute.aspx?tourID="+TourId;
                   window.location.href=Url;
                 }
                 else
                 {
                    Url="<%=EyouSoft.Common.Domain.UserBackCenter %>";
                    window.location.href=Url;
                 }


            } else {
                var returnUrl=escape('<%=Request.ServerVariables["SCRIPT_NAME"]%>?TourId=<%=Request.QueryString["TourId"] %>&<%=Request.QueryString%>');
                //登录
                window.location.href = "/Register/Login.aspx?isShow=1&CityId=<%=CityId %>&returnurl="+returnUrl;
            }
        }
    </script>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
</asp:Content>
