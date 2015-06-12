<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrModifyTickets.aspx.cs" Inherits="UserBackCenter.ScenicManage.AddOrModifyTickets" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script> 
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc" class="margintop5" style="width:100%;">
          <tr>
            <td width="19%" align="right" bgcolor="#f2f9fe">所属景区：</td>
            <td align="left">
                <asp:DropDownList ID="ddlScenic" Width="120" valid="required" errmsg="请选择景区" runat="server">
                </asp:DropDownList>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#f2f9fe" >门票类型名称：</td>
            <td align="left"><input type="text" runat="server" valid="required" maxlength="20" style="width:200px;" errmsg="请填写门票类型名称" id="txtTypeName" name="textfield42" />
                             <span style="color:Red;">(必填)长度不能超过20个字符</span>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#f2f9fe" >英文名称：</td>
            <td align="left"><input type="text" runat="server" style="width:200px;" maxlength="100" id="txtEnName" name="textfield42" />
            </td>
          </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">门市价：</td>
            <td align="left"><input name="textfield22" type="text" valid="required|IsDecimalTwo" errmsg="门市价不能为空|门市价只能为数值" size="10" runat="server" id="txtRetailPrice" />
              元<span style="color:Red;">(必填)</span>
                <input type="hidden" id="hidMSPrice" runat="server" /></td>
          </tr>
          <tr>
            <td align="right" bgcolor="#f2f9fe">网站优惠价：</td>
            <td align="left"><input name="textfield222" type="text" valid="IsDecimalTwo" errmsg="网站优惠价只能为数值" size="10" runat="server" id="txtWebsitePrices" />
        元<input type="hidden" id="hidWZPrice" runat="server" /></td>
          </tr>
          <tr>
            <td align="right" bgcolor="#f2f9fe">市场限制最低价：</td>
            <td width="81%" align="left"><input valid="IsDecimalTwo" errmsg="市场限制最低价只能为数值" type="text" runat="server" id="txtMarketPrice" size="10" />
        元<input type="hidden" id="hidSCPrice" runat="server" /></td>
          </tr>
          <tr>
            <td align="right" bgcolor="#f2f9fe">同行分销价：</td>
            <td align="left"><input valid="IsDecimalTwo" errmsg="同行分销价只能为数值" type="text" runat="server" id="txtDistributionPrice" size="10" />
        元<input type="hidden" id="hidTHPrice" runat="server" /></td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">最少限制：</td>
            <td align="left"><input valid="RegInteger" errmsg="最少限制只能为整型" runat="server" id="txtLimit" type="text" size="10" />
            （张/套）</td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">支付方式： </td>
            <td align="left">
                <asp:RadioButtonList ID="rdoPayment" RepeatDirection="Horizontal" valid="required|Radioed" errmsg="请选择支付方式" runat="server">
                </asp:RadioButtonList>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">票价有效时间段：</td>
            <td align="left"><input name="textfield10" type="text" onfocus="WdatePicker()" id="txtStartTime" runat="server" size="10" />
        至
          <input name="textfield11" type="text" id="txtEndTime" onfocus="WdatePicker()" runat="server" size="10" />
        （不写为无时间限制）</td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">门票说明：</td>
            <td align="left"><textarea id="txtDescription" runat="server" cols="50" rows="3"></textarea></td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">同业销售须知：</td>
            <td align="left"><textarea id="txtSaleDescription" runat="server" cols="50" rows="3"></textarea>
            （只有同业分销商能看到）</td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">状态：</td>
            <td align="left">
                <asp:Label runat="server" ID="lblExamineStatus"></asp:Label>
                <asp:DropDownList ID="ddlStatus" runat="server" valid="required" errmsg="请选择状态">
                </asp:DropDownList>
             </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">门票类型排序：</td>
            <td align="left"><input id="txtCustomOrder" runat="server" type="text" size="10" value="9" valid="required|RegInteger" errmsg="请填写门票类型排序|门票类型排序只能为整型" />
        按照0，1，2顺序排列，前3位在列表时显示更突出</td>
          </tr>
          <%--<tr>
            <td align="right" bgcolor="#F2F9FE">B2B显示控制：</td>
            <td align="left">
                <asp:DropDownList ID="ddlB2B" runat="server" valid="required" errmsg="请选择B2B显示控制">
                </asp:DropDownList>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">B2B排序值：</td>
            <td align="left">
                <input type="text" id="txtB2BOrder" valid="required|RegInteger" errmsg="请填写B2B排序值|B2B排序值只能为整型" runat="server" />
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">B2C显示控制：</td>
            <td align="left">
                <asp:DropDownList ID="ddlB2C" runat="server" valid="required" errmsg="请选择B2C显示控制">
                </asp:DropDownList>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#F2F9FE">B2C排序值：</td>
            <td align="left">
                <input type="text" id="txtB2COrder" runat="server" valid="required|RegInteger" errmsg="请填写B2C排序值|B2C排序值只能为整型"  />
            </td>
          </tr>--%>
          <tr>
            <td colspan="2" align="center" bgcolor="#F2F9FE">
                <asp:Button ID="btnCommit" runat="server" Text=" 提交 " 
                    onclick="btnCommit_Click" />
            </td>
          </tr>
        </table>
        <input type="hidden" id="hfId" runat="server" />
    </form>
    <script type="text/javascript">
        function DecimalComparison(value,value1){
            var isGn = true;
            if(value == "" || value1 == ""){
                isGn = true;
            }
            else if(value >= value1){
                isGn = true;
            }
            else {
                isGn = false;
            }
            return isGn;
        }
        $(function(){
            $("#btnCommit").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "alert")) {
                        var start = $("#txtStartTime").val();
                        var end = $("#txtEndTime").val();
                        if(start != "" || end != ""){
                            if(start == "" || end <= start){
                                alert("结束时间要大于开始时间");
                                return false;
                            }
                        }
                        var description = $("#txtDescription").val();
                        if(description.length > 500){
                            alert("门票说明不能超过500个字符!");
                            return false;
                        }
                        var saleDescription = $("#txtSaleDescription").val();
                        if(saleDescription.length > 500){
                            alert("同业销售须知不能超过500个字符!");
                            return false;
                        }
                        //同业价<最低限价<优惠价<门市价
                        var txtRetailPrice = $("#txtRetailPrice").val() == "" ? 0:parseFloat($("#txtRetailPrice").val()); //门市价
                        var txtWebsitePrices = $("#txtWebsitePrices").val() == ""?0: parseFloat($("#txtWebsitePrices").val()); //网站优惠价
                        var txtMarketPrice = $("#txtMarketPrice").val() == "" ?0:parseFloat($("#txtMarketPrice").val());     //市场最低价
                        var txtDistributionPrice = $("#txtDistributionPrice").val() == ""?0:parseFloat($("#txtDistributionPrice").val()); //同行价
                            
                        var oldmsp = $("#<%= hidMSPrice.ClientID %>").val() == ""?0:parseFloat($("#<%= hidMSPrice.ClientID %>").val());  //修改前门市价
                        var oldyhp = $("#<%= hidWZPrice.ClientID %>").val() == ""?0:parseFloat($("#<%= hidWZPrice.ClientID %>").val());;   //修改前优惠价
                        var oldzdp = $("#<%= hidSCPrice.ClientID %>").val() == ""?0:parseFloat($("#<%= hidSCPrice.ClientID %>").val());;   //修改前最低价
                        var oldthp = $("#<%= hidTHPrice.ClientID %>").val() == ""?0:parseFloat($("#<%= hidTHPrice.ClientID %>").val());;   //修改前同行价
                        var isGn1 = DecimalComparison(txtRetailPrice,txtWebsitePrices);
                        var isGn2 = DecimalComparison(txtRetailPrice,txtMarketPrice);
                        var isGn3 = DecimalComparison(txtRetailPrice,txtDistributionPrice);
                        
                        var isGn4 = DecimalComparison(txtWebsitePrices,txtMarketPrice);
                        var isGn5 = DecimalComparison(txtWebsitePrices,txtDistributionPrice);
                         
                        var isGn6 = DecimalComparison(txtMarketPrice,txtDistributionPrice);
                        
                        if(!isGn1 || !isGn2 || !isGn3 || !isGn4 || !isGn5 || !isGn6){
                            alert("同行分销价<市场限制最低价<网站优惠价<门市价");
                            return false;
                        }else{
                            if((txtRetailPrice != oldmsp ||
                                txtMarketPrice != oldyhp ||
                                txtWebsitePrices != oldzdp ||
                                txtDistributionPrice != oldthp) && $("#hfId").val() != "")
                            {
                                if(confirm("由于门票价格的修改导致要重新审核门票\n确定要提交更改吗？") == true){
                                    return true;
                                }else{
                                    return false;
                                }
                            }else{
                                return true;
                            }
                        }
                   }
                   else
                   {
                    return false
                   }
                })
        
        });
        
    
    </script>
</body>
</html>
