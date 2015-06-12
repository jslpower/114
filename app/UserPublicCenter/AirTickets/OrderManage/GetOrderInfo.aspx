<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetOrderInfo.aspx.cs" Inherits="UserPublicCenter.AirTickets.OrderManage.GetOrderInfo" %>

<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px #dcd8d8 solid;" id="tb_OrderInfo" align="center">
    <tr>
        <td>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="16%" height="30" align="center">
                        乘客票号：
                    </td>
                    <td width="16%" align="center">
                        <input name="txtpnr" type="text" id="txtpnr" size="15" runat="server" />
                    </td>
                    <td colspan="4" align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td height="30" align="center">
                        乘客姓名：
                    </td>
                    <td align="center">
                        <input name="txtTraName" type="text" id="txtTraName" size="15"  runat="server"/>
                    </td>
                    <td width="16%" align="center">
                        有效身份证件号码：
                    </td>
                    <td width="22%" align="center">
                        <input name="txtTraNum" type="text" id="txtTraNum" size="22"  runat="server" />
                    </td>
                    <td width="10%" align="right">
                        <span id="Label4">签 注：</span>
                    </td>
                    <td width="16%" align="center">
                        <input name="textfield4" type="text" id="textfield4" size="15" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="9%" height="25" align="center">
                        &nbsp;
                    </td>
                    <td width="9%" height="25" align="center">
                        航程
                    </td>
                    <td width="9%" align="center">
                        承运人
                    </td>
                    <td width="9%" align="center">
                        航班号
                    </td>
                    <td width="9%" align="center">
                        舱位
                    </td>
                    <td width="9%" align="center">
                        日期
                    </td>
                    <td width="9%" align="center">
                        时间
                    </td>
                    <td width="9%" align="center">
                        客票级别
                    </td>
                    <td width="9%" align="center">
                        生效日期
                    </td>
                    <td width="9%" align="center">
                        截止日期
                    </td>
                    <td width="9%" align="center">
                        免费行李
                    </td>
                </tr>
                <tr>
                    <td height="25" align="center">
                        自 FROM
                    </td>
                    <td height="25" align="center">
                        <input name="textfield6" type="text" id="textfield6" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield7" type="text" id="textfield7" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield8" type="text" id="textfield8" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield9" type="text" id="textfield9" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield10" type="text" id="textfield10" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield11" type="text" id="textfield11" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield12" type="text" id="textfield12" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield13" type="text" id="textfield13" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield14" type="text" id="textfield14" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield15" type="text" id="textfield15" size="10" />
                    </td>
                </tr>
                <tr>
                    <td height="25" align="center">
                        至 TO
                    </td>
                    <td height="25" align="center">
                        <input name="textfield16" type="text" id="textfield16" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield19" type="text" id="textfield19" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield22" type="text" id="textfield22" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield25" type="text" id="textfield25" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield28" type="text" id="textfield28" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield31" type="text" id="textfield31" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield34" type="text" id="textfield34" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield37" type="text" id="textfield37" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield40" type="text" id="textfield40" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield43" type="text" id="textfield43" size="10" />
                    </td>
                </tr>
                <tr>
                    <td height="25" align="center">
                        至 TO
                    </td>
                    <td height="25" align="center">
                        <input name="textfield17" type="text" id="textfield17" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield20" type="text" id="textfield20" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield23" type="text" id="textfield23" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield26" type="text" id="textfield26" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield29" type="text" id="textfield29" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield32" type="text" id="textfield32" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield35" type="text" id="textfield35" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield38" type="text" id="textfield38" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield41" type="text" id="textfield41" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield44" type="text" id="textfield44" size="10" />
                    </td>
                </tr>
                <tr>
                    <td height="25" align="center">
                        至 TO
                    </td>
                    <td height="25" align="center">
                        <input name="textfield18" type="text" id="textfield18" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield21" type="text" id="textfield21" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield24" type="text" id="textfield24" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield27" type="text" id="textfield27" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield30" type="text" id="textfield30" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield33" type="text" id="textfield33" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield36" type="text" id="textfield36" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield39" type="text" id="textfield39" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield42" type="text" id="textfield42" size="10" />
                    </td>
                    <td align="center">
                        <input name="textfield45" type="text" id="textfield45" size="10" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td height="40">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="9%" align="center">
                        票价：
                    </td>
                    <td width="11%" align="center">
                        <input name="textfield46" type="text" id="textfield46" size="10" />
                    </td>
                    <td width="10%" align="center">
                        机场建设费：
                    </td>
                    <td width="10%" align="center">
                        <input name="textfield47" type="text" id="textfield47" size="10" />
                    </td>
                    <td width="10%" align="center">
                        附加燃油费：
                    </td>
                    <td width="10%" align="center">
                        <input name="textfield48" type="text" id="textfield48" size="10" />
                    </td>
                    <td width="10%" align="center">
                        其他税费：
                    </td>
                    <td width="10%" align="center">
                        <input name="textfield49" type="text" id="textfield49" size="10" />
                    </td>
                    <td width="10%" align="center">
                        合计：
                    </td>
                    <td width="10%" align="center">
                        <input name="textfield50" type="text" id="textfield50" size="10" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="14%" height="30" align="center">
                        电子客票号码：
                    </td>
                    <td width="10%" align="center">
                        <input name="textfield51" type="text" id="textfield51" size="10" />
                    </td>
                    <td width="12%" align="center">
                        验证码：
                    </td>
                    <td width="12%" align="center">
                        <input name="textfield52" type="text" id="textfield52" size="10" />
                    </td>
                    <td width="12%" align="center">
                        连续客票：
                    </td>
                    <td width="12%" align="center">
                        <input name="textfield53" type="text" id="textfield53" size="10" />
                    </td>
                    <td width="12%" align="center">
                        保险费：
                    </td>
                    <td width="12%" align="center">
                        <input name="textfield54" type="text" id="textfield54" size="10" />
                    </td>
                </tr>
                <tr>
                    <td height="30" align="center">
                        销售单位代码:
                    </td>
                    <td align="center">
                        <input name="textfield55" type="text" id="textfield55" size="10" />
                    </td>
                    <td align="center">
                        填开单位:
                    </td>
                    <td align="center">
                        <input name="textfield56" type="text" id="textfield56" size="10" />
                    </td>
                    <td align="center">
                        填开日期:
                    </td>
                    <td align="center">
                        <input name="textfield57" type="text" id="textfield57" size="10" />
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>

</table>
<ul class="pringBtn">
    <ul>
        <li ><a href="javascript:void(0)" id="hfprior" runat="server" >上一位</a></li>
        <li ><a href="javascript:void(0)" id="hfnext" runat="server">下一位</a></li>
        <li><a href="javascript:void(0)" id="hfClear" onclick="PrintTicketJournal.ClearTableInput()">清空</a></li>
        <li><a href="jfvascript:void(0);" id="hfPrint"  onclick="PrintTicketJournal.PrintShow();return false;">打印预览</a></li>
        <li><a href="jfvascript:void(0);" class="printhidden" id="hfPrint2" onclick="PrintTicketJournal.Print();return false;">我要打印</a></li>
         <li><a href="jfvascript:void(0);" class="printhidden" id="hfReturn" onclick="PrintTicketJournal.PrintReturn();return false;">返回</a></li>
    </ul>
</ul>
<input type="hidden" id="hidTravelerId" runat="server" />
<input type="hidden" id="hidTraveJson" runat="server" />

