<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetLeaveCity.aspx.cs" Inherits="SiteOperationsCenter.LineManage.SetLeaveCity" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置常用出港城市</title>
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
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2" align="center" style="line-height: 25px;">
                <strong>设置常用出港城市</strong><br />
                <table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="6" align="center">
                            <strong>已选择</strong>
                        </td>
                    </tr>
                    <%=strSelectLeaveCity%>
                </table>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#E0E0E0"
                    style="margin-top: 2px;">
                    <tr bgcolor="#DBF7FD">
                        <td height="20" colspan="2" align="center">
                            <strong>请选择</strong>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptProvinceID" runat="server">
                        <ItemTemplate>
                            <tr class="baidi">
                                <td width="10%" align="right" bgcolor="#FFFFFF">
                                    <%# DataBinder.Eval(Container.DataItem,"ProvinceName") %><input type="hidden" name="hidProvinceID"
                                        value='<%# DataBinder.Eval(Container.DataItem,"ProvinceID") %>' />
                                </td>
                                <td align="left" bgcolor="#FFFFFF">
                                    <%# GetIsSiteCity(Convert.ToString(DataBinder.Eval(Container.DataItem,"ProvinceID")))%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="6">
                    <tr>
                        <td width="41%" align="right">
                            <input name="btnSave" id="btnSave" type="button" value="  保存设置  " style="height: 28px;
                                font-weight: bold; color: #990000" />
                        </td>
                        <td width="59%" align="center">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
    $("#btnSave").click(function(){
        var arr = new Array();
        var arrName = new Array();
        $("input[type=checkbox]:checked").each(function(){
            var oid = -1;
            for(var i = 0; i < arr.length; i ++)
            {
                if(arr[i] == $(this).val().split('|')[0])
                {
                    oid = 0;
                    break;
                }
            }
            if(oid == -1)
            {
                arr.push($(this).val().split('|')[0]);
                arrName.push($(this).val().split('|')[1]);
            }
        });
        if(arr.length != 0)
        {
            if('<%=IsCompanyCheck %>' == 'False')
            {
                alert('对不起，您的账号未审核，不能进行操作!');
                return false;
            }
            $.ajax({
	            type:"POST",
	            url:"/LineManage/SetLeaveCity.aspx?flag=save&CityId=<%=SelectedVal %>&type=<%=type %>&CompanyId=<%=CompanyId %>&IdList="+ encodeURI(arr.join(',')) + "&NameList="+ encodeURI(arrName.join(',')) +"&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd="+ Math.random(),
	            cache:false,
	            success:function(html){
	                if(html != ""){
	                    alert("设置成功!");
	                    if('<%=type %>'=="LeaveCity")
	                        window.parent.document.getElementById("tdBindLeaveCity").innerHTML=html;
	                    else if('<%=type %>'=="BackCity")
	                        window.parent.document.getElementById("tdBindBackCity").innerHTML=html;
	                    
		                var frameid=window.parent.Boxy.queryString("iframeId")
                        window.parent.Boxy.getIframeDialog(frameid).hide();
	                }else{
	                    alert("设置失败!");
	                }
	            }
	        });
        }else{
            alert('请选择要设置的出港城市!');
            return false;
        }
    });
    </script>

    </form>
</body>
</html>
