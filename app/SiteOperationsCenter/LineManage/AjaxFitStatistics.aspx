<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxFitStatistics.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.AjaxFitStatistics" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:CustomRepeater ID="repList" runat="server">
        <HeaderTemplate>
            <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C7DEEB"
                class="table_basic">
                <tr>
                    <th>
                        时间
                    </th>
                    <th>
                        专线
                    </th>
                    <th>
                        订单量
                    </th>
                    <th>
                        总人数
                    </th>
                    <th>
                        成人
                    </th>
                    <th>
                        儿童
                    </th>
                    <th>
                        销售总额
                    </th>
                    <th>
                        订单总额
                    </th>
                    <th>
                        功能
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td align="left">
                    <%# Convert.ToDateTime(Eval("LeaveDateMin").ToString()).ToShortDateString()%>至
                    <%# Convert.ToDateTime(Eval("LeaveDateMax").ToString()).ToShortDateString()%>
                </td>
                <td align="center">
                    <%#Eval("AreaName")%>
                </td>
                <td align="center">
                    <a href="FitList.aspx?Line2=<%#Eval("AreaId")%>">
                        <%#Eval("TotalOrder")%></a>
                </td>
                <td align="center">
                    <%#Eval("TotalPeople")%>
                </td>
                <td align="center" valign="middle">
                    <%#Eval("TotalAdult")%>
                </td>
                <td align="center">
                    <%#Eval("TotalChild")%>
                </td>
                <td align="center">
                    <%# Math.Round(Utils.GetDecimal(Eval("TotalSale").ToString()),2)%>
                </td>
                <td align="center">
                    <%# Math.Round(Utils.GetDecimal(Eval("TotalSettle").ToString()), 2)%>
                </td>
                <td align="center">
                    <a href="javascript:void(0)" onclick="LineDetailmanage.OnSearch('<%#Eval("AreaId") %>');return false;"
                        id="LineDetail<%#Eval("AreaId") %>">明细查看</a>
                </td>
            </tr>
            <tr id="htmlshowtr<%#Eval("AreaId") %>">
                <td>
                    <%-- <div id="htmlshow<%#Eval("AreaId") %>">
                    </div>--%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </cc1:CustomRepeater>
    <div align="right">
        <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
    </div>
    </form>

    <script type="text/javascript">

        var Parms1 = {LineId: 0,IsDetail:false,Page: 1 };
        var LineDetailmanage = {//线路列表
            GetLineDetailList: function(v) {
                 if(<%=currentPage %> >0 ){
                     Parms1.Page=<%=currentPage %>;
                 }
                //LoadingImg.ShowLoading("htmlshow"+v);
                //if (LoadingImg.IsLoadAddDataToDiv("htmlshow"+v)) {
                    $.ajax({
                        //url: "AjaxFitStatistics.aspx?type=Linedetail&IsDetail=true&LineId="+v+"&Page=1",
                        url: "AjaxLineDetaile.aspx",
                        cache: false,
                        type: "GET",
                        dataType: "html",
                        data: Parms1,
                        success: function(html) {
                            $("#htmlshowtr"+v).html(html);
                            $("#LineDetail"+v).html("明细隐藏");
                        },
                        error: function() {
                            alert("操作失败");
                        }
                    });
               // }
            },
            
           ckAllCompany: function(obj) {//全选
                $("#tbCompanyList").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            },
            OnSearch: function(v) {//查询
                //if($("#LineDetail"+v).html().replace(/(^\s*)|(\s*$)/g,"")=="明细查看")
                if($.trim($("#LineDetail"+v).html())=="明细查看")
                {
                    Parms1.IsDetail=true;
                    Parms1.LineId=v;
                    Parms1.Page = 1;
                    LineDetailmanage.GetLineDetailList(v);
                }
                else if($.trim($("#LineDetail"+v).html())=="明细隐藏")
                {
                    $("#htmlshowtr"+v).html("");
                    $("#LineDetail"+v).html("明细查看");
                }
            }
     }

    </script>

</body>
</html>
