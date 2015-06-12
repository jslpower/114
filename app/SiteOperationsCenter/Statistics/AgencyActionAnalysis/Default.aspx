<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SiteOperationsCenter.Statistics.AgencyActionAnalysis.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register src="../../usercontrol/StartAndEndDate.ascx" tagname="StartAndEndDate" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组团社行为分析</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>  
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
                            <strong class="shenglanz">零售商预订Top10</strong>
                        </td>
                        <td align="left" style="border-bottom: 1px solid #62A8E4">
                            <a href="AllOrderList.aspx">点击查看全部预订情况</a>
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
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="right">
                            <a href="AllOrderList.aspx">点击查看全部预订情况<img src="<%=ImageServerUrl %>/images/yunying/ico.gif" width="11" height="11" /></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
        margin-bottom: 3px;">
        <tr>
            <td width="15" style="border-bottom: 1px solid #62A8E4">
                &nbsp;
            </td>
            <td width="105" height="24" background="<%=ImageServerUrl %>/images/yunying/weichulidingdan.gif" align="center">
                <strong class="shenglanz">零售商登录Top10</strong>
            </td>
            <td align="left" style="border-bottom: 1px solid #62A8E4">
                <a href="AllLoginList.aspx">点击查看全部登录记录</a>
            </td>
        </tr>
    </table>
    <table id="tblLogin" width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                <table width="100%" border="0" align="left" cellpadding="1" cellspacing="0">
                    <tr>
                        <td id="tdLogin">
                            单位名称：<input type="text" id="txtLoginCompanyName" name="txtLoginCompanyName" />
                            <uc1:StartAndEndDate ID="StartAndEndDate1" runat="server" />&nbsp;<img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom:-3px;cursor:pointer"  id="imgBtnLoginSearch"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   <div id="divCompanyLoginList"  align="center"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <a href="AllLoginList.aspx">点击查看全部登录记录<img src="<%=ImageServerUrl %>/images/yunying/ico.gif" width="11" height="11" /></a>
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    <script type="text/javascript">
        var AgencyActionAnalysis = {
            /* 指示页面是否加载完成,2代表已加载完成*/
            IsLoadState:0,
            OrderParms: { "CompanyName": "", "StartDate": "", "EndDate": "", "OrderType": 0 },
            LoginParms: { "CompanyName": "", "StartDate": "", "EndDate": "" },
            GetOrderList: function() {
                LoadingImg.ShowLoading("divCompanyOrderList");
                if (LoadingImg.IsLoadAddDataToDiv("divCompanyOrderList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "GetAgencyOrderList.aspx?" + $.param(AgencyActionAnalysis.OrderParms),
                        cache: false,
                        success: function(html) {
                            $("#divCompanyOrderList").html(html);
                            AgencyActionAnalysis.IsLoadState++;
                        },
                        error: function(xhr, s, errorThrow) {
                            $("#divCompanyOrderList").html("未能成功获取响应结果");
                            AgencyActionAnalysis.IsLoadState++;
                        }
                    });
                }
            },
            GetLoginList: function() {
                LoadingImg.ShowLoading("divCompanyLoginList");
                if (LoadingImg.IsLoadAddDataToDiv("divCompanyLoginList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "GetAgencyLoginList.aspx?" + $.param(AgencyActionAnalysis.LoginParms),
                        cache: false,
                        success: function(html) {
                            $("#divCompanyLoginList").html(html);
                            AgencyActionAnalysis.IsLoadState++;
                        },
                        error: function(xhr, s, errorThrow) {
                            $("#divCompanyLoginList").html("未能成功获取响应结果");
                            AgencyActionAnalysis.IsLoadState++;
                        }
                    });
                }
            },
            OrderOnSearch: function() {
                this.OrderParms.CompanyName = encodeURIComponent($.trim($("#txtOrderCompanyName").val()));
                this.OrderParms.StartDate = $("#OrderDate_dpkStart").val();
                this.OrderParms.EndDate = $("#OrderDate_dpkEnd").val();
                this.OrderParms.OrderType = $("#OrderTypeList").val();
                this.GetOrderList();
            },
            LoginOnSearch: function() {
                this.LoginParms.CompanyName = encodeURIComponent($.trim($("#txtLoginCompanyName").val()));
                this.LoginParms.StartDate = $("#StartAndEndDate1_dpkStart").val();
                this.LoginParms.EndDate = $("#StartAndEndDate1_dpkEnd").val();
                this.GetLoginList();
            },
            focusToHash:function(){
                var ch = this.removeHash(window.location.hash);
                if(ch==null||ch==""){
                    return;
                }
                var a = $("#"+ch);
                if(a.length>0){
                    var offset = a.offset();
                    $(document).scrollTop(offset.top);
                }
            },
            removeHash:function(hashValue){
                if (hashValue == null || hashValue == undefined)
                     return null;
                  else if (hashValue == "")
                     return "";
                  else if (hashValue.length == 1 && hashValue.charAt(0) == "#")
                     return "";
                  else if (hashValue.length > 1 && hashValue.charAt(0) == "#")
                     return hashValue.substring(1);
                  else
                     return hashValue;  
            }
        };

        $(document).ready(function() {
            
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            AgencyActionAnalysis.GetOrderList();
            AgencyActionAnalysis.GetLoginList();
            $("#imgBtnOrderSearch").click(function() {
                AgencyActionAnalysis.OrderOnSearch();
            });
            $("#imgBtnLoginSearch").click(function() {
                AgencyActionAnalysis.LoginOnSearch();
            });
            
            //回车查询
            $("#tdOrder input[type='text']").keypress(function(e){
                if(e.keyCode == 13)
                {
                    AgencyActionAnalysis.OrderOnSearch();
                    return false;
                }
            });
            $("#tdLogin input[type='text']").keypress(function(e){
                if(e.keyCode == 13)
                {
                    AgencyActionAnalysis.LoginOnSearch();
                    return false;
                }
            });
            
            /*指定一个定时器检查页面是否已经完成了加载*/
            globalInte = setInterval(function(){
                if(AgencyActionAnalysis.IsLoadState==2){
                    clearInterval(globalInte);
                    AgencyActionAnalysis.focusToHash();
                }
            },200);
        });
    </script>
    </form>
</body>
</html>
