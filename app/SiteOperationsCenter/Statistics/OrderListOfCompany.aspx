<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderListOfCompany.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.OrderListOfCompany" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/StartAndEndDate.ascx" TagName="StartAndEndDate"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainbody">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="tab_luand">
            <tr class="lr_hangbg">
                <td align="left">
                    <span class="unnamed1">
                        <img src="<%=ImageServerUrl %>/Images/point16.gif" width="21" height="21"></span>
                    线路名称：
                    <input name="txtRouteName" type="text" id="txtRouteName" class="textfield" style="width: 279px;" />
                    <uc1:StartAndEndDate ID="StartAndEndDate1" runat="server" />
                    <input type="button" name="btnSearch" value="查询" id="btnSearch" class="an_tijiaobaocun" />
                    
                </td>
            </tr>
            <tr>
                <td align="center" id="td_content">
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>" cache="true"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js" cache="true"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>" cache="true"></script>

    <script type="text/javascript">        
       /** SortType：排序类型；Page：当前页数；OrderState：订单状态(int)；OrderType:排序类型：升/降(0/1)*/
        var OrderListOfCompany = {
            BeginTime: '<%=Request.QueryString["BeginTime"] %>',
            EndTime: '<%=Request.QueryString["EndTime"] %>',
            SortType: '',
            OrderType: 1,
            CompanyID: '<%=Request.QueryString["CompanyID"] %>',
            RouteName: '',
            OrderState: '<%=Request.QueryString["OrderState"] %>',
            Page: 1,
            getOrderList: function() {//获取订单列表
                var self = this;
                $.ajax({
                    cache: false,
                    url: "/Statistics/AjaxOrderListOfCompany.aspx?rd=" + Math.random(),
                    data: { CompanyID: self.CompanyID, RouteName: self.RouteName, BeginTime: self.BeginTime, EndTime: self.EndTime, SortType: self.SortType, OrderType: self.OrderType, Page: self.Page, OrderState: self.OrderState },
                    success: function(html) {
                        $("#td_content").html(html);
                        //取得当前页数
                        self.Page = $("#hidAjaxOrderListOfCompany_pageindex").val();
                        $("a[sorttype]").click(function() {//排序
                            self.SortType = $(this).attr("SortType");
                            self.OrderType = $(this).attr("ordertype");
                            self.getOrderList();
                            return false;
                        });
                        $(".TourCompanyName").click(function() {
                            var iframeUrl = $(this).attr("href")
                            parent.Boxy.iframeDialog({ title: '公司详细信息', iframeUrl: iframeUrl, width: 560, height: 220 });
                            return false;
                        });
                    },
                    error: function() {
                        $("#td_content").html("未能成功获取响应结果！");
                    }
                })
            },
            search: function() {//查询
                var self = this;
                self.Page = 1;
                self.BeginTime = StartAndEndDate1.GetStartDate();
                self.EndTime = StartAndEndDate1.GetEndDate();
                self.RouteName = $("#txtRouteName").val();
                self.getOrderList();
            },
            LoadData: function(obj) {//分页
                var currPage = exporpage.getgotopage(obj);
                if (this.Page != currPage) {
                    this.Page = currPage;
                    this.getOrderList();
                }
            },
            pageInit: function() {
                var self = this;
                LoadingImg.stringPort = '<%=EyouSoft.Common.Domain.ServerComponents %>';
                LoadingImg.ShowLoading("td_content"); //初始化是引入loading....
                self.getOrderList();
                $("#btnSearch").click(function() {
                    self.search();
                });
                $("input[type='text']").keyup(function(e) {
                    if (e.keyCode == 13) {
                        self.search();
                    }
                });
            }
        };
        $(function() {
            OrderListOfCompany.pageInit();
        });
    </script>
    </form>
</body>
</html>
