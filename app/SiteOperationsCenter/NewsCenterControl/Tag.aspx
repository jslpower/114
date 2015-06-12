<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tag.aspx.cs" Inherits="SiteOperationsCenter.NewsCenterControl.Tag" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
<title>Tag标签管理</title>

</head>

<body>
<form runat="server">
<table width="100%" border="1" cellspacing="0" cellpadding="0" style="border:1px solid #ccc;">
  <tr style="background:#fff; height:24px; text-align:center;">
    <td colspan="3">
        Tag标签：<asp:TextBox ID="txtTagName" runat="server"></asp:TextBox>
      <input type="button" onclick="serach()" value="搜索"/>    </td>
  </tr>
  <tr style="background:#C0DEF3; height:28px; text-align:center; font-weight:bold;">
    <td width="5%">序号</td>
    <td width="13%">Tag标签组</td>
    <td width="9%">操作</td>
  </tr>
    <asp:Repeater ID="repList" runat="server" onitemcommand="repList_ItemCommand">
        <ItemTemplate>
            <tr style="background:#fff; height:24px; text-align:center;">
                <td><%# Container.ItemIndex+1+(pageIndex-1)*PAGESIZE %></td>
                <td align="center"><%# Eval("ItemName") %></td>
                <td>
                   <%-- <a href="<%# string.Format("javascript:window.location.href='Tag.aspx?id={0}&tag={1}&type=edit'",Eval("ID"),Eval("ItemName")) %>">编辑</a>
                   --%>
                    <asp:LinkButton ID="lbEdit" CommandArgument='<%# Eval("ID") %>' CommandName="Edit" runat="server">编辑</asp:LinkButton>
                    <asp:LinkButton ID="lbDel" CommandName="Del" CommandArgument='<%# Eval("ID") %>'
                     OnClientClick="return confirm('确定要删除该Tag标签吗?')"
                     runat="server">删除</asp:LinkButton></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr style="background:#fff; height:24px; text-align:center;">
    <td></td>
    <td>
        <asp:TextBox ID="txtTag" runat="server" MaxLength="255"></asp:TextBox></td>
    <td>
        <asp:Button ID="btnAdd" runat="server" Text="保存" onclick="btnAdd_Click" OnClientClick="return valid()"
            style="height: 26px" /></td>
  </tr>
</table><table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo2" runat="server" />
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
<asp:HiddenField ID="hfid" runat="server" />
        </form>
</body>

</html>

<script type="text/javascript" src="<%= JsManage.GetJsFilePath("jquery") %>"></script>
<script type="text/javascript" language="javascript">
    function valid(){
        var tag = $("#<%= txtTag.ClientID %>").val();
        if(tag == ""){
            alert("不能为空");
            return false;
        }else{
        return true;
        }
    }
    function serach(){
        var tag = $("#txtTagName").val();
        window.location.href = "Tag.aspx?tag1="+tag;
    }
    function GetQueryString(name) {

       var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)","i");

       var r = window.location.search.substr(1).match(reg);

       if (r!=null) return decodeURIComponent((r[2])); return null;
    }
    if(GetQueryString("tag1") != null && GetQueryString("tag1") != "")
        $("#txtTagName").val(GetQueryString("tag1"));
</script>