<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupOrderShow.aspx.cs" Inherits="UserBackCenter.HotelCenter.HotelOrderManage.GroupOrderShow" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>

<asp:Content id="HotelOrderList" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div class="right">
       <div class="tablebox"><!--添加信息表格-->
<table cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center" style="width:100%;">
           <tbody><tr>
             <td valign="top" align="left" colspan="2"><strong>查询我的订单</strong></td>
           </tr>
           <tr>
             <td width="16%" bgcolor="#f2f9fe" align="right">入住时间：</td>
             <td align="left"><asp:Label runat="server" ID="lbStartTime"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">城市：</td>
             <td align="left"><asp:Label runat="server" ID="lbCityName"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">地址位置要求：</td>
             <td align="left"><asp:Label runat="server" ID="lbPlace"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">是否有指定酒店：</td>
             <td align="left"><asp:Label runat="server" ID="lbIsPointHotel"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">指定酒店名称：</td>
             <td align="left"><asp:Label runat="server" ID="lbHotelName"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">星级要求：</td>
             <td align="left"><asp:Label runat="server" ID="lbStar"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">房型要求：</td>
             <td align="left"><asp:Label runat="server" ID="lbRoom"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">房间数量：</td>
             <td align="left"><asp:Label runat="server" ID="lbRoomCount"></asp:Label>
      间</td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">人数：</td>
             <td align="left"><asp:Label runat="server" ID="lbPeopleCount"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">团房预算：</td>
             <td align="left"><asp:Label runat="server" ID="lbGroupPlan"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">订单联系人：</td>
             <td align="left"><asp:Label runat="server" ID="lbContact"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">宾客类型：</td>
             <td align="left"><asp:Label runat="server" ID="lbPeopleType"></asp:Label></td>
           </tr>

           <tr>
             <td bgcolor="#f2f9fe" align="right">团队类型：</td>
             <td align="left"><asp:Label runat="server" ID="lbGroupType"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#f2f9fe" align="right">其他要求：</td>
             <td align="left"><asp:Label runat="server" ID="lbOther"></asp:Label>             </td>
           </tr>
           <tr>
             <td align="center" colspan="2"><a class="baocun_btn" href="javascript:void(0);" onclick="topTab.url(topTab.activeTabIndex,'/HotelCenter/HotelOrderManage/GroupOrderList.aspx')">返 回</a></td>
           </tr>
         </tbody></table>
         <table width="100%" border="0">
  <tbody><tr></tr>
</tbody></table>

       </div>
     </div>
</asp:Content>