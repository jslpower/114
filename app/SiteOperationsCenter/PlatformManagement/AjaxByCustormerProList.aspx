<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxByCustormerProList.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.AjaxByCustormerProList" %>

<%@ Import Namespace="EyouSoft.Common" %>

<script language="javascript" type="text/javascript">
    //删除客户等级
    function DeleteCustomerPro(CustomerId) {
            if (confirm('您确定要删除此客户等级吗？\n\n此操作不可恢复！')) {
            $.ajax
            ({
                url: "AjaxByCustormerProList.aspx?DeletID=" + CustomerId,
                cache: false,
                success: function(html) {
                    alert("删除成功");
                    ajaxPages.init(2);
                }
            });
        }
    }
     //修改客户等级
    function EidtCustomerPro(objId, ID) {
        if (confirm('确定要修改此数据吗?')) {
            var obj = document.getElementById(objId);
              var CustormerName = $(obj).closest("table").find("input").val();
            if (CustormerName == "") {
                alert("客户等级名称不能为空！");
            }else{
                var isFlag=false; 
                $("#tbl_CusList").children().find("input[name]='LevleName'").each(function(){
                    var length = $(this).closest("tr").find("a[id='"+objId+"']").length;  
                  if(length>0){
                    return true;
                  }
                  if($(this).attr("value")==CustormerName)
                    {
                       isFlag=true;
                       return false;
                    }
                });
                if(isFlag==true)
                {
                   alert("此客户等级名称已存在，请重新输入！");
                   ajaxPages.init(2);
                }
                else{
                    $.ajax
                    ({
                        url: "AjaxByCustormerProList.aspx?EidtID=" + ID + "&CustormerName=" + escape(CustormerName),
                        cache: false,
                        success: function(html) {
                            alert("修改成功");
                            ajaxPages.init(2);
                        }
                    });
                 }
            }
        }
        return false;
    }
    //新增客户等级
    function AddCustomerPro() {
        var CustormerName =$.trim($("#txtCustomerName").val());

        if (CustormerName== "") {
            $("#errMsg_txtCustomerName").html("请输入客户等级名称！");
        }else{
                var isFlag=false; 
                $("#tbl_CusList").children().find("input[name]='LevleName'").each(function(){
                      if($(this).attr("value")==CustormerName)
                        {
                           isFlag=true;
                        }
                });
                if(isFlag==true)
                {
                   alert("此客户等级名称已存在，请重新输入！");
                   $("#txtCustomerName").val("");
                }
                else{
                    $.ajax
                    ({
                        url: "AjaxByCustormerProList.aspx?EidtID=&CustormerName=" + encodeURIComponent(CustormerName),
                        cache: false,
                        success: function(html) {

                            alert("新增成功");
                            ajaxPages.init(2);
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
            &nbsp;客户等级名称：
            <input id="txtCustomerName" name="txtCustomerName" maxlength="10" />
            <input id="btnCustomerAdd" runat="server" onclick="AddCustomerPro();" type="button" value="添加" class="an_tijiaobaocun" /><span
                id="errMsg_txtCustomerName" style="color: Red"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table>
<div  id="tbl_CusList" >
<asp:datalist id="dalList" runat="server" repeatcolumns="3" borderstyle="None" gridlines="Horizontal"
    onitemdatabound="dalList_ItemDataBound">
        <ItemTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td><asp:Label ID="lblAutoNumber" runat="server"></asp:Label></td>
                    <td align="center">   
                        <input name="LevleName" maxlength="10"  type="text" class="textfield" value="<%# DataBinder.Eval(Container.DataItem,"FieldName") %>">
                    </td>
                    <td align="center">
                        <%# CreateOperation(Convert.ToString(DataBinder.Eval(Container.DataItem, "FieldId")), Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDefault")))%>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:datalist>
    </div>
