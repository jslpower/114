<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" enableEventValidation="false" Inherits="UserBackCenter.TicketsCenter.Default" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/WebHeader.ascx" TagName="webHeader" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/WebFooter.ascx" TagName="webFooter" TagPrefix="uc1" %>
<%@ Register Src="~/TicketsCenter/usercontrols/FileUploadControl.ascx" TagName="fileUploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>机票供应商_同业114</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("jipiaoadminstyle") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .topbar, .divtopfull, .divmainfull, .margin10
        {
            position: absolute;
            top: -10000px;
            left: -10000px;
        }
        #top
        {
            background-image: url(<%=ImageServerUrl %>/images/UserPublicCenter/sitebarbj.gif);
        }
        #top
        {
            background-repeat: repeat-x;
            height: 26px;
            text-align: left;
        }  
        .topda
        {
            width: 970px;
            margin: 0 auto;
            padding: 0;
            padding-top: 5px;
        }
        .topda .daleft
        {
            width: 460px;
            float: left;
        }
        .topda .daright
        {
            width: 510px;
            float: left;
            text-align: right;
        }
        .topda .daright a
        {
            color: #333;
        }    
        html{overflow-Y:scroll; margin-right : 0 ; padding : 0 ; }
        </style>
</head>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dcommon") %>"></script>
<body>

    <form id="form1" runat="server">
    <div id="loadingItem" style="position: absolute; top: 8em; left: 2em">
        <br />
        <font size="+2">正在加载...</font><br />
    </div>
    <noscript>
        <style type="text/css">
            div
            {
                display: none;
            }
            table
            {
                display: none;
            }
            #noscript
            {
                padding: 3em;
                font-size: 130%;
            }
        </style>
        <p id="noscript">
            要使用当前网站平台，必须启用 JavaScript。不过，JavaScript 似乎已被禁用或者您的浏览器不支持 JavaScript。要使用网站平台，请更改您的浏览器选项，启用
            JavaScript，然后 <a href="/Default.aspx">重试</a>。</p>
    </noscript>
    <div id="topmessage" class="topmessage">
        <span>正在载入...</span></div>
    <cc1:webHeader ID="header" runat="server" />
    <!-- divtopfull start-->
    <div class="divtopfull">
        <div class="logo">
            <img id="imgLogo" runat="server" src="" width="130" height="50" /></div>
        <div class="headright">
            <span><asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal></span>
            <div class="option" id="optionTab">
                <div class="option-tab-on-index">
                    <span><a href="javascript:;">首页</a></span></div>
            </div>
        </div>
    </div>
    <!-- divtopfull end-->
    <!--divmainfull start-->
    <div class="divmainfull">
        <!--leftmenu start-->
        <div class="leftmenu">
            <div class="topopeara">
                <b>操作台</b></div>
            <div id="divOrderMange">
                <a href="javascript:;" class="gongnengzu">订单管理</a></div>
            <div class="gongnengxx">
            </div>
            <div class="gongneng">
                <a href="/ticketscenter/ordermanage/queryorder.aspx" class="commbar" rel="toptab">订单查询</a> <a href="/ticketscenter/ordermanage/orderlist.aspx"
                    class="commbar" rel="toptab">订单处理</a>
                    <%--<a href="/ticketscenter/ordermanage/queryorder.aspx" style="display:none;">订单明细</a>--%>
                <div style="height: 7px; clear: both">
                </div>
            </div>
            <div id="FreightManage">
                <a href="javascript:;" class="gongnengzu">运价管理</a></div>
            <div class="gongnengxx" style="display:none;"></div>
            <div class="gongneng" style="display:none;">
               <a class="commbar" href="/ticketscenter/freightmanage/freightadd.aspx" rel="toptab">团队／散拼</a>
                <div style="height: 7px; clear: both;">
                </div>
            </div>
            <div id="StatisticsManage">
                <a href="javascript:;" class="gongnengzu">统计分析</a>
            </div>
            <div class="gongnengxx" style="display:none;"></div>
			<div class="gongneng" style="display:none;">
				<a href="/ticketscenter/statisticsmanage/purchasinganalysis/default.aspx" class="commbar" rel="toptab">采购分析</a>
				<a href="/ticketscenter/statisticsmanage/orderstatistics/default.aspx" class="commbar" rel="toptab">订单统计</a>
				<div style="height:7px; clear:both"></div>
	       </div>
            <div>
                <a class="gongnengzu" href="/ticketscenter/joinpartner/default.aspx" rel="toptab">加入支付圈</a></div>	
            <div>
                <a href="/ticketscenter/purchaserouteship/default.aspx" class="gongnengzu" rel="toptab">购买运价航线</a></div>
            <div id="divServices">
                <a href="javascript:;" class="gongnengzu">行程单服务</a></div>
            <div class="gongnengxx" style="display:none;"></div>
            <div class="gongneng" style="display:none;">
                <a class="commbar" href="/ticketscenter/itinerary/suppliersinfo.aspx" rel="toptab">供应商详情</a>
                <div style="height: 7px; clear: both;">
                </div>
            </div>
            <div>
                <a href="/supplyinformation/addsupplyinfo.aspx" class="bigclassbar" rel="toptab">供求信息</a></div>
            <div>
                <a class="gongnengzu" href="/smscenter/sendsms.aspx" rel="toptab">短信中心</a>
            </div>
            <div id="divTools">
                <a href="javascript:;" class="gongnengzu">常用工具</a></div>
            <div class="gongnengxx" style="display:none;">
            </div>
            <div class="gongneng" style="display:none;">
               <a href="/memorandum/memorandumcalendar.aspx"
                        class="gongnengbw" onclick='topTab.open($(this).attr("href"),"备忘录日历",{isRefresh:false});return false;'>
                        备忘录</a>
                <div style="height: 7px; clear: both">
                </div>
            </div>
            <%--<div>
                <a href="#" class="gongnengzu">系统设置</a></div>--%>
        </div>
        <!--leftmenu end-->
        <!--right start-->
        <div class="admin_right" id="optionPanel">
            <div class="option-panel-on">
                <div class="rigtop">
                    <span style="text-align: left; display: block; padding-left: 25px; line-height: 25px;
                        color: #0056A3;">您好，<asp:Literal ID="ltrUserName" runat="server"></asp:Literal> 欢迎登录同业114。<br />
                        目前平台已经有 <strong class="chengse"><asp:Literal ID="ltrGongYingCompanyCount" runat="server"></asp:Literal></strong> 家机票供应商加盟</span>
                </div>
                <!--flight_info start-->
                <div class="flight_info">
                    <span class="info_Title">
                        <img src="<%=ImageServerUrl %>/images/jipiao/orderform_icon.jpg" class="Img" />订单管理</span>
                    <table id="Default_tblOrderList" width="835" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#7dabd8"
                        style="margin-top: 10px;">
                        <tr>
                            <th width="139" height="30" align="center" bgcolor="#EEF7FF">
                                &nbsp;
                            </th>
                            <th width="139" align="center" bgcolor="#EEF7FF">
                                特殊代理费
                            </th>
                            <th width="139" align="center" bgcolor="#EEF7FF">
                                散客票
                            </th>
                            <th width="139" align="center" bgcolor="#EEF7FF">
                                团队/散拼票
                            </th>
                            <th width="139" align="center" bgcolor="#EEF7FF">
                                国际票
                            </th>
                            <th width="139" align="center" bgcolor="#EEF7FF">
                                特价申请
                            </th>
                        </tr>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                待审核（张）
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="0" ratetype="2" Orderstattype="0">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="0" ratetype="1" Orderstattype="0">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="0" ratetype="3" Orderstattype="0" >
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="0" ratetype="5" Orderstattype="0">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="0" ratetype="4" Orderstattype="0">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                待处理（张）
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="3" ratetype="2" Orderstattype="1">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="3" ratetype="1" Orderstattype="1">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="3" ratetype="3" Orderstattype="1">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="3" ratetype="5" Orderstattype="1">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="3" ratetype="4" Orderstattype="1">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                待退票（张）
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="2" Orderstattype="2">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="1" Orderstattype="2">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="3" Orderstattype="2">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="5" Orderstattype="2">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="4" Orderstattype="2">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                自愿/非自愿（天）
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.特殊代理费]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.特殊代理费]).ToString("F1"))%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.散客政策]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.散客政策]).ToString("F1"))%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.团队散拼]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.团队散拼]).ToString("F1"))%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.国际政策]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.国际政策]).ToString("F1"))%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.特价政策]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.特价政策]).ToString("F1"))%>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                待作废（张）
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="2" Orderstattype="4">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="1" Orderstattype="4">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="3" Orderstattype="4">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="5" Orderstattype="4">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="4" Orderstattype="4">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                待改期（张）
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="2" Orderstattype="5">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="1" Orderstattype="5">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="3" Orderstattype="5">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="5" Orderstattype="5">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="4" Orderstattype="5">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                待改签（张）
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="2" Orderstattype="6">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="1" Orderstattype="6">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="3" Orderstattype="6">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="5" Orderstattype="6" >
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="4" Orderstattype="6">
                                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" colspan="6" align="left" bgcolor="#FFFFFF">
                                <table width="40%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="6%">
                                            &nbsp;
                                        </td>
                                        <td width="94%">
                                            <b><font color="#FF0000">注：点击数字可以直接处理相应订单！</font></b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <!--flight_info start-->
                <!--Passenger_info start-->
                <div class="Passenger_info">
                    <span class="info_Title">
                        <img src="<%=ImageServerUrl %>/images/jipiao/user_icon.jpg" class="Img" />用户资料管理</span>
                        <form id="DefaultForm" style="padding:0px;margin:0px;">
                    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#EEF7FF"
                        class="userInfo" id="tableID">
                        <tr>
                            <td height="10" colspan="4" align="center">
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" height="30" align="center">
                                联系人：
                            </td>
                            <td width="35%" align="left">
                                <input type="text" runat="server" id="Default_ContactPeople" value="" errmsg="联系人不能为空!" valid="required" />
                                <span style="position:relative;" class="errmsg" id="errMsg_Default_ContactPeople" class="errmsg"></span>
                            </td>
                            <td width="15%" align="center">
                                联系电话：
                            </td>
                            <td width="35%" align="left">
                                <input runat="server" type="text" id="Default_Phone" value="" errmsg="电话不能为空|电话号码格式不正确" valid="required|isPhone" />
                                <span class="errmsg" id="errMsg_Default_Phone" class="errmsg"></span>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="center">
                                代理级别：
                            </td>
                            <td align="left">
                                <asp:Literal ID="ltrUserLevel" runat="server">无</asp:Literal>
                            </td>
                            <td align="center">
                                出票成功率：
                            </td>
                            <td align="left">
                                <asp:Literal ID="ltrTicketSuccess" runat="server">100%</asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="center">
                                ICP备案号：
                            </td>
                            <td align="left">
                                <input runat="server" type="text" id="Default_ICP" value="" size="20" errmsg="ICP备案号不能为空!" valid="required" />
                                <span class="errmsg" id="errMsg_Default_ICP" class="errmsg"></span>
                            </td>
                            <td align="center">
                                网址：
                            </td>
                            <td align="left">
                                <label for="Default_Url">http://</label><input runat="server" type="text" id="Default_Url" value="" errmsg="网址不能为空!|请填写有效的网址：例如:www.tongye114.com/" valid="required|notHttpUrl" />
                                <span class="errmsg" id="errMsg_Default_Url" class="errmsg"></span>
                            </td>
                        </tr>
                        <tr>
                            <td height="36" align="center">
                                营业执照：
                            </td>
                            <td colspan="3" align="left">
                                <table border="0" align="left" cellpadding="0" cellspacing="0">
                                    <tr>
                                    <td><uc2:fileUploadControl ID="fileUploadControl1" runat="server" /></td>
                                    <td id="tdFile1"><asp:Literal ID="ltrFile1" runat="server"></asp:Literal></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="36" align="center">
                                铜牌图片上传：
                            </td>
                            <td colspan="3" align="left">
                                <table border="0" align="left" cellpadding="0" cellspacing="0">
                                    <tr>
                                    <td><uc2:fileUploadControl ID="fileUploadControl2" runat="server" /></td>
                                    <td id="tdFile2"><asp:Literal ID="ltrFile2" runat="server"></asp:Literal></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="36" align="center">
                                行业奖项图片：
                            </td>
                            <td colspan="3" align="left">
                                <table border="0" align="left" cellpadding="0" cellspacing="0">
                                    <tr>
                                    <td><uc2:fileUploadControl ID="fileUploadControl3" runat="server" /></td>
                                    <td id="tdFile3"><asp:Literal ID="ltrFile3" runat="server"></asp:Literal></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="150" align="center">
                                备注：
                            </td>
                            <td colspan="3" align="left">
                                <textarea runat="server" id="Default_Remark" cols="45" rows="8"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td height="40" align="right">
                                &nbsp;
                            </td>
                            <td height="40" align="left">
                            <a id="Default_linkSave" style="cursor:pointer;"><img alt="保存" src="<%=ImageServerUrl %>/images/jipiao/baocun_btn.jpg" width="79" height="25" /></a><span class="errmsg" id="spanSaveMsg"></span>
                            </td>
                            <td height="40" align="left">
                                &nbsp;
                            </td>
                            <td height="30" align="left">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    </form>
                </div>
                <!--Passenger_info end-->
            </div>
        </div>
        <!--right end-->
    </div>
    <!--divmainfull end-->
    <uc1:webFooter ID="footer" runat="server" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dhtmlHistory") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("517ticketcore2") %>"></script>
 
    <script type="text/javascript">
     var commonTourModuleData={
            _data:[],
            add:function(obj){
                this._data[obj.ContainerID] = obj;
            },
            get:function(id){
                return this._data[id];
            }
        };
    </script>
    <script type="text/javascript">
    function Hide_Show_Div(obj)
    {
         if($(obj).next().next().css("display") == "none"){
            $(obj).next().css("display","");
            $(obj).next().next().css("display","");
        }else{
            $(obj).next().css("display","none");
            $(obj).next().next().css("display","none");
        }
    }
    function historyChangeHandler(hash,historyData){
        var url = decodeURIComponent(hash).replace("page:","");
        var index = topTab.getIndexByUrl(url);
        if(index!=undefined){
            topTab.select(index);
        }else{
            if(historyData==null||historyData==undefined){
                var currentLink = $(".leftmenu").find("a[href*='"+url+"']");
                if(currentLink.length>0){
                    historyData = currentLink.eq(0).text();
                }
            }
            if(historyData!=null && historyData!=undefined){
                 topTab.add(url,historyData);
            }
        }
    }
    function GetQueryString(key,url) {
        //var uri = window.location.search;
        var uri = url;
        var re = new RegExp("" + key + "\=([^\&\?]*)", "ig");
        return ((uri.match(re)) ? (uri.match(re)[0].substr(key.length + 1)) : null);
    }
    var topTab,currentSwfuploadInstances=[],LoginUrl="<%=EyouSoft.Security.Membership.UserProvider.Url_Login %>";
    function renderBody(){
        topTab = new TopTab({
            onSelect:function(tabObj){
                 if(tabObj.index==0){
                    dhtmlHistory.add("",tabObj.title);
                 }else{
                    dhtmlHistory.add(encodeURIComponent("page:"+tabObj.url),tabObj.title);
                 }
            },
            onAdd:function(tabObj){
                if(tabObj.index==0){
                    dhtmlHistory.add("",tabObj.title);
                 }else{
                    dhtmlHistory.add(encodeURIComponent("page:"+tabObj.url),tabObj.title);
                 }
            }
        });
        
        dhtmlHistory.initialize();
       
       // add ourselves as a DHTML History listener
       dhtmlHistory.addListener(historyChangeHandler);
       
       //currentHash
       var currentHash = decodeURIComponent(dhtmlHistory.getCurrentLocation()).replace("page:","");
        var currentLink = $(".leftmenu").find("a[href*='"+currentHash+"']");
        if(currentHash!=""&&currentLink.length>0){
            var b = topTab.open(currentHash,currentLink.eq(0).text());
        }else if(currentHash!=""){
            //find is orderdetailds
            var ist = GetQueryString("orderdetail",currentHash);
            if(ist=="1"){
                var path = currentHash.substring(0,currentHash.indexOf("?")).toLowerCase();
                if(path=="/ticketscenter/ordermanage/orderdetailinfo.aspx"){
                    var orderId = GetQueryString("orderid",currentHash);
                    var orderstate = GetQueryString("orderstate",currentHash);
                    var changetype = GetQueryString("changetype",currentHash);
                    topTab.open("/ticketscenter/ordermanage/orderdetailinfo.aspx","订单操作处理",{
                        data:{
                            orderid:orderId,
                            orderstate:orderstate,
                            changetype:changetype
                        }
                    });
                }
            }
            //find in child tab.
            var parentUrl =ChildTab.getParentUrl(currentHash);
            var parentUrlLink = $(".leftmenu").find("a[href*='"+parentUrl+"']");
            if(parentUrl!=""){
                topTab.open(parentUrl,parentUrlLink.eq(0).text(),{desUrl:currentHash});
            }else{
                var isForm = FormTab.isFormUrl(currentHash);
                if(isForm){
                    topTab.open(currentHash,"线路修改");
                }
            }
        }
        
        $("#loadingItem").html("").css({top:"-1000px",left:"-1000px"});
        $(".topbar,.divtopfull,.divmainfull,.margin10").css({
            position:"static",top:"0px",left:"0px"
        });
        
        $(".leftmenu a[href][rel='toptab']").click(function(){
            var a = $(this);
            var tabrefresh=a.attr("tabrefresh")=="false"?false:true;
            var href = a.attr("href");
            var hash = href.replace(/^.*#/, '');
            if(href.indexOf("#")==-1){
                var b = topTab.open(href,a.text(),{isRefresh:tabrefresh});
            }
            return false;
        });
        
        $("#divOrderMange,#divTools,#FreightManage,#StatisticsManage,#divServices").click(function(){
           Hide_Show_Div(this);
        });
        
        //for css file encoding bug.
        if($.browser.mozilla||$.browser.safari){
            createStyleRule("body",'color:#333;font-size:12px;font-family:"宋体",Arial, Helvetica, sans-serif; text-align: center; background:#fff; margin:0px;');
            createStyleRule("input,textarea,select",'font-size:12px;font-family:"宋体",Arial, Helvetica, sans-serif;COLOR: #333;');
        }
    };
    renderBody();
    </script>
    <script type="text/javascript">
    var Default={
        file1:<%=fileUploadControl1.ClientID %>,
        file2:<%=fileUploadControl2.ClientID %>,
        file3:<%=fileUploadControl3.ClientID %>,
        isHaveNewFile1:false,
        isHaveNewFile2:false,
        isHaveNewFile3:false,
        resetFileUpload:function(){
            if(Default.isHaveNewFile1){
                Default._resetFileUploadControl(Default.file1);
                Default.isHaveNewFile1 = false;
            }
            if(Default.isHaveNewFile2){
                Default._resetFileUploadControl(Default.file2);
                Default.isHaveNewFile2 = false;
            }
            if(Default.isHaveNewFile3){
                Default._resetFileUploadControl(Default.file3);
                Default.isHaveNewFile3 = false;
            }
        },
        _resetFileUploadControl:function(fileInstance){
            try{
                var file = fileInstance.getFile(0);
                var i = 0;
                while(true){
                    if(file==null){
                        break;
                    }else{
                        if(fileInstance.getFile(i+1)==null){
                            break;
                        }else{
                            file = fileInstance.getFile(++i);
                        }
                    }
                }
                if(file==null){
                    return;
                }
                resetSwfupload(fileInstance,file);
            }catch(e){}
        },
        uploadFile2:function(){
        
            if(Default.file2.getStats().files_queued>0){ 
                Default.isHaveNewFile2 = true;
                Default.file2.customSettings.UploadSucessCallback = Default.uploadFile3;
                Default.file2.startUpload();
            }
            else{
             Default.uploadFile3();
            }
        },
        uploadFile3:function(){
            if(Default.file3.getStats().files_queued>0){ 
                Default.isHaveNewFile3 = true;
                Default.file3.customSettings.UploadSucessCallback = Default.save2;
                Default.file3.startUpload();
            }
            else{
             Default.save2();
            }
        },
        showUploadedFileInfo:function(){
            var fileSystemPath = "<%=Domain.FileSystem %>";
            if(Default.isHaveNewFile1){
                $("#tdFile1").html("<a target='_blank' href='"+fileSystemPath+$("#"+Default.file1.customSettings.HidFileNameId).val()+"'>点击查看营业执照</a>");
            }
            if(Default.isHaveNewFile2){
                $("#tdFile2").html("<a target='_blank' href='"+fileSystemPath+$("#"+Default.file2.customSettings.HidFileNameId).val()+"'>点击查看铜牌图片</a>");
            }
            if(Default.isHaveNewFile3){
                $("#tdFile3").html("<a target='_blank' href='"+fileSystemPath+$("#"+Default.file3.customSettings.HidFileNameId).val()+"'>点击查看行业奖项图片</a>");
            }
        },
        showMsg:function(m){
            $("#spanSaveMsg").html(m); 
        },
        save:function(){
            //form validator
            var form = $("#Default_linkSave").closest("form").get(0);
            var b = ValiDatorForm.validator(form, "span");
            if(!b){
                return false;
            }
            //show current state.
            Default.showMsg("正在保存...");
            $("#Default_linkSave").unbind().css("cursor","default");
            
            //upload file.
            if(Default.file1.getStats().files_queued>0){
                Default.isHaveNewFile1 = true;
                Default.file1.customSettings.UploadSucessCallback = Default.uploadFile2;
                Default.file1.startUpload();
            }
            else{
               Default.uploadFile2();
            }
            
            return false;
        },
        save2:function(){
            
            //submit data.
            $.ajax({
                 url:"/TicketsCenter/Default.aspx?type=save",
	             data:$(".Passenger_info *").serialize(),
                 dataType:"json",
                 cache:false,
                 type:"post",
                 success:function(result){
                    if(result && result.Islogin==false){
                        alert('对不起你未登录，请登录！');
                        window.location.href = LoginUrl + "?returnurl=" + encodeURIComponent(window.location.href);
                    }else if(result && result.success){
                        if(result.success=="1"){
                            Default.showMsg("保存成功！");
                            Default.showUploadedFileInfo();
                            setTimeout(function(){
                                Default.showMsg("");
                                Default.resetFileUpload();
                                $("#Default_linkSave").click(Default.save).css("cursor","pointer");
                            },1500);
                        }else{
                            Default.showMsg(result.message);
                            setTimeout(function(){
                                Default.showMsg("");
                                Default.resetFileUpload();
                                $("#Default_linkSave").click(Default.save).css("cursor","pointer");
                            },1500);
                        }
                    }
                 },
                 error:function(){
                    Default.showMsg("保存失败，请稍候再试");
                    $("#Default_linkSave").click(Default.save).css("cursor","pointer");
                 }
            });
        },
        openOrderList:function(){
            var $obj = $(this);
            var orderstate = $obj.attr("orderstate");
            var changetype = $obj.attr("changetype");
            var ratetype = $obj.attr("ratetype");
            var Orderstattype = $obj.attr("Orderstattype");
            
            var data = {};
            
            if(orderstate!=undefined){
                data.orderstate = orderstate;
            }
            if(changetype!=undefined){
                data.changetype = changetype;
            }
            if(ratetype!=undefined){
                data.ratetype = ratetype;
            }
            if(Orderstattype!=undefined){
                data.Orderstattype = Orderstattype;
            }
            
            topTab.open("/ticketscenter/ordermanage/orderlistbystateandtype.aspx","订单处理列表",{
                data:data
            });
            
            return false;
        },
        init:function(){
            var form = $("#Default_linkSave").closest("form").get(0);
            FV_onBlur.initValid(form);
            $("#Default_linkSave").click(Default.save);
            $("#Default_tblOrderList").find("a").click(Default.openOrderList);
        }
    }
    Default.init();
    </script>
    </form>
</body>
</html>
