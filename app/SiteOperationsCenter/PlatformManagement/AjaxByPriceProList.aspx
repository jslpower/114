<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxByPriceProList.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.AjaxByPriceProList" %>

<%@ Import Namespace="EyouSoft.Common" %>

<script language="javascript" type="text/javascript">
    //删除报价等级
    function DeletePricePro(PriceId) {
            if (confirm('您确定要删除此报价等级吗？\n\n此操作不可恢复！')) {
            $.ajax
            ({
                url: "AjaxByPriceProList.aspx?DeletID=" + PriceId,
                cache: false,
                success: function(html) {
                    alert("删除成功");
                    ajaxPages.init(1);
                }
            });
        }
    }
    //修改报价等级
    function EidtPricePro(objId, ID) {
        if (confirm('确定要修改此数据吗?')) {
            var obj = document.getElementById(objId);
              var PriceName = $(obj).closest("table").find("input").val();
            if (PriceName == "") {
                alert("报价等级名称不能为空！");
            }else{
                var isFlag=false; 
                $("#tbl_PriceList").children().find("input[name]='LevleName'").each(function(){
                    var length = $(this).closest("tr").find("a[id='"+objId+"']").length;  
                  if(length>0){
                    return true;
                  }
                      if($(this).attr("value")==PriceName)
                        {
                           isFlag=true;
                           return false;
                        }
                });
                if(isFlag==true)
                {
                       alert("此报价等级名称已存在，请重新输入！");
                       ajaxPages.init(1);
                }
                else{
                    $.ajax
                    ({
                        url: "AjaxByPriceProList.aspx?EidtID=" + ID + "&PriceName=" + escape(PriceName),
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
    function AddPricePro() {
        var PriceName =$.trim($("#txtPName").val());

        if (PriceName== "") {
            $("#errMsg_txtPriceName").html("请输入报价等级名称！");
        }else{
                var isFlag=false; 
                $("#tbl_PriceList").children().find("input[name]='LevleName'").each(function(){
                      if($(this).attr("value")==PriceName)
                        {
                           isFlag=true;
                        }
                });
                if(isFlag==true)
                {
                       alert("此报价等级名称已存在，请重新输入！");
                       $("#txtPName").val("");
                }
                else{
                    $.ajax
                    ({
                        url: "AjaxByPriceProList.aspx?EidtID=&PriceName=" + encodeURIComponent(PriceName),
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
            &nbsp;报价等级名称：
            <input id="txtPName" name="txtPName"  maxlength="10" />
            <input id="btnPriceAdd" runat="server" onclick="AddPricePro();" type="button" value="添加" class="an_tijiaobaocun" /><span
                id="errMsg_txtPriceName" style="color: Red"></span>
        </td>
    </tr>
</table>
<div  id="tbl_PriceList" >
<asp:datalist id="dalList" runat="server" repeatcolumns="3" borderstyle="None" gridlines="Horizontal"
    onitemdatabound="dalList_ItemDataBound">
        <ItemTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td>
                        <asp:Label ID="lblAutoNumber" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        <input name="LevleName" maxlength="10"  type="text" class="textfield" value="<%# DataBinder.Eval(Container.DataItem,"PriceStandName") %>">
                    </td>
                    <td align="center">
                        <%# CreateOperation(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")))%>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:datalist>
</div>
