<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxOrderSearch.aspx.cs" Inherits="IMFrame.Hotel.AjaxOrderSearch" %>
<%@ Import Namespace="EyouSoft.Common" %>
 <%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

 <td align="left" >
  <span  style="display:none" id="orderNum"><%=recordCountHistory%>|<%=recordCountNow%></span>
 <%=remark %>
 <asp:Repeater runat="server" id="rpt_orderList">
 <ItemTemplate>
		<table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom:1px solid #E2E2E2;">
          <tr>
            <td height="24" colspan="2"><img src="<%=ImageServerUrl %>/IM/images/Hotel/tubiao.gif" width="9" height="9" /> <a href='<%#GetOrderLink(Eval("ResOrderId")) %>' target="_blank" class="he"><%# Eval("HotelName") %></a></td>
          </tr>
          <tr>
            <td width="176" valign="top" class="pand3"><table width="206" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="70"><a target="_blank" href='<%#GetOrderLink(Eval("ResOrderId")) %>' ><img src="<%=ImageServerUrl %>/IM/images/Hotel/tu.gif" class="imgb" /></a></td>
                <td width="136" rowspan="2"><span class="pand3" style="line-height:17px; color:#666666">订单号：<%# Eval("ResOrderId")%><br />
总价：￥<%# Utils.GetMoney((decimal)Eval("TotalAmount")) %><br />
返佣金额：￥<%#Utils.GetMoney(decimal.Round((decimal)Eval("TotalAmount") * (decimal)Eval("CommissionPercent")))%><br />
入住日期：<%#Convert.ToDateTime(Eval("CheckInDate")).ToString("yyyy-MM-dd")%> <br />
离店日期：<%#Convert.ToDateTime(Eval("CheckOutDate")).ToString("yyyy-MM-dd") %></span></td>
              </tr>
              <tr>
                <td height="20" align="center"><font color="#FF3300"><%# Eval("OrderState")%></font></td>
                </tr>
            </table></td>
            <td width="926" valign="top" class="pand3" style="line-height:17px; color:#666666">&nbsp;</td>
          </tr>
        </table>
   </ItemTemplate>
	 </asp:Repeater>
	   <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton"  LinkType="3"  HrefType="JsHref" ></cc1:ExporPageInfoSelect>
		</td>