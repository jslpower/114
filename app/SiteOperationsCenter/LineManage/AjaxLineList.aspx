<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxLineList.aspx.cs" Inherits="SiteOperationsCenter.LineManage.AjaxLineList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<input id="hidAjaxNotStartingTeamsPage" value="<%=PageIndex %>" type="hidden" />
<cc1:CustomRepeater ID="repList" runat="server">
    <HeaderTemplate>
        <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C7DEEB"
            class="table_basic">
            <tr>
                <th nowrap="nowrap">
                    <input type="checkbox" id="chkSelAll" name="chkSelAll" value="checkbox" />
                    全选
                </th>
                <th nowrap="nowrap">
                    线路名称
                </th>
                <th nowrap="nowrap">
                    发布单位
                </th>
                <th nowrap="nowrap">
                    状态
                </th>
                <th nowrap="nowrap">
                    天数
                </th>
                <th nowrap="nowrap">
                    班次计划
                </th>
                <th nowrap="nowrap">
                    成人
                </th>
                <th nowrap="nowrap">
                    儿童
                </th>
                <th nowrap="nowrap">
                    B2B
                </th>
                <th nowrap="nowrap">
                    B2C
                </th>
                <th nowrap="nowrap">
                    计划管理
                </th>
                <th nowrap="nowrap">
                    点击
                </th>
                <th nowrap="nowrap">
                    创建时间
                </th>
                <th nowrap="nowrap">
                    操作
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="left">
                <input type="checkbox" name="checkboxLine" value="<%#Eval("RouteId") %>" id="chk<%#Eval("RouteId") %>" />
            </td>
            <td align="left" title="<%#Eval("RouteName") %>">
                <a href="javascript:void(0)" onclick="UrlUpdateLine('<%#Eval("RouteId") %>');return false;">
                    <%# Utils.GetText2(Eval("RouteName").ToString(),10,true)%></a>
            </td>
            <td align="left">
                <a href="/CompanyManage/AddBusinessMemeber.aspx?EditId=<%#Eval("Publishers") %>">
                    <%#Eval("PublishersName")%></a>
            </td>
            <td align="center">
                <a href="javascript:void(0);" onclick="SearchByRecommendType(<%# Convert.ToInt32(Enum.Parse(typeof(EyouSoft.Model.NewTourStructure.RecommendType), Eval("RecommendType").ToString()))%>);return false;">
                    <span class="state<%# Convert.ToInt32(Enum.Parse(typeof(EyouSoft.Model.NewTourStructure.RecommendType), Eval("RecommendType").ToString()))-1%>">
                        <%#Eval("RecommendType").ToString() == EyouSoft.Model.NewTourStructure.RecommendType.无.ToString() ? "" : Eval("RecommendType")%>
                    </span></a>
            </td>
            <td align="center">
                <%#Eval("Day")%>
            </td>
            <td align="center">
                <a style="display: <%# Eval("RouteSource").ToString() == "地接社添加" ? "none;" : "" %>"
                    href="TeamAdd.aspx?RouteId=<%#Eval("RouteId") %>&AreaId=<%#Eval("AreaId") %>&CompanyId=<%#Eval("Publishers") %>">
                    <%# Eval("TeamPlanDes")%>&gt;&gt;</a>
            </td>
            <td align="left">
                <%# Utils.FilterEndOfTheZeroDecimal(Math.Round(Utils.GetDecimal(Eval("RetailAdultPrice").ToString()), 2))%>
            </td>
            <td align="left">
                <%--<%# Math.Round(Utils.GetDecimal(Eval("RetailAdultPrice").ToString()),2)%>--%>
                <%# Utils.FilterEndOfTheZeroDecimal(Math.Round(Utils.GetDecimal(Eval("RetailChildrenPrice").ToString()), 2))%>
            </td>
            <td align="center">
                <%#Eval("B2B")%>
            </td>
            <td align="center">
                <%#Eval("B2C")%>
            </td>
            <td align="center">
                <a style="display: <%# Eval("RouteSource").ToString() == "地接社添加" ? "none;" : "" %>"
                    href="TeamAdd.aspx?RouteId=<%#Eval("RouteId") %>&AreaId=<%#Eval("AreaId") %>&CompanyId=<%#Eval("Publishers") %>">
                    修改计划</a>
            </td>
            <td align="center">
                <%#Eval("ClickNum")%>
            </td>
            <td style="text-align:center;">
                <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm:ss}")%>
            </td>
            <td align="center">
                <a href="AddLine.aspx?LineId=<%#Eval("RouteId") %>">修改</a> 
                <a href="AddLine.aspx?OperatType=copy&LineId=<%#Eval("RouteId") %>">复制</a>
                <br />
                <a href="javascript:void(0)" style="display: <%# Eval("IsDeleted").ToString() == "true" ? "none;": ""%>"
                    onclick="DeleteLine('<%#Eval("RouteId") %>');return false;">删除</a>
                <%# Eval("RouteStatus")%>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
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

</script>

<script type="text/javascript">


    function SearchByRecommendType(val) {
        $("#form1 #dropRecommendType").val(val);
        LineManage.OnSearch();
    }


    //删除
    function DeleteLine(id) {
        LineManage.DeleteLine(id);
    }


    //复制 修改
    function UrlUpdateLine(id, type) {
        if (type == "copy")//复制
        {
            window.location = "AddLine.aspx?OperatType=copy&LineId=" + id;
        }
        else//修改
        {
            window.location = "AddLine.aspx?LineId=" + id;
        }
    }


    //上架下架
    function SetRouteStatusajax(v, id) {
        $("#chk" + id).attr("checked", true);
        if (v == "上架") {
            LineManage.setRouteStatus(1);
        }
        else if (v == "下架") {
            LineManage.setRouteStatus(2)
        }
    }


    //全选
    $("#chkSelAll").click(function() {
        var isChecked = $("#chkSelAll").attr("checked");
        $("input[name=checkboxLine]").each(function() {
            $(this).attr("checked", isChecked);
        });
    });
</script>

