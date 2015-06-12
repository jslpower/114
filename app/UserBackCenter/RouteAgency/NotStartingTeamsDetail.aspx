<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotStartingTeamsDetail.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.NotStartingTeamsDetail" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<asp:content id="NotStartingTeamsDetail" runat="server" contentplaceholderid="ContentPlaceHolder1">  
    <table id="tblNotStartingTeamsDetail" border="0" cellspacing="0" cellpadding="0" style="width: 99%;">
        <tr>
            <td align="left" valign="top" style="background: #D0E2FA url(<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif) repeat-x top;">
                <table width="100%" id="tbl_Query" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px solid #CCCCCC;">
                    <tr>                       
                        <td width="93%" align="left" background="<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif">                        
                            团号：<input type="text" class="shurukuang" value="<%=TourNumber %>" id="txt_NotStartingTeamsDetail_TourNumber"
                                name="TourNumber" size="20" />
                            收客状态：
                            <asp:DropDownList runat="server" ID="dplSearchTourState" name="TourState">
                               
                            </asp:DropDownList>
                            出团日期：<input class="shurukuang" onfocus="WdatePicker()" style="width: 80px;"
                                value="<%=ShowBeginDate %>" type="text" id="txt_NotStartingTeamsDetail_BeginDate"  name="BeginDate" />
                            至
                            <input  class="shurukuang" onfocus="WdatePicker()" style="width: 80px;"
                                value="<%=ShowEndDate %>" type="text" id="txt_NotStartingTeamsDetail_EndDate"  name="EndDate" />
                            <a href="javascript:void(0)">
                                <input type="image" name="BtnQuery" id="BtnQuery"
                                    src="<%=ImageServerUrl %>/Images/chaxun.gif" style="border-width: 0px; margin-bottom: -3px;" />
                            </a>
                        </td>
                    </tr>
                </table>                
            </td>
        </tr>
        <tr>
            <td background="<%=ImageServerUrl %>/images/gongneng_bg.gif">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #cccccc;
                    width: 100%;">
                    <tr align="left">
                        <td width="80%" background="<%=ImageServerUrl %>/images/gongneng_bg.gif">
                            <span class="guestmenu">收客状态</span> <span class="guestmenu1"><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.changeState(1)"
                                class="keman">客满</a> <a href="javascript:void(0)" onclick="NotStartingTeamsDetail.changeState(2)" class="tings">停收</a> <a href="javascript:void(0)"
                                    class="zhengc" onclick="NotStartingTeamsDetail.changeState(3)">正常</a> </span><span class="guestmenu">团队类型</span> <span class="guestmenu2">
                                        <a href="javascript:void(0)" onclick="NotStartingTeamsDetail.setTourMarkerNote('1'); return false;"  class="state1">
                                            <nobr>推荐精品</nobr>
                                        </a><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.setTourMarkerNote('2'); return false;"  class="state2">
                                            <nobr>促销</nobr>
                                        </a><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.setTourMarkerNote('3'); return false;"  class="state3">
                                            <nobr>最新</nobr>
                                        </a><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.setTourMarkerNote('4'); return false;"  class="state4">
                                            <nobr>品质</nobr>
                                        </a><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.setTourMarkerNote('5'); return false;"  class="state5">
                                            <nobr>纯玩</nobr>
                                        </a><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.saveTourMarkerNote('')"  class="nostate">
                                            <nobr>取消设置</nobr>
                                        </a></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<table width="99%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#D2D1D1"
    class="liststylesp" id="tbl_NotStartingTeamsDetail">
    <tr background="<%=ImageServerUrl %>/Images/detail_list_th.gif" height="23">
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <input name="checkbox" type="checkbox" TourID="" onclick="NotStartingTeamsDetail.CheckAll(this)" />
            <strong>全</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="14%">
            <strong>团号</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
             width="8%">
            <strong>状态</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
             width="7%">
            <strong>出团日期</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <strong>天数</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="7%">
            <strong>成人价</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="7%">
            <strong>儿童价</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="7%">
            <strong>单房差</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>计划</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <strong>实收</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <strong>留位<span class="hot"></span></strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>剩余</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <strong>未处理<span class="font12_grean"></span></strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width=5%">
            <strong>订单<span class="hot"></span></strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>名单</strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>阅</strong>
        </td>
    </tr>
    <asp:repeater runat="server" id="rpt_NotStartingTeamsDetail"  OnItemDataBound="rpt_NotStartingTeamsDetail_ItemDataBound">
            <ItemTemplate>
                <tr class="<%#ReturnCss(Eval("TourState").ToString())%>" >                
                      <td height="24" align="left">
                        <input type="checkbox" name='cbTourId' TourID="<%#Eval("id") %>" ReleaseType="<%#Eval("ReleaseType") %>" />
                        <asp:Literal runat="server" id="ltrXH" ></asp:Literal>
                        <!--序号-->
                    </td>
                    <td align="center">
                        <!--团号-->
                        <a target="_blank" href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("id") %>"><%#Eval("TourNo")%></a>
                    </td>
                    <td align="center" class="linestate">
                        <!--团队推广状态-->                          
                           <asp:Literal runat="server" id="ltrSateName"></asp:Literal>      
                        <span class="hong18" style="position:absolute; display:none; line-height:25px;"><%#Eval("TourSpreadDescription").ToString()!=""?"推广理由："+Eval("TourSpreadDescription"):""%></span>      
                    </td>
                    <td align="center" class="hong18">
                        <!--出团日期-->
                         <%# Eval("LeaveDate", "{0:MM-dd}")%>
                    </td>
                    <td align="center">
                        <!--天数-->
                        <%#Eval("TourDays")%>
                    </td>
                    <td align="center" pepolePrice="pepolePrice">
                        <!--同行成人价-->                        
                        <strong><a
                            style="cursor: pointer"><%#Eval("TravelAdultPrice", "{0:F2}")%></a>
                            <div id="NotStartingTeamsDetail_PriceInfo<%#Container.ItemIndex %>" style='display:none; position: absolute; margin-top: 12px; margin-left: -180px; '
                                    class='kuang1'>
                                    <table width='380' class='font1' border='1' style="background-color: White;" bordercolor='#D0D8E0'>
                                        <asp:Repeater runat="server" ID="rptPriceInfo">
                                            <HeaderTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#57b9f0'" onmouseout="this.style.backgroundColor='#57b9f0'" style="background-color:#57b9f0">
                                                    <td width='20%'  style="background-color:#57b9f0;">
                                                        类别                                                       
                                                    </td>
                                                    <td width='30%' style="background-color:#57b9f0;">
                                                        门市价
                                                    </td>
                                                    <td width='18%' style="background-color:#57b9f0;">
                                                        普通同行
                                                    </td>                       
                                                    <td width='14%' style="background-color:#57b9f0;">
                                                        单房差
                                                    </td>
                                                </tr>
                                                <tr onmouseover="this.style.backgroundColor='#cbe7f7'" style="background-color:#cbe7f7">
                                                    <td width='20%'  style="background-color:#cbe7f7;">
                                                        &nbsp;
                                                    </td>
                                                    <td width='30%'  style="background-color:#cbe7f7;">
                                                        成人/儿童/单房差
                                                    </td>
                                                    <td width='18%'  style="background-color:#cbe7f7;">
                                                        成人/儿童
                                                    </td>
                                                   
                                                    <td width='14%'  style="background-color:#cbe7f7;">
                                                        结算
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style=" font-weight:lighter; color:#ff6600">
                                                     <td style="background-color:#ffffff;">
                                                        <%#Eval("PriceStandName")%>
                                                    </td>
                                                    <td style="background-color:#ffffff;">
                                                        <%#Eval("AdultPrice2", "{0:F2}")%><span class='fonth'>/</span><%#Eval("ChildrenPrice2", "{0:F2}")%><span class='fonth'>/</span><%#Eval("SingleRoom2", "{0:F2}")%>
                                                    </td>
                                                    <td style="background-color:#ffffff;">
                                                        <%#Eval("AdultPrice1", "{0:F2}")%><span class='fonth'>/</span><%#Eval("ChildrenPrice1", "{0:F2}")%>
                                                    </td>                      
                                                    <td style="background-color:#ffffff;">
                                                        <%#Eval("SingleRoom1", "{0:F2}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>                            
                        </strong>
                        
                    </td>
                    <td align="center" pepolePrice="pepolePrice" class="hong18">
                        <!--同行儿童价-->
                       
                        <strong><a 
                            style="cursor:pointer;"> <%#Eval("TravelChildrenPrice", "{0:F2}")%></a>
                           
                        </strong>
                    </td>
                    <td align="center" pepolePrice="pepolePrice">
                        <!--单房差结算价 -->
                       <strong><a style="cursor:pointer;"><%#Eval("RoomDiffSettlementPrice", "{0:F2}")%></a></strong>
                    </td>
                    <td align="center">
                        <!--计划-->
                        <%#Eval("PlanPeopleCount")%>
                    </td>
                    <td align="center">
                        <!--实收-->
                        <strong><%#Eval("CollectAdultNumber")%><sup>+<%#Eval("CollectChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center">
                        <!--留位-->
                       <strong style="color: #3399FF">
                            <%#Eval("AllowanceAdultNumber")%><sup>+<%#Eval("AllowanceChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center" onclick="NotStartingTeamsDetail.surplus(this)">
                        <!--剩余-->
                        <input name="textfield2" type="text"  style="display:none;" RealRemnantNumber="<%#Eval("RealRemnantNumber")%>"   SingleTourID="<%#Eval("id") %>"  class="bitian2" value="<%#Eval("RemnantNumber")%>" />
                        <span class="RedFnt"><%#Eval("RemnantNumber")%></span>
                    </td>
                    <td align="center">
                        <!--未处理-->
                        <strong style="color: #ff0000;">
                            <%#Eval("UntreatedAdultNumber")%><sup>+<%#Eval("UntreatedChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center">
                        <!--订单-->
                        <a href="javascript:void(0)" dialogUrl="/RouteAgency/Booking.aspx?ParentTourID=<%#Eval("ParentTourID") %>&TourID=<%#Eval("id") %>" onclick="NotStartingTeamsDetail.orderDialog('订单预订',this,800,450);return false;"><asp:Literal runat="server" id="ltrOrderDetail"></asp:Literal></a>
                    </td>
                    <td align="center">
                        <!--名单-->
                      <a href="/PrintPage/BookingList.aspx?TourID=<%#Eval("ID") %>" target="_blank">名单</a>
                    </td>
                    <td width="45" align="center">
                        <!--阅-->
                        <a class="fontcolor" href="javascript:void(0)" dialogUrl="/RouteAgency/AccessRecords.aspx?TourID=<%#Eval("ID") %>" onclick="NotStartingTeamsDetail.dialog('浏览次数',this,700,400);return false;"><%#Eval("Clicks")%>次</a>                        
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="<%#ReturnCss(Eval("TourState").ToString())%>" bgcolor='#ffffff'>
                    <td height="24" align="left">
                        <input type="checkbox" name='cbTourId' TourID="<%#Eval("id") %>" ReleaseType="<%#Eval("ReleaseType") %>" />
                        <asp:Literal runat="server" id="ltrXH" ></asp:Literal>
                        <!--序号-->
                    </td>
                    <td align="center">
                        <!--团号-->
                        <a target="_blank" href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("id") %>"><%#Eval("TourNo")%></a>
                    </td>
                    <td align="center" class="linestate">
                        <!--团队推广状态-->                          
                           <asp:Literal runat="server" id="ltrSateName"></asp:Literal>      
                        <span class="hong18" style="position:absolute; display:none; line-height:25px;"><%#Eval("TourSpreadDescription").ToString()!=""?"推广理由："+Eval("TourSpreadDescription"):""%></span>      
                    </td>
                    <td align="center" class="hong18">
                        <!--出团日期-->
                         <%# Eval("LeaveDate", "{0:MM-dd}")%>
                    </td>
                    <td align="center">
                        <!--天数-->
                        <%#Eval("TourDays")%>
                    </td>
                    <td align="center" pepolePrice="pepolePrice" class="hong18">
                        <!--同行成人价-->                        
                        <strong><a
                            style="cursor: pointer"><%#Eval("TravelAdultPrice", "{0:F2}")%></a> 
                            <div id="NotStartingTeamsDetail_PriceInfo<%#Container.ItemIndex %>" style='display:none; position: absolute; margin-top: 12px; margin-left: -180px; background-color:White;'
                                    class='kuang1'>
                                    <table width='380' class='font1' border='1' style="background-color: White;" bordercolor='#D0D8E0'>
                                        <asp:Repeater runat="server" ID="rptPriceInfo">
                                            <HeaderTemplate>
                                            <tr onmouseover="this.style.backgroundColor='#57b9f0'" onmouseout="this.style.backgroundColor='#57b9f0'" style="background-color:#57b9f0">
                                                    <td width='20%' style="background-color:#57b9f0;" >
                                                        类别
                                                    </td>
                                                    <td width='30%' style="background-color:#57b9f0;">
                                                        门市价
                                                    </td>
                                                    <td width='18%' style="background-color:#57b9f0;">
                                                        普通同行
                                                    </td>                       
                                                    <td width='14%' style="background-color:#57b9f0;">
                                                        单房差
                                                    </td>
                                                </tr>
                                                <tr onmouseover="this.style.backgroundColor='#cbe7f7'" style="background-color:#cbe7f7">
                                                    <td width='20%'  style="background-color:#cbe7f7;">
                                                        &nbsp;
                                                    </td>
                                                    <td width='30%'  style="background-color:#cbe7f7;">
                                                        成人/儿童/单房差
                                                    </td>
                                                    <td width='18%'  style="background-color:#cbe7f7;">
                                                        成人/儿童
                                                    </td>
                                                   
                                                    <td width='14%'  style="background-color:#cbe7f7;">
                                                        结算
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style=" font-weight:lighter; color:#ff6600">
                                                     <td style="background-color:#ffffff;">
                                                        <%#Eval("PriceStandName")%>
                                                    </td>
                                                    <td style="background-color:#ffffff;">
                                                        <%#Eval("AdultPrice2", "{0:F2}")%><span class='fonth'>/</span><%#Eval("ChildrenPrice2", "{0:F2}")%><span class='fonth'>/</span><%#Eval("SingleRoom2", "{0:F2}")%>
                                                    </td>
                                                    <td style="background-color:#ffffff;">
                                                        <%#Eval("AdultPrice1", "{0:F2}")%><span class='fonth'>/</span><%#Eval("ChildrenPrice1", "{0:F2}")%>
                                                    </td>                      
                                                    <td style="background-color:#ffffff;">
                                                        <%#Eval("SingleRoom1", "{0:F2}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>                           
                        </strong>
                        
                    </td>
                    <td align="center" pepolePrice="pepolePrice" class="hong18">
                        <!--同行儿童价-->
                       
                        <strong><a 
                            style="cursor:pointer;"> <%#Eval("TravelChildrenPrice", "{0:F2}")%></a>
                           
                        </strong>
                    </td>
                    <td align="center" pepolePrice="pepolePrice">
                        <!--单房差结算价 -->
                       <strong><a style="cursor:pointer;"><%#Eval("RoomDiffSettlementPrice", "{0:F2}")%></a></strong>
                    </td>
                    <td align="center">
                        <!--计划-->
                        <%#Eval("PlanPeopleCount")%>
                    </td>
                    <td align="center">
                        <!--实收-->
                        <strong><%#Eval("CollectAdultNumber")%><sup>+<%#Eval("CollectChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center">
                        <!--留位-->
                       <strong style="color: #3399FF">
                            <%#Eval("AllowanceAdultNumber")%><sup>+<%#Eval("AllowanceChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center" onclick="NotStartingTeamsDetail.surplus(this)">
                        <!--剩余-->
                        <input name="textfield2" type="text"  style="display:none;" RealRemnantNumber="<%#Eval("RealRemnantNumber")%>"   SingleTourID="<%#Eval("id") %>"  class="bitian2" value="<%#Eval("RemnantNumber")%>" />
                        <span class="RedFnt"><%#Eval("RemnantNumber")%></span>
                    </td>
                    <td align="center">
                        <!--未处理-->
                        <strong style="color: #ff0000;">
                            <%#Eval("UntreatedAdultNumber")%><sup>+<%#Eval("UntreatedChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center">
                        <!--订单-->
                        <a href="javascript:void(0)" dialogUrl="/RouteAgency/Booking.aspx?ParentTourID=<%#Eval("ParentTourID") %>&TourID=<%#Eval("id") %>" onclick="NotStartingTeamsDetail.orderDialog('订单预订',this,800,450);return false;"><asp:Literal runat="server" id="ltrOrderDetail"></asp:Literal></a>
                    </td>
                    <td align="center">
                        <!--名单-->
                      <a href="/PrintPage/BookingList.aspx?TourID=<%#Eval("ID") %>" target="_blank">名单</a>
                    </td>
                    <td width="45" align="center">
                        <!--阅-->
                        <a class="fontcolor" href="javascript:void(0)" dialogUrl="/RouteAgency/AccessRecords.aspx?TourID=<%#Eval("ID") %>" onclick="NotStartingTeamsDetail.dialog('浏览次数',this,700,400);return false;"><%#Eval("Clicks")%>次</a>                        
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:repeater>                
         <tr runat="server" id="NoData" visible="false">
            <td colspan="16">
                暂无未出发的团队信息！
            </td>
        </tr>    
    <tr background="<%=ImageServerUrl %>/images/detail_list_th.gif" height="23">
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <input name="checkbox" type="checkbox" TourID="" onclick="NotStartingTeamsDetail.CheckAll(this)" />
            <strong>全</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="14%">
            <strong>团号</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
             width="8%">
            <strong>状态</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
             width="7%">
            <strong>出团日期</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <strong>天数</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="7%">
            <strong>成人价</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="7%">
            <strong>儿童价</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="7%">
            <strong>单房差</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>计划</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <strong>实收</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <strong>留位<span class="hot"></span></strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>剩余</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width="5%">
            <strong>未处理<span class="font12_grean"></span></strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif"
            width=5%">
            <strong>订单<span class="hot"></span></strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>名单</strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>阅</strong>
        </td>
    </tr>
