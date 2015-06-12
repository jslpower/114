<%@ Page Language="C#" MasterPageFile="~/master/GeneralShop.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SeniorOnlineShop.shop.Default" Title="无标题页" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Src="../GeneralShop/GeneralShopControl/SecondMenu.ascx" TagName="SecondMenu"
    TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("head2011") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("xinmian") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("news2011") %>" rel="Stylesheet"
        type="text/css" />
    <div class="xinmain">
        <uc1:SecondMenu ID="SecondMenu1" runat="server" />
        <div class="main_center2">
            <div class="gscp">
                <ul>
                    <li class="gs_title"><span class="gs_xianlu">线路</span> <span class="gs_banci">班次</span>
                        <span class="gs_chengren">成人价</span> <span class="gs_ertong">儿童价</span> <span class="gs_caozuo">
                            操作</span> </li>
                    <asp:Repeater ID="RptProductList" runat="server">
                        <ItemTemplate>
                            <li class="" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)"><span
                                class="gs_xianlu2"><%# GetRecommendTypeImgSrc(Eval("RecommendType")) %><a href='<%#EyouSoft.Common.Domain.UserPublicCenter + "/tour_" + Eval("RouteId").ToString()+"_"+CityId%>'><%#Eval("RouteName") %><%= EyouSoft.Common.Utils.GetMQ(GetMq(CompanyId))%></a></span>
                                <span class="gs_banci2"><a href="javascript:void(0)" onclick="ClickCalendar('<%#Eval("RouteId") %>',this, '<%#GetTourId(Eval("RouteId")) %>');return false;">
                                    <%#string.IsNullOrEmpty(Eval("TeamPlanDes").ToString())?"暂无班次":Eval("TeamPlanDes")%></a>
                                </span><span class="gs_chengren2"><span class="fd7b19">
                                    <%# GetTourPrice(Eval("TeamPlanDes"), Eval("RetailAdultPrice"), Eval("IndependentGroupPrice"))%></span></span>
                                <span class="gs_ertong2"><span class="fd7b19">
                                    <%# GetTourPrice(Eval("TeamPlanDes"), Eval("RetailChildrenPrice"), Eval("IndependentGroupPrice"))%></span></span>
                                <span class="gs_caozuo2">
                                    <%#GetLinkByRoute(Eval("RouteId"), Eval("TourId"), Eval("RouteSource"), Eval("TeamPlanDes"))%>
                                </span></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="fangye">
                <cc2:ExporPageInfoSelect ID="PageInfoSelect1" runat="server" PageStyleType="NewButton" />
            </div>
        </div>
        <div style="clear: both; height: 10px;">
        </div>
    </div>
    <%--浮动咨询开始--%>
    <div id="divZX" style="display: none; z-index: 99999;">
        <table height="140" cellspacing="0" cellpadding="0" border="0" background="<%= Domain.ServerComponents %>/images/seniorshop/zixunbg.gif"
            width="400">
            <tbody>
                <tr>
                    <td height="5" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left" valign="top" colspan="2">
                        &nbsp;&nbsp;您好，<asp:Label runat="server" ID="lbCompanyName"></asp:Label>竭诚为您服务
                    </td>
                </tr>
                <tr>
                    <td valign="middle" colspan="2">
                        <asp:Label runat="server" Text="欢迎您,有什么可以帮助您的吗？" ID="lbGuestInfo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <a href="/OlServer/Default.aspx?cid=<%= Master.CompanyId %>" target="blank">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/jieshou.gif"></a>
                    </td>
                    <td align="left">
                        <a href="javascript:;" onclick="CloseLeft();">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/hulue.gif"></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <%--浮动咨询结束--%>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery.floating.js") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("groupdate") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("TourCalendar") %>"></script>

    <script type="text/javascript">
    //预定按钮调用的方法 线路编号，点击对象
        function ClickCalendar(RouteId,obj,Tourid) {
            SingleCalendar.config.isLogin = "<%=IsLogin %>"; //是否登陆
            SingleCalendar.config.stringPort ="<%=EyouSoft.Common.Domain.UserPublicCenter %>";//配置
            SingleCalendar.initCalendar({
                currentDate:<%=thisDate %>,//当时月
                firstMonthDate: <%=thisDate %>,//当时月
                srcElement: obj,
                //areatype:AreaType,//当前模板团线路区域类型
                TourId:RouteId,//线路编号
                AddOrder:AddOrder
            });
        }
         function AddOrder(TourId) {
         var Url="";
            if ("<%=IsLogin %>" == "True") {
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
                window.location.href = "<%=EyouSoft.Common.Domain.UserPublicCenter %>/Register/Login.aspx?isShow=1&CityId=<%=CityId %>&returnurl="+returnUrl;
            }
        }
      function mouseovertr(o) {
            o.style.backgroundColor = "#F1EDF4";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    $(function(){
         $("#divZX").easydrag();
            $("#divZX").floating({ position: "left", top: 100, left: 10, width: 400 });
});
    </script>

</asp:Content>
