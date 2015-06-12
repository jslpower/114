<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdersOutSource.aspx.cs"
    Inherits="UserBackCenter.UserOrder.OrdersOutSource" %>
<%@ Register src="../usercontrol/UserOrder/OrderOutSourceSearch.ascx" tagname="OrderOutSourceSearch" tagprefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="/usercontrol/UserOrder/OrderOutSourceTab.ascx" TagName="OrderOutSourceTab"
    TagPrefix="uc1" %>
<asp:content id="OrdersOutSource" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <table align="center" id="tblOrdersOutSource" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td height="30" align="left" bgcolor="#E2F3FC">
                <img src="<%=ImageServerUrl %>/images/ddsearch.gif"
                    height="14" />线路名称:
                <input size="18" class="shurukuang" id="txt_OrdersOutSource_RouteName" value="<%=RouteName %>" class="ddinput" />
                专线商<asp:DropDownList ID="dplRouteCompany" runat="server" Width="180">
                    
                </asp:DropDownList>
                出团日期<input size="5" value="<%=ShowBeginDate %>" style="width:65px;" id="txt_OrdersOutSource_BeginDate" onfocus="WdatePicker()"
                    class="shurukuang" />-<input size="5"  style="width:64px;" id="txt_OrdersOutSource_EndDate" value="<%=ShowEndDate %>" onfocus="WdatePicker()" class="shurukuang" />
                下单日期<input size="5" class="shurukuang"  style="width:64px;" id="txt_OrdersOutSource_OrderBeginDate" value="<%=ShowOrderBeginDate %>" onfocus="WdatePicker()"
                    class="ddinput" />-<input size="5" class="shurukuang"  style="width:65px;" id="txt_OrdersOutSource_OrderEndDate" value="<%=ShowOrderEndDate %>" onfocus="WdatePicker()" class="ddinput" />
                <input type="button" id="btnSearch" style="width:50px;"
                    value="搜索" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td>
                <table border="0" align="center" id="OrdersindexNavigationBar" cellpadding="0" cellspacing="0"
                    style="border-bottom: 1px solid #F86A0C; width: 100%;">
                    <tr>
                        <uc1:orderoutsourcetab id="OrderOutSourceTab1" tabindex="1" runat="server" />
                        <td width="200" align="center">
                            &nbsp;
                            <asp:DropDownList ID="dplselect_Time" runat="server">
                                <asp:ListItem value="30">最近30天</asp:ListItem>
                                <asp:ListItem  value="7">最近一周</asp:ListItem>
                                <asp:ListItem value="60">最近60天</asp:ListItem>
                                <asp:ListItem value="183">最近半年</asp:ListItem>
                                <asp:ListItem value="0">所有</asp:ListItem>
                             </asp:DropDownList>
                        </td>
                        <td width="356" align="left">
                            &nbsp; <a href="javascript:void(0)" class="<%=ChangeCss("all") %>" onclick="OrdersOutSource.queryDataByContions('all',this);return false;">
                                全部</a> |<a href="javascript:void(0)" class="<%=ChangeCss("transaction")%>" onclick="OrdersOutSource.queryDataByContions('transaction',this);return false;">
                                    已成交订单</a> | <a href="javascript:void(0)" class="<%=ChangeCss("untreated")%>" onclick="OrdersOutSource.queryDataByContions('untreated',this);return false;">
                                        未处理订单</a> | <a href="javascript:void(0)" class="<%=ChangeCss("reservation")%>" onclick="OrdersOutSource.queryDataByContions('reservation',this);return false;">
                                            已留位订单</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table border="1" id="tbl_OrdersOutSource" align="center" cellpadding="0" cellspacing="1" bordercolor="#D7D7DD"
                    style="width: 100%;">
                    <tr>
                        <td width="16%" height="27" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>交易信息</strong>
                        </td>
                        <td width="26%" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>详细信息</strong>
                        </td>                       
                        <td width="6%" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>成人价</strong>
                        </td>
                        <td width="6%" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>儿童价 </strong>
                        </td>
                        <td width="6%" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>单房差</strong>
                        </td>
                        <td width="5%" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>人数</strong>
                        </td>
                        <td width="7%" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>总金额</strong>
                        </td>
                        <td width="12%" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>打印</strong>
                        </td>
                        <td width="8%" align="center" background="<%=ImageServerUrl %>/images/barcjbj.gif">
                            <strong>订单详情</strong>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt_OrdersOutSource">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <!--交易信息-->
                                    <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>[<%#Eval("OrderState")%>]<br />
                                    <%#Eval("TourCompanyName")%>
                                </td>
                                <td align="left">
                                    <!--详细信息-->
                                    <a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("TourId") %>&OrderID=<%#Eval("id") %>" target="_blank"><%#Eval("RouteName")%></a>
                                </td>                               
                                <td align="center">
                                    <!--成人价-->
                                    <span class="price2">￥<%#Eval("PersonalPrice", "{0:F2}")%></span>
                                </td>
                                <td align="center">
                                    <!--儿童价-->
                                    <span class="price2">￥<%#Eval("ChildPrice", "{0:F2}")%></span>
                                </td>
                                <td align="center">
                                    <!--单房差-->
                                    <span class="price2">￥<%#Eval("MarketPrice", "{0:F2}")%></span>
                                </td>
                                <td align="center">
                                    <!--人数-->
                                   
                                        <%#Eval("PeopleNumber")%>
                                </td>
                                <td align="center">
                                    <!--总金额-->
                                    ￥<%#Eval("SumPrice", "{0:F2}")%>
                                </td>
                                <td align="center">
                                    <!--打印-->
                                    <a target="_blank" href="/PrintPage/TeamNotice.aspx?OrderID=<%#Eval("id") %>&TourID=<%#Eval("TourID") %>">打印出团通知书</a><br />
                                   <a target="_blank" href="/PrintPage/TourConfirmation.aspx?OrderID=<%#Eval("id") %>&TourID=<%#Eval("TourID") %>">打印确认单</a>                                 
                                </td>
                                <td align="center">
                                    <!--查看详情-->
                                    <a href="javascript:void(0)" dialogUrl="/UserOrder/EditOrder.aspx?action=OrdersReceived&OrderID=<%#Eval("ID") %>&OrderState=<%#Eval("OrderState")%>" onclick="OrdersOutSource.dialog('处理订单',this,800,400); return false;">
                                        查看详情</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr runat="server" id="NoData" visible="false">
                        <td colspan="10">
                            暂时没有你订单信息！
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<div id="OrdersOutSource_ExportPage" class="F2Back" style="text-align:right;">
    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
</div>

    <script language="javascript" type="text/javascript">
    var OrdersOutSource={
           ContionType:"<%=ContionType %>",
           queryData:function(){
                var queryUrl="/userorder/ordersoutsource.aspx?"+OrdersOutSource.getParam();
                topTab.url(topTab.activeTabIndex,queryUrl);
                return false;
           },
           queryDataByContions:function(ContionType,obj){//  全部 | 已成交订单 | 未处理订单 | 已留位订单 
               var Contion=ContionType;    
               var QueryTime=$("#<%=dplselect_Time.ClientID %>").val();          
               topTab.url(topTab.activeTabIndex,'/userorder/ordersoutsource.aspx?ContionType='+Contion+"&QueryTime="+QueryTime);
               return false;       
           },
           pageInit:function(){  
                //分页控件链接控制
                $("#OrdersOutSource_ExportPage a").each(function(){
                    $(this).click(function(){                    
                        topTab.url(topTab.activeTabIndex,$(this).attr("href")+"&"+OrdersOutSource.getParam()+"&ContionType="+OrdersOutSource.ContionType);
                        return false;
                    })
                });
                $("#<%=dplselect_Time.ClientID %>").change(function(){
                    topTab.url(topTab.activeTabIndex,"/userorder/ordersoutsource.aspx?QueryTime="+$(this).val());
                    return false;
                });
                $("#btnSearch").click(function(){
                    OrdersOutSource.queryData();
                });     
                $("#tbl_OrdersOutSource tr").hover(function(){
                    this.style.backgroundColor="#FFF9E7";
                },function(){
                    this.style.backgroundColor=""
                });
           },
           dialog:function(title,obj,width,height){//弹出窗
                var url=encodeURI($(obj).attr("dialogUrl"));
                Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height,draggable:true,data:{OrderCallBack:"OrdersOutSource.getData"}});
           },
           getParam:function(){
                var RouteName=encodeURI($.trim($("#txt_OrdersOutSource_RouteName").val()));                
                var RouteCompany=$("#<%=dplRouteCompany.ClientID %>").val();
                var BeginDate=$("#txt_OrdersOutSource_BeginDate").val();
                var EndDate=$("#txt_OrdersOutSource_EndDate").val();
                var OrderBeginDate=$("#txt_OrdersOutSource_OrderBeginDate").val();
                var OrderEndDate=$("#txt_OrdersOutSource_OrderEndDate").val();
                return $.param({RouteName:RouteName,RouteCompany:RouteCompany,BeginDate:BeginDate,EndDate:EndDate,OrderBeginDate:OrderBeginDate,OrderEndDate:OrderEndDate})
           },
           getData:function(){
                //空方法防治报错
           }  
        }
        $(document).ready(function(){
            OrdersOutSource.pageInit()
        });
    </script>

     
</asp:content>
