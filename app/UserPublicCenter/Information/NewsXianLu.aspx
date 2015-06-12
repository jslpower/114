<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsXianLu.aspx.cs" Inherits="UserPublicCenter.Information.NewsXianLu"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/RouteRightControl.ascx" TagName="RouteRightControl"
    TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>

<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("gouwu") %>" />
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("main") %>" />
    <uc1:CityAndMenu ID="CityAndMenu1" HeadMenuIndex="7" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-bottom: 2px solid #FF5500;">
                    <tr>
                        <td width="67%">
                            <div class="xianluon">
                                <strong><asp:Label ID="labTourAreaName" runat="server" Text="所有线路"></asp:Label></div>
                        </td>
                        <td width="33%">
                            <a href="#" class="dianright">下一页</a>
                            <div class="dianlefth">
                                上一页</div>
                            <div style="float: right; margin-right: 10px; font-size: 14px;">
                                1/100
                            </div>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" border="0" runat="server" id="NoDate" visible="false" width="100%" style="border: 1px solid rgb(216, 216, 216);">
                    <tbody>
                        <tr>
                            <td height="27">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td width="14%">
                                                <img height="58" width="54" src="<%=ImageServerPath %>/images/UserPublicCenter/paoqian.gif ">
                                            </td>
                                            <td width="86%">
                                                <div style="background: none repeat scroll 0% 0% rgb(254, 255, 237); border: 1px solid rgb(235, 229, 158);
                                                    font-size: 16px; padding: 10px; font-weight: bold; text-align: left;">
                                                    <%=strAllEmpty%></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td style="padding-top: 10px; text-align: left;">
                                                <span class="hei14"><strong>建议你：</strong></span>
                                                <ul>
                                                    <li>·适当删减或更改关键词试试</li>
                                                    <li>·看看输入的文字是否有错别字</li>
                                                    <li>·如关键词中含有城市名，去掉城市名试试</li>
                                                    <li>·换个分类搜索试试</li></ul>
                                            </td>
                                        </tr>
                                        
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="xianluhangcx"
                    style="line-height: 10px; padding: 0px; border: 1px solid #ccc; border-bottom: 0px;"
                    height="10">
                    <tr>
                        <td width="67%" align="left" style="padding-left: 65px;">
                            <strong>团队基本信息</strong>
                        </td>
                        <td width="22%" align="left" style="padding-left: 45px;">
                            <strong>成人价/儿童价</strong>
                        </td>
                        <td width="11%">
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #D8D8D8;
                    text-align: left;">
                    <asp:Repeater ID="rptXianLu" runat="server">
                        <ItemTemplate>
                            <tr bgcolor="#FFFFFF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                                <td width="489" style="border-bottom: 1px dashed #ccc; height: 85px; padding-top: 5px;">
                                    <strong>
                                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/ico.gif" width="11" height="11" />
                                        <%# TourSpreadState(Eval("TourState")!=null?Eval("TourState").ToString():"", Eval("TourSpreadStateName")!=null?Eval("TourSpreadStateName").ToString():"")%>
                                        <%--<span class="state1">推荐</span>--%>
                                        <a href="<%# EyouSoft.Common.URLREWRITE.Tour.GetXianLuUrl(int.Parse(Eval("LeaveCityId").ToString())) %>">【<%# Eval("LeaveCityName") %>出发】
                                        </a> <a href="<%#EyouSoft.Common.URLREWRITE.Tour.GetTourPrintUrl(Eval("ID").ToString())%>" target="_blank" class="lan14">
                                        <%# Eval("RouteName")%>（<%#Eval("TourDays")%>天）</a></strong><br />
                                        <%# GetCompanyInfo(Eval("CompanyID").ToString(), Eval("CompanyName").ToString())%>
                                        
                                    <br />
                                    &nbsp;&nbsp;<span class="danhui">最近一班：</span><span class="huise"><%# GetLeaveInfo(Convert.ToDateTime(Eval("LeaveDate").ToString()))%>/</span><span
                                        class="chengse"><strong>剩:<%# Eval("RemnantNumber")%></strong></span>&nbsp;&nbsp; 
                                        <a href="javascript:void(0);" onclick="ClickCalendar('<%#Eval("ParentTourID") %>',this, <%# Convert.ToInt32(Eval("AreaType")) %>);return false;">
                                            <span class="huise">
                                                <img src="<%=ImageServerPath %>/images/rili.gif" style="margin-bottom: -3px;" />其它<%#Eval("RecentLeaveCount")%>个发团日期>>
                                            </span>
                                        </a>
                                </td>
                                <td width="150" style="border-bottom: 1px dashed #ccc; line-height: 18px;">
                                    <%# IsLogin && IsCompanyCheck ? "门市价：<span class='chengse'>￥" + Convert.ToDecimal(Eval("RetailAdultPrice").ToString()).ToString("F0") + "</span>/" + Convert.ToDecimal(Eval("RetailChildrenPrice").ToString()).ToString("F0") + "元<span style='color:green'></span><br />同行价：<span class='chengse'>￥" + Convert.ToDecimal(Eval("TravelAdultPrice").ToString()).ToString("F0") + "</span>/" + Convert.ToDecimal(Eval("TravelChildrenPrice").ToString()).ToString("F0") + "元<span style='color:green'></span>" : "门市价：<span class='chengse'>￥" + Convert.ToDecimal(Eval("RetailAdultPrice").ToString()).ToString("F0") + "</span>/" + Convert.ToDecimal(Eval("RetailChildrenPrice").ToString()).ToString("F0") + "元<span style='color:green'></span>"%>
                                </td>
                                <td width="94" style="border-bottom: 1px dashed #ccc; line-height: 14px;">
                                    <div style="width: 65px; text-align: center">
                                        <a href="<%#EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(Eval("ID").ToString(),CityId )%>?ReturnUrl=<%=ReturnUrl %>"
                                        class="goumai0" target="_blank">预订</a>
                                    </div>
                                    <div style="width: 65px; text-align: center; padding-top: 3px;">
                                        <img src="<%= ImageServerPath %>/images/MQWORD.gif" width="49" height="16" />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <div class="digg">
                                <cc1:ExporPageInfoSelect ID="ExportPageInfo" runat="server" PageStyleType="NewButton" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <uc2:RouteRightControl ID="RouteRightControl1" runat="server" />
            </td>
        </tr>
    </table>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("TourCalendar") %>"></script>
    
    <script language="JavaScript">

        function mouseovertr(o) {
            o.style.backgroundColor = "#FFEEC4";
            //o.style.cursor="hand";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
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
                Boxy.iframeDialog({ title: "预定", iframeUrl: "<%=EyouSoft.Common.Domain.UserBackCenter %>/TeamService/RouteOrder.aspx", width: "800", height: GetAddOrderHeight(), draggable: true, data: strParms });
            } else {
                      var returnUrl=escape("<%=Request.ServerVariables["SCRIPT_NAME"]%>?TourId=<%=Request.QueryString["TourId"] %>&<%=Request.QueryString%>");
                //登录
                window.location.href = "/Register/Login.aspx?isShow=1&CityId=<%=CityId %>&returnurl="+returnUrl;
            }
        }

    </script>

    
</asp:Content>
