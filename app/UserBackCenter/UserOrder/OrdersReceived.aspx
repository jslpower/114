<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdersReceived.aspx.cs"
    Inherits="UserBackCenter.UserOrder.OrdersReceived" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:content id="OrdersReceived" runat="server" contentplaceholderid="ContentPlaceHolder1">   
        <table id="OrderReceivedNavigationTab" width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
            margin-bottom: 3px;">
            <tr>
                <td width="15" style="border-bottom: 1px solid #62A8E4">
                    &nbsp;
                </td>
                <td width="105" height="24" background="<%=ImageServerUrl %>/images/weichulidingdan.gif"
                    align="center">
                    <strong class="shenglanz"><a href="/userorder/ordersreceived.aspx" rel='OrderReceivedTab'>
                        未处理订单</a></strong>
                </td>
                <td width="105" height="24" background="<%=ImageServerUrl %>/images/weichulidingdanf.gif"
                    align="center">
                    <strong class="shenglanz"><a href="/userorder/orderprocessed.aspx" rel='OrderReceivedTab'>
                        已处理订单</a></strong>
                </td>
                <td width="105" height="24" background="<%=ImageServerUrl %>/images/weichulidingdanf.gif"
                    align="center">
                    <strong class="shenglanz"><a href="/userorder/orderhistory.aspx" rel='OrderReceivedTab'>
                        历史团队</a></strong>
                </td>
                <td style="border-bottom: 1px solid #62A8E4" align="right">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="24%" align="center">
                                <a href="/RouteAgency/TradedCustomers.aspx" class="chengse" rel='OrderReceivedTab'>
                                    <img src="<%=ImageServerUrl %>/images/state1.gif" width="7" height="13" style="margin-bottom: -3px" />已成交客户</a>
                            </td>
                            <td width="76%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>       
    <table id="tbl_OrdersReceived" width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
        margin-bottom: 3px;">
        <asp:Repeater runat="server" ID="rpt_OrdersReceived" OnItemDataBound="rpt_OrdersReceived_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td height="48" colspan="2" align="left" background="/images/newhang5bg.gif" bgcolor="#E9F2FB"
                        style="border: 1px solid #C7D9EB">
                        &nbsp;<img src="<%=ImageServerUrl %>/images/ttt.gif" width="15" height="16">
                        团号：<%#Eval("TourNo")%>
                        &nbsp;<a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("ID") %>" target="_blank"><%#Eval("RouteName")%>【<%#Eval("LeaveDate","{0:MM-dd}")%>出团】</a>
                        <br />
                        &nbsp;当前空位<strong class="font"><%#Eval("RealRemnantNumber")%></strong>个；现有 <span class="chengse"><strong><%#Eval("BuyCompanyNumber")%>家</strong></span>
                        零售商共采购 <strong class="chengse"><%#Eval("BuySeatNumber")%></strong> 个位置
                    </td>
                </tr>
                <tr>
                    <td width="5%" align="right" valign="top" bgcolor="#E9F2FB">
                        <img src="<%=ImageServerUrl %>/images/zhexian.gif" alt="查看该线路被预定信息" width="20" height="22">
                    </td>
                    <td width="95%" bgcolor="#E9F2FB" style="padding: 5px;">
                        <table width="100%" border="1" cellpadding="1" cellspacing="0">                            
                            <asp:Repeater runat="server" ID="rpt_OrdersReceivedChild">
                                <HeaderTemplate>
                                      <tr class="white">
                                        <th height="20" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                            <strong>零售商</strong>
                                        </th>
                                        <th width="9%" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                            <strong>联系人</strong>
                                        </th>
                                        <th width="14%"  background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                            <strong>电话</strong>
                                        </th>
                                        <th width="10%"  background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                            <strong>预定时间</strong>
                                        </th>
                                        <th width="6%"  background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                            <strong>人数</strong>
                                        </th>
                                        <th width="9%"  background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                            <strong>金额</strong>
                                        </th>
                                        <th width="16%" background="<%=ImageServerUrl %>/images/hangbgdd.gif">
                                            <strong>操作</strong>
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td height="30">
                                            <!--零售商-->
                                            <a href="/PrintPage/TourConfirmation.aspx?type=1&OrderID=<%#Eval("ID") %>&TourID=<%#Eval("TourID") %>" target="_blank"><%#Eval("BuyCompanyName")%></a>
                                        </td>
                                        <td>
                                            <!--联系人-->
                                            <%#Eval("ContactName")%>
                                        </td>
                                        <td>
                                            <!--电话-->
                                            <%#Eval("ContactTel")%>
                                        </td>
                                        <td>
                                            <!--预定时间-->
                                             <%#Eval("IssueTime","{0:MM-dd HH:mm}")%>
                                        </td>
                                        <td>
                                            <!--人数-->
                                          <a href="/PrintPage/BookingList.aspx?TourID=<%#Eval("TourID") %>&OrderID=<%#Eval("ID") %>" target="_blank"><%#Eval("AdultNumber")%><sup>+<%#Eval("ChildNumber")%></sup></a>  
                                        </td>
                                        <td>
                                            <!--金额-->
                                            <strong><%#Eval("SumPrice","{0:F2}")%></strong>
                                        </td>
                                        <td>
                                            <!--操作-->                                            
                                            <input type="button"name="Submit" value="<%#GetSateName((int)Eval("OrderState"),Eval("SaveSeatDate")) %>" dialogUrl="/UserOrder/EditOrder.aspx?action=OrdersReceived&OrderID=<%#Eval("ID") %>" onclick="OrdersReceived.dialog('处理订单',this,930,500,'<%#Eval("ID") %>')" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <asp:Panel runat="server" id="NoData"  visible="false">
        <div style="text-align:center;">
        暂无未处理订单信息！
    </div>
    </asp:Panel>      
    <div id="OrdersReceived_ExportPage" class="F2Back"  style="text-align:right;" height="40">
        <cc2:ExportPageInfo ID="ExportPageInfo1"  CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
    </div>          

    <script type="text/javascript" language="javascript">        
        var OrdersReceived={
           Page:"<%=intPageIndex %>", 
           isGrant:"<%=isGrant %>", 
           dialog:function(title,obj,width,height,orderId){//弹出窗
                if(OrdersReceived.isGrant=="False"){
                    alert("对不起，你目前的帐号没有修改权限！");
                    return;
                }
                var url=$(obj).attr("dialogUrl");
                Boxy.iframeDialog({title:title,afterHide:function(){
                     $.newAjax({
                        cache:false,
                        url:"/userorder/ordersreceived.aspx?action=OrdersReceived&OrderID="+orderId,                                        
                        success:function(html){
                            topTab.url(topTab.activeTabIndex,"/userorder/ordersreceived.aspx?Page="+OrdersReceived.Page);
                        }
                    });
                },                                                    
                iframeUrl:url,width:800,height:GetAddOrderHeight(),draggable:true,data:{OrderCallBack:"OrdersReceived.getData"}});
            },
            pageInit:function(){  
                //分页控件链接控制
                $("#OrdersReceived_ExportPage a").each(function(){
                    $(this).click(function(){                    
                        topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                        return false;
                    })
                });
                $("#OrderReceivedNavigationTab a[rel='OrderReceivedTab']").click(function(i){                                   
                    topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                    return false;  
                }); 
               $("#tbl_OrdersReceived tr").hover(function(){
                    this.style.backgroundColor="#FFF9E7";
                },function(){
                    this.style.backgroundColor=""
               });                   
           } ,
           getData:function(){            
              //topTab.url(topTab.activeTabIndex,"/userorder/ordersreceived.aspx?Page="+OrdersReceived.Page);
           }       
        }
        $(document).ready(function(){
            OrdersReceived.pageInit();                 
        });
    </script>
</asp:content>
