<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteList.aspx.cs" Inherits="UserBackCenter.RouteAgency.RouteList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择线路信息</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0" bgcolor="#ebf4ff">
        <tr>
            <td align="center" height="30">
                <span class="font14_bk">【 请选择线路 】</span>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0" bgcolor="#ebf4ff">
        <tr class="tab_luan">
            <td align="left" height="40">
                <asp:Label ID="lblRouteClass" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="tab_luan">
            <td align="left" height="40">
                <label for="txtRouteName">线路名称：</label><input type="text" id="txtRouteName" value="<%=Request.QueryString["rn"] %>" />
                <input type="button" id="txtRouteQuery" value="搜索" />
            </td>
        </tr>
    </table>
    <asp:DataList ID="dlRouteList" runat="server" RepeatDirection="Horizontal" RepeatColumns="3"
        BorderStyle="Solid" HorizontalAlign="Center" Width="98%" BorderColor="Silver"
        BorderWidth="1px" ShowFooter="False" ShowHeader="False" GridLines="Both" bgcolor="#ebf4ff">
        <ItemStyle HorizontalAlign="Left" />
        <ItemTemplate>
            <input type="radio" name="RouteID" value='<%# DataBinder.Eval(Container.DataItem,"ID") %>' /><a
                href="/routeagency/routemanage/routeprint.aspx?RouteID=<%# DataBinder.Eval(Container.DataItem,"ID") %>" target="_blank"><%# DataBinder.Eval(Container.DataItem,"RouteName") %></a>
        </ItemTemplate>
    </asp:DataList>
    <asp:Panel ID="pnlNoData" runat="server" Visible="false">
    <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0" bgcolor="#ebf4ff">
    <tr>
    <td rowspan="3" colspan="3">暂无线路数据!</td>
    </tr>
    </table>
    </asp:Panel>
    <table id="Table1" bordercolor="#b0ae7c" cellspacing="0" cellpadding="0" width="98%"
        align="center" border="0" bgcolor="#ebf4ff">
        <tr>
            <td align="center" height="27">
                <cc1:ExportPageInfo ID="ExportPageInfo2" LinkType="4" runat="server" />
                &nbsp;
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0" bgcolor="#ebf4ff">
        <tr>
            <td align="center" height="40">
                <input type="button" id="btnSubmit" name="btnSubmit" class="baocunlandi_an" value="选用线路" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
    $("#btnSubmit").click(function(){       
        if($("input[type=radio]:checked").length > 0)
        {
            var RouteID = $("input[type=radio]:checked").val();
            $.ajax({
                url:"/routeagency/routelist.aspx?flag=select&RouteID="+ encodeURI(RouteID)+"&rnd="+ Math.random(),
                success:function(html){
                    if(html != '')
                    {
                        var frameid=window.parent.Boxy.queryString("iframeId")
                        var callBack=window.parent.Boxy.queryString("callBack")
                        window.parent.Boxy.getIframeDialog(frameid).hide(function(){
                            eval('parent.TourModule.InitRouteInfo('+ html +',"<%=ContainerID %>");');
                        });
                    }
                }
            });
        }else{
            alert("请选择线路!");
            return false;
        }
    });
    function getQueryString(val){
        var uri = window.location.search;
        var re = new RegExp("" + val + "\=([^\&\?]*)", "ig");
        return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
    }
    $("#txtRouteQuery").click(function(){
        var areaId = getQueryString("AreaID");
        var type = getQueryString("ReleaseType"); 
        var ContainerID = getQueryString("ContainerID");
        var txtRouteName = $.trim($("#txtRouteName").val());
        var obj={
            AreaID:areaId,
            ReleaseType:type,
            ContainerID:ContainerID,
            rn:txtRouteName
        };
        window.location.href =window.location.href.replace(window.location.search,"")+"?"+$.param(obj);
    });
    $("#txtRouteName").bind("keypress",function(e){
        if(e.keyCode == 13){
             $("#txtRouteQuery").click();
             return false;
        }
    });
    </script>

    </form>
</body>
</html>
