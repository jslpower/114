<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllOrders.aspx.cs" Inherits="UserBackCenter.UserOrder.AllOrders" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:content id="AllOrders" runat="server" contentplaceholderid="ContentPlaceHolder1">
   <table align="center" id="tblAllOrders" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td height="30" align="left" bgcolor="#E2F3FC">
                <img src="<%=ImageServerUrl %>/images/ddsearch.gif" width="14"
                    height="14" />线路名称:
                <input size="18" class="shurukuang" id="txt_AllOrders_RouteName" value="<%=RouteName %>" class="ddinput" />
                专线商<asp:DropDownList ID="dplRouteCompany" runat="server"  Width="180">
                    <asp:ListItem>请选择</asp:ListItem>
                </asp:DropDownList>
                出团日期<input size="5" value="<%=ShowBeginDate %>" style="width:65px;" id="txt_AllOrders_BeginDate" onfocus="WdatePicker()"
                    class="shurukuang" />-<input size="5"  style="width:65px;"  id="txt_AllOrders_EndDate" value="<%=ShowEndDate %>" onfocus="WdatePicker()" class="shurukuang" />
               下单日期<input size="5"  style="width:65px;"  class="shurukuang" id="txt_AllOrders_OrderBeginDate" value="<%=ShowOrderBeginDate %>" onfocus="WdatePicker()"
                    class="ddinput" />-<input size="5"  style="width:65px;"  class="shurukuang" id="txt_AllOrders_OrderEndDate" value="<%=ShowOrderEndDate %>" onfocus="WdatePicker()" class="ddinput" />
                <input type="button" id="btn_AllOrders_Search"
                    value="搜索"  style="width:50px;"/>
            </td>
        </tr>
    </table>
    <table height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td valign="top">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="margin6"
                    style="background: url(images/listheadbj.gif)">
                    <tr>
                        <td width="150" align="center" valign="bottom">
                            <input type="button" name="Submit3" onclick="AllOrders.topTab(this); return false;"
                                value="返回订单汇总页" />
                        </td>
                        <td width="101" height="26" align="center" valign="bottom" class="<%=ChangeCss("all") %>">
                            <a href="javascript:void(0)" onclick="AllOrders.queryDataByContions('all',this);return false;">所有订单</a>
                        </td>
                        <td width="101" align="center" valign="bottom" class="<%=ChangeCss("untreated") %>">
                            <a href="javascript:void(0)" onclick="AllOrders.queryDataByContions('untreated',this);return false;">
                                未处理订单</a>
                        </td>
                        <td width="101" align="center" valign="bottom" class="<%=ChangeCss("transaction") %>">
                            <a href="javascript:void(0)" onclick="AllOrders.queryDataByContions('transaction',this);return false;">
                                成功交易</a>
                        </td>
                        <td width="101" align="center" valign="bottom" class="<%=ChangeCss("reservation") %>">
                            <a href="javascript:void(0)" onclick="AllOrders.queryDataByContions('reservation',this);return false;">
                                已留位</a>
                        </td>
                        <td width="101" align="center" valign="bottom" class="<%=ChangeCss("reservationdue") %>">
                            <a href="javascript:void(0)" onclick="AllOrders.queryDataByContions('reservationdue',this);return false;">
                                留位到期</a>
                        </td>
                        <td width="101" align="center" valign="bottom" class="<%=ChangeCss("notaccepted") %>">
                            <a href="javascript:void(0)" onclick="AllOrders.queryDataByContions('notaccepted',this);return false;">
                                未受理</a>
                        </td>
                        <td align="left" valign="bottom">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="liststyle">
                    <thead>
                        <tr>
                            <th width="350">
                                详细信息
                            </th>
                            <th width="80">
                                成人价
                            </th>
                            <th width="45">
                                儿童价
                            </th>
                            <th width="42">
                                单房差
                            </th>
                            <th width="55">
                                人数
                            </th>
                            <th width="108">
                                总金额
                            </th>
                            <th width="107">
                                打印
                            </th>
                            <th width="77">
                                专线确认
                            </th>
                            <th width="55">
                                订单详情
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAllOrders" OnItemDataBound="rpt_AllOrders_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <div class="listtitle">
                                            <img src="<%=ImageServerUrl %>/images/ico.gif" width="11" height="11" /><a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("TourID") %>&OrderID=<%#Eval("id") %>"
                                                target="_blank"><%#Eval("RouteName")%></a></div>
                                        <div class="listdata">
                                            团号：<%#Eval("TourNo")%>
                                            &nbsp; 发团时间：<span class="lv">【<%#Eval("LeaveDate", "{0:yyyy-MM-dd}")%>出发】</span>天数：<%#Eval("Tourdays")%></div>
                                        <div class="listcompany">
                                            公司：<%#Eval("BuyCompanyName")%></div>
                                    </td>
                                    <td align="center" class="typeprice">
                                        <span class="price2">
                                            ￥<%#Eval("PersonalPrice", "{0:F2}")%></span>
                                    </td>
                                    <td align="center">
                                        <span class="price2">
                                           ￥<%#Eval("ChildPrice", "{0:F2}")%></span>
                                    </td>
                                    <td align="center">
                                        <span class="price2">
                                            ￥<%#Eval("MarketPrice", "{0:F2}")%></span>
                                    </td>
                                    <td align="center">
                                       
                                            <%#Eval("AdultNumber")%><sup>+<%#Eval("ChildNumber")%></sup>
                                    </td>
                                    <td align="center" bgcolor="#fafafa">
                                        ￥<span class="price2"><%#Eval("SumPrice", "{0:F2}")%></span><br />
                                    </td>
                                    <td align="center">
                                        <a target="_blank" href="/PrintPage/TeamNotice.aspx?TourID=<%#Eval("TourID") %>&OrderID=<%#Eval("id") %>">打印出团通知书</a><br />
                                        <a target="_blank" href="/PrintPage/TourConfirmation.aspx?OrderID=<%#Eval("id") %>&TourID=<%#Eval("TourID") %>">
                                            打印确认单</a>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="ltrOrderState"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <a href="javascript:void(0)" dialogUrl="/UserOrder/EditOrder.aspx?action=OrdersReceived&OrderID=<%#Eval("ID") %>&OrderState=<%#Eval("OrderState")%>" onclick="AllOrders.dialog('订单详情',this,800,400);return false;">
                                            <img src="<%=ImageServerUrl %>/images/ddicodetail.gif" /><br />
                                            查看详情</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr runat="server" id="NoData" visible="false">
                            <td colspan="12" align="center">
                                没有你要找的订单信息！
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="right">
                                汇总：
                            </td>
                            <td align="center" bgcolor="#DBE7EF">
                                ￥<span class="price2"><%=taxBaseAmount.ToString("F2") %></span>
                            </td>
                            <td colspan="3" align="center">
                                &nbsp;
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div id="AllOrders_ExportPage" class="F2Back" style="text-align:right;" height="40">
    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
