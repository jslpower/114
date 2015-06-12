<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTicketSelect.aspx.cs"
    Inherits="UserPublicCenter.AirTickets.TeamBook.AjaxTicketSelect" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="myASP" %>
<%@ Import Namespace="EyouSoft.Model.TicketStructure" %>
<div style="position: absolute; display: none; background-color: yellow; width: 200px;
    text-align: left; height: auto; border: 1px solid #555" id="ats_remark">
</div>
<div style="position: absolute; display: none; top: 410px; left: 350px;" id="ats_CompanyInfo">
    <table width="450" border="0" cellpadding="0" cellspacing="0" bgcolor="#e9f6fd" style="border: 2px #82d0fd solid;">
        <tr>
            <td colspan="2" height="35">
                <table width="98%" border="0" cellspacing="0" cellpadding="0" style="background: url(<%=ImageServerUrl%>/images/xq_top_bg.jpg) repeat-x 0 0;">
                    <tr>
                        <td width="89%" height="29" align="center" valign="middle">
                            <span class="title_text" style="color: #000">代理商详情</span>
                        </td>
                        <td width="11%" align="center" valign="middle">
                            <span class="title_text"><a href="javascript:void(0);" onclick="return TicketSelect.closeDiv();">
                                X关闭</a></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="5">
            </td>
        </tr>
        <tr>
            <td width="28%" align="center">
                <img src="images/4.jpg" alt="" width="80" height="58" id="cerImg" />
            </td>
            <td width="52%" rowspan="6" align="left" valign="top" style="line-height: 33px;">
                联系人：<span id="ats_cName"></span><br />
                联系电话：<span id="ats_cTel"></span><br />
                上下班时间：<span id="ats_wTime"></span><br />
                供应商主页：<a href="#"><span id="ats_url"></span></a><br />
                代理级别：<span id="ats_lev"></span><br />
                出票成功率：<span id="ats_success"></span><br />
                退票平均时间：自愿/非自愿：<span id="ats_tTime"></span>（小时）<br />
            </td>
        </tr>
        <tr align="center">
            <td>
                营业执照
            </td>
        </tr>
        <tr align="center">
            <td>
                <img src="images/tongpai.jpg" id="bronze" width="80" height="57" />
            </td>
        </tr>
        <tr align="center">
            <td>
                铜牌
            </td>
        </tr>
        <tr align="center">
            <td>
                <img src="images/jz.jpg" id="trade" width="80" height="58" />
            </td>
        </tr>
        <tr align="center">
            <td>
                行业奖项
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" height="10">
            </td>
        </tr>
    </table>
</div>
<myASP:CustomRepeater ID="acl_rptCustomerList" runat="server">
    <ItemTemplate>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30">
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                        <tr>
                            <td height="35" align="left" bgcolor="#FAFAFA" colspan="10">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="search_results">
                                    <tr>
                                        <td width="19%" align="center">
                                            <span style="font-size: 14px;">
                                                <%# Eval("AirportName") %></span>
                                        </td>
                                        <td width="8%" height="25" align="center">
                                            去程：
                                        </td>
                                        <td width="24%" align="left">
                                            <%=startCityName %>--&gt;<%=toCityName %>
                                        </td>
                                        <td width="16%" align="left">
                                            航班号：暂无
                                        </td>
                                        <td width="16%" align="left">
                                            出发时间：<%=startDatestr %>
                                        </td>
                                        <td width="17%" align="left">
                                            旅客类型：<%=peopleType.Value%>
                                        </td>
                                    </tr>
                                    <%if (airTypestr == 2)
                                      {%>
                                    <tr>
                                        <td width="19%" align="center">
                                            <span style="font-size: 14px;"></span>
                                        </td>
                                        <td width="8%" height="25" align="center">
                                            回程：
                                        </td>
                                        <td width="24%" align="left">
                                            <%=toCityName%>--&gt;<%=startCityName%>
                                        </td>
                                        <td width="16%" align="left">
                                            航班号：暂无
                                        </td>
                                        <td width="16%" align="left">
                                            返回时间：<%=backDatestr %>
                                        </td>
                                        <td width="17%" align="left">
                                            旅客类型：<%=peopleType.Value%>
                                        </td>
                                    </tr>
                                    <%} %>
                                </table>
                            </td>
                        </tr>
                        <tr bgcolor="#EDF8FC">
                            <th width="18%" height="25" align="center" bgcolor="#EDF8FC">
                                代理商名称
                            </th>
                            <th width="14%" align="center">
                                参考面价
                            </th>
                            <th width="10%" align="center">
                                参考扣率
                            </th>
                            <th width="8%" align="center">
                                结算价<br />
                                (不含税)
                            </th>
                            <th width="8%" align="center">
                                人数上限
                            </th>
                            <th width="12%" align="center">
                                燃油/机建
                            </th>
                            <th width="5%" align="center">
                                类型
                            </th>
                            <th width="10%" align="center">
                                备注
                            </th>
                            <th width="8%" align="center">
                                供应商MQ
                            </th>
                            <th align="center">
                                预订
                            </th>
                        </tr>
                        <%# GetSuppliers(Eval("FreightInfoList"))%>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</myASP:CustomRepeater>
