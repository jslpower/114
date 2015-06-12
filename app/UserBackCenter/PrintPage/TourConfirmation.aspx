<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="TourConfirmation.aspx.cs"
    Inherits="UserBackCenter.PrintPage.TourConfirmation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单确认单_同业114</title>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("style_1") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("style_2") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("style_3") %>" rel="Stylesheet"
        type="text/css" />
    <style>
        .bottow_no
        {
            border: 0px solid #ffffff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="696" border="0" id="tbl_top" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <%--<input type="checkbox" id="cb_stamp" value="checkbox">
                盖章--%>
                &nbsp;
            </td>
            <td width="270" align="right">
                字体：<a href="javascript:void(0)" onclick="TourConfirmation.changeSheets(3)">大</a>&nbsp;&nbsp;<a
                    href="javascript:void(0)" onclick="TourConfirmation.changeSheets(2)">中</a>&nbsp;&nbsp;<a
                        href="javascript:void(0)" onclick="TourConfirmation.changeSheets(1)">小</a>&nbsp;&nbsp;&nbsp;<img
                            src="<%=ImageServerPath %>/images/dayin2.gif" style="cursor: pointer;" onclick="TourConfirmation.printPage()"
                            width="57" height="19">
                <asp:ImageButton ID="imgbtnToWord" OnClick="btnWord_Click" Width="57" Height="19"
                    runat="server" />
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <div id="TourCodeHtml">
        <table width="696" border="0" align="center" cellpadding="0" style="margin-left: auto;
            margin-right: auto;" cellspacing="0">
            <tr>
                <td align="center">
                    <font style="color: #000000; font-size: 25px; font-weight: bold; line-height: 35px;
                        text-align: center;">关于接待<%=TourCode %>团队散客的确认单</font>
                </td>
            </tr>
        </table>
    </div>
    <table width="696" border="0" id="tblShowTop" align="center" cellpadding="0" cellspacing="0"
        style="margin-left: auto; margin-right: auto;">
        <tr style="display: inline;">
            <td align="left">
                <strong>接收人</strong>
            </td>
            <td>
                <input name="BuyCompanyName" style="width: 200px;" runat="server" type="text" id="txtBuyCompanyName"
                    class="bottow_side" />
            </td>
            <td>
                <strong>姓名</strong>
            </td>
            <td>
                <input name="BuyConnectName" style="width: 100px;" runat="server" type="text" id="txtBuyConnectName"
                    class="bottow_side" />
            </td>
            <td>
                <strong>传真</strong>
            </td>
            <td>
                <textarea rows="2" cols="10" name="BuyConnectFax" style="width: 100px; overflow: hidden"
                    id="txtBuyConnectFax" class="bottow_side" runat="server">
            </textarea>
                <%--<input name="BuyConnectFax" style="width:100px;"  type="text"  class="bottow_side"
                    runat="server" />--%>
            </td>
            <td>
                <strong>电话</strong>
            </td>
            <td>
                <input name="BuyConnectTel" style="width: 100px;" type="text" id="txtBuyConnectTel"
                    class="bottow_side" runat="server" />
            </td>
        </tr>
        <tr style="display: inline;">
            <td align="left">
                <strong>发送人</strong>
            </td>
            <td>
                <input name="SellCompanyName" style="width: 200px;" type="text" id="txtSellCompanyName"
                    class="bottow_side" runat="server" />
            </td>
            <td>
                <strong>姓名</strong>
            </td>
            <td>
                <input name="SellConnectName" style="width: 100px;" type="text" runat="server" id="txtSellConnectName"
                    class="bottow_side" />
            </td>
            <td>
                <strong>传真</strong>
            </td>
            <td>
                <textarea rows="2" cols="10" name="SellConnectFax" style="width: 100px; overflow: hidden"
                    id="txtSellConnectFax" class="bottow_side" runat="server">
            </textarea>
                <%-- <input name="SellConnectFax" style="width:100px;"  runat="server" type="text" id="txtSellConnectFax" class="bottow_side"
                    />--%>
            </td>
            <td>
                <strong>电话</strong>
            </td>
            <td>
                <input name="SellConnectTel" style="width: 100px;" type="text" id="txtSellConnectTel"
                    class="bottow_side" runat="server" />
            </td>
        </tr>
    </table>
    <div id="printPage" style="width: 696px; margin-left: auto; margin-right: auto;">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp;根据贵社的订购要求，客人参加散拼团的行程及服务标准整理如下，请仔细核对相关项目及费用，如果无误，请盖章确认后，回传至我公司，谢谢您的支持！
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000"
            style="table-layout: fixed;">
            <tr>
                <td style="width: 85px;">
                    <strong>线路名称：</strong>
                </td>
                <td>
                    <span class="bottow_side">
                        <%=RouteName%></span>
                </td>
                <td width="125">
                    <strong>天&nbsp;&nbsp;&nbsp;&nbsp;数：</strong><span class="bottow_side">&nbsp;<%=TourDays%>&nbsp;</span>
                </td>
            </tr>
            <tr runat="server" id="Tr_Traffic">
                <td style="width: 85px; vertical-align:top;">
                    <strong>交通安排：</strong>
                </td>
                <td colspan="2" align="left">
                    <asp:Literal ID="ltrTraffic" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr runat="server" id="tr_CollectionContect">
                <td style="width: 85px;">
                    <strong>集合方式：</strong>
                </td>
                <td colspan="2">
                    <span class="bottow_side">
                        <asp:Literal ID="ltrCollectionContect" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr runat="server" id="tr_MeetTourContect">
                <td style="width: 85px;">
                    <strong>接团方式：</strong>
                </td>
                <td colspan="2">
                    <span class="bottow_side">
                        <asp:Literal ID="ltrMeetTourContect" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <strong>订购人数</strong>：<span class="bottow_side"><%=AdultNum%>(成人)+<%=ChildrenNum%>(儿童)</span>
                </td>
            </tr>
        </table>
        <table width="696" border="1" align="center" runat="server" id="tblCustomers" cellpadding="0"
            cellspacing="0" bordercolor="#000000" style="table-layout: fixed">
            <tr>
                <td align="right">
                    <strong>客人信息：</strong>
                </td>
                <td width="600" colspan="5" align="left" style="word-wrap: break-word;">
                    <asp:Repeater runat="server" ID="rptCustomers">
                        <ItemTemplate>
                            客人<%#Container.ItemIndex+1 %>：
                            <%#Eval("VisitorName")%>
                            &nbsp;证件号码:<%#Eval("CradNumber")%>&nbsp;
                            <%#bool.Parse(Eval("VisitorType").ToString())?"成人":"儿童"%>
                            &nbsp;联系方式:<%#Eval("ContactTel")%><br>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <table width="696" height="5" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <div id="pnlNotQuickPlan" runat="server">
            <asp:Repeater runat="server" ID="rptStandardPlan" OnItemDataBound="rptStandardPlan_ItemDataBound">
                <HeaderTemplate>
                    <table width="696" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#000000"
                        style="table-layout: fixed">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <table width="696" border="0" cellspacing="1" cellpadding="0">
                                <tr>
                                    <td width="187">
                                        <strong>第
                                            <%#Eval("PlanDay")%>
                                            天&nbsp;&nbsp;<asp:Literal ID="ltrPlanDate" runat="server"></asp:Literal>（<asp:Literal
                                                ID="ltrWeekDay" runat="server"></asp:Literal>）</strong>
                                    </td>
                                    <td width="200">
                                        <!--行程区间-->
                                        <strong>
                                            <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("PlanInterval").ToString())%></strong>
                                    </td>
                                    <td width="194">
                                        <strong>住：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("House").ToString())%></strong>
                                    </td>
                                    <td width="115">
                                        <strong>餐：<%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("Dinner").ToString())%></strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="word-wrap: break-word;">
                            <!--行程内容-->
                            <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("PlanContent").ToString())%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <%if (!string.IsNullOrEmpty(ResideContent) || !string.IsNullOrEmpty(DinnerContent) || !string.IsNullOrEmpty(SightContent) || !string.IsNullOrEmpty(CarContent) || !string.IsNullOrEmpty(GuideContent) || !string.IsNullOrEmpty(TrafficContent) || !string.IsNullOrEmpty(IncludeOtherContent))
              {%>
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <p>
                            <strong>&nbsp;服务标准及说明 </strong>
                        </p>
                    </td>
                </tr>
            </table>
            <%} %>
            <table width="696" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000"
                style="table-layout: fixed">
                <%if (!string.IsNullOrEmpty(ResideContent) || !string.IsNullOrEmpty(DinnerContent) || !string.IsNullOrEmpty(SightContent) || !string.IsNullOrEmpty(CarContent) || !string.IsNullOrEmpty(GuideContent) || !string.IsNullOrEmpty(TrafficContent) || !string.IsNullOrEmpty(IncludeOtherContent))
                  {%>
                <tr>
                    <td width="93" align="left">
                        包含项目：
                    </td>
                    <td style="word-wrap: break-word;">
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
                <tr>
                    <td>
                        报价总说明：
                    </td>
                    <td>
                        <strong>成人</strong>：<%=AdultPrice %>元/人*<%=AdultNum%>人+<strong>儿童：</strong><%=ChildrenPrice%>元/人*<%=ChildrenNum%>人；<strong>合计：</strong><%=SumPrice%>元
                    </td>
                </tr>
                <%if (!string.IsNullOrEmpty(NotContainService))
                  { %>
                <tr>
                    <td>
                        其它说明：
                    </td>
                    <td style="word-wrap: break-word;">
                        <%=NotContainService%>
                    </td>
                </tr>
                <%} if (!string.IsNullOrEmpty(SpeciallyNotice))
                  { %>
                <tr>
                    <td align="right">
                        备注：
                    </td>
                    <td style="word-wrap: break-word;">
                        <%=SpeciallyNotice%>
                    </td>
                </tr>
                <%} %>
            </table>
        </div>
        <asp:Panel ID="pnlQuickPlan" runat="server" Width="696" Visible="false">
            <table width="696" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000">
                <tr>
                    <td align="left">
                        <strong>行程信息及相关</strong>
                    </td>
                </tr>
                <tr>
                    <td width="600" colspan="5" align="left" style="word-wrap: break-word;">
                        <%=QuickPlanContent%>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <strong>如果您确认无误,请将款打下我公司以下帐号:</strong>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="262">
                    &nbsp;公司全称：<asp:Literal ID="ltrSellCompanyName" runat="server"></asp:Literal>
                </td>
                <td width="210">
                    &nbsp;开户行：<asp:Literal ID="ltrBuyComapnyBankName" runat="server"></asp:Literal>
                </td>
                <td width="216">
                    &nbsp;帐号：<asp:Literal ID="ltrBuyComapnyBankNo" runat="server"></asp:Literal>
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rptBankAccount">
                <ItemTemplate>
                    <tr>
                        <td>
                            &nbsp;户&nbsp;&nbsp;&nbsp;&nbsp;名：<%#Eval("BankAccountName")%>
                        </td>
                        <td>
                            &nbsp;开户行：<%#Eval("BankName")%>
                        </td>
                        <td>
                            &nbsp;帐号：<%#Eval("AccountNumber")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <table width="696" border="0" align="center" cellpadding="15" cellspacing="0">
        <tr>
            <td width="346">
                组团社盖章：
                <input name="CompanyName1" id="txtBuyCompanyStamp" type="text" class="bottow_no"
                    style="border: 0px;" value="<%=BuyCompanyName %>" size="30" />
                <br />
                <div style="position: absolute;" id="div_SellCompany">
                    <asp:Image ID="imgSellCompany" Visible="false" runat="server" />
                </div>
                联系电话：
                <input name="CompanyTel1" type="text" class="bottow_no" style="border: 0px;" value="<%=BuyCompanyTel %>"
                    size="15" />
                <br />
                <a id="posBuyObject"></a>确认日期：
                <input name="CompanyDate1" type="text" class="bottow_no" style="border: 0px;" value="<%=ComfirmTime %>"
                    size="15" />
            </td>
            <td width="350">
                专线商盖章：
                <input name="CompanyName2" id="txtSellCompanyStamp" type="text" class="bottow_no"
                    style="border: 0px;" value="<%=SellCompanyName %>" size="30" />
                <br />
                <div style="position: absolute;" id="div_BuyCompany">
                    <asp:Image ID="imgBuyCompany" Visible="false" runat="server" />
                </div>
                联系电话：
                <input name="CompanyTel2" type="text" class="bottow_no" style="border: 0px;" value="<%=SellCompanyTel %>"
                    size="15" />
                <br />
                <a id="posSellObject"></a>确认日期：
                <input name="CompanyDate2" type="text" class="bottow_no" style="border: 0px;" value="<%=ComfirmTime %>"
                    size="15" />
            </td>
        </tr>
    </table>
    <table width="60%" id="tbl_Footer" style="z-index: 999;" height="40" border="0" align="center"
        cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="height: 40px;">
            </td>
        </tr>
        <tr>
            <td>
                <input id="btnPrint" class="baocun_an" type="button" value="打　印" onclick="TourConfirmation.printPage()" />
            </td>
            <td>
                <asp:Button ID="btnWord" runat="server" Text="word格式打印" CssClass="baocun_an" OnClick="btnWord_Click" />
            </td>
            <td>
                <asp:Button ID="btnToWord" runat="server" Text="导出到word" CssClass="baocun_an" OnClick="btnWord_Click" />
            </td>
            <td>
                <asp:Button ID="btnToStamp" runat="server" CssClass="baocun_an" Text="盖章" OnClientClick="return true;"
                    OnClick="btnToStamp_Click" />
                <asp:Button ID="btnCancelStamp" runat="server" Visible="false" CssClass="baocun_an"
                    Text="取消盖章" OnClientClick="return true;" OnClick="btnCancelStamp_Click" />
            </td>
        </tr>
    </table>
    <input id="hidrptPrintHTML" name="PrintHTML" type="hidden" />
    <input id="hidTourCodeHtml" name="TourCodeHtml" type="hidden" />
    <input id="hidCompanyID" name="hidCompanyID" value="<%=CompanyID %>" type="hidden" />
    <asp:HiddenField ID="hidsignetType" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="JavaScript" type="text/javascript">
       var doAlerts=false;
       $(function(){
            TourConfirmation.pageInit();
       });
        var TourConfirmation={
            getPosition:function(obj) 
            {
                var objPosition={Top:0,Left:0}
                var offset = $(obj).offset();
                objPosition.Left=offset.left;
                objPosition.Top=offset.top;
                return objPosition;
            },
            pageInit:function(){
                TourConfirmation.getStampPos();                
            },
            getStampPos:function(){
                   var BuyPosition=TourConfirmation.getPosition($("#posBuyObject"));
                   $("#div_BuyCompany").css({left:Number(BuyPosition.Left+25)+"px",top:Number(BuyPosition.Top-$("#div_BuyCompany").height()+40)+"px"});               
                   var SellPosition=TourConfirmation.getPosition($("#posSellObject"));                   
                   $("#div_SellCompany").css({left:Number(SellPosition.Left+25)+"px",top:Number(SellPosition.Top-$("#div_SellCompany").height()+40)+"px"});
            },
            changeSheets:function(whichSheet){
                  whichSheet=whichSheet-1;
                  if(document.styleSheets){
                    var c = document.styleSheets.length;
                    if (doAlerts) alert('Change to Style '+(whichSheet+1));
                    for(var i=0;i<c;i++){
                      if(i!=whichSheet){
                        document.styleSheets[i].disabled=true;
                      }else{
                        document.styleSheets[i].disabled=false;
                      }
                    }
                  }
                  TourConfirmation.getStampPos();
            },
            printPage:function(){
                $("#tbl_top").hide();           
                $("#tbl_Footer").hide();
                if (window.print != null) {
                    window.print();
                    setTimeout(function(){
                        $("#tbl_top").show();   
                        $("#tbl_Footer").show(); 
                    },1200);
                } else {
                    alert('没有安装打印机');
                }
            }         
        }
        function getHtml(){   
            $("#hidrptPrintHTML").val($("#printPage").html());
            $("#hidTourCodeHtml").val($("#TourCodeHtml").html());
        }
    </script>

    </form>
</body>
</html>
