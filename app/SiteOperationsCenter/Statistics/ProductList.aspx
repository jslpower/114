<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="SiteOperationsCenter.Statistics.ProductList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #btnCancle
        {
            width: 86px;
        }
    </style>

    <script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF9E7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = "";
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                    <span id="spanArea" runat="server">线路区域
                        <select id="pl_selRouteArea" runat="server">
                        </select></span> 线路名称
                    <input type="text" class="textfield" size="12" id="pl_txtRouteName" runat="server" />
                    <a id="btnSearch" href="javascript:void(0);" onclick="ProductList.GetOrderList(-1,this);">
                        <img width="62" height="21" style="margin-bottom: -3px; cursor: pointer;" src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" /></a>
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>序号</strong>
                </td>
                <td width="15%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>线路区域</strong>
                </td>
                <td align="left" valign="middle" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>线路名称&nbsp;<asp:DropDownList ID="drpOrderByRouteOrDate" runat="server">
                        <asp:ListItem Value="0">按线路行程</asp:ListItem>
                        <asp:ListItem Value="1">按出团时间</asp:ListItem>
                    </asp:DropDownList>
                    </strong>
                </td>
                <td width="11%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong><a href="javascript:void(0);" style="cursor: pointer" class="DataListDownGray"
                        id="a_Order_0" title="点击进行排序" onclick="ProductList.GetOrderList(0);">成交订单</a></strong>
                </td>
                <td width="11%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong><a href="javascript:void(0);" style="cursor: pointer" class="DataListDownGray"
                        id="a_Order_1" title="点击进行排序" onclick="ProductList.GetOrderList(1);">留位订单</a></strong>
                </td>
                <td width="11%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong><a href="javascript:void(0);" style="cursor: pointer" class="DataListDownGray"
                        id="a_Order_2" title="点击进行排序" onclick="ProductList.GetOrderList(2);">留位过期</a></strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong><a href="javascript:void(0);" style="cursor: pointer" class="DataListDownGray"
                        id="a_Order_3" title="点击进行排序" onclick="ProductList.GetOrderList(3);">不受理订单</a></strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong><a href="javascript:void(0);" style="cursor: pointer" class="DataListDownGray"
                        id="a_Order_4" title="点击进行排序" onclick="ProductList.GetOrderList(4);">被查看次数</a></strong>
                </td>
            </tr>
            <asp:Repeater ID="rptTourStatByOrderInfo" runat="server">
                <ItemTemplate>
                    <tr class="baidi" height="25" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                        <td align="center">
                            <%# GetCount() %>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem,"AreaName") %>
                        </td>
                        <td align="left">
                            <%# GetRouteName(DataBinder.Eval(Container.DataItem, "ID").ToString(), DataBinder.Eval(Container.DataItem, "RouteName").ToString(), DataBinder.Eval(Container.DataItem, "TourNo").ToString())%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "OrdainNum")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "SaveSeatNum")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "SaveSeatExpiredNum")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "NotAcceptedNum")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "Clicks")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr bgcolor="#F3F7FF" height="25" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                        <td align="center">
                            <%# GetCount() %>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem,"AreaName") %>
                        </td>
                        <td align="left">
                            <%# GetRouteName(DataBinder.Eval(Container.DataItem, "ID").ToString(), DataBinder.Eval(Container.DataItem, "RouteName").ToString(),DataBinder.Eval(Container.DataItem,"TourNo").ToString())%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "OrdainNum")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "SaveSeatNum")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "SaveSeatExpiredNum")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "NotAcceptedNum")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "Clicks")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <tr id="tr_NoData" runat="server">
                <td style="height: 50px;" colspan="8" align="center">
                    暂无数据!
                </td>
            </tr>
        </table>
        <table width="99%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="30" align="right">
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </td>
            </tr>
        </table>
        <table width="50%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <input type="button" value="取   消" class="baocunan_an" id="btnCancle" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>

<script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

<script type="text/javascript">
    var URL = "ProductList.aspx?companyId=<%=CompanyID %>&areaid=<%=AreaID %>&startTime=<%=StartTime %>&endTime=<%=EndTime %>";
    var ProductList = {
        GetOrderList: function(sortid){
            var strUrl = URL;
            var radioVal = $("#<%=drpOrderByRouteOrDate.ClientID %>").val();
            if(radioVal == '0')
            {
                strUrl += "&DisType=0";
            }
            var strAreaId = $("#<%=pl_selRouteArea.ClientID %>").val();
            if(strAreaId != null)
            {
                strUrl += "&strareaId=" + strAreaId;
            }
            var RouteName = $("#<%=pl_txtRouteName.ClientID %>").val();
            strUrl += "&RouteName="+ escape(RouteName);
            strUrl += "&SortID="+ sortid +"&OldSortID=<%=OldSortId %>";
            window.location.href = strUrl;
        },
        GetList: function(typeid){
            var strUrl = URL;
            var strAreaId = $("#<%=pl_selRouteArea.ClientID %>").val();
            if(strAreaId != null)
            {
                strUrl += "&strareaId=" + strAreaId;
            }
            var RouteName = $("#<%=pl_txtRouteName.ClientID %>").val();
            strUrl += "&RouteName="+ escape(RouteName);
            strUrl += "&DisType="+ typeid;
            window.location.href = strUrl;
        }
    }
    
    $("#<%=pl_txtRouteName.ClientID %>").bind("keypress",function(e){
        if(e.keyCode == 13){
            ProductList.GetOrderList();
        }
    });
    
    var order_0 = '<%=Order_0 %>',order_1 = '<%=Order_1 %>',order_2 = '<%=Order_2 %>',order_3 = '<%=Order_3 %>',order_4 = '<%=Order_4 %>';
    var sortid = '<%=SortID %>';
    if(sortid == '')
    {
        $("a[id^= id=a_Order]").attr("class","DataListDownGray");
    }else{
        switch(sortid)
        {
            case "0":
                if(order_0 == '0'){
                    $("a[id=a_Order_0]").removeAttr("class").attr("class","DataListUpGreen");
                }else{
                    $("a[id=a_Order_0]").removeAttr("class").attr("class","DataListDownGreen");
                }
                break;
            case "1":
                if(order_1 == '2' || order_1 == '0'){
                    $("a[id=a_Order_1]").removeAttr("class").attr("class","DataListUpGreen");
                }else{
                    $("a[id=a_Order_1]").removeAttr("class").attr("class","DataListDownGreen");
                }
                break;
           case "2":
                if(order_2 == '4' || order_2 == '0'){
                    $("a[id=a_Order_2]").removeAttr("class").attr("class","DataListUpGreen");
                }else{
                    $("a[id=a_Order_2]").removeAttr("class").attr("class","DataListDownGreen");
                }
                break;
           case "3":
                if(order_3 == '6' || order_3 == '0'){
                    $("a[id=a_Order_3]").removeAttr("class").attr("class","DataListUpGreen");
                }else{
                    $("a[id=a_Order_3]").removeAttr("class").attr("class","DataListDownGreen");
                }
                break;
           case "4":
                if(order_4 == '8' || order_4 == '0'){
                    $("a[id=a_Order_4]").removeAttr("class").attr("class","DataListUpGreen");
                }else{
                    $("a[id=a_Order_4]").removeAttr("class").attr("class","DataListDownGreen");
                }
                break;           
        }
    }
    
    $("#btnCancle").click(function(){
        var frameid=window.parent.Boxy.queryString("iframeId")
        window.parent.Boxy.getIframeDialog(frameid).hide();
    });
</script>

</html>
