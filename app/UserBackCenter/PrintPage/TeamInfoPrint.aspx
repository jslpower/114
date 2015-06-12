<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamInfoPrint.aspx.cs"
    Inherits="UserBackCenter.PrintPage.TeamInfoPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
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
        .detailpint
        {
            border-bottom: 1px dashed #cccccc;
            font-size: 12px;
            text-align: left;
        }
        .detailpint p
        {
            margin: auto 0;
            padding: 0;
            clear: both;
            text-align: left;
        }
        .detailpint td, h1, h2, h3, h4
        {
            font-size: 12px;
            font-weight: normal;
            text-align: left;
        }
        .bottow_side2
        {
            background: #EEEEEE none repeat scroll 0 0;
            border-color: -moz-use-text-color -moz-use-text-color #000000;
            border-style: none none solid;
            border-width: 0 0 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="760" border="0" align="center" cellpadding="0" cellspacing="0" class="printbox">
        <tr>
            <td class="padding2">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFE08B"
                    style="border-bottom: 3px solid #CBC1B7;">
                    <tr>
                        <td width="16%" height="30" align="center">
                            <a href="#"></a>
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
                                                <td width="8%" align="left" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="89%" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print1.gif" width="13" height="13" /><a onclick="window.open('print.html','行程单打印','height=700,width=970,top=0,left=30,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no') "
                                                        href="javascript:void(0)"><span style="color: #8D2800">【打印行程单】</span> </a>
                                                </td>
                                                <td width="3%" align="right" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="31%">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="7%" align="left" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="89%" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/word1.gif" width="13" height="13" /><a href="#"
                                                        target="_blank"><span style="color: #8D2800">【行程单word格式】</span></a>
                                                </td>
                                                <td width="4%" align="right" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="31%">
                                        &nbsp;
                                    </td>
                                    <td width="13%">
                                        【关闭】
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFEFA">
                    <tr>
                        <td height="50" align="center" valign="bottom">
                            <h1 style="margin-bottom: 2px">
                                <%=CompanyName%></h1>
                            <span style="font-size: 12px; line-height: 15px;">许可证号:<%=License%></span>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left">
                            <strong>地&nbsp; &nbsp;址：</strong><%=CompanyAddress%><strong> 联系人：</strong><%=TourContact%><strong>
                                电&nbsp; 话：</strong><%=TourContactTel%>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="margin10">
                    <tr>
                        <td height="43" align="center" background="<%=ImageServerPath %>/images/tour_title.gif"
                            class="h2">
                            上海、苏州、杭州、宁波、温州、厦门单飞双卧【5天】
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellspacing="1" bgcolor="#FFFFFF">
                    <asp:Repeater runat="server" ID="rptTourPriceDetail" OnItemDataBound="rptTourPriceDetail_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td width="99" align="left" bgcolor="#eeeeee">
                                    <%#Eval("PriceStandName")%>：
                                </td>
                                <td width="155" bgcolor="#eeeeee">
                                    成人价格：
                                    <input name="txtPeoplePrice" type="text" value="<%#Eval("AdultPrice") %>" id="txtPeoplePrice"
                                        class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                                <td width="92" align="right" bgcolor="#eeeeee">
                                    儿童价格：
                                </td>
                                <td width="143" bgcolor="#eeeeee">
                                    <input name="txtChildPrice" type="text" value="<%#Eval("ChildrenPrice") %>" id="txtChildPrice"
                                        class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                                <td width="83" align="right" bgcolor="#eeeeee">
                                    单房差：
                                </td>
                                <td width="163" bgcolor="#eeeeee">
                                    <input name="txtChildPrice" type="text" value="<%#Eval("SingleRoom") %>" id="txtChildPrice"
                                        class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellpadding="3" cellspacing="0" class="margin10">
                    <tr>
                        <td align="left" bgcolor="#F8EEE6" style="border-bottom: 1px solid #E3CAB7;">
                            <strong>行程信息及相关：<img src="<%=ImageServerPath %>/images/ttt.gif" width="15" height="16" /></strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="detailpint">
                            <%=QuickPlanContent%>
                            <p align="center">
                                <strong>福天福地福建行? 闽山闽水闽南游 </strong>
                                <br />
                                <strong>武夷山/厦门双卧六日游 </strong>
                            </p>
                            <table border="1" cellspacing="0" cellpadding="0" align="left">
                                <asp:Repeater runat="server" ID="rptStandardPlan">
                                    <HeaderTemplate>
                                        <tr>
                                            <td width="64" valign="top">
                                                <p align="center">
                                                    <strong>日期 </strong>
                                                </p>
                                            </td>
                                            <td width="447" colspan="4" valign="top">
                                                <p>
                                                    <strong>旅游行程</strong>
                                                </p>
                                            </td>
                                            <td width="78" valign="top">
                                                <p align="center">
                                                    <strong>用</strong><strong> </strong><strong>餐</strong><strong> </strong>
                                                </p>
                                            </td>
                                            <td width="78" valign="top">
                                                <p align="center">
                                                    <strong>住宿</strong><strong> </strong>
                                                </p>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td width="64" valign="top">
                                                <h1>
                                                    D<%#Eval("PlanDay")%></h1>
                                            </td>
                                            <td width="447" colspan="4" valign="top">
                                                <p>
                                                    <strong>
                                                        <%#Eval("PlanContent")%></strong>
                                                </p>
                                            </td>
                                            <td width="78" valign="top">
                                                <p align="center">
                                                    <%#Eval("Dinner")%></p>
                                            </td>
                                            <td width="78" valign="top">
                                                <p>
                                                    <%#Eval("Vehicle")%>
                                                </p>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td width="667" colspan="7" valign="top">
                                        <p>
                                            <strong>提供标准及价格形成 </strong>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="76" colspan="2" valign="top">
                                        <p>
                                            住 宿
                                        </p>
                                    </td>
                                    <td width="260" valign="top">
                                        <p>
                                            <%=ResideContent %>
                                        </p>
                                    </td>
                                    <td width="76" valign="top">
                                        <p align="center">
                                            门 票
                                        </p>
                                    </td>
                                    <td width="255" colspan="3" valign="top">
                                        <p>
                                            <%=DoorPrice %>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="76" colspan="2" valign="top">
                                        <p>
                                            导 服
                                        </p>
                                    </td>
                                    <td width="260" valign="top">
                                        <p>
                                            <%=GuideContent%>
                                        </p>
                                    </td>
                                    <td width="76" valign="top">
                                        <p align="center">
                                            景 交
                                        </p>
                                    </td>
                                    <td width="255" colspan="3" valign="top">
                                        <p>
                                            <%=CarContent%>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="76" colspan="2" valign="top">
                                        <p>
                                            用 餐
                                        </p>
                                    </td>
                                    <td width="260" valign="top">
                                        <p>
                                            <%=DinnerContent%>
                                        </p>
                                    </td>
                                    <td width="76" valign="top">
                                        <p>
                                            大交通
                                        </p>
                                    </td>
                                    <td width="255" colspan="3" valign="top">
                                        <p>
                                            <%=TrafficContent%>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="76" colspan="2" valign="top">
                                        <p>
                                            报 价
                                        </p>
                                    </td>
                                    <td width="591" colspan="5" valign="top">
                                        <p>
                                            <%=SiglePeplePrice%>元/人
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="76" colspan="2" valign="top">
                                        <p>
                                            友 情
                                            <br />
                                            提 示
                                        </p>
                                    </td>
                                    <td width="591" colspan="5" valign="top">
                                        <p>
                                            <%=SpeciallyNotice%>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
