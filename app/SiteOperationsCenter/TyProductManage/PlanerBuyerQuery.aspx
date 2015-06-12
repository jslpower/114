<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanerBuyerQuery.aspx.cs" EnableViewState="false" Inherits="SiteOperationsCenter.TyProductManage.PlanerBuyerQuery" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>机票采购商查询</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin: 0;
            color: #333333;
            font-family: "宋体" ,Arial,Helvetica,sans-serif;
            font-size: 12px;
        }
        table
        {
            border-collapse: collapse;
        }
        td
        {
            font-size: 12px;
            line-height: 20px;
        }
        .lr_hangbg
        {
            color: #000000;
            font-size: 12px;
            line-height: normal;
        }
        .font12_grean
        {
            color: #008702;
            font-size: 12px;
        }
        .mainbody
        {
            margin-left: auto;
            margin-right: auto;
        }
        #list
        {
        	margin-top:10px;
        }
    </style>
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
                            <strong class="shenglanz">机票采购商查询</strong>
                        </td>
                        <td align="left" style="border-bottom: 1px solid #62A8E4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                            <table width="100%" border="0" align="left" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td id="tdSearch">
                                    其他平台用户ID：<input type="text" id="txtOPUserId" name="txtOPUserId" maxlength="10" />
                                        &nbsp;<img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom:-3px;cursor:pointer" id="imgBtnSearch" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
               <div id="list" align="center"></div>
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
    <script type="text/javascript">
        var BuyerQuery = {
            QueryParms: { "OPUserId": "" },
            GetList: function() {
                LoadingImg.ShowLoading("list");
                if (LoadingImg.IsLoadAddDataToDiv("list")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxPlanerBuyerQuery.aspx?" + $.param(this.QueryParms),
                        cache: false,
                        success: function(html) {
                            $("#list").html(html);
                        },
                        error: function(xhr, s, errorThrow) {
                            $("#list").html("未能成功获取响应结果,请稍后再试")
                        }
                    });
                }
            },
            Search: function() {
                this.QueryParms.OPUserId = $.trim($("#txtOPUserId").val());
                this.GetList();
            }
        };

        $(document).ready(function() {
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            $("#imgBtnSearch").click(function() {
                BuyerQuery.Search();
                return false;
            });
            $("#tdSearch input[type='text']").keypress(function(e){
                if(e.keyCode == 13)
                {
                    BuyerQuery.Search();
                    return false;
                }
            });
        });
    </script>
    
</body>
</html>