</div>
            </td>
        </tr>
    </table>
    
<script language="javascript" type="text/javascript">
    var AllOrders={
           isGrant:"<%=isGrant %>",
           Page:"<%=intPageIndex %>",
           ContionType:"<%=ContionType %>",
           AreaId:"<%=AreaId %>",
           queryData:function(){
                var queryUrl="/userorder/allorders.aspx?"+AllOrders.getParam();
                topTab.url(topTab.activeTabIndex,queryUrl);                
                return false;
           },
           queryDataByContions:function(ContionType,obj){//  所有订单 | 未处理订单 | 成功交易 | 已留位 | 留位到期 | 未受理 
               var Contion=ContionType;    
               var QueryTime=$("#select_Time").val();          
               topTab.url(topTab.activeTabIndex,'/userorder/allorders.aspx?ContionType='+Contion+"&QueryTime"+QueryTime);
               return false;       
           },
           pageInit:function(){  
                //分页控件链接控制
                $("#AllOrders_ExportPage a").each(function(){
                    $(this).click(function(){                    
                        topTab.url(topTab.activeTabIndex,$(this).attr("href")+"&"+AllOrders.getParam()+"&ContionType="+AllOrders.ContionType);
                        return false;
                    })
                });
                $("#btn_AllOrders_Search").click(function(){
                    AllOrders.queryData();
                });                    
           },
           dialog:function(title,obj,width,height){//弹出窗
                var url=encodeURI($(obj).attr("dialogUrl"));
                Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height,draggable:true,data:{OrderCallBack:"AllOrders.getData"}});
           },
           topTab:function(obj){
                topTab.url(topTab.activeTabIndex,"/userorder/ordersalloutsource.aspx");
                return false;
           },
           getParam:function(){
                var RouteName=encodeURI($.trim($("#txt_AllOrders_RouteName").val()));                
                var RouteCompany=$("#<%=dplRouteCompany.ClientID %>").val();
                var BeginDate=$("#txt_AllOrders_BeginDate").val();
                var EndDate=$("#txt_AllOrders_EndDate").val();
                var OrderBeginDate=$("#txt_AllOrders_OrderBeginDate").val();
                var OrderEndDate=$("#txt_AllOrders_OrderEndDate").val();
                return $.param({RouteName:RouteName,RouteCompany:RouteCompany,BeginDate:BeginDate,EndDate:EndDate,OrderBeginDate:OrderBeginDate,OrderEndDate:OrderEndDate,AreaId:AllOrders.AreaId})
           },
           getData:function(){
                //空方法防治报错
           }      
        }
        $(document).ready(function(){
            AllOrders.pageInit()
        });
    </script>
</asp:content>
