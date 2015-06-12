<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Keyword.aspx.cs" Inherits="SiteOperationsCenter.NewsCenterControl.Keyword" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
<title>关键字组管理</title>

</head>

<body>
<form runat="server">
<table width="100%" border="1" cellspacing="0" cellpadding="0" style="border:1px solid #ccc;">
    <tr style="background:#fff; height:24px; text-align:center;">
    <td colspan="6">
        关键字：<asp:TextBox ID="txtKeywordName" runat="server"></asp:TextBox>
        URL：<asp:TextBox ID="txtUrlName" runat="server"></asp:TextBox>
      <input type="button" value="搜索" onclick="serach()" /></td>
  </tr>
  <tr style="background:#C0DEF3; height:28px; text-align:center; font-weight:bold;">
    <td width="19%">序号</td>
    <td width="41%">关键子组</td>
    <td width="20%">连接</td>
    <td width="20%">操作</td>
  </tr>
    <asp:Repeater ID="repList" runat="server" onitemcommand="repList_ItemCommand">
        <ItemTemplate>
            <tr style="background:#fff; height:24px; text-align:center;" class="modifytd"  tid="<%# Eval("ID") %>">
                <td><%# Container.ItemIndex+1 %></td> 
                <td align="center" class="modifytd" c='<%# Eval("ItemUrl") %>' t="k"><%# Eval("ItemName") %></td>
                <td class="modifytd" c='<%# Eval("ItemName") %>' t="u"><%# Eval("ItemUrl") %></td>
                <td>
                <%--<a href="<%# string.Format("javascript:window.location.href='Keyword.aspx?key={0}&url={1}&type=edit&id={2}'",Eval("ItemName"),Eval("ItemUrl"),Eval("ID")) %>">编辑</a>--%>
                <asp:LinkButton ID="lbEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' runat="server">编辑</asp:LinkButton>
                <asp:LinkButton ID="lbDel" CommandName="Del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('确定要删除该关键字吗?')" runat="server">删除</asp:LinkButton></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr style="background:#fff; height:24px; text-align:center;">
    <td></td>
    <td><asp:TextBox ID="txtKeyword" runat="server" MaxLength="255"></asp:TextBox></td>
    <td><asp:TextBox ID="txtUrl" ImeMode="" runat="server" MaxLength="255"></asp:TextBox></td>
    <td><asp:Button ID="btnAdd" runat="server" Text="保存" OnClientClick="return valid()" onclick="btnAdd_Click" /></td>
  </tr>
  <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo2" runat="server" />
            </td>
        </tr>
    </table>
</table>
<asp:HiddenField ID="hfID" runat="server" />
</form>
    
</body>
</html>

<script type="text/javascript" src="<%= JsManage.GetJsFilePath("jquery") %>"></script>
<script type="text/javascript" language="javascript">
    function valid(){
        var key = $("#<%= txtKeyword.ClientID %>").val();
        if(key == ""){
            alert("不能为空");
            return false;
        }else{
        return true;
        }
    }
    function serach(){
        var key = $("#txtKeywordName").val();
        var url = $("#txtUrlName").val();
        window.location.href = "Keyword.aspx?key1="+encodeURIComponent(key)+"&url1="+url;
    }
    function GetQueryString(name) {

       var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)","i");

       var r = window.location.search.substr(1).match(reg);

       if (r!=null) return decodeURIComponent((r[2])); return null;
    }
    if(GetQueryString("key1") != null && GetQueryString("key1") != "")
        $("#txtKeywordName").val(GetQueryString("key1"));
    if(GetQueryString("url1") != null && GetQueryString("url1") != "")
        $("#txtUrlName").val(GetQueryString("url1"));
     //相当于在页面中的body标签加上onload事件  
 $(function(){  
     //找到所有的td节点  
     var tds=$("td.modifytd");  
     var itemName,itemUrl;
     //给所有的td添加点击事件  
     tds.click(function(){  
         //获得当前点击的对象  
         var td=$(this);
         //取出当前td的文本内容保存起来  
         var oldText=td.text();  
         //建立一个文本框，设置文本框的值为保存的值     
         var input=$("<input type='text' class='modifyinput' value='"+oldText+"'/>");   
         //将当前td对象内容设置为input  
         td.html(input);  
         //设置文本框的点击事件失效  
         input.click(function(){  
             return false;  
         });  
         //设置文本框的样式  
         input.css("border-width","1");                
         input.css("font-size","16px");  
         input.css("text-align","center");  
         //设置文本框宽度等于td的宽度  
         input.width(td.width());  
         //当文本框得到焦点时触发全选事件    
         input.trigger("focus").trigger("select");   
         //当文本框失去焦点时重新变为文本  
         input.blur(function(){  
             var input_blur=$(this);  
             var newText=input_blur.val();   
             var that = $(this);
             var tid = that.parent().parent().attr("tid");
             if(td.attr("t") =="k"){
                itemName = newText;
                itemUrl = td.attr("c");
             }else if(td.attr("t")=="u"){
                itemName = td.attr("c");
                itemUrl = newText;
             }
             $.ajax({
                type: "POST",
                url: "Keyword.aspx?type=upsta",
                data: {tid:tid,k:itemName,u:itemUrl},
                dataType: "json",
                success: function(d) {
                    if(d.msg=="true"){
                        td.html(newText); 
                    }else if(d.msg == "false"){
                        td.html(oldText); 
                        alert("关键字已经存在");
                    }else{
                        td.html(oldText); 
                        alert("更新失败");
                    }
                }
             });
             return false;
         });   
//         //响应键盘事件  
//         input.keyup(function(event){  
//             // 获取键值  
//             var keyEvent=event || window.event;  
//             var key=keyEvent.keyCode;  
//             //获得当前对象  
//             var input_blur=$(this);  
//             switch(key)  
//             {  
//                 case 13://按下回车键，保存当前文本框的内容  
//                     var newText=input_blur.val();   
//                     td.html(newText);   
//                    alert("回车");
//                 break;  
//                   
//                 case 27://按下esc键，取消修改，把文本框变成文本  
//                     td.html(oldText);   
//                 break;  
//             }  
//             alert(newText);
//         });  
     });  
 });  

    
    
</script>   