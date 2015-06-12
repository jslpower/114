<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBySuitProduct.aspx.cs" Inherits="SiteOperationsCenter.CustomerManage.AjaxBySuitProduct" %>
<%@ Import Namespace="EyouSoft.Common" %>

<script language="javascript" type="text/javascript">
    //删除客户类型
    function DeleteSuitPro(SuitId) {
            if (confirm('您确定要删除此适用产品吗？\n\n此操作不可恢复！')) {
            $.ajax
            ({
                url: "AjaxBySuitProduct.aspx?DeletID=" + SuitId,
                cache: false,
                success: function(html) {
                    alert("删除成功!");
                    ajaxPages.init(1);
                }
            });
        }
    }
    //修改客户类型
    function EidtSuitPro(objId, ID) {
        if (confirm('确定要修改此数据吗?')) {
            var obj = document.getElementById(objId);
            var SuitName = $(obj).closest("table").find("input").val();
            if (SuitName == "") {
                alert("适用产品名称不能为空!");
            }else{
                var isFlag=false;
                $("#tbl_SuitList").children().find("input[name]='SuitName'").each(function() {
                   var length = $(this).closest("tr").find("a[id='"+objId+"']").length;  
                   if(length>0){
                       return true;
                   }
                   if ($(this).attr("value") == SuitName)
                    {
                       isFlag=true;
                       return false;
                   }
                });
                if(isFlag==true)
                {
                       alert("此适用产品名称已存在，请重新输入!");
                       ajaxPages.init(1);
                }
                else{
                    $.ajax
                    ({
                    url: "AjaxBySuitProduct.aspx?EidtID=" + ID + "&SuitName=" + escape(SuitName),
                        cache: false,
                        success: function(html) {
                            alert("修改成功");
                            ajaxPages.init(1);
                        }
                    });
                }
            }
        }
        return false;
    }
    //新增报价等级
    function AddSuitPro() {
        var SuitName = $.trim($("#txtSuitName").val());

        if (SuitName == "") {
            $("#errMsg_txtSuitName").html("请输入适用产品名称！");
        } else {
            var isFlag = false;
            $("#tbl_SuitList").children().find("input[name]='SuitName'").each(function() {
                if ($(this).attr("value") == SuitName) {
                    isFlag = true;
                }
            });
            if (isFlag == true) {
                alert("此适用产品名称已存在，请重新输入！");
                $("#txtSuitName").val("");
            }
            else {
                $.ajax
                ({
                    url: "AjaxBySuitProduct.aspx?EidtID=&SuitName=" + encodeURIComponent(SuitName),
                    cache: false,
                    success: function(html) {
                        alert("新增成功");
                        ajaxPages.init(1);
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
            &nbsp;适用产品：
            <input id="txtSuitName" name="txtSuitName" maxlength="20"  />
            <input id="btnSuitNameAdd" runat="server" onclick="AddSuitPro();" type="button" value="添加" class="an_tijiaobaocun" /><span
                id="errMsg_txtSuitName" style="color: Red"></span>
        </td>
    </tr>
</table>
<div  id="tbl_SuitList" >
<asp:datalist id="dalList" runat="server" repeatcolumns="3" borderstyle="None" gridlines="Horizontal"
onitemdatabound="dalList_ItemDataBound">
    <ItemTemplate>
        <table width="100%" border="0" cellspacing="0" cellpadding="3">
            <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td>
                    <asp:Label ID="lblAutoNumber" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <input name="SuitName" maxlength="20"  type="text" class="textfield" value="<%# DataBinder.Eval(Container.DataItem,"ProductName") %>">
                </td>
                <td align="center">
                    <%# CreateOperation(Convert.ToString(DataBinder.Eval(Container.DataItem, "ProuctId")))%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:datalist>
</div>
