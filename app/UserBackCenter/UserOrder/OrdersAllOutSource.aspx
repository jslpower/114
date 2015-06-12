<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdersAllOutSource.aspx.cs"
    Inherits="UserBackCenter.UserOrder.OrdersAllOutSource" %>

<%@ Register Src="../usercontrol/UserOrder/OrderOutSourceSearch.ascx" TagName="OrderOutSourceSearch"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:content id="OrdersAllOutSource" runat="server" contentplaceholderid="ContentPlaceHolder1">
<%@ Register Src="/usercontrol/UserOrder/OrderOutSourceTab.ascx" TagName="OrderOutSourceTab"
    TagPrefix="uc1" %>
     <table id="tblOrdersAllOutSourceQuery" align="center" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td height="30" align="left" bgcolor="#E2F3FC">
                <img src="<%=ImageServerUrl %>/images/ddsearch.gif"
                    height="14" />线路名称
                <input size="18" class="shurukuang" id="txt_OrdersAllOutSource_RouteName" value="<%=RouteName %>" class="ddinput" />
                专线商<asp:DropDownList ID="dplRouteCompany" runat="server" Width="190">
                    <asp:ListItem>请选择</asp:ListItem>
                </asp:DropDownList>
                出团日期<input   style="width:65px;"  value="<%=ShowBeginDate %>" id="txt_OrdersAllOutSource_BeginDate" onfocus="WdatePicker()"
                    class="shurukuang" />-<input   style="width:65px;"  id="txt_OrdersAllOutSource_EndDate" value="<%=ShowEndDate %>" onfocus="WdatePicker()" class="shurukuang" />
                下单日期<input   style="width:65px;"  class="shurukuang" id="txt_OrdersAllOutSource_OrderBeginDate" value="<%=ShowOrderBeginDate %>" onfocus="WdatePicker()"
                    class="ddinput" />-<input  style="width:65px;"  class="shurukuang" id="txt_OrdersAllOutSource_OrderEndDate" value="<%=ShowOrderEndDate %>" onfocus="WdatePicker()" class="ddinput" />
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
                        <uc1:OrderOutSourceTab id="OrderOutSourceTab1" runat="server" />
                        <td width="200">
                            &nbsp;
                        </td>
                        <td width="355">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table width="100%" id="tblOrdersAllOutSource" border="1" align="center" cellpadding="2" cellspacing="0" bordercolor="#BAD9F2"
                    class="wddbarsty">
                    <tr>
                        <th width="22%" height="23" align="center" background="<%=ImageServerUrl %>/images/ddbar1.gif">
                                                        <!--AreaId-->
                            <asp:DropDownList runat="server" id="dplRouteInfo">                               
                            </asp:DropDownList>        
                        </th>                      
                        <th width="11%" align="center" background="<%=ImageServerUrl %>/images/ddbar1.gif">
                            所有订单
                        </th>
                        <th width="11%" align="center" background="<%=ImageServerUrl %>/images/ddbar1.gif">
                            <span style="color: #990000">未处理订单</span>
                        </th>
                        <th width="10%" align="center" background="<%=ImageServerUrl %>/images/ddbar1.gif">
                            已成交订单
                        </th>
                        <th width="10%" align="center" background="<%=ImageServerUrl %>/images/ddbar1.gif">
                            已留位订单
                        </th>
                        <th width="10%" align="center" background="<%=ImageServerUrl %>/images/ddbar1.gif">
                            留位到期订单
                        </th>
                        <th width="15%" align="center" background="<%=ImageServerUrl %>/images/ddbar1.gif">
                            未受理订单
                        </th>
                    </tr>                    
                    <asp:Repeater runat="server" ID="rpt_OrdersAllOutSource">
                        <ItemTemplate>
                            <tr AreaId="<%#Eval("AreaId")%>">
                                <td height="28" align="left">
                                    <%#Eval("AreaName")%>
                                </td>                               
                                <td align="center">
                                 <!--所有订单-->
                                    <strong><a href="/UserOrder/AllOrders.aspx?ContionType=all&AreaId=<%#Eval("AreaId")%>"  rel="toptab"><%#Eval("AllOrderNum")%></a></strong>
                                </td>
                                <td align="center">
                                <!--未处理订单-->
                                    <strong><a href="/UserOrder/AllOrders.aspx?ContionType=untreated&AreaId=<%#Eval("AreaId")%>"  rel="toptab"><span style="color: #990000"><%#Eval("UntreatedNum")%></span></a></strong>
                                </td>
                                <td align="center">
                                   <!--已成交订单-->
                                    <a href="/UserOrder/AllOrders.aspx?ContionType=transaction&AreaId=<%#Eval("AreaId")%>"  rel="toptab"><strong><%#Eval("OrdainNum")%></strong></a>
                                </td>
                                <td align="center">
                                   <!--已留位订单-->
                                    <a href="/UserOrder/AllOrders.aspx?ContionType=reservation&AreaId=<%#Eval("AreaId")%>"  rel="toptab"><strong><%#Eval("SaveSeatNum")%></strong></a>
                                </td>
                                <td align="center">
                                   <!--留位到期订单-->
                                     <a href="/UserOrder/AllOrders.aspx?ContionType=reservationdue&AreaId=<%#Eval("AreaId")%>"  rel="toptab"><strong>
                                     <%#Eval("SaveSeatExpiredNum") %></strong></a>
                                </td>
                                <td align="center">
                                <!--未受理订单-->
                                <a href="/UserOrder/AllOrders.aspx?ContionType=notaccepted&AreaId=<%#Eval("AreaId")%>"  rel="toptab"><strong>
                                    <%#Eval("NotAcceptedNum")%></strong></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>                  
                    <tr runat="server" id="NoData" visible="false">
                        <td colspan="8">
                            暂时没有你订单信息！
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<div id="OrdersAllOutSource_ExportPage" class="F2Back" style="text-align:right;" height="40">
    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
