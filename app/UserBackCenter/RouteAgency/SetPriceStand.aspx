<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPriceStand.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.SetPriceStand" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置价格等级</title>
    <style type="text/css">
        body
        {
            margin: auto 0;
            padding: 0;
            background-color: White;
        }
        td
        {
            font-size: 12px;
            line-height: 120%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" bgcolor="#FFFFFF">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2" align="center" style="line-height: 25px;">
                <strong>设置常用价格等级</strong>：（五类）<br />
                <table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="6" align="center">
                            <strong>按酒店等级分类</strong>
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox" value="COMPRICE-0000-0000-0000-000000000001" />
                        </td>
                        <td width="20%" align="left" bgcolor="#FFFFFF">
                            二星
                        </td>
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td width="20%" align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td width="20%" align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox2" id="22" value="COMPRICE-0000-0000-0000-000000000002" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            三星
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox6" value="COMPRICE-0000-0000-0000-000000000003" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            准三星
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox62" value="COMPRICE-0000-0000-0000-000000000004" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            挂三星
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox3" value="COMPRICE-0000-0000-0000-000000000005" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            四星
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox7" value="COMPRICE-0000-0000-0000-000000000006" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            准四星
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox63" value="COMPRICE-0000-0000-0000-000000000007" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            挂四星
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox4" value="COMPRICE-0000-0000-0000-000000000008" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            五星
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox8" value="COMPRICE-0000-0000-0000-000000000009" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            准五星
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0"
                    style="margin-top: 2px;">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="6" align="center">
                            <strong>按火车铺位分类</strong>
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox5" value="COMPRICE-0000-0000-0000-000000000010" />
                        </td>
                        <td width="20%" align="left" bgcolor="#FFFFFF">
                            硬座
                        </td>
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox652" value="COMPRICE-0000-0000-0000-000000000011" />
                        </td>
                        <td width="20%" align="left" bgcolor="#FFFFFF">
                            软座
                        </td>
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td width="20%" align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox22" value="COMPRICE-0000-0000-0000-000000000012" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            硬卧上
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox65" value="COMPRICE-0000-0000-0000-000000000013" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            硬卧中
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox622" value="COMPRICE-0000-0000-0000-000000000014" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            硬卧下
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox32" value="COMPRICE-0000-0000-0000-000000000015" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            软卧上
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox72" value="COMPRICE-0000-0000-0000-000000000016" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            软卧下
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0"
                    style="margin-top: 2px;">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="6" align="center">
                            <strong>按飞机舱位分类</strong>
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox52" value="COMPRICE-0000-0000-0000-000000000017" />
                        </td>
                        <td width="20%" align="left" bgcolor="#FFFFFF">
                            经济舱
                        </td>
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox6522" value="COMPRICE-0000-0000-0000-000000000018" />
                        </td>
                        <td width="20%" align="left" bgcolor="#FFFFFF">
                            商务舱
                        </td>
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox65222" value="COMPRICE-0000-0000-0000-000000000019" />
                        </td>
                        <td width="20%" align="left" bgcolor="#FFFFFF">
                            头等舱
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0"
                    style="margin-top: 2px;">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="6" align="center">
                            <strong>按轮船舱位分类</strong>
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox53" value="COMPRICE-0000-0000-0000-000000000020" />
                        </td>
                        <td width="20%" align="left" bgcolor="#FFFFFF">
                            一等舱上
                        </td>
                        <td width="10%" align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox6523" value="COMPRICE-0000-0000-0000-000000000021" />
                        </td>
                        <td width="20%" align="left" bgcolor="#FFFFFF">
                            一等舱下
                        </td>
                        <td width="10%" align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td width="20%" align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox222" value="COMPRICE-0000-0000-0000-000000000022" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            二等舱上
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox653" value="COMPRICE-0000-0000-0000-000000000023" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            二等舱下
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="baidi">
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox322" value="COMPRICE-0000-0000-0000-000000000024" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            三等舱上
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <input type="checkbox" name="checkbox722" value="COMPRICE-0000-0000-0000-000000000025" />
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            三等舱下
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0" id="tblPriceStand"
                    style="margin-top: 2px;">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="6" align="center">
                            <strong>按其他分类</strong>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptPriceStand" runat="server" 
                        onitemdatabound="rptPriceStand_ItemDataBound">
                        <ItemTemplate>
                            <asp:Literal ID="ltrTR" runat="server" Text="<TR class=baidi>" Visible="false"></asp:Literal>
                                <td width="10%" align="right" bgcolor="#FFFFFF">
                                    <input type="checkbox" name="checkbox522" value='<%# DataBinder.Eval(Container.DataItem,"ID") %>' />
                                </td>
                                <td width="20%" align="left" bgcolor="#FFFFFF">
                                    <%# DataBinder.Eval(Container.DataItem, "PriceStandName")%>
                                </td>
                                <asp:Literal ID="ltrBTR" runat="server" Text="</TR>" Visible="false"></asp:Literal>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="6">
                    <tr>
                        <td width="20%" align="center">
                            <input type="button" id="btnSubmit" name="btnSubmit" onclick="return SubmitCkeck();"
                                value=" 保存设置 " style="height: 28px; font-weight: bold; color: #990000" />
                        </td>
                        <td width="70%" align="center">
                            没找到您要的价格等级，请联系我们
                            <input name="txtPriceStandName" id="txtPriceStandName" type="text" style="height: 15px;
                                padding-top: 3px; border: 1px solid #A7A6AA; color: #666;" size="12" />
                            <input type="button" name="btnAddPrice" value="提交" onclick="return AddCheck();" />
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="hidCkPriceId" name="hidCkPriceId" />
                <input type="hidden" id="hidCkPriceName" name="hidCkPriceName" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
    
     function SetMyPrice(){
        var MyPriceStand="<%=MyPriceStand %>";
        if(MyPriceStand!=""){
            for(var i=0;i<MyPriceStand.split(",").length;i++){
                $("input[type='checkbox']").each(function(){
                    if($(this).val()==MyPriceStand.split(",")[i]){
                        $(this).attr("checked","checked");
                        return false;
                    }
                });
            }
        }
     }
     
     function SubmitCkeck()
     {
        var arrCk=new Array();
        var arrCkPriceName=new Array();
        $("input[type='checkbox']:checked").each(function(){
               //if($(this).attr("checked")){
                   arrCk.push($(this).val());
                   arrCkPriceName.push($.trim($(this).parent().next().html()));
               //}
          });
             
          if(arrCk.length>0){
             $("#hidCkPriceId").val(arrCk.toString());
             $("#hidCkPriceName").val(arrCkPriceName.toString());
             
             if('<%=IsCompanyCheck %>' == 'False')
             {
                alert('对不起，您的账号未审核，不能进行操作!');
                return false;
             }
             $.ajax({
	            type:"POST",
	            url:"/RouteAgency/SetPriceStand.aspx?flag=save&IdList="+ encodeURI(arrCk.join(',')) +"&priceNameList="+ encodeURI(arrCkPriceName.join(',')),
	            cache:false,
	            success:function(html){
	                if(html != ""){
	                    alert("设置成功!");
	                    parent.TourPriceStand.SetNewPrice(html,'<%=ContainerID %>');
		                var frameid=window.parent.Boxy.queryString("iframeId")
                        window.parent.Boxy.getIframeDialog(frameid).hide();
	                }else{
	                    alert("设置失败!");
	                }
	            }
	        });
          }else{
             alert("请选择价格等级!"); 
             return false;
          }
         
     }
     function AddCheck(){
        var PriceName=$.trim($("#txtPriceStandName").val());
        
        if(PriceName.length==0){
            alert("请输入价格等级名称！");
            return false;
        }else{
            if('<%=IsCompanyCheck %>' == 'False')
             {
                alert('对不起，您的账号未审核，不能进行操作!');
                return false;
             }
             
            $.ajax({
	            type:"POST",
	            url:"/RouteAgency/SetPriceStand.aspx?flag=add&PriceName="+ encodeURI(PriceName),
	            cache:false,
	            success:function(html){
	                if(html){
	                    alert("添加成功，请等待审核!");
	                    $("#txtPriceStandName").val('');
	                }else{
	                    alert("添加失败!");
	                }
	            }
	        });
        }
     }
     
     $("input[type=checkbox]").each(function(){
        $(this).bind("click",function(){
            CheckIsUsing($(this));
        });        
     });
     
     function CheckIsUsing(obj){
        if(!obj.attr("checked")){
            var pricestandID = $(obj).val();
            $.ajax({
                type:"POST",
                url:"/RouteAgency/SetPriceStand.aspx?flag=click&pricestandID="+ encodeURI(pricestandID),
                cache:false,
                success:function(html){
                    if(html == "True"){
                        alert("此价格等级正在使用不能取消!");
                        $(obj).attr("checked","checked");
                        return false;
                    }
                }
            });
        }
     }
     
     $(document).ready(function(){
        var count = '<%=OtherPriceCount %>';
        var oid = parseInt(count)%3;
        if(oid > 0)
        {
            var str='';
            for(var i = 0; i < 6-oid*2; i ++ )
            {
                str += "<td align=\"center\" bgcolor=\"#FFFFFF\">";
            }
            str += "</tr>";
            $("#tblPriceStand").find("tr:last").append(str);
        }
        
        SetMyPrice();
     });
    </script>

    </form>
</body>
</html>
