<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllOrderList.aspx.cs" Inherits="SiteOperationsCenter.Statistics.AgencyActionAnalysis.AllOrderList" %>
<%@ Register src="../../usercontrol/StartAndEndDate.ascx" tagname="StartAndEndDate" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>组团社行为分析</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="59%" valign="top">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
                    margin-bottom: 3px;">
                    <tr>
                        <td width="15" style="border-bottom: 1px solid #62A8E4">
                            &nbsp;
                        </td>
                        <td width="105" height="24" background="<%=ImageServerUrl %>/images/yunying/weichulidingdan.gif" align="center">
                            <strong class="shenglanz">零售商全部预订</strong>
                        </td>
                        <td align="left" style="border-bottom: 1px solid #62A8E4">
                            <a href="Default.aspx">点击查看零售商预订Top10</a>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                            <table width="100%" border="0" align="left" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td id="tdOrder">
                                    单位名称：<input type="text" id="txtOrderCompanyName" name="txtOrderCompanyName" />
                                        <uc1:StartAndEndDate ID="OrderDate" runat="server" />预订状态：<select name="select2" id="OrderTypeList" class="textfield">
                                            <option value="">所有</option>
                                            <option value="5">预订成功</option>
                                            <option value="2">留位订单</option>
                                            <option value="3">留位过期</option>
                                            <option value="4">不受理订单</option>
                                        </select>&nbsp;<img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom:-3px;cursor:pointer" id="imgBtnOrderSearch" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
               <div id="divCompanyOrderList" align="center"></div>
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    <script type="text/javascript">
        var AllOrderList = {
            OrderParms: { "CompanyName": "", "StartDate": "", "EndDate": "", "OrderType": 0,"LookAll":1,"Page":1,
            "companyorder":"a","ordernumorder":"a","orderpeopleorder":"a" },
            GetOrderList: function() {
                LoadingImg.ShowLoading("divCompanyOrderList");
                if (LoadingImg.IsLoadAddDataToDiv("divCompanyOrderList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "GetAgencyOrderList.aspx?" + $.param(this.OrderParms),
                        cache: false,
                        success: function(html) {
                            $("#divCompanyOrderList").html(html);
                        },
                        error: function(xhr, s, errorThrow) {
                            $("#divCompanyOrderList").html("未能成功获取响应结果")
                        }
                    });
                }
            },
            OrderOnSearch: function() {
                this.OrderParms.CompanyName = encodeURIComponent($.trim($("#txtOrderCompanyName").val()));
                this.OrderParms.StartDate = $("#OrderDate_dpkStart").val();
                this.OrderParms.EndDate = $("#OrderDate_dpkEnd").val();
                this.OrderParms.OrderType = $("#OrderTypeList").val();
                this.OrderParms.Page = 1;
                this.GetOrderList();
            },
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                this.OrderParms.Page = Page;
                this.GetOrderList();
            },
            UpdateOrderIndex:function(key,value){
                this.OrderParms["companyorder"] = "a";
                this.OrderParms["ordernumorder"] = "a";
                this.OrderParms["orderpeopleorder"] = "a";
                this.OrderParms[key] = value;
                this.OrderParms.Page = 1;
                this.GetOrderList();
            }
        };

        $(document).ready(function() {
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            AllOrderList.GetOrderList();
            $("#imgBtnOrderSearch").click(function() {
                AllOrderList.OrderOnSearch();
            });
            $("#tdOrder input[type='text']").keypress(function(e){
                if(e.keyCode == 13)
                {
                    AllOrderList.OrderOnSearch();
                    return false;
                }
            });
        });
    </script>
    </form>
</body>
</html>