</div>
  <script language="javascript" type="text/javascript">
    var OrdersAllOutSource={
           queryData:function(){
                var queryUrl="/userorder/ordersalloutsource.aspx?"+OrdersAllOutSource.getParam();
                topTab.url(topTab.activeTabIndex,queryUrl);                
                return false;
           },
           pageInit:function(){  
                //分页控件链接控制
                $("#OrdersAllOutSource_ExportPage a").each(function(){
                    $(this).click(function(){                    
                        topTab.url(topTab.activeTabIndex,$(this).attr("href")+"&"+OrdersAllOutSource.getParam());
                        return false;
                    })
                });
                $("#<%=dplRouteInfo.ClientID %>").change(function(){
                    var AreaId=$(this).val();
                    if($.trim(AreaId)!=""){
                        $("#tblOrdersAllOutSource tr:has(td)").each(function(){                        
                            if($(this).attr("AreaId")!=AreaId){
                                $(this).hide();
                            }else{
                                $(this).show();
                            }
                        });
                    }else{
                        $("#tblOrdersAllOutSource tr:has(td)").show();
                    }
                    //topTab.url(topTab.activeTabIndex,"/userorder/ordersalloutsource.aspx?AreaId="+AreaId+"&"+OrdersAllOutSource.getParam());                    
                });
                $("#btnSearch").click(function(){
                    OrdersAllOutSource.queryData();
                }); 
                $("#tblOrdersAllOutSource a[rel='toptab']").click(function(){
                    topTab.url(topTab.activeTabIndex,$(this).attr("href"));      
                    return false;
                });
           },
           mouseovertr:function(o){o.style.backgroundColor="#FFF9E7";},
           mouseouttr:function(o){o.style.backgroundColor=""},
           dialog:function(title,obj,width,height){//弹出窗
                var url=$(obj).attr("dialogUrl");
                Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height,draggable:true,data:null});
           },
           topTab:function(obj){
                topTab.url(topTab.activeTabIndex,"/userorder/allorders.aspx");
                return false;
           },
           getParam:function(){
                var RouteName=encodeURI($.trim($("#txt_OrdersAllOutSource_RouteName").val()));                
                var RouteCompany=$("#<%=dplRouteCompany.ClientID %>").val();
                var BeginDate=$("#txt_OrdersAllOutSource_BeginDate").val();
                var EndDate=$("#txt_OrdersAllOutSource_EndDate").val();
                var OrderBeginDate=$("#txt_OrdersAllOutSource_OrderBeginDate").val();
                var OrderEndDate=$("#txt_OrdersAllOutSource_OrderEndDate").val();
                return $.param({RouteName:RouteName,RouteCompany:RouteCompany,BeginDate:BeginDate,EndDate:EndDate,OrderBeginDate:OrderBeginDate,OrderEndDate:OrderEndDate})
           }          
        }      
        $(document).ready(function(){
            OrdersAllOutSource.pageInit()
        });
    </script>                 
</asp:content>
