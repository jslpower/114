<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMainte.aspx.cs" Inherits="UserBackCenter.TicketsCenter.FreightManage.AjaxMainte" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#7dabd8"
    style="margin-top: 10px;">
    <tr>
        <th width="12%" height="28" align="center" bgcolor="#EEF7FF">
            航空公司
        </th>
        <th width="12%" align="center" bgcolor="#EEF7FF">
            始发地
        </th>
        <th width="12%" align="center" bgcolor="#EEF7FF">
            目的地
        </th>
        <th width="14%" align="center" bgcolor="#EEF7FF">
            最后修改日期
        </th>
        <th width="10%" align="center" bgcolor="#EEF7FF">
            运价状态
        </th>
        <th width="12%" align="center" bgcolor="#EEF7FF">
            购买类型
        </th>
    </tr>
    <asp:repeater id="mai_rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center" width="140px">
                              <a href="javascript:void(0);" onclick="ThisPage.UpdateModel('<%#Eval("id") %>')"> <%#Eval("FlightName")%></a>
                            </td>
                            <td align="center">
                                 <%#Eval("NoGadHomeCityIdName")%>
                            </td>
                            <td align="center">
                                <%#Eval("NoGadDestCityName")%>
                            </td>
                            <td align="center">
                                <%#Eval("LastUpdateDate") == null ? Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd HH:mm:ss") : Convert.ToDateTime(Eval("LastUpdateDate")).ToString("yyyy-MM-dd HH:mm:ss")%>
                            </td>
                            <td align="center">
                                <%#FreTypeInit(Convert.ToInt32(Eval("IsEnabled")), Convert.ToInt32(Eval("IsExpired")), Eval("Id").ToString(), Eval("FreightBuyId").ToString())%>
                            </td>
                            <td align="center">
                                <%#PackageType(Convert.ToInt32(Eval("BuyType")))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
</table>
<div style="width: 100%; text-align: center;">
    <asp:label runat="server" text="" id="lblMsg"></asp:label>
    <div id="div_AjaxList" style="text-align: center; margin-bottom:2px; margin-top:2px;">
        <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
    </div>

</div>

<script type="text/javascript">
    var ThisPage = {
        UpdateFreEn: function(id, obj, BuyId) {
            var state = $(obj).attr("ref");
            $.ajax({
                type: "GET",
                url: "/TicketsCenter/FreightManage/MainteHandle.ashx?id=" + id + "&state=" + state + "&cid=<%=SiteUserInfo.CompanyID %>&BuyId=" + BuyId,
                cache: false,
                success: function(result) {
                    if (result == "OK") {
                        if (state == "1") {
                            $(obj).text("启用");
                            $(obj).attr("ref", "0");
                            $(obj).attr("title", "点击关闭");
                        } else {
                            $(obj).text("关闭");
                            $(obj).attr("ref", "1");
                            $(obj).attr("title", "点击启用");
                        }
                    } else if (result == "ERROR") {
                        alert("操作失败!请刷新后再操作!");
                    } else {
                        alert(result);
                    }
                }
            });
        },
        UpdateModel: function(id) {
            topTab.open("/TicketsCenter/FreightManage/FreightAdd.aspx?id=" + id + "&v=" + Math.random(), "运价修改", { tabType: "form" });
        }
    };
</script>

