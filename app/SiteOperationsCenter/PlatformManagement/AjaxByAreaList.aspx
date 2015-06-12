<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="AjaxByAreaList.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.AjaxByAreaList" %>

<script type="text/javascript">
    function DeleteRouteArea(RouteAreaId) {
    //执行ajax删除
        if (confirm('您确定要删除此线路区域吗？\n\n此操作不可恢复！')) {
            $.ajax
            ({
                url: "AjaxByAreaList.aspx?DeletID=" + RouteAreaId+"&TypeID="+<%=TypeID %>+"&ProvinceID="+<%=ProvinceID %>+"&CityID="+<%=CityID %>,
                cache: false,
                success: function(html) {
                ShowAreaAndCompany("<%=ProvinceID %>","<%=CityID %>","<%=TypeID %>");
                    alert("删除成功");
                }
            });
        }
    }
</script>
<asp:datalist id="dalList" runat="server" repeatcolumns="8" borderstyle="None" gridlines="Horizontal">
     <ItemTemplate> 
       <table width="100%">
            <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td align="center">
                    <label id="levName">
                        <%# DataBinder.Eval(Container.DataItem,"AreaName") %></label>
                </td>
                <td align="center">
                    <%# CreateOperation(Convert.ToString(DataBinder.Eval(Container.DataItem, "AreaId")))%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:datalist>
