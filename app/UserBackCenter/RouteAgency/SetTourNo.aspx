<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetTourNo.aspx.cs" Inherits="UserBackCenter.RouteAgency.SetTourNo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置团号规则</title>
    <style>
        body
        {
            margin: auto 0;
            padding: 0;
        }
        td
        {
            font-size: 12px;
            line-height: 120%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="3">
        自定义团号前缀：<br />
        <asp:Repeater ID="rptRouteArea" runat="server">
            <ItemTemplate>
                <%# InitData(Convert.ToString(DataBinder.Eval(Container.DataItem, "AreaId")), Convert.ToString(DataBinder.Eval(Container.DataItem, "AreaName")))%>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td align="center" style="line-height: 30px;">
                &nbsp;
            </td>
            <td align="left" style="line-height: 30px;">
                <input type="button" id="btnSave" name="btnSave" value="提交团号规则" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
    $("#btnSave").click(function(){
        var arr = new Array();
        var arrAreaId = new Array();
        $("input[type=text][name=txtPrefixText]").each(function(){
            arr.push($(this).val());
            arrAreaId.push($(this).prev("input[type=hidden]").val());
        });
        if(arr.length > 0)
        {    
            if('<%=IsCompanyCheck %>' == 'False')
             {
                alert('对不起，您的账号未审核，不能进行操作!');
                return false;
             }
            
            $.ajax({
	                type:"POST",
	                url:"/RouteAgency/SetTourNo.aspx?flag=save&PrefixText="+ encodeURI(arr.join(',')) +"&AreaIdList="+ encodeURI(arrAreaId.join(',')) +"&rnd="+ Math.random(),
	                cache:false,
	                success:function(html){
	                    if(html){
	                        alert("设置成功!");
		                    var frameid=window.parent.Boxy.queryString("iframeId")
                            window.parent.Boxy.getIframeDialog(frameid).hide();
	                    }else{
	                        alert("设置失败!");
	                    }
	                }
	            });
	      }else{
	        alert('请填写团号前缀!');
	        return false;
	      }
	  });
    </script>

    </form>
</body>
</html>
