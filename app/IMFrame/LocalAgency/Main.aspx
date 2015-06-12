<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="IMFrame.LocalAgency.Main" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>地接线路库</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <style type="text/css">
        /*FENYE*/DIV.digg
        {
            padding-right: 3px;
            padding-left: 3px;
            padding-bottom: 3px;
            margin: 3px;
            margin-top: 10px;
            padding-top: 3px;
            text-align: center;
        }
        DIV.digg A
        {
            border: #54A11C 1px solid;
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 2px;
            margin: 2px;
            color: #54A11C;
            padding-top: 2px;
            text-decoration: none;
        }
        DIV.digg A:hover
        {
            border: #54A11C 1px solid;
            background: #54A11C;
            color: #fff;
        }
        DIV.digg A:active
        {
            border: #54A11C 1px solid;
            color: #000;
        }
        DIV.digg SPAN.current
        {
            border: #54A11C 1px solid;
            padding-right: 5px;
            padding-left: 5px;
            font-weight: bold;
            padding-bottom: 2px;
            margin: 2px;
            color: #fff;
            padding-top: 2px;
            background-color: #54A11C;
        }
        DIV.digg SPAN.disabled
        {
            border: #eee 1px solid;
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 2px;
            margin: 2px;
            color: #ddd;
            padding-top: 2px;
        }
        /*end*/BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #2F1004;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #2F1004;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        a.red
        {
            color: #cc0000;
        }
        a.red:visited
        {
            color: #cc0000;
        }
        a.red:hover
        {
            color: #ff0000;
        }
        a.bbl
        {
            color: #000066;
        }
        a.bbl:visited
        {
            color: #000066;
        }
        a.bbl:hover
        {
            color: #ff0000;
        }
        .bar_on_comm
        {
            width: 80px;
            height: 21px;
            float: left;
            border: 1px solid #E7C994;
            border-bottom: 0px;
            background: #ffffff;
            text-align: center;
        }
        .bar_on_comm a
        {
            color: #cc0000;
        }
        .bar_un_comm
        {
            width: 120px;
            height: 21px;
            float: left;
            text-align: center;
        }
        .bar_un_comm a
        {
            color: #0E3F70;
        }
        a.cliewh
        {
            display: block;
            width: 190px;
            height: 22px;
            overflow: hidden;
        }
        .aun
        {
            background: url(<%=ImageServerUrl %>/IM/images/sreach_annui.gif) no-repeat center;
            text-align: center;
        }
        .aun a
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:visited
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:hover
        {
            color: #f00;
            font-size: 14px;
        }
        .aon
        {
            background: url(<%=ImageServerUrl %>/IM/images/areabottonon.gif) no-repeat center;
            text-align: center;
        }
        .aon a
        {
            color: #000;
            font-weight: bold;
            font-size: 14px;
        }
        .toolbj
        {
            background: url(<%=ImageServerUrl %>/IM/images/toolbj.gif) repeat-x;
            height: 31px;
        }
        .toolbj a
        {
            margin-left: 2px;
        }
        .toolbj a img
        {
            margin-bottom: -3px;
            padding-right: 2px;
        }
    </style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("IMCommon") %>"></script>

    <script type="text/javascript" language="javascript">
        $(function() {
            GetData(0)
            regEvent();
        })
        function LoadData(obj) {
            var gotopage = exporpage.getgotopage(obj);
            GetData(gotopage);
        }
        function GetData(intPage) {
            var strUrl = "AjaxMain.aspx";
            if (intPage != 0)
                strUrl = strUrl + "?Page=" + intPage;

            $.ajax
            ({
                url: strUrl,
                cache: false,
                success: function(html) {
                    $("#RoutesContent").html(html);
                }
            });
        }
        function regEvent() {
            $("#a_modify").click(function() {
                var routeInfo = getRouteId()
                var url = routeInfo.Url + "?type=update&RouteId=" + routeInfo.RouteId + "&RouteSource=2";
                if (routeInfo.RouteCount > 1) {
                    alert('每次只能修改一条线路！')
                    return;
                } else if (routeInfo.RouteCount <= 0) {
                    alert('请选择一条线路进行修改！')
                    return;
                }
                else {
                    if (routeInfo.RouteId != "") {
                        window.open(GetDesPlatformUrlForMQMsg(url, "<%= MQLoginId%>", "<%= Password%>"));
                    }
                }
            })
            $("#a_copy").click(function() {
                var routeInfo = getRouteId()
                var url = routeInfo.Url + "?type=copy&RouteId=" + routeInfo.RouteId + "&RouteSource=2";
                if (routeInfo.RouteCount > 1) {
                    alert('每次只能复制一条线路！')
                    return;
                }
                else if (routeInfo.RouteCount <= 0) {
                    alert('请选择一条线路进行复制！')
                    return;
                }
                else {
                    if (routeInfo.RouteId != "") {
                        window.open(GetDesPlatformUrlForMQMsg(url, "<%= MQLoginId%>", "<%= Password%>"));
                    }
                }
            })
        }
        function getRouteId() {
            var addRouteUrl = "<%= Domain.UserBackCenter %>/RouteAgency/RouteManage/AddTourism.aspx";
            var routeId = { RouteId: '', RouteCount: 0, Url: '', routetype: '' };
            var count = 0;
            $(".RouteMain").each(function() {
                if (this.checked) {
                    routeId.RouteId = $(this).val();
                    routeId.RouteCount += 1;
                    routeId.routetype = $(this).attr("routetype")
                    routeId.Url = addRouteUrl;
                }
            })
            return routeId;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="34" valign="bottom" background="<%=ImageServerUrl %>/IM/images/ztopbjcs.gif">
                <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 3px;">
                    <tr>
                        <td width="111" align="center" style="background: url(<%=ImageServerUrl %>/IM/images/ztopbttttcs2.gif) no-repeat center;
                            height: 30px;">
                            <img src="<%=ImageServerUrl %>/IM/images/shop.gif" width="16" height="16" style="margin-bottom: -3px;" />
                            <a href="<%= GetDesPlatformUrl(RouteListUrl) %>" target="_blank"><strong>我的线路库</strong></a>
                        </td>
                        <td width="100" align="center">
                            <a href="<%= GetDesPlatformUrl(RouteDefaultUrl) %>" target="_blank">进入后台</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <table width="210" border="0" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                    <tr>
                        <td class="toolbj">
                            <%if (RoleAdd)
                              {%>
                            <a href="<%= GetDesPlatformUrl(RouteAddUrl) %>" target="_blank" id="a_add" class="menubarleft">
                                <img src="<%=ImageServerUrl %>/IM/images/add2.gif" width="13" height="17" />发布线路</a>
                            <%} if (RoleEdit)
                              { %><a href="javascript:void(0)" id="a_modify" class="menubarleft">
                                  <img src="<%=ImageServerUrl %>/IM/images/modified.gif" />修改</a>
                            <%} if (RoleAdd)
                              { %><a href="javascript:void(0)" id="a_copy" class="menubarleft">
                                  <img src="<%=ImageServerUrl %>/IM/images/copy.gif" />复制</a>
                            <%}
                            %>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="RoutesContent">
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hid_LocalRouteUrl" runat="server" />
    </form>
</body>
</html>
