<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="TeamInformationPrintPage.aspx.cs"
    Inherits="UserBackCenter.PrintPage.TeamInformationPrintPage" %>
    
    <%@ Register Src="~/usercontrol/WebHeader.ascx" TagName="webHeader" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=RouteName%>行程单查看_同业114</title>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("main") %>" rel="stylesheet"
        type="text/css" />
    <style>
        BODY
        {
            background: #E2F4FF;
        }
        .printbox
        {
            border: 1px solid #CE965F;
            margin-top: 10px;
            background: #ffffff;
        }
        .padding2
        {
            padding: 2px;
        }
        .padding30
        {
            padding-left: 30px;
        }
        h1
        {
            font-size: 28px;
            line-height: 120%;
        }
        .h2
        {
            font-size: 14px;
            line-height: 120%;
            font-weight: bold;
        }
        .color1
        {
            color: #C5670C;
            font-weight: bold;
        }
        .color2
        {
            color: #C50C0C;
            font-size: 14px;
        }
        .bottow_side2
        {
            background: #EEEEEE none repeat scroll 0 0;
            border-color: -moz-use-text-color -moz-use-text-color #000000;
            border-style: none none solid;
            border-width: 0 0 1px;
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
            width: 435px;
            float: left;
        }
        .topda .daright
        {
            width: 430px;
            float: left;
            text-align: right;
        }
        .topda .daright a
        {
            color: #333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:webHeader ID="header" runat="server" />
    <table width="760" border="0" align="center" cellpadding="0" cellspacing="0" class="printbox">
        <tr>
            <td class="padding2">
                <table width="100%" border="0" cellpadding="0" id="tblHeader" cellspacing="0" bgcolor="#FFE08B"
                    style="border-bottom: 3px solid #CBC1B7;">
                    <tr>
                        <td width="16%" height="30" align="center">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            <table width="80%" border="0" align="right" cellpadding="4" cellspacing="0" style="height: 23px;">
                                <tr>
                                    <td width="25%">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="height: ">
                                            <tr>
                                                <td width="8%" align="left" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="89%" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print1.gif" width="13" height="13" />
                                                    <a href="/PrintPage/TourInfoPrintPage.aspx?TourID=<%=TourID %>"><span style="color: #8D2800">
                                                        【打印行程单】</span> </a>
                                                </td>
                                                <td width="3%" align="right" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="31%">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="7%" align="left" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="89%" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/word1.gif" width="13" height="13" />
                                                    <asp:LinkButton ID="lkbWord" OnClick="btnWord_Click" runat="server"><span style="color: #8D2800">【行程单word格式】</span></asp:LinkButton>
                                                </td>
                                                <td width="4%" align="right" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="31%">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%" align="left" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="92%" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/tzs1.gif" width="13" height="13" /><a target="_blank"
                                                        href="/PrintPage/TeamNotice.aspx?TourID=<%=TourID+OrderIDTourLinkParam %>"><span
                                                            style="color: #8D2800">【打印出团通知书】</span></a>
                                                </td>
                                                <td width="3%" align="right" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="13%">
                                        <a href="javascript:void(0):" onclick="closeWin()">【关闭】</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="printPageTop">
                
                    <table width="100%" runat="server" id="tbl_Header" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFEFA"
                        style="table-layout: fixed;">
                        <tr>
                            <td height="50" align="center" valign="bottom">
                                <h1 style="margin-bottom: 2px; margin-top: 10px; text-align: center; font-weight: bold;
                                    font-size: 28px; line-height: 120%;">
                                    <%=CompanyName%></h1>
                                <span style="font-size: 12px; line-height: 15px;">许可证号:<%=License%></span>
                            </td>
                        </tr>                 
                        <tr>
                            <td align="left">
                                <strong>联系人：</strong><%=TourContact%><strong> &nbsp;电话：</strong><%=TourContactTel%><strong>
                                    &nbsp;地址：</strong><%=CompanyAddress%>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="margin10" >
                        <tr>
                            <td height="43" align="center" background="<%=ImageServerPath %>/images/tour_title.gif"
                                class="h2">
                                <%=RouteName%>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="3" cellspacing="1" class="margin5" >
                        <tr>
                            <td style="width: 80px;border: 1px solid #E7D0A2; text-align:right;" bgcolor="#FFFFFF">
                                <strong>团号：</strong>
                            </td>
                            <td  align="left" style="border: 1px solid #E7D0A2;" bgcolor="#FFFFFF">
                                <%=TourCode %>
                            </td>
                            <td align="left"  style="border: 1px solid #E7D0A2;" bgcolor="#FFFFFF">
                                <strong>天数：</strong><%=TourDays%>天
                            </td>
                        </tr>
                        <tr runat="server" id="Tr_CollectionContect">
                            <td style="width: 80px; vertical-align: top;border: 1px solid #E7D0A2;text-align:right;" bgcolor="#FFFFFF">
                                <strong>集合方式：</strong>
                            </td>
                            <td colspan="2" align="left"  style="border: 1px solid #E7D0A2;" bgcolor="#FFFFFF">
                                <asp:Literal ID="ltrCollectionContect" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr runat="server" id="Tr_MeetTourContect">
                            <td style="width: 80px; vertical-align: top;border: 1px solid #E7D0A2;text-align:right;" bgcolor="#FFFFFF">
                                <strong>接团方式：</strong>
                            </td>
                            <td colspan="2"  style="border: 1px solid #E7D0A2;" align="left" bgcolor="#FFFFFF">
                                <asp:Literal ID="ltrMeetTourContect" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <%if (isNotLocalCompany)
                          {%>
                        <tr>
                            <td width="80px"  style="border: 1px solid #E7D0A2;text-align:right;" bgcolor="#FFFFFF">
                                <strong>地 接 社：</strong>
                            </td>
                            <td colspan="2" align="left"  style="border: 1px solid #E7D0A2;" bgcolor="#FFFFFF" runat="server" id="Td_LocalAgency">
                                <table id="tblLocalCompanyInfo" width="100%" border="0" align="center" cellpadding="1"
                                    cellspacing="0" bordercolor="#000000" style="border-top: 0px;">
                                    <tr>
                                        <td style="border-top: 0px; width: 640px;">
                                            <table style="display: inline;">
                                                <asp:Repeater runat="server" ID="rptTourLocalityInfo">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%#Eval("LocalCompanyName")%>
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;许可证号：<%#Eval("LicenseNumber")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%} %>
                        <tr runat="server" id="Tr_Traffic"  style="border: 1px solid #E7D0A2;text-align:right;">
                            <td style="width: 80px; vertical-align: top;border: 1px solid #E7D0A2;" bgcolor="#FFFFFF">
                                <strong>交通安排：</strong>
                            </td>
                            <td colspan="2"  style="border: 1px solid #E7D0A2;" align="left" bgcolor="#FFFFFF">
                                <asp:Literal ID="ltrTraffic" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px;border: 1px solid #E7D0A2;text-align:right;" bgcolor="#FFFFFF">
                                <strong>出发日期：</strong>
                            </td>
                            <td align="left"  style="border: 1px solid #E7D0A2;" bgcolor="#FFFFFF">
                                <strong><span class="color1">
                                    <%=LeaveDate.ToShortDateString() %>
                                    <%=EyouSoft.Common.Utils.ConvertWeekDayToChinese(LeaveDate)%></span></strong>
                            </td>
                            <td align="left"  style="border: 1px solid #E7D0A2;" bgcolor="#FFFFFF">
                                <strong>返程日期：</strong><strong><span class="color1"><%=BackDate.ToShortDateString()%>
                                    <%=EyouSoft.Common.Utils.ConvertWeekDayToChinese(BackDate)%></span></strong>
                            </td>
                        </tr>
                    </table>
                </div>
                <table width="100%" border="0" align="center" cellspacing="1" cellpadding="3" bgcolor="#FFFFFF"
                    style="table-layout: fixed;">
                    <asp:Repeater runat="server" ID="rptTourPriceDetail">
                        <ItemTemplate>
                            <tr>
                                <td width="85" align="left" bgcolor="#eeeeee">
                                    <%#Eval("PriceStandName")%>：
                                </td>
                                <td width="155" bgcolor="#eeeeee">
                                    成人价格：
                                    <input name="txtPeoplePrice" type="text" value="<%#Eval("AdultPrice2","{0:F2}") %>"
                                        id="txtPeoplePrice" class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                                <td width="92" align="right" bgcolor="#eeeeee">
                                    儿童价格：
                                </td>
                                <td width="143" bgcolor="#eeeeee">
                                    <input name="txtChildPrice" type="text" value="<%#Eval("ChildrenPrice2","{0:F2}") %>"
                                        id="txtChildPrice" class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                                <td width="83" align="right" bgcolor="#eeeeee">
                                    单房差：
                                </td>
                                <td width="163" bgcolor="#eeeeee">
                                    <input name="txtChildPrice" type="text" value="<%#Eval("SingleRoom2","{0:F2}") %>"
                                        id="txtChildPrice" class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div id="printPageFooter">
                    <table width="100%" border="0" cellpadding="3" cellspacing="0" class="margin10" style="table-layout: fixed;">
                        <tr>
                            <td align="left" bgcolor="#F8EEE6" style="border-bottom: 1px solid #E3CAB7;">
                                <strong>行程信息及相关：<img src="<%=ImageServerPath %>/images/ttt.gif" width="15" height="16" /></strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="word-wrap: break-word; padding: 0px;">
                                <%=QuickPlanContent %>
                                <asp:Repeater runat="server" ID="rptStandardPlan" OnItemDataBound="rptStandardPlan_ItemDataBound">
                                    <ItemTemplate>
                                        <table width="100%" style="border-bottom: 1px dashed #cccccc;" border="0" cellspacing="0"
                                            cellpadding="3">
                                            <tr>
                                                <td width="30%" height="20" align="left">
                                                    第<%#Eval("PlanDay")%>天&nbsp; <span class="color1">
                                                        <asp:Literal ID="ltrPlanDate" runat="server"></asp:Literal></span> (<asp:Literal
                                                            ID="ltrWeekDay" runat="server"></asp:Literal>)
                                                </td>
                                                <td width="30%" align="left" class="zi" style="word-wrap: break-word;">
                                                    行：
                                                    <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("PlanInterval").ToString())%>
                                                </td>
                                                <td width="19%" align="left" class="red">
                                                    住：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("House").ToString())%>
                                                </td>
                                                <td width="21%" align="left" class="lv">
                                                    餐：<%#Eval("Dinner")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="left" class="padding30" style="word-wrap: break-word;">
                                                    <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("PlanContent").ToString())%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlServiceStandard" runat="server">
                        <%if (!string.IsNullOrEmpty(ResideContent) || !string.IsNullOrEmpty(DinnerContent) || !string.IsNullOrEmpty(SightContent) || !string.IsNullOrEmpty(CarContent) || !string.IsNullOrEmpty(GuideContent) || !string.IsNullOrEmpty(TrafficContent) || !string.IsNullOrEmpty(IncludeOtherContent))
                          {%>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left" bgcolor="#F8EEE6" style="border-bottom: 1px solid #E3CAB7;">
                                    <strong>服务标准及说明：<img src="<%=ImageServerPath %>/images/ttt.gif" width="15" height="16" /></strong>
                                </td>
                            </tr>
                        </table>
                        <%} %>
                        <table width="100%" border="1" align="center" cellpadding="2" cellspacing="0" bordercolor="#cccccc"
                            style="table-layout: fixed;">
                            <%if (!string.IsNullOrEmpty(ResideContent) || !string.IsNullOrEmpty(DinnerContent) || !string.IsNullOrEmpty(SightContent) || !string.IsNullOrEmpty(CarContent) || !string.IsNullOrEmpty(GuideContent) || !string.IsNullOrEmpty(TrafficContent) || !string.IsNullOrEmpty(IncludeOtherContent))
                              {%>
                            <tr>
                                <td width="86" align="right">
                                    包含项目：
                                </td>
                                <td width="604" align="left" style="word-wrap: break-word;">
                                    <%if (!string.IsNullOrEmpty(ResideContent))
                                      {%>
                                    住宿：<%=ResideContent%>
                                    <br />
                                    <%} if (!string.IsNullOrEmpty(DinnerContent))
                                      { %>
                                    用餐：<%=DinnerContent%>
                                    <br />
                                    <%} if (!string.IsNullOrEmpty(SightContent))
                                      { %>
                                    景点：<%=SightContent%>
                                    <br />
                                    <%} if (!string.IsNullOrEmpty(CarContent))
                                      { %>
                                    用车：<%=CarContent%>
                                    <br />
                                    <%} if (!string.IsNullOrEmpty(GuideContent))
                                      { %>
                                    导游：<%=GuideContent%>
                                    <br />
                                    <%} if (!string.IsNullOrEmpty(TrafficContent))
                                      { %>
                                    往返交通：<%=TrafficContent%>
                                    <br />
                                    <%} if (!string.IsNullOrEmpty(IncludeOtherContent))
                                      { %>
                                    其它：<%=IncludeOtherContent%>
                                    <%} %>
                                </td>
                            </tr>
                            <%} %>
                            <tr runat="server" id="Tr_NotContainService">
                                <td align="right" width="86">
                                    其他说明：
                                </td>
                                <td align="left" style="word-wrap: break-word;">
                                    <%=NotContainService%>
                                </td>
                            </tr>
                            <tr runat="server" id="Tr_SpeciallyNotice">
                                <td align="right">
                                    备注：
                                </td>
                                <td align="left" style="word-wrap: break-word;">
                                    <%=SpeciallyNotice%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <table width="100%" id="tblFooter" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFE08B"
                    style="border-bottom: 3px solid #CBC1B7;">
                    <tr>
                        <td width="16%" height="30" align="center">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            <table width="80%" border="0" align="right" cellpadding="4" cellspacing="0">
                                <tr>
                                    <td width="25%">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="8%" align="left" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="89%" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print1.gif" width="13" height="13" />
                                                    <a href="/PrintPage/TourInfoPrintPage.aspx?TourID=<%=TourID %>"><span style="color: #8D2800">
                                                        【打印行程单】</span> </a>
                                                </td>
                                                <td width="3%" align="right" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="31%">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="7%" align="left" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="89%" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/word1.gif" width="13" height="13" />
                                                    <asp:LinkButton ID="lkbWord2" OnClick="btnWord_Click" runat="server"><span style="color: #8D2800">【行程单word格式】</span></asp:LinkButton>
                                                </td>
                                                <td width="4%" align="right" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="31%">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%" align="left" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="92%" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/tzs1.gif" width="13" height="13" /><a target="_blank"
                                                        href="/PrintPage/TeamNotice.aspx?TourID=<%=TourID+OrderIDTourLinkParam %>"><span
                                                            style="color: #8D2800">【打印出团通知书】</span></a>
                                                </td>
                                                <td width="3%" align="right" style="background-image: url(<%=ImageServerPath %>/images/print-m.gif);
                                                    background-repeat: repeat-x;">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="13%">
                                        <a href="javascript:void(0):" onclick="closeWin()">【关闭】</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <iframe id="iframe_TourInfoPrintPage" style="display: none;" src="/PrintPage/TourInfoPrintPage.aspx?TourID=<%=TourID %>">
    </iframe>
    <input id="hidPrintFooterHTML" name="hidPrintFooterHTML" type="hidden" />
    <input type="hidden" id="hidPrintTopHTML" name="hidPrintTopHTML" />

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" language="javascript">
        function closeWin() {
            window.self.close();
        }
        function getHtml() {
            var iframe = document.getElementById("iframe_TourInfoPrintPage");
            var Doc = iframe.document;
            if (iframe.contentDocument) { // For NS6
                Doc = iframe.contentDocument;
            } else if (iframe.contentWindow) { // For IE5.5 and IE6
                Doc = iframe.contentWindow.document;
            }           
            $("#hidPrintTopHTML").val(Doc.getElementById("div_printTop").innerHTML);
            $("#hidPrintFooterHTML").val(Doc.getElementById("div_printFooter").innerHTML);
        }
        function printPage() {
            $("#tblFooter").hide();
            $("tblHeader").hide();
            if (window.print != null) {
                window.print();
                $("#tblFooter").show(1000);
                $("#tblHeader").show(1000);
            } else {
                alert('没有安装打印机');
            }
        }
    </script>
<div style="display:none"><script src="http://s4.cnzz.com/stat.php?id=1125215&amp;web_id=1125215" language="JavaScript"></script><a href="http://www.cnzz.com/stat/website.php?web_id=1125215" target="_blank" title="站长统计">站长统计</a><img src="http://zs7.cnzz.com/stat.htm?id=1125215&amp;r=&amp;lg=undefined&amp;ntime=0.26240200%201308804097&amp;repeatip=50&amp;rtime=63&amp;cnzz_eid=37723136-1299030093-http%3A//www.tongye114.com/xianlu_19&amp;showp=1280x1024&amp;st=321&amp;sin=&amp;res=0" border="0" height="0" width="0"></div>
    </form>
</body>
</html>
