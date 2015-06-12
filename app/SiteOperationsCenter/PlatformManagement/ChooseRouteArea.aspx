<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseRouteArea.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.ChooseRouteArea" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售区域维护第二步：选择线路区域</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="JavaScript">
 
        function mouseovertr(o) {
          o.style.backgroundColor="#FFF9E7";
          //o.style.cursor="hand";
        }
        function mouseouttr(o) {
          o.style.backgroundColor=""
        }   
        //全选
        function CheckAll(obj)
        {	
            var ck=document.getElementsByTagName("input");
            for(var i=0 ;i<ck.length;i++)
             {
                if(ck[i].type=="checkbox")
                {
                 ck[i].checked=obj.checked;
	        } 	
	       }
        }
        //选择线路区域
        $(document).ready(function(){
            $("#btnSave").click(function(){
                $("input[type]='checkbox'").each(function(){
                    //遍历表单上除了是否设为首页的所有复选框，获取用户选中的线路区域相关信息
                    if($(this).attr("checked")==true && $(this).attr("name")!="cbk_IsShowIndex")
                    {   
                        var ss;
                        $(this).attr("value",$(this).attr("value").split('_')[0]);
                        var obj=$(this).nextAll();
                        if(obj[0]!=null)
                        {
                            //给线路区域对应的复选框设置值：线路区域ID|线路区域名称_排序值
                            $(this).attr("value",$(this).attr("value")+"_"+$(obj[0]).children(0).attr("value"));
                        }
                        if(obj[1]!=null)
                        {
                            //取是否设为首页的值
                            ss=$(obj[1]).attr("checked")==true?"1":"0";
                            //将值追加到复选框的值里面，最终格式：线路区域ID|线路区域名称_排序值_是否设为首页
                            $(this).attr("value",$(this).attr("value")+"_"+ss);
                        }
                    }
                });
            });
        });
    </script>

</head>
<body>
    <form name="form1" id="form1" runat="server">
    <table width="855" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="background: #FFF9E7; border: 1px solid #F6D886; padding: 5px;">
                第一步：选择出港城市。<span class="unnamed1">第二步：设置分站的线路区域</span>&nbsp; 保存提交，成功！
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="3" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#C3E3F2">
                经营专线区域：
            </td>
            <td width="720" align="left" bgcolor="#C3E3F2">
                <input type="checkbox" onclick="CheckAll(this)" />全选
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td height="18" align="left" bgcolor="#E8F4FA">
                <strong>国内长线</strong>
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td width="855">
                <asp:DataList ID="dl_GNLongRouteList" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="3"
                    RepeatDirection="Horizontal" Width="98%" >
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="<%# Container.ItemIndex + 1 %>">
                            <tr>
                                <td valign="top" align="left" width="40%">
                                    <%# ShowTourAreaCheckBox(DataBinder.Eval(Container.DataItem, "AreaId").ToString(), DataBinder.Eval(Container.DataItem, "AreaName").ToString(),DataBinder.Eval(Container.DataItem, "RouteType").ToString())%>
                                    <%# DataBinder.Eval(Container.DataItem, "AreaName")%>
                                     <%# ShowEditeInfo(DataBinder.Eval(Container.DataItem, "AreaId").ToString(),DataBinder.Eval(Container.DataItem, "RouteType").ToString())%>
                               
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td height="18" align="left" bgcolor="#E8F4FA">
                <strong>国内短线</strong>
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td width="855">
                <asp:DataList ID="dl_GNShortRouteList" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="3"
                    RepeatDirection="Horizontal" Width="98%" >
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="<%# Container.ItemIndex+ 1 %>">
                            <tr>
                                <td valign="top" align="left" width="40%">
                                    <%# ShowTourAreaCheckBox(DataBinder.Eval(Container.DataItem, "AreaId").ToString(), DataBinder.Eval(Container.DataItem, "AreaName").ToString(),DataBinder.Eval(Container.DataItem, "RouteType").ToString())%>
                                    <%# DataBinder.Eval(Container.DataItem, "AreaName")%>
                                   <%# ShowEditeInfo(DataBinder.Eval(Container.DataItem, "AreaId").ToString(),DataBinder.Eval(Container.DataItem, "RouteType").ToString())%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td height="18" align="left" bgcolor="#E8F4FA">
                <strong>出境线路</strong>
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td width="855">
                <asp:DataList ID="dl_OutRouteList" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="3"
                    RepeatDirection="Horizontal" Width="98%" >
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="<%# Container.ItemIndex + 1 %>">
                            <tr>
                                <td valign="top" align="left" width="40%">
                                    <%# ShowTourAreaCheckBox(DataBinder.Eval(Container.DataItem, "AreaId").ToString(), DataBinder.Eval(Container.DataItem, "AreaName").ToString(), DataBinder.Eval(Container.DataItem, "RouteType").ToString())%>
                                    <%# DataBinder.Eval(Container.DataItem, "AreaName")%>
                                   <%# ShowEditeInfo(DataBinder.Eval(Container.DataItem, "AreaId").ToString(),DataBinder.Eval(Container.DataItem, "RouteType").ToString())%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CssClass="baocun_an" Text="下一步" OnClick="btnSave_Click" />
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
    </form>
</body>
</html>
