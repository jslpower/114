<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxByRouteProList.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.AjaxByRouteProList" %>

<%@ Import Namespace="EyouSoft.Common" %>
    <script language="javascript" type="text/javascript">
    //删除主题
    function DeleteRoutePro(RouteId) {
            if (confirm('您确定要删除此线路主题吗？\n\n此操作不可恢复！')) {
            $.ajax
            ({
                url: "AjaxByRouteProList.aspx?DeletID=" + RouteId,
                cache: false,
                success: function(html) {
                    alert("删除成功");
                    ajaxPages.init(0);
                }
            });
        }
    }
     //修改主题
    function EidtRoutePro(objId, ID) {
        if (confirm('确定要修改此数据吗?')) {
            var obj = document.getElementById(objId);
              var RouteName = $(obj).closest("table").find("input").val();
            if (RouteName == "") {
                alert("线路主题名称不能为空！");
            }else{
                var isFlag=false; 
                $("#tbl_RouteList").children().find("input[name]='LevleName'").each(function(){          
                  var length = $(this).closest("tr").find("a[id='"+objId+"']").length;  
                  if(length>0){
                    return true;
                  }
                  if($(this).attr("value")==RouteName){
                    isFlag=true;
                    return false;
                  }
                });
                if(isFlag==true)
                {
                       alert("此线路主题名称已存在，请重新输入！");
                       ajaxPages.init(0);
                }
                else{
                    $.ajax
                    ({
                        url: "AjaxByRouteProList.aspx?EidtID=" + ID + "&RouteName=" + escape(RouteName),
                        cache: false,
                        success: function(html) {
                            alert("修改成功");
                            ajaxPages.init(0);
                        }
                    });
                }
            }
        }
        return false;
    }
    //操作状态：启用或禁用
    function ChangeState(ID,State)
    {
          $.ajax
            ({
                url: "AjaxByRouteProList.aspx?changeID="+ID+"&state=" + State,
                cache: false,
                success: function(html) {
                    if(State==0){
                        alert("禁用成功！");
                    }else
                    {
                        alert("启用成功");
                    }
                    ajaxPages.init(0);
                }
            });
    }
    //新增线路主题
    function AddRoutePro() {
        var RouteName =$.trim($("#txtRouteName").val());

        if (RouteName== "") {
            $("#errMsg_txtRouteName").html("请输入线路主题名称！");
        }else{
            var isFlag=false; 
            $("#tbl_RouteList").children().find("input[name]='LevleName'").each(function(){
                  if($(this).attr("value")==RouteName)
                    {
                       isFlag=true;
                    }
            });
            if(isFlag==true)
            {
                   alert("此线路主题名称已存在，请重新输入！");
                   $("#txtRouteName").val("");
            }
            else{
                $.ajax
                ({
                    url: "AjaxByRouteProList.aspx?EidtID=&RouteName=" + encodeURIComponent(RouteName),
                    cache: false,
                    success: function(html) {

                        alert("新增成功");
                        ajaxPages.init(0);
                    }
                });
            }
        }

    }
    </script>

    <table id="Table8" cellspacing="0" bordercolordark="white" cellpadding="0" width="100%"
        border="1">
        <tr>
            <td>
                &nbsp;线路主题名称：
                <input id="txtRouteName" name="txtRouteName"  maxlength="10" />
                <input id="btn_RouteAdd" runat="server" onclick="AddRoutePro();" type="button" value="添加" class="an_tijiaobaocun" /><span
                    id="errMsg_txtRouteName" style="color: Red"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
<div  id="tbl_RouteList">
    <asp:DataList ID="dalList" runat="server" RepeatColumns="3" BorderStyle="None" GridLines="Horizontal"
        onitemdatabound="dalList_ItemDataBound">
        <itemtemplate>
            <table  width="100%" border="1" cellspacing="0" cellpadding="0"  bordercolor="#C8E0EB">
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td>
                        <asp:Label ID="lblAutoNumber" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        <input name="LevleName" maxlength="10"  type="text" 
                            value="<%# DataBinder.Eval(Container.DataItem,"FieldName") %>" 
                            style="width: 114px">
                    </td>
                <td align="center">
                    <!--操作启用或停用-->
                    <%# ShowState(Convert.ToString(DataBinder.Eval(Container.DataItem, "FieldId")), Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsEnabled")))%>
                    
                </td>
                    <td align="center">
                        <%# CreateOperation(Convert.ToString(DataBinder.Eval(Container.DataItem, "FieldId")))%>
                    </td>
                </tr>
            </table>
        </itemtemplate>
    </asp:DataList>
    </div>
