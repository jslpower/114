<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="SiteOperationsCenter.NewsCenterControl.Category" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="<%= CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
<title>资讯类别管理</title>
    <style type="text/css">
        .style1
        {
            height: 18px;
        }
        .style2
        {
            height: 34px;
        }
    </style>
</head>

<body>
<form runat="server">
<table width="100%" border="1" cellspacing="0" cellpadding="0" style="border:1px solid #ccc;">
  <tr style="background:#C0DEF3; height:28px; text-align:center; font-weight:bold;">
    <td>序号</td>
    <td>类别</td>
    <td>操作</td>
  </tr> <tr style="background:#fff; height:24px; text-align:center;">
                <td colspan="3" align="left" bgcolor="#E6F2FB"><h2>行业资讯</h2></td>
              </tr>
    <asp:Repeater ID="repList" runat="server" onitemcommand="repList_ItemCommand">
        <ItemTemplate>
              <tr style="background:#fff; height:24px; text-align:center;">
                <td><%# Container.ItemIndex+1 %></td>
                <td><%# Eval("ClassName") %></td>
                <td>
                    <a style='display:<%#  ((bool)Eval("IsSystem"))?"none":"block" %>'
                    href="<%# string.Format("javascript:window.location.href='Category.aspx?id={0}&name={1}&type=edit'",Eval("ID"),Eval("ClassName")) %>">编辑</a>
                    <asp:LinkButton ID="lbDel" OnClientClick="return confirm('确定要删除该类别吗?')"
                    Visible='<%#  ((bool)Eval("IsSystem"))?false:true %>'
                     CommandArgument='<%# Eval("ID") %>' CommandName="Del" runat="server">删除</asp:LinkButton>
                    </td>
              </tr>
        </ItemTemplate>
    </asp:Repeater>
  <tr style="background:#fff; height:24px; text-align:center;">
    <td></td>
    <td>
        <asp:TextBox ID="txtCateName" runat="server"></asp:TextBox></td>
    <td>
        <asp:Button ID="btnAdd" runat="server" Text="保存" OnClientClick="return valid()" onclick="Button1_Click"  /></td>
  </tr>
  <tr style="background:#fff; height:24px; text-align:center;">
    <td colspan="3" align="left" bgcolor="#E6F2FB"><h2>同业学堂</h2></td>
  </tr>
  <asp:Repeater ID="repSchooolList" runat="server" onitemcommand="repSchoolList_ItemCommand">
        <ItemTemplate>
              <tr style="background:#fff; height:24px; text-align:center;">
                <td><%# Container.ItemIndex+1 %></td>
                <td><%# Eval("ClassName") %></td>
                <td>
                 
                    <a style='display:<%#  ((bool)Eval("IsSystem"))?"none":"block" %>'
                    href="<%# string.Format("javascript:window.location.href='Category.aspx?id={0}&name={1}&type=sedit'",Eval("ID"),Eval("ClassName")) %>">编辑</a>
                    
                    <asp:LinkButton ID="lbDel" OnClientClick="return confirm('确定要删除该类别吗?')" CommandArgument='<%# Eval("ID") %>' 
                        Visible='<%#  ((bool)Eval("IsSystem"))?false:true %>'
                    CommandName="Del" runat="server">删除</asp:LinkButton>
                    </td>
              </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr style="background:#fff; height:24px; text-align:center;">
    <td></td>
    <td>
        <asp:TextBox ID="txtSchoolName" runat="server"></asp:TextBox></td>
    <td>
        <asp:Button ID="btnSchoolAdd" runat="server" Text="保存" OnClientClick="return valid1()" onclick="btnSchoolAdd_Click"  /></td>
  </tr>
</table>
</form>
</body>
</html>
<script type="text/javascript" src="<%= JsManage.GetJsFilePath("jquery") %>"></script>
<script type="text/javascript" language="javascript">
    function valid(){
        var name= $("#<%= txtCateName.ClientID %>").val();
        if(name == ""){
            alert("不能为空");
            return false;
        }else{
        return true;
        }
    }function valid1(){
        var name= $("#<%= txtSchoolName.ClientID %>").val();
        if(name == ""){
            alert("不能为空");
            return false;
        }else{
        return true;
        }
    }
//    $(function() {
//        $("a.delall").click(function() {
//            var arrayList = new Array();
//            $("#subtree1").find("input.checkbox").each(function() {
//                if ($(this).attr("checked")) {
//                    arrayList.push($(this).parent().parent().attr("tid"));
//                }
//            });
//            var tid = arrayList.join(",");
//            if (confirm("确认删除？")) {
//                $.ajax({
//                    type: "POST",
//                    url: "/Admin/Notice.aspx?type=del",
//                    data: { tid: tid },
//                    dataType: "json",
//                    success: function(d) {
//                        alert(d.msg);
//                        location.reload();
//                    }
//                });
//            }
//            return false;
//        });

//        $("#searchnotice").click(function() {
//            var key = $("#searchtitle").val();
//            var type = $("#ddlType").val();
//            location.href = "/Admin/Notice.aspx?key=" + key+"&type="+type;
//            return false;
//        });

//    });
</script>