<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllLoginList.aspx.cs" Inherits="SiteOperationsCenter.Statistics.AgencyActionAnalysis.AllLoginList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="../../usercontrol/StartAndEndDate.ascx" tagname="StartAndEndDate" tagprefix="uc1" %>
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
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
        margin-bottom: 3px;">
        <tr>
            <td width="15" style="border-bottom: 1px solid #62A8E4">
                &nbsp;
            </td>
            <td width="105" height="24" background="<%=ImageServerUrl %>/images/yunying/weichulidingdan.gif" align="center">
                <strong class="shenglanz">全部登录记录</strong>
            </td>
            <td align="left" style="border-bottom: 1px solid #62A8E4">
                <a href="Default.aspx#tblLogin">点击查看零售商登录Top10</a>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
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
    <div id="divCompanyLoginList" align="center"></div>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    <script type="text/javascript">
        var AllLoginList = {
            LoginParms: { "CompanyName": "", "StartDate": "", "EndDate": "","LookAll":1,"Page":1,
            "visitcountorder":"a","logincountorder":"a" },
            GetLoginList: function() {
                LoadingImg.ShowLoading("divCompanyLoginList");
                if (LoadingImg.IsLoadAddDataToDiv("divCompanyLoginList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "GetAgencyLoginList.aspx?" + $.param(AllLoginList.LoginParms),
                        cache: false,
                        success: function(html) {
                            $("#divCompanyLoginList").html(html);
                        },
                        error: function(xhr, s, errorThrow) {
                            $("#divCompanyLoginList").html("未能成功获取响应结果")
                        }
                    });
                }
            },
            LoginOnSearch: function() {
                this.LoginParms.CompanyName = encodeURIComponent($.trim($("#txtLoginCompanyName").val()));
                this.LoginParms.StartDate = $("#StartAndEndDate1_dpkStart").val();
                this.LoginParms.EndDate = $("#StartAndEndDate1_dpkEnd").val();
                this.LoginParms.Page = 1;
                this.GetLoginList();
            },
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                this.LoginParms.Page = Page;
                this.GetLoginList();
            },
            UpdateOrderIndex:function(key,value){
                this.LoginParms.Page = 1;
                this.LoginParms["visitcountorder"]="a";
                this.LoginParms["logincountorder"]="a"
                this.LoginParms[key] = value;
                this.GetLoginList();
            }
        };

        $(document).ready(function() {
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            AllLoginList.GetLoginList();
            $("#imgBtnLoginSearch").click(function() {
                AllLoginList.LoginOnSearch();
            });
            
            $("#tdLogin input[type='text']").keypress(function(e){
                if(e.keyCode == 13)
                {
                    AllLoginList.LoginOnSearch();
                    return false;
                }
            });
        });
    </script>
    </form>
</body>
</html>
