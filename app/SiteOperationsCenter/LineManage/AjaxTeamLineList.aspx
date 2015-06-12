<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTeamLineList.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.AjaxTeamLineList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc3" %>
<!--列表-->
<table width="98%" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
    class="table_basic" id="tab_Tlist">
    <cc1:CustomRepeater ID="repList" runat="server">
        <HeaderTemplate>
            <tr class="list_basicbg">
                <th>
                    选择
                </th>
                <th>
                    团号
                </th>
                <th>
                    出团日期
                </th>
                <th>
                    报名截止
                </th>
                <th>
                    人数
                </th>
                <th>
                    余位
                </th>
                <th>
                    留位
                </th>
                <th>
                    状态
                </th>
                <th>
                    成人(市/结)
                </th>
                <th>
                    儿童(市/结)
                </th>
                <th>
                    单房差
                </th>
                <th>
                    功能
                </th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr style="height: 30px" val="<%#Eval("TourId") %>">
                <td align="center">
                    <input id="chk<%#Eval("TourId") %>" name="ckbTeamLine" type="checkbox" class="chk_select"
                        orderoeoplenum="<%#Eval("OrderPeopleNum")%>" value="<%#Eval("TourId") %>" />
                </td>
                <td align="center">
                    <%#Eval("TourNo")%>
                </td>
                <td align="center" nowrap="nowrap">
                    <%#((DateTime)Eval("LeaveDate")).ToString("yyyy-MM-dd")%>
                </td>
                <td align="center">
                    <%#((DateTime)Eval("RegistrationEndDate")).ToString("yyyy-MM-dd")%>
                </td>
                <td align="center">
                    <%#Utils.GetInt(Eval("TourNum").ToString())%>
                </td>
                <td align="center">
                    <%#Utils.GetInt(Eval("MoreThan").ToString())%>
                </td>
                <td align="center">
                    <%#Utils.GetInt(Eval("SaveNum").ToString())%>
                </td>
                <td align="center">
                    <%#Eval("PowderTourStatus").ToString()%>
                </td>
                <td align="center" nowrap="nowrap">
                    <%# Utils.GetDecimal(Eval("RetailAdultPrice").ToString()).ToString("0.00")%> |
                    <%# Utils.GetDecimal(Eval("SettlementAudltPrice").ToString()).ToString("0.00")%>
                </td>
                <td align="center" nowrap="nowrap">
                    <%# Utils.GetDecimal(Eval("RetailChildrenPrice").ToString()).ToString("0.00")%>
                    |
                    <%# Utils.GetDecimal(Eval("SettlementChildrenPrice").ToString()).ToString("0.00")%>
                </td>
                <td align="center">
                    <%# Math.Round(Utils.GetDecimal(Eval("MarketPrice").ToString()), 2)%>
                </td>
                <td align="center">
                    <a href="UpdateScatteredFightPlan.aspx?tourId=<%#Eval("tourID") %>">修改</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </cc1:CustomRepeater>
    <tr>
        <td colspan="12" align="left">
            <input type="checkbox" id="chk_allSelect" />全选
            <input type="button" name="btn_delect" id="btn_delect" value="删除" />
            &nbsp;
            <asp:repeater runat="server" id="rpt_updateStatus">
                                <ItemTemplate>
                                    <input  id="button1" type="button"  onclick="TeamplanManage.BtnOption(<%#Eval("Value") %>);return false;"  value="线路<%#Eval("Text") %>" />
                                </ItemTemplate>
                            </asp:repeater>
        </td>
    </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td height="30" align="right">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>

<script type="text/javascript">

    function BtnOption(v) {
        window.location = "/LineManage/TeamAdd.aspx?Operating=UpdatePowderTourStatus&TourStatus=" + v;
    }
    //全选
    $("#chk_allSelect").click(function() {
        var isChecked = $("#chk_allSelect").attr("checked");
        $("input[name=ckbTeamLine]").each(function() {
            $(this).attr("checked", isChecked);
        });
    });

</script>

<script type="text/javascript">


    $(function() {
        //批量删除
        $("#divTeamLineList #btn_delect").click(function() {
            TeamplanManage.Delect(TeamplanManage.GetTourIds())
            return false
        })
    })

    //删除单个团队计划
    function OneDelete(id) {
        $("#chk" + id).attr("checked", true);
        TeamplanManage.Delect(id);
        return false;
    }
            
            
</script>