</table>

 <div id="TourSpreadDescription" style="position:absolute; display:none; height:auto; background-color:#ADE3F9; color:#0048a3;"><!--推广理由--></div>
<div id="NotStartingTeamsDetail_ExportPage" class="F2Back" style="text-align:right;" height="40">
    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
</div>

<asp:HiddenField runat="server" ID="hidServerUrl"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hidPageIndex"></asp:HiddenField>
<input id="hid_NotStartingTeamsDetail_TemplateTourID" value="<%=TemplateTourID %>" type="hidden" />
<div style="display: none; width: 120px; border:1px solid gray; position:absolute; background-color:White;" id="divShortcutMenu">
            <input id="hid_TourID" type="hidden" />
            <table width="92%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 3px; border-bottom:1px solid gray;">
                <tbody>
                    <tr class="usedea">
                        <td align="left" class="chekboxtitle">
                            操作快捷方式
                        </td>
                        <td align="left">
                            <img onclick="NotStartingTeamsDetail.menuClose(this)" style="" src="<%=ImageServerUrl %>/Images/tool/close.gif" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="92%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 3px;border-bottom:1px solid gray;">
                <tbody>
                    <tr>
                        <td align="left" class="usedea">
                            <span id="divUpdate"><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.tourUpdate(); return false;">修改</a></span>                            
                             <span id="divDelete">
                                <a href="javascript:void(0)" onclick="NotStartingTeamsDetail.tourDelete(); return false;">删除</a></span>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="92%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 3px;border-bottom:1px solid gray;"
                id="TourMarket">
                <tbody>
                    <tr>
                        <td align="left" class="chekboxtitle">
                            团队类型：
                        </td>
                        <td>
                            <a style="color: rgb(0, 0, 0);" onclick="NotStartingTeamsDetail.saveTourMarkerNote(''); return false;" href="javascript:void(0)">
                                <nobr>取消</nobr>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" class="usedea">
                            <a onclick="NotStartingTeamsDetail.setTourMarkerNote('1'); return false;" href="javascript:void(0)" id="1381">
                                <span style="white-space: nowrap;">推荐</span></a> <a onclick="NotStartingTeamsDetail.setTourMarkerNote('2'); return false;"
                                    href="javascript:void(0)" id="1382"><span style="white-space: nowrap;">促销</span></a>
                            <a onclick="NotStartingTeamsDetail.setTourMarkerNote('3'); return false;" href="javascript:void(0)" id="1383">
                                <span style="white-space: nowrap;">最新</span></a> <a onclick="NotStartingTeamsDetail.setTourMarkerNote('4'); return false;"
                                    href="javascript:void(0)" id="1384"><span style="white-space: nowrap;">品质</span></a>
                            <a onclick="NotStartingTeamsDetail.setTourMarkerNote('5'); return false;" href="javascript:void(0)" id="1385">
                                <span style="white-space: nowrap;">纯玩</span></a>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="92%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 3px;border-bottom:1px solid gray;">
                <tbody>
                    <tr>
                        <td align="left" class="chekboxtitle">
                            收客状态：
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="usedea">
                            <a onclick="NotStartingTeamsDetail.changeState(1)" href="javascript:void(0)" id="2">客满</a> <a onclick="NotStartingTeamsDetail.changeState(2)"
                                href="javascript:void(0)" id="0">停收</a> <a onclick="NotStartingTeamsDetail.changeState(3)" href="javascript:void(0)">
                                    正常</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
        <script type="text/javascript" language="javascript">            
               /* 操作快捷方式变量 TourID:组团编号 ReleaseType,0:标准发布,1:快速发布; */
            var NotStartingTeamsDetailParams={
                TemplateTourID:$("#hid_NotStartingTeamsDetail_TemplateTourID").val(),
                TourID:"",
                ReleaseType:0,
                TourMarkerNote:"",
                Page:"<%=intPageIndex %>", 
                IsGrantUpdate:"<%=IsGrantUpdate %>",
                IsGrantDelete:"<%=IsGrantDelete %>",
                getParam:function(){
                    var TourNumber=$("#txt_NotStartingTeamsDetail_TourNumber").val();
                    var BeginDate=$("#txt_NotStartingTeamsDetail_BeginDate").val();
                    var EndDate=$("#txt_NotStartingTeamsDetail_EndDate").val();
                    var TourState=$("#<%=dplSearchTourState.ClientID %>").val();
                    return $.param({TourNumber:TourNumber,TourState:TourState,BeginDate:BeginDate,EndDate:EndDate,TemplateTourID:NotStartingTeamsDetailParams.TemplateTourID});      
                }
            };          
        </script>
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("NotStartingTeamsDetail") %>" cache="true"></script>
</asp:content>
