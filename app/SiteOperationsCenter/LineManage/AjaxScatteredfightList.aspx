<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxScatteredfightList.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.AjaxScatteredfightList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<cc1:CustomRepeater ID="repList" runat="server">
    <HeaderTemplate>
        <table id="tbl_ScatteredfightList" width="98%" border="1" align="center" cellpadding="0"
            cellspacing="0" bordercolor="#9dc4dc" class="table_basic">
            <tr class="list_basicbg">
                <th align="middle" nowrap="nowrap">
                    <input type="checkbox" name="chkSelAll" id="chkSelAll" />
                    全
                </th>
                <th align="middle" nowrap="nowrap">
                    团号
                </th>
                <th align="middle" nowrap="nowrap">
                    出发地
                </th>
                <th align="middle" nowrap="nowrap">
                    线路名称
                </th>
                <th align="middle" nowrap="nowrap">
                    发布单位
                </th>
                <th align="middle" nowrap="nowrap">
                    类型
                </th>
                <th align="middle" nowrap="nowrap">
                    出团日期
                </th>
                <th align="middle" nowrap="nowrap">
                    报名截止
                </th>
                <th align="middle" nowrap="nowrap">
                    人数
                </th>
                <th align="middle" nowrap="nowrap">
                    余位
                </th>
                <th align="middle" nowrap="nowrap">
                    状态
                </th>
                <th align="middle" nowrap="nowrap">
                    成人价
                </th>
                <th align="middle" nowrap="nowrap">
                    儿童价
                </th>
                <th align="middle" nowrap="nowrap">
                    单房差
                </th>
                <th align="middle" nowrap="nowrap">
                    游客
                </th>
                <th align="middle" nowrap="nowrap">
                    功能
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td align="center">
                <input type="checkbox" name="ckbScatteredfight" id="checkbox2" value="<%#Eval("TourId") %>" />
            </td>
            <td align="center">
                <%#Eval("TourNo")%>
            </td>
            <td align="center">
                <%#Eval("StartCityName")%>
            </td>
            <td align="center" title="<%#Eval("RouteName") %>">
                <a href="AddLine.aspx?LineId=<%#Eval("RouteId") %>">
                    <%#Utils.GetText2(Eval("RouteName").ToString(),6,true)%></a>
            </td>
            <td align="left" title="<%#Eval("PublishersName") %>">
                <%#Utils.GetText2(Eval("PublishersName").ToString(),6,true)%>
            </td>
            <td align="center">
                <span class="state<%# Convert.ToInt32(Enum.Parse(typeof(EyouSoft.Model.NewTourStructure.RecommendType), Eval("RecommendType").ToString()))-1%>">
                    <%#Eval("RecommendType").ToString() == EyouSoft.Model.NewTourStructure.RecommendType.无.ToString() ? "" : Eval("RecommendType")%>
                </span>
            </td>
            <td align="center">
                <%#Convert.ToDateTime(Eval("LeaveDate")).ToShortDateString()%>
            </td>
            <td align="center">
                <%#Utils.GetDateTime(Eval("RegistrationEndDate").ToString()).ToShortDateString()%>
            </td>
            <td align="center">
                <%#Eval("TourNum")%>
            </td>
            <td align="center">
                <%#Eval("MoreThan")%>
            </td>
            <td align="center">
                <a class="<%# GetClass(Eval("PowderTourStatus").ToString()) %>" onclick="javascript:void(0)">
                    <%#Eval("PowderTourStatus")%></a>
            </td>
            <td align="center">
                <%#Math.Round(Utils.GetDecimal(Eval("RetailAdultPrice").ToString()),2)%>|<%#Math.Round(Utils.GetDecimal(Eval("SettlementAudltPrice").ToString()),2)%>
            </td>
            <td align="center">
                <%#Math.Round(Utils.GetDecimal(Eval("RetailChildrenPrice").ToString()), 2)%>|<%#Math.Round(Utils.GetDecimal(Eval("SettlementChildrenPrice").ToString()), 2)%>
            </td>
            <td align="center">
                <%#Math.Round(Utils.GetDecimal(Eval("MarketPrice").ToString()), 2)%>
            </td>
            <td align="center">
                <%--<a href="/PrintPage/TouristInfo.aspx?TeamId=<%#Eval("TourId") %>" target="_blank">
                    名单</a>--%><br />
                <a class="a_orders" href="/LineManage/FitList.aspx?tourId=<%#Eval("TourId") %>">订单</a><br />
                </a>
            </td>
            <td align="center">
                <a href="/LineManage/UpdateScatteredFightPlan.aspx?tourId=<%#Eval("TourId") %>" class="a_Update">
                    修改</a><br />
                <a href="javascript:void(0)" onclick="Delete('<%# Eval("TourId") %>');return false;">
                    删除</a>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tr>
            <td align="center">
                <input type="checkbox" name="chkSelAll2" id="chkSelAll2" />
                全选
            </td>
            <td colspan="15" align="left">
                <%-- <input type="submit" name="button" id="button" value="统一修改团队行程" />--%>
            </td>
        </tr>
        </table>
    </FooterTemplate>
</cc1:CustomRepeater>
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td height="30" align="right">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
    
    //删除
    function Delete(tourid)
    {
        if (!confirm("你确定要删除该条数据吗？")) {
                return false;
        }
        $.ajax({
            type: "GET",
            dataType: 'html',
            url: "AjaxScatteredfightList.aspx?action=Delete&argument=" + tourid,
            cache: false,
            success: function(result) {
                if (result == "ok") {
                    alert("删除成功!");
                    ScatteredfightManage.OnSearch();
                } else {
                    alert("删除失败,该计划可能已经存在订单!"); 
                }
            },
            error: function() {
                alert("删除失败，请稍后再试!");
            }
        });
    
    }
    
    //名单
    function Openmingdang(tourid)
    {
        Boxy.iframeDialog({title:'团队推广说明', iframeUrl:'/PrintPage/TouristInfo.aspx?TeamId='+tourid,width:1000,height:500,draggable:true,data:{callBack:'ScatteredfightManage.saveTourMarkerNote'}});
    }


    
        //全选
    $("#chkSelAll").click(function() {
        var isChecked = $("#chkSelAll").attr("checked");
        $("input[name=ckbScatteredfight]").each(function() {
            $(this).attr("checked", isChecked);
    });
    });
    //全选
    $("#chkSelAll2").click(function() {
        var isChecked = $("#chkSelAll2").attr("checked");
        $("input[name=ckbScatteredfight]").each(function() {
            $(this).attr("checked", isChecked);
    });
    });
    
</script>

