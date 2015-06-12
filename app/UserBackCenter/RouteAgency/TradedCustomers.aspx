<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TradedCustomers.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.TradedCustomers" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:content id="TradedCustomers" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table id="OrderReceivedNavigationTab" width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
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
    <table id="tblTradedCustomers" width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="28" align="left" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
                            共<%=SonAccount%>个子账号
                            <asp:DropDownList runat="server" ID="dplUserList">
                            </asp:DropDownList>
                            <span style="text-align: left; padding-left: 15px;">单位：
                                <input  id="txt_TradedCustomers_BuyCompanyName" value="<%=BuyCompanyName %>" class="shurukuang" type="text" size="8" style="width:300px;vertical-align:middle;" />
                                日期：
                                <input id="txt_TradedCustomers_BeginDate" name="BeginDate" value="<%=ShowBeginDate %>" onfocus="WdatePicker()" class="shurukuang" type="text" style="width:75px;vertical-align:middle;" />
                                至
                                <input id="txt_TradedCustomers_EndDate" name="EndDate" value="<%=ShowEndDate %>" onfocus="WdatePicker()" type="text" class="shurukuang"
                                    style="width:75px; vertical-align:middle;" />
                                <img src="<%=ImageServerUrl %>/images/chaxun.gif" style="cursor:pointer;vertical-align:middle;" onclick="TradedCustomers.queryData(); return false;" width="62" height="21" /></span>
                        </td>
                    </tr>
                </table>
                <table id="tbl_TradedCustomers" width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#B0C7D5">
                    <tr  class="white">
                        <td width="23%" background="<%=ImageServerUrl %>/images/hangbg.gif">
                            <strong>零售商</strong>
                        </td>
                        <td width="9%" background="<%=ImageServerUrl %>/images/hangbg.gif">
                            <strong>联系人</strong>
                        </td>
                        <td width="12%" background="<%=ImageServerUrl %>/images/hangbg.gif">
                            <strong>电话</strong>
                        </td>
                        <td width="16%" background="<%=ImageServerUrl %>/images/hangbg.gif">
                            <strong>预订成功次数</strong>
                        </td>
                        <td width="11%" background="<%=ImageServerUrl %>/images/hangbg.gif">
                            <strong>预订人数</strong>
                        </td>
                        <td width="10%" background="<%=ImageServerUrl %>/images/hangbg.gif">
                            <strong>金额</strong>
                        </td>
                        <td width="9%" background="<%=ImageServerUrl %>/images/hangbg.gif">
                            <strong>留位过期</strong>
                        </td>
                        <td width="10%" background="<%=ImageServerUrl %>/images/hangbg.gif">
                            <strong>不受理</strong>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptTradedCustomers" runat="server">
                        <ItemTemplate>
                            <tr bgcolor="#FFFFFF">
                                <td height="30">
                                    <%#Eval("CompanyName")%>
                                </td>
                                <td>
                                    <%#Eval("ContactName")%>
                                </td>
                                <td>
                                    <%#Eval("ContactTel")%>
                                </td>
                                <td>
                                    
                                      <a href="/RouteAgency/HasTradedTeam.aspx?BuyCompanyID=<%#Eval("CompanyId") %>&OrderState=<%=(int)EyouSoft.Model.TourStructure.OrderState.已成交 %>" target="_blank">累计<%#Eval("OrdainNum")%>次</a>  
                                </td>
                                <td>
                                    累计<%#Eval("OrdainPeopleNum")%>人
                                </td>
                                <td>
                                    <strong><%#Eval("TotalMoney", "{0:F2}")%></strong>
                                </td>
                                <td><a href="/RouteAgency/HasTradedTeam.aspx?BuyCompanyID=<%#Eval("CompanyId") %>&OrderState=<%=(int)EyouSoft.Model.TourStructure.OrderState.留位过期 %>" target="_blank">
                                    累计<%#Eval("SaveSeatExpiredNum")%>次</a>
                                </td>
                                <td>
                                <a href="/RouteAgency/HasTradedTeam.aspx?BuyCompanyID=<%#Eval("CompanyId") %>&OrderState=<%=(int)EyouSoft.Model.TourStructure.OrderState.不受理 %>" target="_blank">
                                    累计<%#Eval("NotAcceptedNum")%>次</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr runat="server" id="NoData" visible="false">
                        <td colspan="8">没有找到相关已成交客户！</td>
                    </tr>
                </table>
                <div id="TradedCustomers_ExportPage" class="F2Back" style="text-align: right;" height="40">
                    <cc2:exportpageinfo id="ExportPageInfo1" currencypagecssclass="RedFnt" linktype="4"
                        runat="server"></cc2:exportpageinfo>
                </div>
            </td>
        </tr>
    </table>
        <script type="text/javascript" language="javascript">        
        var TradedCustomers={
            queryData:function(){
                var queryUrl="/routeagency/TradedCustomers.aspx?"+TradedCustomers.getParam();
                topTab.url(topTab.activeTabIndex,queryUrl);                
                return false;
           },
           getParam:function(){
                var CompanyName=encodeURI($.trim($("#txt_TradedCustomers_BuyCompanyName").val()));                
                var UserID=$("#<%=dplUserList.ClientID %>").val();
                var BeginDate=$("#txt_TradedCustomers_BeginDate").val();
                var EndDate=$("#txt_TradedCustomers_EndDate").val();
                return $.param({UserID:UserID,BuyCompanyName:CompanyName,BeginDate:BeginDate,EndDate:EndDate})
           },
           dialog:function(title,obj,width,height){//弹出窗
                var url=$(obj).attr("dialogUrl");
                Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height,draggable:true,data:null,afterHide:function(){
                    $(obj).val("处理中..")
                }});
            },
            pageInit:function(){  
                //分页控件链接控制
                $("#TradedCustomers_ExportPage a").each(function(){
                    $(this).click(function(){                    
                        topTab.url(topTab.activeTabIndex,$(this).attr("href")+"&"+TradedCustomers.getParam());
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
               $("#btnEditOrder").click(function(){
                   var dataArr={OrderState:"0"}                   
                   Boxy.iframeDialog({title:"处理订单", iframeUrl:"/UserOrder/EditOrder.aspx",width:930,height:400,draggable:true,data:dataArr,afterHide:function(){
                        $("#btnEditOrder").val("处理中..")
                   }});
               });
              $("#tbl_TradedCustomers tr").hover(function(){
                   this.style.backgroundColor="#FFEEC4";
              },function(){
                   this.style.backgroundColor=""
              });
           }        
        }
        $(document).ready(function(){
            TradedCustomers.pageInit();                 
        });
    </script>
 </asp:content>
