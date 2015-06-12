<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FitList.aspx.cs" Inherits="SiteOperationsCenter.LineManage.FitList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>订单列表</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <%@ import namespace="EyouSoft.Common" %>
</head>
<body>
    <form id="form1" runat="server">
    <!----订单状态---->
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" class="zhtai_bg">
                <span class="guestmenu">订单状态</span>
                <%=BindOrderStatus()%>
            </td>
        </tr>
        <tr>
            <td align="left" class="zhtai_bg">
                <span class="guestmenu">支付状态</span>
                <%=BindPaymentStatus()%>
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 1px;">
        <tr>
            <td align="left" class="zhtai_bg">
                <%--class="font_f00"--%>
                排序：<a href="#" onclick="FitManage.sortBytime(this,1)">按出发时间排序</a> <a href="#" onclick="FitManage.sortBytime(this,2)">
                    按下单时间排序</a> <a href="#" onclick="FitManage.sortBytime(this,3)">按订单状态排序</a>
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="search_bg">
                关键字
                <input name="txt_Search" runat="server" style="color: #999; width: 265px;" id="txt_Search"
                    type="text" value="订单号、线路名、专线商、组团社、游客" size="30" onfocus="$(this).css('color', 'black');if(this.value == '订单号、线路名、专线商、组团社、游客') {this.value = '';}"
                    onblur="if (this.value == '') {$(this).css('color', '#999');this.value = '订单号、线路名、专线商、组团社、游客';}" />
                出发时间
                <input type="text" id="txtStartDate" name="StartDate" class="size55" style="width: 85px;"
                    onfocus="WdatePicker();" />至<input type="text" id="txtEndDate" name="EndDate" style="width: 85px;"
                        class="size55" onfocus="WdatePicker();" />
            </td>
        </tr>
        <tr>
            <td class="search_bg">
                线路类型
                <asp:DropDownList ID="DropSearchLineType" runat="server" onchange="OnchangeWord(this.value,'GetLineByType')">
                </asp:DropDownList>
                专线：
                <asp:DropDownList ID="DropSearchLineId" runat="server" onchange="OnchangeWord(this.value,'GetCompanyByLine')">
                    <asp:ListItem Value="0">专线</asp:ListItem>
                </asp:DropDownList>
                专线商：
                <asp:DropDownList ID="dropBusinessLineId" runat="server">
                    <asp:ListItem Value="0">专线商</asp:ListItem>
                </asp:DropDownList>
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/chaxun.gif" width="62" height="21"
                    onclick="FitManage.OnSearch()" />
            </td>
        </tr>
    </table>
    <!--分页-->
    <div id="divFitList" align="center">
    </div>
    <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript">
    
     var type=null;
        //获取选择
        function IsSelectAdv() {
            var num = 0;
            var id = "";
            $("input[name='checkboxFit']").each(function() {
                if ($(this).attr("checked")) {
                    num++;
                    id += $(this).val()+"$";
                }
            });
            if (num == 0) {
                return 0;
            }
            else {
                return id;
            }
        }
    
        //Line1为专线（国内，国外，周边） line2为（所选专线区域） BusinessLine专线商 Departure为出发地
        //OrderStatus为订单状态 PaymentStatus为支付状态 时间排序（0为默认值，1为按出发时间排序 2为按下单时间排序 3为按订单状态排序）
        var Parms = { SearchKeyword: "", Line1: -1,Line2:0,BusinessLine: 0,tourid:"",StartDate:"",EndDate:"",OrderStatus:-1,PaymentStatus:0,Sort:1,Page: 1 };
        var FitManage = {//景区列表
            GetFitList: function() {
                 if(<%=currentPage %> >0 ){
                     Parms.Page=<%=currentPage %>;
                 }
                LoadingImg.ShowLoading("divFitList");
                if (LoadingImg.IsLoadAddDataToDiv("divFitList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxFitList.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            
                            $("#divFitList").html(html);
                        }
                    });
                }
            },
            
            
            //根据订单状态查询
            OrderStatus:function(obj,v){
                $(obj).closest("td").find("a").each(function(){
                    if($(this).css("color")=="red")
                    {
                        $(this).css("color","");
                    }                
                })
                $(obj).css("color","red");
                Parms.OrderStatus=v;
                FitManage.OnSearch();
            },
            
            
            //根据支付状态查询
            //1未支付 2待支付 3支付定金 4支付全款 5结账 6退款
            PaymentStatus:function(obj,v){
                $(obj).closest("td").find("a").each(function(){
                    if($(this).css("color")=="red")
                    {
                        $(this).css("color","");
                    }                
                })
                $(obj).css("color","red");
                Parms.PaymentStatus=v;
                FitManage.OnSearch();
            },
            
            //按照时间排序
            //时间排序（0为默认值，1为按出发时间排序 2为按下单时间排序 3为按订单状态排序）
           sortBytime:function(obj,v){
                $(obj).closest("td").find("a").each(function(){
                    if($(this).css("color")=="red")
                    {
                        $(this).css("color","");
                    }                
                })
                $(obj).css("color","red");
                Parms.Sort=v;
                FitManage.OnSearch();
           },
           
           

           ckAllCompany: function(obj) {//全选
                $("#tbCompanyList").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            },
            
            
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Parms.Page = Page;
                this.GetFitList();
            },
            OnSearch: function() {//查询
                if($("#<%=txt_Search.ClientID%>").val()!="订单号、线路名、专线商、组团社、游客")
                    Parms.SearchKeyword = $("#<%=txt_Search.ClientID%>").val();
                Parms.Line1 = $("#<%=DropSearchLineType.ClientID%>").val();
                if('<%=Areaid%>'!="")
                {
                    Parms.Line2='<%=Areaid%>';
                    $("#DropSearchLineType").val('<%=AreaType%>');
                }
                else 
                    Parms.Line2=$("#<%=DropSearchLineId.ClientID%>").val();                
                Parms.BusinessLine=$("#<%=dropBusinessLineId.ClientID%>").val();
                if('<%=tourid %>'!="")
                        Parms.tourid='<%=tourid %>';
                Parms.StartDate = $.trim($("#txtStartDate").val());
                Parms.EndDate = $.trim($("#txtEndDate").val());
                Parms.Page = 1;
                FitManage.GetFitList();
            }
     }
        $(document).ready(function() {
            var FormObj = $("#<%=form1.ClientID%>");

            //回车查询
            FormObj.keydown(function(event) {
                if (event.keyCode == 13) {
                    FitManage.OnSearch();
                    return false;
                }
            });
            FitManage.OnSearch();
        });        
    
    
    </script>

    <script type="text/javascript">
    
     //专线类型（国内国际周边等等）
        function OnchangeWord(v,t)
        {
             $.ajax({
                 url: "FitList.aspx?action="+t+"&argument="+v,
                 cache: false,
                 type:"POST",
                 dataType:"json",
                 success: function(result) {
                     if(t=="GetLineByType"){
                         $("#<%=DropSearchLineId.ClientID %>").html("");
                         $("#<%=DropSearchLineId.ClientID %>").append("<option value=\"0\">专线</option>"); 
                         var list = result.tolist;
                         for(var i=0;i<list.length;i++)
                         {
                             $("#<%=DropSearchLineId.ClientID %>").append("<option value=\""+list[i].AreaId+"\">"+list[i].AreaName+"</option>"); 
                         }
                     }
                     else if(t=="GetCompanyByLine")
                     {
                         $("#<%=dropBusinessLineId.ClientID %>").html("");
                         $("#<%=dropBusinessLineId.ClientID %>").append("<option value=\"0\">专线商</option>"); 
                         var listBusinessLineId = result.tolist;
                         for(var j=0;j<listBusinessLineId.length;j++)
                         {
                             $("#<%=dropBusinessLineId.ClientID %>").append("<option value=\""+listBusinessLineId[j].ID+"\">"+listBusinessLineId[j].CompanyName+"</option>"); 
                         }
                     }
                 },
                 error: function() {
                     alert("操作失败!");
                 }
             });
        }
        
    </script>

    </form>
</body>
</html>
