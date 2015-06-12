<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="UserBackCenter.UserOrder.OrderHistory" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:content id="OrderHistory" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <table id="OrderHistory_NavigationTab" width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
            margin-bottom: 3px;">
            <tr>
                <td width="15" style="border-bottom: 1px solid #62A8E4">
                    &nbsp;
                </td>
                <td width="105" height="24" background="<%=ImageServerUrl %>/images/weichulidingdanf.gif"
                    align="center">
                    <strong class="shenglanz"><a href="/userorder/ordersreceived.aspx" rel='OrderReceivedTab'>
                        未处理订单</a></strong>
                </td>
                <td width="105" height="24" background="<%=ImageServerUrl %>/images/weichulidingdanf.gif"
                    align="center">
                    <strong class="shenglanz"><a href="/userorder/orderprocessed.aspx" rel='OrderReceivedTab'>
                        已处理订单</a></strong>
                </td>
                <td width="105" height="24" background="<%=ImageServerUrl %>/images/weichulidingdan.gif"
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
    <table id="tblOrderHistory" width="98%" height="31"  border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td background="<%=ImageServerUrl %>/images/chaxunbg.gif" ><table width="100%" border="0" align="left" cellpadding="1" cellspacing="0">
              <tr>
              <td width="20%">
                   子账号筛选:
                    <asp:DropDownList runat="server" ID="dplUserList">
                    </asp:DropDownList>
              </td>
                <td width="72%">团号：
                    <input name="TourCode" id="txt_OrderHistory_TourCode" value="<%=TourCode %>" type="text"  size="8" />
                  线路名称：
                  <input name="RouteName" id="txt_OrderHistory_RouteName" value="<%=RouteName %>" type="text" size="15" />
                  天数：
                  <input name="TourDays" id="txt_OrderHistory_TourDays" style="width:40px;" value="<%=TourDays==0?null:TourDays %>" maxlength="3" type="text"  size="2" />
                  出团日期：<span id="txtLeaveDate">
                    <input name="BeginDate" id="txt_OrderHistory_BeginDate"  style="width:60px;" value="<%=ShowBeginDate %>" onfocus="WdatePicker()"  
                         size="9" />
                    </span>至<span >
                      <input name="EndDate" id="txt_OrderHistory_EndDate" style="width:60px;" value="<%=ShowEndDate %>" onfocus="WdatePicker()"
                         size="9" />
                      </span></td>                     
                <td width="12%" align="left"><img src="<%=ImageServerUrl %>/images/chaxun.gif" id="btnOrderHistoryQuery" style="cursor:pointer;" width="62"  height="21" /></td>
              </tr>
          </table></td>
        </tr>
      </table>
    <table id="tbl_OrderHistory" width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
        margin-bottom: 3px;">
        <asp:Repeater runat="server" ID="rpt_OrderHistory" OnItemDataBound="rpt_OrderHistory_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td height="48" colspan="2" align="left" background="/images/newhang5bg.gif" bgcolor="#E9F2FB"
                        style="border: 1px solid #C7D9EB">
                        &nbsp;<img src="<%=ImageServerUrl %>/images/ttt.gif" width="15" height="16">
                        团号：<%#Eval("TourNo")%>
                        &nbsp;<a href="/PrintPage/TeamInformationPrintPage.aspx?type=1&TourID=<%#Eval("ID") %>" target="_blank"><%#Eval("RouteName")%>【<%#Eval("LeaveDate","{0:MM-dd}")%>出团】</a>
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
                            <asp:Repeater runat="server" ID="rpt_OrderHistoryChild">
                                <ItemTemplate>
                                    <tr>
                                          <td height="30">
                                            <!--零售商-->
                                            <a href="/PrintPage/TourConfirmation.aspx?OrderID=<%#Eval("ID") %>&TourID=<%#Eval("TourID") %>" target="_blank"><%#Eval("BuyCompanyName")%></a>
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
                                         <%#Eval("IssueTime", "{0:MM-dd HH:mm}")%>
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
                                            <input type="button" name="Submit" value="<%#GetSateName((int)Eval("OrderState"),Eval("SaveSeatDate")) %>" dialogUrl="/UserOrder/EditOrder.aspx?OrderID=<%#Eval("ID") %>" onclick="OrderHistory.dialog('处理订单',this,930,500)">
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
    <div id="OrderHistory_ExportPage" class="F2Back"  style="text-align:right;" height="40">
        <cc2:ExportPageInfo ID="ExportPageInfo1"  CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
    </div>   
<script type="text/javascript" language="javascript">        
        var OrderHistory={
            Page:"<%=intPageIndex %>",  
            isGrant:"<%=isGrant %>",            
            queryData:function(){
                var action="Query";
                var queryUrl="/UserOrder/orderhistory.aspx?"+OrderHistory.getParam();            
                topTab.url(topTab.activeTabIndex,queryUrl);
                return false;
            },
            pageInit:function(){  
                //分页控件链接控制
                $("#OrderHistory_ExportPage a").each(function(){
                    $(this).click(function(){                    
                        topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                        return false;
                    })
                });
                $("#OrderHistory_NavigationTab a[rel='OrderReceivedTab']").click(function(i){                                      
                    topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                    return false;  
                });      
                $("#<%=dplUserList.ClientID %>").change(function(){
                    topTab.url(topTab.activeTabIndex,"/userorder/orderhistory.aspx?UserID="+$(this).val());
                })
                $("#btnOrderHistoryQuery").click(function(){
                    OrderHistory.queryData();
                    return false;
                })
                $("#tbl_OrderHistory tr").hover(function(){
                    this.style.backgroundColor="#FFF9E7";
                },function(){
                    this.style.backgroundColor=""
               }); 
           },
           dialog:function(title,obj,width,height){//弹出窗
                if(OrderHistory.isGrant=="False"){
                    alert("对不起，你目前的帐号没有修改权限！");
                    return;
                }
                var url=$(obj).attr("dialogUrl");
                Boxy.iframeDialog({title:title, iframeUrl:url,width:800,height:GetAddOrderHeight(),draggable:true,data:{OrderCallBack:"OrderHistory.getData"}});
           },
           getParam:function(){
                var TourCode=$("#txt_OrderHistory_TourCode").val();
                var BeginDate=$("#txt_OrderHistory_BeginDate").val();
                var EndDate=$("#txt_OrderHistory_EndDate").val();
                var TourDays=$("#txt_OrderHistory_TourDays").val();
                var RouteName=encodeURI($.trim($("#txt_OrderHistory_RouteName").val()));
                return $.param({TourCode:TourCode,BeginDate:BeginDate,EndDate:EndDate,RouteName:RouteName,TourDays:TourDays})       
           },
           getData:function(){
                topTab.url(topTab.activeTabIndex,"/userorder/orderhistory.aspx?Page="+OrderHistory.Page);
           }
        };
        $(document).ready(function(){
            OrderHistory.pageInit()
        });
    </script>
</asp:content>
