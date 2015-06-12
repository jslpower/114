<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourInfoPrintPage.aspx.cs"
    Inherits="UserBackCenter.PrintPage.TourInfoPrintPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=RouteName%>行程单_同业114</title>
    <style type="text/css">
        body
        {
            color: #000000;
            font-size: 12px;
            font-family: "宋体";
            background: #fff;
            margin: 0px;
            background: url(<%=ImageServerPath %>/images/lura4.gif);
            background-repeat: no-repeat;
            background-position: 0px 80px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
        }
        td
        {
            font-size: 12px;
            line-height: 18px;
            color: #000000;
        }
        .headertitle
        {
            font-family: "黑体";
            font-size: 25px;
            line-height: 120%;
            font-weight: bold;
        }
        .baocun_an
        {
            font-size: 12px;
            color: #000000;
            background-image: url(<%=ImageServerPath %>/images/baocun.gif);
            float: none;
            height: 24px;
            width: 103px;
            border: 0px;
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
    <table id="tbl_top" width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 10px;">
        <tr>
            <td height="31" valign="bottom" bgcolor="#FFE08B" style="border-bottom: 1px solid #DCAE30;">
                <table width="767" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="155" align="left">
                            调整行间距：<a href="javascript:void(0)" onclick="printConfig.setLineHeight(true)">+加大</a>
                            <a href="javascript:void(0)" onclick="printConfig.setLineHeight(false)">-减小</a>
                        </td>
                        <td width="154" align="center">
                            字体大小：<a href="javascript:void(0)" onclick="printConfig.setFontSize(true)">+加大</a>
                            <a href="javascript:void(0)" onclick="printConfig.setFontSize(false)">-减小</a>
                        </td>
                        <td width="173" align="right">
                            <a title="自动设置 A4 纸张、打印时出现表格、自动设置页眉和页角为空,不出现网址。" href="<%=EyouSoft.Common.Domain.ServerComponents %>/PrintTemplate/printSetup.zip">
                                打印控件安装</a>
                        </td>
                        <td width="206" align="center" valign="bottom">
                            <a href="javascript:void(0)" onclick="printConfig.printPage()">
                                <img src="<%=ImageServerPath %>/images/zjprint.gif" alt="直接打印" width="80" height="22"
                                    border="0" /></a>
                            <asp:ImageButton ID="imgbtnToWord" OnClick="btnWordPrint_Click" Width="80" Height="22"
                                runat="server" />
                        </td>
                        <td width="79" align="right">
                            <a href="#" style="display: none;">打印预览</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="940" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
            <td width="200" rowspan="3" align="right" valign="top">
                <table id="tbl_Left" width="83%" border="0" align="right" cellpadding="0" cellspacing="0"
                    style="margin-top: 90px;">
                    <tr>
                        <td align="left" style="color: #999999;">
                            勾选复选框隐藏指定项目<br />
                            打勾为隐藏，不打勾为显示
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="color: #999999;">
                            <input type="checkbox" name="checkbox" value="checkbox" onclick="TourInfoPrintPage.hide(this,'tr_CollectionContect')"
                                id="cbkContect" /><label for="cbkContect">集合/接团方式</label><br />
                            <input name="checkbox" type="checkbox" value="checkbox" onclick="TourInfoPrintPage.hide(this,'tblLocalCompanyInfo')"
                                id="cbkLocalCompany" /><label for="cbkLocalCompany">地接社信息</label><br />
                            <input type="checkbox" name="checkbox" value="checkbox" onclick="TourInfoPrintPage.hide(this,'tblTourPriceDetail')"
                                id="cbkPriceInfo" />
                            <label for="cbkPriceInfo">
                                报价信息</label><br />
                            <input type="checkbox" name="checkbox2" value="checkbox" onclick="TourInfoPrintPage.hide(this,'tr_ServiceStandard')"
                                id="cbkServiceStandard" /><label for="cbkServiceStandard">服务标准及说明
                                    <br />
                                    <input type="checkbox" name="checkbox2" value="checkbox" onclick="TourInfoPrintPage.hide(this,'tr_Content')"
                                        id="cbkContent" /><label for="cbkContent">
                                            包含项目</label>
                                    <br />
                                    <input type="checkbox" name="checkbox25" value="checkbox" onclick="TourInfoPrintPage.hide(this,'tr_NotContainService')"
                                        id="cbkNotContainService" /><label for="cbkNotContainService">
                                            其它说明</label>
                                    <br />
                                    <input type="checkbox" name="checkbox2" value="checkbox" onclick="TourInfoPrintPage.hide(this,'tblSpeciallyNotice')"
                                        id="cbkSpeciallyNotice" /><label for="cbkSpeciallyNotice">
                                            备注</label>
                                    <br />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="740">
                <table id="tbltopright" width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="2%" align="left">
                            <img src="<%=ImageServerPath %>/images/printboxt-l.gif" width="16" height="37" />
                        </td>
                        <td width="98%" background="<%=ImageServerPath %>/images/printboxt-m.gif">
                            &nbsp;
                        </td>
                        <%-- <td width="1%" align="right">
                            <img src="<%=ImageServerPath %>/images/printboxt-r.gif" width="6" height="37" />
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="printPage" style="border-left: 1px solid #C9C9C9; border-bottom: 1px solid #C9C9C9;
                font-size: 12px; border-right: 5px solid #808080; padding-bottom: 10px;">
                <div id="div_printTop">
                    <table width="720" runat="server" id="tbl_Header" border="0" align="center" cellpadding="0" cellspacing="0" style="font-size: 12px;">
                        <tr>
                            <td height="35" align="center">
                                <span class="headertitle">
                                    <%=CompanyName %></span>许可证号：<%=License %>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                联系人：<%=TourContact%>
                                电话：<%=TourContactTel %>
                                地址：<%=CompanyAddress %>
                            </td>
                        </tr>
                    </table>
                    <table id="tblStandardOtherInfo" width="720" border="1" align="center" cellpadding="1"
                        cellspacing="0" bordercolor="#000000">
                        <tr>
                            <td colspan="4" align="center">
                                <strong style="font-size: 14px;">
                                    <%=RouteName%></strong> 天数：<%=TourDays%>天
                            </td>
                        </tr>
                        <%if (isStadardType)
                          {%>
                        <%if (Traffic != "")
                          { %>
                        <tr>
                            <td style="width: 75px; vertical-align: top; border-right: 0px;">
                                <strong>交通安排：</strong>
                            </td>
                            <td colspan="3" align="left" style="border-left: 0px; width: 650px;">
                                <%=Traffic%>
                            </td>
                        </tr>
                        <%} if (CollectionContect != "" || MeetTourContect != "")
                          { %>
                        <tr id="tr_CollectionContect">
                            <td colspan="4">
                                <table width="99%">
                                    <tr>
                                        <td style="width: 75px; vertical-align: top; border-right: 0px;">
                                            <strong>集合方式：</strong>
                                        </td>
                                        <td align="left" valign="top" style="border-left: 0px;">
                                            <%if (CollectionContect != "")
                                              {%>
                                            <%=CollectionContect%>
                                            <%} %>
                                        </td>
                                        <td style="width: 75px; vertical-align: top; border-right: 0px;">
                                            <strong>接团方式：</strong>
                                        </td>
                                        <td align="left" valign="top" style="border-left: 0px;">
                                            <%if (MeetTourContect != "")
                                              {%>
                                            <%=EyouSoft.Common.Function.StringValidate.TextToHtml(MeetTourContect)%>
                                            <%} %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%}
                          }%>
                    </table>
                    <%if (HasLocalCompanyInfo && isStadardType)
                      { %>
                    <table id="tblLocalCompanyInfo" width="720" border="1" align="center" cellpadding="1"
                        cellspacing="0" bordercolor="#000000" style="border-top: 0px;">
                        <tr>
                            <td colspan="2" align="left" width="75px" style="border-top: 0px; border-right: none;">
                                <strong>地接社：</strong>
                            </td>
                            <td style="border: 0px; border-left: none; width: 645px;">
                                <table style="display: inline; width: auto;">
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
                    <%}
                      %>
                </div>
                <table id="tblTourPriceDetail" width="720" border="0" align="center" cellspacing="1"
                    bgcolor="#FFFFFF">
                    <asp:Repeater runat="server" ID="rptTourPriceDetail">
                        <ItemTemplate>
                            <tr>
                                <td align="left" bgcolor="#eeeeee" style="width: 70px;">
                                    <%#Eval("PriceStandName")%>：
                                </td>
                                <td align="left" bgcolor="#eeeeee">
                                    成人价：
                                    <input name="txtPeoplePrice" type="text" value="<%#Eval("AdultPrice2","{0:F0}") %>"
                                        id="txtPeoplePrice" class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                                <td align="left" bgcolor="#eeeeee">
                                    儿童价：
                                    <input name="txtChildPrice" type="text" value="<%#Eval("ChildrenPrice2","{0:F0}") %>"
                                        class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                                <td align="left" bgcolor="#eeeeee">
                                    单房差：
                                    <input name="txtChildPrice" type="text" value="<%#Eval("SingleRoom2","{0:F0}") %>"
                                        class="bottow_side2" style="width: 45px;" />
                                    元／人
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div id="div_printFooter">
                    <%if (isStadardType)
                      {%>
                    <table width="720" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" bgcolor="#00D9D9" style="border: 1px solid #000000; width: 70px;
                                            border-bottom: 0px;">
                                            <strong>日程</strong>
                                        </td>
                                        <td align="center" bgcolor="#00D9D9" style="border: 1px solid #000000; border-bottom: 0px;
                                            width: 650px;">
                                            <strong>行程安排</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border: 0px; margin: 0px; padding: 0px;">
                                <table width="720" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000">
                                    <asp:Repeater runat="server" ID="rptStandardPlan" OnItemDataBound="rptStandardPlan_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td rowspan="2" align="center" bgcolor="#E8FFFF" style="width: 70px;">
                                                    <strong>第<%#Eval("PlanDay")%>天<br />
                                                        (<asp:Literal ID="ltrWeekDay" runat="server"></asp:Literal>)</strong><br />
                                                    <asp:Literal ID="ltrPlanDate" runat="server"></asp:Literal>
                                                </td>
                                                <td align="left" valign="bottom" bgcolor="#E8FFFF">
                                                    <img src="<%=ImageServerPath %>/images/xing.gif" width="8" height="11" />
                                                    行：<%#Eval("PlanInterval")%>
                                                    <img src="<%=ImageServerPath %>/images/zhu.gif" width="15" height="11" />
                                                    住：<%#Eval("House")%>
                                                    <img src="<%=ImageServerPath %>/images/chi.gif" width="10" height="11" />
                                                    餐：<%#Eval("Dinner")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("PlanContent").ToString())%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="720" id="Table1" border="0" align="center" cellpadding="1" cellspacing="0">
                        <tr id="tr_ServiceStandard">
                            <td bgcolor="#F6E5A9" style="border: 0px; height: 20px;" colspan="2">
                                <strong>&nbsp;服务标准及说明 </strong>
                            </td>
                        </tr>
                    </table>
                    <asp:Repeater runat="server" ID="rptServiceStandard">
                        <ItemTemplate>
                            <table width="720" id="tblServiceStandard" border="0" align="center" cellpadding="1"
                                cellspacing="0">
                                <tr id="tr_Content">
                                    <td align="right" bgcolor="#FCF7E4" style="border: 1px solid #000000; width: 70px;">
                                        包含项目：
                                    </td>
                                    <td align="left" style="border: 1px solid #000000; word-wrap: break-word; overflow: hidden;
                                        width: 650px;">
                                        住宿：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("ResideContent").ToString())%><br />
                                        用餐：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("DinnerContent").ToString())%><br />
                                        景点：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("SightContent").ToString())%><br />
                                        用车：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("CarContent").ToString())%><br />
                                        导游：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("GuideContent").ToString())%><br />
                                        往返交通：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("TrafficContent").ToString())%><br />
                                        其它：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("IncludeOtherContent").ToString())%><br />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%if (NotContainService != "")
                      {%>
                    <table width="720" id="tblServiceStandard" border="0" align="center" cellpadding="1">
                        <tr id="tr_NotContainService">
                            <td align="right" bgcolor="#FCF7E4" style="border: 1px solid #000000; width: 70px;">
                                其它说明：
                            </td>
                            <td align="left" style="border: 1px solid #000000; width: 650px; word-wrap: break-word;
                                overflow: hidden;">
                                <%=NotContainService%>
                            </td>
                        </tr>
                    </table>
                    <%} if (SpeciallyNotice != "")
                      {%>
                    <table width="720" id="tblSpeciallyNotice" border="0" align="center" cellpadding="1"
                        cellspacing="0" bordercolor="#000000">
                        <tr>
                            <td bgcolor="#F6E5A9" style="border: 0px; border-top: 1px solid #000000; width: 70px;">
                                <p>
                                    <strong>&nbsp;备注 </strong>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border: 1px solid #000000; width: 720px; word-wrap: break-word;
                                overflow: hidden;">
                                <%=SpeciallyNotice%>
                            </td>
                        </tr>
                    </table>
                    <%}
                      } %>
                    <asp:Panel ID="pnlQuickPlan" runat="server" Visible="false">
                        <table width="720" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#000000"
                            style="table-layout: fixed">
                            <tr>
                                <td align="left" style="border-top: none;" bgcolor="#F6E5A9">
                                    <strong>行程信息及相关：</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="word-wrap: break-word; overflow: hidden;">
                                    <%=QuickPlanContent%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tbl_Footer" width="60%" height="40" border="0" align="center" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td width="42%" align="center">
                            <input name="Submit3" type="submit" onclick="printConfig.printPage()" class="baocun_an"
                                value="直接打印">
                        </td>
                        <td width="11%">
                            &nbsp;
                        </td>
                        <td width="47%" align="center">
                            <asp:Button ID="btnToWord" runat="server" Text="导出到word" CssClass="baocun_an" OnClick="btnWordPrint_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="hidPrintFooterHTML" name="hidPrintFooterHTML" type="hidden" />
    <input type="hidden" id="hidPrintTopHTML" name="hidPrintTopHTML" />
    <input type="hidden" id="txtFontSize" name="txtFontSize" value="12" />
    <input type="hidden" id="txtLineHeight" name="txtLineHeight" value="18" />
    <input id="hidCompanyID" name="hidCompanyID" type="hidden" />

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="javascript" type="text/javascript">
        var TourInfoPrintPage = {
            hide: function(obj, containerId) {
                if (obj.checked) {
                    $("#" + containerId).hide()
                } else {
                    $("#" + containerId).show()
                }
            }
        }
        var printConfig = {
            config: { maxFontSize: 16, minFontSize: 10, maxLineHeight: 25, minLineHeight: 15 },
            printPreviewHTML: '',
            //设置字体大小 isIncrease=true 加大 isIncrease=false 减小
            setFontSize: function(isIncrease) {
                var currentFontSize = parseInt($("#txtFontSize").val());
                var toFontSize = isIncrease ? currentFontSize + 1 : currentFontSize - 1;
                if (!isIncrease && toFontSize < this.config.minFontSize) {
                    alert("已调整至最小字体" + this.config.minFontSize + "像素");
                    return;
                }
                if (isIncrease && toFontSize > this.config.maxFontSize) {
                    alert("已调整至最大字体" + this.config.maxFontSize + "像素");
                    return;
                }
                $("#printPage td").css({ 'font-size': toFontSize + 'px' });
                $("#txtFontSize").val(toFontSize);
            },
            //设置行间距 isIncrease=true 加大 isIncrease=false 减小
            setLineHeight: function(isIncrease) {
                var currentLineHeight = parseInt($("#txtLineHeight").val());
                var toLineHeight = isIncrease ? currentLineHeight + 1 : currentLineHeight - 1;

                if (!isIncrease && toLineHeight < this.config.minLineHeight) {
                    alert("已调整至最小行间距" + this.config.minLineHeight + "像素");
                    return;
                }

                if (isIncrease && toLineHeight > this.config.maxLineHeight) {
                    alert("已调整至最大行间距" + this.config.maxLineHeight + "像素");
                    return;
                }

                $("#printPage td").css({ 'line-height': toLineHeight + 'px' });
                $("#txtLineHeight").val(toLineHeight);
            },
            //直接打印
            printPage: function() {
                $("#tbl_top").hide();
                $("#tbl_Left").hide();
                $("#tbl_Footer").hide();
                $("#tbltopright").hide();
                $("body").css("background", "none");
                //border-right: 5px solid #808080;
                $("#printPage").css("border-right", "1px solid #C9C9C9")
                if (window.print != null) {
                    window.print();
                    //还原页面内容
                    window.setTimeout(function() {
                        $("#tbl_top").show();
                        $("#tbl_Left").show();
                        $("#tbl_Footer").show();
                        $("#tbltopright").show();
                        $("#printPage").css("border-right", "5px solid #808080")
                        $("body").css({ "background": "url(<%=ImageServerPath %>/images/lura4.gif)", "background-repeat": "no-repeat", "background-position": " 0px 80px" });
                    }, 1500);
                } else {
                    alert('没有安装打印机');
                }
            },
            getHtml: function() {
                $("#hidPrintTopHTML").val($("#div_printTop").html());
                $("#hidPrintFooterHTML").val($("#div_printFooter").html())
            }
        };
        $(function() {
            $("#<%=imgbtnToWord.ClientID %>,#<%=btnToWord.ClientID %>").click(function() {
                printConfig.getHtml();
            });
        })
    </script>

    </form>
</body>
</html>
