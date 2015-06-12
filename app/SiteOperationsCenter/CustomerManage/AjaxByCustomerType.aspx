<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxByCustomerType.aspx.cs" Inherits="SiteOperationsCenter.CustomerManage.AjaxByCustomerType" %>

<%@ Import Namespace="EyouSoft.Common" %>

<script language="javascript" type="text/javascript">
    //删除客户类型
    function DeleteCustomerPro(CustomerId) {
            if (confirm('您确定要删除此客户类型吗？\n\n此操作不可恢复！')) {
            $.ajax
            ({
                url: "AjaxByCustomerType.aspx?DeletID=" + CustomerId,
                cache: false,
                success: function(html) {
                    alert("删除成功!");
                    ajaxPages.init(0);
                }
            });
        }
    }
    //修改客户类型
    function EidtCustomerPro(objId, ID) {
        if (confirm('确定要修改此数据吗?')) {
            var obj = document.getElementById(objId);
            var CustomerName = $(obj).closest("table").find("input").val();
            if (CustomerName == "") {
                alert("客户类型名称不能为空!");
            }else{
                var isFlag=false;
                $("#tbl_CustomerTypeList").children().find("input[name]='TypeName'").each(function() {
                   var length = $(this).closest("tr").find("a[id='"+objId+"']").length;  
                   if(length>0){
                       return true;
                   }
                   if ($(this).attr("value") == CustomerName)
                    {
                       isFlag=true;
                       return false;
                   }
                });
                if(isFlag==true)
                {
                    alert("此客户类型名称已存在，请重新输入!");
                       ajaxPages.init(0);
                }
                else{
                    $.ajax
                    ({
                        url: "AjaxByCustomerType.aspx?EidtID=" + ID + "&CustomerName=" + escape(CustomerName),
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
    //新增报价等级
    function AddCustomerPro() {
        var CustomerName = $.trim($("#txtCustomerName").val());

        if (CustomerName == "") {
            $("#errMsg_txtCustomerName").html("请输入客户类型名称！");
        }else{
            var isFlag=false;
            $("#tbl_CustomerTypeList").children().find("input[name]='TypeName'").each(function() {
            if ($(this).attr("value") == CustomerName) {
                   isFlag=true;
                }
            });
            if(isFlag == true)
            {
                alert("此客户类型名称已存在，请重新输入！");
                $("#txtCustomerName").val("");
            }
            else{
                $.ajax
                ({
                    url: "AjaxByCustomerType.aspx?EidtID=&CustomerName=" + encodeURIComponent(CustomerName),
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
            &nbsp;客户类型：
            <input id="txtCustomerName" name="txtCustomerName" maxlength="20"  />
            <input id="btnCustomerNameAdd" runat="server" onclick="AddCustomerPro();" type="button" value="添加" class="an_tijiaobaocun" /><span
                id="errMsg_txtCustomerName" style="color: Red"></span>
        </td>
    </tr>
</table>
<div  id="tbl_CustomerTypeList" >
<asp:datalist id="dalList" runat="server" repeatcolumns="3" borderstyle="None" gridlines="Horizontal"
onitemdatabound="dalList_ItemDataBound">
    <ItemTemplate>
        <table width="100%" border="0" cellspacing="0" cellpadding="3">
            <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td>
                    <asp:Label ID="lblAutoNumber" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <input name="TypeName" maxlength="20"  type="text" class="textfield" value="<%# DataBinder.Eval(Container.DataItem,"TypeName") %>">
                </td>
                <td align="center">
                    <%# CreateOperation(Convert.ToString(DataBinder.Eval(Container.DataItem, "TypeId")))%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:datalist>
</div>
