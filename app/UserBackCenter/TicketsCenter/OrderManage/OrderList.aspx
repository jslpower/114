<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="UserBackCenter.TicketsCenter.OrderManage.OrderList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="OrderList" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table id="<%=ContainerID %>" width="835" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#7dabd8"
        style="margin-top: 10px;">
        <tr>
            <th width="139" height="30" align="center" bgcolor="#EEF7FF">
                &nbsp;
            </th>
            <th width="139" align="center" bgcolor="#EEF7FF">
                特殊代理费
            </th>
            <th width="139" align="center" bgcolor="#EEF7FF">
                散客票
            </th>
            <th width="139" align="center" bgcolor="#EEF7FF">
                团队/散拼票
            </th>
            <th width="139" align="center" bgcolor="#EEF7FF">
                国际票
            </th>
            <th width="139" align="center" bgcolor="#EEF7FF">
                特价申请
            </th>
        </tr>
        <tr>
            <td height="30" align="center" bgcolor="#FFFFFF">
                待审核（张）
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="0" ratetype="2" Orderstattype="0">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="0" ratetype="1" Orderstattype="0">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="0" ratetype="3" Orderstattype="0" >
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="0" ratetype="5" Orderstattype="0">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="0" ratetype="4" Orderstattype="0">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待审核][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
            </td>
        </tr>
        <tr>
            <td height="30" align="center" bgcolor="#FFFFFF">
                待处理（张）
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="3" ratetype="2" Orderstattype="1">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="3" ratetype="1" Orderstattype="1">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="3" ratetype="3" Orderstattype="1">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="3" ratetype="5" Orderstattype="1">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="3" ratetype="4" Orderstattype="1">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待处理][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
            </td>
        </tr>
        <tr>
            <td height="30" align="center" bgcolor="#FFFFFF">
                待退票（张）
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="2" Orderstattype="2">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="1" Orderstattype="2">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="3" Orderstattype="2">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="5" Orderstattype="2">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="0" ratetype="4" Orderstattype="2">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待退票][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
            </td>
        </tr>
        <tr>
            <td height="30" align="center" bgcolor="#FFFFFF">
                自愿/非自愿（天）
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.特殊代理费]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.特殊代理费]).ToString("F1"))%>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.散客政策]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.散客政策]).ToString("F1"))%>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.团队散拼]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.团队散拼]).ToString("F1"))%>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.国际政策]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.国际政策]).ToString("F1"))%>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.自愿][EyouSoft.Model.TicketStructure.RateType.特价政策]).ToString("F1"))%>/<%=Utils.FilterEndOfTheZeroString(Decimal.Parse(diclist[EyouSoft.Model.TicketStructure.OrderStatType.非自愿][EyouSoft.Model.TicketStructure.RateType.特价政策]).ToString("F1"))%>
            </td>
        </tr>
        <tr>
            <td height="30" align="center" bgcolor="#FFFFFF">
                待作废（张）
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="2" Orderstattype="4">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="1" Orderstattype="4">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="3" Orderstattype="4">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="5" Orderstattype="4">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="1" ratetype="4" Orderstattype="4">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待作废][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
            </td>
        </tr>
        <tr>
            <td height="30" align="center" bgcolor="#FFFFFF">
                待改期（张）
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="2" Orderstattype="5">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="1" Orderstattype="5">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="3" Orderstattype="5">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="5" Orderstattype="5">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="2" ratetype="4" Orderstattype="5">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改期][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
            </td>
        </tr>
        <tr>
            <td height="30" align="center" bgcolor="#FFFFFF">
                待改签（张）
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="2" Orderstattype="6">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.特殊代理费]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="1" Orderstattype="6">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.散客政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="3" Orderstattype="6">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.团队散拼]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="5" Orderstattype="6" >
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.国际政策]%></a>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <a href="javascript:void(0);" orderstate="5" changetype="3" ratetype="4" Orderstattype="6">
                    <%=diclist[EyouSoft.Model.TicketStructure.OrderStatType.待改签][EyouSoft.Model.TicketStructure.RateType.特价政策]%></a>
            </td>
        </tr>
        <tr>
            <td height="30" colspan="6" align="left" bgcolor="#FFFFFF">
                <table width="40%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="6%">
                            &nbsp;
                        </td>
                        <td width="94%">
                            <b><font color="#FF0000">注：点击数字可以直接处理相应订单！</font></b>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<script type="text/javascript">
var OrderList = {
    openOrderList:function(){
        var $obj = $(this);
        var orderstate = $obj.attr("orderstate");
        var changetype = $obj.attr("changetype");
        var ratetype = $obj.attr("ratetype");
        var Orderstattype = $obj.attr("Orderstattype");
        
        var data = {};
        
        if(orderstate!=undefined){
            data.orderstate = orderstate;
        }
        if(changetype!=undefined){
            data.changetype = changetype;
        }
        if(ratetype!=undefined){
            data.ratetype = ratetype;
        }
        if(Orderstattype!=undefined){
            data.Orderstattype = Orderstattype;
        }
        
        topTab.open("/ticketscenter/ordermanage/orderlistbystateandtype.aspx","订单处理列表",{
            data:data
        });
        
        return false;
    }
};

 $("#<%=ContainerID %>").find("a").click(Default.openOrderList);
</script>
</asp:content>
