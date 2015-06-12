<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="TeamNotice.aspx.cs"
    Inherits="UserBackCenter.PrintPage.TeamNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>出团通知单_同业114</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("TeamNotice") %>" rel="Stylesheet"
        type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style2
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="tbl_top" width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 10px;">
        <tr>
            <td height="31" valign="bottom" bgcolor="#f5f5f5" style="border-bottom: 1px solid #DCAE30;">
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
                                <img src="<%=ImageServerUrl %>/images/zjprint.gif" alt="直接打印" width="80" height="22"
                                    border="0" /></a>
                            <asp:ImageButton ID="imgbtnToWord" OnClick="btnWord_Click" Width="80" Height="22"
                                runat="server" />
                        </td>
                        <td width="79" align="right">
                            <a href="javascript:void(0)" style="display: none;">打印预览</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="920" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
            <td width="200" align="right" valign="top">
                <table width="83%" id="tbl_Left" border="0" align="right" cellpadding="0" cellspacing="0"
                    style="margin-top: 40px;">
                    <tr>
                        <td align="left" style="color: #999999;">
                            勾选复选框隐藏指定项目<br />
                            打勾为隐藏，不打勾为显示
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="color: #999999;">
                            <input type="checkbox" name="checkbox" onclick="meetAndCollectionContect(this)" />集合/接团方式<br />
                            <input name="checkbox" type="checkbox" onclick="localInfo(this)" />地接社信息<br />
                            <%-- <input type="checkbox" name="checkbox" onclick="checkbox" />报价信息<br />--%>
                            <input type="checkbox" name="checkbox2" onclick="serviceInfo(this)" />服务标准及说明
                            <br />
                            <input type="checkbox" name="checkbox2" onclick="IncludeContent(this)" />包含项目
                            <br />
                            <input type="checkbox" name="checkbox25" onclick="NotContainService(this)" />其它说明
                            <br />
                            <input type="checkbox" name="checkbox25" onclick="SpeciallyNotice(this)" />备注
                        </td>
                    </tr>
                </table>
            </td>
            <td width="720">
                <div id="printPage">
                    <table width="720" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="bottom">
                                <strong><span class="headertitle">
                                    <%=CompanyName%></span>许可证号:<%=License%></strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: 3px solid #ff0000;">
                                <span style="font-size: 12px; line-height: 15px;"><strong>地址：</strong><%=CompanyAddress%><strong>
                                    联系人：</strong><%=TourContact%><span style="font-size: 12px; line-height: 15px;"><strong>
                                        电话：</strong><%=TourContactTel%></span></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                        </tr>
                    </table>
                    <table width="720" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="25" align="center" class="cttitle">
                                《出团通知书》
                            </td>
                        </tr>
                    </table>
                    <table width="720" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;各位尊敬的团友您们好，感谢对我们的信任与支持，现将本次旅游的行程内容、接待标准、注意事项等内容明确如下，如无异意请在下列签名栏签字。
                            </td>
                        </tr>
                    </table>
                    <table width="720" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#000000"
                        style="border: 1px solid #000000;">
                        <tr>
                            <td style="width: 70px;">
                                <strong>线路名称：</strong>
                            </td>
                            <td >
                                <span class="bottow_side">
                                    <%=RouteName%></span>
                            </td>
                            <td>
                                <strong>天&nbsp;&nbsp;&nbsp;&nbsp;数：</strong><span class="bottow_side">&nbsp;<%=TourDays%>&nbsp;</span>
                            </td>
                        </tr>
                        <tr runat="server" id="Tr_Traffic">
                            <td style="width: 70px; vertical-align:top;">
                                <strong>交通安排：</strong>
                            </td>
                            <td colspan="2" style="width: 650px;">
                                <asp:Literal ID="ltrTraffic" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr runat="server" id="CollectionContect">
                            <td style="width: 70px;vertical-align:top;">
                                <strong>集合方式：</strong>
                            </td>
                            <td colspan="2" style="width: 650px;">
                                <span class="bottow_side">
                                    <asp:Literal ID="ltrCollectionContect" runat="server"></asp:Literal></span>
                            </td>
                        </tr>
                        <tr runat="server" id="MeetTourContect">
                            <td style="width: 70px;vertical-align:top;">
                                <strong>接团方式：</strong>
                            </td>
                            <td colspan="2" style="width: 650px;">
                                <span class="bottow_side">
                                    <asp:Literal ID="ltrMeetTourContect" runat="server"></asp:Literal></span>
                            </td>
                        </tr>
                        <tr runat="server" id="localInfo">
                            <td style="width: 70px;vertical-align:top;">
                                <strong>地接社：</strong>
                            </td>
                            <td colspan="2" style="vertical-align: top;width: 650px;">
                                <table style="display: inline; width: auto;">
                                    <asp:Repeater runat="server" ID="rptTourLocalityInfo">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("LocalCompanyName")%>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;电话：<%#Eval("ContactTel")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-size: 12px; border-top: 1px solid #000000;">
                                备注：本行程为参考行程，详情请咨询营业中心工作人员。
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlNotQuickPlan" runat="server">
                        <table width="720" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000"
                            style="border-top: none; margin-left: auto; margin-right: auto;">
                            <tr>
                                <td align="center" bgcolor="#00D9D9" style="width: 70px; border:1px solid #000000; border-top:none;">
                                    <strong>日程</strong>
                                </td>
                                <td align="center" bgcolor="#00D9D9" style="width: 650px; border:1px solid #000000;border-top:none;">
                                    <strong>行程安排</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border: 0px;">
                                    <table width="720" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000">
                                        <asp:Repeater runat="server" ID="rptTeamNotice" OnItemDataBound="rptTeamNotice_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td rowspan="2" align="center" bgcolor="#E8FFFF" style="width: 70px;">
                                                        <strong>第<%#Eval("PlanDay")%>天<br />
                                                            (<asp:Literal ID="ltrWeekDay" runat="server"></asp:Literal>)</strong><br />
                                                        <asp:Literal ID="ltrPlanDate" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left" valign="bottom" bgcolor="#E8FFFF">
                                                        <img src="<%=ImageServerUrl %>/images/xing.gif" width="8" height="11" />
                                                        行：<%#Eval("PlanInterval")%>
                                                        <img src="<%=ImageServerUrl %>/images/zhu.gif" width="15" height="11" />
                                                        住：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("House").ToString())%>
                                                        <img src="<%=ImageServerUrl %>/images/chi.gif" width="10" height="11" />
                                                        餐：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("Dinner").ToString())%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="word-wrap: break-word;">
                                                        <!--行程内容-->
                                                        &nbsp;<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("PlanContent").ToString())%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        
                        <table width="720" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#000000">
                             
                             <tr  id="serviceInfo">
                                <td bgcolor="#F6E5A9" colspan="2" >
                                    <p>
                                        <strong>&nbsp;服务标准及说明 </strong>
                                    </p>
                                </td>
                            </tr>          
                             <%if (!string.IsNullOrEmpty(ResideContent) || !string.IsNullOrEmpty(DinnerContent) || !string.IsNullOrEmpty(SightContent) || !string.IsNullOrEmpty(CarContent) || !string.IsNullOrEmpty(GuideContent) || !string.IsNullOrEmpty(TrafficContent) || !string.IsNullOrEmpty(IncludeOtherContent))
                              {%>                 
                            <tr id="IncludeContent">
                                <td align="right" bgcolor="#FCF7E4" style="width:70px;">
                                    包含项目：
                                </td>
                                <td align="left" style="width:650px;word-wrap: break-word; overflow:hidden;">
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
                            <%if(!string.IsNullOrEmpty(NotContainService)) {%>
                            <tr id="NotContainService">
                                <td align="right" bgcolor="#FCF7E4" style="width: 70px;">
                                    其它说明：
                                </td>
                                <td align="left" style="width:650px;word-wrap: break-word; overflow:hidden;">
                                    <%=NotContainService%>
                                </td>
                            </tr>
                            <%}if(!string.IsNullOrEmpty(SpeciallyNotice)){ %>
                            <tr id="SpeciallyNotice">
                                <td align="right" bgcolor="#FCF7E4" style="width: 70px;">
                                    备注：
                                </td>
                                <td align="left" style="width:650px;word-wrap: break-word; overflow:hidden;">
                                    <%=SpeciallyNotice%>
                                </td>
                            </tr>
                            <%} %>
                            <tr>
                                <td colspan="2" align="left" style="line-height: 18px;">
                                    此团为我公司重点照顾，请务必保障接待质量。
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlQuickPlan" runat="server" Visible="false">
                        <table width="720" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#000000"
                            >
                            <tr>
                                <td align="left" style="border-top: none;">
                                    <strong>行程信息及相关：</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="word-wrap: break-word; overflow:hidden;">
                                    <%=QuickPlanContent%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table width="720" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000">
                        <tr>
                            <td height="100" valign="top" width="80" align="left">
                                游客签字：
                            </td>
                            <td>
                                <div style="text-align: right; margin-right: 70px;" id="div_BuyCompany">
                                    <asp:Image ID="imgBuyCompany" Visible="false" runat="server" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="tbl_Footer" width="100%" height="40" border="0" align="center" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td width="33%" align="center">
                            <input name="print" type="button" onclick="printConfig.printPage()" class="baocun_an"
                                value="直接打印" />
                        </td>
                        <td width="33%" align="center">
                            <asp:Button ID="btnToWord" runat="server" Text="导出到word" CssClass="baocun_an" OnClick="btnWord_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnToStamp" runat="server" Text="盖章" CssClass="baocun_an" OnClick="btnToStamp_Click" />
                            <asp:Button ID="btnCancelStamp" runat="server" Visible="false" Text="取消盖章" CssClass="baocun_an"
                                OnClick="btnCancelStamp_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="hidrptPrintHTML" name="rptPrintHTML" type="hidden" />
    <input type="hidden" id="txtFontSize" name="txtFontSize" value="12" />
    <input type="hidden" id="txtLineHeight" name="txtLineHeight" value="18" />
    <input type="hidden" id="txtPrintHTML" name="txtPrintHTML" />
    <input id="hidCompanyID" name="hidCompanyID" type="hidden" value="<%=CompanyID %>" />

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
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
                 $("body").css("background","none");
                
                if (window.print != null) {
                    window.print();  
                    //还原页面内容
                    window.setTimeout(function() {
                        $("#tbl_top").show();  
                        $("#tbl_Left").show();  
                        $("#tbl_Footer").show();
                        $("body").css({"background":"url(<%=ImageServerUrl %>/images/lura4.gif)","background-repeat":"no-repeat","background-position":" 0px 80px"});  
                    }, 800);                 
                } else {
                    alert('没有安装打印机');
                }
            }           
        };
        function getHtml(){                     
            $("#hidrptPrintHTML").val($("#printPage").html());
        }
        function meetAndCollectionContect(obj){
            if(obj.checked){
                 $("#MeetTourContect,#CollectionContect").hide();
            }
            else{
                 $("#MeetTourContect,#CollectionContect").show();
            }
        }
        function localInfo(obj){
            if(obj.checked){
                 $("#localInfo").hide();
            }else{
                 $("#localInfo").show();
            }
        }
        function serviceInfo(obj){
            if(obj.checked){
                 $("#serviceInfo").hide();
            }else{
                 $("#serviceInfo").show();
            }
        } 
        function IncludeContent(obj){
            if(obj.checked){
                 $("#IncludeContent").hide();
            }else{
                 $("#IncludeContent").show();
            }
        }
        function NotContainService(obj){
            if(obj.checked){
                 $("#NotContainService").hide();
            }else{
                 $("#NotContainService").show();
            }
        } 
        function SpeciallyNotice(obj){
            if(obj.checked){
                 $("#SpeciallyNotice").hide();
            }else{
                 $("#SpeciallyNotice").show();
            }
        }          
    </script>

    </form>
</body>
</html>
